using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.PayRoll;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hrms.Lite.UI.Areas.PayRoll.Controllers
{
    [Area("PayRoll")]
    [SessionAuthorize]
    public class DailyFandFSalaryProcessingController : Controller
    {
        private readonly IDailyFandFSalaryProcessingService _DailyFandFService;
        private readonly IDropdownService _dropdownService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        private readonly IWebHostEnvironment _Environment;

        public DailyFandFSalaryProcessingController(IDailyFandFSalaryProcessingService DailyFandFService, IDropdownService dropdownService, ICommonService commonService, IHelpers helpers, IWebHostEnvironment environment)
        {
            _DailyFandFService = DailyFandFService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;
            _Environment = environment;
        }
        public async Task<IActionResult> Index()
        {
            try { 
            DailyFandFSalaryProcessing model = new DailyFandFSalaryProcessing();
            Filter filter = new Filter();
            filter.length = 10;
            model = await _DailyFandFService.GetList(0, 0, filter);
            model.PayrollMonth = new Dropdown();
            model.PayrollGroup = new Shared.Master.PayrollGroup();
            ViewBag.PayrollGroup = new SelectList(await _dropdownService.GetPayrollGroupWithGrpCmpyCheckDropdown(), "Code", "Name");
            ViewBag.month = new SelectList(await _dropdownService.GetFandFSalaryProcessing_MonthDropdown(0), "Code", "Name");
            model.Filter = await _commonService.GetFilterDropdowns();
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
        public async Task<IActionResult> GetList(int Month, int PayrollGroup, Filter filter)
        {
            try
            {
                DailyFandFSalaryProcessing model = new DailyFandFSalaryProcessing();
                model = await _DailyFandFService.GetList(Month, PayrollGroup, filter);
                return PartialView("_FandFList", model);
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
        public async Task<IActionResult> GetMonth(int Payrollgrp)
        {
            try
            {

                var Month = await _dropdownService.GetFandFSalaryProcessing_MonthDropdown(Payrollgrp);
                return Json(Month);

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
        public async Task<IActionResult> GetDailyFandF_SalaryProcessing(DailyFandFSalaryProcessing model, int PayrollMonth,int PayrollGroup)
        {
            try
            {

                model.PayrollGroup = new Shared.Master.PayrollGroup();
                model.PayrollMonth = new Dropdown();
                model.PayrollMonth.Code = PayrollMonth;
                model.PayrollGroup.Code = PayrollGroup;
                var response = await _DailyFandFService.GetDailyFandF_SalaryProcessing(model);
                if (response.Data != null && response.Data.DailyFandFProcessing_popup != null && response.Data.DailyFandFProcessing_popup.Count > 0)
                {
                    model = response.Data;
                    return PartialView("_NegativeSalary", model);
                }
                else
                {
                    return Json(response);
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

    }
}
