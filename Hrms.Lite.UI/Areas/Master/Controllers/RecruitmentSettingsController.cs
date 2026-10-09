using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.Master;
using Hrms.Lite.Shared.Master;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.Master.Controllers
{
    [Area("Master")]
    [SessionAuthorize]
    public class RecruitmentSettingsController : Controller
    {
        public readonly IRecruitmentSettingsService _service;
        public readonly ICommonService _commonService;
        public RecruitmentSettingsController(IRecruitmentSettingsService service, ICommonService commonService)
        {
            _service = service;
            _commonService = commonService;
        }
        public async Task <IActionResult>Index()
        {
            try
            {
                RecruitmentSettings model=new RecruitmentSettings();
            model= await _service.GetList();
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
        public async Task <IActionResult> AddNew(Guid DesignationGI)
        {
            try
            {
                RecruitmentSettings model = new RecruitmentSettings();
               //ViewBag.JobTitle= JobTitle;
                model= await _service.GetDetailsList(DesignationGI);
                return PartialView("_Create", model);
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
        public async Task<ActionResult> Save(RecruitmentSettings input)
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
