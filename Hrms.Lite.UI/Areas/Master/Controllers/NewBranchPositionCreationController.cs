
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
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
    public class NewBranchPositionCreationController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly INewBranchPositionCreationService _NewBranchPositionCreationService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        public NewBranchPositionCreationController(INewBranchPositionCreationService NewBranchPositionCreationService, IDropdownService dropdownService, ICommonService commonService, IHelpers helpers)
        {
            _NewBranchPositionCreationService = NewBranchPositionCreationService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                NewBranchPositionCreationViewModel model = new NewBranchPositionCreationViewModel();
               
                ViewBag.PlanningPeriod = new SelectList(await _dropdownService.GetHRPlanningPeriodDropDown(), "Code", "Name");
                ViewBag.Priority = new SelectList(await _dropdownService.GetPriorityDropdown(), "Code", "Name");  

                model = await _NewBranchPositionCreationService.GetNewBranchPositionCreationList("ACTIVE");
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
        public async Task<IActionResult> GetNewBranchPostionCreationList(string TabIndex,  int? PlanningPeriod)
        {
            try
            {
                NewBranchPositionCreationViewModel model = new NewBranchPositionCreationViewModel();
             
                model = await _NewBranchPositionCreationService.GetNewBranchPositionCreationList(TabIndex,  PlanningPeriod);
             
                if (TabIndex != "APPROVAL_PENDING")
                    return PartialView("_ActiveList", model);
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
        [HttpGet]
        public async Task<IActionResult> GetMasterJobDetails(int PlanCode)
        {
            try
            {
                var response = await _NewBranchPositionCreationService.GetMasterJobDetails(PlanCode);
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
        public async Task<IActionResult> SavePositionRequest(PositionRequest model)
        {
            try
            {
                var response = await _NewBranchPositionCreationService.SavePositionRequest(model);
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

         
        
        
       
        public async Task<IActionResult> ExportToExcel(string TabIndex, int? PlanningPeriod)
        {
            try
            {
                NewBranchPositionCreationViewModel model = await _NewBranchPositionCreationService.GetNewBranchPositionCreationList(TabIndex ?? "ACTIVE", PlanningPeriod);
                var positions = model?.Positions ?? new List<NewBranchPositionCreation>();

                string currDateTime = await _helpers.GetCurrentDate();
                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                ExcelPassword EP = await _commonService.GetExcelPassword("New Branch Position Creation", "Excel");

                const int lastCol = 9;
                const int headerRow = 6;

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Position Creation");

                    var titleRange = worksheet.Range(1, 1, 1, lastCol);
                    titleRange.Merge();
                    titleRange.Value = "New Branch Position Creation";
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
                    worksheet.Cell(5, 2).Value = positions.Count;
                    worksheet.Range(2, 1, 5, 1).Style.Font.Bold = true;
                    worksheet.Range(2, 2, 5, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);

                    string[] headers = { "Sl.No", "Branch", "Department", "Section", "Grade", "Plan Count", "Avg Salary", "Total Salary", "Currency" };
                    for (int c = 0; c < headers.Length; c++)
                        worksheet.Cell(headerRow, c + 1).Value = headers[c];

                    var headerRange = worksheet.Range(headerRow, 1, headerRow, headers.Length);
                    headerRange.Style.Font.Bold = true;
                    headerRange.Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);

                    int row = headerRow;
                    int slno = 0;
                    foreach (var item in positions)
                    {
                        row++;
                        slno++;
                        worksheet.Cell(row, 1).Value = slno;
                        worksheet.Cell(row, 2).Value = item.BranchName;
                        worksheet.Cell(row, 3).Value = item.DepartmentName;
                        worksheet.Cell(row, 4).Value = item.SectionName;
                        worksheet.Cell(row, 5).Value = item.GradeName;
                        worksheet.Cell(row, 6).Value = item.PlanCount;
                        worksheet.Cell(row, 7).Value = item.AvgSalary;
                        worksheet.Cell(row, 8).Value = item.TotalSalary;
                        worksheet.Cell(row, 9).Value = item.CurrencyName;
                    }

                    var tableRange = worksheet.Range(headerRow, 1, Math.Max(row, headerRow), headers.Length);
                    tableRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    tableRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range(headerRow + 1, 1, Math.Max(row, headerRow + 1), 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Range(headerRow + 1, 6, Math.Max(row, headerRow + 1), 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
                    worksheet.Range(headerRow + 1, 7, Math.Max(row, headerRow + 1), 8).Style.NumberFormat.Format = "#,##0";
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
                            "New Branch Position Creation.xlsx");
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

