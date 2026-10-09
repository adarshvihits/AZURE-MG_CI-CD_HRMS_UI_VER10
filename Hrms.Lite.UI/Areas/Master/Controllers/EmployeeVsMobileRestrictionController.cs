using Microsoft.AspNetCore.Mvc;
using Hrms.Lite.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Services.IServices.Master;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Hrms.Lite.Shared.Account;
using System.Data;
using ClosedXML.Excel;
using System.IO;
using Spire.Xls;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hrms.Lite.UI.Areas.Master.Controllers
{
    [Area("Master")]
    [SessionAuthorize]
    public class EmployeeVsMobileRestrictionController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly ICommonService _commonService;
        private readonly IEmployeeVsMobileRestrictionService _employeeVsMobileRestrictionService;
        private readonly IHelpers _helpers;
        public EmployeeVsMobileRestrictionController(IDropdownService dropdownService, ICommonService commonService, IEmployeeVsMobileRestrictionService employeeVsMobileRestrictionService, IHelpers helpers)
        {
            _dropdownService = dropdownService;
            _commonService = commonService;
            _employeeVsMobileRestrictionService = employeeVsMobileRestrictionService;
            _helpers = helpers;
        }
        public async Task<IActionResult> Index()
        {

            try
            {
                EmployeeVsMobileRestriction model = new EmployeeVsMobileRestriction();
                model.filter = new Filter();
                model = await _employeeVsMobileRestrictionService.GetList(0, model.filter);
                await fillfilterDropdown();
                model.Tabindex = 0;
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

        [HttpPost]
        public async Task<IActionResult> GetList(int TabIndex, Filter filter)

        {
            try
            {
                EmployeeVsMobileRestriction model = new EmployeeVsMobileRestriction();
                
                if (filter.searchValue == "undefined" || filter.searchValue == null)
                {
                    filter.searchValue = "";
                }
                model = await _employeeVsMobileRestrictionService.GetList(TabIndex, filter);
                model.Tabindex = TabIndex;
                if (TabIndex == 0)
                    return PartialView("_AllList", model);
                else
                    return PartialView("_RestrictedEmpList", model);

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
        public async Task<IActionResult> RestrictedEmpSave(EmployeeVsMobileRestriction model)
        {
            try
            {
                var response = new object();

                response = await _employeeVsMobileRestrictionService.RestrictedEmpSave(model);
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

        public async Task<IActionResult> ExportToExcel(int TabIndex, Filter filter)
        {
            var LoginUser = HttpContext.Session.GetString("LoginUser");
            var CompanyName = HttpContext.Session.GetString("CompanyName");
            var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
            var GroupCompanyName = User.authUser.GroupCompanyName;
            ExcelPassword EP = new ExcelPassword();
            EP = await _commonService.GetExcelPassword("Location Marking Applicable Employees", "Excel");

            string currDateTime = await _helpers.GetCurrentDate();
            

            EmployeeVsMobileRestriction model = new EmployeeVsMobileRestriction();
            filter.length = model.RestrictedEmpCount;
            var dataset = await _employeeVsMobileRestrictionService.GetRestrictedEmp_Excel(TabIndex, filter);

            DataTable dt = dataset.Tables[0];
            DataTable dt1 = dataset.Tables[1];

            //set EXCEL Column Heading 
            for (int i = 0; i < dt1.Columns.Count; i++)
            {

                dt1.Columns[i].ColumnName = char.ToUpper(dt1.Columns[i].ColumnName[0]) + dt1.Columns[i].ColumnName.Substring(1);
            }


            using (XLWorkbook wb = new XLWorkbook())
            {


                var worksheet = wb.Worksheets.Add("Employee vs Mobile Restriction");
                int rowcount = dt1.Rows.Count + 4;
                string columnName = "";
                int Columncount = dt1.Columns.Count;

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



                var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 5)));
                wsReportNameHeaderRange.Style.Font.Bold = true;
                wsReportNameHeaderRange.Style.Font.FontSize = 15;
                wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);

                wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                wsReportNameHeaderRange.Merge();
                wsReportNameHeaderRange.Value = "Mobile App Restricted Employees";

                worksheet.Cell(2, 1).Style.Font.Bold = true;
                worksheet.Cell(2, 1).Value = "Group Company Name";
                worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();

                worksheet.Cell(2, 3).Style.Font.Bold = true;
                worksheet.Cell(2, 3).Value = "Login User";
                worksheet.Cell(2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(2, 4).Value = LoginUser.ToString();


                worksheet.Cell(2, 5).Style.Font.Bold = true;
                worksheet.Cell(2, 5).Value = "Date & Time";
                worksheet.Cell(2, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(2, 6).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
                worksheet.Cell(2, 6).Value = currDateTime.ToString();
                worksheet.Cell(3, 1).Style.Font.Bold = true;
                worksheet.Cell(3, 1).Value = "Total Count";
                worksheet.Cell(3, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(3, 2).Value = dt1.Rows.Count;


                var currentRow = 4;

                worksheet.Cell(currentRow, 1).InsertTable(dt1);
                worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                worksheet.Range("A4:" + colName + "4").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                worksheet.Range("A4:" + colName + "4").Style.Font.Bold = true;

                worksheet.Columns().AdjustToContents();
                worksheet.Rows().AdjustToContents();
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    if (EP.NeedPassword)
                    {
                        Workbook book = new Workbook();
                        book.LoadFromStream(stream);
                        //Protect Workbook
                        book.Protect(EP.Password, false, false);
                        book.SaveToStream(stream);
                    }
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Mobile App Restricted Employees.xlsx");
                }
            }
        }
    }
}
