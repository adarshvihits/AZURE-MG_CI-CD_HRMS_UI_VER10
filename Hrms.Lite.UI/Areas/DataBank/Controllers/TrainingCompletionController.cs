using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.DataBank.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]
    public class TrainingCompletionController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly ITrainingCompletionService _service;
        private readonly ICommonService _commonService;

       
        public TrainingCompletionController(ITrainingCompletionService Service, IDropdownService dropdownService, ICommonService commonService)
        {
            _service = Service;
            _dropdownService = dropdownService;
            _commonService = commonService;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                InductionTraining model = new InductionTraining();
                Filter filter = new Filter();
                model = await _service.GetList(filter);
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
        

        public async Task<IActionResult> ApplyFilter(Filter filter)
        {
            try
            {
                InductionTraining model = new InductionTraining();
                model.Filter = filter;
                model = await _service.GetList(filter);
                return PartialView("_List", model);
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
        public async Task<IActionResult> GetList()
        {
            try
            {
                InductionTraining model = new InductionTraining();
                Filter filter = new Filter();
                model = await _service.GetList(filter);
                return PartialView("_List", model);
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

        public async Task<IActionResult> GetDetails(int HeadCode, int EmployeeCode)
        {
            try
            {
                InductionTraining model = new InductionTraining();
                model = await _service.GetInductionTrainingDetails(HeadCode, EmployeeCode);
                return PartialView("_CompletionSlider", model);
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
        public async Task<ActionResult> AcceptOrReject(InductionTraining model)
        {
            try
            {
                var response = await _service.ApproveCompletion(model);
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
