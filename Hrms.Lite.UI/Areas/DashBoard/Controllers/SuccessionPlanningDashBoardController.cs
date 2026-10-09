using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DashBoard;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Shared.DashBoard;
using Hrms.Lite.Shared.DataBank.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.DashBoard.Controllers
{
    [Area("DashBoard")]
    [SessionAuthorize]
    public class SuccessionPlanningDashBoardController : Controller
    {
        public readonly ISuccessionDashBoardService _service;
        private readonly ISuccessionPlanningService _planning;
        public SuccessionPlanningDashBoardController(ISuccessionDashBoardService service, ISuccessionPlanningService planning)
        {
            _service = service;
            _planning = planning;

        }
        public async Task<IActionResult> Index()
        {
            SuccessionDashboard model = new SuccessionDashboard();
            SuccessionDashboard _model = new SuccessionDashboard();
            model= await _service.GetSuccessionDashboardDetails();
            _model= await _service.GetDashBoardDiagram("Department");
            _model.PlanningGrid = model.PlanningGrid; 
            return View(_model);
        }
        [HttpGet]
        public async Task<IActionResult> getClassifnDiagram(string Type)
        {
            SuccessionDashboard model = new SuccessionDashboard();
            model = await _service.GetDashBoardDiagram(Type);
            return PartialView("_ClassifnDiagram", model.ClassifnDiagram);
        }
        [HttpGet]
        public async Task<IActionResult> GetSuccessorList(Guid EmployeeGI, Guid KeyPositionGI)
        {
            SuccessionDashboard model = new SuccessionDashboard();
            SuccessionPlanning plan = new SuccessionPlanning();
            plan = await _planning.GetSuccessionlanningDetails(KeyPositionGI, EmployeeGI);
            model.SuccessorsList = plan.SuccessorsList;
            return PartialView("_SuccessorList", model);
        }
        public IActionResult Index1()
        {
            return View("Index1");
        }
    }
}
