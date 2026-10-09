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
    public class EmployeeInformationReportController : Controller
    {
        private readonly IEmployeeInformationReportService _employeeInformationReportService;
        private readonly IDropdownService _dropdownService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        public static class Global
        {
            public static EmployeeInformationReport data;
            public static System.Data.DataSet _dataset;
            public static int Tab;
        }
        public EmployeeInformationReportController(IEmployeeInformationReportService employeeInformationReportService, IDropdownService dropdownService, ICommonService commonService, IHelpers helpers)
        {
            _employeeInformationReportService = employeeInformationReportService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                await fillfilterDropdown();
                EmployeeInformationReport model = new EmployeeInformationReport();
                model.EmployeeGrouping = new List<EmployeeInformationReport_GroupBy>();
                model.Tab = 1;
                model.EmployeeGrouping = GetGroupByList();
                return View("~/Areas/DataBank/Reports/Views/EmployeeInformationReport/Index.cshtml", model);
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
        public async Task<IActionResult> GetDetails(EmployeeInformationReport model)
        {
            try
            {
                Global.Tab = model.Tab;
                var ds = await _employeeInformationReportService.GetDetails(model);
                if ((ds.Tables[0]?.Rows?.Count ?? 0) > 0)
                {

                    Global._dataset = ds;
                    return Json(true);
                }
                else
                {
                    return Json(false);
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

        public List<EmployeeInformationReport_GroupBy> GetGroupByList()
        {
            var list = new List<EmployeeInformationReport_GroupBy>
        {
            new EmployeeInformationReport_GroupBy {Code = 1, Name ="Personal Information", Applicability=false },
             new EmployeeInformationReport_GroupBy {Code = 2, Name ="Contact Details", Applicability=false },
             new EmployeeInformationReport_GroupBy {Code = 3, Name ="Address", Applicability=false },
             new EmployeeInformationReport_GroupBy {Code = 4, Name ="Identity Documents", Applicability=false },
             new EmployeeInformationReport_GroupBy {Code = 5, Name ="Job Details", Applicability=false },
             new EmployeeInformationReport_GroupBy {Code = 6, Name ="Compensation & Benefits", Applicability=false },
             new EmployeeInformationReport_GroupBy {Code = 7, Name ="Allowance & Deduction Applicability", Applicability=false },
             new EmployeeInformationReport_GroupBy {Code = 8, Name ="Attendance Settings", Applicability=false },
             new EmployeeInformationReport_GroupBy {Code = 9, Name ="Statutory", Applicability=false },
             new EmployeeInformationReport_GroupBy {Code =10, Name ="Payments", Applicability=false },
             new EmployeeInformationReport_GroupBy {Code = 11, Name ="Resignation Details", Applicability=false },
             new EmployeeInformationReport_GroupBy {Code = 12, Name ="Employee Authorities", Applicability=false },

        };
            return list;
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
            ViewBag.Region = new SelectList(await _dropdownService.GetRegionDropDown(), "Code", "Name");
            ViewBag.State = new SelectList(await _dropdownService.GetStateDropDown(), "Code", "Name");
            ViewBag.FunState = new SelectList(await _dropdownService.GetFunctionalStateDropDown(), "Code", "Name");
        }
        public async Task<IActionResult> ExportToexcel(EmployeeInformationReport model)
        {
            try
            {
               
               
                string groupbyName;
                if (Global.Tab == 1)
                    groupbyName = "Active";
                else if (Global.Tab == 2)
                    groupbyName = "Resigned";
                else
                    groupbyName = "All";
   
                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Employee Information Report", "Excel");
              
              
                string currDateTime = await _helpers.GetCurrentDate();
                


                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Employee Information Report");

                    var ds = await _employeeInformationReportService.GetDetails(model);
                    //ds.Tables[0].Rows.RemoveAt(0);
                    DataTable dt = ds.Tables[0];

                    var columnMappings = new Dictionary<string, string>
                    {
                            { "id", "ID" },
                            { "pt group", "PT Group" },
                            { "esi group", "ESI Group" },
                            { "esi no", "ESI No." },
                            { "pf group", "PF Group" },
                            { "pf no", "PF No." },
                            { "uan no", "UAN No." },
                            { "pan no", "PAN No." },
                            { "edli", "EDLI" },
                            { "mediclaim", "Medi Claim" },
                            { "epf no", "EPF No." },
                            { "personal accident", "Personal Accident" },
                            { "yearly_ctc", "YEARLY_CTC" },
                            
                    };

                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                        var columnName_ = dt.Columns[i].ColumnName;

                        if (columnMappings.ContainsKey(columnName_))
                        {
                            dt.Columns[i].ColumnName = columnMappings[columnName_];
                        }
                        else if (columnName_ == "dob" || columnName_ == "doj" || columnName_ == "dor")
                        {
                            dt.Columns[i].ColumnName = columnName_.ToUpper();
                        }
                        else
                        {
                            dt.Columns[i].ColumnName = char.ToUpper(columnName_[0]) + columnName_.Substring(1);
                        }
                    }


                    int rowcount = dt.Rows.Count + 4;

                    string columnName = "";
                    int Columncount =dt.Columns.Count;

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
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                    wsReportNameHeaderRange.Merge();
                   
                    wsReportNameHeaderRange.Value = "Employee Information Report(" + groupbyName + ")";

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

                 
                    //worksheet.Range("B4:R4").Merge();

                   

                    var headers = dt.Columns.Cast<DataColumn>()
                    .Select(c => c.ColumnName)
                    .ToArray();
                    worksheet.Cell(4, 1).InsertData(new List<string[]> { headers });
                    worksheet.Range(4, 1, 4, headers.Length).Style.Font.Bold = true;

                    var currentRow = 5;

                    worksheet.Cell(currentRow, 1).InsertData(dt);

                    //worksheet.Cell(currentRow, 1).InsertTable(dt);
                    //worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    //worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A4:"+ colName+"4").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A4:"+ colName+"4").Style.Font.Bold = true;


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
                            "Employee Information Report.xlsx"
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
