
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank.Reports;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.DataBank.Reports;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

using System.Threading.Tasks;

using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Data;

using Filter = Hrms.Lite.Shared.General.Filter;

using DataTable = System.Data.DataTable;
using Hrms.Lite.Shared.DataBank.DTO;

namespace Hrms.Lite.UI.Areas.DataBank.Reports.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]
    public class SkillMatrixEmployeeListController : Controller
    {
        private IDropdownService _dropdownService;
        private int i;
        private readonly ISkillMatrixService _employeeProfileService;
        private readonly IHelpers _helpers;

        private readonly ICommonService _commonService;
        private readonly IBlob _blob;
        public static class Global
        {
            public static Filter GlobalFilter;
            public static int ActiveCount;
            public static int ResignedCount;
        }

        public SkillMatrixEmployeeListController(ISkillMatrixService employeeProfileService, ICommonService commonService, IBlob blob, IDropdownService dropdownService, IHelpers Helpers)
        {
            _employeeProfileService = employeeProfileService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _blob = blob;
            _helpers = Helpers;
        }


        public async Task<IActionResult> Index(int skill,string rating)
        {
            try
            {

                SkillMatrix model = new SkillMatrix();
                await fillfilterDropdown();

                Filter filterdata = new Filter();
              

                return View("~/Areas/DataBank/Reports/Views/SkillMatrixEmployeeList/Index.cshtml", model);
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

        DefaultContractResolver contractResolver = new DefaultContractResolver
        {
            NamingStrategy = new CamelCaseNamingStrategy
            {
                OverrideSpecifiedNames = true
            }
        };
        public async Task<IActionResult> GetList([DataSourceRequest] DataSourceRequest request, Filter filter1, int skill,string rating)
        {

            SkillMatrix model = new SkillMatrix();
            List<SkillMatrixEmployee_List> modellist = new List<SkillMatrixEmployee_List>();

           model = await _employeeProfileService.GetSkillMatrixEmployeeDetails (filter1, skill, rating);
            modellist = model.SkillMatrixEmpList;

            var dataResult = modellist.ToDataSourceResult(request);

            return Json(dataResult, new JsonSerializerSettings { ContractResolver = new DefaultContractResolver() });



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
