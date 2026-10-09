using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.DataBank;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Hrms.Lite.Infrastructure.Helpers;
using System.Collections.Generic;
using Hrms.Lite.Shared.General;
using Microsoft.AspNetCore.Http;
using Hrms.Lite.Shared.Account;
using System.Data;
using ClosedXML.Excel;
using System.Linq;
using System.IO;
using Spire.Xls;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hrms.Lite.UI.Areas.DataBank.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]
    public class MailAlertSettingsController : Controller
    {
        private readonly IMailAlertSettingsService _service;
        private readonly ICommonService _commonService;
        private readonly IDropdownService _dropdownService;
        private readonly IHelpers _helpers;
        public MailAlertSettingsController(IMailAlertSettingsService service, IDropdownService dropdownService, ICommonService commonService, IHelpers Helpers)
        {
            _service = service;
            _commonService = commonService;
            _dropdownService = dropdownService;
            _helpers = Helpers;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                MailAlertSettings model = new MailAlertSettings();
                model.Filter = await _commonService.GetFilterDropdowns();
                ViewBag.MailAlertType = new SelectList(await _dropdownService.GetMailAlertTypeDropDown(), "Code", "Name");
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



        [HttpPost]

        public async Task<IActionResult> GetMailAlertSettingsList(Filter filter1, int TypeCode, MailAlertSettings _model)
        {
            {
                try
                {
                    MailAlertSettings model = new MailAlertSettings();

                    if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                    {
                        filter1.searchValue = "";
                    }

                    model = await _service.GetList(TypeCode, filter1);
                    if (_model.MailAlertSettingsList != null && _model.MailAlertSettingsList.Count != 0)
                    {
                        for (int i = 0; i < model.MailAlertSettingsList.Count; i++)
                        {
                            for (int j = 0; j < _model.MailAlertSettingsList.Count; j++)
                            {
                                if (model.MailAlertSettingsList[i].EmployeeCode == _model.MailAlertSettingsList[j].EmployeeCode)
                                {

                                    model.MailAlertSettingsList[i].Applicable = true;

                                }
                            }
                        }
                    }
                    if (_model.MailAlertSettingsUncheckList != null && _model.MailAlertSettingsUncheckList.Count != 0)
                    {
                        for (int i = 0; i < model.MailAlertSettingsList.Count; i++)
                        {
                            for (int j = 0; j < _model.MailAlertSettingsUncheckList.Count; j++)
                            {
                                if (model.MailAlertSettingsList[i].EmployeeCode == _model.MailAlertSettingsUncheckList[j].EmployeeCode)
                                {

                                    model.MailAlertSettingsList[i].Applicable = false;

                                }
                            }
                        }
                    }
                    int totalrows = 0;
                    int totalrowsafterfiltering = 0;

                    if (model.MailAlertSettingsList == null || model.MailAlertSettingsList.Count == 0)
                    {

                        return Json(new
                        {
                            data = model.ActiveCount,

                            draw = filter1.draw,
                            recordsTotal = totalrows,
                            recordsFiltered = totalrows
                        });
                    }

                    if (model.MailAlertSettingsList.Count > 0)
                    {

                        if (filter1.Tabindex == 0)
                        {
                            totalrows = model.MailAlertSettingsList[0].active;
                        }
                        else
                        {
                            totalrows = model.MailAlertSettingsList[0].active;
                        }
                    }

                    if (filter1.searchValue != "")
                    {
                        totalrowsafterfiltering = model.MailAlertSettingsList[0].totalRecords;
                        return Json(new
                        {
                            data = model.MailAlertSettingsList,
                            draw = filter1.draw,
                            recordsTotal = totalrows,
                            recordsFiltered = totalrows
                        });
                    }
                    else
                    {
                        return Json(new
                        {
                            data = model.MailAlertSettingsList,
                            draw = filter1.draw,
                            recordsTotal = totalrows,
                            recordsFiltered = totalrows
                        });
                    }


                }
                catch (Exception ex)
                {
                    return View();
                }
            }
        }

       

        public async Task<ActionResult> Save(MailAlertSettings input)
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


        

        public async Task<IActionResult> ExportToExcel(int TypeCode,  int TotalRows, Filter Filter)
        {
            try
            {

                Filter.length = TotalRows;
                Filter.start = 0;

                string FilterValues = await _helpers.GetFilterValues(Filter);

                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("MailAlertSettingsList ", "Excel");

                if (FilterValues == "")
                {
                    FilterValues = "All";
                }

                string currDateTime = await _helpers.GetCurrentDate();
                

                var dataset = await _service.GetList_Excel(TypeCode, Filter);
                DataTable dt = dataset.Tables[0];
                if ((dataset.Tables[0]?.Rows?.Count ?? 0) > 0)
                {
                    dt = dataset.Tables[0];
                }

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                }

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("MailAlertSettings");
                    int count = dt.Rows.Count + 5;

                    string columnName = "";
                    int Columncount = dt.Columns.Count;

                    while (Columncount > 0)
                    {
                        int modulo = (Columncount - 1) % 26;
                        columnName = Convert.ToChar('A' + modulo) + columnName;
                        Columncount = (Columncount - modulo) / 26;
                    }

                    string colName = columnName;


                    worksheet.Range("A1", colName + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1", colName + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1", colName + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1", colName + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1", colName + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, colName));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                    wsReportNameHeaderRange.Value = "MailAlertSettings List  ";


                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
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
                    worksheet.Cell(3, 6).Value = dt.Rows.Count;
                    worksheet.Cell(4, 1).Style.Font.Bold = true;
                    worksheet.Cell(4, 1).Value = "Filter Applied";
                    worksheet.Range("B4", colName + 4).Merge();
                    worksheet.Cell("B4").Value = FilterValues;

                    var currentRow = 5;

                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A5", colName + 5).Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A5", colName + 5).Style.Font.Bold = true;

                    worksheet.Columns().AdjustToContents();
                    worksheet.Rows().AdjustToContents();
                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        if (EP.NeedPassword)
                        {
                            Workbook book = new Workbook();
                            book.LoadFromStream(stream);
                            
                            book.Protect(EP.Password, false, false);
                            book.SaveToStream(stream);
                        }
                        var content = stream.ToArray();
                        return File(
                            content,
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            "MailAlertSettings List.xlsx"
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

