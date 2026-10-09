using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.PayRoll;
using Hrms.Lite.Shared.DataBank.Generic.Master;
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
    public class FormulaBasedDeductionController : Controller
    {
        private readonly IDropdownService _dropDownService;
        private readonly IFormulaBasedDeductionService _service;
        public FormulaBasedDeductionController(IDropdownService dropdownService, IFormulaBasedDeductionService service)
        {
            _dropDownService = dropdownService;
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                ViewBag.Deduction = new SelectList(await _dropDownService.GetFormulaBasedDeductionDropdown(), "Code", "Name");
                ViewBag.Operations = new SelectList(await _dropDownService.GetFormulaBasedDeductionOperationsDropdown(), "Code", "Name");
                FormulaBasedDeduction model = new FormulaBasedDeduction();
                model = await _service.GetFormulaBasedDeductionDetails(0);
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
        public async Task<IActionResult> GetFormulaBasedDeductionDetails(int DeductionCode)
        {
            FormulaBasedDeduction model = new FormulaBasedDeduction();
            model = await _service.GetFormulaBasedDeductionDetails(DeductionCode);
            ViewBag.Operations = new SelectList(await _dropDownService.GetFormulaBasedDeductionOperationsDropdown(), "Code", "Name");
            model.Deduction = new Deduction();
            model.Deduction.Code = DeductionCode;
            return PartialView("_DeductionAndAllowance",model);
        }

        public async Task<IActionResult> FormulaBasedDeductionSave(FormulaBasedDeduction model)
        {
            var response = await _service.FormulaBasedDeductionSave(model);
            return Json(response);
        }
    }
}
