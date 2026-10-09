using Hrms.Lite.Services.IServices.General;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Shared.General;
using Microsoft.AspNetCore.Http;
using Hrms.Lite.Shared.Account;
using System.Data;
using ClosedXML.Excel;
using System.Linq;
using System.IO;
using Spire.Xls;
using Newtonsoft.Json;
using Hrms.Lite.Services.IServices.DataBank.Reports;
using Hrms.Lite.Shared.DataBank.Reports;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hrms.Lite.UI.Areas.DataBank.Reports.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]
    public class ParentsMediclaimRequestReportController : Controller
    {
        private readonly IParentsMediclaimRequestReportService _service;
        private readonly ICommonService _commonService;
        private readonly IDropdownService _dropdownService;
        private readonly IHelpers _helpers;
        public ParentsMediclaimRequestReportController(IParentsMediclaimRequestReportService service, IDropdownService dropdownService, ICommonService commonService, IHelpers Helpers)
        {
            _service = service;
            _commonService = commonService;
            _helpers = Helpers;
            _dropdownService = dropdownService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                ParentsMediclaimRequestReport model = new ParentsMediclaimRequestReport();
                Filter filter = new Filter();
                filter.length = 10;
                model = await _service.GetList(0, filter);
                ViewBag.FinancialYear = new SelectList(await _dropdownService.GetFinancialYearDropdown(), "Code", "Name");
                model.Filter = await _commonService.GetFilterDropdowns();
                return View("~/Areas/DataBank/Reports/Views/ParentsMediclaimRequestReport/Index.cshtml", model);
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

        public async Task<IActionResult> GetParentsMediclaimRequestReportList(int FinancialYearcode, Filter filter1)
        {
            {
                try
                {
                    ParentsMediclaimRequestReport model = new ParentsMediclaimRequestReport();

                    if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                    {
                        filter1.searchValue = "";
                    }

                    model = await _service.GetList(FinancialYearcode, filter1);

                    int totalrows = 0;
                    int totalrowsafterfiltering = 0;

                    if (model.ParentsMediclaimRequestReportList == null || model.ParentsMediclaimRequestReportList.Count == 0)
                    {

                        return Json(new
                        {
                            data = model.ActiveCount,

                            draw = filter1.draw,
                            recordsTotal = totalrows,
                            recordsFiltered = totalrows
                        });
                    }

                    if (model.ParentsMediclaimRequestReportList.Count > 0)
                    {

                        if (filter1.Tabindex == 0)
                        {
                            totalrows = model.ParentsMediclaimRequestReportList[0].active;
                        }
                        else
                        {
                            totalrows = model.ParentsMediclaimRequestReportList[0].active;
                        }
                    }

                    if (filter1.searchValue != "")
                    {
                        totalrowsafterfiltering = model.ParentsMediclaimRequestReportList[0].totalRecords;
                        return Json(new
                        {
                            data = model.ParentsMediclaimRequestReportList,
                            draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                            recordsTotal = totalrows,
                            recordsFiltered = totalrows//totalrowsafterfiltering
                        });
                    }
                    else
                    {
                        return Json(new
                        {
                            data = model.ParentsMediclaimRequestReportList,
                            draw = filter1.draw,//Request.Form["draw"], 
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


        public async Task<IActionResult> ExportToExcel(int FinancialYearcode, string currentTabName, int TotalRows, Filter Filter)
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
                EP = await _commonService.GetExcelPassword("Parents Mediclaim Request Report", "Excel");

                if (FilterValues == "")
                {
                    FilterValues = "All";
                }

                string currDateTime = await _helpers.GetCurrentDate();
                

                var dataset = await _service.GetList_Excel(FinancialYearcode, Filter);
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
                    var worksheet = workbook.Worksheets.Add("Parents Mediclaim Report");
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
                    wsReportNameHeaderRange.Value = "Parents Mediclaim Request Report";


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
                            //Protect Workbook
                            book.Protect(EP.Password, false, false);
                            book.SaveToStream(stream);
                        }
                        var content = stream.ToArray();
                        return File(
                            content,
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            "Parents Mediclaim Request Report.xlsx"
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



