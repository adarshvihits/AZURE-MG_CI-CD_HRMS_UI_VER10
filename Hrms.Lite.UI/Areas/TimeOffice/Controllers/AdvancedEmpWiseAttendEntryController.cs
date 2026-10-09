using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.TimeOffice;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice;
using Hrms.Lite.Shared.TimeOffice.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.TimeOffice.Controllers
{
    [Area("TimeOffice")]
    [SessionAuthorize]
    public class AdvancedEmpWiseAttendEntryController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly IDailyAttendanceEntryService _dailyAttendanceEntryService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
     

        public AdvancedEmpWiseAttendEntryController(IDropdownService dropdownService, ICommonService commonService, IDailyAttendanceEntryService dailyAttendanceEntryService, IHelpers helpers)
        {
            _dailyAttendanceEntryService = dailyAttendanceEntryService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;
        }
        public async Task<IActionResult> Index(int? PgGroup, int? Month)
        {
            try
            {
                DailyAttendanceEntry model = new DailyAttendanceEntry();               
                var payrollList = await _dropdownService.GetPayrollGroupWithGrpCmpyCheckDropdown();              
                ViewBag.Month = new SelectList(await _dropdownService.GetDailyAttendanceEntry_MonthDropdown(PgGroup), "Code", "Name");           
                ViewBag.PGroup = new SelectList(payrollList, "Code", "Name");
                model.Month = new Shared.TimeOffice.Generic.Master.Month();
                model.PayrollGroup = new Shared.Master.PayrollGroup();
                model.Month.Code = Month;model.PayrollGroup.Code = PgGroup;
                model.Filter = await _commonService.GetFilterDropdowns();
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

        [HttpPost]
        public async Task<IActionResult> GetEmployeeWiseList(Filter filter1, int Month, int PgGroup)
        {
            try
            {
                DailyAttendanceEntry model = new DailyAttendanceEntry(); 
                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }
                if (Month != 0 && PgGroup != 0)
                {
                    model = await _dailyAttendanceEntryService.EmployeeWiseDailyAttendanceEntryList(Month, filter1, PgGroup);

                }               
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.EmployeeWiseAttendanceList == null)
                {
                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }

                if (model.EmployeeWiseAttendanceList.Count > 0)
                {
                    totalrows = model.EmployeeWiseAttendanceList[0].active;                    
                }

                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = model.EmployeeWiseAttendanceList[0].totalRecords;
                    return Json(new
                    {
                        data = model.EmployeeWiseAttendanceList,
                        draw = filter1.draw,
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    return Json(new
                    {
                        data = model.EmployeeWiseAttendanceList,
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
        public async Task<IActionResult> EmployeeWiseAttendanceEdit(Guid EmployeeGI, int Month, int Pgroup)
        {
            try
            {
                DailyAttendanceEntry model = new DailyAttendanceEntry();               
                int ABSTATUS = 0;
                model = await _dailyAttendanceEntryService.EmployeeDailyAttendanceEntryEditDetails(Month, ABSTATUS, EmployeeGI);
                model.EmployeeGI = EmployeeGI;
                model.PayrollGroup = new Shared.Master.PayrollGroup();
                model.PayrollGroup.Code = Pgroup;                            
                    ViewBag.Month = new SelectList(await _dropdownService.GetDailyAttendanceEntry_MonthDropdown(Pgroup), "Code", "Name");
                ViewBag.PGroup = new SelectList(await _dropdownService.GetPayrollGroupWithGrpCmpyCheckDropdown(), "Code", "Name");
                model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);
                ViewBag.COFF = new SelectList(new List<SelectListItem>
                        {
                            new SelectListItem { Value = "", Text = "--Select--" }
                        }, "Value", "Text");

                ViewBag.AttendenceStatus = new SelectList(await _dropdownService.GetAttendanceStatusDropdown(), "Code", "Name");
             
                return View("EmployeeWiseAttendanceEditIndex", model);
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
        public async Task<JsonResult> GetCurrentMonth()
        {
            var data = await _commonService.GetCurrentMonthDropdown();
            return Json(data);

        }
        [HttpGet]
        public async Task<JsonResult> GetCOffDates(Guid EmployeeGI, string Date, string Mode)
        {
            var data = await _dropdownService.GetCOFFEntitlesDropDown(EmployeeGI, Date, Mode);
            return Json(data);

        }

        [HttpPost]
        public async Task<IActionResult> EditSave(DailyAttendanceEditDetails input)
        {
            try
            {
                DailyAttendanceEntry model = new DailyAttendanceEntry();
                model.DailyAttendanceEditDetails = input;                
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
//SHOW ABSENT DAYS
        public async Task<IActionResult> EmployeeWiseAttendanceEditList(Guid EmployeeGI, int Month, int ABSTATUS)
        {
            try
            {
                DailyAttendanceEntry model = new DailyAttendanceEntry();
                model = await _dailyAttendanceEntryService.EmployeeDailyAttendanceEntryEditDetails(Month, ABSTATUS, EmployeeGI);
                ViewBag.COFF = new SelectList(new List<SelectListItem>
                        {
                            new SelectListItem { Value = "", Text = "--Select--" }
                        }, "Value", "Text");

                ViewBag.AttendenceStatus = new SelectList(await _dropdownService.GetAttendanceStatusDropdown(), "Code", "Name");
                model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);
                return PartialView("_EmployeeWiseAttendanceEditList", model);
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

        public async Task<IActionResult> BatchUpdate(Guid EmployeeGI, string MonthName)
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
                model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);
                ViewBag.AttendenceStatus = new SelectList(await _dropdownService.GetAttendanceStatusDropdown(), "Code", "Name");

                return PartialView("_BatchUpdate", model);
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
        public async Task<IActionResult> BatchUpdateSave(DailyAttendanceEntry model)
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

    }
}
