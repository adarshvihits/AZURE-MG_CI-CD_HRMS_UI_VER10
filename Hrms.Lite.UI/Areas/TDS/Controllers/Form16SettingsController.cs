using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.TDS;

using Hrms.Lite.Shared.TDS;

namespace Hrms.Lite.UI.Areas.TDS.Controllers
{
    [Area("TDS")]
    [SessionAuthorize]
    public class Form16SettingsController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly IForm16SettingsService _Form16SettingsService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        private readonly IBlob _blob;
        public Form16SettingsController(IForm16SettingsService Form16SettingsService, IDropdownService dropdownService, ICommonService commonService, IHelpers Helpers, IBlob blob)
        {
            _Form16SettingsService = Form16SettingsService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = Helpers;
            _blob = blob;

        }
        public async Task<IActionResult> Create()
        {
            Form16Settings model = new Form16Settings();
          
            ViewBag.AssessmentYear = new SelectList(await _dropdownService.GetAssessmentYearDropdown(), "Code", "Name");
            ViewBag.GroupCompany = new SelectList(await _dropdownService.GetGroupCompanyDropDown(), "Code", "Name");

            if (model.PrintDate == Convert.ToDateTime("01-Jan-0001"))
            {
                model.PrintDate = null;
            }
            return View(model);
        }
        [HttpPost]

        public async Task<IActionResult> SaveForm16Settings(Form16Settings model)
        {
            try
            {
                var response = await _Form16SettingsService.SaveForm16Settings(model);
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
        [HttpGet]
        public async Task<JsonResult> FillFinYearDetails(int AssessmentYear)
        {
            Form16Settings model = new Form16Settings();
            model = await _Form16SettingsService.GetFinYearDetails(AssessmentYear);
            return Json(model);
        }
    }
}
