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
    public class IncomeTaxRatesController : Controller
    {
        private readonly IIncomeTaxRatesService _service;
        private readonly IDropdownService _dropdownService;
        public IncomeTaxRatesController(IIncomeTaxRatesService service,IDropdownService dropdownService)
        {
            _service = service;
            _dropdownService = dropdownService;
        }
        public async Task<IActionResult> Index()
        {
            try { 
            IncomeTaxRates model = new IncomeTaxRates();
            ViewBag.AssessmentYr = new SelectList(await _dropdownService.GetAssessmentYearDropdown(), "Code", "Name");
            ViewBag.ItCategory = new SelectList(await _dropdownService.GetITCategoryDropdown(), "Code", "Name");
            model.Details = new List<IncomeTaxRates_Details>();
            IncomeTaxRates_Details lst = new IncomeTaxRates_Details()
            {
                SalaryFrom = null,
                SalaryTo = null,
                //Surcharge = null,
                Rate = null
            };
            model.Details.Add(lst);
            
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
        public async Task<IActionResult> GetList(int AssessmentYr,int ItCategory,int BasedOn)
        {
            IncomeTaxRates model = new IncomeTaxRates();
            model = await _service.IncomeTaxRatesList(AssessmentYr, ItCategory, BasedOn);
            if (model.Details.Count == 0)
            {
                IncomeTaxRates_Details lst = new IncomeTaxRates_Details()
                {
                    SalaryFrom = null,
                    SalaryTo = null,
                    //Surcharge = null,
                    Rate = null
                };
                model.Details.Add(lst);
            }
            return PartialView("_List",model);
        }

        public async Task<IActionResult> Save(IncomeTaxRates model)
        {
            try
            {
                var response = await _service.IncomeTaxRates_Save(model);
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
