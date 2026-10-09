using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.TrainingModule;
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
    public class TrainingVsSubjectController : Controller
    {
        public readonly ITrainingVsSubjectService _TrainingVsSubjectServices;
        private readonly IHelpers _helpers;
        public readonly ICommonService _commonService;
        public readonly IDropdownService _dropdownService;
        public TrainingVsSubjectController(ITrainingVsSubjectService TrainingVsSubjectServices, IHelpers Helpers, IDropdownService dropdownService, ICommonService commonService)
        {
            _TrainingVsSubjectServices = TrainingVsSubjectServices;
            _helpers = Helpers;
            _dropdownService = dropdownService;
            _commonService = commonService;
        }

        public async Task<IActionResult> Index()
        {
            TrainingVsSubject model = new TrainingVsSubject();

          
            ViewBag.Training = new SelectList(await _dropdownService.GetTrainingDropdown(), "Code", "Name");
            //await fillfilterDropdown();
            return View("~/Areas/TrainingModule/Views/TrainingVsSubject/Index.cshtml", model);


        }
        public async Task<IActionResult> GetList(int TrainingCode)
        {
            TrainingVsSubject model = new TrainingVsSubject();
            model = await _TrainingVsSubjectServices.GetList(TrainingCode);
            return PartialView("_SubjectList", model);
        }

        [HttpPost]
        public async Task<ActionResult> Save(TrainingVsSubject model)
        {
            try
            {   
                var response = await _TrainingVsSubjectServices.SubjectSave(model);
                return Json(response);
            }
            catch (UnauthorizedAccessException e)
            {

                return RedirectToAction("Error", "Account", new { Area = "" });
            }

        }

    }
}
