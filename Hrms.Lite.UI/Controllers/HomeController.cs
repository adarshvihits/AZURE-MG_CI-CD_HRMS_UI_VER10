using Hrms.Lite.Services.IServices.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Test.Models;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DashBoard;
using Hrms.Lite.Shared.DashBoard;
using Newtonsoft.Json;
using Hrms.Lite.Services.IServices;
using Microsoft.AspNetCore.Http;
using Hrms.Lite.Shared.Account;

namespace Hrms.Lite.Controllers
{
    [SessionAuthorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeService _homeService;
        private readonly IDataBankDashBoardService _dashBoardservice;
        private readonly IMenuService _menuService;

        public HomeController(IHomeService homeService,ILogger<HomeController> logger, IDataBankDashBoardService dashBoardservice, IMenuService menuService)
        {
            _homeService = homeService;
            _logger = logger;
            _dashBoardservice = dashBoardservice;
            _menuService = menuService;
        }

        public IActionResult Index()
        {
            try
            {
                Login  login = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                if (login.authUser.DashBoardRoute == "DataBankDashBoard")
                {
                    return RedirectToAction("Index", "LandingPage", new { Area = "DashBoard" });
                }
                else if (login.authUser.DashBoardRoute == "Home")
                {
                    return View("StaticDashBoard");
                }
                else
                {
                    if (login.authUser.DashBoard == "D2")
                        return RedirectToAction("IndexV3", login.authUser.DashBoardRoute, new { Area = "DashBoard" });
                    else
                        return RedirectToAction("Index", login.authUser.DashBoardRoute, new { Area = "DashBoard" });
                }

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
        public async Task<IActionResult> GetMenu(int ModuleCode,string Name)
        {
            MenuEntity ModuleList = await _menuService.GetModuleList("HRMS");
            MenuEntity menu = await _menuService.GetModuleVsMenuList(ModuleCode);           
            var LoginUser = HttpContext.Session.GetString("User");
            Login login = JsonConvert.DeserializeObject<Login>(LoginUser);
            login.MenuEntity = menu;
            login.MenuEntity.Module = ModuleList.Module;
            HttpContext.Session.SetString("User", JsonConvert.SerializeObject(login));
            if (login.authUser.DashBoard == "D1")
            {
                if (Name == "Databank" || Name == "HRIS")
                    return RedirectToAction("Index", "DataBankDashBoard", new { Area = "DashBoard" });
                else if (Name == "Payroll")
                    return RedirectToAction("Index", "PayRollDashBoard", new { Area = "DashBoard" });
                else if (Name == "Time Office")
                    return RedirectToAction("Index", "TimeOfficeDashBoard", new { Area = "DashBoard" });
                else if (Name == "Recruitment")
                    return RedirectToAction("Index", "RecruitmentDashBoard", new { Area = "DashBoard" });
                else if (Name == "Settings")
                    return RedirectToAction("Index", "SettingsDashBoard", new { Area = "DashBoard" });
                else if (Name == "Survey")
                    return RedirectToAction("Index", "Surveygraph", new { Area = "SURVEY" });
                else if (Name == "PMS")
                    return RedirectToAction("Index", "AppraisalWtgChart", new { Area = "PMS" });
                else if (Name == "Training")
                    return RedirectToAction("Index", "TrnScheduleRpt", new { Area = "TrainingModule" });
                else if (Name == "PPR")
                    return RedirectToAction("Index", "PPRBI", new { Area = "PPR" });
                else if (Name == "Admin")
                    return RedirectToAction("Index", "AdminDashBoard", new { Area = "DashBoard" });
                else if (Name == "Succession Planning")
                    return RedirectToAction("Index", "SuccessionPlanningDashboard", new { Area = "DashBoard" });
                else if (Name == "HR Planning")
                    return RedirectToAction("Index", "HRPlanningDashBoard", new { Area = "DashBoard" });
                else if (Name == "ELearning")
                    return RedirectToAction("Index", "ELearningBI", new { Area = "ELearning" });
                else if (Name == "Travel Claim")
                    return RedirectToAction("Index", "TravelClaimDashBoard", new { Area = "DashBoard" });
                else if (Name == "Custom")
                    return View("CustomView");

                else
                    //return View("StaticDashBoard");
                    return RedirectToAction("Index", "LandingPage", new { Area = "DashBoard" });
            }
            else
            {
                if (Name == "Databank" || Name == "HRIS")
                    return RedirectToAction("IndexV3", "DataBankDashBoard", new { Area = "DashBoard" });
                else if (Name == "Payroll")
                    return RedirectToAction("IndexV3", "PayRollDashBoard", new { Area = "DashBoard" });
                else if (Name == "Time Office")
                    return RedirectToAction("IndexV3", "TimeOfficeDashBoard", new { Area = "DashBoard" });
                else if (Name == "Recruitment")
                    return RedirectToAction("Index", "RecruitmentDashBoard", new { Area = "DashBoard" });
                else if (Name == "Settings")
                    return RedirectToAction("Index", "SettingsDashBoard", new { Area = "DashBoard" });
                else if (Name == "PMS")

                    return RedirectToAction("Index", "AppraisalWtgChart", new { Area = "PMS" });
                else if (Name == "Survey")
                    return RedirectToAction("Index", "Surveygraph", new { Area = "SURVEY" });
                else if (Name == "Training")
                    return RedirectToAction("Index", "TrnScheduleRpt", new { Area = "TrainingModule" });
               
                else if (Name == "PPR")
                    return RedirectToAction("Index", "PPRBI", new { Area = "PPR" });
                else if (Name == "Admin")
                    return RedirectToAction("Index", "AdminDashBoard", new { Area = "DashBoard" });
                else if (Name == "Succession Planning")
                    return RedirectToAction("Index", "SuccessionPlanningDashboard", new { Area = "DashBoard" });
                else if (Name == "HR Planning")
                     return RedirectToAction("Index", "HRPlanningDashBoard", new { Area = "DashBoard" });
                else if (Name == "ELearning")
                    return RedirectToAction("Index", "ELearningBI", new { Area = "ELearning" });
                else if (Name == "Travel Claim")
                    return RedirectToAction("Index", "TravelClaimDashBoard", new { Area = "DashBoard" });
                else if (Name == "Custom")
                    return View("CustomView");
                else
                    //return View("StaticDashBoard");
                    return RedirectToAction("Index", "LandingPage", new { Area = "DashBoard" });
            }
            
             
        }

        public IActionResult Time()
        {
            TimeOfficeDashBoard model = new TimeOfficeDashBoard();
            return View("_Time", model);
        }


        public IActionResult Rec()
        {
            RecruitmentDashBoard model = new RecruitmentDashBoard();
            return View("_Recruit", model);
        }
      
       
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpPost]
        public async Task<IActionResult> ChangeTheme(string ThemeName)
        {
            Theme model = new Theme();
            model.ThemeName = ThemeName;
            var response = await _homeService.SaveTheme(model);
            return Json("Saved Successfully");
        }
        [HttpPost]
       public async Task<IActionResult> ToggleDarkMode(int IsDarkMode)
        {
            var response = await _homeService.ToggleDarkMode(IsDarkMode);
            return Json("Saved Successfully");
        }
        public async Task<IActionResult> GetTheme()
        {
            Theme model = new Theme();
            model = await _homeService.GetTheme();

            return Json(model.ThemeName);
        }
        public async Task<IActionResult> GetDarkModeStatus()
        {
            Theme model = new Theme();
            model = await _homeService.GetDarkModeStatus();

            return Json(model.IsDarkMode);
        }
        public IActionResult ShowInvalidInput()
        {
            TempData["InvalidEntryDetection"] = "Invalid Input";
            return Json(true);
        }
    }
}
