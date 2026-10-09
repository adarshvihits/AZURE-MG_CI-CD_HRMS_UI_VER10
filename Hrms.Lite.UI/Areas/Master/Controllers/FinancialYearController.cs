using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class FinancialYearController : Controller
    {
        public readonly IFinancialYearService _FinancialYearService;
        public readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        public FinancialYearController(IFinancialYearService service, ICommonService commonService, IHelpers helpers)
        {
            _FinancialYearService = service;
            _commonService = commonService;
            _helpers = helpers;
        }
        public async Task<IActionResult> Index()
        {
            try {
                FinancialYear model = new FinancialYear();
                model.FinancialYearList = new List<FinancialYear>();    
                model.FinancialYearList.Add(new FinancialYear());
                return PartialView(await _FinancialYearService.GetList("ACTIVE"));
                //return PartialView("_FinancialYearList", await _FinancialYearService.GetList("ACTIVE"));
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


        public async Task<IActionResult> GetFinancialYearList(string TabIndex)
        {
            try
            {
                FinancialYear model = new FinancialYear();
                model.FinancialYearList = new List<FinancialYear>();
                model.FinancialYearList.Add(new FinancialYear());
                if (TabIndex == "APPROVAL_PENDING")
                {
                    return PartialView("_PendingList", await _FinancialYearService.GetList(TabIndex));
                }
                else
                {
                    return PartialView("_FinancialYearList", await _FinancialYearService.GetList(TabIndex));
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
        public IActionResult AddNew()
        {
            FinancialYear model = new FinancialYear();
            model.Mode = "CREATE";            
            return View("AddNew", model);

        }

        [HttpPost]
        public async Task<IActionResult> CheckIfAlreadyExist(FinancialYear model)
        {
            try
            {
            var response = await _FinancialYearService.GetFinancialYRProceedValidation(model);
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

        public async Task<IActionResult>GetFinancialYearDetailsList(DateTime fromDate,DateTime ToDate)
        {
            try
            {
                FinancialYear model = new FinancialYear();
            model.salaryPeriod = new SalaryPeriod();
            model = await _FinancialYearService.FinancialYeardetailsList(fromDate.ToString("yyyy-MMM-dd"), ToDate.ToString("yyyy-MMM-dd"));
                model.Mode = "Create";
            return PartialView("_CreateEditMonthGrid", model);
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
        public async Task<IActionResult> Save(FinancialYear model)
        {
            
            var response = await _FinancialYearService.Save(model);
            return Json(response);
            

        }
        public async Task<IActionResult> Edit(Guid FinancialYearGI,Guid LogGI)
        {
            try
            {
                FinancialYear model = await _FinancialYearService.GetFinancialYearDetails(FinancialYearGI, LogGI, "MODIFY");
                model.Mode = "MODIFY";
                return View("Edit", model);
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
        public async Task<IActionResult> Approve(Guid FinancialYearGI, Guid LogGI)
        {
            try
            {
                FinancialYear model = await _FinancialYearService.GetFinancialYearDetails(FinancialYearGI, LogGI, "APPROVAL");
                return View("Approve", model);
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
        public async Task<ActionResult> Approve(FinancialYear model)
        {

            model.ApproveMode = "APPROVE";
            var response = await _FinancialYearService.ApproveOrReject(model);
            return Json(response);
        }
        [HttpPost]
        public async Task<ActionResult> Reject(FinancialYear model)
        {

            model.ApproveMode = "Reject";
            var response = await _FinancialYearService.ApproveOrReject(model);
            return Json(response);
        }
        public async Task<IActionResult> Delete(Guid FinancialYearGI, Guid LogGI)
        {
            FinancialYear model = await _FinancialYearService.GetFinancialYearDetails(FinancialYearGI, LogGI, "DELETE");
            model.Mode = "DELETE";
            return PartialView("Delete", model);
        }
        [HttpPost]
        public async Task<ActionResult> Delete(FinancialYear model)
        {
            var response = await _FinancialYearService.Delete(model);
            return Json(response);


        }


        public async Task<ActionResult> GetActiveInactiveValidation(string name, int code)
        {
            try
            {
                var response = await _FinancialYearService.GetMasterInactiveValidation(name, code);

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
                FinancialYear model = new FinancialYear();
                model = await _FinancialYearService.GetList(TabIndex);
                int slno = 0;
                string currDateTime = await _helpers.GetCurrentDate();
                
                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("FinancialYear (" + TabIndex + ")", "Excel");

                if (TabIndex == "ACTIVE")
                {
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("FinancialYearList");
                        int PendingCountnum = model.FinancialYearList.Count + 6;


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
                        wsReportNameHeaderRange.Value = "FinancialYear " + "(" + TabIndex + ")";
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
                        worksheet.Cell(5, 2).Value = model.ActiveCount;
                        var currentRow = 6;
                        #region Header
                        worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 1).Value = "Sl.No";
                        worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 2).Value = "Financial Year";
                        worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 3).Value = "From";
                        worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 4).Value = "To";


                        #endregion

                        #region Body
                        foreach (var item in model.FinancialYearList)
                        {
                            currentRow++;
                            slno = slno + 1;
                            worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 1).Value = slno;
                            worksheet.Cell(currentRow, 2).Value = item.Name;
                            worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 3).Style.DateFormat.Format = "dd-MMM-yyyy";
                            worksheet.Cell(currentRow, 3).Value = item.FromDate;
                            worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 4).Style.DateFormat.Format = "dd-MMM-yyyy";
                            worksheet.Cell(currentRow, 4).Value = item.ToDate;
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
                                "Financial year Active List.xlsx"
                                );
                        }
                    }
                }
                else
                {
                    using(var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("FinancialYearList");
                        int PendingCountnum = model.FinancialYearList.Count + 6;


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
                        wsReportNameHeaderRange.Value = "FinancialYear " + "(" + TabIndex + ")";
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
                        worksheet.Cell(5, 2).Value = model.PendingCount;
                        var currentRow = 6;
                        #region Header
                        worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 1).Value = "Sl.No";
                        worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 2).Value = "Financial Year";
                        worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 3).Value = "From";
                        worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 4).Value = "To";
                        worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 5).Value = "Entry Type";
                        
                        #endregion

                        #region Body
                        foreach (var item in model.FinancialYearList)
                        {
                            currentRow++;
                            slno = slno + 1;
                            worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 1).Value = slno;
                            worksheet.Cell(currentRow, 2).Value = item.Name;
                            worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 3).Style.DateFormat.Format = "MMM-yyyy";
                            worksheet.Cell(currentRow, 3).Value = item.FromDate;
                            worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 4).Style.DateFormat.Format = "MMM-yyyy";
                            worksheet.Cell(currentRow, 4).Value = item.ToDate;
                            worksheet.Cell(currentRow, 5).Value = item.Mode;
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
                                "Financial year Pending List.xlsx"
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
