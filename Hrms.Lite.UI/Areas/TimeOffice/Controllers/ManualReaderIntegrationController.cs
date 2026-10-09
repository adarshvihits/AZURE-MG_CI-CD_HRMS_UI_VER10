using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.TimeOffice;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.TimeOffice;
using Hrms.Lite.Shared.General;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using Spire.Xls;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Hrms.Lite.Shared.Account;
namespace Hrms.Lite.UI.Areas.TimeOffice.Controllers
{
    [Area("TimeOffice")]
    [SessionAuthorize]
    public class ManualReaderIntegrationController : Controller
    {
        private readonly IManualReaderIntegrationService _service;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        public ManualReaderIntegrationController( ICommonService commonService, IManualReaderIntegrationService ManualReaderIntegrationService, IHelpers helper)
        {
            _service = ManualReaderIntegrationService;
            _commonService = commonService;
            _helpers = helper;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                return View();
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

        public async Task<IActionResult> ManualReaderIntegration()
        {
            try
            {
                ManualReaderIntegration model = new ManualReaderIntegration();

                var response = await _service.ManualReaderIntegration(model);
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
