using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DashBoard;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.DashBoard;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.DashBoard.Controllers
{
    [Area("DashBoard")]
    [SessionAuthorize]
    public class TravelClaimDashBoardController : Controller
    {
        private readonly ITravelClaimDashboardService _Service;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        private readonly IDropdownService _dropdownService;
        public TravelClaimDashBoardController(ICommonService commonService, ITravelClaimDashboardService AdminDashBoardService, IHelpers helpers, IDropdownService dropdownService)
        {
            _Service = AdminDashBoardService;
            _commonService = commonService;
            _helpers = helpers;
            _dropdownService = dropdownService;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                //TravelClaimDashboard model = new TravelClaimDashboard();
               
               
                //model = await _Service.GetTravelDashBoardCount();
               

                //return View(model);
                return View("~/Views/Home/TravelClaim.cshtml");
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


        public async Task<IActionResult> GetRequestList(int Type)
        {
            try
            {
                TravelClaimDashboard model = new TravelClaimDashboard();
                model.requestLists = await _Service.GetRequestList(Type);
                return PartialView("~/Areas/DashBoard/Views/TravelClaimDashboard/_RequestList.cshtml", model.requestLists);
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


        public async Task<IActionResult> GetBudgetTracking(int Type)
        {
            try
            {
                TravelClaimDashboard model = new TravelClaimDashboard();
                model.BudjetTrackings = await _Service.GetBudgetTracking(Type);
                return PartialView("~/Areas/DashBoard/Views/TravelClaimDashboard/_BudjetTracking.cshtml", model.BudjetTrackings);
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
