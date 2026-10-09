using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.Master;
using Hrms.Lite.Shared.Master;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.Master.Controllers
{
    [Area("Master")]
    [SessionAuthorize]
    public class DutyRosterLockController : Controller
    {
        private readonly IDutyRosterLockService _service;
        private readonly IDropdownService _dropdownService;
        public DutyRosterLockController(IDropdownService dropdownService, IDutyRosterLockService service)
        {
            _dropdownService = dropdownService;
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            try { 
                DutyRosterLock model = new DutyRosterLock();
                ViewBag.month = new SelectList(await _dropdownService.GetMonthly_AttendanceProcessing_MonthDropdown(null), "Code", "Name");
                model = await _service.GetList(0);
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
        public async Task<IActionResult> GetList(int Month)
        {
            try { 
                DutyRosterLock model = new DutyRosterLock();
                model = await _service.GetList(Month);
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
        public async Task<IActionResult> GetBatchUpdate(int Month)
        {
            try { 
                DutyRosterLock model = new DutyRosterLock();
                model.Month = new SalaryPeriod();
                model.Month.Code = Month;
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
        public async Task<IActionResult> Save(DutyRosterLock model)
        {
            try
            {
                var response = await _service.Save(model);
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

        [HttpPost]
        public async Task<IActionResult> BatchUpdateSave(DutyRosterLock model)
        {
            try
            {
                var response = await _service.BachUpdate(model);
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
