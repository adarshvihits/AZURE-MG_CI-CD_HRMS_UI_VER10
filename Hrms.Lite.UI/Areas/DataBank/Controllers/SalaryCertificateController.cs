using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.Master;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.DataBank.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]
    public class SalaryCertificateController : Controller
    {
        private readonly ICommonService _commonService;

        public SalaryCertificateController(ICommonService commonService)
        {
           
            _commonService = commonService;
           
        }
        public static class Global
        {

            public static string url;



        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GetLetterDetails(int EmpCode, Guid GroupCompanyGI, string Reason) 
        {
            KendoReport model = new KendoReport();
            model.EmpCode = EmpCode;
            model.Reason = Reason;
            model.GroupCompanyGI = GroupCompanyGI;
            model.Url = await _commonService.GetUrl();
            Global.url = model.Url.Urllink;
           //model.Url = await _commonService.GetOfferLetter(model.type);
           model.Url.Urllink = Global.url;
          
                return View("Index", model);
        }
    }
}
