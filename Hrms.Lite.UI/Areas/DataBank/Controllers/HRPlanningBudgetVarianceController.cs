using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hrms.Lite.UI.Areas.DataBank.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]
    public class HRPlanningBudgetVarianceController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly IHRPlanningBudgetService _hrPlanningBudgetService;
        public HRPlanningBudgetVarianceController(IDropdownService dropdownService,IHRPlanningBudgetService hRPlanningBudgetService)
        {
            _dropdownService = dropdownService;
            _hrPlanningBudgetService = hRPlanningBudgetService;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.HrPlan = new SelectList(await _dropdownService.GetHRPlanningDropDown(), "Code", "Name");
            ViewBag.Designation = new SelectList(await _dropdownService.BudgetVarianceDesignationDropdown(), "Code", "Name");
            HRPlanningBudget model = new HRPlanningBudget();
            model.Type = new MasterBase();
            model.Type.Code =1;
            return View(model);
        }
        public async Task<IActionResult> GetList(int HrPlanningCode, int DesignationCode,int TypeCode)
        {
            HRPlanningBudget model = new HRPlanningBudget();
            model = await _hrPlanningBudgetService.GetBudgetVarianceReport(HrPlanningCode, DesignationCode, TypeCode);
            model.Type = new MasterBase();
            model.Type.Code = TypeCode;
            model.HRPlanning = new MasterBase();
            model.Designation = new Designation();
            model.HRPlanning.Code = HrPlanningCode;
            model.Designation.Code = DesignationCode;
            return PartialView("_List",model);
        }
        public async Task<IActionResult> Create(HRPlanningBudget model)
        {
            var response =await _hrPlanningBudgetService.CreateMRF(model);
            return Json(response);
        }
    }
}
