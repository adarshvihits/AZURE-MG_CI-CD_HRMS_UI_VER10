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
    public class JVPostingController : Controller
    {
        private readonly IDropdownService _dropdownService;
        public readonly IJVPostingService _service;
        public readonly ICommonService _commonService;
        public JVPostingController(IJVPostingService service, ICommonService commonService, IDropdownService dropdownService)
        {
            _service = service;
            _dropdownService = dropdownService;
            _commonService = commonService;
        }
        public async Task<IActionResult> Index()
        {
            try { 
            JVPosting model = new JVPosting();
            model.PayrollMonth = new Dropdown();
            model.PayrollGroup = new Shared.Master.PayrollGroup();
            ViewBag.PayrollGroup = new SelectList(await _dropdownService.GetPayrollGroupWithGrpCmpyCheckDropdown(), "Code", "Name");
            ViewBag.month = new SelectList(await _dropdownService.GetJVPostingMonthDropdown(0,0), "Code", "Name");
                Filter filter = new Filter();
                filter.length = 10;
                model = await _service.GetList(0, 0, 0, filter);
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
        [HttpGet]
        public async Task<JsonResult> GetMonth(int PayrollGrpCode,int Type)
        {
            var result = await _dropdownService.GetJVPostingMonthDropdown(Type,PayrollGrpCode);
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> GetList(Filter filter1, int Month, int PayrollGroup, int Type)
        {
            try
            {
                JVPosting model = new JVPosting();
               
                if (filter1.searchValue == "undefined" || filter1.searchValue == null)
                {
                    filter1.searchValue = "";
                }

                model = await _service.GetList(Month, PayrollGroup, Type, filter1);

                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model.JVPostingList == null || model.JVPostingList.Count() == 0)
                {


                    return Json(new
                    {
                        data = "",
                        draw = filter1.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows

                    });
                }

                if (model.JVPostingList.Count > 0)
                {
                    totalrows = model.JVPostingList[0].active;
                }


                if (filter1.searchValue != "")
                {
                    totalrowsafterfiltering = model.JVPostingList[0].totalRecords;

                    return Json(new
                    {
                        data = model.JVPostingList,
                        draw = filter1.draw,
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows

                    }); ;
                }
                else
                {

                    return Json(new
                    {
                        data = model.JVPostingList,
                        draw = filter1.draw,
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows

                    });
                }
            }
            catch (Exception ex)
            {
                return View();
            }
        }
    
        [HttpPost]
        public async Task<IActionResult> Save(int Month, int Payrollgroup, int Type)
        {
            try
            {
                JVPosting model = new JVPosting();
                model.radioSelection = Type;
                model.PayrollGroup = new Shared.Master.PayrollGroup();
                model.PayrollMonth = new Dropdown();
                model.PayrollMonth.Code = Month;
                model.PayrollGroup.Code = Payrollgroup;
                var response = await _service.Save(model);
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
       

        [HttpGet]
        public async Task<IActionResult> CheckJVLinking(int MonthCode)
        {
            try
            {
                var response = await _service.CheckJVLinking(MonthCode);

                if (response.Message == "ALERT")
                {
                    return PartialView("_CheckList", response.Data);
                }

                return Json(new { success = true });
            }
            catch (UnauthorizedAccessException)
            {
                return RedirectToAction("Login", "Account", new { Area = "" });
            }
            catch (Exception)
            {
                return RedirectToAction("Error", "Account", new { Area = "" });
            }
        }
    }
}
