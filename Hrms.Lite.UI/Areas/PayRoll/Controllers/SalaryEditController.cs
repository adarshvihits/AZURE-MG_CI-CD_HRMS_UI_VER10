using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.PayRoll;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.PayRoll.Controllers
{
    [Area("PayRoll")]
    [SessionAuthorize]
    public class SalaryEditController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly ISalaryEditService _SalaryEditService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        public SalaryEditController(ISalaryEditService SalaryEditService, IDropdownService dropdownService, ICommonService commonService, IHelpers Helpers)
        {
            _SalaryEditService = SalaryEditService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = Helpers;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                SalaryEdit model = new SalaryEdit();
                ViewBag.Payroll = new SelectList(await _dropdownService.GetPayrollGroupWithGrpCmpyCheckDropdown(), "Code", "Name");
                ViewBag.month = new SelectList(await _dropdownService.GetMonthDropdown(), "Code", "Name");

                //ViewBag.month = new SelectList(await _dropdownService.GetSalaryPostingMonthDropdown(0), "Code", "Name");
                Filter filter = new Filter();
                filter.length = 10;
                model = await _SalaryEditService.GetList(0, 0, filter);
                model.filter = await _commonService.GetFilterDropdowns();
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
        public async Task<IActionResult> GetList(Filter filter1, int Month, int PayrollGroupCode)
        {
            try
            {
                SalaryEdit model = new SalaryEdit();
                SalaryEdit m = new SalaryEdit();

                Filter filter = new Filter();


                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }
 
                if (Month != 0 && PayrollGroupCode != 0)
                {
                    model = await _SalaryEditService.GetList(Month, PayrollGroupCode, filter1);
                }
                m.SalaryEditList = model.SalaryEditList;
                int totalrows = 0;
                int totalrowsafterfiltering = 0;
                decimal totalAmt = 0;
                if (model.SalaryEditList == null)
                {


                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows,
                        TotalAmount = totalAmt
                    });
                }

                if (model.SalaryEditList.Count > 0)
                {
                    totalrows = m.SalaryEditList[0].active;
                    totalAmt = m.SalaryEditList[0].TotalAmount;

                }


                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = m.SalaryEditList[0].totalRecords;

                    return Json(new
                    {
                        data = model.SalaryEditList,
                        draw = filter1.draw,//Request.Form["draw"],//filter1.draw, //Request.Form["draw"],
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows,//totalrowsafterfiltering
                        TotalAmount = totalAmt

                    });
                }
                else
                {

                    return Json(new
                    {
                        data = model.SalaryEditList,
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows,
                        TotalAmount = totalAmt


                    });
                }


            }
            catch (Exception ex)
            {
                return View();
            }

        }
        public async Task<ActionResult> GetSalaryEditDetails(int Month,Guid EmployeeGI)
        {
            SalaryEdit model = new SalaryEdit();

            model = await _SalaryEditService.GetSalaryEditDetails(Month,EmployeeGI);
            model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);
            model.Month = Month;
            return View("SalaryEditDetails", model);
        }
        [HttpPost]
        public async Task<ActionResult> Save(Guid EmployeeGI,string RowType, int TrnCode, int Month,  decimal Total)
        {
            try
            {
                SalaryEdit model = new SalaryEdit();
                model.Month = Month;
                model.EmployeeHeader = new EmployeeHeader();
                model.EmployeeHeader.EmployeeGuid = EmployeeGI;
                model.SalaryEditDetailsList = new List<SalaryEditDetails>
                {
                new SalaryEditDetails
                {
                DescriptionCode = TrnCode,
                Total = Total,
                RowType=RowType

                }
                };
                var response = await _SalaryEditService.SalaryEditSave(model);
                return Json(response);
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
