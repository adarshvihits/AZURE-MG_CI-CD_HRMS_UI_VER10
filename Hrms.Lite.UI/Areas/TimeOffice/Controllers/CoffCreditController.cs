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
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.TimeOffice.Controllers
{
    [Area("TimeOffice")]
    [SessionAuthorize]
    public class CoffCreditController : Controller
    {
        private readonly ICoffCreditService _CoffCreditService;
        private readonly ICommonService _commonService;
        private readonly IDropdownService _dropdownService;
        private readonly IHelpers _helpers;
        public static class Global
        {
          
            public static Filter GlobalFilter;
            public static DateTime fromDate;
            public static DateTime toDate;
            public static string TabIndex;
            public static bool IsRefreshed = true;

        }

        public CoffCreditController(ICommonService commonService, ICoffCreditService CoffCreditService, IDropdownService dropdownService, IHelpers Helpers)
        {
            _CoffCreditService = CoffCreditService;
            _commonService = commonService;
            _dropdownService = dropdownService;
            _helpers = Helpers;

        }
        public async Task <IActionResult> Index()
        {
            try
            {
                CoffCredit model = new CoffCredit();
            DateAndMonth _date = new DateAndMonth();
             await fillfilterDropdown();
            Filter filter = new Filter();
            var currMonth = await _commonService.GetCurrentMonthDropdown();
            ViewBag.MonthCombo = new SelectList(await _dropdownService.GetMonthDropdown(), "Code", "Name", currMonth.Data);
            _date = await _commonService.GetSelectedMonthDates(currMonth.Data);
            model = await _CoffCreditService.GetList(_date.FromDate, _date.ToDate, "APPROVAL_PENDING", filter);            
            model.From = _date.FromDate;
            model.To = _date.ToDate;
            model.TabValues = "APPROVAL_PENDING";
            return View("Index", model);
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
    
        public async Task<IActionResult> GetCoffCreditList(int Month, string TabIndex, DateTime Fromdate, DateTime Todate)
        {
            CoffCredit model = new CoffCredit();     
            Filter filter = new Filter();                
            model = await _CoffCreditService.GetList(Fromdate, Todate, TabIndex, filter);
            model.TabValues = TabIndex;
            return PartialView("_ConfirmationList", model);
        }

        [HttpPost]
        public async Task<IActionResult> ApproveOrRejectCoff(CoffCredit model,string Mode)
        {       

            var list = from li in model.ConfirmationList
                       where li.ConfirmationStatus == true
                       select li;
            model.ConfirmationList = list.ToList();
            model.Mode = Mode;
            var response = await _CoffCreditService.CoffCreditApproveOrReject(model);
            return Json(response);
        }
        public async Task<IActionResult> ApplyFilter(Filter filter)
        {
            CoffCredit model = new CoffCredit();
            Global.GlobalFilter = filter;
            model = await _CoffCreditService.GetList(Global.fromDate, Global.toDate, Global.TabIndex, filter);
            return PartialView("_ConfirmationList", model);
        }

        public async Task<IActionResult> GetFRomToDates(int Month)
        {
          var Dates= await _commonService.GetSelectedMonthDates(Month);
            return Json(Dates);
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

        public async Task<IActionResult> ExportToExcel(string TabIndex, int Month, DateTime fromdate, DateTime todate)
        {
            CoffCredit model = new CoffCredit();
            Filter filter = new Filter();
            model.DateAndMonth = await _commonService.GetSelectedMonthDates(Month);
            model.From = model.DateAndMonth.FromDate;
            model.To = model.DateAndMonth.ToDate;
            model = await _CoffCreditService.GetList(model.From, model.To, TabIndex, filter);
            int slno = 0;
            if (Global.GlobalFilter == null)
                Global.GlobalFilter = new Filter();
            string FilterValues = await _helpers.GetFilterValues(Global.GlobalFilter);
            if (FilterValues == "")
            {
                FilterValues = "All";
            }
            string currDateTime = await _helpers.GetCurrentDate();
            
            var LoginUser = HttpContext.Session.GetString("LoginUser");
            var CompanyName = HttpContext.Session.GetString("CompanyName");
            var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
            var GroupCompanyName = User.authUser.GroupCompanyName;


            using (var workbook = new XLWorkbook())
            {

                var worksheet = workbook.Worksheets.Add("CoffCredit List");

                int PendingCountnum = model.ConfirmationList.Count + 5;
                int cols = 0;
                string colRange, TabName;
                if (TabIndex != "APPROVAL_PENDING")
                {
                    cols = 11;
                    colRange = "L";
                    TabName = "Completed";
                }
                else
                {
                    cols = 10;
                    colRange = "K";
                    TabName = "Pending";
                }

                worksheet.Range("A1:" + colRange + PendingCountnum).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1:" + colRange + PendingCountnum).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1:" + colRange + PendingCountnum).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1:" + colRange + PendingCountnum).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1:" + colRange + PendingCountnum).Style.Border.LeftBorder = XLBorderStyleValues.Thin;

                var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + cols)));

                wsReportNameHeaderRange.Style.Font.Bold = true;
                wsReportNameHeaderRange.Style.Font.FontSize = 15;
                wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                wsReportNameHeaderRange.Merge();


                wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                wsReportNameHeaderRange.Style.Font.FontSize = 15;
                wsReportNameHeaderRange.Value = "COff Credit " + "(" + TabName + ")";
                worksheet.Range("A5:" + colRange + "5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                worksheet.Cell(2, 1).Style.Font.Bold = true;
                worksheet.Cell(2, 1).Value = "Group Company Name";
                worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                worksheet.Cell(2, 3).Style.Font.Bold = true;
                worksheet.Cell(2, 3).Value = "Month";
                worksheet.Cell(2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(2, 4).Value = Month;
                worksheet.Cell(2, 5).Style.Font.Bold = true;
                worksheet.Cell(2, 5).Value = "From";
                worksheet.Cell(2, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(2, 6).Value = fromdate;
                worksheet.Cell(2, 7).Style.Font.Bold = true;
                worksheet.Cell(2, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(2, 7).Value = "To";
                worksheet.Cell(2, 8).Value = todate;


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
                worksheet.Cell(3, 6).Value = model.ConfirmationList.Count;
                worksheet.Cell(4, 1).Style.Font.Bold = true;
                worksheet.Cell(4, 1).Value = "Filter Applied";
                var filterapplied = worksheet.Range("B4:" + colRange + "4").Merge();

                filterapplied.Value = FilterValues;

                var currentRow = 5;
                #region Header
                worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 1).Value = "Sl.No";
                worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 2).Value = "ID";
                worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 3).Value = "Employee Name";
                worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 4).Value = "Fun.Designation";
                worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 5).Value = "Department";
                worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 6).Value = "Location";
                worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 7).Value = "Date Worked";
                worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 8).Value = "IN";
                worksheet.Cell(currentRow, 9).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 9).Value = "OUT";
                worksheet.Cell(currentRow, 10).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 10).Value = "Total Time";
                worksheet.Cell(currentRow, 11).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 11).Value = "Att.Status";
                if (TabIndex != "APPROVAL_PENDING")
                {
                    worksheet.Cell(currentRow, 12).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 12).Value = "Status";
                }



                #endregion

                #region Body
                foreach (var item in model.ConfirmationList)
                {
                    currentRow++;
                    slno = slno + 1;
                    worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 1).Value = slno;
                    worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 2).Value = item.EmployeeID;
                    worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 3).Value = item.EmployeeName;
                    worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 4).Value = item.Designation.Name;
                    worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 5).Value = item.Department.Name;
                    worksheet.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 6).Value = item.Location.Name;
                    worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 7).Value = item.DateWorked;
                    worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 8).Value = item.IN;
                    worksheet.Cell(currentRow, 9).Value = item.OUT;
                    worksheet.Cell(currentRow, 9).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 10).Value = item.TotalTime;
                    worksheet.Cell(currentRow, 10).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 11).Value = item.AttendanceStatus;
                    if (TabIndex != "APPROVAL_PENDING")
                    {
                        worksheet.Cell(currentRow, 12).Value = item.Status;
                        worksheet.Columns(1, 12).Style.Alignment.WrapText = true;
                    }
                    else
                    {
                        worksheet.Columns(1, 11).Style.Alignment.WrapText = true;
                    }

                }
                #endregion
                worksheet.Columns().AdjustToContents();
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "COff Credit " + TabName + " List.xlsx"
                        );
                }
            }

        }








    }
}
