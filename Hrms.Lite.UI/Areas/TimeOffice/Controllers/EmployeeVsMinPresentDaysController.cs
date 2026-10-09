using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.TimeOffice;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice;
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

namespace Hrms.Lite.UI.Areas.TimeOffice.Controllers
{
    [Area("TimeOffice")]
    [SessionAuthorize]
  
    public class EmployeeVsMinPresentDaysController : Controller
    {

        private readonly IEmployeeVsMinPresentDaysService _service;
        private readonly ICommonService _commonService;
        private readonly IDropdownService _dropdownService;
        private readonly IHelpers _helpers;

        public EmployeeVsMinPresentDaysController(IEmployeeVsMinPresentDaysService service, ICommonService commonService, IDropdownService dropdownService, IHelpers helpers)
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
                EmployeeVsMinPresentDays model = new EmployeeVsMinPresentDays();
                Filter filter = new Filter();
                filter.length = 10;
                model = await _service.GetEmployeeVsMinPresentDays(0, filter);
                await fillfilterDropdown();
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
        public async Task<IActionResult> GetList(Filter filter1, int TabIndex)
        {
            try
            {
                EmployeeVsMinPresentDays model = new EmployeeVsMinPresentDays();


                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }
              
                    model = await _service.GetEmployeeVsMinPresentDays(TabIndex, filter1);

                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.EmployeeVsMinPresentDaysList == null || model.EmployeeVsMinPresentDaysList.Count() == 0)
                {


                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows

                    });
                }

                if (model.EmployeeVsMinPresentDaysList.Count > 0)
                {
                    totalrows = model.EmployeeVsMinPresentDaysList[0].active;
                }


                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = model.EmployeeVsMinPresentDaysList[0].totalRecords;

                    return Json(new
                    {
                        data = model.EmployeeVsMinPresentDaysList,
                        draw = filter1.draw,
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows

                    });
                }
                else
                {

                    return Json(new
                    {
                        data = model.EmployeeVsMinPresentDaysList,
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
   
        [HttpGet]
        public async Task<IActionResult> Save(Guid EmployeeGI, int PresentDays)
        {
            try
            {           
              
                var response = await _service.SaveEmployeeVsMinPresentDays(EmployeeGI, PresentDays);
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

        public async Task fillfilterDropdown()
        {

            ViewBag.Gen = new SelectList(await _dropdownService.GetGenderDropDown(), "Code", "Name");
            ViewBag.Cat = new SelectList(await _dropdownService.GetCategoryDropdown(), "Code", "Name");
            ViewBag.Grd = new SelectList(await _dropdownService.GetGradeDropdown(), "Code", "Name");
            ViewBag.Dsg = new SelectList(await _dropdownService.GetDesignationDropdown(), "Code", "Name");
            ViewBag.Dept = new SelectList(await _dropdownService.GetDepartmentDropdown(), "Code", "Name");
            ViewBag.Loc = new SelectList(await _dropdownService.GetLocationDropdown(), "Code", "Name");

            ViewBag.PayrollGrp = new SelectList(await _dropdownService.GetPayrollGroupDropdown(), "Code", "Name");

            ViewBag.division = new SelectList(await _dropdownService.GetDivisionDropdown(), "Code", "Name");
            ViewBag.section = new SelectList(await _dropdownService.GetSectionDropdown(), "Code", "Name");
            ViewBag.EmploymentType = new SelectList(await _dropdownService.GetEmploymentTypeDropdown(), "Code", "Name");
            ViewBag.EmploymentStatus = new SelectList(await _dropdownService.GetEmploymentStatusDropdown(), "Code", "Name");
            ViewBag.WorkLocation = new SelectList(await _dropdownService.GetWorkLocationDropdown(), "Code", "Name");
            ViewBag.WageGrade = new SelectList(await _dropdownService.GetWageGradeDropdown(), "Code", "Name");

        }


        public async Task<IActionResult> ExportToExcel(Filter filter, int _TabIndex, string TabIndex, int TotalRows)
        {
            var LoginUser = HttpContext.Session.GetString("LoginUser");
            var CompanyName = HttpContext.Session.GetString("CompanyName");
            var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
            var GroupCompanyName = User.authUser.GroupCompanyName;
            ExcelPassword EP = new ExcelPassword();
            EP = await _commonService.GetExcelPassword("Pay Sheet", "Excel");

            string FilterValues = await _helpers.GetFilterValues(filter);
            string currDateTime = await _helpers.GetCurrentDate();
            

            filter.length = TotalRows;
            filter.start = 0;

            var dataset = await _service.GetEmployeeVsMinPresentDays_Excel(_TabIndex, filter);
            DataTable dt = dataset.Tables[0];

            // Set EXCEL Column Headings
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (dt.Columns[i].ColumnName == "doj")
                    dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                else
                    dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                var worksheet = wb.Worksheets.Add("Employee Vs Present Days List");
                int rowcount = dt.Rows.Count + 4;
                string columnName = "";
                int Columncount = dt.Columns.Count;

                while (Columncount > 0)
                {
                    int modulo = (Columncount - 1) % 26;
                    columnName = Convert.ToChar('A' + modulo) + columnName;
                    Columncount = (Columncount - modulo) / 26;
                }

                string colName = columnName;

                worksheet.Range("A1", colName + rowcount).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1", colName + rowcount).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1", colName + rowcount).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1", colName + rowcount).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1", colName + rowcount).Style.Border.LeftBorder = XLBorderStyleValues.Thin;

                var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, colName));
                wsReportNameHeaderRange.Style.Font.Bold = true;
                wsReportNameHeaderRange.Style.Font.FontSize = 15;
                wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                wsReportNameHeaderRange.Merge();
                wsReportNameHeaderRange.Value = "Employee Vs Present Days List";

                worksheet.Cell(2, 1).Style.Font.Bold = true;
                worksheet.Cell(2, 1).Value = "Group Company Name";
                worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();

                worksheet.Cell(2, 3).Style.Font.Bold = true;
                worksheet.Cell(2, 3).Value = "TabIndex";
                worksheet.Cell(2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(2, 4).Value = TabIndex;

                worksheet.Cell(3, 1).Style.Font.Bold = true;
                worksheet.Cell(3, 1).Value = "Login User";
                worksheet.Cell(3, 2).Value = LoginUser.ToString();
                worksheet.Cell(3, 3).Style.Font.Bold = true;
                worksheet.Cell(3, 3).Value = "Date & Time";
                worksheet.Cell(3, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(3, 4).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
                worksheet.Cell(3, 4).Value = currDateTime.ToString();
                worksheet.Cell(3, 5).Style.Font.Bold = true;
                worksheet.Cell(3, 5).Value = "Total Count";
                worksheet.Cell(3, 6).Value = dt.Rows.Count;

                worksheet.Cell(4, 1).Style.Font.Bold = true;
                worksheet.Cell(4, 1).Value = "Filter Applied";
                worksheet.Range("B4:" + colName + "4").Merge();
                worksheet.Cell("B4").Value = FilterValues;

                var currentRow = 5;

                // Insert Table and Remove Default Formatting
                var insertedTable = worksheet.Cell(currentRow, 1).InsertTable(dt);
                insertedTable.ShowAutoFilter = false;
                insertedTable.Theme = XLTableTheme.None;

                // ✅ NEW: Apply borders to each cell in the data range (including header)
                int dataRowCount = dt.Rows.Count + 1; // +1 for header row
                int dataColCount = dt.Columns.Count;

                var dataRange = worksheet.Range(currentRow, 1, currentRow + dataRowCount - 1, dataColCount);
                dataRange.Style.Border.TopBorder = XLBorderStyleValues.Thin;
                dataRange.Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                dataRange.Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                dataRange.Style.Border.RightBorder = XLBorderStyleValues.Thin;

                worksheet.Range("A5:" + colName + "5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                worksheet.Range("A5:" + colName + "5").Style.Font.Bold = true;

                worksheet.Columns().AdjustToContents();
                worksheet.Rows().AdjustToContents();

                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    if (EP.NeedPassword)
                    {
                        Workbook book = new Workbook();
                        book.LoadFromStream(stream);
                        book.Protect(EP.Password, false, false);
                        book.SaveToStream(stream);
                    }

                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Employee Vs Present Days List.xlsx");
                }
            }
        }






    }
}
