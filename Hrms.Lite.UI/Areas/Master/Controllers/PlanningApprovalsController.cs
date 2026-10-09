using System;
using System.Threading.Tasks;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.Master;
using Hrms.Lite.Shared.Master;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hrms.Lite.UI.Areas.Master.Controllers
{
    [Area("Master")]
    [SessionAuthorize]
    public class PlanningApprovalsController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly IPlanningApprovalService _planningApprovalService;
        private readonly IHrPlanningMasterService _hrPlanningMasterService;
        public PlanningApprovalsController(IPlanningApprovalService planningApprovalService, IDropdownService dropdownService, IHrPlanningMasterService hrPlanningMasterService)
        {
            _planningApprovalService = planningApprovalService;
            _dropdownService = dropdownService;
            _hrPlanningMasterService = hrPlanningMasterService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                ViewBag.PlanningPeriod = new SelectList(await _dropdownService.GetHRPlanningPeriodDropDown(), "Code", "Name");
                ViewBag.CurrentTab = "PENDING";
                PlanningApproval model = await _planningApprovalService.GetPlanningApprovalList("PENDING", null);
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

        public async Task<IActionResult> GetPlanningApprovalList(string TabIndex, int? PlanningPeriod)
        {
            try
            {
                ViewBag.CurrentTab = TabIndex;
                PlanningApproval model = await _planningApprovalService.GetPlanningApprovalList(TabIndex, PlanningPeriod);
                return PartialView("_PlanningApprovalList", model);
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
        public async Task<IActionResult> Approve(int Logcode, Guid LogGI, string EntryType, string Remarks)
        {
            try
            {
                HRPlanningMaster model = new HRPlanningMaster();
                model.Logcode = Logcode;
                model.LogGI = LogGI;
                model.EntryType = EntryType;
                model.Remarks = Remarks;
                model.ApproveMode = "Approve";
                var response = await _hrPlanningMasterService.ApproveOrReject(model);
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
        public async Task<IActionResult> Reject(int Logcode, Guid LogGI, string EntryType, string Remarks)
        {
            try
            {
                HRPlanningMaster model = new HRPlanningMaster();
                model.Logcode = Logcode;
                model.LogGI = LogGI;
                model.EntryType = EntryType;
                model.Remarks = Remarks;
                model.ApproveMode = "REJECT";
                var response = await _hrPlanningMasterService.ApproveOrReject(model);
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
