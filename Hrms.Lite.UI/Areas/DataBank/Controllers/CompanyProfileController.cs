using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Spire.Xls;

namespace Hrms.Lite.UI.Areas.DataBank.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]
    public class CompanyProfileController : Controller
    {
        private IDropdownService _dropdownService;
        private readonly ICompanyProfileService _companyProfileService;
        private readonly IEmployeeProfileService _employeeProfileService;
        private readonly ICommonService _commonService;
        private readonly IBlob _blob;
        public string codeName = "";
        //public static class Global
        //{
        //    public static Filter GlobalFilter;
        //    public static int Code;
        //    public static string Name;
        //    public static string Type;


        //}
        //public static int GrpCode;
        //public static string GrpName;
        //public static string GrpType;
        private readonly IHelpers _helpers;
        public CompanyProfileController(ICompanyProfileService companyProfileService, IEmployeeProfileService employeeProfileService, ICommonService commonService, IBlob blob, IDropdownService dropdownService, IHelpers helper)
        {
            _companyProfileService = companyProfileService;
            _employeeProfileService = employeeProfileService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _blob = blob;
            _helpers = helper;
        }
        //[HttpPost]
        public async Task<IActionResult> Index(int GroupCompanyCode=0)
        {
            try { 
            CompanyProfile model = new CompanyProfile();
            Filter filter = new Filter();
            if (TempData["FilterModel"] is string jsonData)
            {
                filter = JsonConvert.DeserializeObject<Filter>(jsonData);                
            }
            model = await _companyProfileService.GetCompanyProfile(filter, GroupCompanyCode);
            model.GrpCmpCode = GroupCompanyCode;
            model.Filter = await _commonService.GetFilterDropdownsAndBindData(filter);
            ViewBag.GrpCmpny = new SelectList(await _dropdownService.GetCompanyProfileGroupCompanyDropDown(), "Code", "Name");
            int DesignationCount = 0;
            int Department = 0;
            int RegionCount = 0;
            int LocationCount = 0;
            int CategoryCount = 0;
            int GradeCount = 0;
            int PayRollCount = 0;
            int SalaryBandCount = 0;
            int AgeCount = 0;
            int GenderCount = 0;
            int ServiceCount = 0;
            int QualificationCount = 0;
            int GroupCompanyCount = 0;
            int SectionCount = 0;


                
            int DesignationGP = 0;
            int DepartmentGP = 0;
            int RegionGP = 0;
            int LocationGP = 0;
            int CategoryGP = 0;
            int GradeGP = 0;
            int PayRollGP = 0;
            int SalaryBandGP = 0;
            int AgeGP = 0;
            int GenderGP = 0;
            int ServiceGP = 0;
            int QualificationGP = 0;
            int DivisionGP = 0;
            int DivisionCount = 0;
            int GroupCompanyGP = 0;
            int SectionGP = 0;


            for (int i = 0; i < model.DesignationDetails.Count; i++)
            {

                DesignationCount = DesignationCount + model.DesignationDetails[i].Count;
                ViewBag.DesignationCount = DesignationCount;

                DesignationGP = DesignationGP + model.DesignationDetails[i].GP;
                ViewBag.DesignationGP = DesignationGP;

            }
            for (int i = 0; i < model.DepartmentDetails.Count; i++)
            {


                Department = Department + model.DepartmentDetails[i].Count;
                ViewBag.DepartmentCount = Department;

                DepartmentGP = DepartmentGP + model.DepartmentDetails[i].GP;
                ViewBag.DepartmentGP = DepartmentGP;
            }
            for (int i = 0; i < model.RegionDetails.Count; i++)
            {


                RegionCount = RegionCount + model.RegionDetails[i].Count;
                ViewBag.RegionCount = RegionCount;

                RegionGP = RegionGP + model.RegionDetails[i].GP;
                ViewBag.RegionGP = RegionGP;
            }
            for (int i = 0; i < model.LocationDetails.Count; i++)
            {


                LocationCount = LocationCount + model.LocationDetails[i].Count;
                ViewBag.LocationCount = LocationCount;
                LocationGP = LocationGP + model.LocationDetails[i].GP;
                ViewBag.LocationGP = LocationGP;
            }
            for (int i = 0; i < model.CategoryDetails.Count; i++)
            {

                CategoryCount = CategoryCount + model.CategoryDetails[i].Count;
                ViewBag.CategoryCount = CategoryCount;

                CategoryGP = CategoryGP + model.CategoryDetails[i].GP;
                ViewBag.CategoryGP = CategoryGP;

            }
            for (int i = 0; i < model.GradeDetails.Count; i++)
            {


                GradeCount = GradeCount + model.GradeDetails[i].Count;
                ViewBag.GradeCount = GradeCount;
                GradeGP = GradeGP + model.GradeDetails[i].GP;
                ViewBag.GradeGP = GradeGP;
            }
            for (int i = 0; i < model.PayrollGroupDetails.Count; i++)
            {


                PayRollCount = PayRollCount + model.PayrollGroupDetails[i].Count;
                ViewBag.PayRollCount = PayRollCount;
                PayRollGP = PayRollGP + model.PayrollGroupDetails[i].GP;
                ViewBag.PayRollGP = PayRollGP;
            }
            for (int i = 0; i < model.SalaryBandDetails.Count; i++)
            {


                SalaryBandCount = SalaryBandCount + model.SalaryBandDetails[i].Count;
                ViewBag.SalaryBandCount = SalaryBandCount;

                SalaryBandGP = SalaryBandGP + model.SalaryBandDetails[i].GP;
                ViewBag.SalaryBandGP = SalaryBandGP;

            }
            for (int i = 0; i < model.AgeBandDetails.Count; i++)
            {


                AgeCount = AgeCount + model.AgeBandDetails[i].Count;
                ViewBag.AgeCount = AgeCount;
                AgeGP = AgeGP + model.AgeBandDetails[i].GP;
                ViewBag.AgeGP = AgeGP;
            }
            for (int i = 0; i < model.GenderDetails.Count; i++)
            {


                GenderCount = GenderCount + model.GenderDetails[i].Count;
                ViewBag.GenderCount = GenderCount;

                GenderGP = GenderGP + model.GenderDetails[i].GP;
                ViewBag.GenderGP = GenderGP;

            }
            for (int i = 0; i < model.ServiceBandDetails.Count; i++)
            {

                ServiceCount = ServiceCount + model.ServiceBandDetails[i].Count;
                ViewBag.ServiceCount = ServiceCount;

                ServiceGP = ServiceGP + model.ServiceBandDetails[i].GP;
                ViewBag.ServiceGP = ServiceGP;

            }
            for (int i = 0; i < model.QualificationDetails.Count; i++)
            {

                QualificationCount = QualificationCount + model.QualificationDetails[i].Count;

                ViewBag.QualificationCount = QualificationCount;

                QualificationGP = QualificationGP + model.QualificationDetails[i].GP;

                ViewBag.QualificationGP = QualificationGP;

            }
                for (int i = 0; i < model.DivisionDetails.Count; i++)
                {

                    DivisionCount = DivisionCount + model.DivisionDetails[i].Count;

                    ViewBag.DivisionCount = DivisionCount;

                    DivisionGP = DivisionGP + model.DivisionDetails[i].GP;

                    ViewBag.DivisionGP = DivisionGP;

                }
                for (int i = 0; i < model.GroupCompanyDetails.Count; i++)
                {

                    GroupCompanyCount = GroupCompanyCount + model.GroupCompanyDetails[i].Count;
                    ViewBag.GroupCompanyCount = GroupCompanyCount;

                    GroupCompanyGP = GroupCompanyGP + model.GroupCompanyDetails[i].GP;
                    ViewBag.GroupCompanyGP = GroupCompanyGP;

                }
                for (int i = 0; i < model.SectionDetails.Count; i++)
                {

                    SectionCount = SectionCount + model.SectionDetails[i].Count;
                    ViewBag.SectionCount = SectionCount;

                    SectionGP = SectionGP + model.SectionDetails[i].GP;
                    ViewBag.SectionGP = SectionGP;

                }


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

        public async Task<IActionResult> GetCompanyProfile(Filter filter, int GroupCompanyCode)
        {
            try
            {
                TempData["FilterModel"] = Newtonsoft.Json.JsonConvert.SerializeObject(filter);
                CompanyProfile model = new CompanyProfile();               
                model = await _companyProfileService.GetCompanyProfile(filter, GroupCompanyCode);
                model.GrpCmpCode = GroupCompanyCode;
                int DesignationCount = 0;
                int Department = 0;
                int RegionCount = 0;
                int LocationCount = 0;
                int CategoryCount = 0;
                int GradeCount = 0;
                int PayRollCount = 0;
                int SalaryBandCount = 0;
                int AgeCount = 0;
                int GenderCount = 0;
                int ServiceCount = 0;
                int QualificationCount = 0;
                int GroupCompanyCount = 0;
                int SectionCount = 0;



                int DesignationGP = 0;
                int DepartmentGP = 0;
                int RegionGP = 0;
                int LocationGP = 0;
                int CategoryGP = 0;
                int GradeGP = 0;
                int PayRollGP = 0;
                int SalaryBandGP = 0;
                int AgeGP = 0;
                int GenderGP = 0;
                int ServiceGP = 0;
                int QualificationGP = 0;
                int DivisionGP = 0;
                int DivisionCount = 0;
                int GroupCompanyGP = 0;
                int SectionGP = 0;


                for (int i = 0; i < model.DesignationDetails.Count; i++)
                {

                    DesignationCount = DesignationCount + model.DesignationDetails[i].Count;
                    ViewBag.DesignationCount = DesignationCount;

                    DesignationGP = DesignationGP + model.DesignationDetails[i].GP;
                    ViewBag.DesignationGP = DesignationGP;

                }
                for (int i = 0; i < model.DepartmentDetails.Count; i++)
                {


                    Department = Department + model.DepartmentDetails[i].Count;
                    ViewBag.DepartmentCount = Department;

                    DepartmentGP = DepartmentGP + model.DepartmentDetails[i].GP;
                    ViewBag.DepartmentGP = DepartmentGP;
                }
                for (int i = 0; i < model.RegionDetails.Count; i++)
                {


                    RegionCount = RegionCount + model.RegionDetails[i].Count;
                    ViewBag.RegionCount = RegionCount;

                    RegionGP = RegionGP + model.RegionDetails[i].GP;
                    ViewBag.RegionGP = RegionGP;
                }
                for (int i = 0; i < model.LocationDetails.Count; i++)
                {


                    LocationCount = LocationCount + model.LocationDetails[i].Count;
                    ViewBag.LocationCount = LocationCount;
                    LocationGP = LocationGP + model.LocationDetails[i].GP;
                    ViewBag.LocationGP = LocationGP;
                }
                for (int i = 0; i < model.CategoryDetails.Count; i++)
                {

                    CategoryCount = CategoryCount + model.CategoryDetails[i].Count;
                    ViewBag.CategoryCount = CategoryCount;

                    CategoryGP = CategoryGP + model.CategoryDetails[i].GP;
                    ViewBag.CategoryGP = CategoryGP;

                }
                for (int i = 0; i < model.GradeDetails.Count; i++)
                {


                    GradeCount = GradeCount + model.GradeDetails[i].Count;
                    ViewBag.GradeCount = GradeCount;
                    GradeGP = GradeGP + model.GradeDetails[i].GP;
                    ViewBag.GradeGP = GradeGP;
                }
                for (int i = 0; i < model.PayrollGroupDetails.Count; i++)
                {


                    PayRollCount = PayRollCount + model.PayrollGroupDetails[i].Count;
                    ViewBag.PayRollCount = PayRollCount;
                    PayRollGP = PayRollGP + model.PayrollGroupDetails[i].GP;
                    ViewBag.PayRollGP = PayRollGP;
                }
                for (int i = 0; i < model.SalaryBandDetails.Count; i++)
                {


                    SalaryBandCount = SalaryBandCount + model.SalaryBandDetails[i].Count;
                    ViewBag.SalaryBandCount = SalaryBandCount;

                    SalaryBandGP = SalaryBandGP + model.SalaryBandDetails[i].GP;
                    ViewBag.SalaryBandGP = SalaryBandGP;

                }
                for (int i = 0; i < model.AgeBandDetails.Count; i++)
                {


                    AgeCount = AgeCount + model.AgeBandDetails[i].Count;
                    ViewBag.AgeCount = AgeCount;
                    AgeGP = AgeGP + model.AgeBandDetails[i].GP;
                    ViewBag.AgeGP = AgeGP;
                }
                for (int i = 0; i < model.GenderDetails.Count; i++)
                {


                    GenderCount = GenderCount + model.GenderDetails[i].Count;
                    ViewBag.GenderCount = GenderCount;

                    GenderGP = GenderGP + model.GenderDetails[i].GP;
                    ViewBag.GenderGP = GenderGP;

                }
                for (int i = 0; i < model.ServiceBandDetails.Count; i++)
                {

                    ServiceCount = ServiceCount + model.ServiceBandDetails[i].Count;
                    ViewBag.ServiceCount = ServiceCount;

                    ServiceGP = ServiceGP + model.ServiceBandDetails[i].GP;
                    ViewBag.ServiceGP = ServiceGP;

                }
                for (int i = 0; i < model.QualificationDetails.Count; i++)
                {

                    QualificationCount = QualificationCount + model.QualificationDetails[i].Count;

                    ViewBag.QualificationCount = QualificationCount;

                    QualificationGP = QualificationGP + model.QualificationDetails[i].GP;

                    ViewBag.QualificationGP = QualificationGP;

                }
                for (int i = 0; i < model.DivisionDetails.Count; i++)
                {

                    DivisionCount = DivisionCount + model.DivisionDetails[i].Count;

                    ViewBag.DivisionCount = DivisionCount;

                    DivisionGP = DivisionGP + model.DivisionDetails[i].GP;

                    ViewBag.DivisionGP = DivisionGP;

                }
                for (int i = 0; i < model.GroupCompanyDetails.Count; i++)
                {

                    GroupCompanyCount = GroupCompanyCount + model.GroupCompanyDetails[i].Count;
                    ViewBag.GroupCompanyCount = GroupCompanyCount;

                    GroupCompanyGP = GroupCompanyGP + model.GroupCompanyDetails[i].GP;
                    ViewBag.GroupCompanyGP = GroupCompanyGP;

                }
                for (int i = 0; i < model.SectionDetails.Count; i++)
                {

                    SectionCount = SectionCount + model.SectionDetails[i].Count;
                    ViewBag.SectionCount = SectionCount;

                    SectionGP = SectionGP + model.SectionDetails[i].GP;
                    ViewBag.SectionGP = SectionGP;

                }


                return View("_CompanyProfile",model);

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
       
        public async Task<IActionResult> ApplyFilter(Filter filter,int GroupCompanyCode, int CODE, string TYPE)
        {
            try
            {
                CompanyProfile model = new CompanyProfile();
                
                model = await _companyProfileService.GetCompanyProfileDetails(CODE, TYPE, filter, GroupCompanyCode);
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
        public async Task<IActionResult> GetCompanyProfileDetails(int CODE, string NAME, string TYPE,int GroupCompanyCode)
        {
            try {
               
            CompanyProfile model = new CompanyProfile();
                
            ViewBag.Code = CODE;
            ViewBag.Type = TYPE;
            codeName = NAME;
            ViewBag.CodeName = NAME;
                Filter filter = new Filter();
                if (TempData["FilterModel"] is string jsonData)
                {
                     filter = JsonConvert.DeserializeObject<Filter>(jsonData);
                    TempData.Keep("FilterModel");
                }              
                model = await _companyProfileService.GetCompanyProfileDetails(CODE, TYPE,filter, GroupCompanyCode);
                model.GrpCmpCode = GroupCompanyCode;
                model.Filter = await _commonService.GetFilterDropdownsAndBindData(filter);
                return View("~/Areas/DataBank/Views/CompanyProfile/CompanyProfileList.cshtml", model);
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
        //public async Task<IActionResult> CompanyProfileList()
        //{
        //    try
        //    {
        //        CompanyProfile model = new CompanyProfile();
        //        Filter filter = new Filter();
        //        ViewBag.Code = Global.Code;
        //        ViewBag.Type = Global.Type;
        //        codeName = Global.Name;
        //        ViewBag.CodeName = Global.Name;
        //        GrpCode = Global.Code;
        //        GrpName = Global.Name;
        //        GrpType = Global.Type ;
        //        model = await _companyProfileService.GetCompanyProfileDetails(Global.Code, Global.Type, filter,0);
        //        for (int i = 0; i < model.CompanyProfileDetailedView.Count; i++)
        //        {
        //            DateTime DOJ = Convert.ToDateTime(model.CompanyProfileDetailedView[i].DOJ);
        //            string DateinFormat = DOJ.ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture);
        //            model.CompanyProfileDetailedView[i].DOJ = DateinFormat;
        //        }
        //        model.Filter = await _commonService.GetFilterDropdowns();
        //        return View("~/Areas/DataBank/Views/CompanyProfile/CompanyProfileList.cshtml", model);
        //    }
        //    catch (UnauthorizedAccessException e)
        //    {

        //        return RedirectToAction("Login", "Account", new { Area = "" });
        //    }
        //    catch (Exception e)
        //    {
        //        return RedirectToAction("Error", "Account", new { Area = "" });
        //    }
        //}
        public async Task<IActionResult> GetEmployeeProfileDetails(Guid EmployeeGI, int FinancialYear, string Breadcrumb, string BreadcrumbName)
        {
            try
            {
                EmployeeProfile model = new EmployeeProfile();

                if (FinancialYear == 0)
                {
                    var CurrentFinancialYearCode = 0;

                    if (CurrentFinancialYearCode == 0)
                    {
                        var response = await _commonService.GetCurrentLFinancialYear();

                        CurrentFinancialYearCode = Convert.ToInt32(response.Data);
                    }
                    model.FinancialYear = new FinancialYear();
                    model.FinancialYear.Code = CurrentFinancialYearCode;
                    FinancialYear = CurrentFinancialYearCode;
                    model = await _employeeProfileService.GetEmployeeProfileDetails(EmployeeGI, FinancialYear);
                }
                else
                {
                    model.FinancialYear.Code = FinancialYear;
                    model = await _employeeProfileService.GetEmployeeProfileDetails(EmployeeGI, FinancialYear);
                }
                var EmpNameWithoutId = model.EmployeeHeader.EmployeeName.Split('(')[0].Trim();
                ViewBag.FinancialYear = new SelectList(await _dropdownService.GetFinancialYearDropdown(), "Code", "Name");

                model.EmployeeHeader.EmployeeGuid = EmployeeGI;
                model.EmployeeHeader.EmployeeName = EmpNameWithoutId;
                model.DocumentUpload.AadharIssueDate = model.DocumentUpload.AadharIssueDate == DateTime.Parse("0001-01-01") ? null : model.DocumentUpload.AadharIssueDate;
                model.DocumentUpload.DrivingLicenseValidFrom = model.DocumentUpload.DrivingLicenseValidFrom == DateTime.Parse("0001-01-01") ? null : model.DocumentUpload.DrivingLicenseValidFrom;
                model.DocumentUpload.DrivingLicenseValidTo = model.DocumentUpload.DrivingLicenseValidTo == DateTime.Parse("0001-01-01") ? null : model.DocumentUpload.DrivingLicenseValidTo;
                model.DocumentUpload.PANIssueDate = model.DocumentUpload.PANIssueDate == DateTime.Parse("0001-01-01") ? null : model.DocumentUpload.PANIssueDate;
                model.DocumentUpload.PassportValidFrom = model.DocumentUpload.PassportValidFrom == DateTime.Parse("0001-01-01") ? null : model.DocumentUpload.PassportValidFrom;
                model.DocumentUpload.PassportValidTo = model.DocumentUpload.PassportValidTo == DateTime.Parse("0001-01-01") ? null : model.DocumentUpload.PassportValidTo;
                model.DocumentUpload.PANIssueDate = model.DocumentUpload.PANIssueDate == DateTime.Parse("0001-01-01") ? null : model.DocumentUpload.PANIssueDate;
                model.Statutory_Payment_Details.ESIEnrollDate = model.Statutory_Payment_Details.ESIEnrollDate == DateTime.Parse("0001-01-01") ? null : model.Statutory_Payment_Details.ESIEnrollDate;
                model.Statutory_Payment_Details.PFEnrollDate = model.Statutory_Payment_Details.PFEnrollDate == DateTime.Parse("0001-01-01") ? null : model.Statutory_Payment_Details.PFEnrollDate;
                //  model.Job_Details.RenewalDate = model.Job_Details.RenewalDate == DateTime.Parse("0001-01-01") ? null : model.Job_Details.RenewalDate;

                for (int i = 0; i < model.AssetRequest.Count; i++)
                {
                    model.AssetRequest[i].AssetReturnDate = model.AssetRequest[i].AssetReturnDate == DateTime.Parse("0001-01-01") ? null : model.AssetRequest[i].AssetReturnDate;
                    model.AssetRequest[i].AssetIssueDate = model.AssetRequest[i].AssetIssueDate == DateTime.Parse("0001-01-01") ? null : model.AssetRequest[i].AssetIssueDate;
                    model.AssetRequest[i].AssetDueDate = model.AssetRequest[i].AssetDueDate == DateTime.Parse("0001-01-01") ? null : model.AssetRequest[i].AssetDueDate;
                    model.AssetRequest[i].Asset.DateOfPurchase = model.AssetRequest[i].Asset.DateOfPurchase == DateTime.Parse("0001-01-01") ? null : model.AssetRequest[i].Asset.DateOfPurchase;


                }
                for (int i = 0; i < model.TrainingLog.Count; i++)
                {
                    model.TrainingLog[i].TrainingRequest.Duration = decimal.Round(model.TrainingLog[i].TrainingRequest.Duration, 0, MidpointRounding.AwayFromZero);
                }
                for (int i = 0; i < model.AppraisalLog.Count; i++)
                {
                    model.AppraisalLog[i].AppraisalRequest.AppraiserScore = (model.AppraisalLog[i].AppraisalRequest.AppraiserScore).Replace(".0", "");
                    model.AppraisalLog[i].AppraisalRequest.ReviewerScore = (model.AppraisalLog[i].AppraisalRequest.ReviewerScore).Replace(".0", "");
                    model.AppraisalLog[i].AppraisalRequest.SelfScore = (model.AppraisalLog[i].AppraisalRequest.SelfScore).Replace(".0", "");


                }
                for (int i = 0; i < model.FamilyDetails.Count; i++)
                {
                    model.FamilyDetails[i].DateOfBirth = model.FamilyDetails[i].DateOfBirth == DateTime.Parse("0001-01-01") ? null : model.FamilyDetails[i].DateOfBirth;

                }
                model.Compensation.EffectFrom = model.Compensation.EffectFrom == DateTime.Parse("0001-01-01") ? null : model.Compensation.EffectFrom;

                model.Breadcrumb = Breadcrumb;
                model.BreadcrumbName = BreadcrumbName;
                model.CompensationStructureReport = new CompensationStructureReport();

                model.CompensationStructureReport = await _employeeProfileService.GetCompensationDetails(EmployeeGI);



                return View("~/Areas/DataBank/Views/EmployeeProfile/EmployeeProfile.cshtml", model);
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
        public async Task<IActionResult> ExportToExcel(Filter filter,int GroupCompanyCode,string GroupCompanyName)
        {
            try
            {
                CompanyProfile model = new CompanyProfile();
                //model = await _companyProfileService.GetCompanyProfileDetails(CODE, TYPE);
                string FilterValues = await _helpers.GetFilterValues(filter);                
                if (FilterValues == "")
                {
                    FilterValues = "All";
                }
                model = await _companyProfileService.GetCompanyProfile(filter,GroupCompanyCode);

                int slno = 0;
                string currDateTime = await _helpers.GetCurrentDate();
                
                string LoginUser = HttpContext.Session.GetString("LoginUser");
                var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                //var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Company Profile", "Excel");
                if (User.authUser.SalaryVisibility != 0)
                {

                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("Company Profile");

                        int DesCount = model.DesignationDetails.Count;
                        int DeptCount = model.DepartmentDetails.Count;
                        int RegCount = model.RegionDetails.Count;
                        int LocCount = model.LocationDetails.Count;
                        int CatCount = model.CategoryDetails.Count;
                        int GradeCount = model.GradeDetails.Count;
                        int PayrollCount = model.PayrollGroupDetails.Count;
                        int DivisionCount = model.DivisionDetails.Count;
                        int salCount = model.SalaryBandDetails.Count;
                        int AgeCount = model.AgeBandDetails.Count;
                        int GenderCount = model.GenderDetails.Count;
                        int serviceCount = model.ServiceBandDetails.Count;
                        int QualCount = model.QualificationDetails.Count;
                        int GrpCmpnyCount = model.GroupCompanyDetails.Count;
                        int SecCount = model.SectionDetails.Count;

                        int[] numbers = { DesCount, DeptCount, RegCount, LocCount, CatCount, GradeCount, PayrollCount, DivisionCount, salCount, AgeCount, GenderCount, serviceCount, QualCount, GrpCmpnyCount, SecCount };
                        int biggestNumber = numbers.Max();


                        int PendingCountnum = biggestNumber + 5;


                        worksheet.Range("A1:BG" + PendingCountnum).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:BG" + PendingCountnum).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:BG" + PendingCountnum).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:BG" + PendingCountnum).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:BG" + PendingCountnum).Style.Border.LeftBorder = XLBorderStyleValues.Thin;

                        //var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(90)));
                        var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, "BG"));

                        wsReportNameHeaderRange.Style.Font.Bold = true;
                        wsReportNameHeaderRange.Style.Font.FontSize = 15;
                        wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        wsReportNameHeaderRange.Merge();
                        wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                        wsReportNameHeaderRange.Style.Font.FontSize = 15;
                        wsReportNameHeaderRange.Value = "Company Profile";
                        worksheet.Range("A5:BG5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                        worksheet.Cell(2, 1).Style.Font.Bold = true;
                        worksheet.Cell(2, 1).Value = "Group Company Name";
                        worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                        worksheet.Cell(2, 4).Style.Font.Bold = true;
                        worksheet.Cell(2, 4).Value = "Login User";
                        worksheet.Cell(2, 5).Value = LoginUser;
                        worksheet.Cell(2, 7).Style.Font.Bold = true;
                        worksheet.Cell(2, 7).Value = "Date & Time";
                        worksheet.Cell(2, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(2, 8).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
                        worksheet.Cell(2, 8).Value = currDateTime.ToString();


                        int DesignationCount = 0;
                        int DesignationGP = 0;
                        for (int i = 0; i < model.DesignationDetails.Count; i++)
                        {

                            DesignationCount = DesignationCount + model.DesignationDetails[i].Count;
                            DesignationGP = DesignationGP + model.DesignationDetails[i].GP;


                        }



                        worksheet.Cell(3, 1).Style.Font.Bold = true;
                        worksheet.Cell(3, 1).Value = "Total Emp Count";
                        worksheet.Cell(3, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(3, 2).Value = DesignationCount;
                        worksheet.Cell(3, 4).Style.Font.Bold = true;
                        worksheet.Cell(3, 4).Value = "Total Gross Pay";
                        worksheet.Cell(3, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
                        worksheet.Cell(3, 5).Value = DesignationGP;
                        worksheet.Cell(4, 1).Style.Font.Bold = true;
                        worksheet.Cell(4, 1).Value = "Filter Applied";
                        worksheet.Range("B4:BG4").Merge();
                        worksheet.Cell("B4").Value = FilterValues;







                        var currentRow = 5;

                        #region Header
                        worksheet.Cell(currentRow, 1).Value = "Designation";
                        worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 2).Value = "Count";
                        worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 3).Value = "GP";
                        worksheet.Cell(currentRow, 3).Style.Font.Bold = true;



                        worksheet.Cell(currentRow, 5).Value = "Department";
                        worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 6).Value = "Count";
                        worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 7).Value = "GP";
                        worksheet.Cell(currentRow, 7).Style.Font.Bold = true;


                        worksheet.Cell(currentRow, 9).Value = "Region";
                        worksheet.Cell(currentRow, 9).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 10).Value = "Count";
                        worksheet.Cell(currentRow, 10).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 11).Value = "GP";
                        worksheet.Cell(currentRow, 11).Style.Font.Bold = true;

                        worksheet.Cell(currentRow, 13).Value = "Location";
                        worksheet.Cell(currentRow, 13).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 14).Value = "Count";
                        worksheet.Cell(currentRow, 14).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 15).Value = "GP";
                        worksheet.Cell(currentRow, 15).Style.Font.Bold = true;

                        worksheet.Cell(currentRow, 17).Value = "Category";
                        worksheet.Cell(currentRow, 17).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 18).Value = "Count";
                        worksheet.Cell(currentRow, 18).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 19).Value = "GP";
                        worksheet.Cell(currentRow, 19).Style.Font.Bold = true;

                        worksheet.Cell(currentRow, 21).Value = "Grade";
                        worksheet.Cell(currentRow, 21).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 22).Value = "Count";
                        worksheet.Cell(currentRow, 22).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 23).Value = "GP";
                        worksheet.Cell(currentRow, 23).Style.Font.Bold = true;

                        worksheet.Cell(currentRow, 25).Value = "PayRoll Group";
                        worksheet.Cell(currentRow, 25).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 26).Value = "Count";
                        worksheet.Cell(currentRow, 26).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 27).Value = "GP";
                        worksheet.Cell(currentRow, 27).Style.Font.Bold = true;

                        worksheet.Cell(currentRow, 29).Value = "Division";
                        worksheet.Cell(currentRow, 29).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 30).Value = "Count";
                        worksheet.Cell(currentRow, 30).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 31).Value = "GP";
                        worksheet.Cell(currentRow, 31).Style.Font.Bold = true;

                        worksheet.Cell(currentRow, 33).Value = "Salary Band";
                        worksheet.Cell(currentRow, 33).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 34).Value = "Count";
                        worksheet.Cell(currentRow, 34).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 35).Value = "GP";
                        worksheet.Cell(currentRow, 35).Style.Font.Bold = true;


                        worksheet.Cell(currentRow, 37).Value = "Age Band";
                        worksheet.Cell(currentRow, 37).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 38).Value = "Count";
                        worksheet.Cell(currentRow, 38).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 39).Value = "GP";
                        worksheet.Cell(currentRow, 39).Style.Font.Bold = true;


                        worksheet.Cell(currentRow, 41).Value = "Gender";
                        worksheet.Cell(currentRow, 41).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 42).Value = "Count";
                        worksheet.Cell(currentRow, 42).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 43).Value = "GP";
                        worksheet.Cell(currentRow, 43).Style.Font.Bold = true;


                        worksheet.Cell(currentRow, 45).Value = "Service Band";
                        worksheet.Cell(currentRow, 45).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 46).Value = "Count";
                        worksheet.Cell(currentRow, 46).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 47).Value = "GP";
                        worksheet.Cell(currentRow, 47).Style.Font.Bold = true;

                        worksheet.Cell(currentRow, 49).Value = "Qualification";
                        worksheet.Cell(currentRow, 49).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 50).Value = "Count";
                        worksheet.Cell(currentRow, 50).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 51).Value = "GP";
                        worksheet.Cell(currentRow, 51).Style.Font.Bold = true;

                        worksheet.Cell(currentRow, 53).Value = "Group Company";
                        worksheet.Cell(currentRow, 53).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 54).Value = "Count";
                        worksheet.Cell(currentRow, 54).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 55).Value = "GP";
                        worksheet.Cell(currentRow, 55).Style.Font.Bold = true;

                        worksheet.Cell(currentRow, 57).Value = "Section";
                        worksheet.Cell(currentRow, 57).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 58).Value = "Count";
                        worksheet.Cell(currentRow, 58).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 59).Value = "GP";
                        worksheet.Cell(currentRow, 59).Style.Font.Bold = true;
                        #endregion

                        #region Body


                        foreach (var item in model.DesignationDetails)
                        {


                            currentRow++;


                            worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 1).Value = item.Name;
                            worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 2).Value = item.Count;
                            worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 3).Value = item.GP;



                            int DesignanCount = 0;
                            int DesnGP = 0;
                            for (int i = 0; i < model.DesignationDetails.Count; i++)
                            {

                                DesignanCount = DesignanCount + model.DesignationDetails[i].Count;

                                DesnGP = DesnGP + model.DesignationDetails[i].GP;

                            }




                            int c = DesCount + 6;
                            worksheet.Cell("A" + c).Style.Font.Bold = true;
                            worksheet.Cell("A" + c).Value = "Total";

                            worksheet.Cell("B" + c).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell("B" + c).Style.Font.Bold = true;
                            worksheet.Cell("B" + c).Value = DesignanCount;

                            worksheet.Cell("C" + c).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell("C" + c).Style.Font.Bold = true;
                            worksheet.Cell("C" + c).Value = DesnGP;

                        }




                        currentRow = 5;
                        foreach (var item in model.DepartmentDetails)
                        {

                            currentRow++;
                            worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 5).Value = item.Name;
                            worksheet.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 6).Value = item.Count;
                            worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 7).Value = item.GP;


                            int depCount = 0;
                            int DepGP = 0;
                            for (int i = 0; i < model.DepartmentDetails.Count; i++)
                            {

                                depCount = depCount + model.DepartmentDetails[i].Count;

                                DepGP = DepGP + model.DepartmentDetails[i].GP;

                            }




                            int d = DeptCount + 6;
                            worksheet.Cell("E" + d).Style.Font.Bold = true;
                            worksheet.Cell("E" + d).Value = "Total";
                            worksheet.Cell("E" + d).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);

                            worksheet.Cell("F" + d).Value = depCount;
                            worksheet.Cell("F" + d).Style.Font.Bold = true;
                            worksheet.Cell("F" + d).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);

                            worksheet.Cell("G" + d).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell("G" + d).Value = DepGP;
                            worksheet.Cell("G" + d).Style.Font.Bold = true;



                        }
                        currentRow = 5;
                        foreach (var item in model.RegionDetails)
                        {

                            currentRow++;
                            worksheet.Cell(currentRow, 9).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 9).Value = item.Name;
                            worksheet.Cell(currentRow, 10).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 10).Value = item.Count;
                            worksheet.Cell(currentRow, 11).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 11).Value = item.GP;







                            int RegICount = 0;
                            int RegGP = 0;
                            for (int i = 0; i < model.RegionDetails.Count; i++)
                            {

                                RegICount = RegICount + model.RegionDetails[i].Count;

                                RegGP = RegGP + model.RegionDetails[i].GP;

                            }




                            int e = RegCount + 6;
                            worksheet.Cell("I" + e).Style.Font.Bold = true;
                            worksheet.Cell("I" + e).Value = "Total";

                            worksheet.Cell("J" + e).Style.Font.Bold = true;
                            worksheet.Cell("J" + e).Value = RegICount;
                            worksheet.Cell("J" + e).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);

                            worksheet.Cell("K" + e).Style.Font.Bold = true;
                            worksheet.Cell("K" + e).Value = RegGP;
                            worksheet.Cell("K" + e).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);

                        }
                        currentRow = 5;
                        foreach (var item in model.LocationDetails)
                        {

                            currentRow++;
                            worksheet.Cell(currentRow, 13).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 13).Value = item.Name;
                            worksheet.Cell(currentRow, 14).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 14).Value = item.Count;
                            worksheet.Cell(currentRow, 15).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 15).Value = item.GP;

                            int lOC = 0;
                            int lOCGP = 0;
                            for (int i = 0; i < model.LocationDetails.Count; i++)
                            {

                                lOC = lOC + model.LocationDetails[i].Count;

                                lOCGP = lOCGP + model.LocationDetails[i].GP;

                            }




                            int F = LocCount + 6;
                            worksheet.Cell("M" + F).Style.Font.Bold = true;
                            worksheet.Cell("M" + F).Value = "Total";

                            worksheet.Cell("N" + F).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell("N" + F).Style.Font.Bold = true;
                            worksheet.Cell("N" + F).Value = lOC;

                            worksheet.Cell("o" + F).Style.Font.Bold = true;
                            worksheet.Cell("o" + F).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell("O" + F).Value = lOCGP;


                        }
                        currentRow = 5;
                        foreach (var item in model.CategoryDetails)
                        {

                            currentRow++;
                            worksheet.Cell(currentRow, 17).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 17).Value = item.Name;
                            worksheet.Cell(currentRow, 18).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 18).Value = item.Count;
                            worksheet.Cell(currentRow, 19).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 19).Value = item.GP;


                            int CAT = 0;
                            int CATGP = 0;
                            for (int i = 0; i < model.CategoryDetails.Count; i++)
                            {

                                CAT = CAT + model.CategoryDetails[i].Count;

                                CATGP = CATGP + model.CategoryDetails[i].GP;

                            }




                            int G = CatCount + 6;
                            worksheet.Cell("Q" + G).Value = "Total";
                            worksheet.Cell("Q" + G).Style.Font.Bold = true;
                            worksheet.Cell("Q" + G).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell("R" + G).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell("S" + G).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell("R" + G).Style.Font.Bold = true;
                            worksheet.Cell("S" + G).Style.Font.Bold = true;
                            worksheet.Cell("R" + G).Value = CAT;
                            worksheet.Cell("S" + G).Value = CATGP;

                        }
                        currentRow = 5;
                        foreach (var item in model.GradeDetails)
                        {

                            currentRow++;
                            worksheet.Cell(currentRow, 21).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 21).Value = item.Name;
                            worksheet.Cell(currentRow, 22).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 22).Value = item.Count;
                            worksheet.Cell(currentRow, 23).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 23).Value = item.GP;
                            int GRADE = 0;
                            int GRADEGP = 0;
                            for (int i = 0; i < model.GradeDetails.Count; i++)
                            {

                                GRADE = GRADE + model.GradeDetails[i].Count;

                                GRADEGP = GRADEGP + model.GradeDetails[i].GP;

                            }




                            int H = GradeCount + 6;
                            worksheet.Cell("U" + H).Style.Font.Bold = true;
                            worksheet.Cell("U" + H).Value = "Total";
                            worksheet.Cell("V" + H).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell("V" + H).Style.Font.Bold = true;
                            worksheet.Cell("V" + H).Value = GRADE;
                            worksheet.Cell("W" + H).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell("W" + H).Style.Font.Bold = true;
                            worksheet.Cell("W" + H).Value = GRADEGP;

                        }
                        currentRow = 5;
                        foreach (var item in model.PayrollGroupDetails)
                        {

                            currentRow++;
                            worksheet.Cell(currentRow, 25).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 25).Value = item.Name;
                            worksheet.Cell(currentRow, 26).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 26).Value = item.Count;
                            worksheet.Cell(currentRow, 27).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 27).Value = item.GP;


                            int PAYRL = 0;
                            int PAYRLGP = 0;
                            for (int i = 0; i < model.PayrollGroupDetails.Count; i++)
                            {

                                PAYRL = PAYRL + model.PayrollGroupDetails[i].Count;

                                PAYRLGP = PAYRLGP + model.PayrollGroupDetails[i].GP;

                            }




                            int I = PayrollCount + 6;
                            worksheet.Cell("Y" + I).Style.Font.Bold = true;

                            worksheet.Cell("Y" + I).Value = "Total";
                            worksheet.Cell("Z" + I).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell("Z" + I).Style.Font.Bold = true;
                            worksheet.Cell("Z" + I).Value = PAYRL;
                            worksheet.Cell("AA" + I).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell("AA" + I).Style.Font.Bold = true;
                            worksheet.Cell("AA" + I).Value = PAYRLGP;

                        }



                        currentRow = 5;
                        foreach (var item in model.DivisionDetails)
                        {

                            currentRow++;
                            worksheet.Cell(currentRow, 29).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 29).Value = item.Name;
                            worksheet.Cell(currentRow, 30).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 30).Value = item.Count;
                            worksheet.Cell(currentRow, 31).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 31).Value = item.GP;


                            int DIV = 0;
                            int DVGP = 0;
                            for (int i = 0; i < model.DivisionDetails.Count; i++)
                            {

                                DIV = DIV + model.DivisionDetails[i].Count;

                                DVGP = DVGP + model.DivisionDetails[i].GP;

                            }




                            int dI = DivisionCount + 6;
                            worksheet.Cell("AC" + dI).Style.Font.Bold = true;

                            worksheet.Cell("AC" + dI).Value = "Total";
                            worksheet.Cell("AD" + dI).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell("AD" + dI).Style.Font.Bold = true;
                            worksheet.Cell("AD" + dI).Value = DIV;
                            worksheet.Cell("AE" + dI).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell("AE" + dI).Style.Font.Bold = true;
                            worksheet.Cell("AE" + dI).Value = DVGP;

                        }










                        currentRow = 5;
                        foreach (var item in model.SalaryBandDetails)
                        {

                            currentRow++;
                            worksheet.Cell(currentRow, 33).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 33).Value = item.Name;
                            worksheet.Cell(currentRow, 34).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 34).Value = item.Count;
                            worksheet.Cell(currentRow, 35).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 35).Value = item.GP;


                            int SAL = 0;
                            int SALLGP = 0;
                            for (int i = 0; i < model.SalaryBandDetails.Count; i++)
                            {

                                SAL = SAL + model.SalaryBandDetails[i].Count;

                                SALLGP = SALLGP + model.SalaryBandDetails[i].GP;

                            }




                            int J = salCount + 6;
                            worksheet.Cell("AG" + J).Style.Font.Bold = true;

                            worksheet.Cell("AG" + J).Value = "Total";
                            worksheet.Cell("AH" + J).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell("AH" + J).Style.Font.Bold = true;
                            worksheet.Cell("AH" + J).Value = SAL;
                            worksheet.Cell("AI" + J).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell("AI" + J).Style.Font.Bold = true;
                            worksheet.Cell("AI" + J).Value = SALLGP;

                        }
                        currentRow = 5;
                        foreach (var item in model.AgeBandDetails)
                        {

                            currentRow++;
                            worksheet.Cell(currentRow, 37).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 37).Value = item.Name;
                            worksheet.Cell(currentRow, 38).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 38).Value = item.Count;
                            worksheet.Cell(currentRow, 39).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 39).Value = item.GP;
                            int AGE = 0;
                            int AGEGP = 0;
                            for (int i = 0; i < model.AgeBandDetails.Count; i++)
                            {

                                AGE = AGE + model.AgeBandDetails[i].Count;

                                AGEGP = AGEGP + model.AgeBandDetails[i].GP;

                            }




                            int K = AgeCount + 6;
                            worksheet.Cell("AK" + K).Style.Font.Bold = true;
                            worksheet.Cell("AK" + K).Value = "Total";
                            worksheet.Cell("AL" + K).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell("AL" + K).Style.Font.Bold = true;
                            worksheet.Cell("AL" + K).Value = AGE;
                            worksheet.Cell("AM" + K).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell("AM" + K).Style.Font.Bold = true;
                            worksheet.Cell("AM" + K).Value = AGEGP;

                        }
                        currentRow = 5;
                        foreach (var item in model.GenderDetails)
                        {

                            currentRow++;
                            worksheet.Cell(currentRow, 41).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 41).Value = item.Name;
                            worksheet.Cell(currentRow, 42).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 42).Value = item.Count;
                            worksheet.Cell(currentRow, 43).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 43).Value = item.GP;

                            int GEN = 0;
                            int GENGP = 0;
                            for (int i = 0; i < model.GenderDetails.Count; i++)
                            {

                                GEN = GEN + model.GenderDetails[i].Count;

                                GENGP = GENGP + model.GenderDetails[i].GP;

                            }




                            int L = GenderCount + 6;
                            worksheet.Cell("AO" + L).Style.Font.Bold = true;
                            worksheet.Cell("AO" + L).Value = "Total";
                            worksheet.Cell("AP" + L).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell("AP" + L).Style.Font.Bold = true;
                            worksheet.Cell("AP" + L).Value = GEN;
                            worksheet.Cell("AQ" + L).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell("AQ" + L).Style.Font.Bold = true;
                            worksheet.Cell("AQ" + L).Value = GENGP;

                        }
                        currentRow = 5;
                        foreach (var item in model.ServiceBandDetails)
                        {

                            currentRow++;
                            worksheet.Cell(currentRow, 45).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 45).Value = item.Name;
                            worksheet.Cell(currentRow, 46).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 46).Value = item.Count;
                            worksheet.Cell(currentRow, 47).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 47).Value = item.GP;



                            int SERV = 0;
                            int SERVGP = 0;
                            for (int i = 0; i < model.ServiceBandDetails.Count; i++)
                            {

                                SERV = SERV + model.ServiceBandDetails[i].Count;

                                SERVGP = SERVGP + model.ServiceBandDetails[i].GP;

                            }




                            int M = serviceCount + 6;
                            worksheet.Cell("AS" + M).Style.Font.Bold = true;
                            worksheet.Cell("AS" + M).Value = "Total";
                            worksheet.Cell("AT" + M).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell("AT" + M).Style.Font.Bold = true;
                            worksheet.Cell("AT" + M).Value = SERV;
                            worksheet.Cell("AU" + M).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell("AU" + M).Style.Font.Bold = true;
                            worksheet.Cell("AU" + M).Value = SERVGP;

                        }
                        currentRow = 5;
                        foreach (var item in model.QualificationDetails)
                        {

                            currentRow++;
                            worksheet.Cell(currentRow, 49).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 49).Value = item.Name;
                            worksheet.Cell(currentRow, 50).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 50).Value = item.Count;
                            worksheet.Cell(currentRow, 51).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 51).Value = item.GP;
                            int QUAL = 0;
                            int QUALGP = 0;
                            for (int i = 0; i < model.QualificationDetails.Count; i++)
                            {

                                QUAL = QUAL + model.QualificationDetails[i].Count;

                                QUALGP = QUALGP + model.QualificationDetails[i].GP;

                            }




                            int N = QualCount + 6;
                            worksheet.Cell("AW" + N).Style.Font.Bold = true;
                            worksheet.Cell("AW" + N).Value = "Total";
                            worksheet.Cell("AX" + N).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell("AX" + N).Style.Font.Bold = true;
                            worksheet.Cell("AX" + N).Value = QUAL;
                            worksheet.Cell("AY" + N).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell("AY" + N).Style.Font.Bold = true;
                            worksheet.Cell("AY" + N).Value = QUALGP;

                        }

                        currentRow = 5;

                        foreach (var item in model.GroupCompanyDetails)
                        {
                            currentRow++;


                            worksheet.Cell(currentRow, 53).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 53).Value = item.Name;
                            worksheet.Cell(currentRow, 54).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 54).Value = item.Count;
                            worksheet.Cell(currentRow, 55).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 55).Value = item.GP;



                            int GroupCmpnyCount = 0;
                            int GroupCmpnyGP = 0;
                            for (int i = 0; i < model.GroupCompanyDetails.Count; i++)
                            {

                                GroupCmpnyCount = GroupCmpnyCount + model.GroupCompanyDetails[i].Count;

                                GroupCmpnyGP = GroupCmpnyGP + model.GroupCompanyDetails[i].GP;

                            }




                            int O = GrpCmpnyCount + 6;
                            worksheet.Cell("BA" + O).Style.Font.Bold = true;
                            worksheet.Cell("BA" + O).Value = "Total";

                            worksheet.Cell("BB" + O).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell("BB" + O).Style.Font.Bold = true;
                            worksheet.Cell("BB" + O).Value = GroupCmpnyCount;

                            worksheet.Cell("BC" + O).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell("BC" + O).Style.Font.Bold = true;
                            worksheet.Cell("BC" + O).Value = GroupCmpnyGP;

                        }




                        currentRow = 5;
                        foreach (var item in model.SectionDetails)
                        {


                            currentRow++;


                            worksheet.Cell(currentRow, 57).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 57).Value = item.Name;
                            worksheet.Cell(currentRow, 58).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 58).Value = item.Count;
                            worksheet.Cell(currentRow, 59).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 59).Value = item.GP;



                            int SectionCount = 0;
                            int SectionGP = 0;
                            for (int i = 0; i < model.SectionDetails.Count; i++)
                            {

                                SectionCount = SectionCount + model.SectionDetails[i].Count;

                                SectionGP = SectionGP + model.SectionDetails[i].GP;

                            }




                            int P = SecCount + 6;
                            worksheet.Cell("BE" + P).Style.Font.Bold = true;
                            worksheet.Cell("BE" + P).Value = "Total";

                            worksheet.Cell("BF" + P).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell("BF" + P).Style.Font.Bold = true;
                            worksheet.Cell("BF" + P).Value = SectionCount;

                            worksheet.Cell("BG" + P).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell("BG" + P).Style.Font.Bold = true;
                            worksheet.Cell("BG" + P).Value = SectionGP;

                        }



                        #endregion
                        worksheet.Columns().Style.Alignment.WrapText = true;
                        worksheet.Columns().AdjustToContents();
                        using (var stream = new MemoryStream())
                        {
                            workbook.SaveAs(stream);
                            if (EP.NeedPassword)
                            {
                                Workbook book = new Workbook();
                                book.LoadFromStream(stream);
                                //Protect Workbook
                                book.Protect(EP.Password, false, false);
                                book.SaveToStream(stream);
                            }
                            var content = stream.ToArray();
                            return File(
                                content,
                                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                                "Company Profile Details List.xlsx"
                                );
                        }


                    }

                }
                else
                    using (var workbook = new XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("Company Profile");

                        int DesCount = model.DesignationDetails.Count;
                        int DeptCount = model.DepartmentDetails.Count;
                        int RegCount = model.RegionDetails.Count;
                        int LocCount = model.LocationDetails.Count;
                        int CatCount = model.CategoryDetails.Count;
                        int GradeCount = model.GradeDetails.Count;
                        int PayrollCount = model.PayrollGroupDetails.Count;
                        int DivisionCount = model.DivisionDetails.Count;
                        int salCount = model.SalaryBandDetails.Count;
                        int AgeCount = model.AgeBandDetails.Count;
                        int GenderCount = model.GenderDetails.Count;
                        int serviceCount = model.ServiceBandDetails.Count;
                        int QualCount = model.QualificationDetails.Count;
                        int GrpCmpnyCount = model.GroupCompanyDetails.Count;
                        int SecCount = model.SectionDetails.Count;
                        int[] numbers = { DesCount, DeptCount, RegCount, LocCount, CatCount, GradeCount, PayrollCount, DivisionCount, salCount, AgeCount, GenderCount, serviceCount, QualCount, GrpCmpnyCount, SecCount };
                        int biggestNumber = numbers.Max();


                        int PendingCountnum = biggestNumber + 5;


                        worksheet.Range("A1:AY" + PendingCountnum).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:AY" + PendingCountnum).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:AY" + PendingCountnum).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:AY" + PendingCountnum).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                        worksheet.Range("A1:AY" + PendingCountnum).Style.Border.LeftBorder = XLBorderStyleValues.Thin;

                        //var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(90)));
                        var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, "AY"));

                        wsReportNameHeaderRange.Style.Font.Bold = true;
                        wsReportNameHeaderRange.Style.Font.FontSize = 15;
                        wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                        wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        wsReportNameHeaderRange.Merge();
                        wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                        wsReportNameHeaderRange.Style.Font.FontSize = 15;
                        wsReportNameHeaderRange.Value = "Company Profile";
                        worksheet.Range("A4:AY4").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                        worksheet.Cell(2, 1).Style.Font.Bold = true;
                        worksheet.Cell(2, 1).Value = "Group Company Name";
                        worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                        worksheet.Cell(2, 4).Style.Font.Bold = true;
                        worksheet.Cell(2, 4).Value = "Login User";
                        worksheet.Cell(2, 5).Value = LoginUser;
                        worksheet.Cell(2, 7).Style.Font.Bold = true;
                        worksheet.Cell(2, 7).Value = "Date & Time";
                        worksheet.Cell(2, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(2, 8).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
                        worksheet.Cell(2, 8).Value = currDateTime.ToString();


                        int DesignationCount = 0;
                        int DesignationGP = 0;
                        for (int i = 0; i < model.DesignationDetails.Count; i++)
                        {

                            DesignationCount = DesignationCount + model.DesignationDetails[i].Count;
                            DesignationGP = DesignationGP + model.DesignationDetails[i].GP;


                        }



                        worksheet.Cell(3, 1).Style.Font.Bold = true;
                        worksheet.Cell(3, 1).Value = "Total Emp Count";
                        worksheet.Cell(3, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                        worksheet.Cell(3, 2).Value = DesignationCount;
                        //worksheet.Cell(3, 4).Style.Font.Bold = true;
                        //worksheet.Cell(3, 4).Value = "Total Gross Pay";
                        //worksheet.Cell(3, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
                        //worksheet.Cell(3, 5).Value = DesignationGP;








                        var currentRow = 4;

                        #region Header
                        worksheet.Cell(currentRow, 1).Value = "Designation";
                        worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 2).Value = "Count";
                        worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                        //worksheet.Cell(currentRow, 3).Value = "GP";
                        //worksheet.Cell(currentRow, 3).Style.Font.Bold = true;



                        worksheet.Cell(currentRow, 5).Value = "Department";
                        worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 6).Value = "Count";
                        worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                        //worksheet.Cell(currentRow, 7).Value = "GP";
                        //worksheet.Cell(currentRow, 7).Style.Font.Bold = true;


                        worksheet.Cell(currentRow, 9).Value = "Region";
                        worksheet.Cell(currentRow, 9).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 10).Value = "Count";
                        worksheet.Cell(currentRow, 10).Style.Font.Bold = true;
                        //worksheet.Cell(currentRow, 11).Value = "GP";
                        //worksheet.Cell(currentRow, 11).Style.Font.Bold = true;

                        worksheet.Cell(currentRow, 13).Value = "Location";
                        worksheet.Cell(currentRow, 13).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 14).Value = "Count";
                        worksheet.Cell(currentRow, 14).Style.Font.Bold = true;
                        //worksheet.Cell(currentRow, 15).Value = "GP";
                        //worksheet.Cell(currentRow, 15).Style.Font.Bold = true;

                        worksheet.Cell(currentRow, 17).Value = "Category";
                        worksheet.Cell(currentRow, 17).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 18).Value = "Count";
                        worksheet.Cell(currentRow, 18).Style.Font.Bold = true;
                        //worksheet.Cell(currentRow, 19).Value = "GP";
                        //worksheet.Cell(currentRow, 19).Style.Font.Bold = true;

                        worksheet.Cell(currentRow, 21).Value = "Grade";
                        worksheet.Cell(currentRow, 21).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 22).Value = "Count";
                        worksheet.Cell(currentRow, 22).Style.Font.Bold = true;
                        //worksheet.Cell(currentRow, 23).Value = "GP";
                        //worksheet.Cell(currentRow, 23).Style.Font.Bold = true;

                        worksheet.Cell(currentRow, 25).Value = "PayRoll Group";
                        worksheet.Cell(currentRow, 25).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 26).Value = "Count";
                        worksheet.Cell(currentRow, 26).Style.Font.Bold = true;
                        //worksheet.Cell(currentRow, 27).Value = "GP";
                        //worksheet.Cell(currentRow, 27).Style.Font.Bold = true;

                        worksheet.Cell(currentRow, 29).Value = "Division";
                        worksheet.Cell(currentRow, 29).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 30).Value = "Count";
                        worksheet.Cell(currentRow, 30).Style.Font.Bold = true;
                        //worksheet.Cell(currentRow, 31).Value = "GP";
                        //worksheet.Cell(currentRow, 31).Style.Font.Bold = true;

                        worksheet.Cell(currentRow, 33).Value = "Salary Band";
                        worksheet.Cell(currentRow, 33).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 34).Value = "Count";
                        worksheet.Cell(currentRow, 34).Style.Font.Bold = true;
                        //worksheet.Cell(currentRow, 35).Value = "GP";
                        //worksheet.Cell(currentRow, 35).Style.Font.Bold = true;


                        worksheet.Cell(currentRow, 37).Value = "Age Band";
                        worksheet.Cell(currentRow, 37).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 38).Value = "Count";
                        worksheet.Cell(currentRow, 38).Style.Font.Bold = true;
                        //worksheet.Cell(currentRow, 39).Value = "GP";
                        //worksheet.Cell(currentRow, 39).Style.Font.Bold = true;


                        worksheet.Cell(currentRow, 41).Value = "Gender";
                        worksheet.Cell(currentRow, 41).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 42).Value = "Count";
                        worksheet.Cell(currentRow, 42).Style.Font.Bold = true;
                        //worksheet.Cell(currentRow, 43).Value = "GP";
                        //worksheet.Cell(currentRow, 43).Style.Font.Bold = true;


                        worksheet.Cell(currentRow, 45).Value = "Service Band";
                        worksheet.Cell(currentRow, 45).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 46).Value = "Count";
                        worksheet.Cell(currentRow, 46).Style.Font.Bold = true;
                        //worksheet.Cell(currentRow, 47).Value = "GP";
                        //worksheet.Cell(currentRow, 47).Style.Font.Bold = true;

                        worksheet.Cell(currentRow, 49).Value = "Qualification";
                        worksheet.Cell(currentRow, 49).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 50).Value = "Count";
                        worksheet.Cell(currentRow, 50).Style.Font.Bold = true;
                        //worksheet.Cell(currentRow, 51).Value = "GP";
                        //worksheet.Cell(currentRow, 51).Style.Font.Bold = true;

                        worksheet.Cell(currentRow, 53).Value = "Group Company";
                        worksheet.Cell(currentRow, 53).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 54).Value = "Count";
                        worksheet.Cell(currentRow, 54).Style.Font.Bold = true;
                        //worksheet.Cell(currentRow, 55).Value = "GP";
                        //worksheet.Cell(currentRow, 55).Style.Font.Bold = true;

                        worksheet.Cell(currentRow, 57).Value = "Section";
                        worksheet.Cell(currentRow, 57).Style.Font.Bold = true;
                        worksheet.Cell(currentRow, 58).Value = "Count";
                        worksheet.Cell(currentRow, 58).Style.Font.Bold = true;
                        //worksheet.Cell(currentRow, 59).Value = "GP";
                        //worksheet.Cell(currentRow, 59).Style.Font.Bold = true;

                        #endregion

                        #region Body


                        foreach (var item in model.DesignationDetails)
                        {


                            currentRow++;


                            worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 1).Value = item.Name;
                            worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 2).Value = item.Count;
                           // worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            //worksheet.Cell(currentRow, 3).Value = item.GP;



                            int DesignanCount = 0;
                            int DesnGP = 0;
                            for (int i = 0; i < model.DesignationDetails.Count; i++)
                            {

                                DesignanCount = DesignanCount + model.DesignationDetails[i].Count;

                                //DesnGP = DesnGP + model.DesignationDetails[i].GP;

                            }




                            int c = DesCount + 5;
                            worksheet.Cell("A" + c).Style.Font.Bold = true;
                            worksheet.Cell("A" + c).Value = "Total";

                            worksheet.Cell("B" + c).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell("B" + c).Style.Font.Bold = true;
                            worksheet.Cell("B" + c).Value = DesignanCount;

                            //worksheet.Cell("C" + c).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            //worksheet.Cell("C" + c).Style.Font.Bold = true;
                            //worksheet.Cell("C" + c).Value = DesnGP;

                        }




                        currentRow = 4;
                        foreach (var item in model.DepartmentDetails)
                        {

                            currentRow++;
                            worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 5).Value = item.Name;
                            worksheet.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 6).Value = item.Count;
                            //worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            //worksheet.Cell(currentRow, 7).Value = item.GP;


                            int depCount = 0;
                            int DepGP = 0;
                            for (int i = 0; i < model.DepartmentDetails.Count; i++)
                            {

                                depCount = depCount + model.DepartmentDetails[i].Count;

                                //DepGP = DepGP + model.DepartmentDetails[i].GP;

                            }




                            int d = DeptCount + 5;
                            worksheet.Cell("E" + d).Style.Font.Bold = true;
                            worksheet.Cell("E" + d).Value = "Total";
                            worksheet.Cell("E" + d).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);

                            worksheet.Cell("F" + d).Value = depCount;
                            worksheet.Cell("F" + d).Style.Font.Bold = true;
                            worksheet.Cell("F" + d).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);

                            //worksheet.Cell("G" + d).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            //worksheet.Cell("G" + d).Value = DepGP;
                            //worksheet.Cell("G" + d).Style.Font.Bold = true;



                        }
                        currentRow = 4;
                        foreach (var item in model.RegionDetails)
                        {

                            currentRow++;
                            worksheet.Cell(currentRow, 9).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 9).Value = item.Name;
                            worksheet.Cell(currentRow, 10).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 10).Value = item.Count;
                            //worksheet.Cell(currentRow, 11).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            //worksheet.Cell(currentRow, 11).Value = item.GP;







                            int RegICount = 0;
                            int RegGP = 0;
                            for (int i = 0; i < model.RegionDetails.Count; i++)
                            {

                                RegICount = RegICount + model.RegionDetails[i].Count;

                                //RegGP = RegGP + model.RegionDetails[i].GP;

                            }




                            int e = RegCount + 5;
                            worksheet.Cell("I" + e).Style.Font.Bold = true;
                            worksheet.Cell("I" + e).Value = "Total";

                            worksheet.Cell("J" + e).Style.Font.Bold = true;
                            worksheet.Cell("J" + e).Value = RegICount;
                            worksheet.Cell("J" + e).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);

                            //worksheet.Cell("K" + e).Style.Font.Bold = true;
                            //worksheet.Cell("K" + e).Value = RegGP;
                            //worksheet.Cell("K" + e).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);

                        }
                        currentRow = 4;
                        foreach (var item in model.LocationDetails)
                        {

                            currentRow++;
                            worksheet.Cell(currentRow, 13).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 13).Value = item.Name;
                            worksheet.Cell(currentRow, 14).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 14).Value = item.Count;
                            //worksheet.Cell(currentRow, 15).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            //worksheet.Cell(currentRow, 15).Value = item.GP;

                            int lOC = 0;
                            int lOCGP = 0;
                            for (int i = 0; i < model.LocationDetails.Count; i++)
                            {

                                lOC = lOC + model.LocationDetails[i].Count;

                                //lOCGP = lOCGP + model.LocationDetails[i].GP;

                            }




                            int F = LocCount + 5;
                            worksheet.Cell("M" + F).Style.Font.Bold = true;
                            worksheet.Cell("M" + F).Value = "Total";

                            worksheet.Cell("N" + F).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell("N" + F).Style.Font.Bold = true;
                            worksheet.Cell("N" + F).Value = lOC;

                            //worksheet.Cell("o" + F).Style.Font.Bold = true;
                            //worksheet.Cell("o" + F).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            //worksheet.Cell("O" + F).Value = lOCGP;


                        }
                        currentRow = 4;
                        foreach (var item in model.CategoryDetails)
                        {

                            currentRow++;
                            worksheet.Cell(currentRow, 17).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 17).Value = item.Name;
                            worksheet.Cell(currentRow, 18).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 18).Value = item.Count;
                            //worksheet.Cell(currentRow, 19).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            //worksheet.Cell(currentRow, 19).Value = item.GP;


                            int CAT = 0;
                            int CATGP = 0;
                            for (int i = 0; i < model.CategoryDetails.Count; i++)
                            {

                                CAT = CAT + model.CategoryDetails[i].Count;

                                //CATGP = CATGP + model.CategoryDetails[i].GP;

                            }




                            int G = CatCount + 5;
                            worksheet.Cell("Q" + G).Value = "Total";
                            worksheet.Cell("Q" + G).Style.Font.Bold = true;
                            worksheet.Cell("Q" + G).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell("R" + G).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell("S" + G).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell("R" + G).Style.Font.Bold = true;
                            worksheet.Cell("S" + G).Style.Font.Bold = true;
                            worksheet.Cell("R" + G).Value = CAT;
                            //worksheet.Cell("S" + G).Value = CATGP;

                        }
                        currentRow = 4;
                        foreach (var item in model.GradeDetails)
                        {

                            currentRow++;
                            worksheet.Cell(currentRow, 21).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 21).Value = item.Name;
                            worksheet.Cell(currentRow, 22).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 22).Value = item.Count;
                            //worksheet.Cell(currentRow, 23).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            //worksheet.Cell(currentRow, 23).Value = item.GP;
                            int GRADE = 0;
                            int GRADEGP = 0;
                            for (int i = 0; i < model.GradeDetails.Count; i++)
                            {

                                GRADE = GRADE + model.GradeDetails[i].Count;

                                //GRADEGP = GRADEGP + model.GradeDetails[i].GP;

                            }




                            int H = GradeCount + 5;
                            worksheet.Cell("U" + H).Style.Font.Bold = true;
                            worksheet.Cell("U" + H).Value = "Total";
                            worksheet.Cell("V" + H).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell("V" + H).Style.Font.Bold = true;
                            worksheet.Cell("V" + H).Value = GRADE;
                            //worksheet.Cell("W" + H).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            //worksheet.Cell("W" + H).Style.Font.Bold = true;
                            //worksheet.Cell("W" + H).Value = GRADEGP;

                        }
                        currentRow = 4;
                        foreach (var item in model.PayrollGroupDetails)
                        {

                            currentRow++;
                            worksheet.Cell(currentRow, 25).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 25).Value = item.Name;
                            worksheet.Cell(currentRow, 26).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 26).Value = item.Count;
                            //worksheet.Cell(currentRow, 27).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            //worksheet.Cell(currentRow, 27).Value = item.GP;


                            int PAYRL = 0;
                            int PAYRLGP = 0;
                            for (int i = 0; i < model.PayrollGroupDetails.Count; i++)
                            {

                                PAYRL = PAYRL + model.PayrollGroupDetails[i].Count;

                                //PAYRLGP = PAYRLGP + model.PayrollGroupDetails[i].GP;

                            }




                            int I = PayrollCount + 5;
                            worksheet.Cell("Y" + I).Style.Font.Bold = true;

                            worksheet.Cell("Y" + I).Value = "Total";
                            worksheet.Cell("Z" + I).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell("Z" + I).Style.Font.Bold = true;
                            worksheet.Cell("Z" + I).Value = PAYRL;
                            //worksheet.Cell("AA" + I).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            //worksheet.Cell("AA" + I).Style.Font.Bold = true;
                            //worksheet.Cell("AA" + I).Value = PAYRLGP;

                        }



                        currentRow = 4;
                        foreach (var item in model.DivisionDetails)
                        {

                            currentRow++;
                            worksheet.Cell(currentRow, 29).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 29).Value = item.Name;
                            worksheet.Cell(currentRow, 30).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 30).Value = item.Count;
                            //worksheet.Cell(currentRow, 31).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            //worksheet.Cell(currentRow, 31).Value = item.GP;


                            int DIV = 0;
                            int DVGP = 0;
                            for (int i = 0; i < model.DivisionDetails.Count; i++)
                            {

                                DIV = DIV + model.DivisionDetails[i].Count;

                                //DVGP = DVGP + model.DivisionDetails[i].GP;

                            }




                            int dI = DivisionCount + 5;
                            worksheet.Cell("AC" + dI).Style.Font.Bold = true;

                            worksheet.Cell("AC" + dI).Value = "Total";
                            worksheet.Cell("AD" + dI).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell("AD" + dI).Style.Font.Bold = true;
                            worksheet.Cell("AD" + dI).Value = DIV;
                            //worksheet.Cell("AE" + dI).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            //worksheet.Cell("AE" + dI).Style.Font.Bold = true;
                            //worksheet.Cell("AE" + dI).Value = DVGP;

                        }










                        currentRow = 4;
                        foreach (var item in model.SalaryBandDetails)
                        {

                            currentRow++;
                            worksheet.Cell(currentRow, 33).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 33).Value = item.Name;
                            worksheet.Cell(currentRow, 34).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 34).Value = item.Count;
                            //worksheet.Cell(currentRow, 35).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            //worksheet.Cell(currentRow, 35).Value = item.GP;


                            int SAL = 0;
                            int SALLGP = 0;
                            for (int i = 0; i < model.SalaryBandDetails.Count; i++)
                            {

                                SAL = SAL + model.SalaryBandDetails[i].Count;

                                //SALLGP = SALLGP + model.SalaryBandDetails[i].GP;

                            }




                            int J = salCount + 5;
                            worksheet.Cell("AG" + J).Style.Font.Bold = true;

                            worksheet.Cell("AG" + J).Value = "Total";
                            worksheet.Cell("AH" + J).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell("AH" + J).Style.Font.Bold = true;
                            worksheet.Cell("AH" + J).Value = SAL;
                            //worksheet.Cell("AI" + J).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            //worksheet.Cell("AI" + J).Style.Font.Bold = true;
                            //worksheet.Cell("AI" + J).Value = SALLGP;

                        }
                        currentRow = 4;
                        foreach (var item in model.AgeBandDetails)
                        {

                            currentRow++;
                            worksheet.Cell(currentRow, 37).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 37).Value = item.Name;
                            worksheet.Cell(currentRow, 38).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 38).Value = item.Count;
                            //worksheet.Cell(currentRow, 39).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            //worksheet.Cell(currentRow, 39).Value = item.GP;
                            int AGE = 0;
                            int AGEGP = 0;
                            for (int i = 0; i < model.AgeBandDetails.Count; i++)
                            {

                                AGE = AGE + model.AgeBandDetails[i].Count;

                                //AGEGP = AGEGP + model.AgeBandDetails[i].GP;

                            }




                            int K = AgeCount + 5;
                            worksheet.Cell("AK" + K).Style.Font.Bold = true;
                            worksheet.Cell("AK" + K).Value = "Total";
                            worksheet.Cell("AL" + K).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell("AL" + K).Style.Font.Bold = true;
                            worksheet.Cell("AL" + K).Value = AGE;
                            //worksheet.Cell("AM" + K).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            //worksheet.Cell("AM" + K).Style.Font.Bold = true;
                            //worksheet.Cell("AM" + K).Value = AGEGP;

                        }
                        currentRow = 4;
                        foreach (var item in model.GenderDetails)
                        {

                            currentRow++;
                            worksheet.Cell(currentRow, 41).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 41).Value = item.Name;
                            worksheet.Cell(currentRow, 42).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 42).Value = item.Count;
                            //worksheet.Cell(currentRow, 43).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            //worksheet.Cell(currentRow, 43).Value = item.GP;

                            int GEN = 0;
                            int GENGP = 0;
                            for (int i = 0; i < model.GenderDetails.Count; i++)
                            {

                                GEN = GEN + model.GenderDetails[i].Count;

                                //GENGP = GENGP + model.GenderDetails[i].GP;

                            }




                            int L = GenderCount + 5;
                            worksheet.Cell("AO" + L).Style.Font.Bold = true;
                            worksheet.Cell("AO" + L).Value = "Total";
                            worksheet.Cell("AP" + L).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell("AP" + L).Style.Font.Bold = true;
                            worksheet.Cell("AP" + L).Value = GEN;
                            //worksheet.Cell("AQ" + L).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            //worksheet.Cell("AQ" + L).Style.Font.Bold = true;
                            //worksheet.Cell("AQ" + L).Value = GENGP;

                        }
                        currentRow = 4;
                        foreach (var item in model.ServiceBandDetails)
                        {

                            currentRow++;
                            worksheet.Cell(currentRow, 45).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 45).Value = item.Name;
                            worksheet.Cell(currentRow, 46).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 46).Value = item.Count;
                            //worksheet.Cell(currentRow, 47).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            //worksheet.Cell(currentRow, 47).Value = item.GP;



                            int SERV = 0;
                            int SERVGP = 0;
                            for (int i = 0; i < model.ServiceBandDetails.Count; i++)
                            {

                                SERV = SERV + model.ServiceBandDetails[i].Count;

                                //SERVGP = SERVGP + model.ServiceBandDetails[i].GP;

                            }




                            int M = serviceCount + 5;
                            worksheet.Cell("AS" + M).Style.Font.Bold = true;
                            worksheet.Cell("AS" + M).Value = "Total";
                            worksheet.Cell("AT" + M).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell("AT" + M).Style.Font.Bold = true;
                            worksheet.Cell("AT" + M).Value = SERV;
                            //worksheet.Cell("AU" + M).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            //worksheet.Cell("AU" + M).Style.Font.Bold = true;
                            //worksheet.Cell("AU" + M).Value = SERVGP;

                        }
                        currentRow = 4;
                        foreach (var item in model.QualificationDetails)
                        {

                            currentRow++;
                            worksheet.Cell(currentRow, 49).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 49).Value = item.Name;
                            worksheet.Cell(currentRow, 50).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 50).Value = item.Count;
                            //worksheet.Cell(currentRow, 51).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            //worksheet.Cell(currentRow, 51).Value = item.GP;
                            int QUAL = 0;
                            int QUALGP = 0;
                            for (int i = 0; i < model.QualificationDetails.Count; i++)
                            {

                                QUAL = QUAL + model.QualificationDetails[i].Count;

                                //QUALGP = QUALGP + model.QualificationDetails[i].GP;

                            }




                            int N = QualCount + 5;
                            worksheet.Cell("AW" + N).Style.Font.Bold = true;
                            worksheet.Cell("AW" + N).Value = "Total";
                            worksheet.Cell("AX" + N).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell("AX" + N).Style.Font.Bold = true;
                            worksheet.Cell("AX" + N).Value = QUAL;
                            //worksheet.Cell("AY" + N).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            //worksheet.Cell("AY" + N).Style.Font.Bold = true;
                            //worksheet.Cell("AY" + N).Value = QUALGP;

                        }
                        foreach (var item in model.GroupCompanyDetails)
                        {


                            currentRow++;


                            worksheet.Cell(currentRow, 53).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 53).Value = item.Name;
                            worksheet.Cell(currentRow, 54).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 54).Value = item.Count;
                            //worksheet.Cell(currentRow, 55).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            //worksheet.Cell(currentRow, 55).Value = item.GP;



                            int GroupCmpnyCount = 0;
                            int GroupCmpnyGP = 0;
                            for (int i = 0; i < model.GroupCompanyDetails.Count; i++)
                            {

                                GroupCmpnyCount = GroupCmpnyCount + model.GroupCompanyDetails[i].Count;

                                //GroupCmpnyGP = GroupCmpnyGP + model.GroupCompanyDetails[i].GP;

                            }




                            int O = GrpCmpnyCount + 5;
                            worksheet.Cell("BA" + O).Style.Font.Bold = true;
                            worksheet.Cell("BA" + O).Value = "Total";

                            worksheet.Cell("BB" + O).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell("BB" + O).Style.Font.Bold = true;
                            worksheet.Cell("BB" + O).Value = GroupCmpnyCount;

                            //worksheet.Cell("C" + c).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            //worksheet.Cell("C" + c).Style.Font.Bold = true;
                            //worksheet.Cell("C" + c).Value = GroupCmpnyGP;

                        }




                        currentRow = 4;
                        foreach (var item in model.SectionDetails)
                        {


                            currentRow++;


                            worksheet.Cell(currentRow, 57).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 57).Value = item.Name;
                            worksheet.Cell(currentRow, 58).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell(currentRow, 58).Value = item.Count;
                            //worksheet.Cell(currentRow, 59).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            //worksheet.Cell(currentRow, 59).Value = item.GP;



                            int SectionCount = 0;
                            int SectionGP = 0;
                            for (int i = 0; i < model.SectionDetails.Count; i++)
                            {

                                SectionCount = SectionCount + model.SectionDetails[i].Count;

                                //SectionGP = SectionGP + model.SectionDetails[i].GP;

                            }




                            int P = SecCount + 5;
                            worksheet.Cell("BE" + P).Style.Font.Bold = true;
                            worksheet.Cell("BE" + P).Value = "Total";

                            worksheet.Cell("BF" + P).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            worksheet.Cell("BF" + P).Style.Font.Bold = true;
                            worksheet.Cell("BF" + P).Value = SectionCount;

                            //worksheet.Cell("C" + c).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                            //worksheet.Cell("C" + c).Style.Font.Bold = true;
                            //worksheet.Cell("C" + c).Value = SectionGP;

                        }
                        #endregion
                        worksheet.Columns().Style.Alignment.WrapText = true;
                        worksheet.Columns().AdjustToContents();
                        using (var stream = new MemoryStream())
                        {
                            workbook.SaveAs(stream);
                            if (EP.NeedPassword)
                            {
                                Workbook book = new Workbook();
                                book.LoadFromStream(stream);
                                //Protect Workbook
                                book.Protect(EP.Password, false, false);
                                book.SaveToStream(stream);
                            }
                            var content = stream.ToArray();
                            return File(
                                content,
                                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                                "Company Profile Details List.xlsx"
                                );
                        }
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
      

        public async Task<IActionResult> ExportToExcelCompanyProfileList(int CODE, string TYPE,string CodeName,Filter filter)
        {
            try {           
         
            string currDateTime = await _helpers.GetCurrentDate();
            
            string LoginUser = HttpContext.Session.GetString("LoginUser");
            var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
                EP = await _commonService.GetExcelPassword("Company Profile(" + CodeName + ")", "Excel");
                if (filter == null)
                    filter = new Filter();
              
                string FilterValues = await _helpers.GetFilterValues(filter);
                if (FilterValues == "")
                {
                    FilterValues = "All";
                }
              
                //Get Dataset
                var data = await _companyProfileService.GetCompanyProfileEmployee_Excel(CODE, TYPE, filter);
                DataTable dt = data.Tables[0];


                //set EXCEL Column Heading 
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].ColumnName == "doj" )
                        dt.Columns[i].ColumnName = dt.Columns[i].ColumnName.ToUpper();
                    else
                        dt.Columns[i].ColumnName = char.ToUpper(dt.Columns[i].ColumnName[0]) + dt.Columns[i].ColumnName.Substring(1);
                }


                using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Company Profile");
                    int count = dt.Rows.Count + 6;
                    string columnName = "";
                    //int Columncount = model.MainList.Header.Count + 3;
                    int Columncount = dt.Columns.Count;
                    int rowcount = dt.Rows.Count + 6;

                    while (Columncount > 0)
                    {
                        int modulo = (Columncount - 1) % 26;
                        columnName = Convert.ToChar('A' + modulo) + columnName;
                        Columncount = (Columncount - modulo) / 26;
                    }

                    string colName = columnName;

                    worksheet.Range("A1" , colName+ rowcount).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1", colName + rowcount).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1", colName + rowcount).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1", colName + rowcount).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1", colName + rowcount).Style.Border.LeftBorder = XLBorderStyleValues.Thin;

                var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, colName));
                wsReportNameHeaderRange.Style.Font.Bold = true;
                wsReportNameHeaderRange.Style.Font.FontSize = 15;
                wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                wsReportNameHeaderRange.Merge();
                wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191);
                wsReportNameHeaderRange.Style.Font.FontSize = 15;
                wsReportNameHeaderRange.Value = "Company Profile";
                worksheet.Range("A6", colName + 6).Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                worksheet.Cell(2, 1).Style.Font.Bold = true;
                worksheet.Cell(2, 1).Value = "Group Company Name";
                    worksheet.Cell(2, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                worksheet.Cell(4, 1).Style.Font.Bold = true;
                worksheet.Cell(4, 1).Value = "Login User";
                    worksheet.Cell(4, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(4, 2).Value = LoginUser;
                worksheet.Cell(4, 3).Style.Font.Bold = true;
                worksheet.Cell(4, 3).Value = "Date & Time";
                worksheet.Cell(4, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(4, 4).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
                worksheet.Cell(4, 4).Value = currDateTime.ToString();

                worksheet.Cell(4, 5).Style.Font.Bold = true;
                worksheet.Cell(4, 5).Value = "Total Count";
                worksheet.Cell(4, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(4, 6).Value =dt.Rows.Count;

                worksheet.Cell(3, 1).Style.Font.Bold = true;
                worksheet.Cell(3, 1).Value = TYPE;
                worksheet.Cell(3, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(3, 2).Value = CodeName;
                    worksheet.Cell(5, 1).Style.Font.Bold = true;
                    worksheet.Cell(5, 1).Value = "Filter Applied";
                    worksheet.Range("B5", colName + 5).Merge();
                    worksheet.Cell("B5").Value = FilterValues;


                    var currentRow = 6;


                    worksheet.Cell(currentRow, 1).InsertTable(dt);
                    worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;
                    worksheet.Tables.FirstOrDefault().Theme = XLTableTheme.None;
                    worksheet.Range("A6", colName + 6).Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Range("A6", colName + 6).Style.Font.Bold = true;



                    worksheet.Columns().AdjustToContents();
                worksheet.Rows().AdjustToContents();

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                        if (EP.NeedPassword)
                        {
                            Workbook book = new Workbook();
                            book.LoadFromStream(stream);
                            //Protect Workbook
                            book.Protect(EP.Password,false,false);
                            book.SaveToStream(stream);
                        }
                        var content = stream.ToArray();
                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Company Profile List.xlsx"
                        );
                }
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
