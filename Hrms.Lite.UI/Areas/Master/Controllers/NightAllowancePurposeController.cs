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
    public class NightAllowancePurposeController : Controller
    {
        public readonly INightAllowancePurposeService _service;
        public readonly ICommonService _commonService;
        public readonly IDropdownService _dropdownService;
        public NightAllowancePurposeController(INightAllowancePurposeService service, ICommonService commonService, IDropdownService dropdownService)
        {
            _service = service;
            _commonService = commonService;
            _dropdownService = dropdownService;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                NightAllowancePurpose model = new NightAllowancePurpose();
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
        public async Task<IActionResult> Create(string Mode)
        {
            try
            {
                NightAllowancePurpose model1 = new NightAllowancePurpose();
                ViewBag.Departments = new SelectList(await _dropdownService.GetDepartmentDropdown(), "Code", "Name");
                model1.Mode = Mode;
                return PartialView("_Create", model1);
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
        public async Task<IActionResult> Edit(Guid purposeGi,string Mode)
        {
            try
            {
                NightAllowancePurpose model1 = new NightAllowancePurpose();
                ViewBag.Departments = new SelectList(await _dropdownService.GetDepartmentDropdown(), "Code", "Name");
                model1= await _service.GetDetails(purposeGi);
                model1.Mode = Mode;
                return PartialView("_Create", model1);
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
        public async Task<IActionResult> Save(NightAllowancePurpose model)
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
        public async Task<IActionResult> Delete(Guid purposeGi)
        {
            try
            {
                NightAllowancePurpose model = new NightAllowancePurpose();
                model.PurposeGi = purposeGi;
                var response = await _service.Delete(model);
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
