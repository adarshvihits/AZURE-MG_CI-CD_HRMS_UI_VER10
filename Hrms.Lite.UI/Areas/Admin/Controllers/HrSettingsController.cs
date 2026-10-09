using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.Admin;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.Admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [SessionAuthorize]
    public class HrSettingsController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly IHrSettingsService _service;
        private readonly ICommonService _commonService;
        public HrSettingsController(IHrSettingsService Service, IDropdownService dropdownService, ICommonService commonService)
        {
            _service = Service;
            _dropdownService = dropdownService;
            _commonService = commonService;
        }


        public async Task<IActionResult> Index()
        {

            try
            {
                HrSettings model = new HrSettings();
                ViewBag.FinancialYear = new SelectList(await _dropdownService.GetFinancialYearDropdown(), "Code", "Name");
                model = await _service.GetList();
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
        public async Task<ActionResult> Save(HrSettings input)
        {
            try
            {
                var response = await _service.Save(input);

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
