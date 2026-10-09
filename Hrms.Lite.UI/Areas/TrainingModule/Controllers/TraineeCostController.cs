using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.TrainingModule;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TrainingModule;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.TrainingModule.Controllers
{
    [Area("TrainingModule")]
    [SessionAuthorize]
    public class TraineeCostController : Controller
    {
            private readonly IDropdownService _dropdownService;
            private readonly ICommonService _commonService;
            private readonly ITraineeCostService _traineeCostService;
            private readonly IHelpers _helpers;
        public TraineeCostController(ITraineeCostService traineeCostService, IDropdownService dropdownService, ICommonService commonService, IHelpers helpers)
        {
            _traineeCostService = traineeCostService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;

        }
        public async Task<IActionResult> Index()
        {

            TraineeCost model = new TraineeCost();
            //await fillfilterDropdown();
            ViewBag.Schedule = new SelectList(await _dropdownService.GetTrnScheduleDropdown(), "Code", "Name");
            Filter filter = new Filter();
            return View(model);
        }
        public async Task<ActionResult> GetList(int ScheduleCode, Filter filter)
        {
            try
            {
                TraineeCost model = new TraineeCost();

                model = await _traineeCostService.GetScheduleList(ScheduleCode, filter);

                return PartialView("~/Areas/TrainingModule/Views/TraineeCost/_TrnParticipantList.cshtml", model);
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


        [HttpGet]
        public async Task<ActionResult> GetCostDetails(Guid empGI)
        {
            try
            {
                TraineeCost model = new TraineeCost();
                model.Details = new List<TraineeCost_details>();

                model = await _traineeCostService.GetCostDetails(empGI);
                model.EmployeeGI = empGI;
                model.EmployeeHeader = await _commonService.GetEmployeeHeader(empGI);
                return PartialView("~/Areas/TrainingModule/Views/TraineeCost/_ViewDetails.cshtml", model);
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


        public async Task<ActionResult> Save(int ScheduleCode, TraineeCost model)
        {
            try
            {
                //TrnParticipantSelection model = new TrnParticipantSelection();

                var response = await _traineeCostService.Save(ScheduleCode, model);

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

        [HttpGet]
        public async Task<ActionResult> Batchupdation(int ScheduleCode)
        {
            try
            {
                TraineeCost model = new TraineeCost();
                TraineeCost model1 = new TraineeCost();
                model.Details = new List<TraineeCost_details>();
                Filter filter = new Filter();
                model1 = await _traineeCostService.GetScheduleList(ScheduleCode, filter);
              
                model = await _traineeCostService.Batchupdation(ScheduleCode);
                model.TraineeCostList = model1.TraineeCostList;
                //model.EmployeeHeader = await _commonService.GetEmployeeHeader(empGI);
                return PartialView("~/Areas/TrainingModule/Views/TraineeCost/_BatchUpdateView.cshtml", model);
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

        public async Task<ActionResult> BatchSave(int ScheduleCode, TraineeCost model)
        {
            try
            {
                //TrnParticipantSelection model = new TrnParticipantSelection();

                var response = await _traineeCostService.BatchSave(ScheduleCode, model);

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
