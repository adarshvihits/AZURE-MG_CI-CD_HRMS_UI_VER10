using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.TimeOffice;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.TimeOffice.Controllers
{
    [Area("TimeOffice")]
    [SessionAuthorize]
    public class AbscondingIntimationController : Controller
    {
        private readonly IAbscondingIntimationService _AbscondingIntimationService;
        private readonly ICommonService _commonService;
        private readonly IDropdownService _dropdownService;
        private readonly IWebHostEnvironment _environment;
        private readonly IHelpers _helpers;

        public AbscondingIntimationController(IDropdownService dropdownService, IAbscondingIntimationService AbscondingIntimationService, ICommonService commonService, IWebHostEnvironment environment, IHelpers helpers)
        {
            _AbscondingIntimationService = AbscondingIntimationService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _environment = environment;
            _helpers = helpers;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                AbscondingIntimation model = new AbscondingIntimation();

            model.Filter = new Filter();
            model = await _AbscondingIntimationService.GetList(model.Filter);
            model.Filter = await _commonService.GetFilterDropdowns();
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

        public async Task<IActionResult> Detag(Guid EmployeeGI)
        {
            AbscondingIntimation model = new AbscondingIntimation();
           
            model = await _AbscondingIntimationService.GetHistoryDetails(EmployeeGI);
            model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);
            model.EmployeeGI = EmployeeGI;
            return PartialView("_PopUp", model);
        }

        public async Task<IActionResult> FilterList(Filter Filter)
        {
            AbscondingIntimation model = new AbscondingIntimation();
            model = await _AbscondingIntimationService.GetList(Filter);
            return PartialView("_EditList", model);
        }



        [HttpPost]
        public async Task<IActionResult> Submit(AbscondingIntimation model)
        {
            try
            {

                var response = await _AbscondingIntimationService.SaveDetag(model);

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
