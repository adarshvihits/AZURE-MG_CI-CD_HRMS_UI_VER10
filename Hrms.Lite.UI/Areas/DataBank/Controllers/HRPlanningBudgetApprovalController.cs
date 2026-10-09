using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.DataBank.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.DataBank.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]
    public class HRPlanningBudgetApprovalController : Controller
    {
        #region private
        private readonly IDropdownService _dropdownService;
        public readonly IHRPlanningBudgetService _Service;

        #endregion
        #region constr
        public HRPlanningBudgetApprovalController(IDropdownService dropdownService, IHRPlanningBudgetService Service, IHelpers helpers)
        {
            _Service = Service;
            _dropdownService = dropdownService;
        }

        #endregion
        public async Task<IActionResult> Index()
        {
            try
            {
                HRPlanningBudget model = new HRPlanningBudget();
                model = await _Service.GetBudgetUploadApprovalMainList();               
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
        public async Task<IActionResult> GetApprovalDetails(int HRPlanningCode, int DesignationCode)
        {
            try
            {
                HRPlanningBudget model = new HRPlanningBudget();
                model = await _Service.GetBudgetUploadApprovalList(HRPlanningCode, DesignationCode);
                return View("Approve",model);
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
        public async Task<IActionResult> ApproveOrReject(HRPlanningBudget model, string Mode)
        {
            try
            {
                var response = await _Service.BudgetApprove(model, Mode);
                if (Mode == "REJECT")
                {
                    if (response.Success == true)
                        response.Message = "Rejected Successfully";
                }
                else
                {
                    if (response.Success == true)
                        response.Message = "Approved Successfully";
                }
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
