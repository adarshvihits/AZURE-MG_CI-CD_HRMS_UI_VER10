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
    public class PaysheetPeriodWiseController : Controller
    {
        private readonly IPaySheetService _PaySheetService;
        private readonly IDropdownService _dropdownService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;

        public static class Global
        {
            public static PaySheet data;
            public static String PayrollMonthFROM;
            public static String PayrollMonthTO;

            public static DateTime? FromDate;
            public static DateTime? ToDate;
        }
        public PaysheetPeriodWiseController(IPaySheetService PaySheetService, IDropdownService dropdownService, ICommonService commonService, IHelpers helpers)
        {
            _PaySheetService = PaySheetService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                await fillfilterDropdown();
                PaySheet model = new PaySheet();

                model.BasedOn = 1;
                return View("~/Areas/PayRoll/Reports/Views/PaySheetPeriodWise/Index.cshtml", model);
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
        [HttpPost]
        public async Task<IActionResult> CheckDataCount(PaySheet model, string PayrollMonthFROM, string PayrollMonthTO)
        {
          
            int count = await _PaySheetService.GetPaysheetDataCount((int)model.PayrollMonthFROM.Code, model, "PERIODWISE");
          
            Global.PayrollMonthFROM = PayrollMonthFROM;
            Global.PayrollMonthTO = PayrollMonthTO;
            return Json(count);
        }
        public async Task<IActionResult> ExportToExcelPaySheet(PaySheet model)
        {

            var LoginUser = HttpContext.Session.GetString("LoginUser");
            var CompanyName = HttpContext.Session.GetString("CompanyName");
            var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
            var GroupCompanyName = User.authUser.GroupCompanyName;
            ExcelPassword EP = new ExcelPassword();
            EP = await _commonService.GetExcelPassword("Pay Sheet", "Excel");


            string currDateTime = await _helpers.GetCurrentDate();
            

            var dataset = await _PaySheetService.GetPaySheetPeriodWiseDetails((int)model.PayrollMonthFROM.Code, (int)model.PayrollMonthTO.Code, model);
            dataset.Tables[0].Rows.RemoveAt(0);
            DataTable dt = dataset.Tables[0];
            DataTable dt1 = dataset.Tables[1];
            var index = 0;

            //set EXCEL Column Heading 
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (dt.Columns[i].ColumnName == "doj" || dt.Columns[i].ColumnName == "dob" || dt.Columns[i].ColumnName == "dor" || dt.Columns[i].ColumnName == "ifsc" ||
                    dt.Columns[i].ColumnName == "hra")
                    dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
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
                if (dt.Columns[i].ColumnName.Contains("esi"))
                {
                    index = dt.Columns[i].ColumnName.IndexOf("esi");
                    if (index == 0)
                        dt.Columns[i].ColumnName = ConvertSubstringToUpperCase(dt.Columns[i].ColumnName, index, 3);
                }
                if (dt.Columns[i].ColumnName.Contains("pan"))
                {
                    index = dt.Columns[i].ColumnName.IndexOf("pan");
                    if (index == 0)
                        dt.Columns[i].ColumnName = ConvertSubstringToUpperCase(dt.Columns[i].ColumnName, index, 3);
                }
                if (dt.Columns[i].ColumnName.Contains("lop"))
                {
                    index = dt.Columns[i].ColumnName.IndexOf("lop");
                    dt.Columns[i].ColumnName = ConvertSubstringToUpperCase(dt.Columns[i].ColumnName, index, 3);
                }

                else
                    dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
            }


            using (XLWorkbook wb = new XLWorkbook())
            {


                var worksheet = wb.Worksheets.Add("PaySheet" );
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

                wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                wsReportNameHeaderRange.Merge();
                wsReportNameHeaderRange.Value = "Pay Sheet";

                worksheet.Cell(2, 1).Style.Font.Bold = true;
                worksheet.Cell(2, 1).Value = "Group Company Name";
                worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();



                worksheet.Cell(2, 3).Style.Font.Bold = true;
                worksheet.Cell(2, 3).Value = "Month";
                worksheet.Cell(2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(2, 4).Style.DateFormat.Format = "MMM-yyyy";
                worksheet.Cell(2, 4).Value = dt1.Rows[0].ItemArray[0] +" - " + dt1.Rows[0].ItemArray[1];
             


                worksheet.Cell(3, 1).Style.Font.Bold = true;
                worksheet.Cell(3, 1).Value = "Login User";
                worksheet.Cell(3, 2).Value = LoginUser.ToString();
                worksheet.Cell(3, 3).Style.Font.Bold = true;
                worksheet.Cell(3, 3).Value = "Date & Time";
                worksheet.Cell(3, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(3, 4).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
                worksheet.Cell(3, 4).Value = currDateTime.ToString();
                worksheet.Cell(3, 5).Style.Font.Bold = true;
                worksheet.Cell(3, 5).Value = "Total Count";
                worksheet.Cell(3, 6).Value = dt.Rows.Count;


                var currentRow = 4;

                worksheet.Cell(currentRow, 1).InsertTable(dt);
                worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                worksheet.Range("A4:" + colName + "4").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                worksheet.Range("A4:" + colName + "4").Style.Font.Bold = true;
                worksheet.Range("E4:E" + (dt.Rows.Count + 4)).Style.DateFormat.Format = "dd-MMM-yyyy";
                worksheet.Range("F4:F" + (dt.Rows.Count + 4)).Style.DateFormat.Format = "dd-MMM-yyyy";
                worksheet.Range("G4:G" + (dt.Rows.Count + 4)).Style.DateFormat.Format = "dd-MMM-yyyy";
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
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "PaySheet(" + Global.PayrollMonthFROM + "," + Global.PayrollMonthTO + ").xlsx");
                }
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
            ViewBag.PayModes = new SelectList(await _dropdownService.GetPaymentModeDropDown(), "Code", "Name");
            ViewBag.AttendanceStatus = new SelectList(await _dropdownService.GetAttendanceStatusDropdown(), "Code", "Name");
            ViewBag.Type = new SelectList(await _dropdownService.GetPaySheetTypeDropDown(), "Code", "Name");
            ViewBag.monthFROM = new SelectList(await _dropdownService.GetPayrollReportMonthDropdown(), "Code", "Name");
            ViewBag.monthTO = new SelectList(await _dropdownService.GetPayrollReportMonthDropdown(), "Code", "Name");
            ViewBag.Reg = new SelectList(await _dropdownService.GetRegionDropDown(), "Code", "Name");
            ViewBag.St = new SelectList(await _dropdownService.GetStateDropDown(), "Code", "Name");

        }
    }
}
