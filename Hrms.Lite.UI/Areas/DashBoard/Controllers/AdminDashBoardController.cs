using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DashBoard;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.DashBoard;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.DashBoard.Controllers
{
    [Area("DashBoard")]
    [SessionAuthorize]
    public class AdminDashBoardController : Controller
    {
        private readonly IAdminDashBoardService _AdminDashBoardService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;

        public AdminDashBoardController(ICommonService commonService, IAdminDashBoardService AdminDashBoardService, IHelpers helpers)
        {
            _AdminDashBoardService = AdminDashBoardService;
            _commonService = commonService;
            _helpers = helpers;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                AdminDashBoard model = new AdminDashBoard();
                model = await _AdminDashBoardService.GetAdminDashBoardCount();
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
    }
}
