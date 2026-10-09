using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.PayRoll.Reports;
using Hrms.Lite.Services.Services.PayRoll.Reports;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll.Reports;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.PayRoll.Reports.Controllers
{
    [Area("PayRoll")]
    [SessionAuthorize]
    public class PTReportStatutoryController : Controller
    {
        private readonly IPTReportStatutoryService _PTReportStatutoryService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        private readonly IDropdownService _dropdownService;

        public PTReportStatutoryController(IPTReportStatutoryService PTReportStatutoryService, ICommonService commonService, IHelpers Helpers, IDropdownService dropdownService)
        {
            _PTReportStatutoryService = PTReportStatutoryService;
            _commonService = commonService;
            _helpers = Helpers;
            _dropdownService = dropdownService;
        }


        public async Task<IActionResult> Index()
        {
            PTReportStatutory model = new PTReportStatutory();
            await fillfilterDropdown();
            ViewBag.PTGrpMonth = new SelectList(await _dropdownService.GetPayrollReportMonthDropdown(), "Code", "Name");
            Filter filter = new Filter();
            int month = 0;
            model = await _PTReportStatutoryService.GetList(month,filter);
            model.Filter = filter;
           
            return View("~/Areas/PayRoll/Reports/Views/PTReportStatutory/Index.cshtml", model);
        }


        [HttpPost]
        public async Task<ActionResult> GetList(Filter filter, int Month)
        {
            try
            {
                PTReportStatutory model = new PTReportStatutory();

                if (Month != 0 )
                {
                    model = await _PTReportStatutoryService.GetList(Month, filter);
                }

                return PartialView("~/Areas/PayRoll/Reports/Views/PTReportStatutory/_List.cshtml", model);
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


        public async Task<IActionResult> GetPTReportStatutoryPrint(int Month)
        {
            try
            {

                PTReportStatutory model = new PTReportStatutory();
                model.Url = await _commonService.GetUrl();            
                model.Month = Month;
              
                return PartialView("~/Areas/PayRoll/Reports/Views/PTReportStatutory/Print.cshtml", model);
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







    }
}
