using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.PayRoll;
using Hrms.Lite.Shared.PayRoll;
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
    public class TDSVsAllowanceSettingsController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly ITDSVsAllowanceSettingsService _service;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        public TDSVsAllowanceSettingsController(ITDSVsAllowanceSettingsService Service, IDropdownService dropdownService, ICommonService commonService, IHelpers Helpers)
        {
            _service = Service;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = Helpers;
        }
        public async Task<IActionResult> Index()
        {
            try { 
            TDSVsAllowanceSettings model = new TDSVsAllowanceSettings();
            ViewBag.ComponentType = new SelectList(await _dropdownService.GetTDSvsComponentTypeDropdown(), "Code", "Name");
            model = await _service.GetTDSVsAllowanceSettingsList();          
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
        [HttpPost]
        public async Task<IActionResult> Save(TDSVsAllowanceSettings model)
        {
            try
            {
                var response = await _service.TDSVsAllowanceSettingsAllowanceSave(model);
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
