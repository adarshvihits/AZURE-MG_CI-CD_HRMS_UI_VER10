using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Spire.Xls;
using System.IO;

namespace Hrms.Lite.UI.Areas.Master.Controllers
{
    [Area("Master")]
    [SessionAuthorize]
    public class SalaryPeriodLockController : Controller
    {
        private readonly ISalaryPeriodLockService _service;
        private readonly ICommonService _commonService;
        public readonly IDropdownService _dropdownService;
        private readonly IHelpers _helpers;
        public SalaryPeriodLockController(ISalaryPeriodLockService service, ICommonService commonService, IDropdownService dropdownService, IHelpers helpers)
        {
            _service = service;
            _commonService = commonService;
            _dropdownService = dropdownService;
            _helpers = helpers;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var currMonth = await _commonService.GetCurrentMonthDropdown();
            int currentmonth = Convert.ToInt32(currMonth.Data);
            ViewBag.month = new SelectList(await _dropdownService.GetMonthDropdown(), "Code", "Name");
            SalaryPeriodLock model = await _service.GetList(currentmonth);

            model.MonthCode = currMonth.Data;
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

        public async Task<IActionResult> GetList(int currentmonth)
        {
            SalaryPeriodLock model = await _service.GetList(currentmonth);

            return PartialView("_SalaryPeriodLockUnlockList",model);
        }
        public async Task<IActionResult> GetPopUp(string mode)
        {
            try
            {
                SalaryPeriodLock model = new SalaryPeriodLock();
                var currMonth = await _commonService.GetCurrentMonthDropdown();
                int currentmonth = Convert.ToInt32(currMonth.Data);
                ViewBag.month = new SelectList(await _dropdownService.GetMonthDropdown(), "Code", "Name");
                ViewBag.PGCode = new SelectList(await _dropdownService.GetPayrollGroupWithGrpCmpyCheckDropdown(), "Code", "Name");
               

                if (mode == "LockUnlock")
                {
                    model = await _service.GetLockUnLockLog(currMonth.Data, 0);
                    model.MonthCode = currMonth.Data;
                    return PartialView("_LockUnlockLogPopUp", model);
                }
                else
                {
                    model = await _service.GetOpenMonth(currMonth.Data, 0);
                    model.MonthCode = currMonth.Data;
                    return PartialView("_OpenMonthPopUp", model);
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


        public async Task<IActionResult> GetMonthPopUpList(int month,int PGcode)
        {
            try
            {
                SalaryPeriodLock model = new SalaryPeriodLock();

                ViewBag.month = new SelectList(await _dropdownService.GetMonthDropdown(), "Code", "Name");
                ViewBag.PGCode = new SelectList(await _dropdownService.GetPayrollGroupWithGrpCmpyCheckDropdown(), "Code", "Name");
                model = await _service.GetOpenMonth(month, PGcode);

                return PartialView("_OpenMonthPopUpList", model);

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

        public async Task<IActionResult> GetLockUnLockList(int month, int PGcode)
        {
            try
            {
                SalaryPeriodLock model = new SalaryPeriodLock();

                ViewBag.month = new SelectList(await _dropdownService.GetMonthDropdown(), "Code", "Name");
                ViewBag.PGCode = new SelectList(await _dropdownService.GetPayrollGroupWithGrpCmpyCheckDropdown(), "Code", "Name");
                model = await _service.GetLockUnLockLog(month, PGcode);

                return PartialView("_LockUnlockLogPopUpList", model);

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
        public async Task<ActionResult> Save(SalaryPeriodLock input)
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


        public async Task<IActionResult> ExportToExcel(string Type, int Month, int PGcode, string MName, string PName)
        {
            try
            {
                SalaryPeriodLock model = new SalaryPeriodLock();
                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                ExcelPassword EP = new ExcelPassword();
                var FileName="";
                int slNo = 0;


                if (Type == "LockUnlock")
                {
                    EP = await _commonService.GetExcelPassword("Salary Period Lock UnLock Log", "Excel");
                    FileName = "Salary Period Lock UnLock Log";
                    model = await _service.GetLockUnLockLog(Month, PGcode);
                }
                else
                {
                    EP = await _commonService.GetExcelPassword("Salary Period Open Month", "Excel");
                    FileName = "Salary Period Lock ( Open Month)";
                    model = await _service.GetOpenMonth(Month, PGcode);
                }

                int count = model.LockUnlock.Count + 4;
                int column = 0;
                IXLRange wsReportNameHeaderRange;
                IXLWorksheet worksheet;




                string currDateTime = await _helpers.GetCurrentDate();
                
                using (var workbook = new XLWorkbook())
                {
                 
                   

                    if (Type == "LockUnlock")
                    {
                        worksheet = workbook.Worksheets.Add("Lock Unlock Log");
                        worksheet.Range("A1:H" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:H" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:H" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:H" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:H" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 7)));
                        wsReportNameHeaderRange.Value = "Salary Period Lock / UnLock Log)";
                    }
                    else
                    {
                        worksheet = workbook.Worksheets.Add("Open Month");
                        worksheet.Range("A1:G" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:G" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:G" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:G" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:G" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 6)));
                        wsReportNameHeaderRange.Value = "Salary Period Lock (Open Month)";

                    }
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                   

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Company Name";
                    worksheet.Cell(2, 2).Value = CompanyName.ToUpper();
                    worksheet.Cell(2, 3).Style.Font.Bold = true;
                    worksheet.Cell(2, 3).Value = "Month";
                    worksheet.Cell(2, 4).Value = MName;
                    worksheet.Cell(2, 4).Style.DateFormat.Format = "MMM-yyyy";
                    worksheet.Cell(2, 5).Style.Font.Bold = true;
                    worksheet.Cell(2, 5).Value = "Payroll Group";
                    worksheet.Cell(2, 6).Value = PName;
                   

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
                    worksheet.Cell(3, 6).Value = model.LockUnlock.Count();
                   

                    var currentRow = 4;
                    #region Header
                    if(Type == "LockUnlock")
                    worksheet.Range("A4:H4").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    else
                        worksheet.Range("A4:G4").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Cell(currentRow, 1).Value = "Sl.No";
                    worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 2).Value = "Month";
                    worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 3).Value = "PayRoll Group";
                    worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                
                    worksheet.Cell(currentRow, 4).Value = "Lock Type";
                    worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                    
                    if (Type == "LockUnlock")
                    {
                        worksheet.Cell(currentRow, 5).Value = "Lock/UnLock";
                        worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 6).Value = "User";
                        worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 7).Value = "Date&Time";
                        worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 8).Value = "Reason";
                        worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                    }
                    else
                    {
                      
                        worksheet.Cell(currentRow, 5).Value = "User";
                        worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 6).Value = "Date&Time";
                        worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 7).Value = "Reason";
                        worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                    }

