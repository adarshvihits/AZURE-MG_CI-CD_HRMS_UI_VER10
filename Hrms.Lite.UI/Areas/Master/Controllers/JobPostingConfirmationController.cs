using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
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
    public class JobPostingConfirmationController : Controller
    {
        private readonly IJobPostingConfirmationService _jobPostingConfirmationService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        private readonly IDropdownService _dropdownService;
        public JobPostingConfirmationController(IJobPostingConfirmationService jobPostingConfirmationService, ICommonService commonService, IHelpers helpers, IDropdownService dropdownService)
        {
            _jobPostingConfirmationService = jobPostingConfirmationService;
            _commonService = commonService;
            _helpers = helpers;
            _dropdownService = dropdownService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                JobPostingConfirmationViewModel model = await _jobPostingConfirmationService.GetJobPostingConfirmationList("PENDING");
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
                JobPostingConfirmationViewModel model = await _jobPostingConfirmationService.GetJobPostingConfirmationList(TabIndex);

                if (TabIndex == "CONFIRMED")
                    return PartialView("_ConfirmedList", model);
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

        public async Task<IActionResult> Proceed(int VacancySourceCode, int SourceTranactionCode)
        {
            try
            {
                JobPostingConfirmationDetails model = await _jobPostingConfirmationService.GetVacancyDetails(VacancySourceCode, SourceTranactionCode);
                model.PostedBy = HttpContext.Session.GetString("LoginUser");
                ViewBag.PostTo = new SelectList(await _dropdownService.GetJobPostingDestinationDropdown(), "Code", "Name");
                return PartialView("_VacancySlider", model);
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
        public async Task<IActionResult> ConfirmPosting(JobPostingConfirmationRequest model)
        {
            try
            {
                var response = await _jobPostingConfirmationService.ConfirmPosting(model);
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
                bool confirmed = TabIndex == "CONFIRMED";
                JobPostingConfirmationViewModel model = await _jobPostingConfirmationService.GetJobPostingConfirmationList(TabIndex ?? "PENDING");
                var vacancies = model?.Vacancies ?? new List<JobPostingConfirmation>();

                string currDateTime = await _helpers.GetCurrentDate();
                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                ExcelPassword EP = await _commonService.GetExcelPassword("Job Posting Confirmation", "Excel");

                var headers = new List<string> { "Sl.No", "Branch", "Department", "Section", "Grade", "Vacancy Source", "Priority", "Plan Count" };
                if (confirmed)
                    headers.AddRange(new[] { "Posted To", "Posted On" });

                const int headerRow = 6;
                int lastCol = headers.Count;

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add(confirmed ? "Confirmed & Posted" : "Pending Confirmation");

                    var titleRange = worksheet.Range(1, 1, 1, lastCol);
                    titleRange.Merge();
                    titleRange.Value = "Job Posting Confirmation";
                    titleRange.Style.Font.Bold = true;
                    titleRange.Style.Font.FontSize = 15;
                    titleRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    titleRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);

                    worksheet.Cell(2, 1).Value = "Company Name";
                    worksheet.Cell(2, 2).Value = (CompanyName ?? "").ToUpper();
                    worksheet.Cell(3, 1).Value = "Login User";
                    worksheet.Cell(3, 2).Value = LoginUser;
                    worksheet.Cell(4, 1).Value = "Date & Time";
                    worksheet.Cell(4, 2).Value = currDateTime;
                    worksheet.Cell(5, 1).Value = "Total Count";
                    worksheet.Cell(5, 2).Value = vacancies.Count;
                    worksheet.Range(2, 1, 5, 1).Style.Font.Bold = true;
                    worksheet.Range(2, 2, 5, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);

                    for (int c = 0; c < headers.Count; c++)
                        worksheet.Cell(headerRow, c + 1).Value = headers[c];

                    var headerRange = worksheet.Range(headerRow, 1, headerRow, lastCol);
                    headerRange.Style.Font.Bold = true;
                    headerRange.Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);

                    int row = headerRow;
                    int slno = 0;
                    foreach (var item in vacancies)
                    {
                        row++;
                        slno++;
                        worksheet.Cell(row, 1).Value = slno;
                        worksheet.Cell(row, 2).Value = item.BranchName;
                        worksheet.Cell(row, 3).Value = item.DepartmentName;
                        worksheet.Cell(row, 4).Value = item.SectionName;
                        worksheet.Cell(row, 5).Value = item.GradeName;
                        worksheet.Cell(row, 6).Value = item.VacancySource;
                        worksheet.Cell(row, 7).Value = item.PriorityName;
                        worksheet.Cell(row, 8).Value = item.PlanCount;
                        if (confirmed)
                        {
                            worksheet.Cell(row, 9).Value = item.PostedTo;
                            worksheet.Cell(row, 10).Value = item.PostedOn?.ToString("dd-MMM-yyyy");
                        }
                    }

                    var tableRange = worksheet.Range(headerRow, 1, Math.Max(row, headerRow), lastCol);
                    tableRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    tableRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range(headerRow + 1, 1, Math.Max(row, headerRow + 1), 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Columns().AdjustToContents();

                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        byte[] content = stream.ToArray();

                        if (EP != null && EP.NeedPassword)
                        {
                            using (var input = new MemoryStream(content))
                            using (var output = new MemoryStream())
                            {
                                Workbook book = new Workbook();
                                book.LoadFromStream(input);
                                book.Protect(EP.Password, false, false);
                                book.SaveToStream(output);
                                content = output.ToArray();
                            }
                        }

                        return File(
                            content,
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            confirmed ? "Job Posting Confirmed List.xlsx" : "Job Posting Pending List.xlsx");
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
