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
    public class SlabBasedDeductionController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly ISlabBasedDeductionService _SlabBasedDeductionService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        public SlabBasedDeductionController(ISlabBasedDeductionService SlabBasedDeductionService, IDropdownService dropdownService, ICommonService commonService, IHelpers Helpers)
        {
            _SlabBasedDeductionService = SlabBasedDeductionService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = Helpers;

        }
        public async Task<IActionResult> Index()
        {
            try
            {
                SlabDeductionSettings model = new SlabDeductionSettings();
                ViewBag.Deduction = new SelectList(await _dropdownService.GetDeductionDropdown(), "Code", "Name");
                model = await _SlabBasedDeductionService.GetList(0);
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

        public async Task<IActionResult> GetList(int DeductionCode)
        {
            try
            {
                SlabDeductionSettings model = new SlabDeductionSettings();
                //model.Details = new List<PTSettings_Details>();
                //model.Details.Add(new PTSettings_Details());
                model = await _SlabBasedDeductionService.GetList(DeductionCode);

               
             
                if (model.SlabDeductionSettingsList.Count == 0)
                {
                    model.SlabDeductionSettingsList = new List<SlabDeductionSettings>();
                    model.SlabDeductionSettingsList.Add(new SlabDeductionSettings());
                }
                return PartialView("_SlabBasedDeductionList", model);
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
        public async Task<IActionResult> Save(SlabDeductionSettings model)
        {
            try
            {
                var response = await _SlabBasedDeductionService.Save(model);
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
