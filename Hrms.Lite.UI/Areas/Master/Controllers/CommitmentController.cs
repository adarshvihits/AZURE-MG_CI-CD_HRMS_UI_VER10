using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.Master;
using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.Master.Controllers
{
    [Area("Master")]
    [SessionAuthorize]
    public class CommitmentController : Controller
    {
        private readonly IDropdownService _dropdownService;
        public readonly ICommitmentService _service;
        public readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        public CommitmentController(IDropdownService dropdownService, ICommitmentService service, ICommonService commonService, IHelpers helpers)
        {
            _dropdownService = dropdownService;
            _service = service;
            _commonService = commonService;
            _helpers = helpers;
        }
        //public IActionResult Index()
        //{
        //    Commitment model = new Commitment();

        //    return View(model);
        //}

        public async Task<ActionResult> Index()
        {
            try
            {
                var a = await _service.GetList("ACTIVE");
                return View(await _service.GetList("ACTIVE"));
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

        public async Task<ActionResult> GetCommitmentList(string TabIndex)
        {
            try
            {
                if (TabIndex == "APPROVAL_PENDING")
                    return PartialView("_PendingList", await _service.GetList(TabIndex));
                else
                    return PartialView("_CommitmentList", await _service.GetList(TabIndex));
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
        public async Task<ActionResult> Create()
        {
            try
            {
                Commitment model = new Commitment();
                model.Mode = "Create";
                model.Active = true;
                ViewBag.CommitmentType = new SelectList(await _dropdownService.GetCommitmentType(), "Code", "Name");

                return PartialView("~/Areas/Master/Views/Commitment/_CreateOrEdit.cshtml", model);
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
        public async Task<ActionResult> Save(Commitment input)
        {
            try
            {
                var response = await _service.Create(input);

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

        public async Task<ActionResult> GetActiveInactiveValidation(string name, int code)
        {
            try
            {
                var response = await _service.GetMasterInactiveValidation(name, code);

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
        public async Task<ActionResult> Edit(Guid CommitmentGI, Guid LogGI)
        {

            try
            {
                Commitment model = await _service.GetCommitmentDetails(CommitmentGI, LogGI, "MODIFY");
                model.Mode = "MODIFY";
                ViewBag.CommitmentType = new SelectList(await _dropdownService.GetCommitmentType(), "Code", "Name");


                return PartialView("_CreateOrEdit", model);
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

        //public async Task<ActionResult> ValidationChecks(Guid CommitmentGI, Guid LogGI, string Type)
        //{
        //    Commitment model = await _service.GetCommitmentDetails(CommitmentGI, LogGI, Type);
        //    var response = model.ResponseEntity;
        //    return Json(response);
        //}





        public async Task<ActionResult> Approve(Guid CommitmentGI, Guid LogGI)
        {
            try
            {
                Commitment model = await _service.GetCommitmentDetails(CommitmentGI, LogGI, "APPROVAL");
                //model.Mode = "Approval";
                return PartialView("_Approve", model);
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
        public async Task<ActionResult> Delete(Guid CommitmentGI, Guid LogGI)
        {
            try
            {
                Commitment model = await _service.GetCommitmentDetails(CommitmentGI, LogGI, "DELETE");
                //model.Mode = "Delete";
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
        public async Task<ActionResult> Delete(Commitment model)
        {
            try
            {
                var response = await _service.Delete(model);
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
        [HttpPost]
        public async Task<ActionResult> Approve(Commitment model)
        {
            try
            {
                model.ApproveMode = "Approve";
                var response = await _service.ApproveOrReject(model);
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
        [HttpPost]
        public async Task<ActionResult> Reject(Commitment model)
        {
            try
            {
                model.ApproveMode = "Reject";
                var response = await _service.ApproveOrReject(model);
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

        public async Task<IActionResult> ExportToExcel(string TabIndex)
        {
            try
            {
                Commitment model = new Commitment();
                int slno = 0;
                string currDateTime = await _helpers.GetCurrentDate();
                
                model = await _service.GetList(TabIndex);
                string LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Commitment (" + TabIndex + ")", "Excel");
                if (TabIndex == "APPROVAL_PENDING")
                {

                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("PendingList");

                        int PendingCountnum = model.PendingCount + 6;


                        worksheet.Range("A1:D" + PendingCountnum).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:D" + PendingCountnum).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:D" + PendingCountnum).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:D" + PendingCountnum).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:D" + PendingCountnum).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 3)));


                        wsReportNameHeaderRange.Style.Font.Bold = true;
                        wsReportNameHeaderRange.Style.Font.FontSize = 15;
                        wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        wsReportNameHeaderRange.Merge();
                        wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                        wsReportNameHeaderRange.Style.Font.FontSize = 15;
                        wsReportNameHeaderRange.Value = "Commitment " + "(" + TabIndex + ")";
                        worksheet.Range("A6:D6").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                        worksheet.Cell(2, 1).Style.Font.Bold = true;
                        worksheet.Cell(2, 1).Value = "Company Name";
                        worksheet.Cell(2, 2).Value = CompanyName.ToUpper();
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
                        worksheet.Cell(5, 2).Value = model.PendingCount;







                        var currentRow = 6;

                        #region Header
                        worksheet.Cell(currentRow, 1).Value = "Sl.No";
                        worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 2).Value = "Name";
                        worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 3).Value = "Short Name";
                        worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 4).Value = "Entry Type";
                        worksheet.Cell(currentRow, 4).Style.Font.Bold = true;


                        #endregion

                        #region Body
                        foreach (var item in model.CommitmentList)
                        {
                            currentRow++;
                            slno = slno + 1;
                            worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);

                            worksheet.Cell(currentRow, 1).Value = slno;
                            worksheet.Cell(currentRow, 2).Value = item.Name;
                            worksheet.Cell(currentRow, 3).Value = item.ShortName;
                            worksheet.Cell(currentRow, 4).Value = item.Mode;
                            worksheet.Columns(1, 5).Style.Alignment.WrapText = true;

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
                                "Commitment Pending List.xlsx"
                                );
                        }
                    }

                }
                else
                {
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("Commitment List");
                        int count = model.CommitmentList.Count + 6;


                        worksheet.Range("A1:E" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:E" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:E" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:E" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:E" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 4)));
                        wsReportNameHeaderRange.Style.Font.Bold = true;
                        wsReportNameHeaderRange.Style.Font.FontSize = 15;
                        wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        wsReportNameHeaderRange.Merge();
                        wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                        wsReportNameHeaderRange.Value = "Commitment" + "(" + TabIndex + ")";
                        worksheet.Range("A6:E6").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                        worksheet.Cell(2, 1).Style.Font.Bold = true;
                        worksheet.Cell(2, 1).Value = "Company Name";
                        worksheet.Cell(2, 2).Value = CompanyName.ToUpper();
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
                        worksheet.Cell(5, 2).Value = model.CommitmentList.Count;




                        var currentRow = 6;

                        #region Header

                        worksheet.Cell(currentRow, 1).Value = "Sl.No";
                        worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 2).Value = "Name";
                        worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 3).Value = "Short Name";
                        worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 4).Value = "Commitment Type";
                        worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 5).Value = "Description";
                        worksheet.Cell(currentRow, 5).Style.Font.Bold = true;



                        #endregion

                        #region Body
                        foreach (var item in model.CommitmentList)
                        {

                            currentRow++;
                            slno = slno + 1;
                            worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 1).Value = slno;
                            worksheet.Cell(currentRow, 2).Value = item.Name;
                            worksheet.Cell(currentRow, 3).Value = item.ShortName;
                            worksheet.Cell(currentRow, 4).Value = item.CommitmentType;
                            worksheet.Cell(currentRow, 5).Value = item.Description;
                            worksheet.Columns(1, 5).Style.Alignment.WrapText = true;


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
                                "Commitment List.xlsx"
                                );
                        }
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
