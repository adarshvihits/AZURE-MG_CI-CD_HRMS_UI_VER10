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
    public class YearlyOffdaySettingsController : Controller
    {
        public readonly IYearlyOffdaySettingsService _service;
        private readonly IDropdownService _dropdownService;
        private readonly IHelpers _helpers;
        private readonly ICommonService _commonService;
        public YearlyOffdaySettingsController(IYearlyOffdaySettingsService service, IDropdownService dropdownService, ICommonService commonService, IHelpers helpers)
        {
            _service = service;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                YearlyOffdaySettings model = new YearlyOffdaySettings();
            ViewBag.Year = new SelectList(await _dropdownService.GetCalendarYearDropdown(), "Code", "Name");
            model = await _service.GetYearlyOffdayList();
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
        public async Task<IActionResult> Edit(Guid OffdayGI)
        {
            try
            {


                ViewBag.Year = new SelectList(await _dropdownService.GetCalendarYearDropdown(), "Code", "Name");
                YearlyOffdaySettings model = new YearlyOffdaySettings();


                model = await _service.GetDetails(OffdayGI);
                model.mode = "Modify";
                return PartialView("_AddNewOrEdit", model);

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
        public async Task<IActionResult> Delete(Guid OffdaySettingsGI)
        {
            try
            {
                YearlyOffdaySettings model = new YearlyOffdaySettings();
                model = await _service.GetDetails(OffdaySettingsGI);
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
        public async Task<IActionResult> Delete(YearlyOffdaySettings input)
        {
            try
            {

                YearlyOffdaySettings model = new YearlyOffdaySettings();
                var response = await _service.Delete(input);
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
        public async Task<IActionResult> Create()
        {
            try
            {
                YearlyOffdaySettings model = new YearlyOffdaySettings();
                ViewBag.Year = new SelectList(await _dropdownService.GetCalendarYearDropdown(), "Code", "Name");
                model = await _service.GetDetails(model.OffdaySettingsGI);
                model.mode = "Create";
                return PartialView("_AddNewOrEdit", model);
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
        public async Task<IActionResult>GetDays(int Year,YearlyOffdaySettings model)
        {
            try
            {
                var list = from li in model.Days
                           where li.Applicability == true
                           select li;
                model.Days = list.ToList();
                model = await _service.GetDaysList(Year, model);
                return PartialView("_DaysGrid", model);
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
        public async Task<IActionResult>Save(YearlyOffdaySettings model)
       
        {
            try
            {
              
                var list = from li in model.Days
                           where li.Applicability == true
                           select li;
                model.Days = list.ToList();
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

        public async Task<IActionResult> ExportToExcel()
        {
            YearlyOffdaySettings model = new YearlyOffdaySettings();
            model = await _service.GetYearlyOffdayList();
            int slno = 0;
            string currDateTime = await _helpers.GetCurrentDate();
            
            var LoginUser = HttpContext.Session.GetString("LoginUser");
            var CompanyName = HttpContext.Session.GetString("CompanyName");
            ExcelPassword EP = new ExcelPassword();
           
            EP = await _commonService.GetExcelPassword("YearlyOffdaySettings", "Excel");

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("YearlyOffDayList");
                int PendingCountnum = model.List.Count + 6;


                worksheet.Range("A1:C" + PendingCountnum).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1:C" + PendingCountnum).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1:C" + PendingCountnum).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1:C" + PendingCountnum).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1:C" + PendingCountnum).Style.Border.LeftBorder = XLBorderStyleValues.Thin;



                var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 2)));
                wsReportNameHeaderRange.Style.Font.Bold = true;
                wsReportNameHeaderRange.Style.Font.FontSize = 15;
                wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                wsReportNameHeaderRange.Merge();


                wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                wsReportNameHeaderRange.Style.Font.FontSize = 15;
                wsReportNameHeaderRange.Value = "Yearly Offday Settings ";
                worksheet.Range("A6:C6").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
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
                worksheet.Cell(currentRow, 2).Value = "Year";
                worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 3).Value = "Combination Name";
               


                #endregion

                #region Body
                foreach (var item in model.List)
                {
                    currentRow++;
                    slno = slno + 1;
                    worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 1).Value = slno;
                    worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 2).Value = item.Year.Name;
                    worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 3).Value = item.CombinationName;
                    worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                  
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
                        "Yearly Offday Settings List.xlsx"
                        );
                }
            }
        }

        }
    }
