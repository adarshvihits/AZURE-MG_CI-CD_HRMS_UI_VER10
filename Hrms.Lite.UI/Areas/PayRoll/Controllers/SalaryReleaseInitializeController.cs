using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.PayRoll;
using Hrms.Lite.Shared.PayRoll;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.PayRoll.Controllers
{
    [Area("PayRoll")]
    [SessionAuthorize]
    public class SalaryReleaseInitializeController : Controller
    {
        private readonly IDropdownService _dropdownService;
        public readonly ISalaryReleaseInitializeService _service;
        public readonly ICommonService _commonService;
        public SalaryReleaseInitializeController(ISalaryReleaseInitializeService service, ICommonService commonService,IDropdownService dropdownService)
        {
            _service = service;
            _dropdownService = dropdownService;
            _commonService = commonService;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                SalaryReleaseInitialize model = new SalaryReleaseInitialize();
                ViewBag.month = new SelectList(await _dropdownService.GetSalaryProcessedMonth_Dropdown(0), "Code", "Name");
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
            try
            {
                SalaryReleaseInitialize model = new SalaryReleaseInitialize();
                model = await _service.GetList(Month);
                return PartialView("_SalaryReleaseInitializeList", model);
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
        public async Task<IActionResult> Save(SalaryReleaseInitialize model)
        {
            try
            {
                var response = await _service.Create(model);
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
