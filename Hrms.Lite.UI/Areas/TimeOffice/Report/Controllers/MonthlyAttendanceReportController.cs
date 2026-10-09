using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.TimeOffice;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice;
using Hrms.Lite.Shared.TimeOffice.Generic;
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

namespace Hrms.Lite.UI.Areas.TimeOffice.Report.Controllers
{
    [Area("TimeOffice")]
    [SessionAuthorize]
    public class MonthlyAttendanceReportController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly IDailyAttendanceEntryService _dailyAttendanceEntryService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        public static class Global
        {
            public static int Global_MonthCode;
            public static int Global_PayRollGrpCode;
            public static int Global_TabIndex;
            public static string Month;
            public static string PayRollGrp;

            public static string Global_Headerdate;
            public static int Global_AttendanceStatus;
            public static Filter Global_Filter;
            public static decimal AllCount;
            public static decimal AbsenteesamCount;
            public static decimal LeaveCount;



        }


        public MonthlyAttendanceReportController(IDropdownService dropdownService, ICommonService commonService, IDailyAttendanceEntryService dailyAttendanceEntryService, IHelpers helper)
        {
            _dailyAttendanceEntryService = dailyAttendanceEntryService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helper;


        }

        public async Task<IActionResult> Index(int? Month, int? PayrollGrp, int? TabIndex)
        {
            DailyAttendanceEntryPaging model = new DailyAttendanceEntryPaging();
            ViewBag.FormName = "outerList";
            var payrollList = await _dropdownService.GetPayrollGroupWithGrpCmpyCheckDropdown();
            model.Month = new Shared.TimeOffice.Generic.Master.Month();
            model.PayrollGroup = new Shared.Master.PayrollGroup();
            Global.Global_Filter = new Filter();

            ViewBag.PayRollGroup = new SelectList(payrollList, "Code", "Name", PayrollGrp);
            if ((PayrollGrp ?? 0) == 0)
            {
                //ViewBag.Month = new SelectList(await _dropdownService.GetMonthly_AttendanceProcessing_MonthDropdown(payrollList[0].Code), "Code", "Name", Month);
                ViewBag.Month = new SelectList(await _dropdownService.GetMonthDropdown(), "Code", "Name", Month);
            }
            else
                //ViewBag.Month = new SelectList(await _dropdownService.GetMonthly_AttendanceProcessing_MonthDropdown(PayrollGrp.Value), "Code", "Name", Month);
                ViewBag.Month = new SelectList(await _dropdownService.GetMonthDropdown(), "Code", "Name", Month);

            model.TabIndex = 0;
            //int? month = currMonth.Data;
            if (Month == null && PayrollGrp == null)
            {
                model.PayrollGroup.Code = payrollList[0].Code;
                var currMonth = await _commonService.GetCurrentMonthDropdown();
                model.Month.Code = currMonth.Data;

            }


            return View("~/Areas/TimeOffice/Report/Views/MonthlyAttendanceReport/Index.cshtml", model);



        }

