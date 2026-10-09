using ClosedXML.Excel;

using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.DataBank;
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
using Microsoft.AspNetCore.StaticFiles;


namespace Hrms.Lite.UI.Areas.DataBank.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]
    public class EmployeeVsBankDetailsApprovalController : Controller
    {
        private readonly IEmployeeVsBankDetailsApprovalService _EmployeeVsBankDetailsApprovalService;
        private readonly IHelpers _helpers;
        private readonly IDropdownService _dropdownService;
        private readonly ICommonService _commonService;

        public EmployeeVsBankDetailsApprovalController(IEmployeeVsBankDetailsApprovalService EmployeeVsBankDetailsApprovalService, IHelpers Helpers, IDropdownService dropdownService, ICommonService commonService)
        {
            _EmployeeVsBankDetailsApprovalService = EmployeeVsBankDetailsApprovalService;
            _helpers = Helpers;
            _dropdownService = dropdownService;
            _commonService = commonService;
        }
        public async Task<IActionResult> Index()
        {
            EmployeeVsBankDetailsApproval model = new EmployeeVsBankDetailsApproval();
            model.filter = await _commonService.GetFilterDropdowns();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> GetList(Filter filter1)
        {
            try
            {
                EmployeeVsBankDetailsApproval model = new EmployeeVsBankDetailsApproval();
                EmployeeVsBankDetailsApproval m = new EmployeeVsBankDetailsApproval();
                Filter filter = new Filter();
                model.filter = filter1;
                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }
                model = await _EmployeeVsBankDetailsApprovalService.GetList(filter1);
                m.EmployeeVsBankDetailsList = model.EmployeeVsBankDetailsList;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;
                if (model.EmployeeVsBankDetailsList.Count > 0)
                {
                    totalrows = m.EmployeeVsBankDetailsList[0].active;
                }
                if (filter1.searchValue != "")
                {
                    if (model.EmployeeVsBankDetailsList.Count > 0)
                    {
                        totalrowsafterfiltering = m.EmployeeVsBankDetailsList[0].totalRecords;
                    }
                    else
                    {
                        totalrowsafterfiltering = 0;
                    }
                    return Json(new
                    {
                        data = model.EmployeeVsBankDetailsList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows,//totalrowsafterfiltering
                    });
                }
                else
                {
                    return Json(new
                    {
                        data = model.EmployeeVsBankDetailsList,
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows,
                    });
                }
            }
            catch (Exception ex)
            {
                return View();
            }
        }
        public async Task<IActionResult> ViewDetails(Guid EmployeeGI)
        {
            try
            {
                EmployeeVsBankDetailsApproval model = await _EmployeeVsBankDetailsApprovalService.GetEmpBankDetails(EmployeeGI);
                model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);
                return PartialView("_EmpBankDetails", model);
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
        public async Task<IActionResult> ApproveOrReject(Guid EmployeeGI, string mode)
        {
            try
            {
                var response = await _EmployeeVsBankDetailsApprovalService.ApproveOrReject(EmployeeGI, mode);
                return Json(response);
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

        public async Task<IActionResult> ShowDoc(Guid RequestGI, string DocType)
        {
            var fileData = await _EmployeeVsBankDetailsApprovalService.DownloadDoc(RequestGI, DocType);
            if (fileData == null || fileData.fdata == null)
                return NotFound();
            var contentType = GetContentType(fileData.FileName);
            var decompressedBytes = await DecompressFileAsync(fileData.fdata);
            //return File(decompressedBytes, "application/pdf", fileData.FileName);
            Response.Headers["Content-Disposition"] = $"inline; filename=\"{fileData.FileName}\"";
            //return File(decompressedBytes, "application/pdf");
            return File(decompressedBytes, contentType);
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
        private string GetContentType(string fileName)
        {
            var provider = new FileExtensionContentTypeProvider();

            if (!provider.TryGetContentType(fileName, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            return contentType;
        }
        public async Task<IActionResult> ExportToExcel(int TotalRow, Filter filter)
        {
            try
            {
                filter.length = TotalRow;
                filter.start = 0;

                string FilterValues = await _helpers.GetFilterValues(filter);

                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Employee Vs Bank Details Approval", "Excel");

                if (FilterValues == "")
                {
                    FilterValues = "All";
                }
                string currDateTime = await _helpers.GetCurrentDate();
                
                //Get Dataset
                var dataset = await _EmployeeVsBankDetailsApprovalService.GetList_Excel(filter);
                DataTable dt = dataset.Tables[0];

                if ((dataset.Tables[0]?.Rows?.Count ?? 0) > 0)
                {
                    dt = dataset.Tables[0];
                }
                //set EXCEL Column Heading 
                //for (int i = 0; i < dt.Columns.Count; i++)
                //{
                //    dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                //}

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName == "ifsc")
                        dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                    else
                        dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                }

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Employee Vs Bank Approval");
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
                    wsReportNameHeaderRange.Value = "Employee Vs Bank Details Approval";

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
                    worksheet.Cell(4, 1).Style.Font.Bold = true;
                    worksheet.Cell(4, 1).Value = "Filter Applied";
                    worksheet.Range("B4:J4").Merge();
                    worksheet.Cell("B4").Value = FilterValues;



                    var currentRow = 5;



                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A5:K5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A5:K5").Style.Font.Bold = true;
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
                            "Employee Vs Bank Details Approval List.xlsx"
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
