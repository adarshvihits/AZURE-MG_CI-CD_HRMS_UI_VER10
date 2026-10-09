using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Spire.Xls;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.Master.Controllers
{
    [Area("Master")]
    [SessionAuthorize]
    public class JobDescriptionController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly ICommonService _commonService;
        private readonly IJobDescriptionService _service;
        private readonly IHelpers _helpers;
        public JobDescriptionController(IDropdownService dropdownService, ICommonService commonService, IJobDescriptionService service, IHelpers helpers)
        {
            _dropdownService = dropdownService;
            _commonService = commonService;
            _service = service;
            _helpers = helpers;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
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
        public async Task<IActionResult> GetJobDescriptionList(string TabIndex)
        {
            try
            {
                if (TabIndex != "APPROVAL_PENDING")
                    return PartialView("_List", await _service.GetList(TabIndex));
                else
                    return PartialView("_PendingList", await _service.GetList(TabIndex));
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
            JobDescription model = new JobDescription();
            ViewBag.MasterJob = new SelectList(await _dropdownService.GetMasterJobDropdown(), "Code", "Name");
            model.Mode = "CREATE";
            model.Active = true;
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetJDVSDesignation(string Designations, int? JobDescriptionCode)
        {
            try
            {
                var response = await _service.GetJDVSDesignation(Designations, JobDescriptionCode);
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
        public async Task<IActionResult> Create(JobDescription model)
        {
            try
            {
                var response = await _service.JobDescriptionSave(model);
                return Json(response);

            }
            catch (UnauthorizedAccessException e)
            {
                // Job Description carries a rich-text field, which the API's
                // generic invalid-character filter (422) is prone to flag on
                // ordinary formatted text. Report it as a normal save failure
                // instead of falling into the shared "invalid characters"
                // popup flow (Login -> InvalidEntry view) used by every other
                // form on a genuine 401/403.
                if (e.Message == "422 Unprocessable Entity")
                {
                    HttpContext.Session.Remove("InvalidEntry");
                    return Json(new ResponseEntity("Save failed. Please review the job description and try again.", false));
                }

                return RedirectToAction("Login", "Account", new { Area = "" });
            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Account", new { Area = "" });
            }
        }

        public async Task<IActionResult> Modify(Guid JobDescriptionGI, Guid LogGI)
        {
            JobDescription model = await _service.GetJobDescriptionDetails(JobDescriptionGI, LogGI, "MODIFY");
            ViewBag.MasterJob = new SelectList(await _dropdownService.GetMasterJobDropdown(), "Code", "Name");
            model.Mode = "MODIFY";
            return View("Create", model);
        }

        public async Task<IActionResult> Delete(Guid JobDescriptionGI, Guid LogGI)
        {
            try
            {
                JobDescription model = await _service.GetJobDescriptionDetails(JobDescriptionGI, LogGI, "DELETE");
                model.Mode = "DELETE";
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
        public async Task<IActionResult> Delete(JobDescription model)
        {
            try
            {
                var response = await _service.JobDescriptionDelete(model);
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

        public async Task<IActionResult> Approve(Guid JobDescriptionGI, Guid LogGI)
        {
            try
            {
                JobDescription model = await _service.GetJobDescriptionDetails(JobDescriptionGI, LogGI, "APPROVAL");
                if (string.IsNullOrEmpty(model.JDescription))
                {
                    // The "APPROVAL" lookup doesn't return JDescription, leaving the
                    // slider's hidden field blank and the Approve save failing on the
                    // required-field check. "MODIFY" reliably returns it (same call
                    // the edit form uses), so fall back to that for this field only.
                    JobDescription detailsWithDescription = await _service.GetJobDescriptionDetails(JobDescriptionGI, LogGI, "MODIFY");
                    model.JDescription = detailsWithDescription?.JDescription;
                }
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
        [HttpPost]
        public async Task<IActionResult> Approve(JobDescription model)
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
        public async Task<IActionResult> Reject(JobDescription model)
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

        [HttpGet]
        public async Task<IActionResult> GetMasterJobDetails(int PositionPlanCode)
        {
            try
            {
                var response = await _service.GetMasterJobDetails(PositionPlanCode);
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

        public async Task<IActionResult> GetActiveInactiveValidation(string name, int code)
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

        [HttpGet]
        public async Task<IActionResult> GetMasterJobList()
        {
            try
            {
                var response = await _dropdownService.GetMasterJobDropdown();
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
                JobDescription model = new JobDescription();
                model = await _service.GetList(TabIndex);
                int slno = 0;
                string currDateTime = await _helpers.GetCurrentDate();

                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Job Description List (" + TabIndex + ")", "Excel");
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add(TabIndex + " List");
                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 5)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();


                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Value = "Job Description List (" + TabIndex + ")";
                    worksheet.Range("A6:F6").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
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
                    var currentRow = 6;
                    #region Header
                    worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 1).Value = "Sl.No";
                    worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 2).Value = "Master Job";
                    worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 3).Value = "Business Type";
                    worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 4).Value = "Department";
                    worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 5).Value = "Section";
                    worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 6).Value = "Grade";


                    #endregion

                    #region Body
                    foreach (var item in model.JobDescriptionList)
                    {
                        currentRow++;
                        slno = slno + 1;
                        worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 1).Value = slno;
                        worksheet.Cell(currentRow, 2).Value = item.MasterJob?.Name;
                        worksheet.Cell(currentRow, 3).Value = item.BusinessType?.Name;
                        worksheet.Cell(currentRow, 4).Value = item.Department?.Name;
                        worksheet.Cell(currentRow, 5).Value = item.Section?.Name;
                        worksheet.Cell(currentRow, 6).Value = item.Grade?.Name;
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
                            "Job Description List.xlsx"
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