                    #endregion

                    #region Body
                    foreach (var item in model.LockUnlock)
                    {
                        currentRow++;
                        slNo = slNo + 1;
                        worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 1).Value = slNo;
                        worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 2).Value = item.MonthName;
                        worksheet.Cell(currentRow, 2).Style.DateFormat.Format = "MMM-yyyy";
                        worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 3).Value = item.PGName;
                        worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 4).Value = item.LockType;
                        if (Type == "LockUnlock")
                        {
                        worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 5).Value = item.Lock;
                            worksheet.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 6).Value = item.User;
                            worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 7).Value = item.Date;
                            worksheet.Cell(currentRow, 7).Style.DateFormat.Format = "dd-MMM-yyyy";



                            worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 8).Value = item.Reason;
                            worksheet.Columns(1, 8).Style.Alignment.WrapText = true;
                            worksheet.Rows().Style.Alignment.WrapText = true;
                        }
                        else
                        {
                            worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 5).Value = item.User;
                            worksheet.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 6).Value = item.Date;
                            worksheet.Cell(currentRow, 6).Style.DateFormat.Format = "dd-MMM-yyyy";

                            worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 7).Value = item.Reason;
                            worksheet.Columns(1, 7).Style.Alignment.WrapText = true;
                            worksheet.Rows().Style.Alignment.WrapText = true;
                        }
                      
                    }
                    #endregion
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
                            book.Protect(EP.Password,false,false);
                            book.SaveToStream(stream);
                        }
                        var content = stream.ToArray();
                        return File(
                            content,
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            FileName+".xlsx"
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
