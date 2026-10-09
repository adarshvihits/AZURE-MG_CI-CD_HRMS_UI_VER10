using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank.Reports;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.DataBank.Reports.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]
    public class ServiceRecordController : Controller
    {
        private readonly IServiceRecordService _ServiceRecordService;
        private readonly ICommonService _commonService;
       
        private readonly IHelpers _helpers;
     
        public ServiceRecordController(IServiceRecordService ServiceRecordService, ICommonService commonService, IHelpers Helpers)
        {
            _ServiceRecordService = ServiceRecordService;
            _commonService = commonService;
            _helpers = Helpers;
        }
        public async Task <IActionResult> Index()
        {
            try { 
            ServiceRecord model = new ServiceRecord();
       
            model.Url = await _ServiceRecordService.GetUrl();
            //Global.urlLink = model.Url.Urllink;
            model = await _ServiceRecordService.GetServiceRecordList(new Filter());
            model.Url = await _ServiceRecordService.GetUrl();
            //Global.urlLink = model.Url.Urllink;
            //model.Url.Urllink = Global.urlLink;
            return View("~/Areas/DataBank/Reports/Views/ServiceRecord/Index.cshtml",model);
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
