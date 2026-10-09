using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.TimeOffice;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.TimeOffice;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.TimeOffice.Controllers
{
    [Area("TimeOffice")]
    [SessionAuthorize]
    public class PunchingRegularizationApprovalController : Controller
    {
        private readonly IPunchingRegularizationApprovalService _punchingRegularizationApprovalService;
        private readonly IDropdownService _dropdownService;
        private readonly IHelpers _helpers;
        private readonly ICommonService _commonService;
        public PunchingRegularizationApprovalController(IPunchingRegularizationApprovalService PunchingRegularizationApprovalService, IDropdownService dropdownService, ICommonService commonService, IHelpers helpers)
        {
            _punchingRegularizationApprovalService = PunchingRegularizationApprovalService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;
        }


        public async Task<IActionResult> Index()
        {
            try
            {
                PunchingRegularizationApproval model = new PunchingRegularizationApproval();
            Login CurrentUser = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
            model.EmployeeGI = CurrentUser.authUser.EmployeeGI;
            model = await _punchingRegularizationApprovalService.GetList(model.EmployeeGI);
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


        public async Task<IActionResult> GetList(Guid EmployeeGI)
        {
            PunchingRegularizationApproval model = new PunchingRegularizationApproval();
            Login CurrentUser = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
            model.EmployeeGI = CurrentUser.authUser.EmployeeGI;
            model = await _punchingRegularizationApprovalService.GetList(model.EmployeeGI);
            return PartialView("List", model);
        }



        [HttpPost]
        public async Task<IActionResult> Approve(Guid PunchRegularizationGI)
        {
            try
            {
                PunchingRegularizationApproval model = new PunchingRegularizationApproval();
                model.PunchRegularizationGI = PunchRegularizationGI;
                var response = await _punchingRegularizationApprovalService.Approve(model);
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



        [HttpPost]
        public async Task<IActionResult> Reject(Guid PunchRegularizationGI)
        {
            try
            {
                PunchingRegularizationApproval model = new PunchingRegularizationApproval();
                model.PunchRegularizationGI = PunchRegularizationGI;

                var response = await _punchingRegularizationApprovalService.Reject(model);
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
