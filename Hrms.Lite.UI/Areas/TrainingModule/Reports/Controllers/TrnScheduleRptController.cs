using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.TrainingModule;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TrainingModule;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.TrainingModule.Reports.Controllers
{
    [Area("TrainingModule")]
    [SessionAuthorize]
    public class TrnScheduleRptController : Controller
    {
        private readonly ITrnScheduleRptService _trnScheduleRptService;
        private readonly IDropdownService _dropdownService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        public TrnScheduleRptController(IDropdownService dropdownService, ICommonService commonService, IHelpers helpers, ITrnScheduleRptService trnScheduleRptService)
        {
            _trnScheduleRptService = trnScheduleRptService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;

        }
        public async Task<IActionResult> Index()
        {

            try
            {

                TrainingSchedule model = new TrainingSchedule();

                ViewBag.FinancialYear = new SelectList(await _dropdownService.GetFinancialYearDropdown(), "Code", "Name");
                var CurrenYear = await _commonService.GetCurrentLFinancialYear();
                model = await _trnScheduleRptService.GetDashboardCountList(CurrenYear.Data);
                var dataSet = await _trnScheduleRptService.GetTrnScheduleRptList(CurrenYear.Data, 0);
                model.DataTable = dataSet.Tables[0];

      

               model.FinancialYear = new Shared.Master.FinancialYear();
               model.FinancialYear.Code = CurrenYear.Data;

                return View("~/Areas/TrainingModule/Reports/Views/TrnScheduleRpt/Index.cshtml", model);
          
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

        public async Task<IActionResult> GetList( int FinancialYearCode, int Category)
        {
            try
            {
                TrainingSchedule model = new TrainingSchedule();
                model = await _trnScheduleRptService.GetDashboardCountList(FinancialYearCode);

                var dataSet = await _trnScheduleRptService.GetTrnScheduleRptList(FinancialYearCode, Category);
                model.DataTable = dataSet.Tables[0];
                return View("~/Areas/TrainingModule/Reports/Views/TrnScheduleRpt/_ScheduleRptList.cshtml", model);
                //model = await _trnScheduleRptService.GetTrnScheduleRptList( FinancialYearCode, Category);
                //return View("~/Areas/TrainingModule/Reports/Views/TrnScheduleRpt/_ScheduleRptList.cshtml", model);
                //return View("_TrainingSchedule", model);

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
