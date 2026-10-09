using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared;
using Hrms.Lite.Shared.Account;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Hrms.Lite.Shared.TimeOffice.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Hrms.Lite.Shared.TimeOffice;
using Microsoft.AspNetCore.Mvc;
using Hrms.Lite.Infrastructure.Helpers;

namespace Hrms.Lite.Services.Services.General
{
  public  class CommonService: ICommonService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        private readonly IDropdownService _dropdownService;
        private readonly IBlob _blob;
        public CommonService(IServiceBase httpService,IDropdownService dropdownService, IBlob blob)
        {
            _baseURL = "Common";
            _httpService = httpService;
            _dropdownService = dropdownService;
            _blob = blob;
        }

    public    async Task<ResponseEntity> CheckEmptyDropDown(string Mode)
        {
            return await _httpService.GetAsync<ResponseEntity>($"{_baseURL}/CheckEmptyDropDown?Mode={Mode}");
        }
        public async Task<SoftwareURLs> GetSoftwareURLs()
        {
            return await _httpService.GetAsync<SoftwareURLs>($"{_baseURL}/GetSoftwareURLs");
        }

        public async Task<ResponseEntity<File>> UploadIformFileToBLOBElearn(File input, string SubFolder)
        {
            var content = new MultipartFormDataContent();
            content.Add(new StreamContent(input.FileData.OpenReadStream())
            {
                Headers =
                    {
                        ContentLength = input.FileData.Length,
                        ContentType = new MediaTypeHeaderValue(input.FileData.ContentType)
                    }
            }, "FileData", input.FileData.FileName);

            return await _httpService.PostFormDataAsync<ResponseEntity<File>>($"{_baseURL}/UploadIformFileToBLOBElearn?SubFolder={SubFolder}", content);
        }
        public async Task<ResponseEntity<File>> UploadIformFileToBLOBElearnUI(File input, string SubFolder)
        {
            var content = new MultipartFormDataContent();
            content.Add(new StreamContent(input.FileData.OpenReadStream())
            {
                Headers =
                    {
                        ContentLength = input.FileData.Length,
                        ContentType = new MediaTypeHeaderValue(input.FileData.ContentType)
                    }
            }, "FileData", input.FileData.FileName);

            try
            {

                string errorMsg = "";
                if (FileValidationHelper.ValidateFile(input.FileData, out errorMsg, 31457280))
                {

                    var response = await GetFileStoragePath("BLOB", SubFolder);
                    var fileobj = await _blob.UploadToAzureAsync(input, response.PhysicalPath);
                    fileobj.FileData = null;
                    return new ResponseEntity<File>("SUCCESS", Convert.ToBoolean(1), fileobj);
                }
                else
                {
                    input.FileData = null;
                    return new ResponseEntity<File>(errorMsg, Convert.ToBoolean(0), input);
                }
            }

            catch (Exception ex)
            {
                string ErrorMessage = ex.Message.ToString();
                if (ex.InnerException != null)
                {
                    if (ex.InnerException.Message != null)
                    {
                        ErrorMessage = ErrorMessage + ex.InnerException.Message.ToString();
                    }
                }

                return new ResponseEntity<File>(ErrorMessage, Convert.ToBoolean(0), null);
            }

        }
        

