using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.PayRoll;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Hrms.Lite.Shared.PayRoll;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Spire.Xls;

namespace Hrms.Lite.UI.Areas.PayRoll.Controllers
{
    [Area("PayRoll")]
    [SessionAuthorize]
    public class PFSettingsController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly ICommonService _commonService;
        private readonly IPFSettingsService _pFSettingsService;
        private readonly IHelpers _helpers;

        public static class Global
        {
            public static Filter GlobalFilter;
            public static int deductnCode;
            public static int monthCode;
            public static int pfgroup;
        }
        public PFSettingsController(IDropdownService dropdownService, ICommonService commonService, IPFSettingsService pFSettingsService, IHelpers Helpers)
        {
            _dropdownService = dropdownService;
            _commonService = commonService;
            _pFSettingsService = pFSettingsService;
            _helpers = Helpers;
        }
        public async Task<IActionResult> Index()
        {
            try {
                PFSettings model = new PFSettings();
                ViewBag.Pfd = new SelectList(await _dropdownService.GetPFDeductionDropdown(), "Code", "Name");
                ViewBag.Sap = new SelectList(await _dropdownService.GetSalaryProcessing_MonthDropdown(0), "Code", "Name");
                ViewBag.PFG = new SelectList(await _dropdownService.GetPFGroupDropdown(), "Code", "Name");
                await fillfilterDropdown();
                Filter filter = new Filter();
                filter.length = 10;
                model = await _pFSettingsService.GetPFEmployeeWiseSettingsList(0, 0, 0, filter);
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

        public async Task<IActionResult> ApplyFilter(Filter filter)
        {
            try { 
            PFSettings model = new PFSettings();
            Global.GlobalFilter = filter;
            model = await _pFSettingsService.GetPFEmployeeWiseSettingsList(Global.deductnCode,Global.monthCode, Global.pfgroup, filter);
            return PartialView("_EmployeeList", model);
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
        public async Task<IActionResult> GetEmployeeList(int deduction,int monthcode, int PFGroup)
        {
            try { 
            Global.deductnCode = deduction;
            Global.monthCode = monthcode;
            PFSettings model = new PFSettings();
            Filter filter = new Filter();
            model = await _pFSettingsService.GetPFEmployeeWiseSettingsList(deduction, monthcode, PFGroup, filter);
            if (monthcode == 0 && deduction == 0)
            {
                model.PFSettingsList = new List<PFSettingsDetails>();
            }
            return PartialView("_EmployeeList", model);
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
        public async Task<IActionResult> GetSettingsList(Filter filter1, int Month,int Deduction,int PFGroup)
        {
            try
            {
                PFSettings model = new PFSettings();
                PFSettings m = new PFSettings();
              

                Filter filter = new Filter();
                Global.GlobalFilter = filter1;
                Global.deductnCode = Deduction;
                //m.GridSettings = new List<LeaveRegisterReport_GridSettings>();
                //m = await _leaveRegisterService.GetGridSettings();
                //model.GridSettings.Add(new LeaveRegisterReport_GridSettings());
                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }
                //model.filter = filter1;
                //model.GridSettings = m.GridSettings;
                
                if (Month != 0 && Deduction != 0 && PFGroup != 0)
                {
                    model = await _pFSettingsService.GetPFEmployeeWiseSettingsList(Deduction, Month, PFGroup, filter1);
                }
                if (model == null || model.PFSettingsList == null || model.PFSettingsList.Count() == 0)
                {
                    m.PFSettingsList = new List<PFSettingsDetails>();
                    model = new PFSettings();
                    model.PFSettingsList = new List<PFSettingsDetails>();
                }
                else
                {
                    m.PFSettingsList = model.PFSettingsList;
                }                
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.PFSettingsList == null || model.PFSettingsList.Count() == 0)
                {
                    //model.RequestVsAuthorityList = new List<RequestVsAuthorityList>();
                    //model.RequestVsAuthorityList.Add(new RequestVsAuthorityList());


                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }

                if (model.PFSettingsList.Count > 0)
                {
                    totalrows = m.PFSettingsList[0].active;

                    //m.OnBoardingList.ToList<Hrms.Lite.Shared.Master.Test>().Count; ;
                }


                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.PFSettingsList[0].totalRecords;
                    //return Json(new { data = model.EmployeeProfileList, draw = filter1.draw, recordsTotal = totalrows, recordsFiltered = totalrowsafterfiltering });
                    return Json(new
                    {
                        data = model.PFSettingsList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                 
                    return Json(new
                    {
                        data = model.PFSettingsList,
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




        public async Task<IActionResult> GetPFEmployeeWiseSettingsDetails(Guid EmployeeGI,int deduction,int monthcode,int pfgroup)
        {
            try { 
            PFSettings model = new PFSettings();
            model = await _pFSettingsService.GetPFEmployeeWiseSettingsDetails(EmployeeGI, deduction, monthcode, pfgroup);
            model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);
            ViewBag.Pfg = new SelectList(await _dropdownService.GetPFGroupDropdown(), "Code", "Name");
            model.PFDeduction = new Shared.PayRoll.Generic.PayrollCommon.PFDeduction();
            model.PFDeduction.Code = deduction;
            PFSettings GeneralSettings = new PFSettings();
            GeneralSettings = await _pFSettingsService.GetPFGeneralsettingsList(deduction);
            for (int i = 0; i < GeneralSettings.PFGeneralSettings.Count; i++)
            {
                if (GeneralSettings.PFGeneralSettings[i].ParameterCode == 1)
                {
                    model.PFSettingsDetails.Gen_EPF = GeneralSettings.PFGeneralSettings[i].Value;
                }
                if (GeneralSettings.PFGeneralSettings[i].ParameterCode == 2)
                {
                    model.PFSettingsDetails.Gen_EPS = GeneralSettings.PFGeneralSettings[i].Value;
                }
                if (GeneralSettings.PFGeneralSettings[i].ParameterCode == 3)
                {
                    model.PFSettingsDetails.Gen_ERPF = GeneralSettings.PFGeneralSettings[i].Value;
                }
                if (GeneralSettings.PFGeneralSettings[i].ParameterCode == 6)
                {
                    model.PFSettingsDetails.Gen_MinLimit = GeneralSettings.PFGeneralSettings[i].Value;
                }
                if (GeneralSettings.PFGeneralSettings[i].ParameterCode == 7)
                {
                    model.PFSettingsDetails.Gen_EmpMaxLimit = GeneralSettings.PFGeneralSettings[i].Value;
                }
                if (GeneralSettings.PFGeneralSettings[i].ParameterCode == 11)
                {
                    model.PFSettingsDetails.Gen_EmployerMaxLimit = GeneralSettings.PFGeneralSettings[i].Value;
                }
            }
                return PartialView("_Edit", model);
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
        public async Task<IActionResult> GetPFGeneralSettings(int deduction)
        {
            try { 
            PFSettings model = new PFSettings();
            model = await _pFSettingsService.GetPFGeneralsettingsList(deduction);
            model.PFDeduction = new Shared.PayRoll.Generic.PayrollCommon.PFDeduction();
            model.PFDeduction.Code = Global.deductnCode;
          

                return PartialView("_PfGeneralSettingsList", model);
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
        public async Task<IActionResult> GetApplicableAllowance(int deduction,int pfgroup)
        {
            try { 
            PFSettings model = new PFSettings();
            model = await _pFSettingsService.GetPFSettingsApplicableAllowanceList(deduction, pfgroup);
            model.PFDeduction = new Shared.PayRoll.Generic.PayrollCommon.PFDeduction();
            model.PFDeduction.Code = Global.deductnCode;
            model.PFGroup = new PFGroup();
            model.PFGroup.Code = pfgroup;
            return PartialView("_ApplicableAllowanceList", model);
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
        public async Task<IActionResult> PfDetailsSave(PFSettings model)
        {
            try { 
            var response = await _pFSettingsService.PFEmployeewisesettingsSave(model);
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
        public async Task<IActionResult> GeneralSettingsSave(PFSettings model)
        {
            try { 
            var response = await _pFSettingsService.GeneralSettingsSave(model);
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
        public async Task<IActionResult> ApplicableAllowanceSave(PFSettings model)
        {
            try { 
            var response = await _pFSettingsService.ApplicableAllowanceSave(model);
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


        public async Task<IActionResult> ExportToExcel(string deductionname, string monthname, int TotalRows)
        {
            try
            {

                if (Global.GlobalFilter == null)
                    Global.GlobalFilter = new Filter();
                else
                {
                    Global.GlobalFilter.length = TotalRows;
                    Global.GlobalFilter.start = 0;
                }
                string FilterValues = await _helpers.GetFilterValues(Global.GlobalFilter);

                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("PF Settings (" + deductionname + ")", "Excel");

                if (FilterValues == "")
                {
                    FilterValues = "All";
                }

                string currDateTime = await _helpers.GetCurrentDate();
                

                //Get Datset
                var dataset = await _pFSettingsService.GetPFEmployeeWiseSettingsList_Excel(Global.deductnCode, Global.monthCode, Global.GlobalFilter);
                DataTable dt = dataset.Tables[0];


                //set EXCEL Column Heading 
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName == "doj" || dt.Columns[i].ColumnName == "dob" || dt.Columns[i].ColumnName == "y/s")
                        dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                    else
                        dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                }

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("PF Settings (" + deductionname + ")");
                    int count = dt.Rows.Count + 5;


                    worksheet.Range("A1:O" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:O" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:O" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:O" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:O" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 14)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                    wsReportNameHeaderRange.Value = "PF Settings (" + deductionname + ")";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                    worksheet.Cell(2, 3).Style.Font.Bold = true;
                    worksheet.Cell(2, 3).Value = "PF Deduction";
                    worksheet.Cell(2, 4).Value = deductionname;
                    worksheet.Cell(2, 5).Style.Font.Bold = true;
                    worksheet.Cell(2, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(2, 5).Value = "Month";
                    worksheet.Cell(2, 6).Value = monthname;
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
                    worksheet.Range("B4:O4").Merge();
                    worksheet.Cell("B4").Value = FilterValues;



                    var currentRow = 5;

                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A5:O5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A5:O5").Style.Font.Bold = true;


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
                            "PF Settings (" + deductionname + ") List.xlsx"
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







        //public async Task<IActionResult> ExportToExcel(string deductionname,string monthname,int TotalRows)
        //{
        //    try { 
        //    PFSettings model = new PFSettings();

        //    var LoginUser = HttpContext.Session.GetString("LoginUser");
        //    var CompanyName = HttpContext.Session.GetString("CompanyName");
        //        ExcelPassword EP = new ExcelPassword();
        //        EP = await _commonService.GetExcelPassword("PF Settings (" + deductionname + ")", "Excel");
        //        int slNo = 0;
        //    //Filter filter = new Filter();
        //    //filter = JsonConvert.DeserializeObject<Filter>(TempData["FilterModel"].ToString());
        //    if (Global.GlobalFilter == null)
        //        Global.GlobalFilter = new Filter();
        //     else
        //     {
        //       Global.GlobalFilter.length = TotalRows;
        //            Global.GlobalFilter.start = 0;
        //        }
        //        string FilterValues = await _helpers.GetFilterValues(Global.GlobalFilter);
        //    if (FilterValues == "")
        //    {
        //        FilterValues = "All";
        //    }
        //    model = await _pFSettingsService.GetPFEmployeeWiseSettingsList(Global.deductnCode,Global.monthCode,Global.GlobalFilter);
        //    string currDateTime = await _helpers.GetCurrentDate();
        //    
        //    using (var workbook = new XLWorkbook())
        //    {

        //        var worksheet = workbook.Worksheets.Add("PF Settings");


        //        int count = model.PFSettingsList.Count + 5;


        //        worksheet.Range("A1:N" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        //        worksheet.Range("A1:N" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
        //        worksheet.Range("A1:N" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
        //        worksheet.Range("A1:N" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
        //        worksheet.Range("A1:N" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;






        //        var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 13)));
        //        wsReportNameHeaderRange.Style.Font.Bold = true;
        //        wsReportNameHeaderRange.Style.Font.FontSize = 15;
        //        wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        //        wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        //        wsReportNameHeaderRange.Merge();
        //        wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
        //        wsReportNameHeaderRange.Value = "PF Settings";

        //        worksheet.Cell(2, 1).Style.Font.Bold = true;
        //        worksheet.Cell(2, 1).Value = "Company Name";
        //        worksheet.Cell(2, 2).Value = CompanyName.ToUpper();
        //        worksheet.Cell(2, 3).Style.Font.Bold = true;
        //        worksheet.Cell(2, 3).Value = "PF Deduction";
        //        worksheet.Cell(2, 4).Value = deductionname;
        //            worksheet.Cell(2, 5).Style.Font.Bold = true;
        //            worksheet.Cell(2, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Cell(2, 5).Value = "Month";
        //            worksheet.Cell(2, 6).Value = monthname;

        //            worksheet.Cell(3, 1).Style.Font.Bold = true;
        //        worksheet.Cell(3, 1).Value = "Login User";
        //        worksheet.Cell(3, 2).Value = LoginUser;
        //        worksheet.Cell(3, 3).Style.Font.Bold = true;
        //        worksheet.Cell(3, 3).Value = "Date & Time";
        //        worksheet.Cell(3, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //        worksheet.Cell(3, 4).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
        //        worksheet.Cell(3, 4).Value = currDateTime.ToString();
        //        worksheet.Cell(3, 5).Style.Font.Bold = true;
        //        worksheet.Cell(3, 5).Value = "Total Count";
        //        worksheet.Cell(3, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //        worksheet.Cell(3, 6).Value = model.PFSettingsList.Count();
        //        worksheet.Cell(4, 1).Style.Font.Bold = true;
        //        worksheet.Cell(4, 1).Value = "Filter Applied";
        //        var filterapplied = worksheet.Range("B4:N4").Merge();

        //        filterapplied.Value = FilterValues;


        //        var currentRow = 5;
        //        #region Header
        //        worksheet.Range("A5:N5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
        //        worksheet.Cell(currentRow, 1).Value = "Sl.No";
        //        worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
        //        worksheet.Cell(currentRow, 2).Value = "ID";
        //        worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
        //        worksheet.Cell(currentRow, 3).Value = "EmployeeName";
        //        worksheet.Cell(currentRow, 3).Style.Font.Bold = true;

        //            worksheet.Cell(currentRow, 4).Value = "PF Group";
        //        worksheet.Cell(currentRow, 4).Style.Font.Bold = true;


        //            worksheet.Cell(currentRow, 5).Value = "PF No";
        //        worksheet.Cell(currentRow, 5).Style.Font.Bold = true;


        //            worksheet.Cell(currentRow, 6).Value = "UAN No";
        //        worksheet.Cell(currentRow, 6).Style.Font.Bold = true;

        //            worksheet.Cell(currentRow, 7).Value = "EPF%";
        //        worksheet.Cell(currentRow, 7).Style.Font.Bold = true;


        //            worksheet.Cell(currentRow, 8).Value = "ERPF%";
        //        worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
        //        worksheet.Cell(currentRow, 9).Value = "EPS%";
        //        worksheet.Cell(currentRow, 9).Style.Font.Bold = true;
        //        worksheet.Cell(currentRow, 10).Value = "VPF%";
        //        worksheet.Cell(currentRow, 10).Style.Font.Bold = true;
        //        worksheet.Cell(currentRow, 11).Value = "VPF AMount";
        //        worksheet.Cell(currentRow, 11).Style.Font.Bold = true;
        //        worksheet.Cell(currentRow, 12).Value = "Min Limit";
        //        worksheet.Cell(currentRow, 12).Style.Font.Bold = true;
        //        worksheet.Cell(currentRow, 13).Value = "Emp Max Limit";
        //        worksheet.Cell(currentRow, 13).Style.Font.Bold = true;
        //        worksheet.Cell(currentRow, 14).Value = "Employer Max Limit";
        //        worksheet.Cell(currentRow, 14).Style.Font.Bold = true;


        //        #endregion

        //        #region Body
        //        foreach (var item in model.PFSettingsList)
        //        {
        //            currentRow++;
        //            slNo = slNo + 1;
        //            worksheet.Columns().AdjustToContents();
        //            worksheet.Rows().AdjustToContents();
        //            worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Cell(currentRow, 1).Value = slNo;
        //            worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Cell(currentRow, 2).Value = item.EmployeeID;
        //            worksheet.Cell(currentRow, 3).Value = item.EmployeeName;
        //                worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                worksheet.Cell(currentRow, 4).Value = item.PFGroup.Name;
        //            worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Cell(currentRow, 5).Value = item.PFNo;
        //            worksheet.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Cell(currentRow, 6).Value = item.UANNo;
        //            worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Cell(currentRow, 7).Value = item.EPFPercentage;
        //            worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Cell(currentRow, 8).Value = item.ERPFPercentage;
        //            worksheet.Cell(currentRow, 9).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Cell(currentRow, 9).Value = item.EPSPercentage;
        //            worksheet.Cell(currentRow, 10).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Cell(currentRow, 10).Value = item.VPFPercentage;
        //            worksheet.Cell(currentRow, 11).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Cell(currentRow, 11).Value = item.VPFAmount;
        //            worksheet.Cell(currentRow, 12).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Cell(currentRow, 12).Value = item.MinLimit;
        //            worksheet.Cell(currentRow, 13).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Cell(currentRow, 13).Value = item.EmpMaxLimit;
        //            worksheet.Cell(currentRow, 14).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Cell(currentRow, 14).Value = item.EmployerMaxLimit;
        //            worksheet.Columns(1, 14).Style.Alignment.WrapText = true;
        //            worksheet.Rows().Style.Alignment.WrapText = true;
        //        }
        //        #endregion
        //        worksheet.Columns().AdjustToContents();
        //        worksheet.Rows().AdjustToContents();
        //        //worksheet.Protect("123");
        //        using (var stream = new MemoryStream())
        //        {
        //            workbook.SaveAs(stream);
        //                if (EP.NeedPassword)
        //                {
        //                    Workbook book = new Workbook();
        //                    book.LoadFromStream(stream);
        //                    //Protect Workbook
        //                    book.Protect(EP.Password,false,false);
        //                    book.SaveToStream(stream);
        //                }
        //                var content = stream.ToArray();
        //            return File(
        //                content,
        //                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
        //                "PF Settings List.xlsx"
        //                );
        //        }
        //    }
        //}
        //    catch (UnauthorizedAccessException e)
        //    {

        //        return RedirectToAction("Login", "Account", new { Area = "" });
        //    }
        //    catch (Exception e)
        //    {
        //        return RedirectToAction("Error", "Account", new { Area = "" });
        //    }

        //}


    }
}
