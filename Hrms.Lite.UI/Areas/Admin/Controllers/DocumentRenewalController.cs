using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.Admin;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.Admin;
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

namespace Hrms.Lite.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [SessionAuthorize]
    public class DocumentRenewalController : Controller
    {
        // GET: DocumentRenewalController
        private readonly IDropdownService _dropdownService;
        private readonly ICommonService _commonService;
        private readonly IDocumentRenewalService _DocumentRenewalService;
        private readonly IHelpers _helpers;

        public DocumentRenewalController(IDropdownService dropdownService, ICommonService commonService, IDocumentRenewalService DocumentRenewalService, IHelpers helpers)
        {
            _dropdownService = dropdownService;
            _commonService = commonService;
            _DocumentRenewalService = DocumentRenewalService;
            _helpers = helpers;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                DocumentRenewal model = new DocumentRenewal();
                Filter filter = new Filter();
                model = await _DocumentRenewalService.GetDocumentRenewalList();
                model.filter = await _commonService.GetFilterDropdowns();
                return View(model);
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
        public async Task<ActionResult> GetDocumentRenewalList()
        {
            try
            {
                return PartialView("_List", await _DocumentRenewalService.GetDocumentRenewalList());
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
        public async Task<IActionResult> Create()
        {
            try
            {
                DocumentRenewal model = new DocumentRenewal();
                model.Mode = "CREATE";
                ViewBag.RenewalsCategory = new SelectList(await _dropdownService.GetRenewalsCategoryDropdown(), "Code", "Name");
                return PartialView("~/Areas/Admin/Views/DocumentRenewal/_Create.cshtml", model);
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
        public async Task<IActionResult> GetDocumentRenewalDetails(Guid DocumentRenewalGI)
        {
            try
            {
                DocumentRenewal model = new DocumentRenewal();

                model = await _DocumentRenewalService.GetDocumentRenewalDetails(DocumentRenewalGI);
                model.Mode = "MODIFY";
                ViewBag.RenewalsCategory = new SelectList(await _dropdownService.GetRenewalsCategoryDropdown(), "Code", "Name");
                return PartialView("_Create", model);
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
        public async Task<ActionResult> Save(DocumentRenewal Input)
        {
            try
            {
                var response = await _DocumentRenewalService.Create(Input);

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
        public async Task<IActionResult> Delete(Guid DocumentRenewalGI)
        {
            try
            {
                DocumentRenewal model = new DocumentRenewal();

                model = await _DocumentRenewalService.GetDocumentRenewalDetails(DocumentRenewalGI);
                model.Mode = "Delete";
                return PartialView("_Delete", model);
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
        public async Task<ActionResult> Delete(DocumentRenewal model)
        {
            try
            {
                var response = await _DocumentRenewalService.Delete(model);
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
        public async Task<IActionResult> ExportToExcel()
        {
            try
            {
                DocumentRenewal model = new DocumentRenewal();
                model = await _DocumentRenewalService.GetDocumentRenewalList();
                int slno = 0;
                string currDateTime = await _helpers.GetCurrentDate();
                

                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                ExcelPassword EP = new ExcelPassword();


                EP = await _commonService.GetExcelPassword("DocumentRenewal", "Excel");
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Document Renewal List");
                    int count = model.DocumentRenewalList.Count + 6;


                    worksheet.Range("A1:E" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:E" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:E" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:E" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:E" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;

                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 3)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Value = "Document Renewal List";
                    worksheet.Range("A6:E6").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Company Name";
                    worksheet.Cell(2, 2).Value = CompanyName;
                    worksheet.Cell(3, 1).Style.Font.Bold = true;
                    worksheet.Cell(3, 1).Value = "Login User";
                    worksheet.Cell(3, 2).Value = LoginUser;
                    worksheet.Cell(4, 1).Style.Font.Bold = true;
                    worksheet.Cell(4, 1).Value = "Date & Time";
                    worksheet.Cell(4, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(4, 2).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
                    worksheet.Cell(4, 2).Value = currDateTime.ToString();
                    worksheet.Cell(5, 1).Style.Font.Bold = true;
                    worksheet.Cell(5, 1).Value = "Total Count";
                    worksheet.Cell(5, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(5, 2).Value = model.DocumentRenewalList.Count;







                    var currentRow = 6;

                    #region Header
                    worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 1).Value = "Sl.No";
                    worksheet.Cell(currentRow, 2).Value = "Category";
                    worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 3).Value = "Name";
                    worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 4).Value = "Renewal Date";
                    worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 5).Value = "Remarks";
                    worksheet.Cell(currentRow, 5).Style.Font.Bold = true;





                    #endregion

                    #region Body
                    foreach (var item in model.DocumentRenewalList)
                    {
                        currentRow++;
                        slno = slno + 1;
                        worksheet.Cell(currentRow, 1).Value = slno;
                        worksheet.Cell(currentRow, 2).Value = item.RenewalsCategory.Name;
                        worksheet.Cell(currentRow, 3).Value = item.DocumentName;
                        worksheet.Cell(currentRow, 4).Value = item.RenewalDate;
                        worksheet.Cell(currentRow, 5).Value = item.Remarks;
                        worksheet.Columns(1, 6).Style.Alignment.WrapText = true;






                    }
                        #endregion
                        worksheet.Columns().AdjustToContents();
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
                                "Document Renewal List.xlsx"
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
