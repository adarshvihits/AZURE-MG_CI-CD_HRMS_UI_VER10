using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.TDS.Reports;
using Hrms.Lite.Services.Services.TDS.Reports;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Hrms.Lite.Shared.TDS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hrms.Lite.UI.Areas.TDS.Reports.Controllers
{
    [Area("TDS")]
    [SessionAuthorize]
    public class FORM12BBController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly IFORM12BBService _FORM12BService;
        private readonly ICommonService _commonService;
        public class Global
        {
            public static Filter GlobalFilter;
            public static string url;
        }
        public FORM12BBController(IDropdownService dropdownService, IFORM12BBService FORM12BBService, ICommonService commonService)
        {
            _dropdownService = dropdownService;
            _FORM12BService = FORM12BBService;
            _commonService = commonService;
        }
        public async Task<IActionResult> Index()
        {
            Form12B model = new Form12B();
            var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
            model.Url = new Url();
            ViewBag.AssessmentYear = new SelectList(await _dropdownService.GetAssessmentYearDropdown(), "Code", "Name");
            var AssCode = await _commonService.GetCurrentAssessmentYear();
            model.AssessmentYear = new AssessmentYear();
            model.AssessmentYear.Code = AssCode.Data;
            model.CompanyCode = User.authUser.CompanyID;
            return View("~/Areas/TDS/Reports/Views/FORM12BB/Index.cshtml", model);
        }


        [HttpPost]
        public async Task<IActionResult> GetEmployeeList(Filter Filter, int AssessmentYear)
        {
            try
            {
               
              
                Form12B model = new Form12B();
                model.Url = new Url();
                Filter filter = new Filter();
                Global.GlobalFilter = Filter;
                model.Url = await _commonService.GetUrl();
                
                string url = model.Url.Urllink;
                int AssessmentYearCode = AssessmentYear;

                if (Filter.searchValue == "undefined" || Filter.searchValue == null)
                {
                    Filter.searchValue = "";
                }
                if (AssessmentYear != 0)
                {
                    model = await _FORM12BService.GetEmployeeList(AssessmentYear, Filter);
                }
               

                int totalrows = 0;
                int totalrowsafterfiltering = 0;

                if (model == null || model.EmployeeList == null || model.EmployeeList.Count() == 0)
                {


                    return Json(new
                    {
                        data = "",
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
                        AssessmentYearCode = AssessmentYearCode,
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
                        AssessmentYearCode= AssessmentYearCode,
                        draw = Filter.draw,//Request.Form["draw"], 
                        recordsTotal = totalrows,
                        recordsFiltered = totalrows
                    });;
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
