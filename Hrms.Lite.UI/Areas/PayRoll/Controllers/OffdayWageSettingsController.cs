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
using Spire.Xls;
using System.Data;
using Newtonsoft.Json;
using Hrms.Lite.Shared.Account;

namespace Hrms.Lite.UI.Areas.PayRoll.Controllers
{
    [Area("PayRoll")]
    [SessionAuthorize]
    public class OffdayWageSettingsController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly IOffdayWageSettingsService __OffdayWageSettingsService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;

        public static class Global
        {
            public static int Allowance;
            public static Filter GlobalFilter;

        }

        public OffdayWageSettingsController(IOffdayWageSettingsService OffdayWageSettingsService, IDropdownService dropdownService, ICommonService commonService, IBlob blob,IHelpers helpers)
        {
            __OffdayWageSettingsService = OffdayWageSettingsService;
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
                ViewBag.CalculationType = new SelectList(await _dropdownService.GetCalculationTypeDropdown(), "Code", "Name");
                Filter filter = new Filter();
                filter.length = 10;
                ViewBag.Allowance = new SelectList(await _dropdownService.GetOffdayWageSettingsAllowanceDropdown(), "Code", "Name");
                await fillfilterDropdown();
                m.SettingsEmployeeList = await __OffdayWageSettingsService.GetSettingsEmployeeList(0, filter);

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
            model.SettingsEmployeeList = await __OffdayWageSettingsService.GetSettingsEmployeeList(Global.Allowance, filter);
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


        public async Task<IActionResult> GetApplicableAllowanceList()
        {
            try { 
            ApplicableAllowance model = new ApplicableAllowance();
            model.Allowance = new Allowance();
            model = await __OffdayWageSettingsService.GetApplicableAllowanceList();
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
        public async Task<IActionResult> GetList(Filter filter1,int Allowance)
        {
            try
            {
                AllowanceSettingsEmployeeList model = new AllowanceSettingsEmployeeList();
                AllowanceSettingsEmployeeList m = new AllowanceSettingsEmployeeList();
                //model.EmployeeHeader = new EmployeeHeader();




                Filter filter = new Filter();
                Global.GlobalFilter = filter1;


                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }
                if (Allowance != 0)
                {

                    model.SettingsEmployeeList = (List<AllowanceSettingsEmployeeList>)await __OffdayWageSettingsService.GetSettingsEmployeeList(Allowance, filter1);
                }
                m.SettingsEmployeeList = model.SettingsEmployeeList;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

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

                }


                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.SettingsEmployeeList[0].totalRecords;
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







        public async Task<IActionResult> GetSettingsEmployeeList(int Allowance)
        {
            try { 
            AllowanceSettingsEmployeeList model = new AllowanceSettingsEmployeeList();
            ViewBag.CalculationType = new SelectList(await _dropdownService.GetCalculationTypeDropdown(), "Code", "Name");
            Global.Allowance = Allowance;
            Filter filter = new Filter();
            model.SettingsEmployeeList = (List<AllowanceSettingsEmployeeList>)await __OffdayWageSettingsService.GetSettingsEmployeeList(Allowance, filter);

            TempData["AllowanceToFilter"] = Allowance;

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
            model.Allowance = new Allowance();

            ViewBag.CalculationType = new SelectList(await _dropdownService.GetCalculationTypeDropdown(), "Code", "Name");

            Filter filter = new Filter();
            model.AllowanceCode = AllowanceCode;
            model.Allowance.Name = AllowanceName;

            return PartialView("_BatchUpdate", model);
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

            

            var response = await __OffdayWageSettingsService.SaveApplicableAllowance(model);
            return Json(response);
                //if (response.Success == true)
                //{
                //    TempData["Message"] = "Saved Successfully";
                //    return RedirectToAction("Index", "OffdayWageSettings");
                //}
                //else
                //{
                //    TempData["Message"] = response.Message;

                //    return View("~/Areas/PayRoll/Views/OffdayWageSettings/_ApplicableAllowance.cshtml", model);
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

        public async Task<IActionResult> GetDefaultSettingsList()
        {

            try { 
            ViewBag.CalculationType = new SelectList(await _dropdownService.GetCalculationTypeDropdown(), "Code", "Name");

            AllowanceSettingsBatchUpdate model = new AllowanceSettingsBatchUpdate();

           model = await __OffdayWageSettingsService.GetDefaultSettingsDetails();
            //return Json(response);

            return PartialView("_DefaultSettings",model);
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
        public async Task<ActionResult> SaveEmployeeWiseSettings(int UpdatedRate, int UpdatedCalcType, Guid EmployeeGI, int AllowanceCode)
        {
            try { 
            AllowanceSettingsSave model = new AllowanceSettingsSave();

            model.EmployeeGI = EmployeeGI;
            model.Rate = UpdatedRate;
            model.CalcTypeCode = UpdatedCalcType;
            model.AllowanceCode = AllowanceCode;


            var response = await __OffdayWageSettingsService.SaveEmployeeWiseSettings(model);
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
        public async Task<IActionResult> BatchUpdateEmployeeWiseSettings(AllowanceSettingsBatchUpdate model)
        {
            try { 

            Guid EmployeeGI = model.EmployeeGI;
            if (Global.GlobalFilter == null)
                model.filter = new Filter();
            else
                model.filter = Global.GlobalFilter;

            var response = await __OffdayWageSettingsService.BatchUpdateEmployeeWiseSettings(model);
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
        public async Task<ActionResult> SaveDefaultSettings(AllowanceSettingsBatchUpdate model)
        {
            try { 

            var response = await __OffdayWageSettingsService.SaveDefaultSettings(model);
            response.Message = "SAVED SUCCESSFULLY";
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
        
        public async Task<IActionResult> ExportToExcel(int Allowance,string AllowanceName,int totRecords)
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

           // model.SettingsEmployeeList = (List<AllowanceSettingsEmployeeList>)await __OffdayWageSettingsService.GetSettingsEmployeeList(Allowance, Global.GlobalFilter);


            var LoginUser = HttpContext.Session.GetString("LoginUser");
            var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Offday Wage Settings (" + AllowanceName + ")", "Excel");
                int slNo = 0;
            if (FilterValues == "")
            {
                FilterValues = "All";
            }
            string currDateTime = await _helpers.GetCurrentDate();
            


                var dataset = await __OffdayWageSettingsService.GetSettingsEmployeeList_Excel(Allowance, Global.GlobalFilter);
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
                var worksheet = workbook.Worksheets.Add("OffdayWage Settings List");
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
                wsReportNameHeaderRange.Value = "Offday Wage Settings";

                worksheet.Cell(2, 1).Style.Font.Bold = true;
                worksheet.Cell(2, 1).Value = "Group Company Name";
                worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                worksheet.Cell(2, 3).Style.Font.Bold = true;
                worksheet.Cell(2, 3).Value = "Offday Wage Allowance";
                worksheet.Cell(2, 4).Value = AllowanceName;

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


                    #region Body

                    #endregion
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
                            "Offday Wage Settings List.xlsx"
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
        [HttpGet]
        public async Task<JsonResult> GetOffdayWageSettingsAllowanceDropdown()
        {
            var Allowance = new SelectList(await _dropdownService.GetOffdayWageSettingsAllowanceDropdown(), "Code", "Name");
            return Json(Allowance);
        }
    }
}
