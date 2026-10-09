using ClosedXML.Excel;
using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.Services.DataBank;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Spire.Xls;
using Hrms.Lite.Shared.Master;
using Newtonsoft.Json;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Services.IServices.Account;

namespace Hrms.Lite.UI.Areas.DataBank.Controllers
{
    [Area("DataBank")]
    [SessionAuthorize]
    public class JobTransitionController : Controller
    { 
        private readonly IDropdownService _dropdownService;
        private readonly IJobTransitionService _jobTransitionService;
        private readonly IAccountService _AccountService;
        private readonly ICommonService _commonService;
        private readonly IHelpers _helpers;
        public static class Global
        {
            public static Filter GlobalFilter;

        }
        public JobTransitionController(IDropdownService dropdownService, ICommonService commonService, IJobTransitionService jobTransitionService, IHelpers helpers, IAccountService AccountService)
        {
            _jobTransitionService = jobTransitionService;
            _dropdownService = dropdownService;
            _commonService = commonService;
            _helpers = helpers;
            _AccountService = AccountService;
        }


        public async Task<IActionResult> Index()
        {
            try { 
            JobTransition model = new JobTransition();
            JobTransition _model = new JobTransition();
            Filter filter = new Filter();
            model.Filter = new  FilterSlabComboFill();
            model.EmployeeHeader = new EmployeeHeader();
            model.EmployeeHeader.Department = new Shared.Master.Department();
            model.EmployeeHeader.Designation = new Shared.Master.Designation();
            model.EmployeeHeader.Location = new Shared.Master.Location();
            model.EmployeeHeader.Category = new Shared.Master.Category();
            ViewBag.Employee = new SelectList(await _dropdownService.GetEmployeeDropDown(), "Code", "Name");
            
            ViewBag.Filter = new SelectList(await _commonService.GetDurationWiseFilterSlabDropDown(), "Code", "Name");
            model.Filter.Code = 0;
            int TabIndex = 0;
            int SearchId = 0;
            model.JobTransition_Tabs = (List<JobTransition_Tabs>)await _jobTransitionService.GetJobTransitionTabs();
            _model = await _jobTransitionService.GetJobTransitionList(TabIndex, SearchId,filter);
           
            model.JobTransitionList = _model.JobTransitionList;
            model.AllTabCount = _model.AllTabCount;
            model.Tab1Count = _model.Tab1Count;
            model.Tab2Count = _model.Tab2Count;
            model.Tab3Count = _model.Tab3Count;
            model.Tab4Count = _model.Tab4Count;
            model.MoreCount = _model.MoreCount;
            await fillfilterDropdown();
            ViewBag.Tabs = new SelectList(model.JobTransition_Tabs, "Code", "Name");
                
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
            ViewBag.VDAGroup = new SelectList(await _dropdownService.GetVDACombo(), "Code", "Name");

        }


        public async Task<IActionResult> ApplyFilter(int TabIndex, int SearchId, Filter filter)
        {
            try { 
            JobTransition model = new JobTransition();
            Global.GlobalFilter = filter;
            model = await _jobTransitionService.GetJobTransitionList(TabIndex, SearchId, filter);
            return PartialView("_JobTransitionList", model);
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

     
        public IActionResult  NewRequest()
        {
                 return PartialView("_NewRequest");
        }


        public async Task<IActionResult> EmployeeCard(int EmpCode)
        {
            try { 
            JobTransition model = new JobTransition();
            model.EmployeeHeader = await _commonService.GetEmployeeCard(EmpCode);

            return PartialView("_EmployeeCard", model.EmployeeHeader);
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




        public async Task<IActionResult> Create(Guid EmployeeGI,string BasedOn)
        {
            try {   
                JobTransition model = new JobTransition();
    
                //model.JobTransition_Allowance = new List<JobTransition_Allowance>();
                //model.JobTransition_Deduction = new List<JobTransition_Deduction>();
                //model.JobTransition_StatutoryDeductions = new List<JobTransition_StatutoryDeductions>();

                JobTransition _growth_history = new JobTransition();
                JobTransition _model = new JobTransition();
                Guid HistoryGI = new Guid("00000000-0000-0000-0000-000000000000");
                await fillDropdown();
           
                _growth_history = await _jobTransitionService.GetJobTransitionGrowthHistory(EmployeeGI);
           
               _model= await _jobTransitionService.GetJobTransitionDetails(EmployeeGI, HistoryGI,"CREATE", BasedOn);
                model = _model;
                if (_growth_history.GrowthHistory == null)
                    model.GrowthHistory = new List<JobTransition_GrowthHistory>();
                else
                    model.GrowthHistory = _growth_history.GrowthHistory;
                model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);
                model.EmployeeGI = EmployeeGI;
                ViewBag.SlryStruct = new SelectList(await _dropdownService.GeSalaryStructureDropDown(), "Code", "Name");
                model.JobTransition_JobDetail.NewSalaryStructure = new SalaryStructure();
                model.JobTransition_JobDetail.NewSalaryStructure = model.JobTransition_JobDetail.CurrentSalaryStructure;
                model.JobTransition_JobDetail.JobTransitionReason = new JobTransitionReason();
                model.JobTransition_JobDetail.JobTransitionsType = new JobTransitionsType();
                model.OfferedSalary = model.JobTransition_JobDetail.CurrentGrossSalary;

                if(model.JobTransition_JobDetail.NewEmploymentType == null)
                    model.JobTransition_JobDetail.NewEmploymentType = new EmploymentType();

                if(model.JobTransition_JobDetail.CurrentEmploymentType == null)
                    model.JobTransition_JobDetail.CurrentEmploymentType = new EmploymentType();

                Login login = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                ViewBag.GrpCompany = new SelectList(await _AccountService.GetGroupCompanyDropDown(login), "Code", "Name");
                ViewBag.Aut = new SelectList(await _dropdownService.GetRequestAuthorityDropDown(), "Code", "Name");
                ViewBag.Alw = new SelectList(await _dropdownService.GetAllowanceWithSalaryStructureDropdown((int)model.JobTransition_JobDetail.CurrentSalaryStructure.Code), "Code", "Name");
                ViewBag.Position = new SelectList(await _dropdownService.GetNewPositionDropdown((int)model.JobTransition_JobDetail.NewDesignation.Code, (int)model.JobTransition_JobDetail.NewLocation.Code, (int)model.JobTransition_JobDetail.NewDepartment.Code), "Code", "Name");

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


        public async Task<IActionResult> GetCompensationANdBenefits(Guid EmployeeGI)
        {
            JobTransition model = new JobTransition();
            Guid HistoryGI = new Guid("00000000-0000-0000-0000-000000000000");
            ViewBag.SlryStruct = new SelectList(await _dropdownService.GeSalaryStructureDropDown(), "Code", "Name");
            model = await _jobTransitionService.GetJobTransitionDetails(EmployeeGI, HistoryGI, "CREATE",null);
            return PartialView("_CompensationBenefits", model);
           
        }
        [HttpPost]
        public async Task<IActionResult> Save(JobTransition model)
        {
            var StorageType = await _commonService.GetFileStorageType();
            try 
            {
                if (model.Confidential != null)
                {
                    if (model.Confidential.FileData != null)
                    {
                        if (StorageType.Data == "BLOB")
                        {

                            var fileobj = await _commonService.UploadIformFileToBLOB(model.Confidential, "Documents");
                            if (fileobj.Success == false)
                            {
                                fileobj.Message = "FILE UPLOAD ERROR";
                                return Json(fileobj);
                            }
                            model.Confidential = fileobj.Data;

                        }
                        else if (StorageType.Data == "IIS")
                        {
                            var fileobj = await _commonService.UploadIformFileToIIS(model.Confidential, "Documents");
                            if (fileobj.Success == false)
                            {
                                fileobj.Message = "FILE UPLOAD ERROR";
                                return Json(fileobj);
                            }
                            model.Confidential = fileobj.Data;

                        }
                        else
                        {
                            var fileobj = await _commonService.UploadIformFileToAWS(model.Confidential, "Documents");
                            if (fileobj.Success == false)
                            {
                                fileobj.Message = "FILE UPLOAD ERROR";
                                return Json(fileobj);
                            }
                            model.Confidential = fileobj.Data;

                        }
                    }
                }
                var response = await _jobTransitionService.SaveJobTransition(model);
            if (response.Success == true)
                    response.Message = "Saved successfully";
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


        public async  Task fillDropdown()
        {
            //ViewBag.TransitionType = new SelectList(await _dropdownService.GetJobTransitionTypeDropdown(), "Code", "Name");
            //ViewBag.TransitionReason = new SelectList(await _dropdownService.GetJobTransitionReasonDropDown(0), "Code", "Name");
            //ViewBag.PRApplicability = new SelectList(await _dropdownService.GetJobTransitionReasonDropDown(0), "Code", "Name");

            ViewBag.Category = new SelectList(await _dropdownService.GetCategoryDropdown(), "Code", "Name");
            ViewBag.Department = new SelectList(await _dropdownService.GetDepartmentDropdown(), "Code", "Name");
            ViewBag.Grade = new SelectList(await _dropdownService.GetGradeDropdown(), "Code", "Name");
            ViewBag.Location = new SelectList(await _dropdownService.GetLocationDropdown(), "Code", "Name");
           
            ViewBag.Designation = new SelectList(await _dropdownService.GetDesignationDropdown(), "Code", "Name");
            ViewBag.PayrollGroup = new SelectList(await _dropdownService.GetPayrollGroupDropdown(), "Code", "Name");
            ViewBag.PFGroup = new SelectList(await _dropdownService.GetPFGroupDropdown(), "Code", "Name");
            ViewBag.ESIGroup = new SelectList(await _dropdownService.GetESIGroupDropDown(), "Code", "Name");
            ViewBag.PTGroup = new SelectList(await _dropdownService.GetPTGroupDropDown(), "Code", "Name");
            ViewBag.Div = new SelectList(await _dropdownService.GetDivisionDropdown(), "Code", "Name");
            ViewBag.Sec = new SelectList(await _dropdownService.GetSectionDropdown(), "Code", "Name");
            ViewBag.EmpType = new SelectList(await _dropdownService.GetEmploymentTypeDropdown(), "Code", "Name");
            ViewBag.WrkLoc = new SelectList(await _dropdownService.GetWorkLocationDropdown(), "Code", "Name");
            ViewBag.WageGrade = new SelectList(await _dropdownService.GetWageGradeDropdown(), "Code", "Name");
            ViewBag.VDAGroup = new SelectList(await _dropdownService.GetVDACombo(), "Code", "Name");


            ViewBag.IncApplicability = new SelectList(new[] { new { Code = "1", Name = "Yes" }, new { Code = "0", Name = "No" } }, "Code", "Name");
            ViewBag.IncentiveCostCenter = new SelectList(await _dropdownService.GetIncentiveCostCenterDropdown(1), "Code", "Name");
            ViewBag.SubDepartment = new SelectList(await _dropdownService.GetSubDepartmentDropDown(), "Code", "Name");
            ViewBag.Function = new SelectList(await _dropdownService.GetFunctionDropDown(), "Code", "Name");
            ViewBag.Position = new SelectList(await _dropdownService.GetNewPositionDropdown(0, 0, 0), "Code", "Name");
            ViewBag.Alw = new SelectList(await _dropdownService.GetAllowanceDropdown(), "Code", "Name");



        }


        public async Task<IActionResult> Approve (Guid EmployeeGI,Guid HistoryGI)
        {
            try { 
            JobTransition model = new JobTransition();
           
            model = await _jobTransitionService.GetJobTransitionDetails(EmployeeGI, HistoryGI, "APPROVE",null);
            model.JobTransition_Approve.ApproveMode = "APPROVE";
            model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);
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
     

        [HttpPost]
        public async Task<IActionResult> Approve(JobTransition model)
        {
            try
            {
                model.JobTransition_Approve.ApproveMode = "APPROVE";
                var response = await _jobTransitionService.ApproveJobTransition(model);
                if (response.Message == "APPROVED")
                    response.Message = "Approved Successfully";
                if (response.Message == "REJECTED")
                    response.Message = "Rejected Successfully";
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
      
        [HttpPost]
        public async Task<IActionResult> Reject(JobTransition model)
        {
            try { 
            model.JobTransition_Approve.ApproveMode = "REJECT";
            var response = await _jobTransitionService.ApproveJobTransition(model);
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
        //[HttpPost]
        //public async Task<IActionResult> ConfirmReject(JobTransition model)
        //{
        //    model.JobTransition_Approve.ApproveMode = "CONFIRM_REJECT";
        //    var response = await _jobTransitionService.ApproveJobTransition(model);
        //    return Json(response);

        //}
        public async Task<IActionResult> GetSalaryStructureAlwDedGrid(int EmpCode, int SalaryStructureCode, decimal GrossSalary, int currentsalstr, int VDAGroup)
        {

            try
            {
                JobTransition model = new JobTransition();
                var ComponsationGrid = await _jobTransitionService.GetSalaryStructureAlwDedGrid(EmpCode, SalaryStructureCode, GrossSalary, VDAGroup);
                ViewBag.SlryStruct = new SelectList(await _dropdownService.GeSalaryStructureDropDown(), "Code", "Name");

                model.JobTransition_FixedAllowance = new List<Allowance>();
                model.JobTransition_FixedDeduction = new List<Deduction>();
                model.JobTransition_FixedProvision = new List<Provisions>();
                model.JobTransition_VaiableAllowance = new List<Allowance>();
                model.JobTransition_VariableDeduction = new List<Deduction>();
                model.JobTransition_VariableProvision = new List<Provisions>();

                model.JobTransition_FixedAllowance = ComponsationGrid.FixedAllowance;
                model.JobTransition_FixedDeduction = ComponsationGrid.FixedDeduction;
                model.JobTransition_FixedProvision = ComponsationGrid.FixedProvisions;
                model.JobTransition_VaiableAllowance = ComponsationGrid.VariableAllowance;
                model.JobTransition_VariableDeduction = ComponsationGrid.VariableDeduction;
                model.JobTransition_VariableProvision = ComponsationGrid.VariableProvisions;
                model.JobTransition_JobDetail = new JobTransition_JobDetail();
                model.JobTransition_JobDetail.NewSalaryStructure = new SalaryStructure();
                model.JobTransition_JobDetail.CurrentSalaryStructure = new SalaryStructure();
                model.JobTransition_JobDetail.NewSalaryStructure.Code = SalaryStructureCode;
                model.JobTransition_JobDetail.CurrentSalaryStructure.Code = currentsalstr;
                model.OfferedSalary = GrossSalary;
                return PartialView("_CompensationBenefits", model);
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

        public async Task<IActionResult> GetSalaryStructureAlwDedGridGeojit(int EmpCode,int SalaryStructureCode, decimal CTC,int currentsalstr,int VDAGroup,
            decimal NewBasic, decimal ABNewValue,int AllowanceCode, decimal AllowanceAmt, int NewDepartment, int NewLocation, decimal CTCOld, decimal OBasic, decimal NBasic, decimal Oabns,decimal Nabns, decimal AlwAmtOld,
           decimal ChgGross, decimal chgBasic, decimal chgAB,int CalcVal)
        {

            try
            {
                JobTransition model = new JobTransition();              
                var ComponsationGrid = await _jobTransitionService.GetSalaryStructureAlwDedGridGeojit(EmpCode,SalaryStructureCode, CTC, VDAGroup,
                    NewBasic,  ABNewValue,  AllowanceCode,  AllowanceAmt,  NewDepartment,  NewLocation);


                //ViewBag.SlryStruct = new SelectList(await _dropdownService.GeSalaryStructureDropDown(), "Code", "Name");

                model.JobTransition_FixedAllowance = new List<Allowance>();
                model.JobTransition_FixedDeduction = new List<Deduction>();
                model.JobTransition_FixedProvision = new List<Provisions>();
                model.JobTransition_VaiableAllowance = new List<Allowance>();
                model.JobTransition_VariableDeduction = new List<Deduction>();
                model.JobTransition_VariableProvision = new List<Provisions>();

                model.JobTransition_FixedAllowance = ComponsationGrid.FixedAllowance;
                model.JobTransition_FixedDeduction = ComponsationGrid.FixedDeduction;
                model.JobTransition_FixedProvision = ComponsationGrid.FixedProvisions;
                model.JobTransition_VaiableAllowance = ComponsationGrid.VariableAllowance;
                model.JobTransition_VariableDeduction = ComponsationGrid.VariableDeduction;
                model.JobTransition_VariableProvision = ComponsationGrid.VariableProvisions;

                model.JobTransition_JobDetail = new JobTransition_JobDetail();
                model.JobTransition_JobDetail.Allowance = new MasterBase();

                model.JobTransition_JobDetail.NewSalaryStructure = new SalaryStructure();
                model.JobTransition_JobDetail.CurrentSalaryStructure = new SalaryStructure();
                model.JobTransition_JobDetail.NewSalaryStructure.Code = SalaryStructureCode;
                model.JobTransition_JobDetail.CurrentSalaryStructure.Code = currentsalstr;
                //model.JobTransition_JobDetail.GrossOldAmt = GrossOld;
                model.JobTransition_JobDetail.oldCTCValue = CTCOld;
                model.JobTransition_JobDetail.NewCTCValue = CTC;
                model.JobTransition_JobDetail.NewCTC = CTC;
                //model.JobTransition_JobDetail.GrossNewValue = GrossSalary;
                //model.JobTransition_JobDetail.BasicOldAmt = OBasic;
                //model.JobTransition_JobDetail.BasicNewValue = NBasic;
                //model.JobTransition_JobDetail.ABOldAmt = Oabns;
                //model.JobTransition_JobDetail.ABNewValue = Nabns;
                ViewBag.Alw = new SelectList(await _dropdownService.GetAllowanceWithSalaryStructureDropdown((int)SalaryStructureCode), "Code", "Name");
                model.JobTransition_JobDetail.Allowance.Code = AllowanceCode;
                model.JobTransition_JobDetail.AlwAmtNew = AllowanceAmt;
                model.JobTransition_JobDetail.AlwAmtOld = AlwAmtOld;
                model.JobTransition_JobDetail.GrossChangeValue = ChgGross;
                //model.JobTransition_JobDetail.BasicChangeValue = chgBasic;
                model.JobTransition_JobDetail.ABChangeValue = chgAB;
                model.OfferedSalary = CTC;
                //model.OfferedSalary = GrossSalary;
                model.JobTransition_JobDetail.GrossCalculator = new MasterBase() ;
                model.JobTransition_JobDetail.GrossCalculator.Code = CalcVal;
                return PartialView("_CompensationBenefits", model);
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
        public async Task<JsonResult> GetTransitionReasonDrpDwn(int TypeCode)
        {
            var result = await _dropdownService.GetJobTransitionReasonDropDown(TypeCode);
            return Json(result);
        }


        [HttpGet]
        public async Task<JsonResult> PR_Appl_Validation(DateTime EffDate, int EmpCode)
        {
            var data = await _jobTransitionService.GetJobTransitionPRApplicableValidation(EffDate.ToString("yyyy-MMM-dd"), EmpCode);
            return Json(data);
        }
        [HttpGet]
        public async Task<JsonResult> GetNoticePeriod(int DesignationCode, Guid? EmployeeGI)
        {
            var data = await _commonService.GetNoticePeriod(DesignationCode, EmployeeGI);
            return Json(data);
        }
        [HttpGet]
        public async Task<JsonResult> GetRenewalDate(Guid EmployeeGI,int CategoryCode,string EffectiveDate)
        {
            var result = await _jobTransitionService.GetRenewalDate(EmployeeGI, CategoryCode,EffectiveDate);
            return Json(result.JobTransition_JobDetail.NewRenewalDate.Value.ToString("dd-MMM-yyyy"));
        }
        public async Task<IActionResult> ExportToExcel(int TabIndex, int SearchId,string TabName)
        {
            try { 
            JobTransition model = new JobTransition();
            var LoginUser = HttpContext.Session.GetString("LoginUser");
            var CompanyName = HttpContext.Session.GetString("CompanyName");
                var User = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                var GroupCompanyName = User.authUser.GroupCompanyName;
                ExcelPassword EP = new ExcelPassword();
            EP = await _commonService.GetExcelPassword("Job Transition (" + TabName + ")", "Excel");
            int slNo = 0;
            if (Global.GlobalFilter == null)
                Global.GlobalFilter = new Filter();
            string FilterValues = await _helpers.GetFilterValues(Global.GlobalFilter);
            model = await _jobTransitionService.GetJobTransitionList(TabIndex, SearchId, Global.GlobalFilter);
            if (FilterValues == "")
            {
                FilterValues = "All";
            }
            string currDateTime = await _helpers.GetCurrentDate();
            

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Job Transition List");
                int count = model.JobTransitionList.Count + 5;


                worksheet.Range("A1:I" + count).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1:I" + count).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1:I" + count).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1:I" + count).Style.Border.RightBorder = XLBorderStyleValues.Thin;
                worksheet.Range("A1:I" + count).Style.Border.LeftBorder = XLBorderStyleValues.Thin;

                var wsReportNameHeaderRange = worksheet.Range(string.Format("A{0}:{1}{0}", 1, Char.ConvertFromUtf32(65 + 8)));
                wsReportNameHeaderRange.Style.Font.Bold = true;
                wsReportNameHeaderRange.Style.Font.FontSize = 15;
                wsReportNameHeaderRange.Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                wsReportNameHeaderRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                wsReportNameHeaderRange.Merge();
                wsReportNameHeaderRange.Style.Fill.BackgroundColor = XLColor.FromArgb(191, 191, 191); ;
                wsReportNameHeaderRange.Value = "Job Transition (" + TabName + ")";

                worksheet.Cell(2, 1).Style.Font.Bold = true;
                worksheet.Cell(2, 1).Value = "Group Company Name";
                worksheet.Cell(2, 2).Value = GroupCompanyName.ToUpper();
                worksheet.Cell(3, 1).Style.Font.Bold = true;
                worksheet.Cell(3, 1).Value = "Login User";
                worksheet.Cell(3, 2).Value = LoginUser;
                worksheet.Cell(3, 3).Style.Font.Bold = true;
                worksheet.Cell(3, 3).Value = "Date & Time";
                worksheet.Cell(3, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(3, 4).Style.DateFormat.Format = "dd-MMM-yyyy   h:mm AM/PM";
                worksheet.Cell(3, 4).Value = currDateTime.ToString();
                worksheet.Cell(3, 5).Style.Font.Bold = true;
                worksheet.Cell(3, 5).Value = "Total Count";
                worksheet.Cell(3, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                worksheet.Cell(3, 6).Value = model.JobTransitionList.Count;
                worksheet.Cell(4, 1).Style.Font.Bold = true;
                worksheet.Cell(4, 1).Value = "Filter Applied";
                worksheet.Range("B4:G4").Merge();
                worksheet.Cell("B4").Value = FilterValues;
                var currentRow = 5;


                #region Header
                worksheet.Cell(currentRow, 1).Value = "Sl.No";
                worksheet.Cell(currentRow, 1).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 2).Value = "ID";
                worksheet.Cell(currentRow, 2).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 3).Value = "EmployeeName";
                worksheet.Cell(currentRow, 3).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 4).Value = "Designation";
                worksheet.Cell(currentRow, 4).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 5).Value = "Department";
                worksheet.Cell(currentRow, 5).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 6).Value = "Location";
                worksheet.Cell(currentRow, 6).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 7).Value = "Type";
                worksheet.Cell(currentRow, 7).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 8).Value = "Order Date";
                worksheet.Cell(currentRow, 8).Style.Font.Bold = true;
                worksheet.Cell(currentRow, 9).Value = "Effective Date";
                worksheet.Cell(currentRow, 9).Style.Font.Bold = true;



                #endregion

                #region Body
                foreach (var item in model.JobTransitionList)
                {
                    currentRow++;
                    slNo = slNo + 1;
                    worksheet.Cell(currentRow, 1).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Range("A5:I5").Style.Fill.BackgroundColor = XLColor.FromArgb(217, 217, 217);
                    worksheet.Cell(currentRow, 1).Value = slNo;
                    worksheet.Cell(currentRow, 2).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 2).Value = item.EmployeeID;
                    worksheet.Cell(currentRow, 3).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 3).Value = item.EmployeeName;
                    worksheet.Cell(currentRow, 4).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 4).Value = item.Designation;
                    worksheet.Cell(currentRow, 5).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 5).Value = item.Department;
                    worksheet.Cell(currentRow, 6).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 6).Value = item.Location;
                    worksheet.Cell(currentRow, 7).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 7).Value = item.TransitionType;
                    worksheet.Cell(currentRow, 8).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 8).Style.DateFormat.Format = "dd-MMM-yyyy";
                    worksheet.Cell(currentRow, 8).Value = item.OrderDate;
                    worksheet.Cell(currentRow, 9).Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left);
                    worksheet.Cell(currentRow, 9).Style.DateFormat.Format = "dd-MMM-yyyy";
                    worksheet.Cell(currentRow, 9).Value = item.EffectiveDate;
                    worksheet.Columns(1, 9).Style.Alignment.WrapText = true;
                    worksheet.Rows().Style.Alignment.WrapText = true;
                }
                #endregion
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
                        "Job Transition List.xlsx"
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
        public async Task<IActionResult> EditList(Filter Filter,int IsFilter)
        {
            JobTransition model = new JobTransition();
            //Filter Filter = new Filter();
            model = await _jobTransitionService.GetJobTransitionEditList(0, 0, Filter);
            await fillfilterDropdown();
            if(IsFilter !=1)
                return View("JobTransitionEdit", model);
            else
                return View("_JobTransitionEditList", model);
        }


        public async Task<IActionResult> Edit(Guid EmployeeGI, Guid HistoryGI)
        {
            try
            {
                JobTransition model = new JobTransition();

                //model.JobTransition_Allowance = new List<JobTransition_Allowance>();
                //model.JobTransition_Deduction = new List<JobTransition_Deduction>();
                //model.JobTransition_StatutoryDeductions = new List<JobTransition_StatutoryDeductions>();

                JobTransition _growth_history = new JobTransition();
                JobTransition _model = new JobTransition();
                //Guid HistoryGI = new Guid("00000000-0000-0000-0000-000000000000");
                await fillDropdown();

                _growth_history = await _jobTransitionService.GetJobTransitionGrowthHistory(EmployeeGI);

                _model = await _jobTransitionService.GetJobTransitionEditDetails(EmployeeGI, HistoryGI, "EDIT");
                model = _model;
                if (_growth_history.GrowthHistory == null)
                    model.GrowthHistory = new List<JobTransition_GrowthHistory>();
                else
                    model.GrowthHistory = _growth_history.GrowthHistory;
                model.EmployeeHeader = await _commonService.GetEmployeeHeader(EmployeeGI);
                model.EmployeeGI = EmployeeGI;
                ViewBag.SlryStruct = new SelectList(await _dropdownService.GeSalaryStructureDropDown(), "Code", "Name");
                model.JobTransition_JobDetail.NewSalaryStructure = new SalaryStructure();
                model.JobTransition_JobDetail.NewSalaryStructure = model.JobTransition_JobDetail.CurrentSalaryStructure;
                //model.JobTransition_JobDetail.JobTransitionReason = new JobTransitionReason();
                //model.JobTransition_JobDetail.JobTransitionsType = new JobTransitionsType();
                model.OfferedSalary = model.JobTransition_JobDetail.NewGrossSalary;
                if (model.JobTransition_JobDetail.NewEmploymentType == null)
                    model.JobTransition_JobDetail.NewEmploymentType = new EmploymentType();
                if (model.JobTransition_JobDetail.CurrentEmploymentType == null)
                    model.JobTransition_JobDetail.CurrentEmploymentType = new EmploymentType();

                Login login = JsonConvert.DeserializeObject<Login>(HttpContext.Session.GetString("User"));
                ViewBag.GrpCompany = new SelectList(await _AccountService.GetGroupCompanyDropDown(login), "Code", "Name");
                ViewBag.Aut = new SelectList(await _dropdownService.GetRequestAuthorityDropDown(), "Code", "Name");
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
        public async Task<JsonResult> GetReplacementEmployee(int Designation, int Location, int Department,string Type)
        {
            var Position = new SelectList(await _dropdownService.GetReplacementEmployeeDropdown(Designation, Location, Department, Type), "Code", "Name");
            return Json(Position);
        }
        [HttpGet]
        public async Task<IActionResult> GetNewSubordinates(int positionCode, int employeeCode)
        {
            JobTransition model = new JobTransition();
             var Subordinates = await _jobTransitionService.GetNewSubordinates(positionCode, employeeCode);

            model.Subordinates_C = new List<JobTransition_Subordinates>();
            model.Subordinates_N = new List<JobTransition_Subordinates>();

            model.Subordinates_C = Subordinates.Subordinates_C;
            model.Subordinates_N= Subordinates.Subordinates_N;

            return PartialView("_Subordinates", model);
        }
        [HttpGet]
        public async Task<JsonResult> onWageGradeChange(int WageGradeCode)
        {
            var SalaryStructure = new SelectList(await _dropdownService.GetWageGradeVsSalaryBand(WageGradeCode), "Code", "Name");
            return Json(SalaryStructure);
        }
        [HttpGet]
        public async Task<IActionResult> GetJobTransitionReason(string ChangingParameters,int CurrentWageGrade, int NewWageGrade)
        {
            JobTransition model = new JobTransition();
            var Summary = await _jobTransitionService.GetJobTransitionReason(ChangingParameters,CurrentWageGrade,NewWageGrade);
            model.JobTransition_Summary = Summary.JobTransition_Summary;
            return PartialView("_JobTransitionReasons", model);
        }
        [HttpGet]
        public async Task<IActionResult> GetJobTransitionSummary(string ChangingParameters, int CurrentWageGrade, int NewWageGrade)
        {
            JobTransition model = new JobTransition();
            var Summary = await _jobTransitionService.GetJobTransitionSummary(ChangingParameters, CurrentWageGrade, NewWageGrade);
            model.Summary = new List<JobTransition_Summary>();
            model.Summary = Summary.Summary;
            return PartialView("_Summary", model);
        }
        [HttpPost]
        public async Task<IActionResult> SalarySummary(JobTransition model)
        {
            try
            {
                model = await _jobTransitionService.SalaryDetails_JobTransition(model);

                return PartialView("_SalaryDetails", model);
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
