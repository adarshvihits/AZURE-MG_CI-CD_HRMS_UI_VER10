
using Hrms.Lite.Shared.DataBank.DTO;

using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using Hrms.Lite.Shared.Account;
using System.Threading.Tasks;
using Hrms.Lite.Shared.TimeOffice.Generic;
using Hrms.Lite.Shared;
using Hrms.Lite.Shared.General;
using Microsoft.AspNetCore.Http;
using Hrms.Lite.Shared.TimeOffice;
using Microsoft.AspNetCore.Mvc;

namespace Hrms.Lite.Services.IServices.General
{
    public interface ICommonService
    {
        public Task<ResponseEntity> CheckEmptyDropDown(string Mode);
        public Task<EmployeeHeader> GetEmployeeCard(int EmpCode);
        public Task<EmployeeHeader> GetEmployeeHeader(Guid EmployeeGI);
        Task<List<Dropdown>> GetDurationWiseFilterSlabDropDown();

        public Task<ResponseEntity<string>> GetCurrentLeavePeriod();
        public Task<AddOnClassification> GetAddOnClassificationApplicability();

        public Task<ResponseEntity<Int32>> GetDisplayOrder(string master);
        Task<ResponseEntity> GetDailyAttendanceBatchUpdateLeaveValidation(int AttendanceStatusCode);
        public Task<List<EmployeeAuthority>> GetEmployeeAuthorityDetails(Guid EmployeeGI);

        public Task<ResponseEntity> GetLeaveValidation(LeaveApplication model,Guid GetLeaveValidation);

        public Task<ResponseEntity<Int32>> GetCurrentLFinancialYear();
        #region resume databank
        public Task<ResponseEntity<string>> GetReferenceNumber( string TransactionName);
        #endregion
        public Task<ResponseEntity<Int32>> GetCurrentMonthDropdown();
        public Task<DateAndMonth> GetSelectedMonthDates(int MonthCode);

        Task<AttendanceStatus> GetAttendanceStatusType(int AttendanceStatusCode);
        Task<AttendanceStatus> GetAttendanceStatusType_FH_SH(int FH,int SH);
        public Task<DateAndMonth> GetAttendanceProcessedLastMonth();
        public Task<ExcelPassword> GetExcelPassword(string ReportName, string Type);
        Task<DateAndMonth> GetCurrentMonthDetails();
        public Task<ResumeDataBank> GetMappedResumeDetails(Guid VacancyGI);
        public Task<ResumeDataBank> GetResumeDataBankDetails(Guid ResumeDataBankGI);
        public Task<UserRole> UserRole_Actions();
        public Task<PreEnrollment> GetPFMaxLimit();
        public Task<ResponseEntity> GetSalaryPeriodLockDatewiseValidation(string SeletedDate,string form,Guid? EmployeeGI);
        public Task<PreEnrollment> GetMandatoryFiledValidation(string Type);
        public Task<Url> GetUrl();
        public Task<Url> GetLettersFileName(int Type);

        public Task<Url> GetOfferLetter(string Type);

        public Task<ResponseEntity<Int32>> GetLetterFormatCode(int DocTypeCode);
        Task<ResponseEntity> GetOnboardingActiveVacanciesValidation( int VacancyManagementCode);
        Task<UserRole> GetTabApplicability(string MenuName);
        Task<ResponseEntity> Save_SoftwareLogo(string Type, CustomSoftware model);
        Task<ResponseEntity> Save_JPG_SoftwareLogo(string Type, CustomSoftware model);
        public Task<ResponseEntity<Int32>> GetCurrentAssessmentYear();
        Task<int> GetMaternityLeaveDays();
        public Task<Int32> GetNoticePeriod(int DesignationCode, Guid? EmployeeGI);



        public Task<EmployeeHeader> GetAadharDuplication(string DocNmber, int TypeCode);
        #region FileStorage
        Task<ResponseEntity<string>> GetFileStorageType();
        Task<FileStoragePath> GetFileStoragePath(string storageType, string Subfoldername);
        public Task<ResponseEntity<File>> UploadByteArrayToBLOB(File File, string SubFolder);
        public Task<ResponseEntity<File>> UploadIformFileToBLOB( File File, string SubFolder);
        public Task<ResponseEntity<File>> UploadIformFileToBLOBElearn( File File, string SubFolder);
        public Task<ResponseEntity<File>> UploadIformFileToBLOBElearnUI( File File, string SubFolder);
        public Task<ResponseEntity<File>> UploadByteArrayToIIS(File File, string SubFolder);

        public Task<ResponseEntity<File>> UploadIformFileToIIS(File File, string SubFolder);
        public Task<ResponseEntity<File>> UploadIformFileToAWS(File File, string SubFolder);

        public Task<ResponseEntity<File>> UploadByteArrayToAWS(File File, string SubFolder);
        #endregion
        public Task<Filter> GetFilterDropdowns();
        public Task<Filter> GetFilterDropdownsAndBindData(Filter filterdata);
        public Task<FilterMultiSelect> GetMultiSelectFilterDropdowns();
        Task<SoftwareURLs> GetSoftwareURLs();

        Task<ResponseEntity<File>> UploadMultipleFileToBLOB(IFormFile input, string SubFolder);
        Task<ResponseEntity<File>> UploadMultipleFileToIIS(IFormFile input, string SubFolder);
        Task<ResponseEntity<File>> UploadMultipleFileToAWS(IFormFile input, string SubFolder);
        public Task<Bank> GetDigitsInAccountNumber(int BankCode);

        Task<DailyAttendanceEditDetails> CheckCoffBalance(Guid EmployeeGI, string CoffEntitleDate);
        Task<ResponseEntity<string>> GetBankCode();
        Task<File> GetEmpThumbnail(Guid EmployeeGI );
        Task<File> GetEmpThumbnail_Approve(Guid EmployeeGI );
        Task<File> GetEmpThumbnail_OnBoarding(Guid GI );
        Task<int> GetBankRepostCount(int? MonthCode,String Type, int ReportFormat);
    }

}
