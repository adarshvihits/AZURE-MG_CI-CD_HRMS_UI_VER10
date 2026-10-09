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
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;


namespace Hrms.Lite.UI.Areas.DataBank.Reports.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]
    public class IDCardDetailsController : Controller
    {
        private readonly IIDCardDetailsService _IDCardDetailsService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        private readonly IDropdownService _dropdownService;

        public IDCardDetailsController(IIDCardDetailsService IDCardDetailsService, ICommonService commonService, IHelpers Helpers, IDropdownService dropdownService)
        {
            _IDCardDetailsService = IDCardDetailsService;
            _commonService = commonService;
            _helpers = Helpers;
            _dropdownService = dropdownService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                IDCardDetails model = new IDCardDetails();
                //model.FromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                //model.ToDate = DateTime.Now;
                DateTime fDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                DateTime tDate = DateTime.Now;
               
                var StorageType = await _commonService.GetFileStorageType();
                model.filter = await _commonService.GetFilterDropdowns();
                model = await _IDCardDetailsService.GetIDCardDetails(fDate.ToString("dd-MMM-yyyy"), tDate.ToString("dd-MMM-yyyy"), model.filter);
                model.filter = await _commonService.GetFilterDropdowns();
                model.FromDate = fDate;
                model.ToDate = tDate;
             
                ViewBag.Storage = StorageType.Data;
                return View("~/Areas/DataBank/Reports/Views/IDCardDetails/Index.cshtml", model);
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

        public async Task<IActionResult> GetList(string FromDate, string ToDate, Filter filter)
        {

            IDCardDetails model = new IDCardDetails();
            model.filter = new Filter();
           
            model = await _IDCardDetailsService.GetIDCardDetails(FromDate, ToDate, filter);
               return PartialView("~/Areas/DataBank/Reports/Views/IDCardDetails/_IDCardDetailsList.cshtml", model);
        }
        public async Task<IActionResult> ApplyFilter(string FromDate, string ToDate, Filter filter)
        {
            try
            {
                IDCardDetails model = new IDCardDetails();
               model.filter = filter;

                model = await _IDCardDetailsService.GetIDCardDetails(FromDate, ToDate, filter);
                return PartialView("~/Areas/DataBank/Reports/Views/IDCardDetails/_IDCardDetailsList.cshtml", model);
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


        public async Task<IActionResult> ExportToExcel(string FromDate, string ToDate, Filter filter, int totRecords)
        {
            var LoginUser = HttpContext.Session.GetString("LoginUser");
            var CompanyName = HttpContext.Session.GetString("CompanyName");
            var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
            var GroupCompanyName = User.authUser.GroupCompanyName;
            ExcelPassword EP = new ExcelPassword();
            EP = await _commonService.GetExcelPassword("ID Card Details", "Excel");

            string currDateTime = await _helpers.GetCurrentDate();
            

            filter.length = totRecords;
            var dataset = await _IDCardDetailsService.GetIDCardDetails_Excel(FromDate, ToDate, filter);

            DataTable dt = dataset.Tables[0];

            //set EXCEL Column Heading 
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                
                if (dt.Columns[i].ColumnName == "employee Id")

                    dt.Columns[i].ColumnName = "Employee ID";

                else

                    dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
            }


            using (XLWorkbook wb = new XLWorkbook())
            {


                var worksheet = wb.Worksheets.Add("ID Card Details");
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
                wsReportNameHeaderRange.Value = "ID Card Details";

                worksheet.Cell(2, 1).Style.Font.Bold = true;
                worksheet.Cell(2, 1).Value = "Group Company Name";
                worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();

                worksheet.Cell(2, 3).Style.Font.Bold = true;
                worksheet.Cell(2, 3).Value = "From";
                worksheet.Cell(2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(2, 4).Value = FromDate;
                worksheet.Cell(2, 4).Style.DateFormat.Format = "dd-MMM-yyyy";
                worksheet.Cell(2, 5).Style.Font.Bold = true;
                worksheet.Cell(2, 5).Value = "To";
                worksheet.Cell(2, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(2, 6).Value = ToDate;
                worksheet.Cell(2, 6).Style.DateFormat.Format = "dd-MMM-yyyy";
                //worksheet.Cell(2, 7).Style.Font.Bold = true;
                //worksheet.Cell(2, 7).Value = "Total Count";
                //worksheet.Cell(2, 8).Value = dt.Rows.Count;
                //worksheet.Cell(2, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);


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
                worksheet.Cell(3, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);


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
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ID Card Details.xlsx");
                }
            }
        }



        [HttpGet]
        public async Task<IActionResult> DownloadDoc(string EmployeeId)
        {
            try
            {
                var fileData = await _IDCardDetailsService.DownloadDoc(EmployeeId);

                if (fileData == null || fileData.fdata == null || fileData.fdata.Length == 0)
                {
                    return Content("NO DATA FOUND");
                }

                byte[] fileBytes = GetActualFileBytes(fileData.fdata);

                // Detect file type
                string mimeType = GetMimeType(fileBytes);
                string extension = GetExtension(mimeType);

                Response.Headers["Content-Disposition"] =
                    $"inline; filename=\"{Path.GetFileNameWithoutExtension(fileData.FileName)}{extension}\"";

                return File(fileBytes, mimeType);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error while downloading file.");
            }
        }

        private byte[] GetActualFileBytes(byte[] data)
        {
            if (IsGZip(data))
                return DecompressGZip(data);

            if (IsDeflate(data))
                return DecompressDeflate(data);

            return data;
        }

        private bool IsGZip(byte[] data)
        {
            return data.Length > 2 &&
                   data[0] == 0x1F &&
                   data[1] == 0x8B;
        }

        private bool IsDeflate(byte[] data)
        {
            return data.Length > 2 &&
                   data[0] == 0x78;
        }

        private byte[] DecompressGZip(byte[] compressedData)
        {
            using (var input = new MemoryStream(compressedData))
            using (var gzip = new GZipStream(input, CompressionMode.Decompress))
            using (var output = new MemoryStream())
            {
                gzip.CopyTo(output);
                return output.ToArray();
            }
        }

        private byte[] DecompressDeflate(byte[] compressedData)
        {
            using (var input = new MemoryStream(compressedData))
            using (var deflate = new DeflateStream(input, CompressionMode.Decompress))
            using (var output = new MemoryStream())
            {
                deflate.CopyTo(output);
                return output.ToArray();
            }
        }

        private string GetMimeType(byte[] data)
        {
            // JPEG
            if (data.Length > 3 && data[0] == 0xFF && data[1] == 0xD8 && data[2] == 0xFF)
                return "image/jpeg";

            // PNG
            if (data.Length > 4 && data[0] == 0x89 && data[1] == 0x50 && data[2] == 0x4E && data[3] == 0x47)
                return "image/png";

            // PDF
            if (data.Length > 4 && data[0] == 0x25 && data[1] == 0x50 && data[2] == 0x44 && data[3] == 0x46)
                return "application/pdf";

            // Default
            return "application/octet-stream";
        }

        private string GetExtension(string mimeType)
        {
            switch (mimeType)
            {
                case "image/jpeg":
                    return ".jpg";

                case "image/png":
                    return ".png";

                case "application/pdf":
                    return ".pdf";

                default:
                    return "";
            }
        }
}
}




