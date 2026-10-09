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
    [SessionAuthorize]
    [Area("DataBank")]
    public class StaffConfirmationReportController : Controller
    {
        private readonly IStaffConfirmationReportService _service;
        private readonly IDropdownService _dropdownService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;

        public StaffConfirmationReportController(IStaffConfirmationReportService StaffConfirmationReportService, IDropdownService dropdownService, ICommonService commonService, IHelpers helpers)
        {
            _service = StaffConfirmationReportService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;
        }

        public async Task<IActionResult> Index()
        {
            try { 
            StaffConfirmationReport model = new StaffConfirmationReport();
            ViewBag.PayrollGroup = new SelectList(await _dropdownService.GetPayrollGroupDropdown(), "Code", "Name");
            ViewBag.Month = new SelectList(await _dropdownService.GetMonthDropdown(), "Code", "Name");
                model = await _service.GetStaffConfirmationList(0);
                model.Filter = await _commonService.GetFilterDropdowns();
            return View("~/Areas/DataBank/Reports/Views/StaffConfirmationReport/Index.cshtml", model);
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

        public async Task<IActionResult> GetList(int Month)
        {
            try
            {

                StaffConfirmationReport model = new StaffConfirmationReport();
                model = await _service.GetStaffConfirmationList(Month);
                return View("/Areas/DataBank/Reports/Views/StaffConfirmationReport/_List.cshtml", model);

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
        public async Task<ActionResult> GetDetails(Guid EmployeeGI, int Month)
        {
            try
            {
                StaffConfirmationReport model = new StaffConfirmationReport();


                model = await _service.GetStaffConfirmationReportDetails(EmployeeGI, Month);
                model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);
                model.EmployeeGI = EmployeeGI;
                return PartialView("~/Areas/DataBank/Reports/Views/StaffConfirmationReport/_PopUp.cshtml", model);

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





        public async Task<IActionResult> ExportToExcel(int Month, Filter filter, int totRecords)
        {
            var LoginUser = HttpContext.Session.GetString("LoginUser");
            var CompanyName = HttpContext.Session.GetString("CompanyName");
            var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
            var GroupCompanyName = User.authUser.GroupCompanyName;
            ExcelPassword EP = new ExcelPassword();
            EP = await _commonService.GetExcelPassword("Staff Confirmation Report", "Excel");

            string currDateTime = await _helpers.GetCurrentDate();
            

            filter.length = totRecords;
            var dataset = await _service.GetStaffConfirmationReport_Excel(Month, filter);

            DataTable dt = dataset.Tables[0];

            //set EXCEL Column Heading 
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (dt.Columns[i].ColumnName == "doj")

                    dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                if (dt.Columns[i].ColumnName == "employee Id")

                    dt.Columns[i].ColumnName = "Employee ID";

                else

                    dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
            }


            using (XLWorkbook wb = new XLWorkbook())
            {


                var worksheet = wb.Worksheets.Add("Staff Confirmation Report");
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
                wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);

                wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                wsReportNameHeaderRange.Merge();
                wsReportNameHeaderRange.Value = "Staff Confirmation Report";

                worksheet.Cell(2, 1).Style.Font.Bold = true;
                worksheet.Cell(2, 1).Value = "Group Company Name";
                worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();

                //worksheet.Cell(2, 3).Style.Font.Bold = true;
                //worksheet.Cell(2, 3).Value = "From";
                //worksheet.Cell(2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                //worksheet.Cell(2, 4).Value = fromDate;
                //worksheet.Cell(2, 4).Style.DateFormat.Format = "dd-MMM-yyyy";
                //worksheet.Cell(2, 5).Style.Font.Bold = true;
                //worksheet.Cell(2, 5).Value = "To";
                //worksheet.Cell(2, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                //worksheet.Cell(2, 6).Value = toDate;
                //worksheet.Cell(2, 6).Style.DateFormat.Format = "dd-MMM-yyyy";
                worksheet.Cell(2, 7).Style.Font.Bold = true;
                worksheet.Cell(2, 7).Value = "Total Count";
                worksheet.Cell(2, 8).Value = dt.Rows.Count;
                worksheet.Cell(2, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);


                worksheet.Cell(3, 1).Style.Font.Bold = true;
                worksheet.Cell(3, 1).Value = "Login User";
                worksheet.Cell(3, 2).Value = LoginUser.ToString();
                worksheet.Cell(3, 3).Style.Font.Bold = true;
                worksheet.Cell(3, 3).Value = "Date & Time";
                worksheet.Cell(3, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(3, 4).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
                worksheet.Cell(3, 4).Value = currDateTime.ToString();
                //worksheet.Cell(3, 5).Style.Font.Bold = true;
                //worksheet.Cell(3, 5).Value = "Total Count";
                //worksheet.Cell(3, 6).Value = dt.Rows.Count;
                //worksheet.Cell(3, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);


                var currentRow = 4;

                worksheet.Cell(currentRow, 1).InsertTable(dt);
                worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                worksheet.Range("A4:" + colName + "4").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                worksheet.Range("A4:" + colName + "4").Style.Font.Bold = true;

                worksheet.Columns().AdjustToContents();
                worksheet.Rows().AdjustToContents();
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    if (EP.NeedPassword)
                    {
                        Workbook book = new Workbook();
                        book.LoadFromStream(stream);
                        //Protect Workbook
                        book.Protect(EP.Password, false, false);
                        book.SaveToStream(stream);
                    }
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Staff Confirmation Report.xlsx");
                }
            }
        }


        public async Task<IActionResult> ItemDetailsExportToExcel(Guid EmployeeGI,int Month)
        {
            var LoginUser = HttpContext.Session.GetString("LoginUser");
            var CompanyName = HttpContext.Session.GetString("CompanyName");
            var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
            var GroupCompanyName = User.authUser.GroupCompanyName;
            ExcelPassword EP = new ExcelPassword();
            EP = await _commonService.GetExcelPassword("Onboarding Checklist Details List", "Excel");


            string currDateTime = await _helpers.GetCurrentDate();
            


            var dataset = await _service.GetStaffConfirmationReportDetails_Excel(EmployeeGI,Month);

            DataTable dt = dataset.Tables[0];
            //DataTable dt1 = dataset.Tables[1];

            //set EXCEL Column Heading 
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (dt.Columns[i].ColumnName == "doj")

                    dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                if (dt.Columns[i].ColumnName == "employee Id")

                    dt.Columns[i].ColumnName = "Employee ID";

                else

                    dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
            }


            using (XLWorkbook wb = new XLWorkbook())
            {


                var worksheet = wb.Worksheets.Add("Staff Confirmation Report");
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
                wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);

                wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                wsReportNameHeaderRange.Merge();
                wsReportNameHeaderRange.Value = "Staff Confirmation Report";

                worksheet.Cell(2, 1).Style.Font.Bold = true;
                worksheet.Cell(2, 1).Value = "Group Company Name";
                worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();

                //worksheet.Cell(2, 3).Style.Font.Bold = true;
                //worksheet.Cell(2, 3).Value = "From";
                //worksheet.Cell(2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                //worksheet.Cell(2, 4).Value = fromDate;
                //worksheet.Cell(2, 4).Style.DateFormat.Format = "dd-MMM-yyyy";
                //worksheet.Cell(2, 5).Style.Font.Bold = true;
                //worksheet.Cell(2, 5).Value = "To";
                //worksheet.Cell(2, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                //worksheet.Cell(2, 6).Value = toDate;
                //worksheet.Cell(2, 6).Style.DateFormat.Format = "dd-MMM-yyyy";
                worksheet.Cell(2, 7).Style.Font.Bold = true;
                worksheet.Cell(2, 7).Value = "Total Count";
                worksheet.Cell(2, 8).Value = dt.Rows.Count;
                worksheet.Cell(2, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);


                worksheet.Cell(3, 1).Style.Font.Bold = true;
                worksheet.Cell(3, 1).Value = "Login User";
                worksheet.Cell(3, 2).Value = LoginUser.ToString();
                worksheet.Cell(3, 3).Style.Font.Bold = true;
                worksheet.Cell(3, 3).Value = "Date & Time";
                worksheet.Cell(3, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(3, 4).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
                worksheet.Cell(3, 4).Value = currDateTime.ToString();
                //worksheet.Cell(3, 5).Style.Font.Bold = true;
                //worksheet.Cell(3, 5).Value = "Total Count";
                //worksheet.Cell(3, 6).Value = dt.Rows.Count;
                //worksheet.Cell(3, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);


                var currentRow = 4;

                worksheet.Cell(currentRow, 1).InsertTable(dt);
                worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                worksheet.Range("A4:" + colName + "4").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                worksheet.Range("A4:" + colName + "4").Style.Font.Bold = true;

                worksheet.Columns().AdjustToContents();
                worksheet.Rows().AdjustToContents();
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    if (EP.NeedPassword)
                    {
                        Workbook book = new Workbook();
                        book.LoadFromStream(stream);
                        //Protect Workbook
                        book.Protect(EP.Password, false, false);
                        book.SaveToStream(stream);
                    }
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Staff Confirmation Report.xlsx");
                }
            }
        }
    }
}
