using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.PayRoll;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using Hrms.Lite.Shared.PayRoll.Reports;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hrms.Lite.UI.Areas.PayRoll.Controllers
{
    [Area("PayRoll")]
    [SessionAuthorize]
    public class MonthlySalaryProcessingIndividualController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly IMonthlySalaryProcessingIndividualService _service;
        private readonly ICommonService _commonService;
        public MonthlySalaryProcessingIndividualController(IDropdownService dropdownService,IMonthlySalaryProcessingIndividualService service,ICommonService commonService)
        {
            _dropdownService = dropdownService;
            _service = service;
            _commonService = commonService;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                SalaryProcessing model = new SalaryProcessing();
                ViewBag.Month = new SelectList(await _dropdownService.GetSalaryProcessing_MonthDropdown(0), "Code", "Name");
                model = await _service.Individual_SalaryProcessingValidations(0, 0);
                var currMonth = await _commonService.GetCurrentMonthDropdown();
                ViewBag.Emp = new SelectList(await _dropdownService.GetIndividual_ProcessingEmployeeDropDown(0, currMonth.Data), "Code", "Name");
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
        public async Task<IActionResult> GetIndividual_SalaryProcessingValidations(int SalaryPeriodCode, int EmployeeCode)
        {
            try
            {
                SalaryProcessing model = new SalaryProcessing();
                model = await _service.Individual_SalaryProcessingValidations(SalaryPeriodCode, EmployeeCode);
                model.Employee.Code = EmployeeCode;
                return PartialView("_PopUp", model);
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
        public async Task<IActionResult> GetIndividual_SalaryProcessing(SalaryProcessing SP)
        {
            try
            {
                SalaryProcessing model = new SalaryProcessing();
                var response = await _service.Individual_SalaryProcessing(SP);
                if (response.Success != true && response.Message == "NEGATIVE SALARY")
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

        public async Task<IActionResult> Individual_SalaryProcessing_Informations(int Month, int EmployeeCode, Guid EmployeeGI)
        {
            try
            {
                SalaryProcessing model = new SalaryProcessing();
                model.SalaryIntegrityReport = await _service.Individual_SalaryProcessing_Informations(Month, EmployeeCode);
                model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);
                return PartialView("_List", model);
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
        public async Task<IActionResult> GetEmployeeDropdown(int Month, int EmpStatus)
        {
            try
            {
                var EmployeeDropdown = await _dropdownService.GetIndividual_ProcessingEmployeeDropDown(EmpStatus, Month);
                return Json(EmployeeDropdown);
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