        public async Task<IActionResult> ValidateAttendanceStatus(int AttnStatus)
        {
            try
            {
                var details = await _commonService.GetDailyAttendanceBatchUpdateLeaveValidation(AttnStatus);
                return Json(details);
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
        public async Task<IActionResult> GetMonth(int PayrollGrp)
        {
            try
            {
                var Months = await _dropdownService.GetMonthly_AttendanceProcessing_MonthDropdown(PayrollGrp);
                return Json(Months);
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
        public async Task<IActionResult> CheckIffCOFF(int AttendanceStatusCode)
        {
            try
            {
                var Details = await _commonService.GetAttendanceStatusType(AttendanceStatusCode);
                return Json(Details);
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
        public async Task<IActionResult> GetList(Filter filter1, int month, int PayrollGroup, int TabIndex)
        {
            try
            {
                DailyAttendanceEntryPaging model = new DailyAttendanceEntryPaging();
                DailyAttendanceEntryPaging m = new DailyAttendanceEntryPaging();
                //model.EmployeeHeader = new EmployeeHeader();




                Filter filter = new Filter();
                Global.Global_Filter = filter1;


                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }
                if (month != 0 && PayrollGroup != 0)
                    model = await _dailyAttendanceEntryService.DailyAttendanceEntryPagingList(month, PayrollGroup, TabIndex, filter1);

                Global.AllCount = model.AllCount;
                Global.AbsenteesamCount = model.AbsenteesamCount;
                Global.LeaveCount = model.LeaveCount;
                Global.Global_MonthCode = month;
                Global.Global_PayRollGrpCode = PayrollGroup;
                Global.Global_TabIndex = TabIndex;

                m.DailyAttendanceList = model.DailyAttendanceList;
                m.DailyAttendanceFooterList = model.DailyAttendanceFooterList;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.DailyAttendanceList == null || model.DailyAttendanceList.Count() == 0)
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




                if (model.DailyAttendanceList.Count > 0)
                {
                    totalrows = m.DailyAttendanceList[0].active;

                }

                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.DailyAttendanceList[0].totalRecords;
                    return Json(new
                    {
                        data = model.DailyAttendanceList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows,
                        var1 = model.DailyAttendanceFooterList,
                        all = model.AllCount,
                        absenteeism = model.AbsenteesamCount,
                        Count = model,
                        leave = model.LeaveCount

                    });
                }
                else
                {
                    return Json(new
                    {
                        data = model.DailyAttendanceList,

                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows,
                        var1 = model.DailyAttendanceFooterList,
                        all = model.AllCount,
                        absenteeism = model.AbsenteesamCount,
                        Count = model,
                        leave = model.LeaveCount
                    });
                }


            }
            catch (Exception ex)
            {
                return View();
            }




        }


        public async Task<IActionResult> DailyAttendanceEntryList(int month, int PayRollGrp, int TabIndex, string MonthName, string payroll)
        {
            try
            {
                Filter filter = new Filter();
                if (Global.Global_Filter != null)
                {
                    filter = Global.Global_Filter;

                }
                //-----global variable  Initialization starts ----------------------------

                Global.Global_MonthCode = month;
                Global.Global_PayRollGrpCode = PayRollGrp;
                Global.Global_TabIndex = TabIndex;
                Global.Month = MonthName;
                Global.PayRollGrp = payroll;

                //------------------------------------------------------------------------


                DailyAttendanceEntry model = new DailyAttendanceEntry();
                model = await _dailyAttendanceEntryService.DailyAttendanceEntryList(month, PayRollGrp, TabIndex, filter);
                model.Month = new Shared.TimeOffice.Generic.Master.Month();
                model.PayrollGroup = new Shared.Master.PayrollGroup();
                model.Month.Code = month;
                model.PayrollGroup.Code = PayRollGrp;
                model.TabIndex = TabIndex;

                //TempData["Global_Filter_DailyAttendanceEntry"] = JsonConvert.SerializeObject(filter);
                return PartialView("~/Areas/TimeOffice/Report/Views/MonthlyAttendanceReport/_DailyAttendanceEntryList.cshtml", model);
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
        public async Task<IActionResult> Filter(string FormName)
        {
            if (FormName == "outerList")
                ViewBag.FormName = "outerList";
            else
                ViewBag.FormName = "innerList";
            Filter filter = new Filter();
            filter = await _commonService.GetFilterDropdowns();

            return PartialView("~/Views/Shared/_TimeOffice_Payroll_EmployeeFilter.cshtml", filter);
        }


        [HttpPost]
        public async Task<IActionResult> ApplyFilter(Filter filter)
        {
            try
            {
                DailyAttendanceEntry model = new DailyAttendanceEntry();
                model = await _dailyAttendanceEntryService.DailyAttendanceEntryList(Global.Global_MonthCode, Global.Global_PayRollGrpCode, Global.Global_TabIndex, filter);
                Global.Global_Filter = filter;

                model.Month = new Shared.TimeOffice.Generic.Master.Month();
                model.PayrollGroup = new Shared.Master.PayrollGroup();
                model.Month.Code = Global.Global_MonthCode;
                model.PayrollGroup.Code = Global.Global_PayRollGrpCode;
                model.TabIndex = Global.Global_TabIndex;
                return PartialView("~/Areas/TimeOffice/Report/Views/MonthlyAttendanceReport/_DailyAttendanceEntryList.cshtml", model);
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
        public async Task<IActionResult> ApplyFilterEmpWiseAttendance(Filter filter)
        {
            try
            {
                DailyAttendanceEntry model = new DailyAttendanceEntry();
                model = await _dailyAttendanceEntryService.DailyAttendanceEntryEditList(Global.Global_Headerdate, Global.Global_PayRollGrpCode, Global.Global_AttendanceStatus, filter);

                Global.Global_Filter = filter;
                model.Month = new Shared.TimeOffice.Generic.Master.Month();

                model.PayrollGroup = new Shared.Master.PayrollGroup();
                model.Month.Code = Global.Global_MonthCode;
                model.PayrollGroup.Code = Global.Global_PayRollGrpCode;
                model.TabIndex = Global.Global_TabIndex;
                return PartialView("~/Areas/TimeOffice/Report/Views/MonthlyAttendanceReport/_DailyAttendanceEditListPartial.cshtml", model);
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


        public async Task<IActionResult> DailyAttendanceEditList(string HeaderDate)
        {
            try
            {
                DailyAttendanceEntry model = new DailyAttendanceEntry();
                Filter filter = new Filter();

                ViewBag.FormName = "innerList";
                //ViewBag.selectedDate= HeaderDate;
                DateTime DT = Convert.ToDateTime(HeaderDate);
                ViewBag.selectedDate = DT.ToString("dd-MMM-yyyy");
                //ViewBag.BatchupdateDate=DT.ToString("yyyy-MM-dd");
                if (Global.Global_Filter.Basic_Filter == null && Global.Global_Filter.Optional_Filter == null)
                {
                    ViewBag.IsFilterApplied = 0;
                }
                else
                {
                    filter = Global.Global_Filter;
                    ViewBag.IsFilterApplied = 1;
                }
                ViewBag.AttendenceStatus = new SelectList(await _dropdownService.GetAttendanceStatusDropdown(), "Code", "Name");


                int AttendanceStatusCode = 0; //All

                //model = await _dailyAttendanceEntryService.DailyAttendanceEntryEditList(HeaderDate,Global.Global_PayRollGrpCode, AttendanceStatusCode, filter);


                //-----------------Setting  global variables------------------------------
                Global.Global_AttendanceStatus = AttendanceStatusCode;
                Global.Global_Headerdate = HeaderDate;


                //----------------------------------------------------------------------
                model.Month = new Shared.TimeOffice.Generic.Master.Month();
                model.PayrollGroup = new Shared.Master.PayrollGroup();
                //---------------Fetching global variable values--------------------------- 
                model.TabIndex = Global.Global_TabIndex;
                model.PayrollGroup.Code = Global.Global_PayRollGrpCode;
                model.Month.Code = Global.Global_MonthCode;

                //-----------------------------------------------------------------------------
                //TempData["Global_Filter_DailyAttendanceEntry"] = JsonConvert.SerializeObject(filter);

                model.Filter = await _commonService.GetFilterDropdowns();
                return View("~/Areas/TimeOffice/Report/Views/MonthlyAttendanceReport/DailyAttendanceEditList.cshtml", model);
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
        public async Task<IActionResult> GetEditList(Filter filter1, int AttendanceStatusCode)
        {
            try
            {
                DailyAttendanceEntry model = new DailyAttendanceEntry();
                DailyAttendanceEntry m = new DailyAttendanceEntry();
                //model.EmployeeHeader = new EmployeeHeader();

                Filter filter = new Filter();
                if (Global.Global_Filter != null)
                    filter = Global.Global_Filter;
                //if (AttendanceStatusCode == null)
                //{
                //    AttendanceStatusCode = 0;
                //}


                Global.Global_Filter = filter1;
                Global.Global_AttendanceStatus = AttendanceStatusCode;
                //Global.Global_Headerdate = HeaderDate;
                //--------------------------------------------------------------------


                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }
                model = await _dailyAttendanceEntryService.DailyAttendanceEntryEditList(Global.Global_Headerdate, Global.Global_PayRollGrpCode, Global.Global_AttendanceStatus, filter1);


                m.DailyAttendanceEditList = model.DailyAttendanceEditList;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.DailyAttendanceEditList == null || model.DailyAttendanceEditList.Count() == 0)
                {


                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,
                        recordsTotal = totalrows,
                        selectedDate = model.SelectedDate,
                        recordsFiltered = totalrows
                    });
                }


                if (model.DailyAttendanceEditList.Count > 0)
                {
                    totalrows = m.DailyAttendanceEditList[0].active;

                }


                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.DailyAttendanceEditList[0].totalRecords;

                    return Json(new
                    {
                        data = model.DailyAttendanceEditList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        selectedDate = model.SelectedDate,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {

                    return Json(new
                    {
                        data = model.DailyAttendanceEditList,
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        selectedDate = model.SelectedDate,
                        recordsFiltered = totalrows
                    });
                }






            }

            catch (Exception ex)
            {
                return View();
            }

        }

        public async Task<IActionResult> DailyAttendanceEditListPartial(string HeaderDate, int? AttendanceStatusCode)
        {
            try
            {
                DailyAttendanceEntry model = new DailyAttendanceEntry();
                Filter filter = new Filter();
                if (Global.Global_Filter != null)
                    filter = Global.Global_Filter;
                if (AttendanceStatusCode == null)
                    AttendanceStatusCode = 0;
                //-------------------------setting global variables-------------------------------------------

                Global.Global_AttendanceStatus = AttendanceStatusCode.Value;
                Global.Global_Headerdate = HeaderDate;
                //--------------------------------------------------------------------
                model = await _dailyAttendanceEntryService.DailyAttendanceEntryEditList(HeaderDate, Global.Global_PayRollGrpCode, AttendanceStatusCode.Value, filter);

                //TempData["Global_Filter_DailyAttendanceEntry"] = JsonConvert.SerializeObject(filter);
                return PartialView("~/Areas/TimeOffice/Report/Views/MonthlyAttendanceReport/_DailyAttendanceEditListPartial.cshtml", model);
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


        public async Task<IActionResult> DailyAttendanceEdit(Guid EmployeeGI, string HeaderDate, string Mode)
        {
            try
            {
                DailyAttendanceEntry model = new DailyAttendanceEntry();
                model = await _dailyAttendanceEntryService.DailyAttendanceEntryEditDetails(HeaderDate, EmployeeGI);
                ViewBag.AttendenceStatus = new SelectList(await _dropdownService.GetAttendanceStatusDropdown(), "Code", "Name");
                ViewBag.CoffEntitleDates = new SelectList(await _dropdownService.GetCOFFEntitlesDropDown(EmployeeGI, HeaderDate, Mode), "Name", "Name");

                return PartialView("~/Areas/TimeOffice/Report/Views/MonthlyAttendanceReport/_DailyAttendanceEdit.cshtml", model);
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
        public async Task<IActionResult> EmployeeLeaveDetails(Guid EmployeeGI)
        {
            try
            {
                DailyAttendanceEntry model = new DailyAttendanceEntry();
                model = await _dailyAttendanceEntryService.EmployeeLeaveDetails(Global.Global_MonthCode, EmployeeGI);

                return PartialView("~/Areas/TimeOffice/Report/Views/MonthlyAttendanceReport/_LeaveDetailsPopup.cshtml", model);
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
        public async Task<IActionResult> DailyattendanceEntryBatchUpdate(DailyAttendanceEntry model)
        {
            try
            {

                model.Filter = Global.Global_Filter;
                var response = await _dailyAttendanceEntryService.DailyattendanceEntryBatchUpdate(model);
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



        public async Task<IActionResult> EmployeeWiseAttendance(Guid EmployeeGI, int Month)
        {
            try
            {
                DailyAttendanceEntry model = new DailyAttendanceEntry();
                int ABSTATUS = 0;
                model = await _dailyAttendanceEntryService.EmployeeDailyAttendanceEntryEditDetails(Month, ABSTATUS, EmployeeGI);

                //---------------Fetching global variable values--------------------------- 
                model.TabIndex = Global.Global_TabIndex;

                //-----------------------------------------------------------------------------
                model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);
                ViewBag.Month = new SelectList(await _dropdownService.GetMonthDropdown(), "Code", "Name");
                model.EmployeeGI = EmployeeGI;

                return View("~/Areas/TimeOffice/Report/Views/MonthlyAttendanceReport/EmployeeWiseAttendance.cshtml", model);
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

        public async Task<IActionResult> EmployeeWiseAttendanceList(Guid EmployeeGI, int Month, int ABSTATUS)
        {
            try
            {
                DailyAttendanceEntry model = new DailyAttendanceEntry();
                model = await _dailyAttendanceEntryService.EmployeeDailyAttendanceEntryEditDetails(Month, ABSTATUS, EmployeeGI);
                model.Month = new Shared.TimeOffice.Generic.Master.Month();
                model.PayrollGroup = new Shared.Master.PayrollGroup();
                //---------------Fetching global variable values--------------------------- 
                model.TabIndex = Global.Global_TabIndex;
                model.PayrollGroup.Code = Global.Global_PayRollGrpCode;
                model.Month.Code = Global.Global_MonthCode;
                model.EmployeeGI = EmployeeGI;
                return PartialView("~/Areas/TimeOffice/Report/Views/MonthlyAttendanceReport/_EmployeeWiseAttendanceList.cshtml", model);
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

        public async Task<IActionResult> EmployeeWiseDailyAttendanceBatchUpdate(Guid EmployeeGI, string MonthName)
        {
            try
            {
                DailyAttendanceEntry model = new DailyAttendanceEntry();
                model.Employee_DailyAttendanceBatchUpdate = new Employee_DailyAttendanceBatchUpdate();
                model.Employee_DailyAttendanceBatchUpdate.FirstHalf = new AttnDayStatus();
                model.Employee_DailyAttendanceBatchUpdate.SecondHalf = new AttnDayStatus();
                model.Employee_DailyAttendanceBatchUpdate.Month = new Shared.TimeOffice.Generic.Master.Month();

                model.Employee_DailyAttendanceBatchUpdate.EmployeeGI = EmployeeGI;
                model.Employee_DailyAttendanceBatchUpdate.Month.Name = MonthName;
                model.Employee_DailyAttendanceBatchUpdate.PayrollGroup = new Shared.Master.PayrollGroup();
                model.Employee_DailyAttendanceBatchUpdate.PayrollGroup.Name = Global.PayRollGrp;

                ViewBag.AttendenceStatus = new SelectList(await _dropdownService.DailyAttnEntryBatchUpDateLeaveDropDown(), "Code", "Name");

                return PartialView("~/Areas/TimeOffice/Report/Views/MonthlyAttendanceReport/_EmployeeWiseAttendanceBatchUpdate.cshtml", model);
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
        public async Task<IActionResult> EmployeeWiseAttendanceEntryBatchUpdate(DailyAttendanceEntry model)
        {
            try
            {

                var response = await _dailyAttendanceEntryService.EmployeeWiseAttendanceEntryBatchUpdate(model);
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

        public async Task<IActionResult> EmployeeDailyAttendanceEdit(Guid EmployeeGI, string Date, string Mode)
        {
            try
            {
                DailyAttendanceEntry model = new DailyAttendanceEntry();
                model = await _dailyAttendanceEntryService.DailyAttendanceEntryEditDetails(Date, EmployeeGI);

                ViewBag.AttendenceStatus = new SelectList(await _dropdownService.GetAttendanceStatusDropdown(), "Code", "Name");
                ViewBag.CoffEntitleDates = new SelectList(await _dropdownService.GetCOFFEntitlesDropDown(EmployeeGI, Date, Mode), "Name", "Name");
                return PartialView("~/Areas/TimeOffice/Report/Views/MonthlyAttendanceReport/_EmployeeWiseAttendanceEdit.cshtml", model);
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
        public async Task<IActionResult> DailyAttendanceBatchUpdate()
        {
            try
            {
                DailyAttendanceEntry model = new DailyAttendanceEntry();
                model.PayrollGroup = new Shared.Master.PayrollGroup();
                model.BatchUpDate = new BatchUpDate();
                model.BatchUpDate.FirstHalfNew = new AttnDayStatus();
                model.BatchUpDate.FirstHalfOld = new AttnDayStatus();
                model.BatchUpDate.SecondHalfOld = new AttnDayStatus();
                model.BatchUpDate.SecondHalfNew = new AttnDayStatus();
                //model.BatchUpDate.Date = Convert.ToDateTime(Date);
                model.BatchUpDate.Date = Convert.ToDateTime(Global.Global_Headerdate);
                model.PayrollGroup.Code = Global.Global_PayRollGrpCode;
                ViewBag.AttendenceStatus = new SelectList(await _dropdownService.DailyAttnEntryBatchUpDateLeaveDropDown(), "Code", "Name");

                return PartialView("~/Areas/TimeOffice/Report/Views/MonthlyAttendanceReport/_DailyAttendanceBatchUpdate.cshtml", model);
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
        public async Task<IActionResult> DailyAttendanceEditSave(DailyAttendanceEntry model)
        {
            try
            {
                var response = await _dailyAttendanceEntryService.DailyattendanceEntry(model);
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

        public async Task<IActionResult> ExportToexcel_DailyAttendanceEntryList(string MName, string PGroup, int totRecords)
        {
            try
            {

                Filter filter = new Filter();
                if (Global.Global_Filter == null)
                    Global.Global_Filter = new Filter();
                else
                {
                    Global.Global_Filter.length = totRecords;
                    Global.Global_Filter.start = 0;
                }
                string FilterValues = await _helpers.GetFilterValues(Global.Global_Filter);
                //TempData["Global_Filter_DailyAttendanceEntry"] = JsonConvert.SerializeObject(filter);

                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Monthly Attendance Report (" + PGroup + ")", "Excel");

                if (FilterValues == "")
                {
                    FilterValues = "All";
                }

                string currDateTime = await _helpers.GetCurrentDate();
                

                //Get Datset
                var dataset = await _dailyAttendanceEntryService.DailyAttendanceEntryList_Excel(Global.Global_MonthCode, Global.Global_PayRollGrpCode, Global.Global_TabIndex, Global.Global_Filter);
                DataTable dt = dataset.Tables[0];

                dt.Columns.RemoveAt(0);

                dt.Columns.RemoveAt(0);
                //set EXCEL Column Heading 
                for (int i = 0; i < dt.Columns.Count; i++)
                {


                    dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                }

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Monthly Attendance Report");
                    int rowcount = dt.Rows.Count + 5;

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
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                    wsReportNameHeaderRange.Value = "Monthly Attendance Report";

                    if (Global.Global_TabIndex == 0)
                        wsReportNameHeaderRange.Value = "Monthly Attendance Report (All)";
                    else if (Global.Global_TabIndex == 1)
                        wsReportNameHeaderRange.Value = "Monthly Attendance Report (Absentism)";
                    else
                        wsReportNameHeaderRange.Value = "Monthly Attendance Report (Leave)";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();


                    worksheet.Cell(2, 3).Style.Font.Bold = true;
                    worksheet.Cell(2, 3).Value = "Month";
                    worksheet.Cell(2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(2, 4).Value = MName;
                    worksheet.Cell(2, 5).Style.Font.Bold = true;
                    worksheet.Cell(2, 5).Value = "Payroll Group";
                    worksheet.Cell(2, 6).Value = PGroup;

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
                    worksheet.Range("B4:AH4").Merge();
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
                            book.Protect(EP.Password,false,false);
                            book.SaveToStream(stream);
                        }
                        var content = stream.ToArray();
                        return File(
                            content,
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            "Monthly Attendance Report.xlsx"
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







        public async Task<IActionResult> ExportToExcel_DailyAttendanceEDitList(string HeaderDate, int? AttendanceStatusCode, string AttendanceStatusName, int totalRows)
        {
            try
            {
                DailyAttendanceEntry model = new DailyAttendanceEntry();

                Filter filter = new Filter();


                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Monthly Attendance Report (" + AttendanceStatusName + ")", "Excel");
                int slNo = 0;
                if (Global.Global_Filter == null)
                    Global.Global_Filter = filter;


                var filterLength = Global.Global_Filter.length;
                Global.Global_Filter.length = totalRows;
                Global.Global_Filter.start = 0;

                string FilterValues = await _helpers.GetFilterValues(Global.Global_Filter);

                if (FilterValues == "")
                {
                    FilterValues = "All";
                }
                string currDateTime = await _helpers.GetCurrentDate();
                

                if ((AttendanceStatusCode ?? 0) == 0)
                    AttendanceStatusCode = 0;
                model = await _dailyAttendanceEntryService.DailyAttendanceEntryEditList(Global.Global_Headerdate, Global.Global_PayRollGrpCode, AttendanceStatusCode.Value, Global.Global_Filter);
                Global.Global_Filter.length = filterLength;


                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Monthly Attendance Report");
                    int count = model.DailyAttendanceEditList.Count + 5;


                    worksheet.Range("A1:L" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:L" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:L" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:L" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:L" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 11)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Value = "Monthly Attendance Report";


                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                    worksheet.Cell(2, 3).Style.Font.Bold = true;
                    worksheet.Cell(2, 3).Value = "Selected Date";
                    worksheet.Cell(2, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(2, 4).Value = HeaderDate;
                    worksheet.Cell(2, 5).Style.Font.Bold = true;
                    worksheet.Cell(2, 5).Value = "Attendance Status";
                    if ((AttendanceStatusCode ?? 0) == 0)
                        worksheet.Cell(2, 6).Value = "All";
                    else
                        worksheet.Cell(2, 6).Value = AttendanceStatusName;

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
                    worksheet.Cell(3, 6).Value = model.DailyAttendanceEditList.Count;
                    worksheet.Cell(4, 1).Style.Font.Bold = true;
                    worksheet.Cell(4, 1).Value = "Filter Applied";
                    worksheet.Range("B4:L4").Merge();
                    worksheet.Cell("B4").Value = FilterValues;








                    var currentRow = 5;
                    #region Header
                    worksheet.Range("A5:L5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Cell(currentRow, 1).Value = "Sl.No";
                    worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 2).Value = "ID";
                    worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 3).Value = "EmployeeName";
                    worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 4).Value = "Department";
                    worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 5).Value = "Location";
                    worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 6).Value = "Sh.Code";
                    worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 7).Value = "IN";
                    worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 8).Value = "OUT";
                    worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 9).Value = "Tot.Time";
                    worksheet.Cell(currentRow, 9).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 10).Value = "FH";
                    worksheet.Cell(currentRow, 10).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 11).Value = "SH";
                    worksheet.Cell(currentRow, 11).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 12).Value = "Remarks";
                    worksheet.Cell(currentRow, 12).Style.Font.Bold = true;


                    #endregion

                    #region Body
                    foreach (var item in model.DailyAttendanceEditList)
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
                        worksheet.Cell(currentRow, 4).Value = item.Department.Name;
                        worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 5).Value = item.Location.Name;
                        worksheet.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 6).Value = item.Shift.Name;
                        worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 7).Value = item.IN;
                        worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 8).Value = item.OUT;
                        worksheet.Cell(currentRow, 9).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 9).Value = item.TotalTime;
                        worksheet.Cell(currentRow, 10).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 10).Value = item.FirstHalf.Name;
                        worksheet.Cell(currentRow, 11).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 11).Value = item.SecondHalf.Name;
                        worksheet.Cell(currentRow, 12).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 12).Value = item.Remarks;

                        worksheet.Columns(1, 12).Style.Alignment.WrapText = true;
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
                            book.Protect(EP.Password,false,false);
                            book.SaveToStream(stream);
                        }
                        var content = stream.ToArray();
                        return File(
                            content,
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            "Monthly Attendance Report.xlsx"
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
            //}
        }

        public async Task<IActionResult> ExportToExcel_EmployeeAttendanceEDitList(Guid EmployeeGI, int Month, int ABSTATUS, string MonthName)
        {
            try
            {
                DailyAttendanceEntry model = new DailyAttendanceEntry();

                //Filter filter = new Filter();
                //if (TempData["Global_Filter_DailyAttendanceEntry"] != null)
                //{
                //    filter = JsonConvert.DeserializeObject<Filter>(TempData["Global_Filter_DailyAttendanceEntry"].ToString());
                //}

                //TempData["Global_Filter_DailyAttendanceEntry"] = JsonConvert.SerializeObject(model);

                var LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Monthly Attendance Summary (" + MonthName + ")", "Excel");
                int slNo = 0;
                model = await _dailyAttendanceEntryService.EmployeeDailyAttendanceEntryEditDetails(Month, ABSTATUS, EmployeeGI);
                string currDateTime = await _helpers.GetCurrentDate();
                
                model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);


                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Monthly Attendance Summary");
                    int count = model.Employee_DailyAttendanceEditDetails.Count + 4;


                    worksheet.Range("A1:J" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:J" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:J" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:J" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Range("A1:J" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                    var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 9)));
                    wsReportNameHeaderRange.Style.Font.Bold = true;
                    wsReportNameHeaderRange.Style.Font.FontSize = 15;
                    wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    wsReportNameHeaderRange.Merge();
                    wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                    if (ABSTATUS == 1)
                        wsReportNameHeaderRange.Value = "Monthly Attendance Summary(Absent Days Only)";
                    else
                        wsReportNameHeaderRange.Value = "Monthly Attendance Summary";

                    worksheet.Cell(2, 1).Style.Font.Bold = true;
                    worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                    worksheet.Cell(2, 3).Style.Font.Bold = true;
                    worksheet.Cell(2, 3).Value = "Employee Name";
                    worksheet.Cell(2, 4).Value = model.EmployeeHeader.EmployeeName;
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
                    worksheet.Cell(3, 6).Value = model.Employee_DailyAttendanceEditDetails.Count;
                    var currentRow = 4;
                    #region Header
                    worksheet.Range("A4:J4").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Cell(currentRow, 1).Value = "Sl.No";
                    worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 2).Value = "Date";
                    worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 3).Value = "Day";
                    worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 4).Value = "Sh.Code";
                    worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 5).Value = "IN";
                    worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 6).Value = "OUT";
                    worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 7).Value = "Tot.Time";
                    worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 8).Value = "FH";
                    worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 9).Value = "SH";
                    worksheet.Cell(currentRow, 9).Style.Font.Bold = true;
                    worksheet.Cell(currentRow, 10).Value = "Remarks";
                    worksheet.Cell(currentRow, 10).Style.Font.Bold = true;


                    #endregion

                    #region Body
                    foreach (var item in model.Employee_DailyAttendanceEditDetails)
                    {
                        currentRow++;
                        slNo = slNo + 1;
                        worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 1).Value = slNo;
                        worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 2).Value = item.Date;
                        worksheet.Cell(currentRow, 2).Style.DateFormat.Format = "dd-MMM-yyyy";
                        worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 3).Value = item.Day;
                        worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 4).Value = item.Shift.Name;
                        worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 5).Value = item.IN;
                        worksheet.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 6).Value = item.OUT;
                        worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 7).Value = item.TotalTime;
                        worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 8).Value = item.FirstHalf.Name;
                        worksheet.Cell(currentRow, 9).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 9).Value = item.SecondHalf.Name;
                        worksheet.Cell(currentRow, 10).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(currentRow, 10).Value = item.Remarks;

                        worksheet.Columns(1, 10).Style.Alignment.WrapText = true;
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
                            book.Protect(EP.Password,false,false);
                            book.SaveToStream(stream);
                        }
                        var content = stream.ToArray();
                        return File(
                            content,
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            "Monthly Attendance Summary.xlsx"
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
            //}
        }

    }
}
