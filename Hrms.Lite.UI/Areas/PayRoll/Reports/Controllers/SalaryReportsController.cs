using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.PayRoll.Reports;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll.Reports;
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

namespace Hrms.Lite.UI.Areas.PayRoll.Reports.Controllers
{
    [Area("PayRoll")]
    [SessionAuthorize]
    public class SalaryReportsController : Controller
    {
        private readonly ISalaryReportsService _service;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        private readonly IDropdownService _dropdownService;
        public static class Global
        {
            public static Filter GlobalFilter;
            public static SalaryIntegrityReport GlobalModel;
                public static int MonthA;
            public static int MonthB;
            public static int EmployeeCode;

        }

        public SalaryReportsController(ISalaryReportsService service, ICommonService commonService, IHelpers Helpers, IDropdownService dropdownService)
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
                SalaryIntegrityReport model = new SalaryIntegrityReport();
            SalaryIntegrityReport m = new SalaryIntegrityReport();
            ViewBag.Month = new SelectList(await _dropdownService.GetPayrollReportMonthDropdown(), "Code", "Name");
            ViewBag.MonthB = new SelectList(await _dropdownService.GetSalaryIntegrityPreviousMonthDropdown(0));
            ViewBag.AllowDed= new SelectList(await _dropdownService.GetSalaryIntegrity_AlwDedDropdown(0), "Code", "Name");
            var monthdetails = await _commonService.GetCurrentMonthDetails();
            m = await _service.GetSalaryIntegrityReport_CheckList();
                model = await _service.GetSalaryIntegrityReportList(model);
                model.CheckList = m.CheckList;
            await fillfilterDropdown();
            return View("~/Areas/PayRoll/Reports/Views/SalaryReports/Index.cshtml",model);
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
        public async Task<IActionResult> GetSelectedMonthDates(int MonthCode)
        {
            //var monthC = await _commonService.GetSelectedMonthDates(MonthCode);
            //monthC.MonthCode = MonthCode;
            var monthC = await _dropdownService.GetSalaryIntegrityPreviousMonthDropdown(MonthCode);
            return Json(monthC);
        }

        [HttpGet]
        public async Task<JsonResult> GetAllowance_Deduction(int? id)
        {
            var AllowDed = new SelectList(await _dropdownService.GetSalaryIntegrity_AlwDedDropdown(id), "Code", "Name");
            return Json(AllowDed);
        }
        [HttpPost]
        public async Task<IActionResult> GetReportList(SalaryIntegrityReport model, Filter filter1, int monA)
        {
            try
            {
                SalaryIntegrityReport m = new SalaryIntegrityReport();
                //model.EmployeeHeader = new EmployeeHeader();

                model.MonthA = new MasterBase();
                model.MonthA.Code = monA;


                Filter filter = new Filter();
                Global.GlobalFilter = filter1;
                
                model.Filter = filter1;
                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }
                if (model != null)
                {
                    model = await _service.GetSalaryIntegrityReportList(model);
                }
                m.MainList = model.MainList;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;



                if (model == null || model.MainList == null || model.MainList.Count() == 0)
                {
                    m.MainList = new List<SalaryIntegrityReportList>();
                    model = new SalaryIntegrityReport();
                    model.MainList = new List<SalaryIntegrityReportList>();
                }
                else
                {
                    m.MainList = model.MainList;
                }



                if (model.MainList == null || model.MainList.Count() == 0)
                {

                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }




                if (model.MainList.Count > 0)
                {
                    totalrows = m.MainList[0].Active;

                }


                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.MainList[0].totalRecords;
                    return Json(new
                    {
                        data = model.MainList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    return Json(new
                    {
                        data = model.MainList,
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

        public async Task<IActionResult> GetSalaryReport(SalaryIntegrityReport model)
        {
            try
            {
                model = await _service.GetSalaryIntegrityReportList(model);
                return PartialView("~/Areas/PayRoll/Reports/Views/SalaryReports/_List.cshtml", model);
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
        public async Task<IActionResult> GetSalaryIntegrityReport_Details(int MonthA, int MonthB, int EmployeeCode,string EmpName,string empId)
        {
            try
            {
                SalaryIntegrityReport model = new SalaryIntegrityReport();
                Global.MonthA = MonthA;
                Global.MonthB = MonthB;
                Global.EmployeeCode = EmployeeCode;
                model = await _service.GetSalaryIntegrityReport_Details(MonthA, MonthB, EmployeeCode);
                ViewBag.empName = EmpName;
                ViewBag.empId = empId;
                return PartialView("~/Areas/PayRoll/Reports/Views/SalaryReports/_DetailsPopUp.cshtml", model);
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








        public async Task<IActionResult>ExcelEmpPopUp(string EmpName,string empId)
        {
            try
            {
                SalaryIntegrityReport model = new SalaryIntegrityReport();
                model = await _service.GetSalaryIntegrityReport_Details(Global.MonthA, Global.MonthB, Global.EmployeeCode);
                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Salary Integrity Report ", "Excel");
                int slNo = 0;

                string currDateTime = await _helpers.GetCurrentDate();
                
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Salary Integrity Report");
                    int count = model.Details.Count + 4;


                    worksheet.Range("A1:G" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:G" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:G" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:G" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:G" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 6)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                    wsReportNameHeaderRange.Value = "Salary Integrity Report";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                    worksheet.Cell(2, 3).Style.Font.Bold = true;
                  
                    worksheet.Cell(2, 3).Value = "Employee Name";
                    worksheet.Cell(2, 4).Value = EmpName+"("+ empId + ")";
                


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
                    worksheet.Cell(3, 6).Value = model.Details.Count;




                    var currentRow = 4;
                    #region Header
                    worksheet.Range("A4:G4").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Cell(currentRow, 1).Value = "Sl.No";
                    worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 2).Value = "Description";
                    worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 3).Value = "mnthA";
                    worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 4).Value = " mnthB ";
                    worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 5).Value = "Diff (A-B)";
                    worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 6).Value = "LOP (A)";
                    worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 7).Value = "LOP (B)";
                    worksheet.Cell(currentRow, 7).Style.Font.Bold = true;



                    #endregion

                    #region Body
                    foreach (var item in model.Details)
                    {
                        currentRow++;
                        slNo = slNo + 1;
                        worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 1).Value = slNo;
                        worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 2).Value = item.Name;
                        worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 3).Value = item.AmountA;
                        worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 4).Value = item.AmountB;
                        worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 5).Value = item.Difference;
                        worksheet.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 6).Value = item.LopA;
                        worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 7).Value = item.LopB;

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
                            "Salary Integrity Report List.xlsx"
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

