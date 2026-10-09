using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.DataBank;
using Hrms.Lite.Shared.General;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using Spire.Xls;
using Newtonsoft.Json;
using Hrms.Lite.Shared.Account;
using File = Hrms.Lite.Shared.File;

namespace Hrms.Lite.UI.Areas.DataBank.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]
    public class DealerUndertakingApprovalController : Controller
    {
        private readonly IDealerUndertakingApprovalService _DealerUndertakingApprovalService;
        private readonly IHelpers _helpers;
        private readonly IDropdownService _dropdownService;
        private readonly ICommonService _commonService;

        public DealerUndertakingApprovalController(IDealerUndertakingApprovalService DealerUndertakingApprovalService, IDropdownService dropdownService, IHelpers Helpers, ICommonService commonService)
        {
            _DealerUndertakingApprovalService = DealerUndertakingApprovalService;
            _dropdownService = dropdownService;
            _helpers = Helpers;
            _commonService = commonService;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                DealerUndertakingApproval model = new DealerUndertakingApproval();

                model = await _DealerUndertakingApprovalService.DealerUndertakingApprovalList();

                return View("Index", model);
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
        public async Task<IActionResult> GetDealerUndertakingApproval(Guid EmployeeGI)
        {
            try
            {
                DealerUndertakingApproval model = new DealerUndertakingApproval();

                model = await _DealerUndertakingApprovalService.GetDealerUndertakingApproval(EmployeeGI);
                model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);
                
                    return View("Approve", model);
    


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
        public async Task<ActionResult> GetDealerUndertakingApprovalDetails(Guid EmployeeGI, int RequestCode)
        {
            try
            {
                DealerUndertakingApproval model = new DealerUndertakingApproval();
                model = await _DealerUndertakingApprovalService.GetDealerUndertakingApprovalDetails(EmployeeGI, RequestCode);
                return PartialView("_ApprovePopup", model);
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
        public async Task<IActionResult> ApproveDealerUndertakingDetails(DealerUndertakingApproval Model)
        {
            var response = await _DealerUndertakingApprovalService.ApproveDealerUndertakingDetails(Model);
            return Json(response);
        }
    }
}
