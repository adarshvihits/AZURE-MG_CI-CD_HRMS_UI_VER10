using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.PayRoll.Reports;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll.Reports;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.PayRoll.Reports.Controllers
{
    [Area("PayRoll")]
    [SessionAuthorize]
    public class PFSummaryReportController : Controller
    {
        private readonly IPFSummaryReportService _PFSummaryReportService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        private readonly IDropdownService _dropdownService;

        public static class Global
        {
            public static Filter GlobalFilter;
            public static int UserCode;
            public static int CompanyCode;
            public static string url;

        }
        public PFSummaryReportController(IPFSummaryReportService PFSummaryReportService, ICommonService commonService, IHelpers Helpers, IDropdownService dropdownService)
        {
            _PFSummaryReportService = PFSummaryReportService;
            _commonService = commonService;
            _helpers = Helpers;
            _dropdownService = dropdownService;
        }
        public async Task <IActionResult> Index()
        {
            try
            {
                PFSummaryReport model = new PFSummaryReport();
                model.UserBase_Code = new UserBase_Code();
                ViewBag.Month = new SelectList(await _dropdownService.GetMonthDropdown(), "Code", "Name");
                ViewBag.PFgrp = new SelectList(await _dropdownService.GetPFGroupDropdown(), "Code", "Name");
                Filter filter = new Filter();
                filter.length = 10;
                Global.GlobalFilter = filter;
                model = await _PFSummaryReportService.GetList(500, 500, Global.GlobalFilter);
                await fillfilterDropdown();
                model.UserBase_Code.User_Code = Global.UserCode;
                model.UserBase_Code.Company_Code = Global.CompanyCode;
                return View("~/Areas/PayRoll/Reports/Views/PFSummaryReport/Index.cshtml",model);
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


        public async Task<IActionResult> GetReportPDF(int PFGroup,int Month,Filter Filter)
        {
            PFSummaryReport model = new PFSummaryReport();
            model.Report = new Shared.Master.KendoReport();
            var LoginUser = HttpContext.Session.GetString("User");
           
            Login login = JsonConvert.DeserializeObject<Login>(LoginUser);
            var usercode = login.authUser.UserCode;
            var companycode = login.authUser.CompanyID;
            model.UserBase_Code = new UserBase_Code();
            model.UserBase_Code.Company_Code = companycode;
            model.UserBase_Code.User_Code = usercode;
            model.PFGroup = PFGroup;
            model.Month = Month;
            model.UserBase_Code.User_Code = Global.UserCode;
            model.UserBase_Code.Company_Code = Global.CompanyCode;
            model.Report.Url = await _PFSummaryReportService.GetUrl();
            Global.url = model.Report.Url.Urllink;
            model.Type = "PFSummary";
            model.Report.Url = await _PFSummaryReportService.GetOfferLetter(model.Type);
            model.Report.Url.Urllink = Global.url;
            return View("~/Areas/PayRoll/Reports/Views/PFSummaryReport/PfSummaryReportPdf.cshtml", model);
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
        public async Task<IActionResult> GetPFSummaryReportList(Filter filter1,int Month,int PFGroup)
        {
            try
            {
                PFSummaryReport model = new PFSummaryReport();
                PFSummaryReport m = new PFSummaryReport();
                model.UserBase_Code = new UserBase_Code();
                //model.EmployeeHeader = new EmployeeHeader();




                Filter filter = new Filter();
                Global.GlobalFilter = filter1;
             
                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }
                if(Month!=0 && PFGroup !=0)
                model = await _PFSummaryReportService.GetList(Month, PFGroup, Global.GlobalFilter);
                Global.UserCode = model.UserBase_Code.User_Code;
                Global.CompanyCode = model.UserBase_Code.Company_Code;
                m.PFSummary = model.PFSummary;



                decimal ActualPFSalarySUM = 0;
                decimal PFSalarySUM = 0;
                decimal EmployeeContributionSUM = 0;
                decimal PensionFundSUM = 0;
                decimal PFSUM = 0;
                decimal EDLIWageSUM = 0;
                decimal EPFWageSUM = 0;
                decimal AdminChargeSUM = 0;
                decimal EDLIChargeSUM = 0;

                ActualPFSalarySUM = model.ActualPFSalarySUM;
                PFSalarySUM = model.PFSalarySUM;
                EmployeeContributionSUM = model.EmployeeContributionSUM;
                PensionFundSUM = model.PensionFundSUM;
                PFSUM = model.PFSUM;
                EDLIWageSUM = model.EDLIWageSUM;
                EPFWageSUM = model.EPFWageSUM;
                AdminChargeSUM = model.AdminChargeSUM;
                EDLIChargeSUM = model.EDLIChargeSUM;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.PFSummary == null || model.PFSummary.Count() == 0)
                {


                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows,
                        actualpfsalarySum = ActualPFSalarySUM,
                        pfsalarySum = PFSalarySUM,
                        employeecontributionSum = EmployeeContributionSUM,
                        pensionFundSum = PensionFundSUM,
                        pfSum = PFSUM,
                        edliWageSum = EDLIWageSUM,
                        epfWageSum = EPFWageSUM,
                        adminChargeSum = AdminChargeSUM,
                        edliChargeSum = EDLIChargeSUM


                }) ;
                }




                if (model.PFSummary.Count > 0)
                {
                    totalrows = m.PFSummary[0].active;

                    
                }

                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.PFSummary[0].totalRecords;
                    return Json(new
                    {
                        data = model.PFSummary,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,


                        actualpfsalarySum = ActualPFSalarySUM,
                        pfsalarySum = PFSalarySUM,
                        employeecontributionSum = EmployeeContributionSUM,
                        pensionFundSum = PensionFundSUM,
                        pfSum = PFSUM,
                        edliWageSum = EDLIWageSUM,
                        epfWageSum = EPFWageSUM,
                        adminChargeSum = AdminChargeSUM,
                        edliChargeSum = EDLIChargeSUM,

                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    return Json(new
                    {
                        data = model.PFSummary,
                        userCode=model.UserBase_Code.User_Code,
                        comCode=model.UserBase_Code.Company_Code,
                        draw = filter1.draw,//Request.Form["draw"], 
                        actualpfsalarySum = ActualPFSalarySUM,
                        pfsalarySum = PFSalarySUM,
                        employeecontributionSum = EmployeeContributionSUM,
                        pensionFundSum = PensionFundSUM,
                        pfSum = PFSUM,
                        edliWageSum = EDLIWageSUM,
                        epfWageSum = EPFWageSUM,
                        adminChargeSum = AdminChargeSUM,
                        edliChargeSum = EDLIChargeSUM,
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




        public async Task<ActionResult> GetList(int Month,int PFGroup)
        {
            try
            {

                PFSummaryReport model = new PFSummaryReport();          
                if (Global.GlobalFilter == null)
                    Global.GlobalFilter = new Filter();
                model = await _PFSummaryReportService.GetList(Month, PFGroup,Global.GlobalFilter);
                model.PFSummary = new List<PFSummary>();
                model.PFSummary.Add(new PFSummary());
                return PartialView("~/Areas/PayRoll/Reports/Views/PFSummaryReport/_PFSummaryReportList.cshtml", model);

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

        public static string ConvertSubstringToUpperCase(string originalString, int startIndex, int length)
        {
            string upperCaseSubstring = originalString.Substring(startIndex, length).ToUpper();
            return originalString.Substring(0, startIndex) + upperCaseSubstring + originalString.Substring(startIndex + length);
        }

        //public async Task<IActionResult> ExportToExcel(int Month, int PFGroup,string MonthName,string PFGroupName,int totRecords)
        //{
        //    try
        //    {
              
        //        if (Global.GlobalFilter == null)
        //            Global.GlobalFilter = new Filter();
        //        else 
        //        {
        //            Global.GlobalFilter.length = totRecords;
        //            Global.GlobalFilter.start = 0;
        //        }
        //        string FilterValues = await _helpers.GetFilterValues(Global.GlobalFilter);               
              
        //        var LoginUser = HttpContext.Session.GetString("LoginUser");
        //        var CompanyName = HttpContext.Session.GetString("CompanyName");
        //        var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
        //        var GroupCompanyName = User.authUser.GroupCompanyName;
        //        ExcelPassword EP = new ExcelPassword();
        //        EP = await _commonService.GetExcelPassword("PF Summary Report", "Excel");
            
        //        if (FilterValues == "")
        //        {
        //            FilterValues = "All";
        //        }
        //        string currDateTime = await _helpers.GetCurrentDate();
        //        

        //        int index = 0;
        //        //Get Dataset
        //        var dataset = await _PFSummaryReportService.GetPFSummaryReport_Excel(Month, PFGroup, Global.GlobalFilter);
        //        DataTable dt = dataset.Tables[0];

        //        //set EXCEL Column Heading 
        //        for (int i = 0; i < dt.Columns.Count; i++)
        //        {
        //            if(dt.Columns[i].ColumnName.Contains("pf"))
        //            {
        //                index = dt.Columns[i].ColumnName.IndexOf("pf");
        //                dt.Columns[i].ColumnName = ConvertSubstringToUpperCase(dt.Columns[i].ColumnName, index, 2);
        //            }
        //            if (dt.Columns[i].ColumnName.Contains("uan"))
        //            {
        //                index = dt.Columns[i].ColumnName.IndexOf("uan");
        //                dt.Columns[i].ColumnName = ConvertSubstringToUpperCase(dt.Columns[i].ColumnName, index, 3);
        //            }
        //            else
        //            dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
        //        }


             

        //        using (var workbook = new XLWorkbook())
        //        {
        //            var worksheet = workbook.Worksheets.Add("PF Summary Report");
        //            int count =dt.Rows.Count + 5;


        //            worksheet.Range("A1:P" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        //            worksheet.Range("A1:P" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
        //            worksheet.Range("A1:P" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
        //            worksheet.Range("A1:P" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
        //            worksheet.Range("A1:P" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
        //            var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 15)));
        //            wsReportNameHeaderRange.Style.Font.Bold = true;
        //            wsReportNameHeaderRange.Style.Font.FontSize = 15;
        //            wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        //            wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        //            wsReportNameHeaderRange.Merge();
        //            wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
        //            wsReportNameHeaderRange.Value = "PF summary Report";

        //            worksheet.Cell(2, 1).Style.Font.Bold = true;
        //            worksheet.Cell(2, 1).Value = "Group Company Name";
        //            worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
        //            worksheet.Cell(2, 3).Style.Font.Bold = true;
        //            worksheet.Cell(2, 3).Value = "Month";
        //            worksheet.Cell(2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Cell(2, 4).Style.DateFormat.Format = "MMM-yyyy";
        //            worksheet.Cell(2, 4).Value = MonthName;
        //            worksheet.Cell(2, 5).Value = "PF Group";
        //            worksheet.Cell(2, 5).Style.Font.Bold = true;
        //            worksheet.Cell(2, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Cell(2, 6).Value = PFGroupName;

        //            worksheet.Cell(3, 1).Style.Font.Bold = true;
        //            worksheet.Cell(3, 1).Value = "Login User";
        //            worksheet.Cell(3, 2).Value = LoginUser;
        //            worksheet.Cell(3, 3).Style.Font.Bold = true;
        //            worksheet.Cell(3, 3).Value = "Date & Time";
        //            worksheet.Cell(3, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Cell(3, 4).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
        //            worksheet.Cell(3, 4).Value = currDateTime.ToString();
        //            worksheet.Cell(3, 5).Style.Font.Bold = true;
        //            worksheet.Cell(3, 5).Value = "Total Count";
        //            worksheet.Cell(3, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //            worksheet.Cell(3, 6).Value =dt.Rows.Count;
        //            worksheet.Cell(4, 1).Style.Font.Bold = true;
        //            worksheet.Cell(4, 1).Value = "Filter Applied";
        //            worksheet.Range("B4:P4").Merge();
        //            worksheet.Cell("B4").Value = FilterValues;



        //            var currentRow = 5;

        //            worksheet.Cell(currentRow, 1).InsertTable(dt);
        //            worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

        //            worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
        //            worksheet.Range("A5:P5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
        //            worksheet.Range("A5:P5").Style.Font.Bold = true;


        //            worksheet.Columns().AdjustToContents();
        //            worksheet.Rows().AdjustToContents();
        //            using (var stream = new MemoryStream())
        //            {
        //                workbook.SaveAs(stream);
        //                if (EP.NeedPassword)
        //                {
        //                    Workbook book = new Workbook();
        //                    book.LoadFromStream(stream);
        //                    //Protect Workbook
        //                    book.Protect(EP.Password,false,false);
        //                    book.SaveToStream(stream);
        //                }
        //                var content = stream.ToArray();
        //                return File(
        //                    content,
        //                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
        //                    "PF summary Report List.xlsx"
        //                    );
        //            }
        //        }
        //    }
        //    catch (UnauthorizedAccessException e)
        //    {

        //        return RedirectToAction("Login", "Account", new { Area = "" });
        //    }
        //    catch (Exception e)
        //    {
        //        return RedirectToAction("Error", "Account", new { Area = "" });
        //    }

        //}






        public async Task<IActionResult> ExportToExcel(int Month, int PFGroup, string MonthName, string PFGroupName, int totRecords)
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
                EP = await _commonService.GetExcelPassword("PF Summary Report", "Excel");

                if (FilterValues == "")
                {
                    FilterValues = "All";
                }

                string currDateTime = await _helpers.GetCurrentDate();
                

                int index = 0;
                // Get Dataset
                var dataset = await _PFSummaryReportService.GetPFSummaryReport_Excel(Month, PFGroup, Global.GlobalFilter);
                DataTable dt = dataset.Tables[0];

                // Set EXCEL Column Heading 
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName.Contains("pf"))
                    {
                        index = dt.Columns[i].ColumnName.IndexOf("pf");
                        dt.Columns[i].ColumnName = ConvertSubstringToUpperCase(dt.Columns[i].ColumnName, index, 2);
                    }
                    if (dt.Columns[i].ColumnName.Contains("uan"))
                    {
                        index = dt.Columns[i].ColumnName.IndexOf("uan");
                        dt.Columns[i].ColumnName = ConvertSubstringToUpperCase(dt.Columns[i].ColumnName, index, 3);
                    }
                    else
                    {
                        dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                    }
                }

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("PF Summary Report");
                    int count = dt.Rows.Count + 6;

                    worksheet.Range("A1:P" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:P" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:P" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:P" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:P" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 15)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                    wsReportNameHeaderRange.Value = "PF summary Report";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                    worksheet.Cell(2, 3).Style.Font.Bold = true;
                    worksheet.Cell(2, 3).Value = "Month";
                    worksheet.Cell(2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(2, 4).Style.DateFormat.Format = "MMM-yyyy";
                    worksheet.Cell(2, 4).Value = MonthName;
                    worksheet.Cell(2, 5).Value = "PF Group";
                    worksheet.Cell(2, 5).Style.Font.Bold = true;
                    worksheet.Cell(2, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(2, 6).Value = PFGroupName;

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
                    worksheet.Range("B4:P4").Merge();
                    worksheet.Cell("B4").Value = FilterValues;

                    var currentRow = 5;

                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A5:P5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A5:P5").Style.Font.Bold = true;

                    // Calculate sum for each column
                    int sumRow = dt.Rows.Count + 6;
                    for (int col = 8; col <= dt.Columns.Count; col++)
                    {
                        double sum = 0;
                        for (int row = 6; row < sumRow; row++)
                        {
                            if (double.TryParse(worksheet.Cell(row, col).Value.ToString(), out double value))
                            {
                                sum += value;
                            }
                        }
                        worksheet.Cell(sumRow, col).Value = sum;
                        worksheet.Cell(sumRow, 1).Value = "Total";
                    }

                    // Format the sum row
                    var sumRange = worksheet.Range(sumRow, 1, sumRow, dt.Columns.Count);
                    sumRange.Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    sumRange.Style.Font.Bold = true;

                    worksheet.Columns().AdjustToContents();
                    worksheet.Rows().AdjustToContents();
                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        if (EP.NeedPassword)
                        {
                            Workbook book = new Workbook();
                            book.LoadFromStream(stream);
                            // Protect Workbook
                            book.Protect(EP.Password, false, false);
                            book.SaveToStream(stream);
                        }
                        var content = stream.ToArray();
                        return File(
                            content,
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            "PF summary Report List.xlsx"
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






    }
}
