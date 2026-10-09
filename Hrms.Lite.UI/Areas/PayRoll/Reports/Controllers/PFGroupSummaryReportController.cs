using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.PayRoll.Reports;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll.Reports;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
    public class PFGroupSummaryReportController : Controller
    {
        private readonly IPFGroupSummaryReportService _PFGroupSummaryReportService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        private readonly IDropdownService _dropdownService;
       
        public PFGroupSummaryReportController(IPFGroupSummaryReportService PFGroupSummaryReportService, ICommonService commonService, IHelpers Helpers, IDropdownService dropdownService)
        {
            _PFGroupSummaryReportService = PFGroupSummaryReportService;
            _commonService = commonService;
            _helpers = Helpers;
            _dropdownService = dropdownService;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                PFGroupSummaryReport model = new PFGroupSummaryReport();
           
            ViewBag.Month = new SelectList(await _dropdownService.GetMonthDropdown(), "Code", "Name");
            ViewBag.PFgrp = new SelectList(await _dropdownService.GetPFSummaryGroupbyDropdown(), "Code", "Name");
            model.SliderFilter = await _commonService.GetFilterDropdowns();
                Filter filter = new Filter();
                filter.length = 10;
                model = await _PFGroupSummaryReportService.GetList(0, 0, filter);

                return View("~/Areas/PayRoll/Reports/Views/PFGroupSummaryReport/Index.cshtml", model);
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
        public async Task<IActionResult> GetPFGroupSummaryReportList(Filter filter1, int Month, int PFGroup)
        {
            try
            {
                PFGroupSummaryReport model = new PFGroupSummaryReport();
                PFGroupSummaryReport m = new PFGroupSummaryReport();
 




                Filter filter = new Filter();
               

                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }
                if (Month != 0 && PFGroup != 0)
                    model = await _PFGroupSummaryReportService.GetList(Month, PFGroup, filter1);
               
                m.PFGroupSummary = model.PFGroupSummary;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.PFGroupSummary == null || model.PFGroupSummary.Count() == 0)
                {


                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }




                if (model.PFGroupSummary.Count > 0)
                {
                    totalrows = m.PFGroupSummary[0].active;


                }

                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.PFGroupSummary[0].totalRecords;
                    return Json(new
                    {
                        data = model.PFGroupSummary,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    return Json(new
                    {
                        data = model.PFGroupSummary,
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

        public async Task<ActionResult> GetList(int Month, int PFGroup,Filter filter)
        {
            try
            {

                PFGroupSummaryReport model = new PFGroupSummaryReport();
               
                model = await _PFGroupSummaryReportService.GetList(Month, PFGroup, filter);
                model.PFGroupSummary = new List<PFGroupSummary>();
                model.PFGroupSummary.Add(new PFGroupSummary());
                return PartialView("~/Areas/PayRoll/Reports/Views/PFGroupSummaryReport/_PFGroupSummaryReportList.cshtml", model);

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

        public async Task<IActionResult> ExportToExcel(int Month, int PFGroup, string MonthName, string PFGroupName, int totRecords,Filter filter)
        {
            try
            {
                filter.length = totRecords;
                filter.start = 0;
                string FilterValues = await _helpers.GetFilterValues(filter);

                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("PF Group Summary Report", "Excel");

                if (FilterValues == "")
                {
                    FilterValues = "All";
                }
                string currDateTime = await _helpers.GetCurrentDate();
                

                int index = 0;
                //Get Dataset
                var dataset = await _PFGroupSummaryReportService.GetPFGroupSummaryReport_Excel(Month, PFGroup,filter);
                DataTable dt = dataset.Tables[0];

                //set EXCEL Column Heading 
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName.Contains("name"))
                    {
                        
                        dt.Columns[i].ColumnName = PFGroupName;
                    }
                    if (dt.Columns[i].ColumnName.Contains("epf"))
                    {
                        index = dt.Columns[i].ColumnName.IndexOf("epf");
                        dt.Columns[i].ColumnName = ConvertSubstringToUpperCase(dt.Columns[i].ColumnName, index, 3);
                    }
                    if (dt.Columns[i].ColumnName.Contains("edli Charges"))
                    {
                        index = dt.Columns[i].ColumnName.IndexOf("edli");
                        dt.Columns[i].ColumnName = ConvertSubstringToUpperCase(dt.Columns[i].ColumnName, index, 4);
                    }
                    if (dt.Columns[i].ColumnName.Contains("pf"))
                    {
                        index = dt.Columns[i].ColumnName.IndexOf("pf");
                        dt.Columns[i].ColumnName = ConvertSubstringToUpperCase(dt.Columns[i].ColumnName, index, 2);
                    }
                    

                    else
                        dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                }




                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("PF Group Summary Report");
                    int count = dt.Rows.Count + 5;


                    worksheet.Range("A1:J" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:J" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:J" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:J" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:J" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 9)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                    wsReportNameHeaderRange.Value = "PF Group Summary Report";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Company Name";
                    worksheet.Cell(2, 2).Value = CompanyName.ToUpper();
                    worksheet.Cell(2, 3).Style.Font.Bold = true;
                    worksheet.Cell(2, 3).Value = "Month";
                    worksheet.Cell(2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(2, 4).Style.DateFormat.Format = "MMM-yyyy";
                    worksheet.Cell(2, 4).Value = MonthName;
                    worksheet.Cell(2, 5).Value = "Group By";
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
                    worksheet.Range("B4:I4").Merge();
                    worksheet.Cell("B4").Value = FilterValues;



                    var currentRow = 5;

                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A5:J5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A5:J5").Style.Font.Bold = true;


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
                            "PF Group Summary Report List.xlsx"
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
