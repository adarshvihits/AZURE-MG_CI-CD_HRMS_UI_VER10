using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.DataBank;
using Hrms.Lite.Shared.General;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.DataBank.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]
    public class IncidentReportController : Controller
    {
        private readonly IIncidentReportService _service;
        private readonly ICommonService _commonService;
        private readonly IDropdownService _dropdownService;
        private readonly IHelpers _helpers;
        public IncidentReportController(IIncidentReportService service, ICommonService commonService, IDropdownService dropdownService, IHelpers Helpers)
        {
            _service = service;
            _commonService = commonService;
            _dropdownService = dropdownService;
            _helpers = Helpers;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                IncidentReport model = new IncidentReport();
                Filter filter = new Filter();
                model = await _service.GetList(0);
                model.SliderFilter = await _commonService.GetFilterDropdowns();
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

        public async Task<IActionResult> GetList(int radiomode)
        {

            try
            {
                IncidentReport model = new IncidentReport();

                model = await _service.GetList(radiomode);

                return PartialView("~/Areas/DataBank/Views/IncidentReport/_IncidentReportList.cshtml", model);
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



        public async Task<IActionResult> GetDetails(Guid EmployeeGI, int IncidentCode,int radiomode)
        {
            try
            {
                IncidentReport model = new IncidentReport();
                //model.ResignationDeleteDetails = new ResignationList();

                model = await _service.GetIncidentReportDetails(EmployeeGI, IncidentCode);
                model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);
                model.radiomode = radiomode;
                return PartialView("_IncidentSlider", model);
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
        public async Task<ActionResult> Save(IncidentReport input)
        {
            try
            {
                var response = await _service.Create(input);

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
