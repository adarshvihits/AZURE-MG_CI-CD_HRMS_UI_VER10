using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.TimeOffice.Report;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Hrms.Lite.Shared.TimeOffice.Report;
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
    public class EmployeeVsLeaveDetailsReportController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly ICommonService _commonService;
        private readonly IEmployeeVsLeaveDetailsReportService _service;
        private readonly IHelpers _helpers;
        public EmployeeVsLeaveDetailsReportController(IDropdownService dropdownService, IEmployeeVsLeaveDetailsReportService service, ICommonService commonService, IHelpers helpers)
        {
            _dropdownService = dropdownService;
            _service = service;
            _commonService = commonService;
            _helpers = helpers;
        }
        public async Task<IActionResult> Index()
        {
            try {
                LeaveRegister model = new LeaveRegister();
                ViewBag.LeavePeriod = new SelectList(await _dropdownService.GetLeavePeriodFilterDropdown(), "Code", "Name");
                ViewBag.Emp = new SelectList(await _dropdownService.GetEmployeeLeaveDropDown("ACTIVE",0), "Code", "Name");
                var CurrLeavePeriod = await _commonService.GetCurrentLeavePeriod();
                model = await _service.GetEmployeeVsLeaveReports(Convert.ToInt32(CurrLeavePeriod.Data), ViewBag.Emp.Items[0].Code);
                if (model.LeaveRegisterList != null && model.LeaveRegisterList.Count > 0)
                    model.EmployeeHeader = await _commonService.GetEmployeeHeader(model.LeaveRegisterList[0].EmployeeGI);
                model.LeavePeriod = new LeavePeriod();
               //model.LeavePeriod.Code = Convert.ToInt32(CurrLeavePeriod.Data);
               // model.EmployeeCode = ViewBag.Emp.Items[0].Code;
                model.Type = "ACTIVE";
              
                return View("~/Areas/TimeOffice/Report/Views/EmployeeVsLeaveDetailsReport/Index.cshtml",model);
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

        public async Task<IActionResult> GetList(int EmployeeCode,int LeavePeriod)
        {
            try
            {    
                LeaveRegister model = new LeaveRegister();
                model = await _service.GetEmployeeVsLeaveReports(LeavePeriod, EmployeeCode);
                if (model.LeaveRegisterList != null && model.LeaveRegisterList.Count > 0)
                    model.EmployeeHeader = await _commonService.GetEmployeeHeader(model.LeaveRegisterList[0].EmployeeGI);
                return View("~/Areas/TimeOffice/Report/Views/EmployeeVsLeaveDetailsReport/_LeaveList.cshtml", model);
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
        public async Task<JsonResult> GetEmployeeList(string Type,int LeavePeriodCode)
        {

            var employee = new SelectList(await _dropdownService.GetEmployeeLeaveDropDown(Type, LeavePeriodCode), "Code", "Name");
            return Json(employee);

        }

        public async Task<IActionResult> GetLeaveDetails(int EmployeeCode,int LeavePeriod,int LeaveCode)
        {
            try
            {    
                LeaveRegister model = new LeaveRegister();
                model = await _service.GetEmployeeVsLeaveReportDetails(LeavePeriod, EmployeeCode,LeaveCode);
               
                return View("~/Areas/TimeOffice/Report/Views/EmployeeVsLeaveDetailsReport/_LeaveDetailsList.cshtml", model);
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
        public async Task<IActionResult> ExportToexcel(int LeavePeriod,int EmployeeCode)
        {
            try
            {
                
                DataTable dt = new DataTable();
                
                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Employee Vs Leave Details Report", "Excel");


                string currDateTime = await _helpers.GetCurrentDate();
                
                var ds = await _service.GetEmployeeVsLeaveReports_Excel(LeavePeriod, EmployeeCode);
                //ds.Tables[0].Rows.RemoveAt(0);
                //if ((ds.Tables[0]?.Rows?.Count ?? 0) > 0)
                //{
                //    dt = ds.Tables[0];
                //}
                    dt = ds.Tables[0];

                //set EXCEL Column Heading 
                for (int i = 0; i < dt.Columns.Count; i++)
                {

               
                    if (dt.Columns[i].ColumnName == "cf" )
                        dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                    else
                        dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                    if (dt.Columns[i].ColumnName == "Name")
                        dt.Columns[i].ColumnName = "Employee Name";

                }

                var headercount = dt.Rows.Count + 2;


                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Emp Vs Leave Details Report");
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

                    wsReportNameHeaderRange.Value = "Employee Vs Leave Details Report";

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
                            book.Protect(EP.Password, false, false);
                            book.SaveToStream(stream);
                        }
                        var content = stream.ToArray();
                        return File(
                            content,
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            "Employee Vs Leave Details Report.xlsx"
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
        public async Task<IActionResult> ExportToExcelDetails(int LeaveCode, int LeavePeriod, int EmployeeCode)
        {
            try
            {
                LeaveRegister model = new LeaveRegister();
                DataTable dt = new DataTable();

                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Employee Vs Leave Details Report", "Excel");


                string currDateTime = await _helpers.GetCurrentDate();
                
                var ds = await _service.GetEmployeeVsLeaveDetailsReports_Excel(LeavePeriod, EmployeeCode,LeaveCode);
               
                dt = ds.Tables[0];

                //set EXCEL Column Heading 
                for (int i = 0; i < dt.Columns.Count; i++)
                {


                    if (dt.Columns[i].ColumnName == "cf")
                        dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                    else
                        dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                    if (dt.Columns[i].ColumnName == "Name")
                        dt.Columns[i].ColumnName = "Employee Name";

                }

                var headercount = dt.Rows.Count + 2;


                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Emp Vs Leave Details Report");
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

                    wsReportNameHeaderRange.Value = "Employee Vs Leave Details Report";

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
                            book.Protect(EP.Password, false, false);
                            book.SaveToStream(stream);
                        }
                        var content = stream.ToArray();
                        return File(
                            content,
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            "Employee Vs Leave Details Report.xlsx"
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
