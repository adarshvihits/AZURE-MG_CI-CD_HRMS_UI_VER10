using ClosedXML.Excel;
using ExcelDataReader;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.TimeOffice;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Hrms.Lite.Shared.TimeOffice;
using Hrms.Lite.Shared.TimeOffice.Generic.Master;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace Hrms.Lite.UI.Areas.TimeOffice
{
    [Area("TimeOffice")]
    [SessionAuthorize]
    public class DutyRosterController : Controller
    {

        private readonly IDropdownService _dropdownService;
        private readonly IDutyRosterService _dutyRosterService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        private readonly IWebHostEnvironment _environment;      

        public DutyRosterController(IDropdownService dropdownService, ICommonService commonService, IDutyRosterService dutyRosterService, IHelpers helpers, IWebHostEnvironment env)
        {
            _dutyRosterService = dutyRosterService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;
            _environment = env;

        }
        public async Task<IActionResult> Index()
        {
            try
            {
                DutyRoster model = new DutyRoster();                
                var payrollList = (await _dropdownService.GetPayrollGroupDropdown());
                var MonthLsit = (await _dropdownService.GetDailyAttendanceEntry_MonthDropdown(0/*payrollList[0].Code*/));
                ViewBag.Month = new SelectList(MonthLsit, "Code", "Name");
                ViewBag.PayRollGroup = new SelectList(payrollList, "Code", "Name");
                ViewBag.Circle = new SelectList(await _dropdownService.GetDutyRosterCircleDropdown(), "Code", "Name");
                ViewBag.FormName = "outerList";
                model.Month = new Month();
                model.PayrollGroup = new Shared.Master.PayrollGroup();
                model.Circle = new Circle();
                Filter filter = new Filter();
                filter.length = 10;

                //model = await _dutyRosterService.DutyRosterEntryList_Paging(0, 0, 0, 0, filter);
                if (MonthLsit.Count > 0 && payrollList.Count > 0 && ViewBag.Circle.Items.Count > 0)
                {
                   
                   
                    model.Month.Code = MonthLsit[0].Code;
                    
                    model.PayrollGroup.Code = 0/*payrollList[0].Code*/;
                    model.Circle.Code = ViewBag.Circle.Items[0].Code;
                }
                var shifttype = await _dropdownService.GetShiftTypeDropDown();
                var addi = new List<Dropdown>
                {
                     new Dropdown{ Code = -1, Name = "--Select Shift Type--" },
                       new Dropdown{ Code = 0, Name = "ALL" }
                };
                var combinedShiftTypes = shifttype.Concat(addi);
                ViewBag.ShiftType = new SelectList(combinedShiftTypes.OrderBy(item => item.Code), "Code", "Name");
                model.ShiftType = new Dropdown(); model.ShiftType.Code = 0;
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

        public async Task<IActionResult> GetMonth(int Payrollgrp)
        {
            try
            {
                var Month = await _dropdownService.GetDailyAttendanceEntry_MonthDropdown(Payrollgrp);

                return Json(Month);

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

        public async Task<IActionResult> Filter(string FormName, Filter Filter)
        {
            if (FormName == "outerList")
                ViewBag.FormName = "outerList";
            else
                ViewBag.FormName = "innerList";
            await fillfilterDropdown();

            return PartialView("~/Views/Shared/_TimeOffice_Payroll_Filter.cshtml", Filter);
        }
        public async Task<IActionResult> ApplyFilterEntry(int Month, int PayrollGroup, int CIRCLE, Filter filter)
        {
            try
            {
                DutyRoster model = new DutyRoster();
                model = await _dutyRosterService.DutyRosterEntryList(Month, PayrollGroup, CIRCLE, filter);
                model.Month.Code = Month;
                model.PayrollGroup.Code = PayrollGroup;
                model.Circle.Code = CIRCLE;
                return PartialView("_DutyRosterEntryList", model);
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
        public async Task<IActionResult> ApplyFilterDateWise(Filter filter, int Month, int PayrollGroup, int CIRCLE, string SelectedDate)
        {
            try
            {
                DutyRoster model = new DutyRoster();
                model = await _dutyRosterService.DutyRosterDateWiseEntryList(Month, PayrollGroup, SelectedDate, CIRCLE, filter);
                ViewBag.Shift = new SelectList(await _dropdownService.GetDutyRosterShiftDropdown(), "Code", "Name");
                return PartialView("_DateWiseDutyRosterList", model);
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
        public async Task<IActionResult> GetShiftDetailsPOPUPAsync()
        {
            try
            {
                ShiftDetails model = new ShiftDetails();
                model.ShiftList = (List<ShiftDetails>)await _dutyRosterService.GetShiftDetailsforPopUp();
                return PartialView("_ShiftDetailsPopUp", model);
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
        public async Task<IActionResult> GetShiftDetails(int ShiftCode)
        {
            var Details = await _dutyRosterService.GetShiftDetails(ShiftCode);

            return Json(Details);
        }

        public async Task<IActionResult> GetEmpWiseShiftDrpDown(Guid EmployeeGI)
        {
            var details = new SelectList(await _dropdownService.GetDutyRosterEmployeeWiseShiftDropdown(EmployeeGI), "Code", "Name");
            return Json(details);
        }


        [HttpPost]
        public async Task<IActionResult> GetList(int month, int PayrollGroup, int CIRCLE,int ShiftType, Filter filter1)
        {
            try
            {
                DutyRoster model = new DutyRoster();
                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }
                if (month != 0 /*&& PayrollGroup != 0*/)
                    model = await _dutyRosterService.DutyRosterEntryList_Paging(month, PayrollGroup, CIRCLE, ShiftType, filter1);
                model.Circle.Code = CIRCLE;
                model.PayrollGroup.Code = PayrollGroup;
                model.Month.Code = month;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.DutyRosterList == null || model.DutyRosterList.Count() == 0)
                {

                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        Count = model,
                        recordsFiltered = totalrows
                    });
                }
                if (model.DutyRosterList.Count > 0)
                {
                    totalrows = model.DutyRosterList[0].active;

                }

                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = model.DutyRosterList[0].totalRecords;
                    return Json(new
                    {
                        data = model.DutyRosterList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows,
                        var1 = model.DutyRosterFooterList,                      
                        Count = model,
                       

                    });
                }
                else
                {
                    return Json(new
                    {
                        data = model.DutyRosterList,

                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows,
                        var1 = model.DutyRosterFooterList,                      
                        Count = model,
                        
                    });
                }


            }
            catch (Exception ex)
            {
                return View();
            }

        }
        public async Task<IActionResult> DutyRoster(int Month, int PayrollGroup, int CIRCLE,Filter filter)
        {
            try
            {
                DutyRoster model = new DutyRoster();                
                ViewBag.Month = new SelectList(await _dropdownService.GetDailyAttendanceEntry_MonthDropdown(PayrollGroup), "Code", "Name");
                ViewBag.PayRollGroup = new SelectList(await _dropdownService.GetPayrollGroupDropdown(), "Code", "Name");
                ViewBag.Circle = new SelectList(await _dropdownService.GetDutyRosterCircleDropdown(), "Code", "Name");
                model.Month = new Month();
                model.Month.Code = Month;
                model.PayrollGroup = new PayrollGroup();
                model.PayrollGroup.Code = PayrollGroup;
                model.Circle = new Circle();
                model.Circle.Code = CIRCLE;
                DateAndMonth FromTo = await _commonService.GetSelectedMonthDates(Month);
                model.From = FromTo.FromDate;
                model.To = FromTo.ToDate;
                ViewBag.FormName = "outerList";
                await fillfilterDropdown();
                var shifttype = await _dropdownService.GetShiftTypeDropDown();
                var addi = new List<Dropdown>
                {
                     new Dropdown{ Code = -1, Name = "--Select Shift Type--" },
                       new Dropdown{ Code = 0, Name = "ALL" }
                };
                var combinedShiftTypes = shifttype.Concat(addi);
                ViewBag.ShiftType = new SelectList(combinedShiftTypes.OrderBy(item => item.Code), "Code", "Name");
                model.ShiftType = new Dropdown(); model.ShiftType.Code = 0;
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
        public async Task<IActionResult> DutyRosterEntryList(int Month, int PayrollGroup, int CIRCLE, Filter filter)
        {
            try
            {
                DutyRoster model = new DutyRoster();

                if (Month != 0 && PayrollGroup != 0 && CIRCLE != 0)
                    model = await _dutyRosterService.DutyRosterEntryList(Month, PayrollGroup, CIRCLE, filter);
                model.Month = new Month();
                model.Month.Code = Month;
                model.PayrollGroup = new PayrollGroup();
                model.PayrollGroup.Code = PayrollGroup;
                model.Circle = new Circle();
                model.Circle.Code = CIRCLE;
                     

                return PartialView("_DutyRosterEntryList", model);
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
        public async Task<IActionResult> EntryBatchUpdate(int Month, int PayrollGroup, int CIRCLE, Filter filter)
        {
            try
            {
                DutyRoster model = new DutyRoster();

                DateAndMonth FromTo = await _commonService.GetSelectedMonthDates(Month);

                ViewBag.Shift = new SelectList(await _dropdownService.GetDutyRosterShiftDropdown(), "Code", "Name");
                model = await _dutyRosterService.DutyRosterMailListBatchUpdate_Details(Month, PayrollGroup, FromTo.FromDate.Value.ToString("yyyy-MMM-dd"), FromTo.ToDate.Value.ToString("yyyy-MMM-dd"), CIRCLE, 0, filter);

                model.Month.Code = Month;
                model.PayrollGroup.Code = PayrollGroup;
                model.From = FromTo.FromDate;
                model.To = FromTo.ToDate;
                model.Circle.Code = CIRCLE;
                return PartialView("_EntryBatchUpdate", model);
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

        public async Task<IActionResult> GetEntryBatchUpdateEmployeeList(int ShiftCode, string FormName, int Month, int PayrollGroup, int CIRCLE, string SelectedDate, Filter filter)
        {
            try
            {
                DutyRoster model = new DutyRoster();

                DateAndMonth FromTo = await _commonService.GetSelectedMonthDates(Month);
                if (FormName == "entry")
                    model = await _dutyRosterService.DutyRosterMailListBatchUpdate_Details(Month, PayrollGroup, FromTo.FromDate.Value.ToString("yyyy-MMM-dd"), FromTo.ToDate.Value.ToString("yyyy-MMM-dd"), CIRCLE, ShiftCode, filter);
                else
                    model = await _dutyRosterService.DutyRosterMailListBatchUpdate_Details(Month, PayrollGroup, SelectedDate, SelectedDate, CIRCLE, ShiftCode, filter);

                return PartialView("_EntryBatchUpdateEmpList", model);
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
        public async Task<IActionResult> DateWiseBatchUpdate(string SelectedDate, int Month, int PayrollGroup, int CIRCLE, Filter filter)
        {
            try
            {
                DutyRoster model = new DutyRoster();


                ViewBag.Shift = new SelectList(await _dropdownService.GetDutyRosterShiftDropdown(), "Code", "Name");
                model = await _dutyRosterService.DutyRosterDateWiseEntryList_BatchUpdate_Details
                        (Month, PayrollGroup, Convert.ToDateTime(SelectedDate).ToString("yyyy-MMM-dd"), CIRCLE, filter);
                model.Month.Code = Month;
                model.PayrollGroup.Code = PayrollGroup;
                model.SelectedDate = Convert.ToDateTime(SelectedDate);
                model.Circle.Code = CIRCLE;
                return PartialView("_DateWiseBatchUpdate", model);
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
        public async Task<IActionResult> EmpWiseBatchUpdate(Guid EmployeeGI, int Month)
        {
            try
            {
                DutyRoster model = new DutyRoster();


                ViewBag.EmployeeWiseShift = new SelectList(await _dropdownService.GetDutyRosterEmployeeWiseShiftDropdown(EmployeeGI), "Code", "Name");
                model = await _dutyRosterService.DutyRosterEmployeeWiseEntry_BatchUpdate_Details(Month);

                DateAndMonth FromTo = await _commonService.GetSelectedMonthDates(Month);


                model.EmployeeGI = EmployeeGI;
                model.Batch_From = FromTo.FromDate;
                model.Batch_To = FromTo.ToDate;
                return PartialView("_EmployeeWiseBatchUpdate", model);
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
        public async Task<IActionResult> DutyRoster_Employeewise_BatchUpDate(DutyRoster model)
        {
            model.From = model.Batch_From;
            model.To = model.Batch_To;
            var response = await _dutyRosterService.DutyRoster_Employeewise_BatchUpDate(model);
            return Json(response);
        }
        public async Task<IActionResult> EntryBatchUpdateSave(DutyRoster model)
        {
            try
            {
                var list = from li in model.DutyRoster_EmployeePrimaryInfo
                           where li.Applicability == true
                           select li;
                model.DutyRoster_EmployeePrimaryInfo = list.ToList();
                var response = await _dutyRosterService.DutyRoster_BatchUpDate(model);
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

        public async Task<IActionResult> DutyRosterDateWise(DateTime SelectedDate, int Month, int PayrollGroup, int CIRCLE, Filter filter)
        {
            try
            {
                DutyRoster model = new DutyRoster();

                if (filter.Basic_Filter == null && filter.Optional_Filter == null)
                {
                    ViewBag.IsFilterApplied = 0;
                }
                else
                {

                    ViewBag.IsFilterApplied = 1;
                }


                model = await _dutyRosterService.DutyRosterDateWiseEntryList(Month, PayrollGroup, SelectedDate.ToString("yyyy-MMM-dd"), CIRCLE, filter);
                ViewBag.Shift = new SelectList(await _dropdownService.GetDutyRosterShiftDropdown(), "Code", "Name");
                DateAndMonth FromTo = await _commonService.GetSelectedMonthDates(Month);
                model.Month.Code = Month;
                model.PayrollGroup.Code = PayrollGroup;
                model.Circle.Code = CIRCLE;
                model.From = Convert.ToDateTime(SelectedDate);
                model.To = FromTo.ToDate;
                model.Filter = new Filter();
                model.Filter = filter;
                model.Filter.F_Month = new Month();
                model.Filter.F_PayrollGroup = new PayrollGroup();
                model.Filter.F_Circle = new Circle();
                model.Filter.F_Month.Code = Month;
                model.Filter.F_PayrollGroup.Code = PayrollGroup;
                model.Filter.F_Circle.Code = CIRCLE;
                model.Filter.F_From = FromTo.FromDate.Value;
                model.Filter.F_To = FromTo.ToDate.Value;
                model.SelectedDate = SelectedDate;
                await fillfilterDropdown();

                return View("DateWiseDutyRosterEdit", model);
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

        public async Task<IActionResult> DutyRosterDateWiseEntryList(DateTime SelectedDate, int Month, int PayrollGroup, int CIRCLE, Filter filter)
        {
            try
            {
                DutyRoster model = new DutyRoster();
                model = await _dutyRosterService.DutyRosterDateWiseEntryList(Month, PayrollGroup, SelectedDate.ToString("yyyy-MMM-dd"), CIRCLE, filter);

                ViewBag.Shift = new SelectList(await _dropdownService.GetDutyRosterShiftDropdown(), "Code", "Name");
                return PartialView("_DateWiseDutyRosterList", model);
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
        public async Task<IActionResult> EmployeeDutyRosterEditDetails(Guid EmployeeGI, int Month, int PayrollGroup, int CIRCLE)
        {
            try
            {
                DutyRoster model = new DutyRoster();
                DateAndMonth FromTo = await _commonService.GetSelectedMonthDates(Month);
                model = await _dutyRosterService.EmployeeDutyRosterEditDetails(Month, PayrollGroup, FromTo.FromDate.Value.ToString("yyyy-MMM-dd"), FromTo.ToDate.Value.ToString("yyyy-MMM-dd"), CIRCLE, EmployeeGI);
                model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);
                ViewBag.EmployeeWiseShift = new SelectList(await _dropdownService.GetDutyRosterEmployeeWiseShiftDropdown(EmployeeGI), "Code", "Name");
                model.Month.Code = Month;
                model.PayrollGroup.Code = PayrollGroup;
                model.Circle.Code = CIRCLE;
                model.EmployeeGI = EmployeeGI;
                return View("EmployeeWiseDutyRosterEdit", model);
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

        public async Task<IActionResult> DutyRosterSave(Guid EmployeeGI, DateTime Date, int ShiftCode)
        {
            try
            {
                Employee_DutyRosterEditDetails model = new Employee_DutyRosterEditDetails();
                model.Date = Date;
                model.EmployeeGI = EmployeeGI;
                model.ShiftDetails = new Shared.DataBank.Generic.Master.ShiftDetails();
                model.ShiftDetails.Code = ShiftCode;
                var response = await _dutyRosterService.DutyRosterSave(model);
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

        public async Task<IActionResult> DutyrosterLockCheck(int CircleCode, string SelectedDate)
        {
            var response = new ResponseEntity("Invalid Date", true);
            if (SelectedDate != null)


                response = await _dutyRosterService.DutyrosterLockCheck(CircleCode, SelectedDate);

            return Json(response);
        }

        public async Task<IActionResult> ExportToexcel_DutyRosterEntryList(int totRecords, string CIRCLEName, string PayrollGroupName, string MName, int Month, int PayrollGroup, int CIRCLE,
            int Type, string Stype,Filter filter)
        {
            try
            {              
                filter.length = totRecords;
                filter.start = 0;
                string FilterValues = await _helpers.GetFilterValues(filter);

                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Duty Roster Entry List", "Excel");
                if (FilterValues == "")
                {
                    FilterValues = "All";
                }

                string currDateTime = await _helpers.GetCurrentDate();
                

                //Get Datset
                var dataset = await _dutyRosterService.DutyRosterEntryList_Excel(Month, PayrollGroup, CIRCLE, Type, filter);
                DataTable dt = dataset.Tables[0];
                DataTable dt1 = dataset.Tables[1];




                //set EXCEL Column Heading 
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName == "doj" || dt.Columns[i].ColumnName == "dob" || dt.Columns[i].ColumnName == "y/s")
                        dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                    else
                        dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                    if (dt.Columns[i].ColumnName == "EmployeeName")
                        dt.Columns[i].ColumnName = "Employee Name";

                    if (dt.Columns[i].ColumnName == "EmployeeID")
                        dt.Columns[i].ColumnName = "Employee ID";
                }

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Duty Roster Entry List");
                    int Rowcount = dt.Rows.Count + 6;
                    string columnName = "";
                    //int Columncount = model.MainList.Header.Count + 3;
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


                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, colName));// + col+ Rowcount));

                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                    wsReportNameHeaderRange.Value = "Duty Roster Entry List";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();

                    worksheet.Cell(2, 3).Style.Font.Bold = true;
                    worksheet.Cell(2, 3).Value = "Month ";
                    worksheet.Cell(2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(2, 4).Style.DateFormat.Format = "MMM-yyyy";

                    worksheet.Cell(2, 4).Value = CIRCLEName;

                    worksheet.Cell(2, 5).Style.Font.Bold = true;
                    worksheet.Cell(2, 5).Value = "From ";
                    worksheet.Cell(2, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(2, 6).Style.DateFormat.Format = "dd-MMM-yyyy";
                    worksheet.Cell(2, 6).Value = dt1.Rows[0].ItemArray[0].ToString();

                    worksheet.Cell(3, 1).Style.Font.Bold = true;
                    worksheet.Cell(3, 1).Value = "To";
                    worksheet.Cell(3, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(3, 2).Style.DateFormat.Format = "dd-MMM-yyyy";
                    worksheet.Cell(3, 2).Value = dt1.Rows[0].ItemArray[1].ToString(); 

                    worksheet.Cell(3, 3).Style.Font.Bold = true;
                    worksheet.Cell(3, 3).Value = "Circle ";
                    worksheet.Cell(3, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(3, 4).Value = CIRCLEName;

                    worksheet.Cell(3, 5).Style.Font.Bold = true;
                    worksheet.Cell(3, 5).Value = "Payroll Group ";
                    worksheet.Cell(3, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(3, 6).Value = PayrollGroupName;

                    worksheet.Cell(3, 7).Style.Font.Bold = true;
                    worksheet.Cell(3, 7).Value = "Shift Type";
                    worksheet.Cell(3, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(3, 8).Value = Stype;

                    worksheet.Cell(4, 1).Style.Font.Bold = true;
                    worksheet.Cell(4, 1).Value = "Login User";
                    worksheet.Cell(4, 2).Value = LoginUser;
                    worksheet.Cell(4, 3).Style.Font.Bold = true;
                    worksheet.Cell(4, 3).Value = "Date & Time";
                    worksheet.Cell(4, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(4, 4).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
                    worksheet.Cell(4, 4).Value = currDateTime.ToString();
                    worksheet.Cell(4, 5).Style.Font.Bold = true;
                    worksheet.Cell(4, 5).Value = "Total Count";
                    worksheet.Cell(4, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(4, 6).Value = dt.Rows.Count;




                    worksheet.Cell(5, 1).Style.Font.Bold = true;
                    worksheet.Cell(5, 1).Value = "Filter Applied";
                    worksheet.Range("B5", colName + 5).Merge();
                    worksheet.Cell("B5").Value = FilterValues;

                    var currentRow = 6;

                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;

                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A6", colName + 6).Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A6", colName + 6).Style.Font.Bold = true;


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
                            "Duty Roster Entry List.xlsx"
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


        public async Task<IActionResult> ExportToexcel_DutyRosterEmployeeWiseList(Guid EmployeeGI, int Month, int PayrollGroup, int CIRCLE, Filter filter)
        {
            try
            {
                DutyRoster model = new DutyRoster();
                DateAndMonth FromTo = await _commonService.GetSelectedMonthDates(Month);
                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Employee Wise Duty Roster List", "Excel");
                int slNo = 0;
                string currDateTime = await _helpers.GetCurrentDate();
                
                model = await _dutyRosterService.EmployeeDutyRosterEditDetails(Month, PayrollGroup, FromTo.FromDate.Value.ToString("yyyy-MMM-dd"), FromTo.ToDate.Value.ToString("yyyy-MMM-dd"), CIRCLE, EmployeeGI);

                model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);


                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Employee Wise Duty Roster List");
                    int Bordercount = model.Employee_DutyRosterEditDetails.Count + 6;


                    worksheet.Range("A1:G" + Bordercount).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:G" + Bordercount).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:G" + Bordercount).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:G" + Bordercount).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:G" + Bordercount).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 6)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();

                    wsReportNameHeaderRange.Value = "Employee Wise Duty Roster List";
                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();

                    worksheet.Cell(2, 3).Style.Font.Bold = true;
                    worksheet.Cell(2, 3).Value = "Employee Name:";
                    worksheet.Cell(2, 4).Value = model.EmployeeHeader.EmployeeName;

                    worksheet.Cell(2, 5).Style.Font.Bold = true;
                    worksheet.Cell(2, 5).Value = "Month ";
                    worksheet.Cell(2, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(2, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(2, 6).Style.DateFormat.Format = "MMM-yyyy";
                    worksheet.Cell(2, 6).Value = model.Month.Name;
                    worksheet.Cell(3, 1).Style.Font.Bold = true;
                    worksheet.Cell(3, 1).Value = "From";
                    worksheet.Cell(3, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(3, 2).Value = model.From;
                    worksheet.Cell(3, 2).Style.DateFormat.Format = "dd-MMM-yyyy";
                    worksheet.Cell(3, 3).Style.Font.Bold = true;
                    worksheet.Cell(3, 3).Value = "To";
                    worksheet.Cell(3, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(3, 4).Value = model.To;
                    worksheet.Cell(3, 4).Style.DateFormat.Format = "dd-MMM-yyyy";
                    worksheet.Cell(3, 5).Style.Font.Bold = true;
                    worksheet.Cell(3, 5).Value = "Circle";
                    worksheet.Cell(3, 6).Value = model.Circle.Name;
                    worksheet.Cell(4, 1).Style.Font.Bold = true;
                    worksheet.Cell(4, 1).Value = "PayRollGroup";
                    worksheet.Cell(4, 2).Value = model.PayrollGroup.Name;


                    worksheet.Cell(5, 1).Style.Font.Bold = true;
                    worksheet.Cell(5, 1).Value = "Login User";
                    worksheet.Cell(5, 2).Value = LoginUser;
                    worksheet.Cell(5, 3).Style.Font.Bold = true;
                    worksheet.Cell(5, 3).Value = "Date & Time";
                    worksheet.Cell(5, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(5, 4).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
                    worksheet.Cell(5, 4).Value = currDateTime.ToString();
                    worksheet.Cell(5, 5).Style.Font.Bold = true;
                    worksheet.Cell(5, 5).Value = "Total Count";
                    worksheet.Cell(5, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(5, 6).Value = model.Employee_DutyRosterEditDetails.Count;

                    var currentRow = 6;

                    #region Header
                    worksheet.Range("A6:G6").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Cell(currentRow, 1).Value = "Sl.No";
                    worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 2).Value = "Date";
                    worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 3).Value = "Day";
                    worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 4).Value = "Shift Code";
                    worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 5).Value = "Shift Name";
                    worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 6).Value = "Shift Start";
                    worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 7).Value = "Shift End";
                    worksheet.Cell(currentRow, 7).Style.Font.Bold = true;

                    #endregion

                    #region Body
                    foreach (var item in model.Employee_DutyRosterEditDetails)
                    {
                        currentRow++;
                        slNo = slNo + 1;
                        worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 1).Value = slNo;
                        worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 2).Style.DateFormat.Format = "dd-MMM-yyyy";
                        worksheet.Cell(currentRow, 2).Value = item.Date;
                        worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 3).Value = item.Day;
                        worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 4).Value = item.ShiftDetails.ShiftCode;
                        worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 5).Value = item.ShiftDetails.Name;
                        if (item.ShiftDetails.ShiftStartTime != "")
                        {
                            worksheet.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 6).Value = "(" + item.ShiftDetails.ShiftStartTime + " )";

                        }
                        else
                        {
                            worksheet.Cell(currentRow, 6).Value = "";
                        }
                        if (item.ShiftDetails.ShiftEndTime != "")
                        {

                            worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 7).Value = "(" + item.ShiftDetails.ShiftEndTime + " )";
                        }
                        else
                        {
                            worksheet.Cell(currentRow, 7).Value = "";
                        }

                        worksheet.Columns(1, 7).Style.Alignment.WrapText = true;
                        worksheet.Rows().Style.Alignment.WrapText = true;
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
                            book.Protect(EP.Password, false, false);
                            book.SaveToStream(stream);
                        }
                        var content = stream.ToArray();
                        return File(
                            content,
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            "Employee Wise Duty Roster List.xlsx"
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



        public async Task<IActionResult> ExportToexcel_DutyRosterDateWiseList(string MName, int Month, int PayrollGroup, int CIRCLE, Filter filter, string SelectedDate)
        {
            try
            {
                DutyRoster model = new DutyRoster();
                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("DateWise Duty Roster List", "Excel");
                int slNo = 0;

                string FilterValues = await _helpers.GetFilterValues(filter);
                model = await _dutyRosterService.DutyRosterDateWiseEntryList(Month, PayrollGroup, SelectedDate, CIRCLE, filter);
                if (FilterValues == "")
                {
                    FilterValues = "All";
                }
                string currDateTime = await _helpers.GetCurrentDate();
                



                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("DateWise Duty Roster List");
                    int Rowcount = model.DateWise_DutyRosterEditDetails.Count + 6;


                    worksheet.Range("A1:G" + Rowcount).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:G" + Rowcount).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:G" + Rowcount).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:G" + Rowcount).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:G" + Rowcount).Style.Border.LeftBorder = XLBorderStyleValues.Thin;

                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 6)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                    wsReportNameHeaderRange.Value = "DateWise Duty Roster List";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();

                    worksheet.Cell(2, 3).Style.Font.Bold = true;
                    worksheet.Cell(2, 3).Value = "selected Date";
                    worksheet.Cell(2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(2, 4).Style.DateFormat.Format = "dd-MMM-yyyy";
                    worksheet.Cell(2, 4).Value = model.From;
                    worksheet.Cell(2, 5).Style.Font.Bold = true;
                    worksheet.Cell(2, 5).Value = "Month ";
                    worksheet.Cell(2, 5).Style.DateFormat.Format = "MMM-yyyy";
                    worksheet.Cell(2, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(2, 6).Value = MName;
                    worksheet.Cell(2, 6).Value = SelectedDate;

                    worksheet.Cell(3, 1).Style.Font.Bold = true;
                    worksheet.Cell(3, 1).Value = "Circle : ";
                    worksheet.Cell(3, 2).Value = model.Circle.Name;
                    worksheet.Cell(3, 3).Style.Font.Bold = true;
                    worksheet.Cell(3, 3).Value = "PayRollGroup : ";
                    worksheet.Cell(3, 4).Value = model.PayrollGroup.Name;

                    worksheet.Cell(4, 1).Style.Font.Bold = true;
                    worksheet.Cell(4, 1).Value = "Login User";
                    worksheet.Cell(4, 2).Value = LoginUser;
                    worksheet.Cell(4, 3).Style.Font.Bold = true;
                    worksheet.Cell(4, 3).Value = "Date & Time";
                    worksheet.Cell(4, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(4, 4).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
                    worksheet.Cell(4, 4).Value = currDateTime.ToString();
                    worksheet.Cell(4, 5).Style.Font.Bold = true;
                    worksheet.Cell(4, 5).Value = "Total Count";
                    worksheet.Cell(4, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(4, 6).Value = model.DateWise_DutyRosterEditDetails.Count;
                    worksheet.Cell(5, 1).Style.Font.Bold = true;
                    worksheet.Cell(5, 1).Value = "Filter Applied";
                    worksheet.Range("B5:G5").Merge();
                    worksheet.Cell("B5").Value = FilterValues;
                    var currentRow = 6;
                    #region Header
                    worksheet.Range("A6:G6").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Cell(currentRow, 1).Value = "Sl.No";
                    worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 2).Value = "ID";
                    worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 3).Value = "Emploee Name";
                    worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 4).Value = "Shift Code";
                    worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 5).Value = "Shift Name";
                    worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 6).Value = "Shift Start";
                    worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 7).Value = "Shift End";


                    #endregion

                    #region Body
                    foreach (var item in model.DateWise_DutyRosterEditDetails)
                    {
                        currentRow++;
                        slNo = slNo + 1;
                        worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 1).Value = slNo;
                        worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 2).Value = item.EmployeeID;
                        worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 3).Value = item.FullName;
                        worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 4).Value = item.ShiftDetails.ShiftCode;
                        worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 5).Value = item.ShiftDetails.Name;
                        if (item.ShiftDetails.ShiftStartTime != "")
                        {
                            worksheet.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 6).Value = "(" + item.ShiftDetails.ShiftStartTime + " )";
                        }
                        else
                        {
                            worksheet.Cell(currentRow, 6).Value = "";
                        }
                        if (item.ShiftDetails.ShiftEndTime != "")
                        {
                            worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 7).Value = "(" + item.ShiftDetails.ShiftEndTime + " )";
                        }
                        else
                        {
                            worksheet.Cell(currentRow, 7).Value = "";
                        }
                        worksheet.Columns(1, 7).Style.Alignment.WrapText = true;
                        worksheet.Rows().Style.Alignment.WrapText = true;
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
                            book.Protect(EP.Password, false, false);
                            book.SaveToStream(stream);
                        }
                        var content = stream.ToArray();
                        return File(
                            content,
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            "DateWise Duty Roster List.xlsx"
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
        public async Task<IActionResult> ImportFromExcel(DutyRoster model)
        {
            string path = Path.Combine(this._environment.WebRootPath, "Uploads");
            string fileName = model.Excel.FileData.FileName;
            try
            {
                model.DutyRosterList = new List<DutyRosterList>();
                string wwwPath = this._environment.WebRootPath;
                string contentPath = this._environment.ContentRootPath;


                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                {
                    model.Excel.FileData.CopyTo(stream);

                }
                try
                {
                    using (var stream = System.IO.File.Open(path + "\\" + fileName, FileMode.Open, FileAccess.Read))
                    {

                        using (var reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            List<DutyRosterList> List = new List<DutyRosterList>();
                            var readerrr = ExcelReaderFactory.CreateReader(stream);
                            while (reader.Read()) //Each row of the file
                            {
                                var item = new DutyRosterList();
                                if (reader.GetValue(0) != null)
                                {
                                    item.EmployeeID = reader.GetValue(0).ToString();
                                }
                                if (reader.GetValue(1) != null)
                                {
                                    item.EmployeeName = reader.GetValue(1).ToString();
                                }
                                if (reader.GetValue(2) != null)
                                {
                                    item.Day1 = reader.GetValue(2).ToString();
                                }
                                if (reader.GetValue(3) != null)
                                {
                                    item.Day2 = reader.GetValue(3).ToString();
                                }
                                if (reader.GetValue(4) != null)
                                {
                                    item.Day3 = reader.GetValue(4).ToString();
                                }
                                if (reader.GetValue(5) != null)
                                {
                                    item.Day4 = reader.GetValue(5).ToString();
                                }
                                if (reader.GetValue(6) != null)
                                {
                                    item.Day5 = reader.GetValue(6).ToString();
                                }
                                if (reader.GetValue(7) != null)
                                {
                                    item.Day6 = reader.GetValue(7).ToString();
                                }
                                if (reader.GetValue(8) != null)
                                {
                                    item.Day7 = reader.GetValue(8).ToString();
                                }
                                if (reader.GetValue(9) != null)
                                {
                                    item.Day8 = reader.GetValue(9).ToString();
                                }
                                if (reader.GetValue(10) != null)
                                {
                                    item.Day9 = reader.GetValue(10).ToString();
                                }
                                if (reader.GetValue(11) != null)
                                {
                                    item.Day10 = reader.GetValue(11).ToString();
                                }
                                if (reader.GetValue(12) != null)
                                {
                                    item.Day11 = reader.GetValue(12).ToString();
                                }
                                if (reader.GetValue(13) != null)
                                {
                                    item.Day12 = reader.GetValue(13).ToString();
                                }
                                if (reader.GetValue(14) != null)
                                {
                                    item.Day13 = reader.GetValue(14).ToString();
                                }
                                if (reader.GetValue(15) != null)
                                {
                                    item.Day14 = reader.GetValue(15).ToString();
                                }
                                if (reader.GetValue(16) != null)
                                {
                                    item.Day15 = reader.GetValue(16).ToString();
                                }
                                if (reader.GetValue(17) != null)
                                {
                                    item.Day16 = reader.GetValue(17).ToString();

                                }
                                if (reader.GetValue(18) != null)
                                {
                                    item.Day17 = reader.GetValue(18).ToString();
                                }
                                if (reader.GetValue(19) != null)
                                {
                                    item.Day18 = reader.GetValue(19).ToString();
                                }
                                if (reader.GetValue(20) != null)
                                {
                                    item.Day19 = reader.GetValue(20).ToString();
                                }
                                if (reader.GetValue(21) != null)
                                {
                                    item.Day20 = reader.GetValue(21).ToString();
                                }
                                if (reader.GetValue(22) != null)
                                {
                                    item.Day21 = reader.GetValue(22).ToString();
                                }
                                if (reader.GetValue(23) != null)
                                {
                                    item.Day22 = reader.GetValue(23).ToString();
                                }
                                if (reader.GetValue(24) != null)
                                {
                                    item.Day23 = reader.GetValue(24).ToString();
                                }
                                if (reader.GetValue(25) != null)
                                {
                                    item.Day24 = reader.GetValue(25).ToString();
                                }
                                if (reader.GetValue(26) != null)
                                {
                                    item.Day25 = reader.GetValue(26).ToString();
                                }
                                if (reader.GetValue(27) != null)
                                {
                                    item.Day26 = reader.GetValue(27).ToString();
                                }
                                if (reader.GetValue(28) != null)
                                {
                                    item.Day27 = reader.GetValue(28).ToString();
                                }
                                if (reader.GetValue(29) != null)
                                {
                                    item.Day28 = reader.GetValue(29).ToString();
                                }
                                if (reader.GetValue(30) != null)
                                {
                                    item.Day29 = reader.GetValue(30).ToString();
                                }
                                if (reader.GetValue(31) != null)
                                {
                                    item.Day30 = reader.GetValue(31).ToString();
                                }
                                if (reader.GetValue(32) != null)
                                {
                                    item.Day31 = reader.GetValue(32).ToString();
                                }

                                List.Add(item);

                            }
                            if (List[0].EmployeeID != "Employee ID" || List[0].EmployeeName != "Employee Name" || List[0].Day1 != "1" || List[0].Day2 != "2" || List[0].Day3 != "3" || List[0].Day4 != "4"
                                || List[0].Day5 != "5" || List[0].Day6 != "6" || List[0].Day7 != "7" || List[0].Day8 != "8" || List[0].Day9 != "9" || List[0].Day10 != "10" || List[0].Day11 != "11"
                                || List[0].Day12 != "12" || List[0].Day13 != "13" || List[0].Day14 != "14" || List[0].Day15 != "15" || List[0].Day16 != "16" || List[0].Day17 != "17" || List[0].Day18 != "18"
                                || List[0].Day19 != "19" || List[0].Day20 != "20" || List[0].Day21 != "21" || List[0].Day22 != "22" || List[0].Day23 != "23" || List[0].Day24 != "24" || List[0].Day25 != "25"
                                || List[0].Day26 != "26" || List[0].Day27 != "27" || List[0].Day28 != "28" || List[0].Day29 != "29" || List[0].Day30 != "30" || List[0].Day31 != "31")
                            {
                                reader.Close();
                                if (System.IO.File.Exists(path + "\\" + fileName))
                                {
                                    System.IO.File.Delete(path + "\\" + fileName);
                                }
                                return Json(new ResponseEntity("Excel File not in the prescribed format", false));
                            }
                            model.DutyRosterList = List;

                            // model.VariableAllowanceList.RemoveAt(0);
                        }
                    }
                }
                catch (Exception e)
                {
                    // Check if file exists with its full path    
                    if (System.IO.File.Exists(path + "\\" + fileName))
                    {
                        System.IO.File.Delete(path + "\\" + fileName);
                    }
                    return Json(new ResponseEntity("Excel File not in the prescribed format", false));
                }

                model.Excel.FileData = null;
                var response = await _dutyRosterService.ImportFromExcel(model);
                // Check if file exists with its full path    
                if (System.IO.File.Exists(path + "\\" + fileName))
                {
                    System.IO.File.Delete(path + "\\" + fileName);
                }
                if (response.Message != "UPDATED")
                {
                    model.DutyRosterList = response.Data;
                    model.Message = response.Message;
                    return PartialView("_UnmatchedList", model);
                }
                else
                {
                    response.Message = "Uploaded Successfully";
                    return Json(response);
                }
            }
            catch (UnauthorizedAccessException e)
            {

                return RedirectToAction("Login", "Account", new { Area = "" });
            }
            catch (Exception e)
            {
                // Check if file exists with its full path    
                if (System.IO.File.Exists(path + "\\" + fileName))
                {
                    System.IO.File.Delete(path + "\\" + fileName);
                }
                return RedirectToAction("Error", "Account", new { Area = "" });
            }


        }

        public async Task<IActionResult> ImportFromExcel_FromToDateUpdation(DutyRoster model)
        {
            string path = Path.Combine(this._environment.WebRootPath, "Uploads");
            string fileName = model.Excel.FileData.FileName;
            try
            {
                model.DutyRosterList = new List<DutyRosterList>();
                string wwwPath = this._environment.WebRootPath;
                string contentPath = this._environment.ContentRootPath;


                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                {
                    model.Excel.FileData.CopyTo(stream);

                }
                try
                {
                    using (var stream = System.IO.File.Open(path + "\\" + fileName, FileMode.Open, FileAccess.Read))
                    {

                        using (var reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            List<DutyRosterList> List = new List<DutyRosterList>();
                            var readerrr = ExcelReaderFactory.CreateReader(stream);
                            if (model.ImportFrom > model.ImportTo)
                            {
                                return Json(new ResponseEntity("Start date must be earlier than or equal to end date.", false));
                            }

                            // Calculate the difference
                            TimeSpan dateDifference = model.ImportTo.Value - model.ImportFrom.Value;

                            // Get the total days
                            int totalDays = dateDifference.Days + 1;
                            int fromDayNumber = model.ImportFrom.Value.Day;
                            int toDayNumber = model.ImportTo.Value.Day;

                            while (reader.Read()) //Each row of the file
                            {
                                var item = new DutyRosterList();
                                if (reader.GetValue(0) != null)
                                {
                                    item.EmployeeID = reader.GetValue(0).ToString();
                                }
                                if (reader.GetValue(1) != null)
                                {
                                    item.EmployeeName = reader.GetValue(1).ToString();
                                }

                                int k = 1;
                                int j = 2;
                                for (int i = fromDayNumber; i <=toDayNumber; i++)
                                {
                                    if (reader.GetValue(j) != null)
                                    {
                                        // Construct the property name dynamically
                                        var propertyName = "Day" + i;

                                        // Get the property using reflection
                                        var property = item.GetType().GetProperty(propertyName);

                                        if (property != null)
                                        {
                                            // Set the value of the property dynamically
                                            property.SetValue(item, reader.GetValue(j).ToString());
                                        }

                                        k++;
                                        j++;
                                    }
                                }
                              
                            List.Add(item);

                            }
                          /*  if (List[0].EmployeeID != "Employee ID" || List[0].EmployeeName != "Employee Name"
                               || List[0].Day1 != "1" || List[0].Day2 != "2" || List[0].Day3 != "3" || List[0].Day4 != "4"
                                || List[0].Day5 != "5" || List[0].Day6 != "6" || List[0].Day7 != "7" || List[0].Day8 != "8" || List[0].Day9 != "9" || List[0].Day10 != "10" || List[0].Day11 != "11"
                                || List[0].Day12 != "12" || List[0].Day13 != "13" || List[0].Day14 != "14" || List[0].Day15 != "15" || List[0].Day16 != "16" || List[0].Day17 != "17" || List[0].Day18 != "18"
                                || List[0].Day19 != "19" || List[0].Day20 != "20" || List[0].Day21 != "21" || List[0].Day22 != "22" || List[0].Day23 != "23" || List[0].Day24 != "24" || List[0].Day25 != "25"
                                || List[0].Day26 != "26" || List[0].Day27 != "27" || List[0].Day28 != "28" || List[0].Day29 != "29" || List[0].Day30 != "30" || List[0].Day31 != "31")
                            {
                                reader.Close();
                                if (System.IO.File.Exists(path + "\\" + fileName))
                                {
                                    System.IO.File.Delete(path + "\\" + fileName);
                                }
                                return Json(new ResponseEntity("Excel File not in the prescribed format", false));
                            }*/

                            // Check if EmployeeID and EmployeeName are valid
                            if (List[0].EmployeeID != "Employee ID" || List[0].EmployeeName != "Employee Name")
                            {
                                reader.Close();
                                if (System.IO.File.Exists(path + "\\" + fileName))
                                {
                                    System.IO.File.Delete(path + "\\" + fileName);
                                }
                                return Json(new ResponseEntity("Excel File not in the prescribed format", false));
                            }

                            // Dynamically check Day1 to Day31
                            for (int day = fromDayNumber; day <= toDayNumber; day++)
                            {
                                string propertyName = $"Day{day}";
                                var property = List[0].GetType().GetProperty(propertyName);

                                if (property == null)
                                {
                                    reader.Close();
                                    if (System.IO.File.Exists(path + "\\" + fileName))
                                    {
                                        System.IO.File.Delete(path + "\\" + fileName);
                                    }
                                    return Json(new ResponseEntity("Excel File not in the prescribed format", false));
                                }

                                var value = property.GetValue(List[0])?.ToString();
                                if (value != day.ToString())
                                {
                                    reader.Close();
                                    if (System.IO.File.Exists(path + "\\" + fileName))
                                    {
                                        System.IO.File.Delete(path + "\\" + fileName);
                                    }
                                    return Json(new ResponseEntity("Excel File not in the prescribed format", false));
                                }
                            }                          

                          
                            model.DutyRosterList = List;

                            // model.VariableAllowanceList.RemoveAt(0);
                        }
                    }
                }
                catch (Exception e)
                {
                    // Check if file exists with its full path    
                    if (System.IO.File.Exists(path + "\\" + fileName))
                    {
                        System.IO.File.Delete(path + "\\" + fileName);
                    }
                    return Json(new ResponseEntity("Excel File not in the prescribed format", false));
                }

                model.Excel.FileData = null;
                var response = await _dutyRosterService.ImportFromExcel_FromToDateUpdation(model);
                // Check if file exists with its full path    
                if (System.IO.File.Exists(path + "\\" + fileName))
                {
                    System.IO.File.Delete(path + "\\" + fileName);
                }
                if (response.Message != "UPDATED")
                {
                    model.DutyRosterList = response.Data;
                    model.Message = response.Message;
                    return PartialView("_UnmatchedList", model);
                }
                else
                {
                    response.Message = "Uploaded Successfully";
                    return Json(response);
                }
            }
            catch (UnauthorizedAccessException e)
            {

                return RedirectToAction("Login", "Account", new { Area = "" });
            }
            catch (Exception e)
            {
                // Check if file exists with its full path    
                if (System.IO.File.Exists(path + "\\" + fileName))
                {
                    System.IO.File.Delete(path + "\\" + fileName);
                }
                return RedirectToAction("Error", "Account", new { Area = "" });
            }


        }  
      

    }
}




