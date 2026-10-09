using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DashBoard;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.DashBoard;
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
    public class RecruitmentDashBoardController : Controller
    {
        private readonly IRecruitmentDashBoardService _recruitmentDashBoardService;
        private readonly IDropdownService _dropdownService;
        public RecruitmentDashBoardController(IRecruitmentDashBoardService recruitmentDashBoardService , 
        IDropdownService dropdownService)        {
            _recruitmentDashBoardService = recruitmentDashBoardService;
            _dropdownService = dropdownService;    
        }

        public async Task<IActionResult> Index(int? ClassifnType= 2, int? Classifn=0)
        {
            try
            {
                RecruitmentDashBoard model = new RecruitmentDashBoard();

                ViewBag.ClassifnType = new SelectList(await _dropdownService.GetFilterBydDropDown(), "Code", "Name");
           
                if ((ClassifnType ?? 0) == 4 && (Classifn ?? 0) == 0)
                {                    
                    model = await _recruitmentDashBoardService.GetDashBoardDetails(ClassifnType.Value, 0);
               
                }
                else
                {              
                    model = await _recruitmentDashBoardService.GetDashBoardDetails(ClassifnType.Value, Classifn.Value);
                }
                ViewBag.Classifn = new SelectList(await _dropdownService.GetRecruitmentdDropDown(ClassifnType.Value), "Code", "Name");                
                model.ClassificationType = new Shared.General.Dropdown();
                model.ClassificationType.Code = ClassifnType.Value;
                model.Classification = new Shared.General.Dropdown();
                model.Classification.Code = Classifn.Value;

                //chart

                 var pie= await _recruitmentDashBoardService.GetBarDiagramDetails(ClassifnType.Value, Classifn.Value, "Pie");
                var Funnel = await _recruitmentDashBoardService.GetBarDiagramDetails(ClassifnType.Value, Classifn.Value, "Funnel");
                var Bar = await _recruitmentDashBoardService.GetBarDiagramDetails(ClassifnType.Value, Classifn.Value, "Bar");
                ViewBag.Pie = JsonConvert.SerializeObject(pie.ChartList);
                ViewBag.Funnel= JsonConvert.SerializeObject(Funnel.ChartList);
                ViewBag.Bar = JsonConvert.SerializeObject(Bar.ChartList);
                model.ChartList = pie.ChartList;
                model.BarChartList = Bar.ChartList;
                model.FunnelChartList = Funnel.ChartList;
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
        public async Task<IActionResult> IndexV2()
        {
            try
            {                
                return View();
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

        public async Task<IActionResult> IndexV3()
        {
            try
            {
                return View();
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
        public async Task<IActionResult> GetPieChart(int? ClassifnType = 2, int? Classifn = 0)
        {
            RecruitmentDashBoard model = new RecruitmentDashBoard();
            model = await _recruitmentDashBoardService.GetBarDiagramDetails(ClassifnType.Value, Classifn.Value, "Pie");
            return PartialView("_PieChart", model.ChartList);
        }
        [HttpGet]
        public async Task<JsonResult> GetClassifnDropdown(int ClassifnType)
        {

            var classfn = new SelectList(await _dropdownService.GetRecruitmentdDropDown(ClassifnType), "Code", "Name");
            return Json(classfn);
        }

        public async Task<IActionResult> GetVacancyNearlyDuesPopUp()
        {

            try
            {
                RecruitmentDashBoard model = new RecruitmentDashBoard();
                model = await _recruitmentDashBoardService.GetVacancyNearlyDuesPopUpList();
                return PartialView("_VacancyNearlyDuePopUp", model);

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
