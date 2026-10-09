using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.TimeOffice;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice;
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
    public class IndividualAttendanceProcessingController : Controller
    {

        private readonly IMonthlyAttendanceProcessingService _Service;
        private readonly ICommonService _commonService;
        private readonly IDropdownService _dropdownService;

        public IndividualAttendanceProcessingController(IMonthlyAttendanceProcessingService Service, ICommonService commonService, IDropdownService dropdownService)
        {
            _Service = Service;
            _dropdownService = dropdownService;
            _commonService = commonService;

        }

        public async Task<IActionResult> Index()
        {
            try
            {
                MonthlyAttendanceProcessing model = new MonthlyAttendanceProcessing();
                ViewBag.Month = new SelectList(await _dropdownService.GetMonthly_AttendanceProcessing_MonthDropdown(0), "Code", "Name");
                var currMonth = await _commonService.GetCurrentMonthDropdown();
                model.Month = new Shared.TimeOffice.Generic.Master.Month();
                model.Month.Code = currMonth.Data;
                model.EmployeeHeader = new EmployeeHeader();
                ViewBag.Employee = new SelectList(await _dropdownService.GetIndividual_ProcessingEmployeeDropDown(0, currMonth.Data), "Code", "Name");
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


        public async Task<IActionResult> GetIndividual_AttendanceProcessingValidations(int Month, int EmployeeCode)
        {
            try
            {
                MonthlyAttendanceProcessing model = new MonthlyAttendanceProcessing();
                model = await _Service.Individual_MonthlyAttendanceProcessingList(Month, EmployeeCode, 0);
                model.Month.Code = Month;
                model.Employee.Code = EmployeeCode;
                return PartialView("_PopUp", model);
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
        public async Task<IActionResult> Process(MonthlyAttendanceProcessing model)
        {
            try
            {
                var response = await _Service.Individual_ProcessingSave(model);
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

        public async Task<IActionResult> Individual_MonthlyAttendanceProcessingList(int Month, int EmployeeCode, Guid EmployeeGI)
        {
            try
            {
                MonthlyAttendanceProcessing model = new MonthlyAttendanceProcessing();
                model = await _Service.Individual_MonthlyAttendanceProcessingList(Month, EmployeeCode, 1);
                model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);
                return PartialView("_List", model);
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
          public async Task<IActionResult> GetEmployeeDropdown(int Month, int EmpStatus)
          {
            try
            {
                var EmployeeDropdown = await _dropdownService.GetIndividual_ProcessingEmployeeDropDown(EmpStatus,Month);
                return Json(EmployeeDropdown);
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
