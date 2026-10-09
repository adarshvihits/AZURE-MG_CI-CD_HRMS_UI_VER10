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
    public class TaskReportController : Controller
    {
        private readonly ITaskReportService _TaskReportService;
        private readonly ICommonService _commonService;
        private readonly IDropdownService _dropdownService;
        private readonly IHelpers _helpers;
        public TaskReportController(ITaskReportService _separationReportService, ICommonService commonService, IDropdownService dropdownService, IHelpers Helpers)
        {
            _TaskReportService = _separationReportService;
            _commonService = commonService;
            _dropdownService = dropdownService;
            _helpers = Helpers;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                EmployeeTaskReport model = new EmployeeTaskReport();
               
                ViewBag.Status = new SelectList(await _dropdownService.GetEmployeeTaskStatusDropDown(), "Code", "Name");

                model.Filter = await _commonService.GetFilterDropdowns();


                return View("~/Areas/DataBank/Reports/Views/TaskReport/Index.cshtml", model);
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
        public async Task<IActionResult> GetList(string Status,Filter filter1)
        {
            try
            {
                EmployeeTaskReport model = new EmployeeTaskReport();
                EmployeeTaskReport m = new EmployeeTaskReport();


                Filter filter = new Filter();

                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }

                model = await _TaskReportService.TaskReportList(Status, filter1);

                m.EmployeeTaskReportList = model.EmployeeTaskReportList;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.EmployeeTaskReportList == null || model.EmployeeTaskReportList.Count() == 0)
                {


                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }

                if (model.EmployeeTaskReportList.Count > 0)
                {
                    totalrows = m.EmployeeTaskReportList[0].active;


                }


                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.EmployeeTaskReportList[0].totalRecords;

                    return Json(new
                    {
                        data = model.EmployeeTaskReportList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {

                    return Json(new
                    {
                        data = model.EmployeeTaskReportList,
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


        public async Task<IActionResult> ExportToExcel(int TotalRows, string Status,string statusRemarks, Filter filter1)
        {
            try
            {
                JobTransitionReport _model = new JobTransitionReport();
                
                _model.GridSettings = new List<JobTransitionReport_GridSettings>();

                filter1.length = TotalRows;
               

                string FilterValues = await _helpers.GetFilterValues(filter1);
                if (FilterValues == "")
                {
                    FilterValues = "All";
                }
                string currDateTime = await _helpers.GetCurrentDate();
                
                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("To Do Report List", "Excel");



                var dataset = await _TaskReportService.GetTaskReport_Excel(Status, filter1);
                DataTable dt = dataset.Tables[0];
                dataset.Tables[0].Rows.RemoveAt(0);
                if ((dataset.Tables[0]?.Rows?.Count ?? 0) > 0)
                {
                    dt = dataset.Tables[0];
                }

                //set EXCEL Column Heading 
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName == "pfmc" || dt.Columns[i].ColumnName == "ctc" || dt.Columns[i].ColumnName == "esimc")
                        dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                    else
                        dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                }


                using (var workbook = new XLWorkbook())
                {

                    var worksheet = workbook.Worksheets.Add("To Do Report List");


                    int count = dt.Rows.Count + 5;

                    string columnName = "";

                    int Columncount = dt.Columns.Count;

                    while (Columncount > 0)
                    {
                        int modulo = (Columncount - 1) % 26;
                        columnName = Convert.ToChar('A' + modulo) + columnName;
                        Columncount = (Columncount - modulo) / 26;
                    }

                    string colName = columnName;

                    worksheet.Range("A1", colName + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1", colName + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1", colName + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1", colName + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1", colName + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;






                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, colName));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();


                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Value = "To Do Report List" + "(" + statusRemarks + ")";
                    //worksheet.Range("A5:J5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                    worksheet.Cell(2, 3).Style.Font.Bold = true;
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
                    var filterapplied = worksheet.Range("B4", colName + 4).Merge();

                    filterapplied.Value = FilterValues;

                    var currentRow = 5;

                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A5:" + colName + "5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A5:" + colName + "5").Style.Font.Bold = true;
                    worksheet.Range("F5:F" + (dt.Rows.Count + 5)).Style.DateFormat.Format = "dd-MMM-yyyy";

                    worksheet.Columns().AdjustToContents();
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
                            "To Do Report List" + "(" + statusRemarks + ").xlsx"
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
        public async Task<IActionResult> TaskReports()
        {
            try
            {
                EmployeeTaskReport model = new EmployeeTaskReport();

                ViewBag.Status = new SelectList(await _dropdownService.GetEmployeeTaskStatusDropDown(), "Code", "Name");

                model.Filter = await _commonService.GetFilterDropdowns();


                return View("~/Areas/DataBank/Reports/Views/TaskReport/TaskReports.cshtml", model);
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
        public async Task<IActionResult> GetTaskReportList(string Status, Filter filter1)
        {
            try
            {
                EmployeeTaskReport model = new EmployeeTaskReport();
                EmployeeTaskReport m = new EmployeeTaskReport();


                Filter filter = new Filter();

                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }

                model = await _TaskReportService.GetTaskReportList(Status, filter1);

                m.EmployeeTaskReportList = model.EmployeeTaskReportList;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.EmployeeTaskReportList == null || model.EmployeeTaskReportList.Count() == 0)
                {


                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }

                if (model.EmployeeTaskReportList.Count > 0)
                {
                    totalrows = m.EmployeeTaskReportList[0].active;


                }


                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.EmployeeTaskReportList[0].totalRecords;

                    return Json(new
                    {
                        data = model.EmployeeTaskReportList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {

                    return Json(new
                    {
                        data = model.EmployeeTaskReportList,
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
        public async Task<IActionResult> TaskReportExportToExcel(int TotalRows, string Status, string statusRemarks, Filter filter1)
        {
            try
            {
                JobTransitionReport _model = new JobTransitionReport();

                _model.GridSettings = new List<JobTransitionReport_GridSettings>();

                filter1.length = TotalRows;


                string FilterValues = await _helpers.GetFilterValues(filter1);
                if (FilterValues == "")
                {
                    FilterValues = "All";
                }
                string currDateTime = await _helpers.GetCurrentDate();
                
                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Task Report List", "Excel");



                var dataset = await _TaskReportService.TaskReport_Excel(Status, filter1);
                DataTable dt = dataset.Tables[0];
                dataset.Tables[0].Rows.RemoveAt(0);
                if ((dataset.Tables[0]?.Rows?.Count ?? 0) > 0)
                {
                    dt = dataset.Tables[0];
                }

                //set EXCEL Column Heading 
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName == "pfmc" || dt.Columns[i].ColumnName == "ctc" || dt.Columns[i].ColumnName == "esimc")
                        dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                    else
                        dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                }


                using (var workbook = new XLWorkbook())
                {

                    var worksheet = workbook.Worksheets.Add("To Do Report List");


                    int count = dt.Rows.Count + 5;

                    string columnName = "";

                    int Columncount = dt.Columns.Count;

                    while (Columncount > 0)
                    {
                        int modulo = (Columncount - 1) % 26;
                        columnName = Convert.ToChar('A' + modulo) + columnName;
                        Columncount = (Columncount - modulo) / 26;
                    }

                    string colName = columnName;

                    worksheet.Range("A1", colName + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1", colName + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1", colName + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1", colName + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1", colName + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;






                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, colName));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();


                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Value = "To Do Report List" + "(" + statusRemarks + ")";
                    //worksheet.Range("A5:J5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                    worksheet.Cell(2, 3).Style.Font.Bold = true;
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
                    var filterapplied = worksheet.Range("B4", colName + 4).Merge();

                    filterapplied.Value = FilterValues;

                    var currentRow = 5;

                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A5:" + colName + "5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A5:" + colName + "5").Style.Font.Bold = true;
                    worksheet.Range("F5:F" + (dt.Rows.Count + 5)).Style.DateFormat.Format = "dd-MMM-yyyy";

                    worksheet.Columns().AdjustToContents();
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
                            "To Do Report List" + "(" + statusRemarks + ").xlsx"
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


        public async Task<IActionResult> TaskActionDetails(int TaskCode )
        {
            try
            {
                EmployeeTaskReport model = new EmployeeTaskReport();
                model = await _TaskReportService.GetActionDetails(TaskCode);



                return View("~/Areas/DataBank/Reports/Views/TaskReport/_ActionDetails.cshtml", model);
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
