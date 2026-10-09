using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank.Reports;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class CertificationTrackingReportController : Controller
    {
        private readonly ICertificationTrackingReportService _service;
        private readonly IHelpers _helpers;
        private readonly IDropdownService _dropdownService;
        private readonly ICommonService _commonService;

        public CertificationTrackingReportController(ICertificationTrackingReportService CertificationTrackingReportService, IDropdownService dropdownService, IHelpers Helpers, ICommonService commonService)
        {
            _service = CertificationTrackingReportService;
            _dropdownService = dropdownService;
            _helpers = Helpers;
            _commonService = commonService;
        }
        public async Task<IActionResult> Index()
        {
            try { 
            CertificationTrackingReport model = new CertificationTrackingReport();
            CertificationTrackingReport m = new CertificationTrackingReport();
            model.From = DateTime.Now.AddMonths(-3);
            model.To = DateTime.Now;
            Filter filter = new Filter();
            filter.length = 10;
            m = await _service.GetList(model.From.ToString("yyyy-MMM-dd"), model.To.ToString("yyyy-MMM-dd"), filter);
            model.Filter= await _commonService.GetFilterDropdowns();
            return View("~/Areas/DataBank/Reports/Views/CertificationTrackingReport/Index.cshtml", model);
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
        public async Task<IActionResult> GetList(DateTime From, DateTime To, Filter filter1)
        {
            try
            {
                CertificationTrackingReport model = new CertificationTrackingReport();
                CertificationTrackingReport m = new CertificationTrackingReport();

                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }
                model = await _service.GetList(From.ToString("yyyy-MMM-dd"), To.ToString("yyyy-MMM-dd"), filter1);

                m.CertificationTrackingReportList = model.CertificationTrackingReportList;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.CertificationTrackingReportList == null || model.CertificationTrackingReportList.Count() == 0)
                {
                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }

                if (model.CertificationTrackingReportList.Count > 0)
                {
                    totalrows = m.CertificationTrackingReportList[0].active;
                }

                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.CertificationTrackingReportList[0].totalRecords;
                    return Json(new
                    {
                        data = model.CertificationTrackingReportList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    return Json(new
                    {
                        data = model.CertificationTrackingReportList,
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
        public async Task<IActionResult> GetTrackingDetails(int HDcode, Guid EmployeeGI)
        {
            CertificationTrackingReport model = new CertificationTrackingReport();
            model = await _service.GetCertificationTrackingDetails(HDcode);
            model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);
            return PartialView("~/Areas/DataBank/Reports/Views/CertificationTrackingReport/_CertificationDetailsPopup.cshtml", model);
        }
        public async Task<IActionResult> ExportToExcel(Filter filter, DateTime fromDate, DateTime toDate, int TotalRows)
        {
            try
            {
                filter.length = TotalRows;
                filter.start = 0;
                string FilterValues = await _helpers.GetFilterValues(filter);
                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Certification Tracking", "Excel");

                if (FilterValues == "")
                {
                    FilterValues = "All";
                }

                string currDateTime = await _helpers.GetCurrentDate();
                

                //Get Datset
                var dataset = await _service.GetList_Excel(fromDate.ToString("yyyy-MMM-dd"), toDate.ToString("yyyy-MMM-dd"), filter);
                DataTable dt = dataset.Tables[0];

                if ((dataset.Tables[0]?.Rows?.Count ?? 0) > 0)
                {
                    dt = dataset.Tables[0];
                }

                //set EXCEL Column Heading 
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName == "doj")

                        dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                    else
                        dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                }

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Certification Tracking");
                    int count = dt.Rows.Count + 4;

                    worksheet.Range("A1:O" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:O" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:O" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:O" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:O" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 14)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                    wsReportNameHeaderRange.Value = "Certification Tracking";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                    worksheet.Cell(2, 3).Style.Font.Bold = true;
                    worksheet.Cell(2, 3).Value = "Login User";
                    worksheet.Cell(2, 4).Value = LoginUser;
                    worksheet.Cell(2, 5).Style.Font.Bold = true;
                    worksheet.Cell(2, 5).Value = "Date & Time";
                    worksheet.Cell(2, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(2, 6).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
                    worksheet.Cell(2, 6).Value = currDateTime.ToString();
                    worksheet.Cell(2, 7).Style.Font.Bold = true;
                    worksheet.Cell(2, 7).Value = "Total Count";
                    worksheet.Cell(2, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(2, 8).Value = dt.Rows.Count;
                    worksheet.Cell(2, 9).Style.Font.Bold = true;
                    worksheet.Cell(2, 9).Value = "From";
                    worksheet.Cell(2, 10).Value = fromDate;
                    worksheet.Cell(2, 11).Style.Font.Bold = true;
                    worksheet.Cell(2, 11).Value = "To";
                    worksheet.Cell(2, 12).Value = toDate;
                    worksheet.Cell(3, 1).Style.Font.Bold = true;
                    worksheet.Cell(3, 1).Value = "Filter Applied";
                    worksheet.Range("B3:O3").Merge();
                    worksheet.Cell("B3").Value = FilterValues;

                    var currentRow = 4;

                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A4:O4").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A4:O4").Style.Font.Bold = true;
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
                            "Certification Tracking.xlsx"
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
