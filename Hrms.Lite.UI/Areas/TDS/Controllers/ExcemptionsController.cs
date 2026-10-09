using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.TDS;
using Hrms.Lite.Shared.TDS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.TDS.Controllers
{
    [Area("TDS")]
    [SessionAuthorize]
    public class ExcemptionsController : Controller
    {
        public readonly IExcemptionsService _service;
        public readonly ICommonService _commonService;
        private readonly IDropdownService _dropdownService;
        public ExcemptionsController(IExcemptionsService service, ICommonService commonService, IDropdownService dropdownService)
        {
            _service = service;
            _commonService = commonService;
            _dropdownService = dropdownService;
        }
        public async Task<ActionResult> Index()
        {
            try
            {
                Excemptions model = new Excemptions();
                ViewBag.TDSAllowance = new SelectList(await _dropdownService.GetTDSvsAllowanceDropdown(), "Code", "Name");
                model = await _service.GetExcemptionsList();
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
        public async Task<ActionResult> Save(Excemptions model)
        {
            try
            {
                var response = await _service.SaveExcemptionsSettings(model);
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
