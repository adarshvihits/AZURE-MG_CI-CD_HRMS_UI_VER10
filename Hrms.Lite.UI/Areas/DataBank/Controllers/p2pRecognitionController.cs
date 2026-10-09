using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.DataBank;
using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
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
    public class p2pRecognitionController : Controller
    {
        private readonly Ip2pRecognitionService _service;
        private readonly ICommonService _commonService;
        private readonly IDropdownService _dropdownService;
        private readonly IHelpers _helpers;
        public p2pRecognitionController(Ip2pRecognitionService service, ICommonService commonService, IDropdownService dropdownService, IHelpers Helpers)
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
                p2pRecognition model = new p2pRecognition();
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
                p2pRecognition model = new p2pRecognition();

                model = await _service.GetList(radiomode);

                return PartialView("~/Areas/DataBank/Views/p2pRecognition/_p2pRecognitionList.cshtml", model);
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



        public async Task<IActionResult> GetDetails(Guid EmployeeGI, int RewardCode,int radiomode)
        {
            try
            {
                if (radiomode == 0)
                {
                    fillPointCriteriaDropdown();
                    p2pRecognition model = new p2pRecognition();
                    model.Rewards = new Rewards();
                    model.PointCriteria = new PointCriteria();
                    ViewBag.Rewards = new SelectList(await _dropdownService.GetRewardsDropdown(EmployeeGI), "Code", "Name");
                    model = await _service.Getp2pRecognitionDetails(EmployeeGI, RewardCode);
                    model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);
                    model.radiomode = radiomode;
                    return PartialView("_RecognitionSlider", model);
                }
                else
                {
                    //fillPointCriteriaDropdown();
                    p2pRecognition model = new p2pRecognition();
                    model.Rewards = new Rewards();
                    model.PointCriteria = new PointCriteria();
                    ViewBag.Rewards = new SelectList(await _dropdownService.GetRewardsDropdown(EmployeeGI), "Code", "Name");
                    //ViewBag.PointCriteria = new SelectList(await _dropdownService.GetRewardPointsCriteriaDropdown(RewardCode), "Code", "Name");
                    model = await _service.Getp2pRecognitionDetails(EmployeeGI, RewardCode);
                    ViewBag.PointCriteria = new SelectList(await _dropdownService.GetRewardPointsCriteriaDropdown(model.Rewards.Code), "Code", "Name");
                    model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);
                    model.radiomode = radiomode;
                    return PartialView("_RecognitionSlider", model);
                }
                
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


        private async void fillPointCriteriaDropdown(int RewardCode = 0)
        {
            ViewBag.PointCriteria = new SelectList(await _dropdownService.GetRewardPointsCriteriaDropdown(RewardCode), "Code", "Name");

        }
        [HttpGet]
        public async Task<JsonResult> GetPointCriteria(int RewardCode)
        {
            var PointCriterias = await _dropdownService.GetRewardPointsCriteriaDropdown(RewardCode);
            return Json(PointCriterias);

        }
        [HttpGet]
        public async Task<JsonResult> GetPoints(int PointCriteriaCode)
        {
            var Point = await _service.GetRewardPoints(PointCriteriaCode);
            return Json(Point);

        }


        [HttpPost]
        public async Task<ActionResult> Save(p2pRecognition input)
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
