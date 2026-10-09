using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.Master;
using Hrms.Lite.Services.IServices.Recruitment;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Hrms.Lite.Shared.Recruitment;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Hrms.Lite.Shared.DataBank.Generic.Master;

namespace Hrms.Lite.UI.Areas.Recruitment.Controllers
{
    [Area("Recruitment")]
    [SessionAuthorize]
    public class RecruitmentdriveController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly IRecruitmentdriveService _service;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        public RecruitmentdriveController(IDropdownService dropdownService, IRecruitmentdriveService service, ICommonService commonService, IHelpers helpers)
        {
            _dropdownService = dropdownService;
            _service = service;
            _commonService = commonService;
            _helpers = helpers;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                Recruitmentdrive model = new Recruitmentdrive();
                model = await _service.GetList("ACTIVE");
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
        public async Task<IActionResult> GetList(string TabIndex)
        {
            try
            {
                Recruitmentdrive model = new Recruitmentdrive();
                model = await _service.GetList(TabIndex);
                if (TabIndex != "APPROVAL_PENDING")
                    return PartialView("_List", model);
                else
                    return PartialView("_PendingList", model);
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
        public async Task<IActionResult> AddNew()
        {
            try
            {
                ViewBag.RecruitmentVenue = new SelectList(await _dropdownService.GetRecruitmentVenueDropdown(), "Code", "Name");
                ViewBag.ExpenseType = new SelectList(await _dropdownService.GetRecruitmentExpenseTypeDropdown(), "Code", "Name");

                Recruitmentdrive model = new Recruitmentdrive();
                model.Venue = new Dropdown();
              
                model.CostSplitUpList = new List<RecruitmentDriveCost>();
                model.Active = true;
                model.Mode = "CREATE";
                return View("~/Areas/Recruitment/Views/Recruitmentdrive/AddNew.cshtml", model);
            }
            catch (UnauthorizedAccessException)
            {
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Account", new { Area = "" });
            }
        }
        public async Task<IActionResult> Edit(Guid RecruitmentdriveGI, Guid LogGI)
        {
            try
            {
                ViewBag.RecruitmentVenue = new SelectList(await _dropdownService.GetRecruitmentVenueDropdown(), "Code", "Name");
                ViewBag.ExpenseType = new SelectList(await _dropdownService.GetRecruitmentExpenseTypeDropdown(), "Code", "Name");

                Recruitmentdrive model = await _service.GetResultList(RecruitmentdriveGI, LogGI, "MODIFY");
                if (model == null)
                    return RedirectToAction("Index", "Recruitmentdrive", new { Area = "Recruitment" });

                model.Mode = "MODIFY";
                return View("~/Areas/Recruitment/Views/Recruitmentdrive/AddNew.cshtml", model);
            }
            catch (UnauthorizedAccessException)
            {
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Account", new { Area = "" });
            }
        }
        public async Task<IActionResult> Approve(Guid RecruitmentdriveGI, Guid LogGI)
        {
            try
            {
                Recruitmentdrive model = await _service.GetResultList(RecruitmentdriveGI, LogGI, "APPROVAL");
                if (model == null)
                    return RedirectToAction("Index", "Recruitmentdrive", new { Area = "Recruitment" });

                model.Mode = "APPROVAL";
                return PartialView("~/Areas/Recruitment/Views/Recruitmentdrive/_Approve.cshtml", model);
            }
            catch (UnauthorizedAccessException)
            {
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Account", new { Area = "" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Save(Recruitmentdrive model)
        {
            try
            {
                if (model.Mode != "MODIFY")
                    model.Mode = "CREATE";
                var response = await _service.Save(model);

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
        public async Task<IActionResult> Delete(Guid RecruitmentdriveGI, Guid LogGI)
        {
            try
            {
                Recruitmentdrive model = await _service.GetResultList(RecruitmentdriveGI, LogGI, "DELETE");
                if (model == null)
                    return RedirectToAction("Index", "Recruitmentdrive", new { Area = "Recruitment" });

                model.Mode = "DELETE";
                return PartialView("~/Areas/Recruitment/Views/Recruitmentdrive/Delete.cshtml", model);
            }
            catch (UnauthorizedAccessException)
            {
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Account", new { Area = "" });
            }
        }
        public async Task<IActionResult> DeleteRecruitmentdrive(Guid RecruitmentdriveGI)
        {
            try
            {
                var response = await _service.Delete(RecruitmentdriveGI);
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
        public async Task<IActionResult> ApproveRecruitmentdrive(Recruitmentdrive model)
        {
            try
            {
                model.ApproveMode = "APPROVE";
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
        public async Task<IActionResult> RejectRecruitmentdrive(Recruitmentdrive model)
        {
            try
            {
                model.ApproveMode = "REJECT";
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
                Recruitmentdrive model = new Recruitmentdrive();
                model = await _service.GetList(TabIndex);
                int slno = 0;
                string currDateTime = await _helpers.GetCurrentDate();

                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Recruitmentdrive(" + TabIndex + ")", "Excel");

                if (TabIndex != "APPROVAL_PENDING")
                {
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("Recruitment Drive List");
                        int count = model.List.Count + 6;

                        worksheet.Range("A1:G" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:G" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:G" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:G" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:G" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;

                        var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 6)));
                        wsReportNameHeaderRange.Style.Font.Bold = true;
                        wsReportNameHeaderRange.Style.Font.FontSize = 15;
                        wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        wsReportNameHeaderRange.Merge();
                        wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                        wsReportNameHeaderRange.Value = "Recruitmentdrive" + "(" + TabIndex + ")";

                        worksheet.Range("A6:G6").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
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
                        worksheet.Cell(5, 2).Value = model.List.Count;

                        var currentRow = 6;
                        #region Header
                        worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 1).Value = "Sl.No";
                        worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 2).Value = "Name";
                        worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 3).Value = "Location";
                        worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 4).Value = "Venue";
                        worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 5).Value = "From";
                        worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 6).Value = "To";
                        worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 7).Value = "Cost";
                        #endregion

                        #region Body
                        foreach (var item in model.List)
                        {
                            currentRow++;
                            slno = slno + 1;
                            worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 1).Value = slno;
                            worksheet.Cell(currentRow, 2).Value = item.Name;
                            worksheet.Cell(currentRow, 3).Value = item.LocationName;
                            worksheet.Cell(currentRow, 4).Value = item.Venue?.Name;

                            if (item.DateFrom.HasValue)
                                worksheet.Cell(currentRow, 5).Value = item.DateFrom.Value;
                            worksheet.Cell(currentRow, 5).Style.DateFormat.Format = "dd-MMM-yyyy";

                            if (item.DateTo.HasValue)
                                worksheet.Cell(currentRow, 6).Value = item.DateTo.Value;
                            worksheet.Cell(currentRow, 6).Style.DateFormat.Format = "dd-MMM-yyyy";

                            if (item.Cost.HasValue)
                                worksheet.Cell(currentRow, 7).Value = item.Cost.Value;
                            worksheet.Cell(currentRow, 7).Style.NumberFormat.Format = "#,##0.00";
                        }
                        worksheet.Columns(1, 4).Style.Alignment.WrapText = true;
                        #endregion

                        worksheet.Columns().AdjustToContents();
                        worksheet.Column(5).Width = 14;
                        worksheet.Column(6).Width = 14;

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
                                "Recruitmentdrive List.xlsx"
                                );
                        }
                    }
                }
                else
                {
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("Pending List");
                        int PendingCountnum = model.PendingCount + 6;

                        worksheet.Range("A1:H" + PendingCountnum).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:H" + PendingCountnum).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:H" + PendingCountnum).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:H" + PendingCountnum).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:H" + PendingCountnum).Style.Border.LeftBorder = XLBorderStyleValues.Thin;

                        var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 7)));
                        wsReportNameHeaderRange.Style.Font.Bold = true;
                        wsReportNameHeaderRange.Style.Font.FontSize = 15;
                        wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        wsReportNameHeaderRange.Merge();
                        wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                        wsReportNameHeaderRange.Value = "Recruitmentdrive " + "(" + TabIndex + ")";

                        worksheet.Range("A6:H6").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
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
                        worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 1).Value = "Sl.No";
                        worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 2).Value = "Name";
                        worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 3).Value = "Location";
                        worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 4).Value = "Venue";
                        worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 5).Value = "From";
                        worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 6).Value = "To";
                        worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 7).Value = "Cost";
                        worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 8).Value = "Entry Type";
                        #endregion

                        #region Body
                        foreach (var item in model.List)
                        {
                            currentRow++;
                            slno = slno + 1;
                            worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 1).Value = slno;
                            worksheet.Cell(currentRow, 2).Value = item.Name;
                            worksheet.Cell(currentRow, 3).Value = item.LocationName;
                            worksheet.Cell(currentRow, 4).Value = item.Venue?.Name;

                            if (item.DateFrom.HasValue)
                                worksheet.Cell(currentRow, 5).Value = item.DateFrom.Value;
                            worksheet.Cell(currentRow, 5).Style.DateFormat.Format = "dd-MMM-yyyy";

                            if (item.DateTo.HasValue)
                                worksheet.Cell(currentRow, 6).Value = item.DateTo.Value;
                            worksheet.Cell(currentRow, 6).Style.DateFormat.Format = "dd-MMM-yyyy";

                            if (item.Cost.HasValue)
                                worksheet.Cell(currentRow, 7).Value = item.Cost.Value;
                            worksheet.Cell(currentRow, 7).Style.NumberFormat.Format = "#,##0.00";

                            worksheet.Cell(currentRow, 8).Value = item.ApprovalStatus;
                        }
                        worksheet.Columns(1, 4).Style.Alignment.WrapText = true;
                        #endregion

                        worksheet.Columns().AdjustToContents();
                        worksheet.Column(5).Width = 14;
                        worksheet.Column(6).Width = 14;

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
                                "RecruitmentdriveList.xlsx"
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
