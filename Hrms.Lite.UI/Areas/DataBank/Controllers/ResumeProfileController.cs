using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Shared.DataBank.DTO;
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
    public class ResumeProfileController : Controller
    {
        private readonly IResumeProfileService _service;
        private readonly IHelpers _helpers;
        public ResumeProfileController(IResumeProfileService resumeProfileService, IHelpers Helpers)
        {
            _service = resumeProfileService;
            _helpers = Helpers;
        }

        public async Task<IActionResult> Index(Guid ResumeGi, string ControllerName = "ResumeProfile", string Action = "Index", string Area = "DataBank", string DisplayName = "Resume Repository")
        {
            ResumeDataBank model = new ResumeDataBank();
            model = await _service.GetResumeProfile(ResumeGi);
            model.Breadscrumb = new Breadscrumb();
            model.Breadscrumb.Controller = ControllerName;
            model.Breadscrumb.Action = Action;
            model.Breadscrumb.Area = Area;
            model.Breadscrumb.DisplayName = DisplayName;
            return View("Index", model);
        }
    }
}
