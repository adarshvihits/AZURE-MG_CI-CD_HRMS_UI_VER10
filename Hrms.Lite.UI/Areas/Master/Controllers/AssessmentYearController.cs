using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.Master;
using Hrms.Lite.Services.Services.Master;
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
    public class AssessmentYearController : Controller
    {
        public readonly IAssessmentYearService _service;
        public readonly ICommonService _commonService;
        public readonly IDropdownService _dropdownService;
        private readonly IHelpers _helpers;

        public AssessmentYearController(IAssessmentYearService service, ICommonService commonService, IDropdownService dropdownService, IHelpers helpers)
        {
            _service = service;
            _commonService = commonService;
            _dropdownService = dropdownService;
            _helpers = helpers;

        }
        public async Task <IActionResult> Index()
        {
            AssessmentYear model = new AssessmentYear();
            ViewBag.AssFinYear = new SelectList(await _dropdownService.GetFinancialYearDropdown(), "Code", "Name");
            model = await _service.GetList("ACTIVE");
            return View(model);
        }


        public async Task<IActionResult> GetList(string TabIndex)
        {
            try
            {
                if (TabIndex == "APPROVAL_PENDING")
                    return PartialView("_PendingList", await _service.GetList(TabIndex));
                else
                    return PartialView("_AssessmentYearList", await _service.GetList(TabIndex));
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




        public async Task<ActionResult> Create()
        {
            try
            {
                AssessmentYear model = new AssessmentYear();
                ViewBag.AssFinYear = new SelectList(await _dropdownService.GetFinancialYearDropdown(), "Code", "Name");
                model.Mode = "CREATE";
                model.Active = true;
                return PartialView("~/Areas/Master/Views/AssessmentYear/_CreateOrEdit.cshtml", model);
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
        public async Task<ActionResult> Edit(Guid AssessmentYearGI, Guid LogGI)
        {

            try
            {
                AssessmentYear model = new AssessmentYear();
                ViewBag.AssFinYear = new SelectList(await _dropdownService.GetFinancialYearDropdown(), "Code", "Name");

                model = await _service.AssessmentYearDetails(AssessmentYearGI, LogGI, "MODIFY");
                model.Mode = "MODIFY";

                return PartialView("~/Areas/Master/Views/AssessmentYear/_CreateOrEdit.cshtml", model);
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
        public async Task<ActionResult> Save(AssessmentYear input)
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




        public async Task<ActionResult> Delete(Guid AssessmentYearGI, Guid LogGI)
        {
            try
            {
                AssessmentYear model = new AssessmentYear();
                model = await _service.AssessmentYearDetails(AssessmentYearGI, LogGI, "DELETE");
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
        public async Task<ActionResult> Delete(AssessmentYear model)
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
        public async Task<ActionResult> Approve(Guid AssessmentYearGI, Guid LogGI)
        {
            try
            {
                AssessmentYear model = new AssessmentYear();

                model = await _service.AssessmentYearDetails(AssessmentYearGI, LogGI, "APPROVAL");
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
        [HttpPost]
        public async Task<ActionResult> Approve(AssessmentYear model)
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
        public async Task<ActionResult> Reject(AssessmentYear model)
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
                AssessmentYear model = new AssessmentYear();
                model = await _service.GetList(TabIndex);
                int slno = 0;
                string currDateTime = await _helpers.GetCurrentDate();
                
                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Assessment Year(" + TabIndex + ")", "Excel");
                if (TabIndex == "APPROVAL_PENDING")
                {

                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("PendingList");
                        int PendingCountnum = model.PendingCount + 6;


                        worksheet.Range("A1:F" + PendingCountnum).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:F" + PendingCountnum).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:F" + PendingCountnum).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:F" + PendingCountnum).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:F" + PendingCountnum).Style.Border.LeftBorder = XLBorderStyleValues.Thin;



                        var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 5)));
                        wsReportNameHeaderRange.Style.Font.Bold = true;
                        wsReportNameHeaderRange.Style.Font.FontSize = 15;
                        wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        wsReportNameHeaderRange.Merge();


                        wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                        wsReportNameHeaderRange.Style.Font.FontSize = 15;
                        wsReportNameHeaderRange.Value = "Assessment Year" + "(" + TabIndex + ")";
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
                        worksheet.Cell(5, 1).Value = "Total Count";
                        worksheet.Cell(5, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(5, 2).Value = model.PendingCount;
                        var currentRow = 6;
                        #region Header
                        worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 1).Value = "Sl.No";

                        worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 2).Value = "Assessment Year";

                        worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 3).Value = "From";

                        worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 4).Value = "To";

                        worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 5).Value = "Financial Year";


                        worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 6).Value = "Entry Type";


                        #endregion

                        #region Body
                        foreach (var item in model.AssessmentYearList)
                        {
                            currentRow++;
                            slno = slno + 1;
                            worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 1).Value = slno;

                            worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 2).Value = item.Name;

                            worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 3).Value = item.From;
                            worksheet.Cell(currentRow, 3).Style.DateFormat.Format = "dd-MMM-yyyy";

                            worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 4).Style.DateFormat.Format = "dd-MMM-yyyy";
                            worksheet.Cell(currentRow, 4).Value = item.To;

                            worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 5).Value = item.FinancialYear.Name;


                            worksheet.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 6).Value = item.Mode;
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
                                book.Protect(EP.Password,false,false);
                                book.SaveToStream(stream);
                            }
                            var content = stream.ToArray();
                            return File(
                                content,
                                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                                "Assessment Year List.xlsx"
                                );
                        }
                    }

                }

                else
                {

                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("PendingList");
                        int PendingCountnum = model.AssessmentYearList.Count + 6;


                        worksheet.Range("A1:E" + PendingCountnum).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:E" + PendingCountnum).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:E" + PendingCountnum).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:E" + PendingCountnum).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:E" + PendingCountnum).Style.Border.LeftBorder = XLBorderStyleValues.Thin;



                        var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 4)));
                        wsReportNameHeaderRange.Style.Font.Bold = true;
                        wsReportNameHeaderRange.Style.Font.FontSize = 15;
                        wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        wsReportNameHeaderRange.Merge();


                        wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                        wsReportNameHeaderRange.Style.Font.FontSize = 15;
                        wsReportNameHeaderRange.Value = "Assessment Year" + "(" + TabIndex + ")";
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
                        worksheet.Cell(5, 2).Value = model.AssessmentYearList.Count;
                        var currentRow = 6;
                        #region Header
                        worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 1).Value = "Sl.No";

                        worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 2).Value = "Assessment Year";

                        worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 3).Value = "From";

                        worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 4).Value = "To";

                        worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 5).Value = "Financial Year";


                       


                        #endregion

                        #region Body
                        foreach (var item in model.AssessmentYearList)
                        {
                            currentRow++;
                            slno = slno + 1;
                            worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 1).Value = slno;

                            worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 2).Value = item.Name;

                            worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 3).Value = item.From;
                            worksheet.Cell(currentRow, 3).Style.DateFormat.Format = "dd-MMM-yyyy";

                            worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 4).Style.DateFormat.Format = "dd-MMM-yyyy";
                            worksheet.Cell(currentRow, 4).Value = item.To;

                            worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 5).Value = item.FinancialYear.Name;


                          
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
                                book.Protect(EP.Password,false,false);
                                book.SaveToStream(stream);
                            }
                            var content = stream.ToArray();
                            return File(
                                content,
                                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                                "Assessment Year List.xlsx"
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

        //public async Task<ActionResult> GetActiveInactiveValidation(string name, int code)
        //{
        //    try
        //    {
        //        var response = await _service.GetMasterInactiveValidation(name, code);

        //        return Json(response);
        //    }
        //    catch (UnauthorizedAccessException e)
        //    {

        //        return RedirectToAction("Login", "Account", new { Area = "" });
        //    }
        //    catch (Exception e)
        //    {
        //        return RedirectToAction("Error", "Account", new { Area = "" });
        //    }
        //}


    }
}
