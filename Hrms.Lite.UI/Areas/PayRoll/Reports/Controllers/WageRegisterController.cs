using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.PayRoll.Reports;
using Hrms.Lite.Services.Services.PayRoll.Reports;

using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Hrms.Lite.Shared.PayRoll.Reports;
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
using Telerik.SvgIcons;

namespace Hrms.Lite.UI.Areas.PayRoll.Reports.Controllers
{
    [Area("PayRoll")]
    [SessionAuthorize]
    public class WageRegisterController : Controller
    {
       
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        private readonly IDropdownService _dropdownService;
        public static class Global
        {
            public static PaySheet data;
            public static System.Data.DataSet _dataset;


        }
        public WageRegisterController(ICommonService commonService, IHelpers Helpers, IDropdownService dropdownService)
        {
          
            _commonService = commonService;
            _helpers = Helpers;
            _dropdownService = dropdownService;
        }
        public async Task<IActionResult> Index()
        {
           
            try
            {
                await fillfilterDropdown();
                PaySheet model = new PaySheet();
                Global._dataset = null;

                return View("~/Areas/PayRoll/Reports/Views/WageRegister/Index.cshtml");
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

            ViewBag.Dsg = new SelectList(await _dropdownService.GetDesignationDropdown(), "Code", "Name");
            ViewBag.Dept = new SelectList(await _dropdownService.GetDepartmentDropdown(), "Code", "Name");
            ViewBag.Loc = new SelectList(await _dropdownService.GetLocationDropdown(), "Code", "Name");
            ViewBag.Cat = new SelectList(await _dropdownService.GetCategoryDropdown(), "Code", "Name");
            ViewBag.Grd = new SelectList(await _dropdownService.GetGradeDropdown(), "Code", "Name");
            ViewBag.PayrollGrp = new SelectList(await _dropdownService.GetPayrollGroupDropdown(), "Code", "Name");
            ViewBag.division = new SelectList(await _dropdownService.GetDivisionDropdown(), "Code", "Name");
            ViewBag.section = new SelectList(await _dropdownService.GetSectionDropdown(), "Code", "Name");
            ViewBag.EmploymentType = new SelectList(await _dropdownService.GetEmploymentTypeDropdown(), "Code", "Name");
            ViewBag.WorkLocation = new SelectList(await _dropdownService.GetWorkLocationDropdown(), "Code", "Name");
            ViewBag.WageGrade = new SelectList(await _dropdownService.GetWageGradeDropdown(), "Code", "Name");
            ViewBag.month = new SelectList(await _dropdownService.GetMonthDropdown(), "Code", "Name");
            ViewBag.WageType = new SelectList(await _dropdownService.GetWageTypeDropDown(), "Code", "Name");
            ViewBag.SalaryStructure = new SelectList(await _dropdownService.GeSalaryStructureDropDown(), "Code", "Name");
            ViewBag.PaymentMode = new SelectList(await _dropdownService.GetPaymentModeDropDown(), "Code", "Name");
            ViewBag.Employee = new SelectList(await _dropdownService.GetEmployeeDropDown(), "Code", "Name");
            ViewBag.AttendanceGroup = new SelectList(await _dropdownService.GetAttendanceGroupDropdown(), "Code", "Name");
        }
        public async Task<IActionResult> GetWageRegisterPdf(int EmployeeCode,int SalaryPeriod,string Month,int Designation, int Department, int Location, int Category, int Grade, int PayrollGroup, int Division, int Section, int EmploymentType, int WorkLocation, int WageType, int SalaryStructure, int PaymentMode, int AttendanceGroup)
        {
            WageRegister model = new WageRegister();
            model.PayrollMonth = new MasterBase();
            model.EmployeeCode = EmployeeCode;
            model.PayrollMonth.Code = SalaryPeriod;
            model.PayrollMonth.Name = Month;
            model.Url = await _commonService.GetUrl();
            model.Designation = Designation;
            model.Department = Department;
            model.Location = Location;
            model.Category = Category;
            model.Grade = Grade;
            model.PayrollGroup = PayrollGroup;
            model.Division = Division;
            model.Section = Section;
            model.EmploymentType = EmploymentType;
            model.WorkLocation = WorkLocation;
            model.WageType = WageType;
            model.SalaryStructure = SalaryStructure;
            model.PaymentMode = PaymentMode;
            model.AttendanceGroup = AttendanceGroup;

            Hrms.Lite.Shared.Account.Login CurrentUser = JsonConvert.DeserializeObject<Hrms.Lite.Shared.Account.Login>(HttpContext.Session.GetString("User"));
            model.UserId = CurrentUser.authUser.UserCode;
            model.CompanyId = CurrentUser.authUser.CompanyID;
            model.GroupCompanyId = CurrentUser.GroupCompanyCode;


            return View("~/Areas/PayRoll/Reports/Views/WageRegister/WageRegister.cshtml",model );
        }
       
    }
}
