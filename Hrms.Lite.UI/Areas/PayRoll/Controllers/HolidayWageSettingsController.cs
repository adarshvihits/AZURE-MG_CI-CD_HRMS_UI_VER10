using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.PayRoll;
using Hrms.Lite.Shared.PayRoll.AllowanceSettings;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.PayRoll;

using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Spire.Xls;
using System.Data;
using Hrms.Lite.Shared.Account;

namespace Hrms.Lite.UI.Areas.PayRoll.Controllers
{
    [Area("PayRoll")]
    [SessionAuthorize]
    public class HolidayWageSettingsController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly IHolidayWageSettingsService _HolidayWageSettingsService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;

        public static class Global
        {
            public static int Allowance;
            public static Filter GlobalFilter;

        }

        public HolidayWageSettingsController(IHolidayWageSettingsService HolidayWageSettingsService, IDropdownService dropdownService, ICommonService commonService, IHelpers helpers)
        {
            _HolidayWageSettingsService = HolidayWageSettingsService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                ApplicableAllowance model = new ApplicableAllowance();
                AllowanceSettingsEmployeeList m = new AllowanceSettingsEmployeeList();

                Filter filter = new Filter();
                filter.length = 10;
                TempData.Remove("Filter_HolidayWage");
                ViewBag.Allowance = new SelectList(await _dropdownService.GetHolidayWageSettingsAllowanceDropdown(), "Code", "Name");
                ViewBag.CalculationType = new SelectList(await _dropdownService.GetCalculationTypeDropdown(), "Code", "Name");

                await fillfilterDropdown();

                m.SettingsEmployeeList = await _HolidayWageSettingsService.GetSettingsEmployeeList(0, filter);
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

        public async Task<IActionResult> ApplyFilter(Filter filter)
        {
            try {
                AllowanceSettingsEmployeeList model = new AllowanceSettingsEmployeeList();
                Global.GlobalFilter = filter;
                model.SettingsEmployeeList = await _HolidayWageSettingsService.GetSettingsEmployeeList(Global.Allowance, filter);
                ViewBag.CalculationType = new SelectList(await _dropdownService.GetCalculationTypeDropdown(), "Code", "Name");
                return PartialView("_SettingsEmployeeList", model);
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

      [HttpGet]
        public async Task<IActionResult> GetSettingsEmployeeList(int Allowance)
        {
            try { 
            AllowanceSettingsEmployeeList model = new AllowanceSettingsEmployeeList();
            ViewBag.CalculationType = new SelectList(await _dropdownService.GetCalculationTypeDropdown(), "Code", "Name");
            Global.Allowance = Allowance;
            Filter filter = new Filter();
            model.SettingsEmployeeList = (List<AllowanceSettingsEmployeeList>)await _HolidayWageSettingsService.GetSettingsEmployeeList(Allowance, filter);

            TempData["AllowanceToFilter"]  = Allowance;

            return PartialView("_SettingsEmployeeList", model);
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
        public async Task<IActionResult> GetBatchUpdateDetails(int AllowanceCode, string AllowanceName)
        {
            try { 
            AllowanceSettingsBatchUpdate model = new AllowanceSettingsBatchUpdate();
            model.Allowance= new Allowance();
           
            ViewBag.CalculationType = new SelectList(await _dropdownService.GetCalculationTypeDropdown(), "Code", "Name");

            Filter filter = new Filter();
            model.AllowanceCode = AllowanceCode;
            model.Allowance.Name = AllowanceName;
          
            return PartialView("_BatchUpdate",model);
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

        public async Task<IActionResult> GetDefaultSettingsList()
        {


            try { 
            ViewBag.CalculationType = new SelectList(await _dropdownService.GetCalculationTypeDropdown(), "Code", "Name");

            



                AllowanceSettingsBatchUpdate model = new AllowanceSettingsBatchUpdate();

                model = await _HolidayWageSettingsService.GetDefaultSettingsDetails();

                return PartialView("_DefaultSettings", model);

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
        public async Task<IActionResult> GetApplicableAllowanceList()
        {
            try { 
            ApplicableAllowance model = new ApplicableAllowance();
            model.Allowance = new Allowance();
            model = await _HolidayWageSettingsService.GetApplicableAllowanceList();
            return PartialView("_ApplicableAllowance", model);
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
        [HttpPost]
        public async Task<IActionResult> SaveApplicableAllowance(ApplicableAllowance model)
        {
            try { 
            if (model.Allowance.AllowanceList != null)
            {
                var abc = from li in model.Allowance.AllowanceList
                          where li.Applicable.Equals(true)
                          select li;
                model.Allowance.AllowanceList = abc.ToList();
            }



            var response = await _HolidayWageSettingsService.SaveApplicableAllowance(model);
            if(response.Success==true)
            {
                response.Message = "Saved Successfully";
            }
            return Json(response);
                //if (response.Success == true)
                //{
                //    TempData["Message"] = "Saved Successfully";
                //    return RedirectToAction("Index", "HolidayWageSettings");
                //}
                //else
                //{
                //    TempData["Message"] = response.Message;

                //    return View("~/Areas/PayRoll/Views/HolidayWageSettings/_ApplicableAllowance.cshtml", model);
                //}
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

        [HttpPost]
        public async Task<ActionResult> SaveEmployeeWiseSettings(int UpdatedRate,int UpdatedCalcType,Guid EmployeeGI, int AllowanceCode)
        {
            try { 
            AllowanceSettingsSave model = new AllowanceSettingsSave();
           
            model.EmployeeGI = EmployeeGI;
            model.Rate = UpdatedRate;
            model.CalcTypeCode = UpdatedCalcType;
            model.AllowanceCode = AllowanceCode;


            var response = await _HolidayWageSettingsService.SaveEmployeeWiseSettings(model);
            if (response.Success == true)
            {
                response.Message = "Updated Successfully";
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


        [HttpPost]
        public async Task<IActionResult> GetWagesConfirmationList(Filter filter1, int Allowance)
        {
            try
            {
                AllowanceSettingsEmployeeList model = new AllowanceSettingsEmployeeList();
                AllowanceSettingsEmployeeList m = new AllowanceSettingsEmployeeList();

                Filter filter = new Filter();
                Global.GlobalFilter = filter1;

                //m.GridSettings = new List<LeaveRegisterReport_GridSettings>();
                //m = await _leaveRegisterService.GetGridSettings();
                //model.GridSettings.Add(new LeaveRegisterReport_GridSettings());
                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }
                //model.filter = filter1;
                //model.GridSettings = m.GridSettings;
                //model = await _TDSConfirmationService.GetPFEmployeeWiseSettingsList(Deduction, Month, filter1);
                if (Allowance!=0)
                {
                    model.SettingsEmployeeList = (List<AllowanceSettingsEmployeeList>)await _HolidayWageSettingsService.GetSettingsEmployeeList(Allowance, filter1);

                }

                m.SettingsEmployeeList = model.SettingsEmployeeList;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;


                if (model == null || model.SettingsEmployeeList == null || model.SettingsEmployeeList.Count() == 0)
                {
                    m.SettingsEmployeeList = new List<AllowanceSettingsEmployeeList>();
                    model = new AllowanceSettingsEmployeeList();
                    model.SettingsEmployeeList = new List<AllowanceSettingsEmployeeList>();
                }
                else
                {
                    m.SettingsEmployeeList = model.SettingsEmployeeList;
                }

                if (model.SettingsEmployeeList == null || model.SettingsEmployeeList.Count() == 0)
                {
                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });

                }
                if (model.SettingsEmployeeList.Count > 0)
                {
                    totalrows = m.SettingsEmployeeList[0].active;

                    //m.OnBoardingList.ToList<Hrms.Lite.Shared.Master.Test>().Count; ;
                }


                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.SettingsEmployeeList[0].totalRecords;
                    //return Json(new { data = model.EmployeeProfileList, draw = filter1.draw, recordsTotal = totalrows, recordsFiltered = totalrowsafterfiltering });
                    return Json(new
                    {
                        data = model.SettingsEmployeeList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {

                    return Json(new
                    {
                        data = model.SettingsEmployeeList,
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




        public async Task<IActionResult> BatchUpdateEmployeeWiseSettings(AllowanceSettingsBatchUpdate model)
        {
            try { 

            Guid EmployeeGI = model.EmployeeGI;
            if (Global.GlobalFilter == null)
                model.filter = new Filter();
            else
                model.filter = Global.GlobalFilter;

            var response = await _HolidayWageSettingsService.BatchUpdateEmployeeWiseSettings(model);
            if (response.Success == true)
            {
                response.Message = "Updated Successfully";
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
        public async Task<ActionResult> SaveDefaultSettings(AllowanceSettingsBatchUpdate model)
        {
            //AllowanceSettingsSave model = new AllowanceSettingsSave();

            //  model.EmployeeGI = EmployeeGI;
            //model.Rate = Rate;
            //model.CalcTypeCode = CalcType;
            //   model.AllowanceCode = AllowanceCode;

            try { 
            var response = await _HolidayWageSettingsService.SaveDefaultSettings(model);
            if (response.Success == true)
            {
                response.Message = "Saved Successfully";
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
        
        public async Task<IActionResult> ExportToExcel(int Allowance, string allowanceName,int totRecords)
        {
            try { 
            AllowanceSettingsEmployeeList model = new AllowanceSettingsEmployeeList();
            if (Global.GlobalFilter == null)
                Global.GlobalFilter = new Filter();
                else
                {
                    Global.GlobalFilter.length = totRecords;
                    Global.GlobalFilter.start = 0;
                }
            string FilterValues = await _helpers.GetFilterValues(Global.GlobalFilter);



                // model.SettingsEmployeeList = (List<AllowanceSettingsEmployeeList>)await _HolidayWageSettingsService.GetSettingsEmployeeList(Allowance, Global.GlobalFilter);
                var LoginUser = HttpContext.Session.GetString("LoginUser");
            var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Holiday Wage Settings (" + allowanceName + ")", "Excel");
                int slNo = 0;
            if (FilterValues == "")
            {
                FilterValues = "All";
            }
            string currDateTime = await _helpers.GetCurrentDate();
            



                var dataset = await _HolidayWageSettingsService.GetSettingsEmployeeList_Excel(Allowance, Global.GlobalFilter);
                DataTable dt = dataset.Tables[0];

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName == "doj" || dt.Columns[i].ColumnName == "dob" || dt.Columns[i].ColumnName == "y/s")
                        dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                    else
                        dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                }





                using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("HolidayWage Settings List");
             
                    int count = dt.Rows.Count + 5;


                worksheet.Range("A1:H" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1:H" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1:H" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1:H" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1:H" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 7)));
                wsReportNameHeaderRange.Style.Font.Bold = true;
                wsReportNameHeaderRange.Style.Font.FontSize = 15;
                wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                wsReportNameHeaderRange.Merge();
                wsReportNameHeaderRange.Value = "Holiday Wage Settings";

                worksheet.Cell(2, 1).Style.Font.Bold = true;
                worksheet.Cell(2, 1).Value = "Group Company Name";
                worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                worksheet.Cell(2, 3).Style.Font.Bold = true;
                worksheet.Cell(2, 3).Value = "Holiday Wage Allowance";
                worksheet.Cell(2, 4).Value = allowanceName;

             
                worksheet.Cell(3, 1).Style.Font.Bold = true;
                worksheet.Cell(3, 1).Value = "Login User";
                worksheet.Cell(3, 2).Value = LoginUser;
                worksheet.Cell(3, 3).Style.Font.Bold = true;
                worksheet.Cell(3, 3).Value = "Date & Time";
                worksheet.Cell(3, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(3, 4).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
                worksheet.Cell(3, 4).Value = currDateTime.ToString();
                worksheet.Cell(3, 5).Style.Font.Bold = true;
                worksheet.Cell(3, 5).Value = "Total Count";
                worksheet.Cell(3, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(3, 6).Value = dt.Rows.Count;
                    worksheet.Cell(4, 1).Style.Font.Bold = true;
                worksheet.Cell(4, 1).Value = "Filter Applied";
                worksheet.Range("B4:H4").Merge();
                worksheet.Cell("B4").Value = FilterValues;






                var currentRow = 5;
                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A5:Q5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A5:Q5").Style.Font.Bold = true;


                    worksheet.Columns().AdjustToContents();
                    worksheet.Rows().AdjustToContents();
                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        if (EP.NeedPassword)
                        {
                            Workbook book = new Workbook();
                            book.LoadFromStream(stream);
                            //Protect Workbook
                            book.Protect(EP.Password,false,false);
                            book.SaveToStream(stream);
                        }
                        var content = stream.ToArray();
                        return File(
                            content,
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            "Holiday wage Settings List.xlsx"
                            );
                    }
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
        //public async Task fillfilterDropdown()

        //{

        //    ViewBag.Gen = new SelectList(await _dropdownService.GetGenderDropDown(), "Code", "Name");
        //    ViewBag.Cat = new SelectList(await _dropdownService.GetCategoryDropdown(), "Code", "Name");
        //    ViewBag.Grd = new SelectList(await _dropdownService.GetGradeDropdown(), "Code", "Name");
        //    ViewBag.Dsg = new SelectList(await _dropdownService.GetDesignationDropdown(), "Code", "Name");
        //    ViewBag.Dept = new SelectList(await _dropdownService.GetDepartmentDropdown(), "Code", "Name");
        //    ViewBag.Loc = new SelectList(await _dropdownService.GetLocationDropdown(), "Code", "Name");
        //    ViewBag.PayrollGrp = new SelectList(await _dropdownService.GetPayrollGroupDropdown(), "Code", "Name");

        //    ViewBag.division = new SelectList(await _dropdownService.GetDivisionDropdown(), "Code", "Name");
        //    ViewBag.section = new SelectList(await _dropdownService.GetSectionDropdown(), "Code", "Name");
        //    ViewBag.EmploymentType = new SelectList(await _dropdownService.GetEmploymentTypeDropdown(), "Code", "Name");
        //    ViewBag.EmploymentStatus = new SelectList(await _dropdownService.GetEmploymentStatusDropdown(), "Code", "Name");
        //    ViewBag.WorkLocation = new SelectList(await _dropdownService.GetWorkLocationDropdown(), "Code", "Name");
        //    ViewBag.WageGrade = new SelectList(await _dropdownService.GetWageGradeDropdown(), "Code", "Name");
        //    ViewBag.CalculationType = new SelectList(await _dropdownService.GetCalculationTypeDropdown(), "Code", "Name");

        //}
        //public async Task<IActionResult> ApplyFilterLoad()
        //{
        //    Filter model = new Filter();
        //    if (TempData["Filter_HolidayWage"] != null)
        //    {
        //        model = JsonConvert.DeserializeObject<Filter>(TempData["Filter_HolidayWage"].ToString());
        //    }
        //    //   model.SearchId = SearChId;
        //    //   model.Tabindex = TabIndex;
        //    TempData["Filter_HolidayWage"] = JsonConvert.SerializeObject(model);
        //    await fillfilterDropdown();
        //    return PartialView("_Filter", model);
        //}
        //public async Task<IActionResult> ApplyFilter(Filter filter,int Allowance )
        //{
        //    ViewBag.CalculationType = new SelectList(await _dropdownService.GetCalculationTypeDropdown(), "Code", "Name");
        //    //int AllowanceCode = (int)TempData["AllowanceToFilter"];
        //    AllowanceSettingsEmployeeList model = new AllowanceSettingsEmployeeList();
        //    model.SettingsEmployeeList = (List<AllowanceSettingsEmployeeList>)await _HolidayWageSettingsService.GetSettingsEmployeeList(Allowance, filter);
        //    TempData["Filter_HolidayWage"] = JsonConvert.SerializeObject(filter);

        //    return PartialView("_SettingsEmployeeList", model);


        //}
        [HttpGet]
        public async Task<JsonResult> GetOvertimeSettingsAllowanceDropdown()
        {
            var Allowance = new SelectList(await _dropdownService.GetOvertimeSettingsAllowanceDropdown(), "Code", "Name");
            return Json(Allowance);
        }
    }
}
