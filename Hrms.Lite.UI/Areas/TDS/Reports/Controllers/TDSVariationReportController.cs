using ClosedXML.Excel;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.TDS;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TDS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Spire.Xls;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Services.IServices.TDS.Reports;
using Hrms.Lite.Shared.Master;

namespace Hrms.Lite.UI.Areas.TDS.Reports.Controllers
{
    [Area("TDS")]
    [SessionAuthorize]
    public class TDSVariationReportController : Controller
    {
        private readonly IDropdownService _dropdownService;
        private readonly ITDSVariationReportService _TDSVariationReportService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        public TDSVariationReportController(ITDSVariationReportService TDSVariationReportService, IDropdownService dropdownService, ICommonService commonService, IHelpers Helpers)
        {
            _TDSVariationReportService = TDSVariationReportService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = Helpers;

        }
        public static class Global
        {
            public static Shared.General.Filter GlobalFilter;
            public static string TabName;
            public static string RowName;
            public static int RowCode;
        }
        public async Task<IActionResult> Index()
        {
            TDSVariationReport model = new TDSVariationReport();
          
            var AssCode = await _commonService.GetCurrentAssessmentYear();
            int AssessmentYear = AssCode.Data;
            await fillfilterDropdown();
            ViewBag.AssessmentYear = new SelectList(await _dropdownService.GetAssessmentYearDropdown(), "Code", "Name");
            ViewBag.month = new SelectList(await _dropdownService.GetTDSConfirmationMonthDropdown(AssessmentYear), "Code", "Name");
            ViewBag.PayrollGroup = new SelectList(await _dropdownService.GetPayrollGroupDropdown(), "Code", "Name");
          
            return View("~/Areas/TDS/Reports/Views/TDSVariationReport/Index.cshtml", model);
        }
        [HttpGet]
        public async Task<JsonResult> GetMonthDropDown(int AssessmentYearCode)
        {
            SelectList data = new SelectList("", "Code", "Name");
            data = new SelectList(await _dropdownService.GetTDSConfirmationMonthDropdown(AssessmentYearCode), "Code", "Name");
            return Json(data);
        }
        [HttpPost]
        public async Task<IActionResult> Processing(TDSVariationReport Model)
        {
            try
            {
                var response = await _TDSVariationReportService.TdsVariationReport(Model);
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

        }
        public async Task<IActionResult> GetVariationReportrPDF(int AssessmentYear, int Location, int Department, int Designation, int Category,int Grade, int PayrollGroup)
        {
            Login CurrentUser = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));

            var CompanyGI = CurrentUser.authUser.User.CompanyGuid;
            var UserGuid = CurrentUser.authUser.User.UserGuid;
            var GroupCompanyCode = CurrentUser.GroupCompanyCode;
            TDSVariationReport model = new TDSVariationReport();
            model.Url = new Url();
            model.Url = await _commonService.GetUrl();
            string url = model.Url.Urllink;
            model.AssessmentYear = AssessmentYear;
            model.Location = Location;
            model.Department = Department;
            model.Designation = Designation;
            model.Category = Category;
            model.PayrollGroup = PayrollGroup;
            model.Grade = Grade;
            model.ReportUrl = url;
            model.CompanyGI = CompanyGI;
            model.UserGI = UserGuid;
            model.GroupCompanyCode = GroupCompanyCode;
            return View("~/Areas/TDS/Reports/Views/TDSVariationReport/VariationReport.cshtml", model);


        }


    }
}
