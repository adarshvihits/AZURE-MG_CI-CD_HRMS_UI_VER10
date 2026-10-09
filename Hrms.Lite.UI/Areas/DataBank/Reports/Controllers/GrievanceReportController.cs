using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank.Reports;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
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

namespace Hrms.Lite.UI.Areas.DataBank.Reports.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]
    public class GrievanceReportController : Controller
    {
        private readonly IGrievanceReportService _GrievanceReportService;
        private readonly ICommonService _commonService;
        private readonly IDropdownService _dropdownService;
        private readonly IHelpers _helpers;


        public static class Global
        {
            public static Filter GlobalFilter;
            public static DateTime FromDate;
            public static DateTime ToDate;
            public static int Category;
            public static bool IsRefreshed = true;

        }



        public GrievanceReportController(IGrievanceReportService _grievanceReportService, ICommonService commonService, IDropdownService dropdownService, IHelpers Helpers)
        {
            _GrievanceReportService = _grievanceReportService;
            _commonService = commonService;
            _dropdownService = dropdownService;
            _helpers = Helpers;
        }
        public async Task <IActionResult> Index()
        {
            try{
            Grievance model = new Grievance();
            await fillfilterDropdown();
            ViewBag.Category = new SelectList(await _dropdownService.GrievanceCategoryDropDown(), "Code", "Name");
            string lastdayofmonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1).ToString("yyyy-MMM-dd");
            string firstdate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("yyyy-MMM-dd");

            //model = await _GrievanceReportService.GetGrievanceReportList(firstdate, lastdayofmonth, 4, Global.GlobalFilter);
            model.From = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            model.To = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);
            return View("~/Areas/DataBank/Reports/Views/GrievanceReport/Index.cshtml", model);
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
            try
            {
                Grievance model = new Grievance();
                Global.GlobalFilter = filter;

                model = await _GrievanceReportService.GetGrievanceReportList(Global.FromDate.ToString("yyyy-MMM-dd"), Global.ToDate.ToString("yyyy-MMM-dd"), Global.Category, Global.GlobalFilter);
                return PartialView("~/Areas/DataBank/Reports/Views/GrievanceReport/_List.cshtml", model);


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

        public async Task<IActionResult> GetList(DateTime fromDate, DateTime toDate,int Category)
        {
            Grievance model = new Grievance();
            model.Filter = new Filter();
            Global.GlobalFilter = new Filter();
            Global.FromDate = fromDate;
            Global.ToDate = toDate;
            Global.Category = Category;
            model = await _GrievanceReportService.GetGrievanceReportList(fromDate.ToString("yyyy-MMM-dd"), toDate.ToString("yyyy-MMM-dd"), Category, Global.GlobalFilter);
            return PartialView("~/Areas/DataBank/Reports/Views/GrievanceReport/_List.cshtml", model);

        }


        public async Task<IActionResult> ExportToExcel(DateTime fromDate, DateTime toDate, int Category, string CategoryName, int totRecords)
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
                EP = await _commonService.GetExcelPassword("Grievance Report (" + fromDate.ToShortDateString() + " to " + toDate.ToShortDateString() + ")", "Excel");



                if (FilterValues == "")
                {
                    FilterValues = "All";
                }
                string currDateTime = await _helpers.GetCurrentDate();
                

                var dataset = await _GrievanceReportService.GetGrievanceReport_Excel(fromDate.ToString("yyyy-MMM-dd"), toDate.ToString("yyyy-MMM-dd"), Category, Global.GlobalFilter);
                DataTable dt = dataset.Tables[0];


                //set EXCEL Column Heading Format
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName == "doj" || dt.Columns[i].ColumnName == "dob")
                        dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                    else
                        dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                }

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Grievance Report");
                    int count = dt.Rows.Count + 5;

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
                    wsReportNameHeaderRange.Value = "Grievance Report";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                    worksheet.Cell(2, 3).Style.Font.Bold = true;
                    worksheet.Cell(2, 3).Value = "From";
                    worksheet.Cell(2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(2, 4).Value = fromDate;
                    worksheet.Cell(2, 4).Style.DateFormat.Format = "dd-MMM-yyyy";
                    //worksheet.Cell(2, 4).Value = currDateTime.ToString();
                    worksheet.Cell(2, 5).Style.Font.Bold = true;
                    worksheet.Cell(2, 5).Value = "To";
                    worksheet.Cell(2, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(2, 6).Value = toDate;
                    worksheet.Cell(2, 6).Style.DateFormat.Format = "dd-MMM-yyyy";
                    worksheet.Cell(2, 7).Style.Font.Bold = true;
                    worksheet.Cell(2, 7).Value = "Grievance Category";
                    worksheet.Cell(2, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(2, 8).Value = CategoryName;

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
                            "Grievance Report List.xlsx"
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
