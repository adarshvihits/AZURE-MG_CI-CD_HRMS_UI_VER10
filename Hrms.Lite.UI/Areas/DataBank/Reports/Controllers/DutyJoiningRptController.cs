using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank.Reports;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.DataBank.Reports.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]
    public class DutyJoiningRptController : Controller
    {

        private readonly IDutyJoiningRptService _DutyJoiningRptService;
        private readonly IDropdownService _dropdownService;
        private readonly ICommonService _commonService;
        //private readonly IHelpers _helpers;
        public DutyJoiningRptController(IDutyJoiningRptService DutyJoiningRptService,
          ICommonService commonService, IHelpers Helpers, IDropdownService dropdownService)
        {
            _DutyJoiningRptService = DutyJoiningRptService;
            _commonService = commonService;
           // _helpers = Helpers;
            _dropdownService = dropdownService;
        }
        public async Task <IActionResult> Index()
        {
            try{
            DutyJoiningRpt model = new DutyJoiningRpt();
            await fillfilterDropdown();
            Filter filter = new Filter();
            model = await _DutyJoiningRptService.GetList(filter);
            //   ViewBag.ConsoleType = new SelectList(await _dropdownService.GetConsoleTypesDropDown(), "Code", "Name");
            //model.Filter = new Filter();
            //int Month = 0;
            return View("~/Areas/DataBank/Reports/Views/DutyJoiningRpt/Index.cshtml", model);
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
            ViewBag.month = new SelectList(await _dropdownService.GetPayrollReportMonthDropdown(), "Code", "Name");
        }


        public async Task<IActionResult> GetList()
        {
            try
            {
                DutyJoiningRpt model = new DutyJoiningRpt();
                Filter filter = new Filter();
                model = await _DutyJoiningRptService.GetList(filter);
                //model.ConsoleCode = ConsoleType;
                //model.LoanIssuesList = (List<LoanIssue>)await _loanTopUpservice.GetLoanTopUpList(loantype);
                return PartialView("~/Areas/DataBank/Reports/Views/DutyJoiningRpt/_List.cshtml", model);

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




        public async Task<IActionResult> GetDutyJoiningRptPDF(int EmployeeCode)
        {
            KendoReport model = new KendoReport();
            model.EmpCode = EmployeeCode;
         
            model.Url = await _commonService.GetUrl();

            return View("~/Areas/DataBank/Reports/Views/DutyJoiningRpt/DutyJoiningRptPDF.cshtml", model);


        }


        public async Task<IActionResult> GetEmployeeInformationSheetPDF(int EmployeeCode)
        {
            KendoReport model = new KendoReport();
            model.EmpCode = EmployeeCode;

            model.Url = await _commonService.GetUrl();

            return View("~/Areas/DataBank/Reports/Views/DutyJoiningRpt/EmployeeInformationSheetPDF.cshtml", model);


        }

    }
}
