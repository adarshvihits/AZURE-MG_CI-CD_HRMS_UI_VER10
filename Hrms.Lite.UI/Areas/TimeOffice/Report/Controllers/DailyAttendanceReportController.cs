using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.TimeOffice.Report;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice;
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

namespace Hrms.Lite.UI.Areas.TimeOffice.Report.Controllers
{
    [Area("TimeOffice")]
    [SessionAuthorize]
    public class DailyAttendanceReportController : Controller
    {

        private readonly IDropdownService _dropdownService;
        private readonly IDailyAttendanceReportService _service;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        public DailyAttendanceReportController(IDailyAttendanceReportService service, IDropdownService dropdownService, ICommonService commonService, IHelpers Helpers)
        {
            _service = service;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = Helpers;
        }
        public static class Global
            {
            public static DateTime from;
            public static DateTime to;
            public static int statusCode;
            public static int typeCode;
            public static Filter filter;
        }
        public async Task<IActionResult> Index()
        {
            DailyAttendanceEntry model=new DailyAttendanceEntry();
            ViewBag.AttnStatusReport = new SelectList(await _dropdownService.GetDailyAttnReportTypeDropDown(), "Code", "Name");
            ViewBag.AttnStatus = new SelectList(await _dropdownService.GetAttendanceStatusDropdown(), "Code", "Name");
            model.Filter = await _commonService.GetFilterDropdowns();
            model.From = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            model.To = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);
            return View("~/Areas/TimeOffice/Report/Views/DailyAttendanceReport/Index.cshtml", model);
        }

     





        [HttpPost]
        public async Task<IActionResult> GetDailyAttendanceReportList(Filter filter1, DateTime From,DateTime To,string AttnStatus,int AttnType)
        {
            try
            {
                DailyAttendanceEntry model = new DailyAttendanceEntry();

                Filter filter = new Filter();
                Global.from = From;
                Global.to = To;
               // Global.statusCode = AttnStatus;
                Global.typeCode = AttnType;
                Global.filter = filter1;

                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }



                if (AttnStatus != null)
                {
                    string[] values = AttnStatus.Split(',');
                    for (int i = 0; i < values.Length; i++)
                    {
                        values[i] = values[i].Trim();
                    }

                    model.Attendancestatus = Array.ConvertAll(values, int.Parse);
                }

                //if (AttnStatus != 0|| AttnType != 0)

                model.FromDate = From.ToString("yyyy-MMM-dd");
                model.ToDate = To.ToString("yyyy-MMM-dd");
                model.Filter = filter1;
                model.AttendanceType = AttnType;
                model = await _service.GetDailyAttendanceReportList(model);

  //   model = await _service.GetDailyAttendanceReportList(From.ToString("yyyy-MMM-dd"),To.ToString("yyyy-MMM-dd"), AttnType, AttnStatus,filter1);

                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.DailyAttendanceProcessing == null || model.DailyAttendanceProcessing.Count() == 0)
                {


                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows

                    });
                }

                if (model.DailyAttendanceProcessing.Count > 0)
                {
                    totalrows = model.DailyAttendanceProcessing[0].active;
                }


                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = model.DailyAttendanceProcessing[0].totalRecords;

                    return Json(new
                    {
                        data = model.DailyAttendanceProcessing,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering

                    });
                }
                else
                {

                    return Json(new
                    {
                        data = model.DailyAttendanceProcessing,
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
        public async Task<IActionResult> ExportToExcel(int totRecords, Filter filter, DateTime From, DateTime To, int AttendanceType, string Attendancestatus)


        //public async Task<IActionResult> ExportToExcelDataset(string statusName, string typeName, int totRecords)
        {
            try
            {

                DailyAttendanceEntry model = new DailyAttendanceEntry();
                filter.length = totRecords;
                filter.start = 0;

                if (Attendancestatus != null)
                {
                    string[] values = Attendancestatus.Split(',');
                    for (int i = 0; i < values.Length; i++)
                    {
                        values[i] = values[i].Trim();
                    }

                    model.Attendancestatus = Array.ConvertAll(values, int.Parse);
                }


                string FilterValues = await _helpers.GetFilterValues(filter);
                var GroupByLIst = await _dropdownService.GetEmployeeProfileSummaryGroupByDropDown();
                ViewBag.groupBy = new SelectList(GroupByLIst, "Code", "Name");
                int firstgroupById = GroupByLIst[0].Code;


                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Daily Attendance Report (" + From + " to " + To + ")", "Excel");

                model.FromDate = From.ToString("yyyy-MMM-dd");
                model.ToDate = To.ToString("yyyy-MMM-dd");
                model.AttendanceType = AttendanceType;

                if (FilterValues == "")
                {
                    FilterValues = "All";
                }
                string currDateTime = await _helpers.GetCurrentDate();
                
                var dataset = await _service.DailyAttendanceReport_Excel(model);

                //var dataset = await _service.DailyAttendanceReport_Excel(From.ToString("yyyy-MMM-dd"), To.ToString("yyyy-MMM-dd"), AttendanceType, Attendancestatus, filter);

                DataTable dt = dataset.Tables[0];

                dataset.Tables[0].Rows.RemoveAt(0);
                if ((dataset.Tables[0]?.Rows?.Count ?? 0) > 0)
                {
                    dt = dataset.Tables[0];
                }


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
                    var worksheet = workbook.Worksheets.Add("Daily Attendance Report");
                    int count = dt.Rows.Count + 5;

                    worksheet.Range("A1:T" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:T" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:T" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:T" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:T" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 19)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                    wsReportNameHeaderRange.Value = "Daily Attendance Report";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                    worksheet.Cell(2, 3).Style.Font.Bold = true;
                    worksheet.Cell(2, 3).Value = "From";
                    worksheet.Cell(2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(2, 4).Value = From;
                    worksheet.Cell(2, 4).Style.DateFormat.Format = "dd-MMM-yyyy";
                    //worksheet.Cell(2, 4).Value = currDateTime.ToString();
                    worksheet.Cell(2, 5).Style.Font.Bold = true;
                    worksheet.Cell(2, 5).Value = "To";
                    worksheet.Cell(2, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(2, 6).Value = To;
                    worksheet.Cell(2, 6).Style.DateFormat.Format = "dd-MMM-yyyy";

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
                    worksheet.Range("B4:T4").Merge();
                    worksheet.Cell("B4").Value = FilterValues;



                    var currentRow = 5;

                    worksheet.Cell(currentRow, 1).InsertTable(dt);


                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A5:T5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A5:T5").Style.Font.Bold = true;


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
                            book.Protect(EP.Password, false, false);
                            book.SaveToStream(stream);
                        }
                        var content = stream.ToArray();
                        return File(
                            content,
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            "Daily Attendance Report List.xlsx"
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
