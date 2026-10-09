using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.TDS.Reports;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TDS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.TDS.Reports.Controllers
{
    [Area("TDS")]
    [SessionAuthorize]
    public class EmployeePerquisitesReportController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly IEmployeePerquisitesReportService _service;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        public EmployeePerquisitesReportController(IEmployeePerquisitesReportService _EmployeePerquisitesReportService, ICommonService commonService, IDropdownService dropdownService, IHelpers Helpers)
        {
            _service = _EmployeePerquisitesReportService;
            _commonService = commonService;
            _dropdownService = dropdownService;
            _helpers = Helpers;
        }
        public async Task<IActionResult> Index()
        {
            EmployeePerquisitesReport model = new EmployeePerquisitesReport();
            ViewBag.AssessmentYear = new SelectList(await _dropdownService.GetAssessmentYearDropdown(), "Code", "Name");
            ViewBag.month = new SelectList(await _dropdownService.GetMonthDropdown(), "Code", "Name");
            ViewBag.Perquisite = new SelectList(await _dropdownService.GetPerquisiteDropdown(), "Code", "Name");
            model.filter =await _commonService.GetFilterDropdowns();
            return View("~/Areas/TDS/Reports/Views/EmployeePerquisitesReport/Index.cshtml", model);
        }
        [HttpGet]
        public async Task<JsonResult> GetMonthDropDown(int AssessmentYearCode)
        {
            SelectList data = new SelectList("", "Code", "Name");
            data = new SelectList(await _dropdownService.GetTDSAssessmentYearMonthDropdown(AssessmentYearCode), "Code", "Name");
            return Json(data);
        }
        [HttpPost]
        public async Task<IActionResult> GetList(Filter filter1, int AssessmentYear, int Month, int PerquisiteNature)
        {
            try
            {
                EmployeePerquisitesReport model = new EmployeePerquisitesReport();
                Filter filter = new Filter();
                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }
                if (AssessmentYear != 0)
                {
                    model.EmployeePerquisitesReportList = await _service.GetList(AssessmentYear, Month, PerquisiteNature, filter1);
                }

                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model == null || model.EmployeePerquisitesReportList == null || model.EmployeePerquisitesReportList.Count() == 0)
                {
                    model.EmployeePerquisitesReportList = new List<EmployeePerquisitesReport>();
                }
                if (model.EmployeePerquisitesReportList == null || model.EmployeePerquisitesReportList.Count() == 0)
                {
                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }

                if (model.EmployeePerquisitesReportList.Count > 0)
                {
                    totalrows = model.EmployeePerquisitesReportList[0].active;
                }
                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = model.EmployeePerquisitesReportList[0].totalRecords;
                    return Json(new
                    {
                        data = model.EmployeePerquisitesReportList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    return Json(new
                    {
                        data = model.EmployeePerquisitesReportList,
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
        public async Task<IActionResult> ExportToExcel(Filter filter, int AssessmentYear, string AssessmentYearName, int Month, string MName, int Perquisite, string PerquisiteName, int Totalrow)
        {
            try
            {
                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;


                filter.length = Totalrow;

                string FilterValues = await _helpers.GetFilterValues(filter);

                var dataset = await _service.GetList_Excel(AssessmentYear, Month, Perquisite, filter);
                DataTable dt = dataset.Tables[0];
                for (int i = 0; i < dt.Columns.Count; i++)
                {

                    dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                }

                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Employee Perquisites Report (" + AssessmentYearName + ")", "Excel");

                if (FilterValues == "")
                {
                    FilterValues = "All";
                }
                string currDateTime = await _helpers.GetCurrentDate();
                
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Employee Perquisites Report");
                    int count = dt.Rows.Count + 5;


                    worksheet.Range("A1:H" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:H" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:H" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:H" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:H" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 7)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Value = "Employee Perquisites Report";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                    worksheet.Cell(2, 3).Style.Font.Bold = true;
                    worksheet.Cell(2, 3).Value = "Assessment Year";
                    worksheet.Cell(2, 4).Value = AssessmentYearName;
                    worksheet.Cell(2, 5).Style.Font.Bold = true;
                    worksheet.Cell(2, 5).Value = "Month";

                    worksheet.Cell(2, 6).Value = MName;
                    worksheet.Cell(2, 6).Style.DateFormat.Format = "MMM-yyyy";
                    worksheet.Cell(2, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(2, 7).Style.Font.Bold = true;
                    worksheet.Cell(2, 7).Value = "Perquisite";
                    worksheet.Cell(2, 8).Value = PerquisiteName;

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

                    var filterapplied = worksheet.Range("B4:H4").Merge();

                    filterapplied.Value = FilterValues;

                    var currentRow = 5;
                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;
                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A5:H5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A5:H5").Style.Font.Bold = true;



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
                            "Employee Perquisites Report.xlsx"
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
