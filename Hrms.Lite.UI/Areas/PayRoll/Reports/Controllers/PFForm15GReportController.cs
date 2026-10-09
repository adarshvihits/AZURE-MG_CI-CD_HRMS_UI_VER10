using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.PayRoll.Reports;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.PayRoll.Reports.Controllers
{
    [Area("PayRoll")]
    [SessionAuthorize]
    public class PFForm15GReportController : Controller
    {
        private readonly ICommonService _commonService;
        public PFForm15GReportController(ICommonService CommonService)
        {

            _commonService = CommonService;

        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GetForm15GData(int EmpCode,string Date,string Place)
        {
            PF15GReport model = new PF15GReport();
            model.Url = new Shared.General.Url();
            model.EmployeeCode = EmpCode;
            model.Url = await _commonService.GetUrl();
            model.Place = Place;
            model.Date = Date;
            return View("~/Areas/PayRoll/Reports/Views/PFForm15GReport/Index.cshtml", model);
        }
    }
}
