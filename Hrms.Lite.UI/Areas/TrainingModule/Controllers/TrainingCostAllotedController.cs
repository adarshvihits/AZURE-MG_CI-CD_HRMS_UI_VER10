using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.TrainingModule;
using Hrms.Lite.Shared.TrainingModule;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.TrainingModule.Controllers
{

    [Area("TrainingModule")]
    public class TrainingCostAllotedController : Controller
    {
        public readonly ITrainingCostAllotedServices _service;
        public readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        public TrainingCostAllotedController(ITrainingCostAllotedServices service, ICommonService commonService, IHelpers helpers)
        {
            _service = service;
            _commonService = commonService;
            _helpers = helpers;
        }
        public async Task<ActionResult> Index()
        {
           
            return View(await _service.GetList());


        }
        [HttpPost]
        public async Task<ActionResult>Save(TraineeCost input)
        {
            var response = await _service.Create(input);
            return Json(response);
        }
    }
}


