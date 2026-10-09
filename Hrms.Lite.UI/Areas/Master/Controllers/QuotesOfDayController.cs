using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.Master;
using Hrms.Lite.Shared.General;
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
    public class QuotesOfDayController : Controller
    {
        private readonly IQuoteOfDayServices _QuoteOfDayServices;
        private readonly IHelpers _helpers;
        private readonly IDropdownService _dropdownService;
        private readonly ICommonService _commonService;


        public QuotesOfDayController(IQuoteOfDayServices QuoteOfDayServices, IHelpers Helpers, IDropdownService dropdownService, ICommonService commonService)
        {
            _QuoteOfDayServices = QuoteOfDayServices;
            _helpers = Helpers;
            _dropdownService = dropdownService;
            _commonService = commonService;
        }




        public async Task<IActionResult>Index()
        {
            try
            {
                QuotesOfDay model = new QuotesOfDay();

                var finYear = await _commonService.GetCurrentLFinancialYear();
                var currntFY = Convert.ToInt32(finYear.Data);
                model.FYCODE = currntFY;
                ViewBag.FinancialYear = new SelectList(await _dropdownService.GetFinancialYearDropdown(), "Code", "Name");
                model = await _QuoteOfDayServices.GetList(0);
                //await fillfilterDropdown();
                return View("~/Areas/Master/Views/QuotesOfDay/Index.cshtml", model);
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

      
        public async Task<IActionResult> GetList(int FYCODE)
        {
            QuotesOfDay model = new QuotesOfDay();
            model = await _QuoteOfDayServices.GetList(FYCODE);
            return PartialView("Quotes_List", model);
        }

        [HttpPost]
        public async Task<ActionResult> QuotesOfDaySave(string Thought, int ThoughtCode, DateTime Date)
        {
            try
            {
                
                QuotesOfDay model = new QuotesOfDay();
                model.ThoughtCode = ThoughtCode;
                model.Thought = Thought;
                model.Date = Date;

                var response = await _QuoteOfDayServices.QuotesOfDaySave(model);
                return Json(response);
            }
            catch (UnauthorizedAccessException e)
            {

                return RedirectToAction("Error", "Account", new { Area = "" });
            }

        }

        [HttpPost]
        public async Task<ActionResult> Delete(int ThoughtCode)
        {
          
            try
            {
                QuotesOfDay model = new QuotesOfDay();
                model.ThoughtCode = ThoughtCode;
                var response = await _QuoteOfDayServices.Delete(model);
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
        public async Task<IActionResult> BatchUpdate(int FYCODE, string FYNAME)
        {
            try
            {
                QuotesOfDay model = new QuotesOfDay();
                model.FYCODE = FYCODE;
             
                QuotesOfDay header = new QuotesOfDay();
                ViewBag.From = new SelectList(await _dropdownService.GetFinancialYearDropdown(), "Code", "Name");
                ViewBag.To = new SelectList(await _dropdownService.GetFinancialYearDropdown(), "Code", "Name");
                model = await _QuoteOfDayServices.GetBatchUpdateDetails(model);
                model.FYNAME = FYNAME;
                return PartialView("_BatchUpdate", model);
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
        public async Task<IActionResult> BatchUpdateSave(QuotesOfDay model)
        {
            try
            {
              
                var response = await _QuoteOfDayServices.BatchUpdateSave(model);
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
