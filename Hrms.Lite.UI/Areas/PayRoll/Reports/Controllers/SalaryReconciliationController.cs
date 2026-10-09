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
    public class SalaryReconciliationController : Controller
    {
        private readonly ISalaryReconciliationService _salaryReconciliationService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        private readonly IDropdownService _dropdownService;
        public class Global {
            public static Filter GlobalFilter;
        }
        public SalaryReconciliationController(ISalaryReconciliationService salaryReconciliationService, ICommonService commonService, IHelpers helpers, IDropdownService dropdownService)
        {
            _salaryReconciliationService = salaryReconciliationService;
            _commonService = commonService;
            _helpers = helpers;
            _dropdownService = dropdownService;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                SalaryReconciliation model = new SalaryReconciliation();
                ViewBag.PayRollRptCompareWith = new SelectList(await _dropdownService.GetPayrollReportMonthDropdown(), "Code", "Name");
            ViewBag.PayRollRptMonth = new SelectList(await _dropdownService.GetPayrollReportPreviousMonthDropdown(0), "Code", "Name");
            ViewBag.Status = new SelectList(await _dropdownService.GetSalaryReconciliationStatusDropdown(), "Code", "Name");
            ViewBag.Payrollgrp = new SelectList(await _dropdownService.GetPayrollGroupWithGrpCmpyCheckDropdown(), "Code", "Name");
                Filter filter = new Filter();
                filter.length = 10;
                model = await _salaryReconciliationService.GetSalaryReconciliation(0, 0, 0, 0, 0, filter);
                ViewBag.Emp = new SelectList("", "Code", "Name");
            await fillfilterDropdown();
           
         
          

            return View("~/Areas/PayRoll/Reports/Views/SalaryReconciliation/Index.cshtml",model);
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
        public async Task<IActionResult> GetList(int month, int compareWith, int Status, int EmployeeCode,int PayrollGroup)
        {
            SalaryReconciliation model = new SalaryReconciliation();
            Filter filter = new Filter();
            if (month != 0 && compareWith != 0 && Status != 0)
            {
                model = await _salaryReconciliationService.GetSalaryReconciliation(month, compareWith, Status, EmployeeCode, PayrollGroup, filter);
            }
         
            return PartialView("~/Areas/PayRoll/Reports/Views/SalaryReconciliation/_List.cshtml", model);
        }
        public async Task<IActionResult> GetSalaryReconciliationEmployeeDetails(int month,int compareWith, int Status,int EmployeeCode,int typeCode,string type, int PayrollGroup)
        {
            SalaryReconciliation model = new SalaryReconciliation();
            Filter filter = new Filter();
            model = await _salaryReconciliationService.GetSalaryReconciliationEmployeeDetails(month, compareWith, Status, EmployeeCode, typeCode, type, PayrollGroup, filter);
            return PartialView("~/Areas/PayRoll/Reports/Views/SalaryReconciliation/_PopUp.cshtml",model);
        }

        public async Task fillfilterDropdown()
        {

            ViewBag.Gen = new SelectList(await _dropdownService.GetGenderDropDown(), "Code", "Name");
            ViewBag.Cat = new SelectList(await _dropdownService.GetCategoryDropdown(), "Code", "Name");
            ViewBag.Grd = new SelectList(await _dropdownService.GetGradeDropdown(), "Code", "Name");
            ViewBag.Dsg = new SelectList(await _dropdownService.GetDesignationDropdown(), "Code", "Name");
            ViewBag.Dept = new SelectList(await _dropdownService.GetDepartmentDropdown(), "Code", "Name");
            ViewBag.Loc = new SelectList(await _dropdownService.GetLocationDropdown(), "Code", "Name");
            ViewBag.PayrollGroup = new SelectList(await _dropdownService.GetPayrollGroupDropdown(), "Code", "Name");

            ViewBag.division = new SelectList(await _dropdownService.GetDivisionDropdown(), "Code", "Name");
            ViewBag.section = new SelectList(await _dropdownService.GetSectionDropdown(), "Code", "Name");
            ViewBag.EmploymentType = new SelectList(await _dropdownService.GetEmploymentTypeDropdown(), "Code", "Name");
            ViewBag.EmploymentStatus = new SelectList(await _dropdownService.GetEmploymentStatusDropdown(), "Code", "Name");
            ViewBag.WorkLocation = new SelectList(await _dropdownService.GetWorkLocationDropdown(), "Code", "Name");
            ViewBag.WageGrade = new SelectList(await _dropdownService.GetWageGradeDropdown(), "Code", "Name");

        }
        [HttpPost]
        public async Task<IActionResult> GetList(Filter Filter,int month, int compareWith, int Status, int EmployeeCode, int typeCode, string type, int PayrollGroup)
        {
            try
            {
                await fillfilterDropdown();
                SalaryReconciliation model = new SalaryReconciliation();
                Filter filter = new Filter();
                Global.GlobalFilter = Filter;


                if (Filter.searchValue == "undefined" || Filter.searchValue == null)
                {
                    Filter.searchValue = "";
                }
                if (month != 0 && compareWith != 0 && Status != 0)
                {
                    model = await _salaryReconciliationService.GetSalaryReconciliationEmployeeDetails(month, compareWith, Status, EmployeeCode, typeCode, type, PayrollGroup, Filter);
                }
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model== null || model.SalaryReconciliationEmployeeDetails == null || model.SalaryReconciliationEmployeeDetails.Count() == 0)
                {


                    return Json(new
                    {
                        data = "",
                        draw = Filter.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }




                if (model.SalaryReconciliationEmployeeDetails.Count > 0)
                {
                    totalrows = model.SalaryReconciliationEmployeeDetails[0].active;


                }

                //model.AllTabCount = m.AllTabCount;
                if (Filter.searchValue != "")
                {
                    totalrowsafterfiltering = model.SalaryReconciliationEmployeeDetails[0].totalRecords;
                    return Json(new
                    {
                        data = model.SalaryReconciliationEmployeeDetails,
                        draw = Filter.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    return Json(new
                    {
                        data = model.SalaryReconciliationEmployeeDetails,
                        draw = Filter.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
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


        [HttpGet]
        public async Task<JsonResult> GetEmployee(int id)
        {
            var Emp = new SelectList(await _dropdownService.GetSalaryReconciliationEmployeeDropDown(id), "Code", "Name");
            return Json(Emp);
        }
        [HttpGet]
        public async Task<JsonResult> GetPrevMonth(int Month)
        {
            var PrevMonth = new SelectList(await _dropdownService.GetPayrollReportPreviousMonthDropdown(Month), "Code", "Name");
            return Json(PrevMonth);
        }

        public async Task<IActionResult> ExportToExcel(int month, int compareWith, int Status, int EmployeeCode,int PayrollGroup, string monthTxt,string compareWithTxt,string statusTxt)
        {
            try
            {
                SalaryReconciliation model = new SalaryReconciliation();

                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                Filter filter = new Filter();
                EP = await _commonService.GetExcelPassword("Salary Comparison", "Excel");
                int slNo = 0;

                model = await _salaryReconciliationService.GetSalaryReconciliation(month, compareWith, Status, EmployeeCode, PayrollGroup, filter);
                string currDateTime = await _helpers.GetCurrentDate();
                
                using (var workbook = new XLWorkbook())
                {

                    var worksheet = workbook.Worksheets.Add("Salary Comparison");


                    int count = model.SalaryReconciliationDetails.Count + 5; //no of data + no of heading rows


                    worksheet.Range("A1:I" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin; //for table border
                    worksheet.Range("A1:I" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:I" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:I" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:I" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;






                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 8))); //heading
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                    wsReportNameHeaderRange.Value = "Salary Comparison";

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
                    worksheet.Cell(3, 6).Value = model.SalaryReconciliationDetails.Count();
                    worksheet.Cell(4, 1).Style.Font.Bold = true;
                    worksheet.Cell(4, 1).Value = "Month";
                    worksheet.Cell(4, 2).Style.DateFormat.Format = "MMM-yyyy";
                    worksheet.Cell(4, 2).Value = monthTxt;
                    worksheet.Cell(4, 3).Style.Font.Bold = true;
                    worksheet.Cell(4, 3).Value = "Compare with";
                    worksheet.Cell(4, 4).Value = compareWithTxt;
                    worksheet.Cell(4, 4).Style.DateFormat.Format = "MMM-yyyy";
                    worksheet.Cell(4, 5).Style.Font.Bold = true;
                    worksheet.Cell(4, 5).Value = "Status";
                    worksheet.Cell(4, 6).Value = statusTxt;



                    var currentRow = 5;
                    #region Header
                    worksheet.Range("A5:I5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Cell(currentRow, 1).Value = "Sl.No";
                    worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 2).Value = "Description";
                    worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 3).Value = "Pre.Emp.Count";
                    worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 4).Value = " Cur.Emp.Count";
                    worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 5).Value = "Prev.Gross";
                    worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 6).Value = "Cur.Gross";
                    worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 7).Value = "Pre.Earnings";
                    worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 8).Value = "Cur.Earnings";
                    worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 9).Value = "Variance in Earnings";
                    worksheet.Cell(currentRow, 9).Style.Font.Bold = true;
                   


                    #endregion

                    #region Body
                    foreach (var item in model.SalaryReconciliationDetails)
                    {
                        currentRow++;
                        slNo = slNo + 1;
                        worksheet.Columns().AdjustToContents();
                        worksheet.Rows().AdjustToContents();
                        worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 1).Value = slNo;
                        worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 2).Value = item.Description;
                        worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 3).Value = item.PreviousEmpCount;
                        worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 4).Value = item.CurrentEmpCount;                        
                        worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 5).Value = item.PreviousGross;                        
                        worksheet.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 6).Value = item.CurrentGross;
                        worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 7).Value = item.PreviousEarning;
                        worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 8).Value = item.CurrentEarning;
                        worksheet.Cell(currentRow, 9).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 9).Value = item.Variance;                        
                        worksheet.Columns(1, 9).Style.Alignment.WrapText = true;
                        worksheet.Rows().Style.Alignment.WrapText = true;
                    }
                    #endregion
                    worksheet.Columns().AdjustToContents();
                    worksheet.Rows().AdjustToContents();
                    //worksheet.Protect("123");
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
                            "Salary Comparison List.xlsx"
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
        public async Task<IActionResult> ExportToExcelPopUp(int month, int compareWith, int Status, int EmployeeCode, int typeCode, int PayrollGroup, string type,string monthTxt, string compareWithTxt, string statusTxt,int totRecords)
        {
            try
            {               

                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                Filter filter = new Filter();
                EP = await _commonService.GetExcelPassword("Salary Comparison", "Excel");
              
                if (Global.GlobalFilter == null)
                    Global.GlobalFilter = new Filter();
                else
                {
                    Global.GlobalFilter.length = totRecords;
                    Global.GlobalFilter.start = 0;
                }
                string FilterValues = await _helpers.GetFilterValues(Global.GlobalFilter);
                if (FilterValues == "")
                {
                    FilterValues = "All";
                }
                //Get Dataset
                var dataset = await _salaryReconciliationService.GetEmployeeList_Excel(month, compareWith, Status, EmployeeCode, typeCode, type, PayrollGroup, Global.GlobalFilter);
                DataTable dt = dataset.Tables[0];


                //set EXCEL Column Heading 
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    
                        dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                }

               
                string currDateTime = await _helpers.GetCurrentDate();
                
                using (var workbook = new XLWorkbook())
                {

                    var worksheet = workbook.Worksheets.Add("Salary Comparison");


                    int count = dt.Rows.Count + 6; //no of data + no of heading rows


                    worksheet.Range("A1:I" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin; //for table border
                    worksheet.Range("A1:I" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:I" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:I" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:I" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;



                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 8))); //heading
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                    wsReportNameHeaderRange.Value = "Salary Comparison";

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
                    worksheet.Cell(3, 6).Value =dt.Rows.Count;
                    worksheet.Cell(4, 1).Style.Font.Bold = true;
                    worksheet.Cell(4, 1).Value = "Month";
                    worksheet.Cell(4, 2).Value = monthTxt;
                    worksheet.Cell(4, 2).Style.DateFormat.Format = "MMM-yyyy";
                    worksheet.Cell(4, 3).Style.Font.Bold = true;
                    worksheet.Cell(4, 3).Value = "Compare with";
                    worksheet.Cell(4, 4).Value = compareWithTxt;
                    worksheet.Cell(4, 4).Style.DateFormat.Format = "MMM-yyyy";
                    worksheet.Cell(4, 5).Style.Font.Bold = true;
                    worksheet.Cell(4, 5).Value = "Status";
                    worksheet.Cell(4, 6).Value = statusTxt;
                    worksheet.Cell(5, 1).Style.Font.Bold = true;
                    worksheet.Cell(5, 1).Value = "Filter Applied";
                    var filterapplied = worksheet.Range("B5:I5").Merge();

                    filterapplied.Value = FilterValues;

                    var currentRow = 6;

                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A6:I6").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A6:I6").Style.Font.Bold = true;


                    worksheet.Columns().AdjustToContents();
                    worksheet.Rows().AdjustToContents();
                    //worksheet.Protect("123");
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
                            "Salary Comparison List.xlsx"
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
