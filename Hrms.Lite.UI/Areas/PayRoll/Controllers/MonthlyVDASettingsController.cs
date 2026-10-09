using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.PayRoll;
using Hrms.Lite.Shared.Master;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.PayRoll.Controllers
{
    [Area("PayRoll")]
    [SessionAuthorize]
    public class MonthlyVDASettingsController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly ICommonService _commonService;
        private readonly IMonthlyVDASettingsService _service;

        public MonthlyVDASettingsController(IMonthlyVDASettingsService service, IDropdownService dropdownService, ICommonService commonService)
        {
            _service = service;
            _dropdownService = dropdownService;
            _commonService = commonService;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                ViewBag.SalaryPeriod = new SelectList(await _dropdownService.GetMonthDropdown(), "Code", "Name");
                VDAGroup model = new VDAGroup();
                model = await _service.GetMonthlyVDASettingsList(0);
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

        //VDA Amount = (DaPoint - Standard Deduction) * DAIndex
        public async Task<IActionResult> GetMonthlyVDASettingsList(int SalaryperiodCode)
        {
            try
            {
                VDAGroup model = new VDAGroup();
                model = await _service.GetMonthlyVDASettingsList(SalaryperiodCode);
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
        [HttpPost]
        public async Task<IActionResult> Save(VDAGroup model)
        {
            try
            {
                var response = await _service.MonthlyVDASettingsSave(model);
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
