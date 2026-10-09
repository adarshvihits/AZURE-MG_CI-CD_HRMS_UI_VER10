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
    public class CompanyGeneralSettingsController : Controller
    {
        public readonly ICompanyGeneralSettingsService _service;
        public readonly ICommonService _commonService;
        private readonly IDropdownService _dropdownService;
        public CompanyGeneralSettingsController(ICompanyGeneralSettingsService service, IDropdownService dropdownService, ICommonService commonService)
        {
            _service = service;
            _commonService = commonService;
            _dropdownService = dropdownService;
        }


   

        public async Task<IActionResult> Index()
        {
            try
            {

                CompanyGeneralSettings model = new CompanyGeneralSettings();
            model = await _service.GetCompanyGeneralSettingsDetails();
   
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
        public async Task<IActionResult> Save(CompanyGeneralSettings model,int Drop1,int Drop2)
        {
            try
            {

                model.ShowCelebritiesinsocial = Drop1;
                model.ShowPhotoinsocial = Drop2;
                var response = await _service.CompanyGeneralSettingsSave(model);
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

        public async Task<ActionResult> GetCompanyGeneralSettingsDetails()
        {
            try
            {
                CompanyGeneralSettings model = await _service.GetCompanyGeneralSettingsDetails();
        
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




    }
}
