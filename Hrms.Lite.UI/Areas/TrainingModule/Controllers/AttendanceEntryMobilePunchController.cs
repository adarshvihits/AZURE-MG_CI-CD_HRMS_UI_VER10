using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.TrainingModule;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TrainingModule;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.TrainingModule.Controllers
{
    [Area("TrainingModule")]
    [SessionAuthorize]
    public class AttendanceEntryMobilePunchController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly ICommonService _commonService;
        private readonly IAttendanceEntryMobilePunchService _AttendanceEntryMobilePunchService;
        private readonly IHelpers _helpers;
        public AttendanceEntryMobilePunchController(IAttendanceEntryMobilePunchService trnParticipantService, IDropdownService dropdownService, ICommonService commonService, IHelpers helpers)
        {
            _AttendanceEntryMobilePunchService = trnParticipantService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;
        }

        public async Task<IActionResult> Index()
        {

            AttendanceEntryMobilePunch model = new AttendanceEntryMobilePunch();
            model.Filter = await _commonService.GetFilterDropdowns();
            ViewBag.Schedule = new SelectList(await _dropdownService.GetTrnScheduleDropdown(), "Code", "Name");
            Filter filter = new Filter();

            //model = await _trnParticipantService.GetList(Month, filter);
            //model.Filter = filter;
            //return View("~/Areas/PayRoll/Reports/Views/SalaryReleaseReport/Index.cshtml", model);
            return View(model);
        }

        public async Task<ActionResult> GetList(int ScheduleCode, Filter filter, int radiomode,int radio1)
        {
            try
            {
                AttendanceEntryMobilePunch model = new AttendanceEntryMobilePunch();

                model = await _AttendanceEntryMobilePunchService.GetScheduleList(ScheduleCode, filter, radiomode,radio1);

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

        public async Task<ActionResult> Save(int ScheduleCode, AttendanceEntryMobilePunch model, int radiomode)
        {
            try
            {
                model.completionmode = "MULTIPLE";

                var response = await _AttendanceEntryMobilePunchService.Save(ScheduleCode, model, radiomode);

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

        public async Task<ActionResult> EditChange(int employeecode, int status, int radiomode)
        {
            try
            {
                AttendanceEntryMobilePunch model = new AttendanceEntryMobilePunch();
                model.EmployeeCode = employeecode;
                model.completionmode = "SINGLE";

                var response = await _AttendanceEntryMobilePunchService.Save(status, model, radiomode);

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

        public async Task<ActionResult> GetTopicsPopUp(int ScheduleCode, int EmployeeCode)
        {
            try
            {

                AttendanceEntryMobilePunch model = new AttendanceEntryMobilePunch();

                model = await _AttendanceEntryMobilePunchService.GetTopicsPopUp(ScheduleCode, EmployeeCode);

                return PartialView("_TopicsPopup", model);
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
