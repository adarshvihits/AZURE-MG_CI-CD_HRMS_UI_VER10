using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.TDS.Reports;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Hrms.Lite.Shared.TDS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.TDS.Controllers
{
    [Area("TDS")]
    public class TDSSlipController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly ITDSSlipService _TDSSlipService;
        private readonly ICommonService _commonService;
        public class Global
        {
            public static Filter GlobalFilter;


            public static string url;
            public static string paylip;

        }
        public TDSSlipController(IDropdownService dropdownService, ITDSSlipService TDSSlipService, ICommonService commonService)
        {
            _dropdownService = dropdownService;
            _TDSSlipService = TDSSlipService;
            _commonService = commonService;
        }
        public async Task<IActionResult> Index()
        {
            TDSSlip model = new TDSSlip();
            TDSSlip m = new TDSSlip();

            model.Url = new Url();
         
            ViewBag.AssessmentYear = new SelectList(await _dropdownService.GetAssessmentYearDropdown(), "Code", "Name");
            var AssCode = await _commonService.GetCurrentAssessmentYear();
            var CMonthCode = await _commonService.GetCurrentMonthDetails();
            model = await _TDSSlipService.GetMonths(AssCode.Data);
            model.AssessmentYear = new AssessmentYear();
            model.AssessmentYear.Code = AssCode.Data;
            model.CurrentSalaryPeriodCode = CMonthCode.LastProcessedMonth;
            await fillfilterDropdown();
            model.Url = await _TDSSlipService.GetTDSSlip();
            Global.paylip = model.Url.TDSSlip;

            model.Url = await _TDSSlipService.GetUrl();
            Global.url = model.Url.Urllink;
            model.Url.Urllink = Global.url;
            model.Url.TDSSlip = Global.paylip;
            return View("~/Areas/TDS/Reports/Views/TDSSlip/Index.cshtml", model);
        }
        public async Task<IActionResult> GetMonths(int AssessmentYrCode)
        {
            TDSSlip model = new TDSSlip();
            model = await _TDSSlipService.GetMonths(AssessmentYrCode);
            ViewBag.AssessmentYear = new SelectList(await _dropdownService.GetAssessmentYearDropdown(), "Code", "Name");
            model.AssessmentYear = new AssessmentYear();
            model.AssessmentYear.Code = AssessmentYrCode;
            return PartialView("~/Areas/TDS/Reports/Views/TDSSlip/_Months.cshtml", model);
        }
        public async Task fillfilterDropdown()
        {

            ViewBag.Gen = new SelectList(await _dropdownService.GetGenderDropDown(), "Code", "Name");
            ViewBag.Cat = new SelectList(await _dropdownService.GetCategoryDropdown(), "Code", "Name");
            ViewBag.Grd = new SelectList(await _dropdownService.GetGradeDropdown(), "Code", "Name");
            ViewBag.Dsg = new SelectList(await _dropdownService.GetDesignationDropdown(), "Code", "Name");
            ViewBag.Dept = new SelectList(await _dropdownService.GetDepartmentDropdown(), "Code", "Name");
            ViewBag.Loc = new SelectList(await _dropdownService.GetLocationDropdown(), "Code", "Name");
            ViewBag.PayrollGrp = new SelectList(await _dropdownService.GetPayrollGroupDropdown(), "Code", "Name");

            ViewBag.division = new SelectList(await _dropdownService.GetDivisionDropdown(), "Code", "Name");
            ViewBag.section = new SelectList(await _dropdownService.GetSectionDropdown(), "Code", "Name");
            ViewBag.EmploymentType = new SelectList(await _dropdownService.GetEmploymentTypeDropdown(), "Code", "Name");
            ViewBag.EmploymentStatus = new SelectList(await _dropdownService.GetEmploymentStatusDropdown(), "Code", "Name");
            ViewBag.WorkLocation = new SelectList(await _dropdownService.GetWorkLocationDropdown(), "Code", "Name");
            ViewBag.WageGrade = new SelectList(await _dropdownService.GetWageGradeDropdown(), "Code", "Name");

        }
        [HttpPost]
        public async Task<IActionResult> GetEmployeeList(Filter Filter, int CurrentSalryPeriodCode)
        {
            try
            {
                if (CurrentSalryPeriodCode == 0)
                {
                    var CMonthCode = await _commonService.GetCurrentMonthDropdown();
                    CurrentSalryPeriodCode = CMonthCode.Data;
                }
                await fillfilterDropdown();
                TDSSlip model = new TDSSlip();
                model.Url = new Url();
                Filter filter = new Filter();
                Global.GlobalFilter = Filter;

                model.Url = await _TDSSlipService.GetTDSSlip();
                string TDSSlip = model.Url.TDSSlip;
                model.Url = await _TDSSlipService.GetUrl();
                string url = model.Url.Urllink;
                if (Filter.searchValue == "undefined" || Filter.searchValue == null)
                {
                    Filter.searchValue = "";
                }
                if (CurrentSalryPeriodCode != 0)
                {
                    model = await _TDSSlipService.GetEmployeeList(CurrentSalryPeriodCode, Filter);
                }
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model == null || model.EmployeeList == null || model.EmployeeList.Count() == 0)
                {


                    return Json(new
                    {
                        data = "",
                        TDSSlip = TDSSlip,
                        draw = Filter.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
                }




                if (model.EmployeeList.Count > 0)
                {
                    totalrows = model.EmployeeList[0].TotalCount;


                }

                //model.AllTabCount = m.AllTabCount;
                if (Filter.searchValue != "")
                {
                    totalrowsafterfiltering = model.EmployeeList[0].ACTIVE;
                    return Json(new
                    {
                        data = model.EmployeeList,
                        url = url,
                        TDSSlip = TDSSlip,
                        draw = Filter.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows//totalrowsafterfiltering
                    });
                }
                else
                {
                    return Json(new
                    {
                        data = model.EmployeeList,
                        url = url,
                        TDSSlip = TDSSlip,
                        draw = Filter.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });
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
