using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DashBoard;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.MIS;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.DashBoard;
using Hrms.Lite.Shared.MIS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.DashBoard.Controllers
{
    [Area("DashBoard")]
    [SessionAuthorize]
    public class HRPlanningDashBoardController : Controller
    {
        #region Private properties
        private readonly IHRPlanningDashBoardService _service;
        private readonly IMISDashBoardService _mis;
        private readonly IDropdownService _drop;
        #endregion
        #region Constructor  
        public HRPlanningDashBoardController(IHRPlanningDashBoardService service, IMISDashBoardService mis, IDropdownService drop)
        {
            _service = service;
            _mis = mis;
            _drop = drop;

        }
        #endregion
        public IActionResult Dummy()
        {
            return View();
        }
            public async Task<IActionResult> Index()
        {
            HRPlanningDashBoard model = new HRPlanningDashBoard();
            model = await _service.GetDashBoardDetails();
            ViewBag.period = new SelectList(await _drop.GetHRPlanningDropDown(), "Code", "Name");
            model.SepJoiningClassifnDiagram = _service.GetSepJoiningClassifnDiagram(model.PlanningPeriod).Result.SepJoiningClassifnDiagram;

          
            model.Todate = DateTime.Now.ToString("dd-MMM-yyyy");
            return View(model);          
        }
        public async Task<IActionResult> GetResignationMonthwiseCount()
        {
            try
            {
                MISDashBoard model = new MISDashBoard();              
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));                
                model = await _mis.GetResignationMonthwiseCount(User.GroupCompanyCode);              
                return Json(model.MonthWiseTerminationList);
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

        [HttpGet]
        public async Task<IActionResult> getClassifnDiagram(int Period)
        {
            HRPlanningDashBoard model = new HRPlanningDashBoard();
            model = await _service.GetSepJoiningClassifnDiagram(Period);
            return PartialView("_SepJoiningClassifnDiagram", model.SepJoiningClassifnDiagram);
        }
    }
}
