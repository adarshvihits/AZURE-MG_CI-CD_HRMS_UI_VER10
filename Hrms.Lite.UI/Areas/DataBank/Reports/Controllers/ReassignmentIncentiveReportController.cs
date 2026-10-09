using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank.Reports;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
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



namespace Hrms.Lite.UI.Areas.DataBank.Reports.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]
    public class ReassignmentIncentiveReportController : Controller
    {
        private readonly IReassignmentIncentiveReportService _reassignmentIncentiveReportService;
        private readonly IDropdownService _dropdownService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        public ReassignmentIncentiveReportController(IReassignmentIncentiveReportService reassignmentMovementReportService, IDropdownService dropdownService, ICommonService commonService, IHelpers helpers)
        {
            _reassignmentIncentiveReportService = reassignmentMovementReportService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                await fillfilterDropdown();
                ReassignmentIncentiveReport model = new ReassignmentIncentiveReport();
                model.DateFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                model.DateTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1);


                return View("~/Areas/DataBank/Reports/Views/ReassignmentIncentiveReport/Index.cshtml", model);
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
            ViewBag.Dsg = new SelectList(await _dropdownService.GetDesignationDropdown(), "Code", "Name");
            ViewBag.Dept = new SelectList(await _dropdownService.GetDepartmentDropdown(), "Code", "Name");
            ViewBag.Loc = new SelectList(await _dropdownService.GetLocationDropdown(), "Code", "Name");
            ViewBag.Cat = new SelectList(await _dropdownService.GetCategoryDropdown(), "Code", "Name");
            ViewBag.Grd = new SelectList(await _dropdownService.GetGradeDropdown(), "Code", "Name");
            ViewBag.PayrollGrp = new SelectList(await _dropdownService.GetPayrollGroupDropdown(), "Code", "Name");
            ViewBag.division = new SelectList(await _dropdownService.GetDivisionDropdown(), "Code", "Name");
            ViewBag.section = new SelectList(await _dropdownService.GetSectionDropdown(), "Code", "Name");
            ViewBag.EmploymentType = new SelectList(await _dropdownService.GetEmploymentTypeDropdown(), "Code", "Name");
            ViewBag.WorkLocation = new SelectList(await _dropdownService.GetWorkLocationDropdown(), "Code", "Name");
            ViewBag.WageGrade = new SelectList(await _dropdownService.GetWageGradeDropdown(), "Code", "Name");
            ViewBag.AttendanceStatus = new SelectList(await _dropdownService.GetAttendanceStatusDropdown(), "Code", "Name");
            ViewBag.GroupCompany = new SelectList(await _dropdownService.GetUserVsGroupCompanyDropDown(), "Code", "Name");
            ViewBag.TransitionType = new SelectList(await _dropdownService.GetTransitionTypeDropdown(), "Code", "Name");
        }
        public async Task<IActionResult> ExportToexcel(ReassignmentIncentiveReport model)
        {
            try
            {


                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Reassignment Report-For Incentive", "Excel");


                string currDateTime = await _helpers.GetCurrentDate();
                


                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Reassignment Incentive Report");
                    var ds = await _reassignmentIncentiveReportService.GetDetails(model);
                    DataTable dt = ds.Tables[0];

                    var columnMappings = new Dictionary<string, string>
                    {
                            { "id", "Employee ID" },
                            { "employee Name", "Employee Name" }

                    };

                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        var columnName_ = dt.Columns[i].ColumnName;

                        if (columnMappings.ContainsKey(columnName_))
                        {
                            dt.Columns[i].ColumnName = columnMappings[columnName_];
                        }
                        else if (columnName_ == "dob" || columnName_ == "doj" || columnName_ == "dow")
                        {
                            dt.Columns[i].ColumnName = columnName_.ToUpper();
                        }
                        else
                        {
                            dt.Columns[i].ColumnName = char.ToUpper(columnName_[0]) + columnName_.Substring(1);
                        }
                    }


                    int rowcount = dt.Rows.Count + 5;

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
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                    wsReportNameHeaderRange.Merge();

                    wsReportNameHeaderRange.Value = "Reassignment Incentive Report";

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


                    worksheet.Cell(3, 7).Style.Font.Bold = true;
                    worksheet.Cell(3, 7).Value = "Date From";
                    worksheet.Cell(3, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(3, 8).Style.DateFormat.Format = "dd-MMM-yyyy";
                    worksheet.Cell(3, 8).Value = model.DateFrom;

                    worksheet.Cell(3, 9).Style.Font.Bold = true;
                    worksheet.Cell(3, 9).Value = "Date To";
                    worksheet.Cell(3, 10).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(3, 10).Style.DateFormat.Format = "dd-MMM-yyyy";
                    worksheet.Cell(3, 10).Value = model.DateTo;

                    var currentRow = 5;


                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A5:" + colName + "5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A5:" + colName + "5").Style.Font.Bold = true;


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
                            "Reassignment Report-For Incentive.xlsx"
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

        [HttpPost]
        public async Task<JsonResult> CheckExcelData(ReassignmentIncentiveReport model)
        {
            DataSet ds = await _reassignmentIncentiveReportService.GetDetails(model);

            bool hasData = ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0;

            return Json(new { hasData = hasData });
        }

    }
}
