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
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.Master.Controllers
{
    [Area("Master")]
    [SessionAuthorize]
    public class RoundOffSettingsController : Controller
    {
        private readonly IRoundOffSettingsService _service;
        private readonly IDropdownService _dropdownService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        public RoundOffSettingsController(IRoundOffSettingsService service, IDropdownService dropdownService, ICommonService commonService, IHelpers helpers)
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
                RoundOffSettings model = new RoundOffSettings();
                model = await _service.GetRoundOffSettings("ALLOWANCE");
                ViewBag.RoundOff = new SelectList(await _dropdownService.GetRoundOffDropDown(), "Code", "Name");
                model.TabName = "ALLOWANCE";
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
        public async Task<ActionResult> GetList(string TabIndex)
        {
            try
            {
                RoundOffSettings model = new RoundOffSettings();
               
                    model = await _service.GetRoundOffSettings(TabIndex);
                    ViewBag.RoundOff = new SelectList(await _dropdownService.GetRoundOffDropDown(), "Code", "Name");
                    model.TabName = TabIndex;
                    return PartialView("_RoudoffSettingsList", model);
          

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
        public async Task<ActionResult> Save(RoundOffSettings input, string TabName)
        {
            try
            {
                object response = new object();
                input.TabName = TabName;
                response = await _service.Create(input);

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
        //public async Task<IActionResult> ExportToExcel(string TabIndex)
        //{
        //    try
        //    {


        //        var LoginUser = HttpContext.Session.GetString("LoginUser");
        //        var CompanyName = HttpContext.Session.GetString("CompanyName");
        //        ExcelPassword EP = new ExcelPassword();
        //        EP = await _commonService.GetExcelPassword("Round Off Settings (" + TabIndex + ")", "Excel");


        //        string currDateTime = await _helpers.GetCurrentDate();
        //        

        //        //Get Datset

        //        {
        //            var dataset = await _service.GetExcel(TabIndex);
        //            DataTable dt = dataset.Tables[0];


        //            //set EXCEL Column Heading 
        //            for (int i = 0; i < dt.Columns.Count; i++)
        //            {
        //                    dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
        //            }

        //            using (var workbook = new XLWorkbook())
        //            {
        //                var worksheet = workbook.Worksheets.Add("RoundOffSettings");
        //                int count = dt.Rows.Count + 5;


        //                worksheet.Range("A1:B" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        //                worksheet.Range("A1:B" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
        //                worksheet.Range("A1:B" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
        //                worksheet.Range("A1:B" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
        //                worksheet.Range("A1:B" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
        //                var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 4)));
        //                wsReportNameHeaderRange.Style.Font.Bold = true;
        //                wsReportNameHeaderRange.Style.Font.FontSize = 15;
        //                wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        //                wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
        //                wsReportNameHeaderRange.Merge();
        //                wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
        //                wsReportNameHeaderRange.Style.Alignment.SetWrapText();
        //                wsReportNameHeaderRange.Value = "Round Off Settings (" + TabIndex + ")";

        //                worksheet.Cell(2, 1).Style.Font.Bold = true;
        //                worksheet.Cell(2, 1).Value = "Company Name";
        //                worksheet.Cell(2, 2).Value = CompanyName.ToUpper();
        //                worksheet.Cell(3, 1).Style.Font.Bold = true;
        //                worksheet.Cell(3, 1).Value = "Login User";
        //                worksheet.Cell(3, 2).Value = LoginUser;
        //                worksheet.Cell(3, 3).Style.Font.Bold = true;
        //                worksheet.Cell(3, 3).Value = "Date & Time";
        //                worksheet.Cell(3, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                worksheet.Cell(3, 4).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
        //                worksheet.Cell(3, 4).Value = currDateTime.ToString();
        //                worksheet.Cell(4, 1).Style.Font.Bold = true;
        //                worksheet.Cell(4, 1).Value = "Total Count";
        //                worksheet.Cell(4, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
        //                worksheet.Cell(4, 2).Value = dt.Rows.Count;




        //                var currentRow = 5;

        //                worksheet.Cell(currentRow, 1).InsertTable(dt);
        //                worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

        //                worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
        //                worksheet.Range("A5:B5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
        //                worksheet.Range("A5:B5").Style.Font.Bold = true;


        //                worksheet.Columns().AdjustToContents();
        //                worksheet.Rows().AdjustToContents();
        //                using (var stream = new MemoryStream())
        //                {
        //                    workbook.SaveAs(stream);
        //                    if (EP.NeedPassword)
        //                    {
        //                        Workbook book = new Workbook();
        //                        book.LoadFromStream(stream);
        //                        //Protect Workbook
        //                        book.Protect(EP.Password, false, false);
        //                        book.SaveToStream(stream);
        //                    }
        //                    var content = stream.ToArray();
        //                    return File(
        //                        content,
        //                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
        //                        "RoundOffSettings.xlsx"
        //                        );
        //                }
        //            }

        //        }


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
        public async Task<IActionResult> ExportToExcel(string TabIndex)
        {
            try
            {
                RoundOffSettings model = new RoundOffSettings();
                model = await _service.GetRoundOffSettings(TabIndex);
                int slno = 0;
                string currDateTime = await _helpers.GetCurrentDate();
                
                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Round Off Settings", "Excel");


                {

                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("Round Off Settings");
                        int count = model.Details.Count + 6;


                        worksheet.Range("A1:D" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:D" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:D" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:D" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:D" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                        var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 3)));
                        wsReportNameHeaderRange.Style.Font.Bold = true;
                        wsReportNameHeaderRange.Style.Font.FontSize = 15;
                        wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        wsReportNameHeaderRange.Merge();
                        wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                        wsReportNameHeaderRange.Value = "ROUND OFF SETTINGS :" + "(" + TabIndex + ")";
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
                        worksheet.Cell(5, 2).Value = model.Details.Count;

                        var currentRow = 6;
                        #region Header
                        worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 1).Value = "Sl.No";
                        worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 2).Value = "Description";
                        worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 3).Value = "Type";
                        worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 4).Value = "Round Off";
                        #endregion

                        #region Body
                        foreach (var item in model.Details)
                        {
                            currentRow++;
                            slno = slno + 1;
                            worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 1).Value = slno;
                            worksheet.Cell(currentRow, 2).Value = item.ParameterName;
                            worksheet.Cell(currentRow, 3).Value = item.Type;
                            worksheet.Cell(currentRow, 4).Value = item.RoundOffName;
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
                               "Round Off Settings.xlsx"
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