        public async Task<EmployeeHeader> GetEmployeeCard(int EmpCode)
        {
            return await _httpService.GetAsync<EmployeeHeader>($"{_baseURL}/GetEmployeeCard?EmpCode={EmpCode}");
        }
        public async Task<EmployeeHeader> GetEmployeeHeader(Guid EmployeeGI)
        {
            return await _httpService.GetAsync<EmployeeHeader>($"{_baseURL}/GetEmployeeHeader?EmployeeGI={EmployeeGI}");
        }
        public async Task<AddOnClassification> GetAddOnClassificationApplicability()
        {
            return await _httpService.GetAsync<AddOnClassification>($"{_baseURL}/AddOnClassification_Applicability");
        }
        public async Task<List<Dropdown>> GetDurationWiseFilterSlabDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetDurationWiseFilterDropDown");
        }

        public async Task<ResponseEntity<string>> GetCurrentLeavePeriod()
        {
            return await _httpService.GetAsync <ResponseEntity<string>>($"{_baseURL}/GetCurrentLeavePeriod");
        }
        
        public async Task<ResponseEntity<Int32>> GetCurrentLFinancialYear()
        {
            return await _httpService.GetAsync<ResponseEntity<Int32>>($"{_baseURL}/GetCurrentLFinancialYear");
        }
        public async Task<ResponseEntity> GetLeaveValidation(LeaveApplication model,Guid EmployeeGI)
        {
            return await _httpService.PostAsync<ResponseEntity, LeaveApplication>($"{_baseURL}/GetLeaveValidation?EmployeeGI={EmployeeGI}", model);
        }
        public async Task<ResponseEntity<Int32>> GetDisplayOrder(string master)
        {
            return await _httpService.GetAsync<ResponseEntity<Int32>>($"{_baseURL}/GetDisplayOrder?Master={master}");
        }
        public async Task<ResponseEntity> GetDailyAttendanceBatchUpdateLeaveValidation(int AttendanceStatusCode)
        {
            return await _httpService.GetAsync<ResponseEntity>($"{_baseURL}/GetDailyAttendanceBatchUpdateLeaveValidation?AttendanceStatusCode={AttendanceStatusCode}");
        }

        async Task<List<EmployeeAuthority>> ICommonService.GetEmployeeAuthorityDetails(Guid EmployeeGI)
        {
            return await _httpService.GetAsync<List<EmployeeAuthority>>($"{_baseURL}/GetEmployeeAuthorityDetails?EmployeeGI="+ EmployeeGI);
        }
        public async Task<ResponseEntity<string>> GetReferenceNumber(string TransactionName)
        {
            return await _httpService.GetAsync<ResponseEntity<string>>($"{_baseURL}/GetReferenceNumber?TransactionName={TransactionName}");
        }
        public async Task<ResponseEntity<Int32>> GetCurrentMonthDropdown()
        {
            return await _httpService.GetAsync<ResponseEntity<Int32>>($"{_baseURL}/GetCurrentMonthDropdown");
        }
        public async Task<DateAndMonth> GetAttendanceProcessedLastMonth()
        {
            return await _httpService.GetAsync<DateAndMonth>($"{_baseURL}/GetAttendanceProcessedLastMonth");
        }
        public async Task<DateAndMonth> GetSelectedMonthDates(int MonthCode)
        {
            return await _httpService.GetAsync<DateAndMonth>($"{_baseURL}/GetSelectedMonthDates?MonthCode={MonthCode}");
        }
        public async Task<ExcelPassword> GetExcelPassword(string ReportName,string Type)
        {
            return await _httpService.GetAsync<ExcelPassword>($"{_baseURL}/GetExcelPassword?ReportName={ReportName}&Type={Type}");
        }
        public async Task<DateAndMonth> GetCurrentMonthDetails()
        {
            return await _httpService.GetAsync<DateAndMonth>($"{_baseURL}/GetCurrentMonthDetails");
        }
        public async Task<ResumeDataBank> GetMappedResumeDetails(Guid VacancyGI)
        {
            return await _httpService.GetAsync<ResumeDataBank>($"MRFVsResume/GetMappedResumeDetails?VacancyGI={VacancyGI}");
        }
        public async Task<ResumeDataBank> GetResumeDataBankDetails(Guid ResumeDataBankGI)
        {
            return await _httpService.GetAsync<ResumeDataBank>($"ResumeDataBank/GetResumeDataBankDetails?ResumeDataBankGI={ResumeDataBankGI}");
        }
        public async Task<UserRole> UserRole_Actions()
        {
            return await _httpService.GetAsync<UserRole>($"{_baseURL}/UserRole_Actions");
        }
        public async Task<PreEnrollment> GetPFMaxLimit()
        {
            return await _httpService.GetAsync<PreEnrollment>($"{_baseURL}/GetPFMaxLimit");
        }

       public async Task<AttendanceStatus> GetAttendanceStatusType(int AttendanceStatusCode)
        {
            return await _httpService.GetAsync<AttendanceStatus>($"{_baseURL}/GetAttendanceStatusType?AttendanceStatusCode={AttendanceStatusCode}");
        }
        public async Task<AttendanceStatus> GetAttendanceStatusType_FH_SH(int FH,int SH)
        {
            return await _httpService.GetAsync<AttendanceStatus>($"{_baseURL}/GetAttendanceStatusType_FH_SH?FH={FH}&SH{SH}");
        }
        public  async Task<ResponseEntity> GetSalaryPeriodLockDatewiseValidation(string SeletedDate,string form,Guid? EmployeeGI)
        {
            return await _httpService.GetAsync<ResponseEntity>($"{_baseURL}/GetSalaryPeriodLockDatewiseValidation?SeletedDate={SeletedDate}&form={form}&EmployeeGI={EmployeeGI}");
        }
        public async Task<PreEnrollment> GetMandatoryFiledValidation(string Type)
        {
            return await _httpService.GetAsync<PreEnrollment>($"{_baseURL}/GetMandatoryFiledValidation?Type={Type}");
        }

        async Task<ResponseEntity> ICommonService.GetOnboardingActiveVacanciesValidation(int VacancyManagementCode)
        {
            return await _httpService.GetAsync<ResponseEntity>($"{_baseURL}/GetOnboardingActiveVacanciesValidation?VacancyManagementCode={VacancyManagementCode}");
        }
        public async Task<Url> GetUrl()
        {
            return await _httpService.GetAsync<Url>($"{_baseURL}/GetAPIUrl");
        }
        
        public async Task<UserRole> GetTabApplicability(string MenuName)
        {
            return await _httpService.GetAsync<UserRole>($"{_baseURL}/GetTabApplicability?MenuName={MenuName}");
        }
      public async Task<ResponseEntity> Save_SoftwareLogo(string Type, CustomSoftware login)
        {
            return await _httpService.PostAsync<ResponseEntity, CustomSoftware>($"{_baseURL}/Save_Softwarelogo?Type={Type}", login);
        }

        public async Task<ResponseEntity> Save_JPG_SoftwareLogo(string Type, CustomSoftware login)
        {
            return await _httpService.PostAsync<ResponseEntity, CustomSoftware>($"{_baseURL}/Save_JPG_SoftwareLogo?Type={Type}", login);
        }

        public async Task<Url> GetLettersFileName(int Type)
        {
            return await _httpService.GetAsync<Url>($"{_baseURL}/GetLettersFileName?Type={Type}");
        }


        public async Task<Url> GetOfferLetter(string Type)
        {
            return await _httpService.GetAsync<Url>($"{_baseURL}/GetOfferLetter?Type={Type}");
        }

        public async Task<ResponseEntity<Int32>> GetCurrentAssessmentYear()
        {
            return await _httpService.GetAsync<ResponseEntity<Int32>>($"{_baseURL}/GetCurrentAssessmentYear");
        }

      public  async Task<int> GetNoticePeriod(int DesignationCode, Guid? EmployeeGI)
        {
            return await _httpService.GetAsync<Int32>($"{_baseURL}/GetNoticePeriod?DesignationCode={DesignationCode}&EmployeeGI={EmployeeGI}");
        }
        public async Task<int> GetMaternityLeaveDays()
        {
            return await _httpService.GetAsync<Int32>($"{_baseURL}/GetMaternityLeaveDays");
        }

      

        public async Task<ResponseEntity<Int32>> GetLetterFormatCode(int DocTypeCode)
        {
            return await _httpService.GetAsync<ResponseEntity<Int32>>($"{_baseURL}/GetFormatCode?DocTypeCode={DocTypeCode}");
        }
        public async Task<EmployeeHeader> GetAadharDuplication(string DocNmber, int TypeCode)
        {
            return await _httpService.GetAsync<EmployeeHeader>($"{_baseURL}/GetAadharDuplication?AadharNo={DocNmber}&TypeCode={TypeCode}");
        }

       public async Task<ResponseEntity<string>> GetFileStorageType()
        {
            return await _httpService.GetAsync <ResponseEntity<string> > ($"{_baseURL}/GetFileStorageType");

        }

       public async Task<FileStoragePath> GetFileStoragePath(string storageType, string Subfoldername)
        {
            return await _httpService.GetAsync<FileStoragePath>($"{_baseURL}/GetFileStoragePath?storageType={storageType}&Subfoldername={Subfoldername}");

        }
        public async  Task<ResponseEntity<File>> UploadIformFileToBLOB(File input, string SubFolder)
        {
            var content = new MultipartFormDataContent();
            content.Add(new StreamContent(input.FileData.OpenReadStream())
            {
                Headers =
                    {
                        ContentLength = input.FileData.Length,
                        ContentType = new MediaTypeHeaderValue(input.FileData.ContentType)
                    }
            }, "FileData", input.FileData.FileName);
            try
            {

                var response = await GetFileStoragePath("BLOB", SubFolder );
                var fileobj = await _blob.UploadToAzureAsync(input, response.PhysicalPath);
                fileobj.FileData = null;
                return new ResponseEntity<File>("SUCCESS", Convert.ToBoolean(1), fileobj);
            }

            catch (Exception ex)
            {
                string ErrorMessage = ex.Message.ToString();
                if (ex.InnerException != null)
                {
                    if (ex.InnerException.Message != null)
                    {
                        ErrorMessage = ErrorMessage + ex.InnerException.Message.ToString();
                    }
                }

                return new ResponseEntity<File>(ErrorMessage, Convert.ToBoolean(0), null);
            }
        }
        public async Task<ResponseEntity<File>> UploadByteArrayToBLOB(File File, string SubFolder)
        {
            return await _httpService.PostAsync<ResponseEntity<File>, File>($"{_baseURL}/UploadByteArrayToBLOB?SubFolder={SubFolder}", File);

        }
        public async Task<ResponseEntity<File>> UploadByteArrayToIIS(File File, string SubFolder)
        {
            return await _httpService.PostAsync<ResponseEntity<File>, File>($"{_baseURL}/UploadByteArrayToIIS?SubFolder={SubFolder}", File);

        }
        public async Task<ResponseEntity<File>> UploadByteArrayToAWS(File File, string SubFolder)
        {
            return await _httpService.PostAsync<ResponseEntity<File>, File>($"{_baseURL}/UploadByteArrayToAWS?SubFolder={SubFolder}", File);

        }
        public async Task<ResponseEntity<File>> UploadIformFileToIIS(File input, string SubFolder)
        {
            var content = new MultipartFormDataContent();
            content.Add(new StreamContent(input.FileData.OpenReadStream())
            {
                Headers =
                    {
                        ContentLength = input.FileData.Length,
                        ContentType = new MediaTypeHeaderValue(input.FileData.ContentType)
                    }
            }, "FileData", input.FileData.FileName);       
          
            return await  _httpService.PostFormDataAsync<ResponseEntity<File>>($"{_baseURL}/UploadIformFileToIIS?SubFolder={SubFolder}", content);
        }

        public async Task<ResponseEntity<File>> UploadIformFileToAWS(File input, string SubFolder)
        {
            var content = new MultipartFormDataContent();
            content.Add(new StreamContent(input.FileData.OpenReadStream())
            {
                Headers =
                    {
                        ContentLength = input.FileData.Length,
                        ContentType = new MediaTypeHeaderValue(input.FileData.ContentType)
                    }
            }, "FileData", input.FileData.FileName);

          
            return await _httpService.PostFormDataAsync<ResponseEntity<File>>($"{_baseURL}/UploadIformFileToAWS?SubFolder={SubFolder}", content);
        }

        public async Task<Filter> GetFilterDropdowns()
        {
            Filter filter = new Filter();
          
            


            #region Basic Filter

            filter.Basic_Filter = new Basic_Filter();
            filter.Basic_Filter.Category = new Category();
            filter.Basic_Filter.Grade = new Grade();
            filter.Basic_Filter.Designation = new Designation();
            filter.Basic_Filter.Department = new Department();
            filter.Basic_Filter.Location = new Location();
            filter.Basic_Filter.PayrollGroup = new PayrollGroup();
            filter.Basic_Filter.State = new State();
            filter.Basic_Filter.Region = new Region();
            filter.Basic_Filter.PFGroup = new PFGroup();
            filter.Basic_Filter.ESIGroup = new ESIGroup();

            filter.Basic_Filter.Category.DropdownData = new SelectList(await _dropdownService.GetCategoryDropdown(), "Code", "Name");
            filter.Basic_Filter.Grade.DropdownData = new SelectList(await _dropdownService.GetGradeDropdown(), "Code", "Name");
            filter.Basic_Filter.Designation.DropdownData = new SelectList(await _dropdownService.GetDesignationDropdown(), "Code", "Name");
            filter.Basic_Filter.Department.DropdownData = new SelectList(await _dropdownService.GetDepartmentDropdown(), "Code", "Name");
            filter.Basic_Filter.Location.DropdownData = new SelectList(await _dropdownService.GetLocationDropdown(), "Code", "Name");
            filter.Basic_Filter.PayrollGroup.DropdownData = new SelectList(await _dropdownService.GetPayrollGroupDropdown(), "Code", "Name");
            filter.Basic_Filter.Region.DropdownData = new SelectList(await _dropdownService.GetRegionDropDown(), "Code", "Name");
            filter.Basic_Filter.State.DropdownData = new SelectList(await _dropdownService.GetStateDropDown(), "Code", "Name");
            filter.Basic_Filter.PFGroup.DropdownData = new SelectList(await _dropdownService.GetPFGroupDropdown(), "Code", "Name");
            filter.Basic_Filter.ESIGroup.DropdownData = new SelectList(await _dropdownService.GetESIGroupDropDown(), "Code", "Name");

            #endregion

            #region Optional Filter

            filter.Optional_Filter = new Optional_Filter();
                filter.Optional_Filter.Gender = new Gender();
                filter.Optional_Filter.Division = new Division();
                filter.Optional_Filter.Section = new Section();
                filter.Optional_Filter.EmploymentType = new EmploymentType();
                filter.Optional_Filter.EmploymentStatus = new EmploymentStatus();
                filter.Optional_Filter.WorkLocation = new WorkLocation();
                filter.Optional_Filter.WageGradeMaster = new WageGradeMaster();

                filter.Optional_Filter.Gender.DropdownData = new SelectList(await _dropdownService.GetGenderDropDown(), "Code", "Name");
                filter.Optional_Filter.Division.DropdownData = new SelectList(await _dropdownService.GetDivisionDropdown(), "Code", "Name");
                filter.Optional_Filter.Section.DropdownData = new SelectList(await _dropdownService.GetSectionDropdown(), "Code", "Name");
                filter.Optional_Filter.EmploymentType.DropdownData = new SelectList(await _dropdownService.GetEmploymentTypeDropdown(), "Code", "Name");
                filter.Optional_Filter.EmploymentStatus.DropdownData = new SelectList(await _dropdownService.GetEmploymentStatusDropdown(), "Code", "Name");
                filter.Optional_Filter.WorkLocation.DropdownData = new SelectList(await _dropdownService.GetWorkLocationDropdown(), "Code", "Name");
                filter.Optional_Filter.WageGradeMaster.DropdownData = new SelectList(await _dropdownService.GetWageGradeDropdown(), "Code", "Name");


            #endregion

            #region Advanced Filter
            filter.Advance_Filter = new Advance_Filter();
            filter.Advance_Filter.Operators = new SelectList("","Code","Name");
            filter.Advance_Filter.DateType = new SelectList("", "Code", "Name");
            filter.Advance_Filter.Operators = new SelectList(await _dropdownService.GetOperators(), "Code", "Name");
            filter.Advance_Filter.DateType = new SelectList(await _dropdownService.GetDateFilter(), "Code", "Name");
            #endregion
            return filter;


            
          


        }
        public async Task<Filter> GetFilterDropdownsAndBindData(Filter filterdata)
        {
            Filter filter = new Filter();




            #region Basic Filter

            filter.Basic_Filter = new Basic_Filter();
            filter.Basic_Filter.Category = new Category();
            filter.Basic_Filter.Grade = new Grade();
            filter.Basic_Filter.Designation = new Designation();
            filter.Basic_Filter.Department = new Department();
            filter.Basic_Filter.Location = new Location();
            filter.Basic_Filter.PayrollGroup = new PayrollGroup();
            filter.Basic_Filter.State = new State();
            filter.Basic_Filter.Region = new Region();
            if (filterdata.Basic_Filter != null)
            { 
                if(filterdata.Basic_Filter.Category !=null)
                    filter.Basic_Filter.Category = filterdata.Basic_Filter.Category;
                if (filterdata.Basic_Filter.Grade != null)
                    filter.Basic_Filter.Grade = filterdata.Basic_Filter.Grade;
                if (filterdata.Basic_Filter.Designation != null)
                    filter.Basic_Filter.Designation = filterdata.Basic_Filter.Designation;
                if (filterdata.Basic_Filter.Department != null)
                    filter.Basic_Filter.Department = filterdata.Basic_Filter.Department;
                if (filterdata.Basic_Filter.Location != null)
                    filter.Basic_Filter.Location = filterdata.Basic_Filter.Location;
                if (filterdata.Basic_Filter.PayrollGroup != null)
                    filter.Basic_Filter.PayrollGroup = filterdata.Basic_Filter.PayrollGroup;
                if (filterdata.Basic_Filter.Region != null)
                    filter.Basic_Filter.Region = filterdata.Basic_Filter.Region;
                if (filterdata.Basic_Filter.State != null)
                    filter.Basic_Filter.State = filterdata.Basic_Filter.State;
            }

            filter.Basic_Filter.Category.DropdownData = new SelectList(await _dropdownService.GetCategoryDropdown(), "Code", "Name");
            filter.Basic_Filter.Grade.DropdownData = new SelectList(await _dropdownService.GetGradeDropdown(), "Code", "Name");
            filter.Basic_Filter.Designation.DropdownData = new SelectList(await _dropdownService.GetDesignationDropdown(), "Code", "Name");
            filter.Basic_Filter.Department.DropdownData = new SelectList(await _dropdownService.GetDepartmentDropdown(), "Code", "Name");
            filter.Basic_Filter.Location.DropdownData = new SelectList(await _dropdownService.GetLocationDropdown(), "Code", "Name");
            filter.Basic_Filter.PayrollGroup.DropdownData = new SelectList(await _dropdownService.GetPayrollGroupDropdown(), "Code", "Name");
            filter.Basic_Filter.Region.DropdownData = new SelectList(await _dropdownService.GetRegionDropDown(), "Code", "Name");
            filter.Basic_Filter.State.DropdownData = new SelectList(await _dropdownService.GetStateDropDown(), "Code", "Name");

            #endregion

            #region Optional Filter

            filter.Optional_Filter = new Optional_Filter();
            filter.Optional_Filter.Gender = new Gender();
            filter.Optional_Filter.Division = new Division();
            filter.Optional_Filter.Section = new Section();
            filter.Optional_Filter.EmploymentType = new EmploymentType();
            filter.Optional_Filter.EmploymentStatus = new EmploymentStatus();
            filter.Optional_Filter.WorkLocation = new WorkLocation();
            filter.Optional_Filter.WageGradeMaster = new WageGradeMaster();

            if(filterdata.Optional_Filter != null)
            {
                if(filterdata.Optional_Filter.Gender != null)
                    filter.Optional_Filter.Gender = filterdata.Optional_Filter.Gender;
                if (filterdata.Optional_Filter.Division != null)
                    filter.Optional_Filter.Division = filterdata.Optional_Filter.Division;
                if (filterdata.Optional_Filter.Section != null)
                    filter.Optional_Filter.Section = filterdata.Optional_Filter.Section;
                if (filterdata.Optional_Filter.EmploymentType != null)
                    filter.Optional_Filter.EmploymentType = filterdata.Optional_Filter.EmploymentType;
                if (filterdata.Optional_Filter.EmploymentStatus != null)
                    filter.Optional_Filter.EmploymentStatus = filterdata.Optional_Filter.EmploymentStatus;
                if (filterdata.Optional_Filter.WorkLocation != null)
                    filter.Optional_Filter.WorkLocation = filterdata.Optional_Filter.WorkLocation;
                if (filterdata.Optional_Filter.WageGradeMaster != null)
                    filter.Optional_Filter.WageGradeMaster = filterdata.Optional_Filter.WageGradeMaster;
            }

            filter.Optional_Filter.Gender.DropdownData = new SelectList(await _dropdownService.GetGenderDropDown(), "Code", "Name");
            filter.Optional_Filter.Division.DropdownData = new SelectList(await _dropdownService.GetDivisionDropdown(), "Code", "Name");
            filter.Optional_Filter.Section.DropdownData = new SelectList(await _dropdownService.GetSectionDropdown(), "Code", "Name");
            filter.Optional_Filter.EmploymentType.DropdownData = new SelectList(await _dropdownService.GetEmploymentTypeDropdown(), "Code", "Name");
            filter.Optional_Filter.EmploymentStatus.DropdownData = new SelectList(await _dropdownService.GetEmploymentStatusDropdown(), "Code", "Name");
            filter.Optional_Filter.WorkLocation.DropdownData = new SelectList(await _dropdownService.GetWorkLocationDropdown(), "Code", "Name");
            filter.Optional_Filter.WageGradeMaster.DropdownData = new SelectList(await _dropdownService.GetWageGradeDropdown(), "Code", "Name");


            #endregion

            #region Advanced Filter
            filter.Advance_Filter = new Advance_Filter();
            filter.Advance_Filter.Operators = new SelectList("", "Code", "Name");
            filter.Advance_Filter.DateType = new SelectList("", "Code", "Name");
            filter.Advance_Filter.Operators = new SelectList(await _dropdownService.GetOperators(), "Code", "Name");
            filter.Advance_Filter.DateType = new SelectList(await _dropdownService.GetDateFilter(), "Code", "Name");
            #endregion
            return filter;






        }
        public async Task<FilterMultiSelect> GetMultiSelectFilterDropdowns()
        {
            FilterMultiSelect filter = new FilterMultiSelect();
          
            


            #region Basic Filter

            filter.Basic_Filter = new Basic_MultiFilter();


            filter.Basic_Filter.CategoryDropdownData = new SelectList(await _dropdownService.GetCategoryDropdown(), "Code", "Name");
            filter.Basic_Filter.GradeDropdownData = new SelectList(await _dropdownService.GetGradeDropdown(), "Code", "Name");
            filter.Basic_Filter.DesignationDropdownData = new SelectList(await _dropdownService.GetDesignationDropdown(), "Code", "Name");
            filter.Basic_Filter.DepartmentDropdownData = new SelectList(await _dropdownService.GetDepartmentDropdown(), "Code", "Name");
            filter.Basic_Filter.LocationDropdownData = new SelectList(await _dropdownService.GetLocationDropdown(), "Code", "Name");
            filter.Basic_Filter.PayrollGroupDropdownData = new SelectList(await _dropdownService.GetPayrollGroupDropdown(), "Code", "Name");
            filter.Basic_Filter.RegionDropdownData = new SelectList(await _dropdownService.GetRegionDropDown(), "Code", "Name");
            filter.Basic_Filter.StateDropdownData = new SelectList(await _dropdownService.GetStateDropDown(), "Code", "Name");

            #endregion

            #region Optional Filter

                filter.Optional_Filter = new Optional_MultiFilter();                

                filter.Optional_Filter.GenderDropdownData = new SelectList(await _dropdownService.GetGenderDropDown(), "Code", "Name");
                filter.Optional_Filter.DivisionDropdownData = new SelectList(await _dropdownService.GetDivisionDropdown(), "Code", "Name");
                filter.Optional_Filter.SectionDropdownData = new SelectList(await _dropdownService.GetSectionDropdown(), "Code", "Name");
                filter.Optional_Filter.EmploymentTypeDropdownData = new SelectList(await _dropdownService.GetEmploymentTypeDropdown(), "Code", "Name");
                filter.Optional_Filter.EmploymentStatusDropdownData = new SelectList(await _dropdownService.GetEmploymentStatusDropdown(), "Code", "Name");
                filter.Optional_Filter.WorkLocationDropdownData = new SelectList(await _dropdownService.GetWorkLocationDropdown(), "Code", "Name");
                filter.Optional_Filter.WageGradeMasterDropdownData = new SelectList(await _dropdownService.GetWageGradeDropdown(), "Code", "Name");


            #endregion

            #region Advanced Filter
            filter.Advance_Filter = new Advance_MultiFilter();
            filter.Advance_Filter.Operators = new SelectList("","Code","Name");
            filter.Advance_Filter.DateType = new SelectList("", "Code", "Name");
            filter.Advance_Filter.Operators = new SelectList(await _dropdownService.GetOperators(), "Code", "Name");
            filter.Advance_Filter.DateType = new SelectList(await _dropdownService.GetDateFilter(), "Code", "Name");
            #endregion
            return filter;


            
          


        }

        #region Multiple File Storage
        public async Task<ResponseEntity<File>> UploadMultipleFileToBLOB(IFormFile input, string SubFolder)
        {
            var content = new MultipartFormDataContent();
            content.Add(new StreamContent(input.OpenReadStream())
            {
                Headers =
                    {
                        ContentLength = input.Length,
                        ContentType = new MediaTypeHeaderValue(input.ContentType)
                    }
            }, "FileData", input.FileName);

            return await _httpService.PostFormDataAsync<ResponseEntity<File>>($"{_baseURL}/UploadIformFileToBLOB?SubFolder={SubFolder}", content);
        }
        public async Task<ResponseEntity<File>> UploadMultipleFileToIIS(IFormFile input, string SubFolder)
        {
            var content = new MultipartFormDataContent();
            content.Add(new StreamContent(input.OpenReadStream())
            {
                Headers =
                    {
                        ContentLength = input.Length,
                        ContentType = new MediaTypeHeaderValue(input.ContentType)
                    }
            }, "FileData", input.FileName);

            return await _httpService.PostFormDataAsync<ResponseEntity<File>>($"{_baseURL}/UploadIformFileToIIS?SubFolder={SubFolder}", content);
        }

        public async Task<ResponseEntity<File>> UploadMultipleFileToAWS(IFormFile input, string SubFolder)
        {
            var content = new MultipartFormDataContent();
            content.Add(new StreamContent(input.OpenReadStream())
            {
                Headers =
                    {
                        ContentLength = input.Length,
                        ContentType = new MediaTypeHeaderValue(input.ContentType)
                    }
            }, "FileData", input.FileName);


            return await _httpService.PostFormDataAsync<ResponseEntity<File>>($"{_baseURL}/UploadIformFileToAWS?SubFolder={SubFolder}", content);
        }

        #endregion
        public async Task<Bank> GetDigitsInAccountNumber(int BankCode)
        {
            return await _httpService.GetAsync<Bank>($"{_baseURL}/GetDigitsInAccountNumber?BankCode={BankCode}");
        }
        public async Task<DailyAttendanceEditDetails> CheckCoffBalance(Guid EmployeeGI, string CoffEntitleDate)
        {
            return await _httpService.GetAsync<DailyAttendanceEditDetails>($"{_baseURL}/CheckCoffBalance?EmployeeGI={EmployeeGI}&CoffEntitleDate={CoffEntitleDate}");
        }
        public async Task<ResponseEntity<string>> GetBankCode()
        {
            return await _httpService.GetAsync<ResponseEntity<string>>($"{_baseURL}/GetBankCode");

        }
        public async Task<File> GetEmpThumbnail(Guid EmployeeGI )
        {
            return await _httpService.GetAsync<File>($"{_baseURL}/GetEmpThumbnail?EmployeeGI={ EmployeeGI}");

        }
        public async Task<File> GetEmpThumbnail_Approve(Guid EmployeeGI)
        {
            return await _httpService.GetAsync<File>($"{_baseURL}/GetEmpThumbnail_Approve?EmployeeGI={ EmployeeGI}");

        }
        public async Task<File> GetEmpThumbnail_OnBoarding(Guid GI)
        {
            return await _httpService.GetAsync<File>($"{_baseURL}/GetEmpThumbnail_OnBoarding?GI={ GI}");

        }
        public async Task<int> GetBankRepostCount(int? MonthCode, String Type,int ReportFormat)
        {
            return await _httpService.GetAsync<Int32>($"{_baseURL}/GetBankRepostCount?MonthCode={MonthCode}&Type={Type}&ReportFormat={ReportFormat}");
        }
    }

}

   