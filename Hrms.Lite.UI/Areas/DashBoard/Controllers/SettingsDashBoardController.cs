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
    public class SettingsDashboardController : Controller
    {
        private readonly ISettingsDashBoardService _SettingsDashBoardService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        public SettingsDashboardController(ICommonService commonService, ISettingsDashBoardService SettingsDashBoardService, IHelpers helpers)
        {
            _SettingsDashBoardService = SettingsDashBoardService;
            _commonService = commonService;
            _helpers = helpers;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                SettingsDashBoard model = new SettingsDashBoard();
                model = await _SettingsDashBoardService.GetSettingsDashBoardList();
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
