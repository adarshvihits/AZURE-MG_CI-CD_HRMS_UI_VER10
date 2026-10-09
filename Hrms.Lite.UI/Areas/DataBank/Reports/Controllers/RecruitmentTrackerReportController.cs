
using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.DataBank.Reports;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.DataBank;
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
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.DataBank.Reports.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]
    public class RecruitmentTrackerReportController : Controller
    {
        private readonly IRecruitmentTrackerReportService _service;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;

        public RecruitmentTrackerReportController(IRecruitmentTrackerReportService service, ICommonService commonService, IHelpers helpers)
        {
            _service = service;
            _commonService = commonService;
            _helpers = helpers;
        }
        public async Task<IActionResult> Index()
        {
            try{
            RecruitmentTrackerReport model = new RecruitmentTrackerReport();
            Filter filter = new Filter();
            filter.length = 10;
            model = await _service.GetRecruitmentTrackerReport(filter);
            model.Filter = await _commonService.GetFilterDropdowns();
            return View("~/Areas/DataBank/Reports/Views/RecruitmentTrackerReport/Index.cshtml");
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
        public async Task<IActionResult> ApplyFilter(Filter filter1)
        {
            try
            {
                RecruitmentTrackerReport model = new RecruitmentTrackerReport();
                RecruitmentTrackerReport m = new RecruitmentTrackerReport();

                Filter filter = new Filter();
               model.Filter = filter1;

                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }


                model = await _service.GetRecruitmentTrackerReport(filter1);

                m.TrackingReporttList = model.TrackingReporttList;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.TrackingReporttList == null || model.TrackingReporttList.Count == 0)
                {



                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }

                if (model.TrackingReporttList.Count > 0)
                {
                    totalrows = m.TrackingReporttList[0].active;


                }


                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.TrackingReporttList[0].totalRecords;
                    return Json(new
                    {
                        data = model.TrackingReporttList,
                        draw = filter1.draw,
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }
                else
                {

                    return Json(new
                    {
                        data = model.TrackingReporttList,
                        draw = filter1.draw,
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
        public async Task<IActionResult> Details(Guid ApplicantGI)
        {
            try
            {
                RecruitmentTrackerReport model = new RecruitmentTrackerReport();
                model = await _service.GetDetails(ApplicantGI);
                if (model.ApplicantHeader.ProfilePhoto.fdata != null)
                {
                    ViewBag.FileName = model.ApplicantHeader.ProfilePhoto.FileName;
                    var ext = Path.GetExtension(ViewBag.FileName);
                    string dataUri = $"data:image/{ext.Substring(1)};base64,{Convert.ToBase64String(model.ApplicantHeader.ProfilePhoto.fdata)}";
                    ViewBag.dataUri = dataUri;
                }
                model.ApplicantGI = ApplicantGI;

                return View("~/Areas/DataBank/Reports/Views/RecruitmentTrackerReport/details.cshtml",model);
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

        public async Task<IActionResult> GetApproveTrackerDetails(Guid ApplicantGI,Guid EmployeeGI)
        {
            try
            {
                RecruitmentTrackerReport model = new RecruitmentTrackerReport();
                model = await _service.GetTrackerDetails(ApplicantGI);
                model.ApplicantGI = ApplicantGI;
                return PartialView("~/Areas/DataBank/Reports/Views/RecruitmentTrackerReport/_Tracking.cshtml", model);
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
        public async Task<IActionResult> DownloadDoc(string DocType, int RowID, Guid ApplicantGI)
        {
            var fileData = await _service.DownloadDoc(DocType, RowID, ApplicantGI);
            if (fileData == null || fileData.fdata == null)
            return NotFound();

            var decompressedBytes = await DecompressFileAsync(fileData.fdata);
            return File(decompressedBytes, "application/pdf", fileData.FileName);
        }
        private async Task<byte[]> DecompressFileAsync(byte[] compressedData)
        {
            using (var input = new MemoryStream(compressedData))
            using (var gzip = new GZipStream(input, CompressionMode.Decompress))
            using (var output = new MemoryStream())
            {
                await gzip.CopyToAsync(output);
                return output.ToArray();
            }
        }

        public async Task<IActionResult> GetDocumentPopup(Guid ApplicantGI, string DocType, int RowID)
        {
            RecruitmentTrackerReport model = new RecruitmentTrackerReport();
            model = await _service.GetDocumentPopup(DocType, RowID, ApplicantGI);
            return PartialView("~/Areas/DataBank/Reports/Views/RecruitmentTrackerReport/_DocumentPopup.cshtml", model);
        }
        public async Task<IActionResult> ShowDoc(Guid ApplicantGI, string DocType, int RowID)
        {
            var fileData = await _service.DownloadDoc(DocType, RowID, ApplicantGI);
            if (fileData == null || fileData.fdata == null)
                return Content("<script>alert('No document uploaded');</script>", "text/html");
            var decompressedBytes = await DecompressFileAsync(fileData.fdata);
            //return File(decompressedBytes, "application/pdf", fileData.FileName);
            Response.Headers["Content-Disposition"] = $"inline; filename=\"{fileData.FileName}\"";
            return File(decompressedBytes, "application/pdf");
        }

        public async Task<IActionResult> ExportToExcel(string FromDate, string ToDate, Filter Filter, int TotalRows)
        {
            RecruitmentTrackerReport model = new RecruitmentTrackerReport();
            var LoginUser = HttpContext.Session.GetString("LoginUser");
            var CompanyName = HttpContext.Session.GetString("CompanyName");
            var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
            var GroupCompanyName = User.authUser.GroupCompanyName;
            ExcelPassword EP = new ExcelPassword();
            EP = await _commonService.GetExcelPassword("Recruitment Tracker Report", "Excel");


            Filter.length = TotalRows;
            Filter.start = 0;

            string FilterValues = await _helpers.GetFilterValues(Filter);
            if (FilterValues == "")
            {
                FilterValues = "All";
            }
            model.Filter = Filter;



            string[] cu_Date = System.DateTime.Now.GetDateTimeFormats();
            string currDateTime = cu_Date[44];

            //Get Dataset
            var dataset = await _service.RecruitmentTrackerReport_Excel(FromDate, ToDate, Filter);
            DataTable dt = dataset.Tables[0];
            //set EXCEL Column Heading 
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (dt.Columns[i].ColumnName == "pf" || dt.Columns[i].ColumnName == "esi" || dt.Columns[i].ColumnName == "uan" || dt.Columns[i].ColumnName == "lic" ||
                    dt.Columns[i].ColumnName == "lwd" || dt.Columns[i].ColumnName == "doj" || dt.Columns[i].ColumnName == "dob")
                    dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                else
                    dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
            }

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Recruitment Tracker Report");
                int count = dt.Rows.Count + 6;

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
                wsReportNameHeaderRange.Value = "Recruitment Tracker Report List";

                worksheet.Cell(2, 1).Style.Font.Bold = true;
                worksheet.Cell(2, 1).Value = "Group Company Name";
                worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                worksheet.Cell(2, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(2, 3).Style.Font.Bold = true;
                worksheet.Cell(2, 3).Value = "Login User";
                worksheet.Cell(2, 4).Value = LoginUser;
                
                worksheet.Cell(3, 1).Style.Font.Bold = true;
                worksheet.Cell(3, 1).Value = "Date & Time";
                worksheet.Cell(3, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(3, 2).Style.DateFormat.Format = "dd-MMM-yyyy";
                worksheet.Cell(3, 2).Value = currDateTime.ToString();
                worksheet.Cell(3, 3).Style.Font.Bold = true;
                worksheet.Cell(3, 3).Value = "Total Count";
                worksheet.Cell(3, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(3, 4).Value = dt.Rows.Count;
                worksheet.Cell(4, 1).Style.Font.Bold = true;
                worksheet.Cell(4, 1).Value = "Filter Applied";
                var filterapplied = worksheet.Range("B4", colName + 4).Merge();

                filterapplied.Value = FilterValues;


                var currentRow = 5;


                worksheet.Cell(currentRow, 1).InsertTable(dt);
                worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                worksheet.Range("A5", colName + 5).Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                worksheet.Range("A5", colName + 5).Style.Font.Bold = true;


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
                        book.Protect(EP.Password);
                        book.SaveToStream(stream);
                    }
                    var content = stream.ToArray();
                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Recruitment Tracker Report List.xlsx"
                        );
                }
            }
        }
    }




}


    
