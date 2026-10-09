using ClosedXML.Excel;
using Hrms.Lite.Services.IServices.DataBank.Reports;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Spire.Xls;
using Newtonsoft.Json;
using Hrms.Lite.Shared.Account;

namespace Hrms.Lite.UI.Areas.DataBank.Reports.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]
    public class AttritionReportController : Controller
    {
       
        private readonly IAttritionReportService _attritionReportService;
        private readonly IDropdownService _dropdownService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        public AttritionReportController(IAttritionReportService attritionReportService, IDropdownService dropdownService, ICommonService commonService, IHelpers helpers)
        {
            _attritionReportService = attritionReportService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
            AttritionReport model = new AttritionReport();
                 model.From = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                model.To = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);
                model.GroupBy = 2;
                model.Calculation = 1;
                ViewBag.AttritionGrouping = new SelectList(await _dropdownService.GetAttritionGroupingDropdown(), "Code", "Name");
                ViewBag.AttritionCalculation = new SelectList(await _dropdownService.GetAttritionCalculationDropdown(), "Code", "Name");
                model = await _attritionReportService.GetAttritionReport(model);
                model.From = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                model.To = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);
                return View("~/Areas/DataBank/Reports/Views/AttritionReport/Index.cshtml",model);
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
        public async Task<IActionResult> GetAttritionReport(DateTime From,DateTime To,int groupbyCode,int calculationCode)
        {
            try
            {
                AttritionReport model = new AttritionReport();
            //Filter filter = new Filter();
            //model.filter = filter;
            model.From = From;
            model.To = To;
            model.GroupBy = groupbyCode;
            model.Calculation = calculationCode;
            model = await _attritionReportService.GetAttritionReport(model);
            return View("~/Areas/DataBank/Reports/Views/AttritionReport/_List.cshtml", model);
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

        public async Task<IActionResult> ExportToexcel(DateTime From, DateTime To, string groupbyName, string calculatioName,int GroupBy,int Calculation)
        {
            try
            {
                AttritionReport model = new AttritionReport();
            //TempData["Global_Filter_DailyAttendanceEntry"] = JsonConvert.SerializeObject(filter);
            model.From = From;
            model.To = To;
            model.GroupBy = GroupBy;
            model.Calculation = Calculation;
            model = await _attritionReportService.GetAttritionReport(model);
            var headercount = model.Head.Count + 2;
            var LoginUser = HttpContext.Session.GetString("LoginUser");
            var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
            EP = await _commonService.GetExcelPassword("Attrition Report List", "Excel");
            //int slNo = 0;
            string currDateTime = await _helpers.GetCurrentDate();
            


            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Attrition Report List");

                int rowcount = model.Rows.Count + 6;


                worksheet.Range("A1:F" + rowcount).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1:F" + rowcount).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1:F" + rowcount).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1:F" + rowcount).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1:F" + rowcount).Style.Border.LeftBorder = XLBorderStyleValues.Thin;


                var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, "F"));
                wsReportNameHeaderRange.Style.Font.Bold = true;
                wsReportNameHeaderRange.Style.Font.FontSize = 15;
                wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                wsReportNameHeaderRange.Merge();
                wsReportNameHeaderRange.Value = "Attrition Report List";


                worksheet.Cell(2, 1).Style.Font.Bold = true;
                worksheet.Cell(2, 1).Value = "Group Company Name";
                worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();


                worksheet.Cell(2, 3).Style.Font.Bold = true;
                worksheet.Cell(2, 3).Value = "From Date";
                worksheet.Cell(2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(2, 4).Style.DateFormat.Format = "dd-MMM-yyyy";
                worksheet.Cell(2, 4).Value = From;
                worksheet.Cell(2, 5).Style.Font.Bold = true;
                worksheet.Cell(2, 5).Value = "To Date";
                worksheet.Cell(2, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(2, 6).Style.DateFormat.Format = "dd-MMM-yyyy";
                worksheet.Cell(2, 6).Value = To;
                worksheet.Cell(3, 5).Style.Font.Bold = true;
                worksheet.Cell(3, 5).Value ="GroupBy Name";
                worksheet.Cell(3, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(3, 6).Value = groupbyName;

                worksheet.Cell(3, 1).Style.Font.Bold = true;
                worksheet.Cell(3, 1).Value = "Login User";
                worksheet.Cell(3, 2).Value = LoginUser;
                worksheet.Cell(3, 3).Style.Font.Bold = true;
                worksheet.Cell(3, 3).Value = "Date & Time";
                worksheet.Cell(3, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(3, 4).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
                worksheet.Cell(3, 4).Value = currDateTime.ToString();
                worksheet.Cell(4, 3).Style.Font.Bold = true;
                worksheet.Cell(4, 3).Value = "Total Count";
                worksheet.Cell(4, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(4, 4).Value = model.Rows.Count;
                worksheet.Cell(4, 1).Style.Font.Bold = true;
                worksheet.Cell(4, 1).Value = "Formula";
                worksheet.Cell(4, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(4, 2).Value = calculatioName;
                #region Header
                var currentRow = 5;
                var a = 1;
                worksheet.Range("A5:F" + rowcount).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A5:F" + rowcount).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A5:F" + rowcount).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A5:F" + rowcount).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A5:F" + rowcount).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                var wsReportTqablenameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 5)));
                wsReportTqablenameHeaderRange.Style.Font.Bold = true;
                wsReportTqablenameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                for (int i = 0; i < model.Head.Count; i++)
                {
                    worksheet.Cell(currentRow, a).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, a).Value = model.Head[i];
                    a++;
                }
                #endregion

                #region Body
                for (int i = 0; i < model.Rows.Count; i++)
                {

                    currentRow++;
                    //slNo = slNo + 1;
                    //worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    //worksheet.Cell(currentRow, 1).Value = slNo;
                    worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 1).Value = model.Rows[i].Columns[0];
                    worksheet.Cell(currentRow, 2).Value = model.Rows[i].Columns[1];
                    worksheet.Cell(currentRow, 3).Value = model.Rows[i].Columns[2];
                    worksheet.Cell(currentRow, 4).Value = model.Rows[i].Columns[3];
                    worksheet.Cell(currentRow, 5).Value = model.Rows[i].Columns[4];
                    worksheet.Cell(currentRow, 6).Value = model.Rows[i].Columns[5];

                    }
                currentRow++;
                var c = 2;
                for (int i = 0; i < 5; i++)
                {
                    worksheet.Cell(currentRow, c).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, c).Value = model.FooterRows[0].Columns[c-2];
                    c++;
                    worksheet.Columns().Style.Alignment.WrapText = true;
                    worksheet.Rows().Style.Alignment.WrapText = true;
                }
                #endregion
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
                        "Attrition Report List.xlsx"
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
