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
    public class ESISettingsController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly IESISettingsService _ESISettingsService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;

        public static class Global
        {
            public static Filter GlobalFilter;
            public static int EsiDeduction;
            public static int MonthCode;

        }

        public ESISettingsController(IESISettingsService ESISettingsService, IDropdownService dropdownService, ICommonService commonService, IHelpers Helpers)
        {
            _ESISettingsService = ESISettingsService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = Helpers;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                ESISettings model = new ESISettings();
                ViewBag.EsiDed = new SelectList(await _dropdownService.GetESIDeductionDropdown(), "Code", "Name");
                ViewBag.PayMonth = new SelectList(await _dropdownService.GetSalaryProcessing_MonthDropdown(0), "Code", "Name");
                await fillfilterDropdown();
                Filter filter = new Filter();
                filter.length = 10;
                model = await _ESISettingsService.GetESIEmployeeWiseSettingsList(0, 0, filter);
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
            Global.GlobalFilter = filter;
            ESISettings model = new ESISettings();
            ESISettings GeneralSettings = new ESISettings();
            ESISettings ApplicableAllowance = new ESISettings();
            model.ESIGeneralSettings = new List<ESIGeneralSettings>();
            model.ESIApplicableAllowance = new List<ESIApplicableAllowance>();
            model = await _ESISettingsService.GetESIEmployeeWiseSettingsList(Global.EsiDeduction, Global.MonthCode,filter);
            GeneralSettings = await _ESISettingsService.GetESIGeneralsettingsList(Global.EsiDeduction);
            ApplicableAllowance = await _ESISettingsService.GetESISettingsApplicableAllowanceList(Global.EsiDeduction);
            model.ESIGeneralSettings = GeneralSettings.ESIGeneralSettings;
            model.ESIApplicableAllowance = ApplicableAllowance.ESIApplicableAllowance;
            return PartialView("_List", model);
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
        public async Task<IActionResult> GetESIList(Filter filter1, int EsiDeduction, int Month)
        {
            try
            {
                ESISettings model = new ESISettings();
                ESISettings m = new ESISettings();
                model.EmployeeHeader = new EmployeeHeader();




                Filter filter = new Filter();
                Global.GlobalFilter = filter1;
                Global.EsiDeduction = EsiDeduction;
                Global.MonthCode = Month;

                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }

                if (EsiDeduction != 0 && Month != 0)
                {

                    model = await _ESISettingsService.GetESIEmployeeWiseSettingsList(EsiDeduction, Month, filter1);
                }
                m.ESISettingsList = model.ESISettingsList;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.ESISettingsList == null || model.ESISettingsList.Count() == 0)
                {
                    
                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }




                if (model.ESISettingsList.Count > 0)
                {
                    totalrows = m.ESISettingsList[0].active;

                }

                
                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.ESISettingsList[0].totalRecords;
                    return Json(new
                    {
                        data = model.ESISettingsList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    return Json(new
                    {
                        data = model.ESISettingsList,
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

        public async Task<IActionResult> GetESIGeneralsettingsList(int ESIDeductionCode)
        {
            ESISettings GeneralSettings = new ESISettings();
            GeneralSettings = await _ESISettingsService.GetESIGeneralsettingsList(ESIDeductionCode);
            GeneralSettings.ESIDeduction = new MasterBase();
            GeneralSettings.ESIDeduction.Code = ESIDeductionCode;
            return PartialView("_ESIGeneralSettings", GeneralSettings);
        }
        public async Task<IActionResult> GetApplicableAllowanceList(int ESIDeductionCode)
        {
            ESISettings ApplicableAllowance = new ESISettings();
            ApplicableAllowance = await _ESISettingsService.GetESISettingsApplicableAllowanceList(ESIDeductionCode);
            ApplicableAllowance.ESIDeduction = new MasterBase();
            ApplicableAllowance.ESIDeduction.Code = ESIDeductionCode;
            return PartialView("_ApplicableAllowance", ApplicableAllowance);
        }

        public async Task<IActionResult> GetList(int ESIDeductionCode,int MonthCode)
        {
            try {
                ESISettings model = new ESISettings();
                ESISettings GeneralSettings = new ESISettings();
                ESISettings ApplicableAllowance = new ESISettings();
                model.ESIGeneralSettings = new List<ESIGeneralSettings>();
                Global.EsiDeduction = ESIDeductionCode;
                Global.MonthCode = MonthCode;
                Filter filter = new Filter();
                model.ESIApplicableAllowance = new List<ESIApplicableAllowance>();
                if (ESIDeductionCode != 0 && MonthCode != 0)
                {
                    model = await _ESISettingsService.GetESIEmployeeWiseSettingsList(ESIDeductionCode, MonthCode, filter);
                    GeneralSettings = await _ESISettingsService.GetESIGeneralsettingsList(ESIDeductionCode);
                    ApplicableAllowance = await _ESISettingsService.GetESISettingsApplicableAllowanceList(ESIDeductionCode);
                    model.ESIGeneralSettings = GeneralSettings.ESIGeneralSettings;
                    model.ESIApplicableAllowance = ApplicableAllowance.ESIApplicableAllowance;
                    model.ESIDeduction = new MasterBase();
                    model.ESIDeduction.Code = ESIDeductionCode;
                    return PartialView("_List", model);
                }
                else if(ESIDeductionCode != 0)
                {
                    GeneralSettings = await _ESISettingsService.GetESIGeneralsettingsList(ESIDeductionCode);
                    ApplicableAllowance = await _ESISettingsService.GetESISettingsApplicableAllowanceList(ESIDeductionCode);
                    model.ESIGeneralSettings = GeneralSettings.ESIGeneralSettings;
                    model.ESIApplicableAllowance = ApplicableAllowance.ESIApplicableAllowance;
                    model.ESIDeduction = new MasterBase();
                    model.ESIDeduction.Code = ESIDeductionCode;
                    return PartialView("_List", model);
                }
                else
                {
                    return PartialView("_List", model);
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
        public async Task<IActionResult> Edit(Guid EmployeeGI,int ESIDeductionCode,int MonthCode)
        {
            try { 
            ESISettings model = new ESISettings();
            ViewBag.EsiGrp = new SelectList(await _dropdownService.GetESIGroupDropDown(), "Code", "Name");
            model = await _ESISettingsService.GetESIEmployeeWiseSettingsDetails(EmployeeGI, ESIDeductionCode, MonthCode);
            model.ESIDeduction = new MasterBase();
            model.ESIDeduction.Code = ESIDeductionCode;
            model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);
            ESISettings GeneralSettings = new ESISettings();
            GeneralSettings = await _ESISettingsService.GetESIGeneralsettingsList(ESIDeductionCode);
            for (int i = 0; i < GeneralSettings.ESIGeneralSettings.Count; i++)
            {
                    if (GeneralSettings.ESIGeneralSettings[i].ParameterCode == 1)
                    {
                        model.ESISettingsDetails.Gen_EESI = GeneralSettings.ESIGeneralSettings[i].Value;
                    }
                    if (GeneralSettings.ESIGeneralSettings[i].ParameterCode == 2)
                    {
                        model.ESISettingsDetails.Gen_MESI = GeneralSettings.ESIGeneralSettings[i].Value;
                    }
                    if (GeneralSettings.ESIGeneralSettings[i].ParameterCode == 3)
                    {
                        model.ESISettingsDetails.Gen_MinLimit = GeneralSettings.ESIGeneralSettings[i].Value;
                    }
                    if (GeneralSettings.ESIGeneralSettings[i].ParameterCode == 4)
                    {
                        model.ESISettingsDetails.Gen_EmpMaxLimit = GeneralSettings.ESIGeneralSettings[i].Value;
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

        public async Task<IActionResult> ESISettingsEmpWiseSave(ESISettings model)
        {
            try { 
            var response = await _ESISettingsService.ESIEmployeewisesettingsSave(model);
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
        public async Task<IActionResult> ESIGeneralSettingsSave(ESISettings model)
        {
            try { 
            //model.ESIDeduction = new MasterBase();
            //model.ESIDeduction.Code = Global.EsiDeduction;
            var response = await _ESISettingsService.ESIGeneralsettingsSave(model);
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
        public async Task<IActionResult> ESISettingsAllowanceSave(ESISettings model)
        {
            try { 
            var ESISettingsAllw = from li in model.ESIApplicableAllowance
                      where li.YN == true
                      select li;
            model.ESIApplicableAllowance = ESISettingsAllw.ToList();
            //model.ESIDeduction = new MasterBase();
            //model.ESIDeduction.Code = Global.EsiDeduction;
            var response = await _ESISettingsService.ESISettingsAllowanceSave(model);
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
        public async Task<IActionResult> ExportToExcel(string EsiDeductionName, string PayrollMonthName, int totRecords)
        {
            try
            {

                if (Global.GlobalFilter == null)
                    Global.GlobalFilter = new Filter();
                else
                {
                    Global.GlobalFilter.length = totRecords;
                    Global.GlobalFilter.start = 0;
                }
                string FilterValues = await _helpers.GetFilterValues(Global.GlobalFilter);

                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("ESI Settings List (" + EsiDeductionName + ")", "Excel");

                if (FilterValues == "")
                {
                    FilterValues = "All";
                }

                string currDateTime = await _helpers.GetCurrentDate();
                

                //Get Datset
                var dataset = await _ESISettingsService.GetESIEmployeeWiseSettingsList_Excel(Global.EsiDeduction, Global.MonthCode, Global.GlobalFilter);
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
                    var worksheet = workbook.Worksheets.Add("ESI Settings ");
                    int count = dt.Rows.Count + 5;


                    worksheet.Range("A1:K" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:K" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:K" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:K" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:K" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 10)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                    wsReportNameHeaderRange.Value = "ESI Settings List (" + EsiDeductionName + ")";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                    worksheet.Cell(2, 3).Style.Font.Bold = true;
                    worksheet.Cell(2, 3).Value = "ESI Deduction";
                    worksheet.Cell(2, 4).Value = EsiDeductionName;
                    worksheet.Cell(2, 5).Style.Font.Bold = true;
                    worksheet.Cell(2, 5).Value = "Salary Period";
                    worksheet.Cell(2, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(2, 6).Style.DateFormat.Format = "MMM-yyyy";
                    worksheet.Cell(2, 6).Value = PayrollMonthName;
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
                    worksheet.Range("B4:K4").Merge();
                    worksheet.Cell("B4").Value = FilterValues;



                    var currentRow = 5;

                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A5:K5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A5:K5").Style.Font.Bold = true;


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
                            "ESI Settings List (" + EsiDeductionName + ") List.xlsx"
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

        //public async Task<IActionResult> ExportToExcel(string EsiDeductionName, string PayrollMonthName, int totRecords)
        //{
        //    try { 
        //    ESISettings model = new ESISettings();

        //    var LoginUser = HttpContext.Session.GetString("LoginUser");
        //    var CompanyName = HttpContext.Session.GetString("CompanyName");
        //        ExcelPassword EP = new ExcelPassword();
        //        EP = await _commonService.GetExcelPassword("ESI Settings List (" + EsiDeductionName + ")", "Excel");
        //        int slNo = 0;

        //    if (Global.GlobalFilter == null)
        //        Global.GlobalFilter = new Filter();
        //        else
        //        {
        //            Global.GlobalFilter.length = totRecords;
        //            Global.GlobalFilter.start = 0;
        //        }
        //        string FilterValues = await _helpers.GetFilterValues(Global.GlobalFilter);
        //    if (FilterValues == "")
        //    {
        //        FilterValues = "All";
        //    }
        //    //model = await _loanTopUpservice.GetLoanTopUpList(Global.GlobalFilter);
        //    model = await _ESISettingsService.GetESIEmployeeWiseSettingsList(Global.EsiDeduction, Global.MonthCode, Global.GlobalFilter);
        //    string currDateTime = await _helpers.GetCurrentDate();
        //    
        //    using (var workbook = new XLWorkbook())
        //    {

        //        var worksheet = workbook.Worksheets.Add("ESI Settings List");


        //        int count = model.ESISettingsList.Count + 5;


        //        worksheet.Range("A1:K" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        //        worksheet.Range("A1:K" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
        //        worksheet.Range("A1:K" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
        //        worksheet.Range("A1:K" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
        //        worksheet.Range("A1:K" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;






        //        var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 10)));
        //        wsReportNameHeaderRange.Style.Font.Bold = true;
        //        wsReportNameHeaderRange.Style.Font.FontSize = 15;
        //        wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        //        wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        //        wsReportNameHeaderRange.Merge();
        //        wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
        //        wsReportNameHeaderRange.Value = "ESI Settings";

        //        worksheet.Cell(2, 1).Style.Font.Bold = true;
        //        worksheet.Cell(2, 1).Value = "Company Name";
        //        worksheet.Cell(2, 2).Value = CompanyName.ToUpper();
        //        worksheet.Cell(2, 3).Style.Font.Bold = true;
        //        worksheet.Cell(2, 3).Value = "ESI Deduction";
        //        worksheet.Cell(2, 4).Value = EsiDeductionName;
        //        worksheet.Cell(2, 5).Style.Font.Bold = true;
        //        worksheet.Cell(2, 5).Value = "Salary Period";
        //        worksheet.Cell(2, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //        worksheet.Cell(2, 6).Style.DateFormat.Format = "MMM-yyyy";
        //        worksheet.Cell(2, 6).Value = PayrollMonthName;

        //        worksheet.Cell(3, 1).Style.Font.Bold = true;
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
        //        worksheet.Cell(3, 6).Value = model.ESISettingsList.Count();
        //        worksheet.Cell(4, 1).Style.Font.Bold = true;
        //        worksheet.Cell(4, 1).Value = "Filter Applied";
        //        var filterapplied = worksheet.Range("B4:K4").Merge();

        //        filterapplied.Value = FilterValues;


        //        var currentRow = 5;
        //        #region Header
        //        worksheet.Range("A5:K5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
        //        worksheet.Cell(currentRow, 1).Value = "Sl.No";
        //        worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
        //        worksheet.Cell(currentRow, 2).Value = "ID";
        //        worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
        //        worksheet.Cell(currentRow, 3).Value = "EmployeeName";
        //        worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
        //        worksheet.Cell(currentRow, 4).Value = "DOJ";
        //        worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
        //        worksheet.Cell(currentRow, 5).Value = "Location";
        //        worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
        //        worksheet.Cell(currentRow, 6).Value = "ESI Group";
        //        worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
        //        worksheet.Cell(currentRow, 7).Value = "ESI No";
        //        worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
        //        worksheet.Cell(currentRow, 8).Value = "EESI";
        //        worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
        //        worksheet.Cell(currentRow, 9).Value = "MESI";
        //        worksheet.Cell(currentRow, 9).Style.Font.Bold = true;
        //        worksheet.Cell(currentRow, 10).Style.Font.Bold = true;
        //        worksheet.Cell(currentRow, 10).Value = "Min Limit";                
        //        worksheet.Cell(currentRow, 11).Style.Font.Bold = true;
        //        worksheet.Cell(currentRow, 11).Value = "Max Limit";



        //        #endregion

        //        #region Body
        //        foreach (var item in model.ESISettingsList)
        //        {
        //            currentRow++;
        //            slNo = slNo + 1;

        //            worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Cell(currentRow, 1).Value = slNo;
        //            worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Cell(currentRow, 2).Value = item.EmployeeID;
        //            worksheet.Cell(currentRow, 3).Value = item.EmployeeName;
        //            worksheet.Cell(currentRow, 4).Value = item.DOJ;
        //                worksheet.Cell(currentRow, 4).Style.DateFormat.Format = "dd-MMM-yyyy";
        //                worksheet.Cell(currentRow, 5).Value = item.Location;
        //            worksheet.Cell(currentRow, 6).Value = item.ESIGroup.Name;
        //            worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Cell(currentRow, 7).Value = item.ESINo;
        //            worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Cell(currentRow, 8).Value = item.EESI;
        //            worksheet.Cell(currentRow, 9).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);

        //            worksheet.Cell(currentRow, 9).Value = item.MESI;
        //            worksheet.Cell(currentRow, 10).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);

        //            worksheet.Cell(currentRow, 10).Value = item.MinLimit;
        //            worksheet.Cell(currentRow, 11).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);

        //            worksheet.Cell(currentRow, 11).Value = item.EmpMaxLimit;

        //            worksheet.Columns(1, 11).Style.Alignment.WrapText = true;
        //            worksheet.Rows().Style.Alignment.WrapText = true;
        //        }
        //        #endregion
        //        worksheet.Columns().AdjustToContents();
        //        worksheet.Rows().AdjustToContents();

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
        //                "ESI Settings List.xlsx"
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
