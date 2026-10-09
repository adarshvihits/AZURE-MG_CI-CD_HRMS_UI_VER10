using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.TDS;

using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TDS;

namespace Hrms.Lite.UI.Areas.TDS.Controllers
{
    [Area("TDS")]
    [SessionAuthorize]
    public class PreviousEmployerController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly IPreviousEmployerHRMSService _service;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        private readonly IBlob _blob;
        public static class Global
        {
          
            public static Filter GlobalFilter;

        }
        public PreviousEmployerController(IPreviousEmployerHRMSService service, IDropdownService dropdownService, ICommonService commonService, IBlob blob, IHelpers helpers)
        {
            _service = service;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;
            _blob = blob;
        }
        public async Task<IActionResult> Index()
        {
            PreviousEmployer model = new PreviousEmployer();
            ViewBag.AssessmentYear = new SelectList(await _dropdownService.GetAssessmentYearDropdown(), "Code", "Name");
            await fillfilterDropdown();
            return View(model);
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
            ViewBag.WorkLocation = new SelectList(await _dropdownService.GetWorkLocationDropdown(), "Code", "Name");
            ViewBag.WageGrade = new SelectList(await _dropdownService.GetWageGradeDropdown(), "Code", "Name");

        }

        [HttpPost]
        public async Task<IActionResult> GetTDSEmployeeList(Filter filter1, int AssessmentYear)
        {
            try
            {
                PreviousEmployer model = new PreviousEmployer();
                PreviousEmployer m = new PreviousEmployer();
                Filter filter = new Filter();
                Global.GlobalFilter = filter1;


                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }

                if (AssessmentYear != 0)
                {
                    model = await _service.GetList(AssessmentYear, filter1);

                }
                m.TDSEmployeeList = model.TDSEmployeeList;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.TDSEmployeeList == null || model.TDSEmployeeList.Count() == 0)
                {


                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }




                if (model.TDSEmployeeList.Count > 0)
                {
                    totalrows = m.TDSEmployeeList[0].active;


                }

                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.TDSEmployeeList[0].totalRecords;
                    return Json(new
                    {
                        data = model.TDSEmployeeList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    return Json(new
                    {
                        data = model.TDSEmployeeList,
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

        public async Task<IActionResult> Create(Guid EmployeeGI, int AssessmentYear)
        {
            PreviousEmployer model = new PreviousEmployer();
            ViewBag.AssessmentYear = new SelectList(await _dropdownService.GetAssessmentYearDropdown(), "Code", "Name");

            model = await _service.GetPreviousEmployerDetails(EmployeeGI);
            if (model.PrevEmployerName == null)
            {
                model.PrevEmployerToDate = DateTime.Now;
                model.PrevEmployerFromDate = DateTime.Now;
            }
            model.Employee = new EmployeeHeader();
            //model.Employee.EmployeeGuid = EmployeeGI;
            model.Employee = await _commonService.GetEmployeeHeader(EmployeeGI);
            model.AssessmentYear = new MasterBase();
            model.AssessmentYear.Code = AssessmentYear;
            return View(model);
        }

        [HttpPost]

        public async Task<IActionResult> SavePreviousEmployer(PreviousEmployer model)
        {
            try
            {
          

                if (model.File1 != null)
                {
                    if (model.File1.FileData != null)
                    {
                        var FileData = model.File1.FileData;
                        model.File1 = await _blob.UploadToAzureAsync(model.File1, "Documents");
                        string filename = model.File1.FileName;
                        model.File1.FileData = null;
                    }
                }


                if (model.File2 != null)
                {
                    if (model.File2.FileData != null)
                    {
                        var FileData = model.File2.FileData;
                        model.File2 = await _blob.UploadToAzureAsync(model.File2, "Documents");
                        string filename = model.File2.FileName;
                        model.File2.FileData = null;
                    }
                }

                var response = await _service.SavePreviousEmployer(model);
                if (response.Success == false && model.File1 != null)
                    await _blob.Delete(model.File1.FileName);

                if (response.Success == false && model.File2 != null)
                    await _blob.Delete(model.File2.FileName);
                return Json(response);
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

        public async Task<IActionResult> DeletePreviousEmployerData(Guid EmployeeGI,string Filename1,string Filename2)
        {
            try {
                var response = await _service.DeletePreviousEmployer(EmployeeGI);
                if (response.Success == true)
                { 
                    if(Filename1 != "" && Filename1 != null)
                        await _blob.Delete(Filename1);
                    if(Filename2 != "" && Filename2 != null)
                        await _blob.Delete(Filename2);
                }
                return Json(response);
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
    }
}
