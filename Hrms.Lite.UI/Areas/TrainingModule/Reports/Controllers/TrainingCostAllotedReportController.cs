using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.TrainingModule;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.TrainingModule.Controllers
{
    [Area("TrainingModule")]
    [SessionAuthorize]
    public class TrainingCostAllotedReportController : Controller
    {
        public readonly ITrainingCostAllotedReportServices _service;
        public readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        public TrainingCostAllotedReportController(ITrainingCostAllotedReportServices service, ICommonService commonService, IHelpers helpers)
        {
            _service = service;
            _commonService = commonService;
            _helpers = helpers;
        }
        public async Task<ActionResult> Index()
        {

            return View("~/Areas/TrainingModule/Reports/Views/TrainingCostAllotedReport/Index.cshtml",await _service.GetList());


        }
    }
}
