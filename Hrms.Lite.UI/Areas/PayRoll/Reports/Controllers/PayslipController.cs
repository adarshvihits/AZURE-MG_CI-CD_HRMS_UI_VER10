using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.PayRoll.Reports;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Hrms.Lite.Shared.PayRoll.Reports;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hrms.Lite.UI.Areas.PayRoll.Reports.Controllers
{
    [Area("PayRoll")]
    [SessionAuthorize]
    public class PayslipController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly IPayslipService _payslipService;
        private readonly ICommonService _commonService;
        public class Global
        {
            public static Filter GlobalFilter;
           

                public static string url;
                public static string paylip;

        }
        public PayslipController(IDropdownService dropdownService, IPayslipService payslipService, ICommonService commonService)
        {
            _dropdownService = dropdownService;
            _payslipService = payslipService;
            _commonService = commonService;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                PaySlip model = new PaySlip();
            PaySlip m = new PaySlip();
       
            model.Url = new Url();
            ViewBag.FinancialYr = new SelectList(await _dropdownService.GetFinancialYearDropdown(),"Code","Name");

                var FCode = await _commonService.GetCurrentLFinancialYear();
            var CMonthCode = await _commonService.GetCurrentMonthDetails();
            model = await _payslipService.GetMonths(FCode.Data);            
            model.FinancialYear = new FinancialYear();
            model.FinancialYear.Code = FCode.Data;
            model.CurrentSalaryPeriodCode = CMonthCode.LastProcessedMonth;
            await fillfilterDropdown();
            model.Url = await _payslipService.GetPaySlip();
            Global.paylip = model.Url.Payslip;

            model.Url = await _payslipService.GetUrl();
            Global.url = model.Url.Urllink;
            model.Url.Urllink = Global.url;
            model.Url.Payslip = Global.paylip;
            return View("~/Areas/PayRoll/Reports/Views/Payslip/Index.cshtml",model);
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
        public async Task<IActionResult> GetMonths(int FinancialYrCode)
        {
            PaySlip model = new PaySlip();
            model = await _payslipService.GetMonths(FinancialYrCode);
            ViewBag.FinancialYr = new SelectList(await _dropdownService.GetFinancialYearDropdown(), "Code", "Name");
            model.FinancialYear = new FinancialYear();
            model.FinancialYear.Code = FinancialYrCode;
            return PartialView("~/Areas/PayRoll/Reports/Views/Payslip/_Months.cshtml", model);
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
                PaySlip model = new PaySlip();
                model.Url = new Url();
                Filter filter = new Filter();
                Global.GlobalFilter = Filter;

                model.Url= await _payslipService.GetPaySlip();
                string paySlip = model.Url.Payslip;
                model.Url = await _payslipService.GetUrl();
                string url = model.Url.Urllink;
                if (Filter.searchValue == "undefined" || Filter.searchValue == null)
                {
                    Filter.searchValue = "";
                }
                if (CurrentSalryPeriodCode != 0)
                {
                    model = await _payslipService.GetEmployeeList(CurrentSalryPeriodCode, Filter);
                }
                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model == null || model.EmployeeList == null || model.EmployeeList.Count() == 0)
                {


                    return Json(new
                    {
                        data = "",
                        paySlip = paySlip,
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
                        paySlip = paySlip,
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
                        url=url,
                        paySlip = paySlip,
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
