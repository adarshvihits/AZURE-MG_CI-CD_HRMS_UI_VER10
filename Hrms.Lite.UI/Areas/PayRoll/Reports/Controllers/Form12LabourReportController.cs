using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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

namespace Hrms.Lite.UI.Areas.PayRoll.Reports.Controllers
{
    [Area("PayRoll")]
    [SessionAuthorize]
    public class Form12LabourReportController : Controller
    {
        private readonly IForm12LabourReportService _Form12Service;
        private readonly IDropdownService _dropdownService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        public Form12LabourReportController(IForm12LabourReportService Form12Service, IDropdownService dropdownService, ICommonService commonService, IHelpers helpers)
        {
            _Form12Service = Form12Service;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                await fillfilterDropdown();
                Form12LabourReport model = new Form12LabourReport();

                var dataset = await _Form12Service.GetDetails(0, model);
                return View("~/Areas/PayRoll/Reports/Views/Form12LabourReport/Index.cshtml", model);
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

        public async Task<IActionResult> ExportToExcelForm12(Form12LabourReport model)
        {

            var LoginUser = HttpContext.Session.GetString("LoginUser");
            var CompanyName = HttpContext.Session.GetString("CompanyName");
            var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
            var GroupCompanyName = User.authUser.GroupCompanyName;
            ExcelPassword EP = new ExcelPassword();
            EP = await _commonService.GetExcelPassword("Labour Report", "Excel");


            string currDateTime = await _helpers.GetCurrentDate();
            

            var dataset = await _Form12Service.GetDetails((int)model.PayrollMonth.Code, model);

            DataTable dt = dataset.Tables[0];


            //set EXCEL Column Heading 
            for (int i = 0; i < dt.Columns.Count; i++)
            {

                dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
            }


            using (XLWorkbook wb = new XLWorkbook())
            {


                var worksheet = wb.Worksheets.Add("Labour Report");
                int rowcount = dt.Rows.Count + 14;
                string columnName = "";
                int Columncount = dt.Columns.Count;

                while (Columncount > 0)
                {
                    int modulo = (Columncount - 1) % 26;
                    columnName = Convert.ToChar('A' + modulo) + columnName;
                    Columncount = (Columncount - modulo) / 26;
                }

                string colName = columnName;





                var wsReportNameHeaderRange = worksheet.Range("B1", "B1");
                wsReportNameHeaderRange.Style.Font.Bold = true;
                wsReportNameHeaderRange.Style.Font.FontSize = 12;
                wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);

                wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Rows("1:1").Height = 100;


                var line1 = "Register of Adult Workers as prescribed under Section 62 of the Act";
                var line2 = "Form No. 12";
                var line3 = "(Rule 78)";
                var line4 = "Register of Adult Workers as prescribed under Section 02 of the Act";

                //wsReportNameHeaderRange.Value = "Register of Adult Workers as prescribed under Section 62 of the Act " + Environment.NewLine + " Form No. 12 " + Environment.NewLine + "   (Rule 78) " + Environment.NewLine + " Register of Adult Workers as prescribed under Section 02 of the Act ";
                wsReportNameHeaderRange.Value = $"{line1} " + Environment.NewLine + $"{line2}" + Environment.NewLine + $"{line3}" + Environment.NewLine + $"{line4}";

                worksheet.Cell(2, 1).Style.Font.Bold = true;

                worksheet.Cell(3, 4).Value = "Rest Period";
                worksheet.Cell(4, 3).Value = "Time of Commencement work";
                worksheet.Cell(4, 4).Value = "From";
                worksheet.Cell(4, 5).Value = "To";
                worksheet.Cell(4, 6).Value = "From";
                worksheet.Cell(4, 7).Value = "To";
                worksheet.Cell(4, 8).Value = "Time of Completion";


                worksheet.Cell(5, 2).Value = "Monday to Friday";

                worksheet.Cell(6, 2).Value = "Saturday";
                worksheet.Cell(7, 2).Value = "Sunday";
                worksheet.Cell(8, 2).Value = "System of rotation of relays";
                worksheet.Cell(9, 9).Value = "Month.......Year";
                worksheet.Range("B3", "I9").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                worksheet.Range("B3", "I9").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                worksheet.Range("B3", "I9").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Range("B3", "I9").Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Range("B3", "I9").Style.Border.LeftBorder = XLBorderStyleValues.Thin;

                worksheet.Range("D3", "G3").Merge();
                worksheet.Range("D3", "G3").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);


                var currentRow = 14;

                worksheet.Range("A14", colName + rowcount).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A14", colName + rowcount).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A14", colName + rowcount).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A14", colName + rowcount).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A14", colName + rowcount).Style.Border.LeftBorder = XLBorderStyleValues.Thin;


                worksheet.Cell(currentRow, 1).InsertTable(dt);
                worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                worksheet.Range("A14:" + colName + "14").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                worksheet.Range("A14:" + colName + "14").Style.Font.Bold = true;
                worksheet.Range("I14:I" + (dt.Rows.Count + 14)).Style.DateFormat.Format = "dd-MMM-yyyy";
                worksheet.Range("J14:J" + (dt.Rows.Count + 14)).Style.DateFormat.Format = "dd-MMM-yyyy";
                worksheet.Range("K14:K" + (dt.Rows.Count + 14)).Style.DateFormat.Format = "dd-MMM-yyyy";
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
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "LabourReportForm12.xlsx");
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
            ViewBag.month = new SelectList(await _dropdownService.GetPayrollReportMonthDropdown(), "Code", "Name");
            ViewBag.Reg = new SelectList(await _dropdownService.GetRegionDropDown(), "Code", "Name");
            ViewBag.St = new SelectList(await _dropdownService.GetStateDropDown(), "Code", "Name");

        }

    }
}
