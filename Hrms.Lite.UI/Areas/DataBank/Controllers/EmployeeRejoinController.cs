using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.DataBank.Generic;
using Hrms.Lite.Shared.General;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.DataBank.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]
    public class EmployeeRejoinController : Controller
    {
        private readonly IEmployeeRejoinService _employeeRejoinService;
        private readonly IDropdownService _dropdownService;
        private readonly ICommonService _commonService;
        private readonly IBlob _blob;
        public EmployeeRejoinController(IEmployeeRejoinService employeeRejoinService, IDropdownService dropdownService, ICommonService commonService, IBlob blob)
        {
            _employeeRejoinService = employeeRejoinService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _blob = blob;
        }
        public async Task<IActionResult> Index()
        {
            try { 
            await fillfilterDropdown();
            OnBoarding model = new OnBoarding();
            OnBoarding m1 = new OnBoarding();
            Filter filter = new Filter();
            filter.length = 10;
            m1 = await _employeeRejoinService.GetRejoinList(filter);
            model.Filter = new Filter();
            return View(model);
        }
            catch (UnauthorizedAccessException e)
            {

                return RedirectToAction("Login", "Account", new { Area = "" });
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Account", new { Area = "" });
            }
        }
        public async Task fillfilterDropdown()
        {

            ViewBag.Gen = new SelectList(await _dropdownService.GetGenderDropDown(), "Code", "Name");
            ViewBag.Cat = new SelectList(await _dropdownService.GetCategoryDropdown(), "Code", "Name");
            ViewBag.Grd = new SelectList(await _dropdownService.GetGradeDropdown(), "Code", "Name");
            ViewBag.Dsg = new SelectList(await _dropdownService.GetDesignationDropdown(), "Code", "Name");
            ViewBag.Dept = new SelectList(await _dropdownService.GetDepartmentDropdown(), "Code", "Name");
            ViewBag.Loc = new SelectList(await _dropdownService.GetLocationDropdown(), "Code", "Name");
            ViewBag.PayrollGrp = new SelectList(await _dropdownService.GetPayrollGroupDropdown(), "Code", "Name");

            ViewBag.division = new SelectList(await _dropdownService.GetDivisionDropdown(), "Code", "Name");
            ViewBag.section = new SelectList(await _dropdownService.GetSectionDropdown(), "Code", "Name");
            ViewBag.EmploymentType = new SelectList(await _dropdownService.GetEmploymentTypeDropdown(), "Code", "Name");
            ViewBag.EmploymentStatus = new SelectList(await _dropdownService.GetEmploymentStatusDropdown(), "Code", "Name");
            ViewBag.WorkLocation = new SelectList(await _dropdownService.GetWorkLocationDropdown(), "Code", "Name");
            ViewBag.WageGrade = new SelectList(await _dropdownService.GetWageGradeDropdown(), "Code", "Name");


        }
        [HttpPost]
        public async Task<IActionResult> GetList(Filter filter1)
        {
            try
            {
                OnBoarding model = new OnBoarding();
               


                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }

                model = await _employeeRejoinService.GetRejoinList(filter1);

               
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.OnBoardingList == null || model.OnBoardingList.Count() == 0)
                {


                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }




                if (model.OnBoardingList.Count > 0)
                {
                    totalrows = model.OnBoardingList[0].active;

                    //m.OnBoardingList.ToList<Hrms.Lite.Shared.Master.Test>().Count; ;
                }

                //model.AllTabCount = m.AllTabCount;
                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = model.OnBoardingList[0].totalRecords;
                    return Json(new
                    {
                        data = model.OnBoardingList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    // return Json(new { data = model.EmployeeProfileList, draw = filter1.draw, recordsTotal = totalrows, recordsFiltered = totalrows });
                    return Json(new
                    {
                        data = model.OnBoardingList,
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }


            }
            catch (Exception ex)
            {
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create(Guid EnrollGI,string OriginalFileName)     {
            try
            {
                string jsonData = HttpContext.Session.GetString("Data");
                HttpContext.Session.SetString("Data","");
                if (jsonData == null || jsonData == "")
                {                             

                    await fillDropdown();                    
                    JoiningConfirmation model = new JoiningConfirmation();
                    Enrollment model2 = new Enrollment();
                    var response = await _commonService.CheckEmptyDropDown("Enroll");
                    if (response.Success is false)   //If Any Empty DropDown
                    {
                        TempData["Message"] = response.Message;
                        return RedirectToAction("Index", "OnBoarding");
                    }
                    else
                    {
                        model2.PreEnrollment = await _commonService.GetMandatoryFiledValidation("ENROLL");
                        List<FiledValidations> list = model2.PreEnrollment.FiledValidations;
                        model = await _employeeRejoinService.GetRejoinEmployeeDetails(EnrollGI);
                        model.Enrollment.PreEnrollment.FiledValidations = list;
                        model.Enrollment.AddOnClassification = await _commonService.GetAddOnClassificationApplicability();
                        model.Enrollment.EnrollGUID = EnrollGI;
                        if (model.Enrollment.AttendanceSettings.ShiftDetailsIds != null && model.Enrollment.AttendanceSettings.ShiftDetailsIds.Count() > 0)
                            model.Enrollment.AttendanceSettings.ShiftDetailsId = model.Enrollment.AttendanceSettings.ShiftDetailsIds[0];
                        ViewBag.St = new SelectList(await _dropdownService.GetStateDropDown(model.Enrollment.PreEnrollment.PermanentAddress.Country.Code), "Code", "Name");
                        ViewBag.Dist = new SelectList(await _dropdownService.GetDistrictDropdown(model.Enrollment.PreEnrollment.PermanentAddress.State.Code), "Code", "Name");
                        model.Enrollment.Breadcrumb = "Employee Rejoin";                        
                        model.Enrollment.SelectedTab = "Personal-Info";
                        model.Enrollment.EmployeeSettings.EmployeeID = "";
                        model.Enrollment.EmployeeSettings.PunchingID = "";
                        if (model.Enrollment.PreEnrollment.BasicInfo != null)
                        {
                            ViewBag.FileName = model.Enrollment.PreEnrollment.BasicInfo.File.FileName;
                        }
                        return View("Create", model.Enrollment);
                    }

                }
                else
                {
                    await fillDropdown();
                    Enrollment model = JsonConvert.DeserializeObject<Enrollment>(jsonData);
                    ViewBag.St = new SelectList(await _dropdownService.GetStateDropDown(model.PreEnrollment.PermanentAddress.Country.Code), "Code", "Name");
                    ViewBag.Dist = new SelectList(await _dropdownService.GetDistrictDropdown(model.PreEnrollment.PermanentAddress.State.Code), "Code", "Name");
                    model.PreEnrollment.BasicInfo.File = new Shared.File();                    
                    model.PreEnrollment.BasicInfo.File.FileName = OriginalFileName;
             
                  
                    if (model.PreEnrollment.BasicInfo != null)
                    {
                        ViewBag.FileName = model.PreEnrollment.BasicInfo.File.FileName;
                        var ext = Path.GetExtension(ViewBag.FileName);

                        string dataUri = $"data:image/{ext.Substring(1)};base64,{Convert.ToBase64String(model.PreEnrollment.BasicInfo.File.fdata)}";
                        ViewBag.dataUri = dataUri;
                    }
                    return View("Create", model);
                }
            }
            catch (UnauthorizedAccessException e)
            {

                return RedirectToAction("Login", "Account", new { Area = "" });
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Account", new { Area = "" });
            }
        }
        public async Task fillDropdown()
        {
            ViewBag.Sal = new SelectList(await _dropdownService.GetSalutationDropDown(), "Code", "Name");
            ViewBag.Gen = new SelectList(await _dropdownService.GetGenderDropDown(), "Code", "Name");
            ViewBag.Cat = new SelectList(await _dropdownService.GetCategoryDropdown(), "Code", "Name");
            ViewBag.Grd = new SelectList(await _dropdownService.GetGradeDropdown(), "Code", "Name");
            ViewBag.Dsg = new SelectList(await _dropdownService.GetDesignationDropdown(), "Code", "Name");
            ViewBag.Dept = new SelectList(await _dropdownService.GetDepartmentDropdown(), "Code", "Name");
            ViewBag.Loc = new SelectList(await _dropdownService.GetLocationDropdown(), "Code", "Name");
            ViewBag.PayrollGrp = new SelectList(await _dropdownService.GetPayrollGroupDropdown(), "Code", "Name");
            ViewBag.Cntry = new SelectList(await _dropdownService.GetCountryDropDown(), "Code", "Name");

            ViewBag.SftType = new SelectList(await _dropdownService.GetShiftTypeDropDown(), "Code", "Name");
            ViewBag.SftDetails = new SelectList(await _dropdownService.GetShiftDetailsDropdown(), "Code", "Name");
            ViewBag.Punching = new SelectList(await _dropdownService.GetPunchingMethodDropdown(), "Code", "Name");
            ViewBag.LeavGroup = new SelectList(await _dropdownService.GetLeaveGroupFilterDropdown(), "Code", "Name");
            ViewBag.HolidayGrp = new SelectList(await _dropdownService.GetHolidayGroupDropdown(), "Code", "Name");
            ViewBag.YearlyOffDaySettings = new SelectList(await _dropdownService.GetOffDaySettings_Dropdown(), "Code", "Name");



            ViewBag.Payment = new SelectList(await _dropdownService.GetPaymentModeDropDown(), "Code", "Name");
            ViewBag.BankName = new SelectList(await _dropdownService.GetBankDropdown(), "Code", "Name");
            ViewBag.PFGroup = new SelectList(await _dropdownService.GetPFGroupDropdown(), "Code", "Name");
            ViewBag.ESIGroup = new SelectList(await _dropdownService.GetESIGroupDropDown(), "Code", "Name");
            ViewBag.PTGroup = new SelectList(await _dropdownService.GetPTGroupDropDown(), "Code", "Name");



            ViewBag.ReportingMgr = new SelectList(await _dropdownService.GetReportingManagerDropdown(), "Code", "Name");
            ViewBag.Div = new SelectList(await _dropdownService.GetDivisionDropdown(), "Code", "Name");
            ViewBag.Sec = new SelectList(await _dropdownService.GetSectionDropdown(), "Code", "Name");
            ViewBag.Emt = new SelectList(await _dropdownService.GetEmploymentTypeDropdown(), "Code", "Name");
            ViewBag.Wlo = new SelectList(await _dropdownService.GetWorkLocationDropdown(), "Code", "Name");
            ViewBag.Wgr = new SelectList(await _dropdownService.GetWageGradeDropdown(), "Code", "Name");

            ViewBag.SlryStruct = new SelectList(await _dropdownService.GeSalaryStructureDropDown(), "Code", "Name");
            ViewBag.VDAGroup = new SelectList(await _dropdownService.GetVDACombo(), "Code", "Name");
            ViewBag.ShiftCircle = new SelectList(await _dropdownService.GetDutyRosterCircleDropdown(), "Code", "Name");
            ViewBag.Relation = new SelectList(await _dropdownService.GetPreenrollRelationDropDown(), "Code", "Name");

        }

        [HttpPost]
        public async Task<IActionResult> Create(Enrollment model, DateTime Renewaldt)
        {
            model.EmpGuid = model.EnrollGUID;
            string filename = null;
            model.EnrollGUID = Guid.Parse("00000000-0000-0000-0000-000000000000");
            
            model.PreEnrollment.BasicInfo.RenewalDate = Renewaldt;
            if (model.AttendanceSettings.ShiftDetailsId != null)
            {
                model.AttendanceSettings.ShiftDetailsIds = new int[1];
                model.AttendanceSettings.ShiftDetailsIds[0] = (int)model.AttendanceSettings.ShiftDetailsId;
            }
            var response = await _employeeRejoinService.Save(model);
            if (response.Success == true)
            {
                return Json(response);
            }
            else
            {
                
                return Json(response);
            }

        }


    }
}
