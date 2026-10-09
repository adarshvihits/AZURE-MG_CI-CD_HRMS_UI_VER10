using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.TimeOffice.Report;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice.Report;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Spire.Xls;

namespace Hrms.Lite.UI.Areas.TimeOffice.Report.Controllers
{
    [Area("TimeOffice")]
    [SessionAuthorize]
    public class DailyAttendanceStatusReportController : Controller
    {
        private readonly IDailyAttendanceStatusReportService _dailyAttendanceStatusReportService;
        private readonly IDropdownService _dropdownService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        public static class Global
        {
            public static DailyAttendanceStatusReport data;
            public static System.Data.DataSet _dataset;
            public static DateTime? FromDate;           
            public static DateTime? ToDate;           
        }
        public DailyAttendanceStatusReportController(IDropdownService dropdownService, ICommonService commonService, IDailyAttendanceStatusReportService dailyAttendanceStatusReportService, IHelpers helpers)
        {
            _dropdownService = dropdownService;
            _commonService = commonService;
            _dailyAttendanceStatusReportService = dailyAttendanceStatusReportService;
            _helpers = helpers;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                DailyAttendanceStatusReport model = new DailyAttendanceStatusReport();
                Global._dataset = null;
                await fillfilterDropdown();
                model.FromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                model.ToDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);
                return View("~/Areas/TimeOffice/Report/Views/DailyAttendanceStatusReport/Index.cshtml", model);
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
        public async Task<ActionResult> CountCheckAJAX(DailyAttendanceStatusReport model)
        {
            try
            {
                DataSet dataset = new DataSet();
                object ResponseEntity = new object();

                var ds = await _dailyAttendanceStatusReportService.GetDetails(model);
                DataTable dt = ds.Tables[0];

                if (dt.Columns.Count == 0)
                {
                    Global._dataset = (DataSet)ds;
                    ResponseEntity = false;
                    return Json(ResponseEntity);

                }
                else
                {
                    Global._dataset = (DataSet)ds;
                    ResponseEntity = true;
                    return Json(ResponseEntity);

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
        public async Task fillfilterDropdown()
        {

            ViewBag.Dsg = new SelectList(await _dropdownService.GetDesignationDropdown(), "Code", "Name");
            ViewBag.Dept = new SelectList(await _dropdownService.GetDepartmentDropdown(), "Code", "Name");
            ViewBag.Loc = new SelectList(await _dropdownService.GetLocationDropdown(), "Code", "Name");
            ViewBag.Cat = new SelectList(await _dropdownService.GetCategoryDropdown(), "Code", "Name");
            ViewBag.Grd = new SelectList(await _dropdownService.GetGradeDropdown(), "Code", "Name");
            ViewBag.PayrollGrp = new SelectList(await _dropdownService.GetPayrollGroupDropdown(), "Code", "Name");
            ViewBag.division = new SelectList(await _dropdownService.GetDivisionDropdown(), "Code", "Name");
            ViewBag.section = new SelectList(await _dropdownService.GetSectionDropdown(), "Code", "Name");
            ViewBag.EmploymentType = new SelectList(await _dropdownService.GetEmploymentTypeDropdown(), "Code", "Name");
            ViewBag.WorkLocation = new SelectList(await _dropdownService.GetWorkLocationDropdown(), "Code", "Name");
            ViewBag.WageGrade = new SelectList(await _dropdownService.GetWageGradeDropdown(), "Code", "Name");            
            ViewBag.AttendanceStatus = new SelectList(await _dropdownService.GetAttendanceStatusDropdown(), "Code", "Name");
            ViewBag.Reg = new SelectList(await _dropdownService.GetRegionDropDown(), "Code", "Name");
            ViewBag.St = new SelectList(await _dropdownService.GetStateDropDown(), "Code", "Name");
            ViewBag.ShiftType = new SelectList(await _dropdownService.GetShiftTypeDropDown(), "Code", "Name");
        }

        [HttpPost]
        public async Task<IActionResult> GetDetails(DailyAttendanceStatusReport model)
        {
            try
            {
                Global.FromDate = model.FromDate;
                Global.ToDate = model.ToDate;
                var ds = await _dailyAttendanceStatusReportService.GetDetails(model);
                //Global.data = model;
                //return Json(true);

                if ((ds.Tables[0]?.Rows?.Count ?? 0) > 0)
                {
                  
                    Global._dataset = ds;
                    return Json(true);
                }
                else
                {
                    return Json(false);
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



        public async Task<IActionResult> ExportToexcel(DailyAttendanceStatusReport model)
        {
            try
            {
                Global.FromDate = model.FromDate;
                Global.ToDate = model.ToDate;
                DataTable dt = new DataTable();
                var dta = Global._dataset;
                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Daily Attendance Status Report", "Excel");


                string currDateTime = await _helpers.GetCurrentDate();
                
                var ds = dta;//await _dailyAttendanceStatusReportService.GetDetails(model);
                //ds.Tables[0].Rows.RemoveAt(0);
                if ((ds.Tables[0]?.Rows?.Count ?? 0) > 0)
                {
                    dt = ds.Tables[0];
                }

                //set EXCEL Column Heading 
                for (int i = 0; i < dt.Columns.Count; i++)
                {

                    string columnName = dt.Columns[i].ColumnName;
                    if (columnName == "id" || columnName == "doj" || columnName == "lwd" || columnName == "ljp" || columnName == "ltp")
                    {
                        dt.Columns[i].ColumnName = columnName.ToUpper();
                    }
                    else if (columnName == "Name")
                    {
                        dt.Columns[i].ColumnName = "Employee Name";
                    }
                    else
                    {
                        dt.Columns[i].ColumnName = char.ToUpper(columnName[0]) + columnName.Substring(1);
                    }

                }

                var headercount = dt.Rows.Count + 2;


                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Daily Attendance Status Report");
                    int rowcount = dt.Rows.Count + 4;

                    string columnName = "";
                    int Columncount = dt.Columns.Count;

                    while (Columncount > 0)
                    {
                        int modulo = (Columncount - 1) % 26;
                        columnName = Convert.ToChar('A' + modulo) + columnName;
                        Columncount = (Columncount - modulo) / 26;
                    }

                    string colName = columnName;

                    worksheet.Range("A1", colName + rowcount).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1", colName + rowcount).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1", colName + rowcount).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1", colName + rowcount).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1", colName + rowcount).Style.Border.LeftBorder = XLBorderStyleValues.Thin;




                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, colName));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                    wsReportNameHeaderRange.Merge();

                    wsReportNameHeaderRange.Value = "Daily Attendance Status Report";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();

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

                    worksheet.Cell(3, 7).Style.Font.Bold = true;
                    worksheet.Cell(3, 7).Value = "From Date";
                    worksheet.Cell(3, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(3, 8).Style.DateFormat.Format = "dd-MMM-yyyy";
                    worksheet.Cell(3, 8).Value = Global.FromDate;

                    worksheet.Cell(3, 9).Style.Font.Bold = true;
                    worksheet.Cell(3, 9).Value = "To Date";
                    worksheet.Cell(3, 10).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(3, 10).Style.DateFormat.Format = "dd-MMM-yyyy";
                    worksheet.Cell(3, 10).Value = Global.ToDate;


                    var currentRow = 4;

                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A4:" + colName + "4").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A4:" + colName + "4").Style.Font.Bold = true;
                    worksheet.Range("E4:E" + (dt.Rows.Count + 4)).Style.DateFormat.Format = "dd-MMM-yyyy";
                    worksheet.Range("F4:F" + (dt.Rows.Count + 4)).Style.DateFormat.Format = "dd-MMM-yyyy";

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
                            "Daily Attendance Status Report.xlsx"
                            );
                        return Json(content);
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
