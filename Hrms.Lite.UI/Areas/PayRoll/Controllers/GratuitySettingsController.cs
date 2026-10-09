using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.PayRoll;
using Hrms.Lite.Shared.PayRoll;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.PayRoll.Controllers
{
    [Area("PayRoll")]
    [SessionAuthorize]
    public class GratuitySettingsController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly IGratuitySettingsService _service;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        public GratuitySettingsController(IGratuitySettingsService Service, IDropdownService dropdownService, ICommonService commonService, IHelpers Helpers)
        {
            _service = Service;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = Helpers;
        }
        public async Task<IActionResult> Index()
        {
            try
            {

                GratuitySettings model = new GratuitySettings();
                model = await _service.GetGratuitySettingsDetails();
                var response = await _service.SaveGratuitySettings(model);
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
        public async Task<IActionResult> GratutySave(GratuitySettings model)
        {
            try
            {
                var response = await _service.SaveGratuitySettings(model);
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