        public async Task<IActionResult> GetExportToExcelData(SalaryIntegrityReport model) {
            Global.GlobalModel = model;
            return Json(true);
        }
        public async Task<IActionResult> ExportToExcel(int AllwOrDedCode,string MonthA,string MonthB,string AllwOrDedName, int Recordcount)
        {
            try
            {
                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                Filter filter = new Filter();
                EP = await _commonService.GetExcelPassword("Salary Integrity Report", "Excel");
                int slNo = 0;
                Global.GlobalModel.Filter = Global.GlobalFilter;
                Global.GlobalModel.Filter.length = Recordcount;
                Global.GlobalFilter.start = 0;

                string FilterValues = await _helpers.GetFilterValues(Global.GlobalFilter);
                if (FilterValues == "")
                {
                    FilterValues = "All";
                }

                string currDateTime = await _helpers.GetCurrentDate();
                


                //Get Dataset
                var dataset = await _service.GetSalaryIntegrityReportList_Excel(Global.GlobalModel);
                DataTable dt = dataset.Tables[0];


                //set EXCEL Column Heading 
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName == "lop(A)" || dt.Columns[i].ColumnName == "lop(B)")
                        dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();

                    if (dt.Columns[i].ColumnName == "month_A")
                        dt.Columns[i].ColumnName = MonthA;

                    if (dt.Columns[i].ColumnName == "month_B" )
                        dt.Columns[i].ColumnName = MonthB;
                    else
                        dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                }


                using (var workbook = new XLWorkbook())
                {

                 


                    var worksheet = workbook.Worksheets.Add("Salary Integrity Report");
                    int Rowcount = dt.Rows.Count + 5;

                    string columnName = "";
                    int Columncount = dt.Columns.Count;

                    while (Columncount > 0)
                    {
                        int modulo = (Columncount - 1) % 26;
                        columnName = Convert.ToChar('A' + modulo) + columnName;
                        Columncount = (Columncount - modulo) / 26;
                    }

                    string colName = columnName;
                    worksheet.Range("A1", colName + Rowcount).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1", colName + Rowcount).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1", colName + Rowcount).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1", colName + Rowcount).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1", colName + Rowcount).Style.Border.LeftBorder = XLBorderStyleValues.Thin;


                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, colName));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                    wsReportNameHeaderRange.Value = "Salary Integrity Report";

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
                        worksheet.Cell(4, 1).Value = "Month A";
                        worksheet.Cell(4, 2).Style.DateFormat.Format = "MMM-yyyy";
                        worksheet.Cell(4, 2).Value = MonthA;
                        worksheet.Cell(4, 3).Style.Font.Bold = true;
                        worksheet.Cell(4, 3).Value = "Month B";
                        worksheet.Cell(4, 4).Style.DateFormat.Format = "MMM-yyyy";
                        worksheet.Cell(4, 4).Value = MonthB;
                        worksheet.Cell(4, 5).Style.Font.Bold = true;
                        if (AllwOrDedCode == 0)
                            worksheet.Cell(4, 5).Value = "Allowance";
                        else
                            worksheet.Cell(4, 5).Value = "Deduction";
                        worksheet.Cell(4, 6).Value = AllwOrDedName;

                    worksheet.Cell(5, 1).Style.Font.Bold = true;
                    worksheet.Cell(5, 1).Value = "Filter Applied";
                    worksheet.Range("B5", colName + 5).Merge();
                    worksheet.Cell("B5").Value = FilterValues;



                    var currentRow = 6;

                        worksheet.Cell(currentRow, 1).InsertTable(dt);
                        worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                        worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A6:" + colName + "6").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A6:" + colName + "6").Style.Font.Bold = true;

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
                            "Salary Integrity Report.xlsx"
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
