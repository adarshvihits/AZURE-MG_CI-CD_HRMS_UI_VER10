using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.PayRoll;
using Hrms.Lite.Shared.Master;
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
    public class PTSettingsController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly IPTSettingsService _PTSettingsService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        public PTSettingsController(IPTSettingsService PTSettingsService, IDropdownService dropdownService, ICommonService commonService, IHelpers Helpers)
        {
            _PTSettingsService = PTSettingsService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = Helpers;

        }
        public async Task <IActionResult> Index()
        {
            try
            {
                PTSettings model = new PTSettings();
                ViewBag.PTGroup = new SelectList(await _dropdownService.GetPTGroupDropDown(), "Code", "Name");
                model = await _PTSettingsService.PTSettingsList(0);
                return View();
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
        public async Task<IActionResult> GetPTSettingsList(int ptGroup)
        {
            try { 
            PTSettings model = new PTSettings();
            //model.Details = new List<PTSettings_Details>();
            //model.Details.Add(new PTSettings_Details());
            model = await _PTSettingsService.PTSettingsList(ptGroup);
            
            model.PTGroup = new PTGroup();
            model.PTGroup.Code = ptGroup;
                if (model.Details.Count == 0)
                {
                    model.Details = new List<PTSettings_Details>();
                    model.Details.Add(new PTSettings_Details());
                }
                return PartialView("_PTSettingsList", model);
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
        public async Task<IActionResult> Save(PTSettings model)
        {
            try { 
            var response = await _PTSettingsService.Save(model);
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
        public async Task<IActionResult> ApplicableAllownacePopUp(int ptGroup)
        {
            try
            {
                PTSettings model = new PTSettings();              
                model = await _PTSettingsService.GetPTSettingsAllowance(ptGroup);
                model.PTGroup = new PTGroup();
                model.PTGroup.Code = ptGroup;
                return PartialView("_ApplicableAllowance", model);
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
        public async Task<IActionResult> GetAllowanceCount(int ptGroup)
        {
            try
            {
                PTSettings model = new PTSettings();
                model = await _PTSettingsService.GetPTSettingsAllowance(ptGroup);
                var list = from li in model.Allowance
                           where li.Applicability == true
                           select li;
                var count = list.Count();
                return Json(count);
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
        public async Task<IActionResult> AllowanceSave(PTSettings model)
        {
            try { 
            var list = from li in model.Allowance
                       where li.Applicability == true
                       select li;
            model.Allowance = list.ToList();

            var response = await _PTSettingsService.AllowanceSave(model);
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
