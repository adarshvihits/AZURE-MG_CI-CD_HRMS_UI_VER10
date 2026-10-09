using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.General
{

    public class DropdownService : IDropdownService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public DropdownService(IServiceBase httpService)
        {
            _baseURL = "Dropdown";
            _httpService = httpService;
        }
        
        async Task < List < Dropdown >> IDropdownService.GetPriorityDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetPriorityDropdown");
        }
        async Task<List<Dropdown>> IDropdownService.GetJobPostingDestinationDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetJobPostingDestinationDropdown");
        }
        async Task<List<Dropdown>> IDropdownService.GetRecruiterScreeningBusinessTypeDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetRecruiterScreeningBusinessTypeDropdown");
        }
        async Task<List<Dropdown>> IDropdownService.GetRecruiterScreeningDepartmentDropdown(int? BusinessTypeCode)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetRecruiterScreeningDepartmentDropdown?BusinessTypeCode={BusinessTypeCode}");
        }
        async Task<List<Dropdown>> IDropdownService.GetRecruiterScreeningSectionDropdown(int? BusinessTypeCode, int? DepartmentCode)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetRecruiterScreeningSectionDropdown?BusinessTypeCode={BusinessTypeCode}&DepartmentCode={DepartmentCode}");
        }
        async Task<List<Dropdown>> IDropdownService.GetRecruiterScreeningJobDropdown(int? BusinessTypeCode, int? DepartmentCode, int? SectionCode)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetRecruiterScreeningJobDropdown?BusinessTypeCode={BusinessTypeCode}&DepartmentCode={DepartmentCode}&SectionCode={SectionCode}");
        }
        async Task<List<Dropdown>> IDropdownService.GetInterviewPanelDropdown(int LocationCode, int DepartmentCode, int SectionCode, int GradeCode)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetInterviewPanelDropdown?LocationCode={LocationCode}&DepartmentCode={DepartmentCode}&SectionCode={SectionCode}&GradeCode={GradeCode}");
        }
        async Task<List<Dropdown>> IDropdownService.GetAbscondingDaysDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetAbscondingDaysDropDown");
        }

        async Task<List<Dropdown>> IDropdownService.GetSalutationDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/SalutationDropDown");
        }
        async Task<List<Dropdown>> IDropdownService.GetHRPlanningPeriodDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetHRPlanningPeriodDropDown");
        }
        async Task<List<Dropdown>> IDropdownService.GetAllowanceDropdown()
        {
            //throw new NotImplementedException();
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/AllowanceDropdown");
        }


        async Task<List<Dropdown>> IDropdownService.GetPlanningLocationDropdown(int? PlanningPeriod)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/PlanningLocationDropdown?PlanningPeriod={PlanningPeriod}");
        }

        async Task<List<Dropdown>> IDropdownService.GetBankDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/BankDropdown");
        }
        async Task<List<Dropdown>> IDropdownService.GetPayTypeDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetPayTypeDropDown");
        }

        async Task<List<Dropdown>> IDropdownService.GetActivityDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetActivityDropDown");
        }
        async Task<List<Dropdown>> IDropdownService.GetWorkFlowAllowanceDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetWorkFlowAllowanceDropDown");
        }

        async Task<List<Dropdown>> IDropdownService.GetCategoryDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/CategoryDropdown");
        }
        async Task<List<Dropdown>> IDropdownService.GetCategoryTypeDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/CategoryTypeDropdown");
        }
        async Task<List<Dropdown>> IDropdownService.GetERPCostCenterDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetERPCostCenterDropDown");
        }
        async Task<List<Dropdown>> IDropdownService.GetRegionDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/RegionDropdown");
        }
        async Task<List<Dropdown>> IDropdownService.GetStateDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetStateDropDown");
        }
        async Task<List<Dropdown>> IDropdownService.GetLocationTypeDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/LocationTypeDropdown");
        }

        async Task<List<Dropdown>> IDropdownService.GetCountryDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/CountryDropdown");
        }


        async Task<List<Dropdown>> IDropdownService.GetDesignationDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/DesignationDropdown");
        }




        async Task<List<Dropdown>> IDropdownService.GetDepartmentDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/DepartmentDropdown");
        }

        async Task<List<Dropdown>> IDropdownService.GetDurationDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/DurationDropdown");
        }
        async Task<List<Dropdown>> IDropdownService.GetPreenrollRelationDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/PreenrollRelationDropDown");
        }
        async Task<List<Dropdown>> IDropdownService.GetDistrictDropdown(int? StateCode)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/DistrictDropdown?StateCode={StateCode}");
        }

        async Task<List<Dropdown>> IDropdownService.DocumentCategoryDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetDocumentCategoryDropDown");
        }
        async Task<List<Dropdown>> IDropdownService.GrievanceCategoryDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetGrievanceCategoryDropDown");
        }
        async Task<List<Dropdown>> IDropdownService.GetGenderDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GenderDropdown");
        }

        async Task <List<Dropdown>> IDropdownService.GetGradeDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GradeDropdown");
        }

        async Task<List<Dropdown>> IDropdownService.GetLocationDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/LocationDropdown");
        }

        async Task<List<Dropdown>> IDropdownService.GetPaymentModeDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/PaymentModeDropDown");
        }

        async Task <List<Dropdown>> IDropdownService.GetPayrollGroupDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/PayrollGroupDropdown");
        }

        async Task<List<Dropdown>> IDropdownService.GetPunchingMethodDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/PunchingMethodDropdown");
        }

     

        async Task<List<Dropdown>> IDropdownService.GetBudgetGradingGradeDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetBudgetGradingGradeDropdown");
        }

        async Task<List<Dropdown>> IDropdownService.GetPrevMonthAdj_EffectiveMonthDropdown(int? Month)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetPrevMonthAdj_EffectiveMonthDropdown?Month=" + Month);
        }



        async Task<List<Dropdown>> IDropdownService.GetShiftDetailsDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/ShiftDetailsDropdown");
        }
        async Task<List<Dropdown>> IDropdownService.GetDutyRosterShiftDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/DutyRosterShiftDropdown");
        }

        async Task<List<Dropdown>> IDropdownService.GetShiftTypeDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/ShiftTypeDropDown");
        }

       public async Task<List<Dropdown>> GetShiftCategoryDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetShiftCategoryDropDown");
        }

        async Task <List<Dropdown>> IDropdownService.GetStateDropDown(int? CountryCode)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/StateDropdown?CountryCode={CountryCode}");
        }

        async Task<List<Dropdown>> IDropdownService.GetFamilyRelationDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/FamilyRelationDropDown");
        }
        async Task<List<Dropdown>> IDropdownService.GetBloodGroupDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/BloodGroupDropDown");
        }
        async Task<List<Dropdown>> IDropdownService.GetMaritalStatusDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/MaritalStatusDropDown");
        }
        async Task<List<Dropdown>> IDropdownService.GetReligiousViewDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/ReligiousViewDropDown");
        }
        async Task<List<Dropdown>> IDropdownService.GetHobbiesViewDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/HobbiesViewDropdown");
        }
        async Task<List<Dropdown>> IDropdownService.GetLanguagesViewDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/LanguagesViewDropdown");
        }
        async Task<List<Dropdown>> IDropdownService.GeEmergencyContactRelationDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/EmergencyContactRelationDropdown");
        }
        async Task<List<Dropdown>> IDropdownService.GetReportingManagerDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/ReportingManagerDropdown");
        }
        async Task<List<Dropdown>> IDropdownService.GetSkillTypeDropDown(int? SkillCode)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/SkillTypeDropDown?SkillCode={SkillCode}");
        }

        async Task<List<Dropdown>> IDropdownService.GetCertificateCategoryDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetCertificateCategoryDropDown");
        }
        async Task<List<Dropdown>> IDropdownService.GetQualificationDropDown(int? QualificationCode)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/QualificationDropDown?QualificationCode={QualificationCode}");
        }
        async Task<List<Dropdown>> IDropdownService.GetAllQualificationDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetQualificationDropDown");
        }
        async Task<List<Dropdown>> IDropdownService.GetSpecialisationDropDown(int? QualificationCode)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/SpecialisationDropDown?QualificationCode={QualificationCode}");
        }
        async Task<List<Dropdown>> IDropdownService.GetAssetCategoryDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetAssetCategoryDropDown");
        }
        async Task<List<Dropdown>> IDropdownService.GetAssetDropDown(int? AssetCategoryCode)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetAssetDropDown?AssetCategoryCode={AssetCategoryCode}");
        }
        async Task<List<Dropdown>> IDropdownService.GetCostCenterDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetCostCenterDropDown");
        }
        async Task<List<Dropdown>> IDropdownService.GetJobTransistionTypeDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetJobTransistionTypeDropDown");
        }
        async Task<List<Dropdown>> IDropdownService.GetAssetReturnConditionDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetAssetReturnConditionDropDown");
        }
        public async Task<List<Dropdown>> GetAssetAllocationFilterDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetAssetAllocationFilterDropDown");
        }
        async Task<List<Dropdown>> IDropdownService.GetEmployeeDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetEmployeeDropDown");
        }
        async Task<List<Dropdown>> IDropdownService.GetEmployeeListDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetEmployeeListDropDown");
        }
        async Task<List<Dropdown>> IDropdownService.GetRewardsDropdown(Guid EmployeeGI)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetRewardsDropdown?EmployeeGI={EmployeeGI}");
        }
     public    async Task<List<Dropdown>> GetRewardPointsCriteriaDropdown(int? RewardCode)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetRewardPointsCriteriaDropdown?RewardCode={RewardCode}");
        }

     public  async Task<List<Dropdown>>GetRewardsRecommendedByDropdown(Guid EmployeeGI)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetRewardsRecommendedByDropdown?EmployeeGI={EmployeeGI}");
        }
        public async Task<List<Dropdown>> GetRewardsAndRecognitionFilterDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetRewardsAndRecognitionFilterDropDown");
        }
        public async Task<List<Dropdown>> GetIncidentTypeDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetIncidentTypeDropDown");
        }
        public async Task<List<Dropdown>> GetActionTakenDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetActionTakenDropDown");
        }
        public async Task<List<Dropdown>> GetIncidentDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetIncidentDropDown");
        }
        public async Task<List<Dropdown>> GetTrainingDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetTrainingDropDown");
        }
        public async Task<List<Dropdown>> GetTrainingEmployeeDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetTrainingDropDown");
        }

        public async Task<List<Dropdown>> GetRequestTypeTrainingDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetRequestTypeTrainingDropDown");
        }
        public async Task<List<Dropdown>> GetTrainingModeDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetTrainingModeDropDown");
        }
        public async Task<List<Dropdown>> GetTrainingLogSearchDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetTrainingLogSearchDropDown");
        }
        public async Task<List<Dropdown>> GetPostAssessmentScoreDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetPostAssessmentScoreDropDown");
        }
        public async Task<List<Dropdown>> GetAppraisalEvaluatorDropDown(Guid EmployeeGI)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetAppraisalEvaluatorDropDown?EmployeeGI={EmployeeGI}");
        }

        public async Task<List<Dropdown>> GetAppraisalTypeDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetAppraisalTypeDropDown");
        }
        public async Task<List<Dropdown>> GetAppraisalRatingDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetAppraisalRatingDropDown");
        }
        public async Task<List<Dropdown>> GetAppraisalRecommendationDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetAppraisalRecommendationDropDown");
        }
        public async Task<List<Dropdown>> GetSalaryVisibilityDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetSalaryVisibilityDropDown");
        }
        public async Task<List<Dropdown>> GetAppraisalEvaluationPeriodDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetAppraisalEvaluationPeriodDropDown");
        }
        public async Task<List<Dropdown>> GetUserRoleDropDown()
        {            
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetUserRoleDropDown");
        }
        public async Task<List<Dropdown>> GetAssetMasterCategoryDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetAssetMasterCategoryDropDown");
        }
        public async Task<List<Dropdown>> GetAssetDepartmentDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetAssetDepartmentDropDown");
        }
        public async Task<List<Dropdown>> GetAssetConditionDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetAssetConditionDropDown");
        }
        public async Task<List<Dropdown>> GetHRPlanningCombination1DropDown(int? HRPlanningCode)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetHRPlanningCombination1DropDown?HRPlanningCode={HRPlanningCode}");
        }

        public async Task<List<Dropdown>> GetDocUploadDocTypeDropDown(string Mode,int? DocumentCode)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetDocUploadDocTypeDropDown?Mode={Mode}&DocumentCode={DocumentCode}");
        }
        public async Task<List<Dropdown>> GetDocUploadGridDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetDocUploadGridDropDown");
        }
       public async Task<List<Dropdown>> GetHRPlanningDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetHRPlanningDropDown");
        }

       public async Task<List<Dropdown>> GetHRPlanningAddNewCombination1DropDown(int? HRPlanningCode)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetHRPlanningAddNewCombination1DropDown?HRPlanningCode={HRPlanningCode}");
        }

      public async Task<List<Dropdown>> GetHRPlanningMonthDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetHRPlanningMonthDropDown");

        }
        public async Task<List<Dropdown>> GetApprisalTypeAssesmentModeDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetApprisalTypeAssesmentModeDropDown");

        }

        public async Task<List<Dropdown>> GetESIExpiryCheckMonthDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetESIExpiryCheckMonthDropdown");

        }   
        public async Task<List<Dropdown>> GetTriningAssesmentModeDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetApprisalTypeAssesmentModeDropDown");

        }

        public async Task<List<Dropdown>> GetTriningCategoryDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetTriningCategoryDropDown");

        }
       

        public async Task<List<Dropdown>> GetTriningTypeDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetTriningTypeDropDown");

        }

        public async Task<List<Dropdown>> GetPTGroupDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetPTGroupDropDown");
        }

        public async Task<List<Dropdown>> GetESIGroupDropDown() 
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetESIGroupDropDown");
        }
        public async Task<List<Dropdown>> GetJobTransitionTypeDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetJobTransitionTypeDropDown");
        }

        public async Task<List<Dropdown>> GetAreaDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetAreaDropDown");
        }
        public async Task<List<Dropdown>> GetFunctionalStateDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetFunctionalStateDropDown");
        }
        public async Task<List<Dropdown>> GetFilteredJobTransitionTypeDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetFilteredJobTransitionTypeDropDown");
        }
        public async Task<List<Dropdown>> GetJobTransitionReasonDropDown(int Typecode)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetJobTransitionReasonDropDown?TypeCode={Typecode}");
        }

        public async Task<List<Dropdown>> GetBankStatementFormatDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetBankStatementFormatDropDown");
        }

        public async Task<List<Dropdown>> GetRequestTypeDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetRequestTypeDropDown");
        }
        public async Task<List<Dropdown>> GetRequestLevelDropDown(int Typecode)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetRequestLevelDropDown?TypeCode={Typecode}");
        }
        public async Task<List<Dropdown>> GetLessonCategoryDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetLessonCategoryDropdown");
        }
        public async Task<List<Dropdown>> GetRequestAuthorityDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetRequestAuthorityDropDown");
        }
        public async Task<List<Dropdown>> GetReqDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetReqDropDown");
        }
        public async Task<List<Dropdown>> GetEditEmployeeInfoListDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetEditEmployeeInfoListDropDown");
        }
        public async Task<List<Dropdown>> GetHRPlanningMasterDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetHRPlanningMasterDropDown");
        }
        public async Task<List<Dropdown>> GetResignationType()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetResignationType");
        }
        public async Task<List<Dropdown>> GetResignationReason(int ResignationTypeCode)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetResignationReason?Type={ResignationTypeCode}");
        }
        public async Task<List<Dropdown>> GetExitcheckStatus()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetExitcheckStatus");
        }
        public async Task<List<Dropdown>> GetQualificationTypeDropDown(int QualificationTypeCode)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/QualificationTypeDropdown?Type ={ QualificationTypeCode}");
        }
        public async Task<List<Dropdown>> GetRequestListDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetRequestListDropDown");
        }
        public async Task<List<Dropdown>> GetPMSRecommendationTypeDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetPMSRecommendationTypeDropDown");
        }
        public async Task<List<Dropdown>> GetMealTypeDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetMealTypeDropDown");
        }
        public async Task<List<Dropdown>> GetMealTimeDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetMealTimeDropDown");
        }
        public async    Task<List<Dropdown>> GetMonthDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetMonthDropdown");
        }
        public async Task<List<Dropdown>> GetLeaveEncashment_MonthDropdown(int? PayrollGroupCode, int LeavePeriod)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetLeaveEncashment_MonthDropdown?PayrollGroupCode={PayrollGroupCode}&leaveperiod={LeavePeriod}");
        }
        public async Task<List<Dropdown>> GetLeaveEncashmentHistory_MonthDropdown(int LeavePeriod)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetLeaveEncashmentHistory_MonthDropdown?leaveperiod={LeavePeriod}");
        }
        public async    Task<List<Dropdown>> GetSalaryPostingMonthDropdown(int payrollgroup)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetSalaryPostingMonthDropdown?payrollgroup={payrollgroup}");
        }

        public async Task<List<Dropdown>> GetFullAndFinalMonthDropdown(int payrollgroup)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetFullAndFinalMonthDropdown?payrollgroup={payrollgroup}");
        }

        public async Task<List<Dropdown>> GetIndividualSalaryPostingMonthDropdown(int payrollgroup)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetIndividualSalaryPostingMonthDropdown?payrollgroup={payrollgroup}");
        }
        public async Task<List<Dropdown>> GetTrainingReportDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetTrainingReportDropdown");
        }

        public async Task<List<Dropdown>> GetPaysheetDraftMonthDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetPaysheetDraftMonthDropdown");
        }
        public async Task<List<Dropdown>> GetTrainingSubjectDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetTrainingSubjectDropDown");
        }
        public async Task<List<Dropdown>> GetSalaryHoldMonthDropDown(int PayrollGroupCode)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetSalaryHoldMonthDropDown?PayrollGroupCode={PayrollGroupCode}");
        }


        public async Task<List<Dropdown>> GetSalaryReleaseMonthDropdown(int PayrollGroupCode)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetSalaryReleaseMonthDropdown?PayrollGroupCode={PayrollGroupCode}");
        }
        public async Task<List<Dropdown>> GetLoanTypeDropdown()//sree
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetLoanTypeDropdown");
        }
        public async Task<List<Dropdown>> GetRepaymentMethodDropdown()//sree
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetRepaymentMethodDropdown");
        }
        public async Task<List<Dropdown>> GetHolidayWageSettingsAllowanceDropdown()//sree-25-04-2022
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetHolidayWageSettingsAllowanceDropdown");
        }
        public async Task<List<Dropdown>> GetCalculationTypeDropdown()//sree
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetCalculationTypeDropdown");
        }
        public async Task<List<Dropdown>> GetOvertimeSettingsAllowanceDropdown()//sree
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetOvertimeSettingsAllowanceDropdown");
        }

        public async Task<List<Dropdown>> GetOffdayWageSettingsAllowanceDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetOffdayWageSettingsAllowanceDropdown");
        }
        public async Task<List<Dropdown>> GetDeductionDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetVariableDeduction_DeductionDropdown");
        }
        public async Task<List<Dropdown>> GetAttendanceStatusDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetAttendanceStatusDropdown");
        }
        public async Task<List<Dropdown>> GetDailyAttnReportTypeDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetDailyAttnReportTypeDropDown");
        }
        public async Task<List<Dropdown>> GetAttendanceStatusTypeDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetAttendanceStatusTypeDropdown");
        }
        public async Task<List<Dropdown>> GetDivisionDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetDivisionDropdown");
        }
        public async Task<List<Dropdown>> GetSectionDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetSectionDropdown");
        }
        public async Task<List<Dropdown>> GetEmploymentTypeDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetEmploymentTypeDropdown");
        }
        public async Task<List<Dropdown>> GetWorkLocationDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetWorkLocationDropdown");
        }
        public async Task<List<Dropdown>> GetWageGradeDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetWageGradeDropdown");
        }
        public async Task<List<Dropdown>> GetEmploymentStatusDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetEmploymentStatusDropdown");
        }

        public async Task<List<Dropdown>> GetEmployeeLeaveDropDown(string Type, int? LeavePeriodCode)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetEmployeeLeaveDropDown?Type={Type}&LeavePeriodCode={LeavePeriodCode}");
        }


        public async Task<List<Dropdown>> GetPersoalDetailsFilterDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetPersoalDetailsFilterDropdown");
        }
        public async Task<List<Dropdown>> GetLeaveGroupFilterDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetLeaveGroupFilterDropdown");
        }
        public async Task<List<Dropdown>> GetMonthlyAttendanceSummaryMonthDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetMonthlyAttendanceSummaryMonthDropdown");
        }
      
        public async Task<List<Dropdown>> GetLeavePeriodFilterDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetLeavePeriodFilterDropdown");
        }
        
        public async Task<List<Dropdown>> GetLeaveGroupApplicableLeavesDropdown(int LeaveGroup)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetLeaveGroupApplicableLeavesDropdown?LeaveGroup={LeaveGroup}");
        }
        
        public async Task<List<Dropdown>> GetPeriodicLeaveCreditLeaveGroupApplicableLeavesDropdown(int LeaveGroup)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetPeriodicLeaveCreditLeaveGroupApplicableLeavesDropdown?LeaveGroup={LeaveGroup}");
        }

        public async Task<List<Dropdown>> GetPeriodicLeaveCreditLeaveGroupApplicableLeavesPeriodDropdown(int LeavePeriod)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetPeriodicLeaveCreditLeaveGroupApplicableLeavesPeriodDropdown?LeavePeriod={LeavePeriod}");
        }
        public async Task<List<Dropdown>> GetPLCreditLeaveDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetPLCreditLeaveDropdown");
        }
        public async Task<List<Dropdown>> GetLeaveFilterDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetLeaveFilterDropdown");
        }
        public async Task<List<Dropdown>> GetLeaveIncrementTypeDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetLeaveIncrementTypeDropdown");
        }
      
        public async Task<List<Dropdown>> GetTrainingDuration()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetTrainingDuration");
        }
        public async Task<List<Dropdown>> GetLeaveEncashment_LeaveDropdown(int LeaveGroup)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetLeaveEncashment_LeaveDropdown?LeaveGroup={LeaveGroup}");
        }
        public async Task<List<Dropdown>> GetLeaveCF_LeaveDropdown(int LeavegRoup)
        {

            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetLeaveCF_LeaveDropdown?LeaveGRoup={LeavegRoup}");
        }

        public async Task<List<Dropdown>> GetLeavePeriodToFilterDropdown(int LeavePeriodFromCode)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetLeavePeriodToFilterDropdown?LeavePeriodFromCode={LeavePeriodFromCode}");
        }
        public async Task<List<Dropdown>> GetCalendarYearDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetCalendarYearDropdown");
        }

        public async Task<List<Dropdown>> GetHolidayTypeDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetHolidayTypeDropdown");
        }

        public async Task<List<Dropdown>> GetHolidaySessionDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetHolidaySessionDropdown");
        }

        public async Task<List<Dropdown>> GetDutyRosterCircleDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetDutyRosterCircleDropdown");
        }
        public async Task<List<Dropdown>> GetResignationEmployeeDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetResignationEmployeeDropDown");
        }


        public async Task<List<Dropdown>> GetDayStatusDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/DayStatusDropDown");
        }

        public async Task<List<Dropdown>> GetHolidayGroupDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/HolidayGroupDropdown");
        }
        public async Task<List<Dropdown>> GetCheckUpTypeDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetCheckupTypeDropDown");
        }
        public async Task<List<Dropdown>> GetDayStatusWithoutSecondHalfDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetDayStatusWithoutSecondHalfDropDown");
        }
        public async Task<List<Dropdown>> GetLeaveDropDown(string type,Guid EmployeeGI)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/LeaveDropDown?type={ type }&EmployeeGI={EmployeeGI}");
        }
        public async Task<List<Dropdown>> GetLeaveVsLeaveReasonsDropDown(int LeaveCode)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetLeaveVsLeaveReasonsDropDown?LeaveCode={LeaveCode}");
        }
        public async Task<List<Dropdown>> GetLeaveDropDownWithAll(string type)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/LeaveDropDownWithAll?type={ type }");
        }
        public async Task<List<Dropdown>> GetLeaveReasonsDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/LeaveReasonsDropDown");
        }
        public async Task<List<Dropdown>> GetLeaveReasonsDropDownWithAll()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/LeaveReasonsDropDownWithAll");
        }
        public async Task<List<Dropdown>> GetFinancialYearDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetFinancialYearDropdown");
        }

        public async Task<List<Dropdown>> GetDateFilter()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetDateFilter");
        }
        public async Task<List<Dropdown>> GetJoiningcheckStatusDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetJoiningcheckStatus");
        }

     public async   Task<List<Dropdown>>GetDutyRosterEmployeeWiseShiftDropdown(Guid EmployeeGI)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetDutyRosterEmployeeWiseShiftDropdown?EmployeeGI={EmployeeGI}");

        }
        public async Task<List<Dropdown>> GetAllowanceType()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetAllowanceType");
        }
        public async Task<List<Dropdown>> GetCheckListType()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetCheckListTypeDropDown");
        }
        public async Task<List<Dropdown>> GetApplicableFor()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetApplicableFor");
        }
        public async Task<List<Dropdown>> GetDisplayType()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetDisplayType");
        }

        public async Task<List<Dropdown>> GetDeductionType()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetDeductionType");
        }
        public async Task<List<Dropdown>> GetAbsenteeismRegularizationDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetRegularizeDropdown");
        }
        
             public async Task<List<Dropdown>> GetAbsenteeismReportType_Dropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetAbsenteeismReportType_Dropdown");
        }
        public async Task<List<Dropdown>> GetEmployeeProfileSummaryGroupByDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetEmployeeProfileSummaryGroupBy");
        }public async Task<List<Dropdown>> GetOperators()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetOperators");
        }
        public async Task<List<Dropdown>> GetPFGroupDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetPFGroupDropdown");
        }
        public async Task<List<Dropdown>> GetPFDeductionDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetPFDeductionDropdown");
        }        
        public async Task<List<Dropdown>> GetESIDeductionDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetESIDeductionDropdown");
        }
        public async Task<List<Dropdown>> GetJoiningAndSeprationgionGrouping()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetJoiningAndSeprationgionGrouping");
        }
        public async Task<List<Dropdown>> GetRenewalInformationDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetRenewalInformationDropdown");
        }

        public async Task<List<Dropdown>> GetEmployeeGroupSummary()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetClassificationGrouping");
        }
        public async Task<List<Dropdown>> GetRepaymentMonthDropdown(string DisbursementDate)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetRepaymentMonthDropdown?DisbursementDate={DisbursementDate}");
        }
        public async Task<List<Dropdown>> GetRepaymentMethodhDropdown(int LoanType)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetRepaymentMethodhDropdown?LoanType={LoanType}");
        }

        public async Task<List<Dropdown>> GetExitInterviewResultDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetExitInterViewResultDropDown");
        }

        public async Task<List<Dropdown>> GetExitInterviewQuestionTypeDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetExitInterViewQuestionTypeDropDown");
        }
        public async Task<List<Dropdown>> GetAllowanceOrDeductionDropDown(int Typecode)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetAllowanceOrDeductionDropDown?Typecode={Typecode}");
        }
        public async Task<List<Dropdown>> GetCertificatePolicyDropdown(string Type)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetCertificatePolicyDropdown?Type={Type}");
        }


        #region PT Confirmation
        public async Task<List<Dropdown>> GetPTConfirmationMonthDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetPTConfirmationMonthDropdown");
        }
        public async Task<List<Dropdown>> GetPTConfirmationDeductionDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetPTConfirmationDeductionDropdown");
        }
        #endregion
        #region Resume Dataabnk
        public async Task<List<Dropdown>> GetResumeSourceDropdown()
        {
             return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetResumeSourceDropdown");
        }
        #endregion
        #region Reader
        public async Task<List<Dropdown>> GetReaderTypeDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetReaderTypeDropdown");
        }
        #endregion
        public async Task<List<Dropdown>> GetEditEmployee_EmployeeDropDown(string Type)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetEditEmployee_EmployeeDropDown?Type={Type}");
        }

     public async Task<List<Dropdown>> GetSkillBasedOnSkillTypeDropdown(int SkillTypeCode)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetSkillBasedOnSkillTypeDropdown?SkillTypeCode={SkillTypeCode}");
        }

       public async Task<List<Dropdown>> GetSkillType_Dropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetSkillType_Dropdown");
        }
        public async Task<List<Dropdown>> GetSalaryStructureTypeDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetSalaryStructureTypeDropDown");
        }
        public async Task<List<Dropdown>> GetSalaryStructureFormulatypeDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetSalaryStructureFormulatypeDropDown");
        }
        public async Task<List<Dropdown>> GetWageTypeDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetWageTypeDropDown");
        }
        #region EmployeeWiseOffDaySettings
        public async Task<List<Dropdown>> GetOffDaySettings_Dropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetOffDaySettings_Dropdown");
        }
        public async Task<List<Dropdown>> GetEmployeeWiseOffDaySettingsNewCombination_Dropdown(int YearCode)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetEmployeeWiseOffDaySettingsNewCombination_Dropdown?Year={YearCode}");
        }
        public async Task<List<Dropdown>> GetEmployeeWiseOffDaySettingsPrvCombination_Dropdown(int YearCode)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetEmployeeWiseOffDaySettingsPrvCombination_Dropdown?Year={YearCode}");
        }
        #endregion
        #region Attritionreport
        public async Task<List<Dropdown>> GetAttritionGroupingDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetAttritionGroupingDropdown");
        }
        public async Task<List<Dropdown>> GetAttritionCalculationDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetAttritionCalculationDropdown");
        }
        #endregion
        #region ReaderLogReport
        public async Task<List<Dropdown>> GetReaderLogReportClassificationDropdown(int GroupByCode)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetReaderLogReportClassificationDropdown?GroupByCode={GroupByCode}");
        }


        #endregion
        public async Task<List<Dropdown>> GetExitInterViewGroupDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetExitInterViewGroupDropDown");
        }
        public async Task<List<Dropdown>> GeSalaryStructureDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetSalaryStructureDropDown");
        }
        #region HR Policy
        public async Task<List<Dropdown>> GetHRPolicyCombinationDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetHRPolicyCombinationDropDown");
        }public async Task<List<Dropdown>> GetEventGroupCombinationDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetEventGroupCombinationDropDown");
        }

        public async Task<List<Dropdown>> GetAnnouncementCombinationDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetAnnouncementCombinationDropDown");
        }
        public async Task<List<Dropdown>> GetLearningGroupSettingsCombinationDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetLearningGroupSettingsCombinationDropDown");
        }
        public async Task<List<Dropdown>> GetHRPolicyCategoryDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetHRPolicyCategoryDropDown");
        }
        #endregion
        #region Applicant Tracking
        public async Task<List<Dropdown>> GetInterviewSelectionDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetInterviewSelectionDropdown");
        }
        public async Task<List<Dropdown>> GetInterviewLevelDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetInterviewLevelDropdown");
        }
        public async Task<List<Dropdown>> GetInterviewTypeDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetInterviewTypeDropdown");
        }
        public async Task<List<Dropdown>> GetInterviewModeDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetInterviewModeDropdown");
        }
        public async Task<List<Dropdown>> GetEvaluationStatusDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetEvaluationStatusDropdown");
        }
        public async Task<List<Dropdown>> GetInteviewNextLevelDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetInteviewNextLevelDropdown");
        }
        public async Task<List<Dropdown>> GetInteviewBackgroundVarificationStatusDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetInteviewBackgroundVarificationStatusDropdown");
        }
        public async Task<List<Dropdown>> GetInteviewBackgroundVarificationTypeDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetInteviewBackgroundVarificationTypeDropdown");
        }
        public async Task<List<Dropdown>> GetInteviewSalaryCommunicationDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetInteviewSalaryCommunicationDropdown");
        }

        #endregion
        public async Task<List<Dropdown>> GetSkillDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetSkillDropdown");
        }

        public async Task<List<Dropdown>> GetResumeMappingFilterTypeDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetResumeMappingFilterTypeDropDown");

        }
        
             public async Task<List<Dropdown>> EmployeeTransferTypeDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetEmployeeTransferTypeDropDownWithAll");
        }
        public async Task<List<Dropdown>> GetFilterBydDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetFilterBydDropDown");
        }

        public async Task<List<Dropdown>> GetRecruitmentdDropDown(int FilterBy)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetRecruitmentdDropDown?FilterBy={FilterBy}");
        }

        public async Task<List<Dropdown>> GetCancellationReasonDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetCancellationReasonDropDown");
        }
        public async Task<List<Dropdown>> GetMRFRequestedByDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/MRFRequestedByDropdown");
        }

       public async Task<List<Dropdown>> BudgetDesignationDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/BudgetDesignationDropdown");
        }
        public async Task<List<Dropdown>> BudgetVarianceDesignationDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/BudgetVarianceDesignationDropdown");
        }

        public async Task<List<Dropdown>> GetBudgetGradingDesignationDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetBudgetGradingDesignationDropdown");
        }
        public async Task<List<Dropdown>> CandidateRejectReasonDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/CandidateRejectReasonDropdown");
        }
        public async Task<List<Dropdown>> GetMRFDesignationDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetMRFDesignationDropdown");
        }
        public async Task<List<Dropdown>> GetTDSConfirmation_DeductionDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetTDSConfirmation_DeductionDropdown");
        }
        public async Task<List<Dropdown>> GetTDSConfirmationMonthDropdown(int AssessmentYearCode)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetTDSConfirmationMonthDropdown?AssessmentYearCode={AssessmentYearCode}");
        }
        public async Task<List<Dropdown>> GetVariableAllowance_AllowanceDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetVariableAllowance_AllowanceDropdown");
        }
        public async Task<List<Dropdown>> GetVariableDeduction_DeductionDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetVariableDeduction_DeductionDropdown");
        }
        public async Task<List<Dropdown>> GetLeaveGroupApplicableLeavesDropdown(int? LeaveGroup)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetLeaveGroupApplicableLeavesDropdown?LeaveGroup={LeaveGroup}");
        }
        #region COFFEntitles
        public async Task<List<Dropdown>> GetCOFFEntitlesDropDown(Guid EmployeeGI, string Date, string  Mode)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetCOFFEntitlesDropDown?EmployeeGI={EmployeeGI}&Date={Date}&Mode={Mode}");
        }
        public async Task<List<Dropdown>> GetCOFFEntitlesBasedonDayStatusDropDown(Guid EmployeeGI, string Date, string Mode,int FH,int SH)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetCOFFEntitlesBasedonDayStatusDropDown?EmployeeGI={EmployeeGI}&Date={Date}&Mode={Mode}&FH={FH}&SH={SH}");
        }

        public async Task<List<Dropdown>> GetPersonalDetailReportTypeDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetPersonalDetailReportTypeDropdown");

        }
        #endregion
        public async Task<List<Dropdown>> GetDueClearanceResponsiblePersonDropdown(int? DepartmentCode)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetDueClearanceResponsiblePersonDropdown?DepartmentCode={DepartmentCode}");
        }

        public async Task<List<Dropdown>> GetResponsiblePersonDropdown(int? DepartmentCode)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetResponsiblePersonDropdown?DepartmentCode={DepartmentCode}");
        }
        public async Task<List<Dropdown>> GetMenuSearchDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetMenuSearchDropdown?LoginFrom=HRMS");
        }
        public async Task<List<Dropdown>> GetCircleAuthorityActionDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetCircleAuthorityActionDropdown");
        }
        #region PayrollReportMonth
        public async Task<List<Dropdown>> GetPayrollReportMonthDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetPayrollReportMonthDropdown");
        }
        public async Task<List<Dropdown>> GetSalaryProcessedMonthDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetSalaryProcessedMonthDropdown");
        }
        public async Task<List<Dropdown>> GetPayrollReportPreviousMonthDropdown(int Month)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetPayrollReportPreviousMonthDropdown?Month={Month}");
        }
        #endregion
        #region SalaryReconciliation
        public async Task<List<Dropdown>> GetSalaryReconciliationStatusDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetSalaryReconciliationStatusDropdown");
        }
        public async Task<List<Dropdown>> GetSalaryReconciliationEmployeeDropDown(int Status)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetSalaryReconciliationEmployeeDropDown?Status={Status}");
        }
        #endregion
        public async Task<List<Dropdown>> GetLeaveEncashmentSettingsAllowanceDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetLeaveEncashmentSettingsAllowanceDropdown");
        }
        public async Task<List<Dropdown>> GetSalaryIntegrity_AlwDedDropdown(int? Type)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetSalaryIntegrity_AlwDedDropdown?Type={Type}");
        }
        #region Monthly_AttendanceProcessing
       public async Task<List<Dropdown>>GetMonthly_AttendanceProcessing_MonthDropdown(int? PayrollGroupCode)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetMonthly_AttendanceProcessing_MonthDropdown?PayrollGroupCode={PayrollGroupCode}");
        }
        #endregion
        #region individual Monthly_Attendance and Salary Processing
     public async Task<List<Dropdown>> GetIndividual_ProcessingEmployeeDropDown(int Status, int SalaryPeriod)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetIndividual_ProcessingEmployeeDropDown?Status={Status}&SalaryPeriod={SalaryPeriod}");
        }


        #endregion

        public async Task<List<Dropdown>> GetSalaryProcessing_MonthDropdown(int? PayrollGroupCode)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetSalaryProcessing_MonthDropdown?PayrollGroupCode={PayrollGroupCode}");
        }
        public async Task<List<Dropdown>> GetSalaryProcessingAttenLockMonthDropdown(int? PayrollGroupCode)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetSalaryProcessingAttenLockMonthDropdown?PayrollGroupCode={PayrollGroupCode}");
        }
        public async Task<List<Dropdown>> GetSalaryProcessedMonth_Dropdown(int? PayrollGroup)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetSalaryProcessedMonth_Dropdown?PayrollGroup={PayrollGroup}");
        }
        public async Task<List<Dropdown>> GetFandFSalaryProcessing_MonthDropdown(int? PayrollGroup)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetFandFSalaryProcessing_MonthDropdown?PayrollGroup={PayrollGroup}");
        }
        
        public async Task<List<Dropdown>> DailyAttnEntryBatchUpDateLeaveDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/DailyAttnEntryBatchUpDateLeaveDropDown");
        }
        public async Task<List<Dropdown>> GetLoanSettingsDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetLoanSettingsDropDown");
        }
        public async Task<List<Dropdown>> GetRewardsnRecognitionRewardsDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetRewardsnRecognitionRewardsDropdown");
        }

        public async Task<List<Dropdown>> GetVariableProvisionAndPerk_AllowanceDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/VariableProvisionAndPerk_AllowanceDropdown");
        }
        public async Task<List<Dropdown>> GetDocumentTypeDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetDocumentTypeDropDown");
        }

       public async Task<List<Dropdown>> GetLeaveOpeningEntryTypeDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetLeaveOpeningEntryTypeDropDown");
        }

      public async Task<List<Dropdown>> GetLeaveCFEntryTypeDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetLeaveCFEntryTypeDropDown");
        }
        
        
        public async Task<List<Dropdown>> GetSalaryIntegrityPreviousMonthDropdown(int Month)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetSalaryIntegrityPreviousMonthDropdown?Month={Month}");
        }
        #region MIS
        public async Task<List<Dropdown>> GetModules()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetModules");
        }
        public async Task<List<Dropdown>> GetMisReports(int moduleCode)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetMisReports?moduleCode={moduleCode}");
        }
        public async Task<List<Dropdown>> GetEmployeeList()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetEmployeeList");
        }
        #endregion
        #region PMS
        public async Task<List<Dropdown>> GetAppraisalCategory()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetAppraisalCategory");
        }
        public async Task<List<Dropdown>> GetAppraisalFactorCategory()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetAppraisalFactorCategory");
        }
        public async Task<List<Dropdown>> GetPMSAppraisalType()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetPMSAppraisalType");
        }

       public async Task<List<Dropdown>> GetPMSAppraisalCombo()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetPMSAppraisalCombo");
        }

       public async Task<List<Dropdown>> GetPMSAppraisalAuthorityLevel()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetPMSAppraisalAuthorityLevel");
        }
        public async Task<List<Dropdown>> GetPMSAppraisal()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetPMSAppraisalCombo");
        }
        public async Task<List<Dropdown>> GetPMSAppraisalRole()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetPMSAppraisalRoleCombo");
        }
        public async Task<List<Dropdown>> GetPMSAppraisalAuhLevelCombo(int AppraisalCode)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetPMSAppraisalAuhLevelComboForReport?AppraisalCode={AppraisalCode}");
        }
        async Task<List<Dropdown>> IDropdownService.GetEmpDropDown(int? LocationCode, int? RegionCode, int? DepartmentCode)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetEmpDropDown?Locationcode={LocationCode}&Departmentcode={DepartmentCode}&RegionCode={RegionCode}");
        }

        public async Task<List<Dropdown>> GetAppraisalTypeUnitDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetAppraisalTypeUnitDropDown");
        }
        public async Task<List<Dropdown>> GetAppraisalRoleTypeDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetAppraisalRoleTypeDropDown");
        }

        public async Task<List<Dropdown>> GetAccountHeadCombo(string Type)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetAccountHeadCombo?Type={Type}");
        }

        public async Task<List<Dropdown>> AccountHeadTypeCombo()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetAccountHeadTypeCombo");
        }
        #endregion
        public async Task<List<Dropdown>> InsuranceCompany()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/InsuranceCompanyDropDown");
        }
        public async Task<List<Dropdown>> GetInsurancePolicyDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/InsurancePolicyDropDown");
        }
        public async Task<List<Dropdown>> GetCostCenterCombo()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetCostCenterCombo");
        }


        public async Task<List<Dropdown>> GetVDACombo()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetVDACombo");
        }
        public async Task<List<Dropdown>> GetAssessmentYearDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetAssessmentYearDropdown");
        }
        public async Task<List<Dropdown>> GetAssessmentYearForTDSOldDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetAssessmentYearForTDSOldDropdown");
        }
        
        public async Task<List<Dropdown>> GetITCategoryDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetITCategoryDropdown");
        }
        public async Task<List<Dropdown>> GetTDSvsComponentTypeDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetTDSvsComponentTypeDropdown");
        }
        public async Task<List<Dropdown>> GetTDSvsAllowanceDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetTDSvsAllowanceDropdown");
        }
        public async Task<List<Dropdown>> GetFormulaBasedDeductionDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetFormulaBasedDeductionDropdown");
        }
        public async Task<List<Dropdown>> GetFormulaBasedDeductionOperationsDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetFormulaBasedDeductionOperationsDropdown");
        }

        #region CUSTOM 
        public async Task<List<Dropdown>> GetAttendanceGroupDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetAttendanceGroupDropdown");
        }

        #endregion
        public async Task<List<Dropdown>> GetSkillProficiencyDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetJobPortalSkillProficiencyDropdown");
        }

        public async Task<List<Dropdown>> GetCautionDepositDeductionDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetCautionDepositDeductionDropdown");
        }

        public async Task<List<Dropdown>> GetVariableAllowanceMonthDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetVariableAllowanceMonthDropdown");
        }


        public async Task<List<Dropdown>> GetCautionDepositAllowanceDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetCautionDepositAllowanceDropdown");
        }
            public async Task<List<Dropdown>> GetOtherDocumentsDropDown(int? DocumentCode)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetOtherDocumentsDropDown?DocumentCode={DocumentCode}");
        }

        async Task<List<Dropdown>> IDropdownService.GetNextMonthDropDown(int SalaryPeriodCode)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetNextMonthDropDown?SalaryPeriodCode={SalaryPeriodCode}");

        }
        public async Task<List<Dropdown>> GetWPSAccountHeadCombo(string Type)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetWPSAccountHeadCombo?Type={Type}");
        }
        public async Task<List<Dropdown>> GetPFSummaryGroupbyDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetPFSummaryGroupbyDropdown");
        }

        public async Task<List<Dropdown>> GetERPClassificationDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetERPClassificationDropDown");
        }
        public async Task<List<Dropdown>> GetPaySheetTypeDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetPaySheetTypeDropDown");
        }
        public async Task<List<Dropdown>> GetCustomFieldDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetCustomFieldDropDown");
        }
        public async Task<List<Dropdown>> GetEntryDataTypeDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetEntryDataTypeDropDown");
        }
        public async Task<List<Dropdown>> GetBulkUploadingTypeDropDown(int EntryType)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetBulkUploadingTypeDropDown?EntryType={EntryType}");
        }
        public async Task<List<Dropdown>> GetRegimeTypeDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetRegimeTypeDropdown");
        }

        public async Task<List<Dropdown>> GetAbscountingTerminationReasonDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetAbscountingTerminationReasonDropDown");
        }
        public async Task<List<Dropdown>> GetGroupCompanyDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetGroupCompanyDropDown");
        }


        public async Task<List<Dropdown>> GetProfitCenterDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetProfitCenterDropDown");
        }

        async Task<List<Dropdown>> IDropdownService.GetAttendanceProcessedMonthDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetAttendanceProcessedMonthDropdown");

        }
        public async Task<List<Dropdown>> GetAuthorityDropDown(int TypeCode,int LevelCode)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetAuthorityDropDown?TypeCode={TypeCode}&LevelCode={LevelCode}");
        }
        async Task<List<Dropdown>> IDropdownService.GetLWFDeductionDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetLWFDeductionDropdown");

        }
        async Task<List<Dropdown>> IDropdownService.GetPerquisiteDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetPerquisiteDropdown");

        }
        public async Task<List<Dropdown>> GetPayrollReportBuilderMonthDropdown(int FinancialYear, int Type)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetPayrollReportBuilderMonthDropdown?FinancialYear={FinancialYear}&Type={Type}");
        }
        async Task<List<Dropdown>> IDropdownService.GetExcemptionDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetExcemptionDropdown");

        }

        async Task<List<Dropdown>> IDropdownService.GetDailyAttendanceEntry_MonthDropdown(int? PayrollGroupCode)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetDailyAttendanceEntry_MonthDropdown?PayrollGroupCode={PayrollGroupCode}");
        }

        public async Task<List<Dropdown>> GetEmployeeMoodDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetEmployeeMoodDropdown");
        }
        public async Task<List<Dropdown>> GetMaternityEsiLeaveDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetMaternityEsiLeaveDropDown");
        }

        public async Task<List<Dropdown>> GetIncentiveMonthDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetIncentiveMonthDropdown");
        }

        public async Task<List<Dropdown>> GetAttendanceUnLockMonthDropdown(int? PayrollGroup)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetAttendanceUnLockMonthDropdown?PayrollGroup={PayrollGroup}");
        }



        public async Task<List<Dropdown>> GetIncentiveAllowanceDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetIncentiveAllowanceDropdown");
        }
        public async Task<List<Dropdown>> GetSalaryAdvanceLoanTypeDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetSalaryAdvanceLoanTypeDropdown");
        }
        public async Task<List<Dropdown>> GetSalaryAdvanceAndLoanIssue_LoanTypeDropdown(string TabIndex)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetSalaryAdvanceAndLoanIssue_LoanTypeDropdown?TabIndex={TabIndex}");
        }
        
        public async Task<List<Dropdown>> GetYearlySalaryEmployee(int FinancialYear)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetYearlySalaryEmployee?FinancialYear={FinancialYear}");
        }
        public async Task<List<Dropdown>> GetTrnScheduleDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetTrnScheduleDropdown");
        }
        public async Task<List<Dropdown>> GetTrainingDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/Training_GetTrainingDropdown");
        }

        public async Task<List<Dropdown>> GetTrainingReportDropdown(int FYCode,int CategoryCode)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetTrainingReportDropdown?FYCode={FYCode}&CategoryCode={CategoryCode}");
        }

        public async Task<List<Dropdown>> GetTrainingEmployeeDropdown(int FYCode, int CategoryCode, int TrainingCode)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetTrainingEmployeeDropdown?FYCode={FYCode}&CategoryCode={CategoryCode}&TrainingCode={TrainingCode}");
        }

        public async Task<List<Dropdown>> GetCalenderYearDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetCalenderYearDropdown?LoginFrom=HRMS");
        }


        public async Task<List<Dropdown>> GetSheduleTrainingTypeDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetSheduleTrainingTypeDropDown");
        }

        public async Task<List<Dropdown>> GetSheduleTrainerDropdown(int type)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetSheduleTrainerDropdown?type={type}");
        }
        public async Task<List<Dropdown>> GetTrainingSubjectTopicDropDown(int TrainingCode)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetTrainingSubjectTopicDropDown?TrainingCode={TrainingCode}");
        }
        public async Task<List<Dropdown>> GetScheduleVenueTypeDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetScheduleVenueTypeDropDown");
        }
        public async Task<List<Dropdown>> GetSheduleVenueDropdown(int type)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetSheduleVenueDropdown?type={type}");
        }
        public async Task<List<Dropdown>> GetTrainingStatusType()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetTrainingStatusType");
        }
        public async Task<List<Dropdown>> GetPerformanceAllowanceDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetPerformanceAllowanceDropdown");
        }
        

        #region PPR

        public async Task<List<Dropdown>> GetEntryTypeDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetEntryTypeDropdown?LoginFrom=HRMS");
        }

        public async Task<List<Dropdown>> GetReviewCategoryDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetReviewCategoryDropdown?LoginFrom=HRMS");
        }
        public async Task<List<Dropdown>> GetFactorGroupDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetFactorGroupDropDown?LoginFrom=HRMS");
        } 
       
        public async Task<List<Dropdown>> GetFactorTypeDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetFactorTypeDropDown?LoginFrom=HRMS");
        }

        public async Task<List<Dropdown>> GetRatingMethodDropDown(int? id)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetRatingMethodDropDown?id={id}");
        }

        public async Task<List<Dropdown>> GetUnitDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetUnitDropDown?LoginFrom=HRMS");
        }

        public async Task<List<Dropdown>> GetPPRAppraisalAuthorityLevel()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetPPRAppraisalAuthorityLevel");
        }
        public async Task<List<Dropdown>> GetPPRAppraisalRole()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetPPRAppraisalRole?LoginFrom=HRMS");
        }
        public async Task<List<Dropdown>> GetPPRAppraisalCombo()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetPPRAppraisalCombo");
        }
        public async Task<List<Dropdown>> GetReviewPeriod()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetReviewPeriod");
        }
        public async Task<List<Dropdown>> GetRatingTypeDropDown(int? FactorTypeCode)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetRatingTypeDropDown?FactorTypeCode={FactorTypeCode}");
        }
        #endregion

        public async Task<List<Dropdown>> GetTrainingCostDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetTrainingCostDropdown");
        }

        public async Task<List<Dropdown>> GetScheduleCategory()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetScheduleCategory");
        }

        public async Task<List<Dropdown>> GetJobTransitionDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetJobTransitionDropDown");
        }
       
        public async Task<List<Dropdown>> GetTrainingCategoryDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetTrainingCategoryDropdown");
        }

        public async Task<List<Dropdown>> GetArrearVsAllowanceDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetArrearVsAllowanceDropdown");
        }
        public async Task<List<Dropdown>> GetBonusGroupDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetBonusGroupDropdown");
        }

        public async Task<List<Dropdown>> GetBonusProcessingMonthDropdown(int MonthCode)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetBonusProcessingMonthDropdown?MonthCode={MonthCode}");
        }
        public async Task<List<Dropdown>> GetPeriodicDeductionReportToMonthDropdown(int Month)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetPeriodicDeductionReportToMonthDropdown?Month={Month}");
        }
        public async Task<List<Dropdown>> GetpPeriodicDeductionDropdown(int MonthFrom, int MonthTo)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetpPeriodicDeductionDropdown?MonthFrom={MonthFrom}&MonthTo={MonthTo}");
        }
        public async Task<List<Dropdown>> GetEmpVsShiftAllowanceDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetEmpVsShiftAllowanceDropdown");
        }
        
        public Task<List<Dropdown>> GetTrainingEmployeeVsReportDropdown()
        {
            throw new NotImplementedException();
        }



        public async Task<List<Dropdown>> GetDeductionSettingsDropDown()//sree
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetDeductionSettingsDropDown");
        }

        async Task<List<Dropdown>> IDropdownService.GetUserDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetUserDropdown");
        }
        async Task<List<Dropdown>> IDropdownService.GetRecruitmentMenuDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetRecruitmentMenuDropdown");
        }
        async Task<List<Dropdown>> IDropdownService.HRAttendanceApprovalListAuthDropdown(string TabIndex, int LeavePeriodCode, int Level, int Month)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/HRAttendanceApprovalListAuthDropdown?TabIndex={TabIndex}&LeavePeriodCode={LeavePeriodCode}&Level={Level}&Month={Month}");
        }
        public async Task<List<Dropdown>> GetTrainingGroupVsEmployeeDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetTrainingGroupVsEmployeeDropdown");
        }
        public async Task<List<Dropdown>> GetTrainingSubjectDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetTrainingSubjectDropdown");
        }
        public async Task<List<Dropdown>> GetReactivationTypeDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetReactivationTypeDropdown");
        }
        public async Task<List<Dropdown>> GetPolicyDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetPolicyDropdown");
        }
        public async Task<List<Dropdown>> GetClubbingLeaveDropDown(int LeaveCode)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetClubbingLeaveDropDown?LeaveCode={LeaveCode}");
        }

        public async Task<List<Dropdown>> GetPunchRegularizationReasonDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetPunchRegularizationReasonDropDown");
        }
        public async Task<List<Dropdown>> GetSurveyTypeDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetSurveyTypeDropdown");
        }
        public async Task<List<Dropdown>> GetReplyGroupDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetReplyGroupDropdown");
        }
        public async Task<List<Dropdown>> GetEmployeeTaskStatusDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetEmployeeTaskStatusDropDownNew");
        }
        public async Task<List<Dropdown>> GetSurveyDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetSurveyDropDown");
        }
        public async Task<List<Dropdown>> GetEarnedLeaveDropdown(int LeaveGroup)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetEarnedLeaveDropdown");
        }
        public async Task<List<Dropdown>> GetRenewalsCategoryDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetRenewalsCategoryDropdown");
        }
        public async Task<List<Dropdown>> GetLeaveBasedOnLaps(int LeaveGpCode)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetLeaveBasedOnLabsDropDown?LeaveGroupCode={LeaveGpCode}");
        }
        public async Task<List<Dropdown>> GetLearningGroup()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetLearningGroup");
        }

        public async Task<List<Dropdown>> GetLessonGroupDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetLessonGroupDropdown");
        }
        public async Task<List<Dropdown>> GetIncrementToolReasonDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetIncrementToolReasonDropDown");
        }
        public async Task<List<Dropdown>> GetIncrementToolTransitionTypeDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetIncrementToolTransitionTypeDropdown");
        }

    
    public async Task<List<Dropdown>> GetLesson()
    {
        return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetLesson");
    }

        public async Task<List<Dropdown>> GetTransactionUserDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetTransactionUser");
        }

        public async Task<List<Dropdown>> GetTransactionMenuDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetTransactionMenuDropdown");
        }
        public async Task<List<Dropdown>> GetVersionDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetVersionDropDown");
        }
        public async Task<List<Dropdown>> GetVersionUpdateTypeDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetVersionUpdateTypeDropDown");
        }
        public async Task<List<Dropdown>> GetRemittanceTypeDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetRemittanceTypeDropDown");
        }
        public async Task<List<Dropdown>> GetRoundOffDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetRoundOffDropDown");
        }

        public async Task<List<Dropdown>> GetCitytypeList()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetCitytypeList");
        }

        public async Task<List<Dropdown>> RateCalculationType()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/RateCalculationType");
        }

        public async Task<List<Dropdown>> LeaveApplicationHistoryLeaveDropdown(string type, Guid EmployeeGI)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/LeaveApplicationHistoryLeaveDropdown?type={type}&EmployeeGI={EmployeeGI}");
        }
        public async Task<List<Dropdown>> GetDocumentType()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetDocumentType");
        }
        public async Task<List<Dropdown>> GetClassificationTypeDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetClassificationTypeDropdown");
        }
        public async Task<List<Dropdown>> GetClassification_Dropdown(int GroupingCode)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetClassification_Dropdown?GroupingCode={GroupingCode}");
        }
        public async Task<List<Dropdown>> GetResignationStatusDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetResignationStatusDropdown");
        }
        public async Task<List<Dropdown>> GetUserVsGroupCompanyDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetUserVsGroupCompanyDropDown");
        }
        public async Task<List<Dropdown>> GetAttendanceSuspensionDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetAttendanceSuspensionDropDown");
        }

        public async Task<List<Dropdown>> GetSalaryPerDayAllowanceDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetSalaryPerDayAllowanceDropDown");
        }
        public async Task<List<Dropdown>> GetLearningReplyGroupDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetLearningReplyGroupDropdown");
        }
        public async Task<List<Dropdown>> GetQuestionTemplateDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetQuestionTemplateDropdown");
        }
        public async Task<List<Dropdown>> GetLessonDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetLessonDropdown");
        }
        public async Task<List<Dropdown>> GetTestLevelDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetTestLevelDropdown");
        }

        public async Task<List<Dropdown>> GetTransitionTypeDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetTransitionTypeDropdown");
        }
        public async Task<List<Dropdown>> GetLearningReplyDropdown(int replyGroupCode)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetLearningReplyDropdown?replyGroupCode={replyGroupCode}");
        }
        public async Task<List<Dropdown>> GetReplyCombo(int replyGroupCode)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetReplyCombo?replyGroupCode={replyGroupCode}");
        }
        public async Task<List<Dropdown>> GetKeyPositionRiskDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetKeyPositionRiskDropdown");
        }
        public async Task<List<Dropdown>> GetTrainingReplyDropdown(int ReplygroupCode)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetTrainingReplyDropdown?ReplyGroupCode={ReplygroupCode}");
        }
        public async Task<List<Dropdown>> GetTrainingReplyGroupDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetTrainingReplyGroupDropdown");
        }
        public async Task<List<Dropdown>> GetFullDayHalfDayStatusDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetFullDayHalfDayStatusDropDown");
        }

        public async Task<List<Dropdown>> GetTrainingPreTestDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetTrainingPreTestDropDown");
        }
        public async Task<List<Dropdown>> GetTrainingPostTestDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetTrainingPostTestDropDown");
        }
        public async Task<List<Dropdown>> GetSurveyCodeDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetSurveyCodeDropDown");
        }
        public async Task<List<Dropdown>> GetLearningCourse()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetLearningCourse");
        }
        public async Task<List<Dropdown>> GetCompanyProfileGroupCompanyDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetCompanyProfileGroupCompanyDropDown");
        }

        public async Task<List<Dropdown>> GetPrivilegeType()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetPrivilegeTypeDropDown");
        }

        public async Task<List<Dropdown>> GetCommitmentType()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetCommitmentTypeDropDown");
        }

        public async Task<List<Dropdown>> GetFaqSoftwareDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetFaqSoftwareDropDown");
        }

        public async Task<List<Dropdown>> GetFaqCategoryDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetFaqCategoryDropDown");
        }

        public async Task<List<Dropdown>> GetAttendanceDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetAttendanceDropDown");
        }
        public async Task<List<Dropdown>> GetCommitteeDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetCommitteeDropDown");
        }
        public async Task<List<Dropdown>> GetMemberTypeDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetMemberTypeDropDown");
        }
        public async Task<List<Dropdown>> GetLoanTypeDetailsDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetLoanTypeDetailsDropdown");
        }
        public async Task<List<Dropdown>> GetSalaryAdvance_LoanTypeDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetSalaryAdvance_LoanTypeDropdown");
        }
        public async Task<List<Dropdown>> GetLocationVsEsiDropDown(int? Locationcode)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetLocationVsEsiDropDown?Locationcode={Locationcode}");
        }
        public async Task<List<Dropdown>> GetLocationVsPTgroupDropDown(int? Locationcode)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetLocationVsPTgroupDropDown?Locationcode={Locationcode}");
        }
        public async Task<List<Dropdown>> GetTargetGroupDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetTargetGroupDropdown");
        }
        public async Task<List<Dropdown>> GetTargetCategoryDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetTargetCategoryDropdown");
        }

        public async Task<List<Dropdown>> GetAppraisalLevelDropdown(int HeadCode)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetAppraisalLevelDropdown?HeadCode={HeadCode}");
        }
        public async Task<List<Dropdown>> GetConsoleTypesDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetConsoleTypesDropdown");
        }

        async Task<List<Dropdown>> IDropdownService.GetAllRegionsDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetAllRegionsDropDown");
        }
        async Task<List<Dropdown>> IDropdownService.GetConsoleTypeDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetConsoleTypeDropDown");
        }
        async Task<List<Dropdown>> IDropdownService.GetEconsoleCategoryDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetEconsoleCategoryDropDown");
        }
        async Task<List<Dropdown>> IDropdownService.GetSubDepartmentDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetSubDepartmentDropDown");
        }
        async Task<List<Dropdown>> IDropdownService.GetFunctionDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetFunctionDropDown");
        }
        #region ROle metrix
        public async Task<List<Dropdown>> GetRoleDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetRoleDropdown");
        }
        public async Task<List<Dropdown>> GetRoleClassificationDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetRoleClassificationDropdown");
        }
        async Task<List<Dropdown>> IDropdownService.GetCountryVsLocationDropDown(int? CountryCode)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetCountryVsLocationDropDown?CountryCode={CountryCode}");
        }
        #endregion

        async Task<List<Dropdown>> IDropdownService.GetWageGradeVsSalaryBand(int WageGradeCode)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetWageGradeVsSalaryBand?WageGradeCode={WageGradeCode}");
                  
        }
        async Task<List<Dropdown>> IDropdownService.GetGCVsPayrollGroup(int GcCode)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetGCVsPayrollGroup?GcCode={GcCode}");
        }
        async Task<List<Dropdown>> IDropdownService.GetEventDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetEventDropDown");
        }
        async Task<List<Dropdown>> IDropdownService.GetNewPositionDropdown(int Designation, int Location, int Departmnet)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetNewPositionDropdown?Designation={Designation}&Location={Location}&Departmnet={Departmnet}");
        }
        async Task<List<Dropdown>> IDropdownService.GetReplacementEmployeeDropdown(int Designation, int Location, int Departmnet, string Type)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetReplacementEmployeeDropdown?Designation={Designation}&Location={Location}&Departmnet={Departmnet}&Type={Type}");
        }
        async Task<List<Dropdown>> IDropdownService.GetIncentiveCostCenterDropdown(int Applicability)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetIncentiveCostCenterDropdown?Applicability={Applicability}");
        }
        public async Task<List<Dropdown>> GetRoleMatrixRoleDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetRoleMatrixRoleDropdown");
        }
        public async Task<List<Dropdown>> GetWorkFlowDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetWorkFlowDropdown");
        }
        public async Task<List<Dropdown>> GetTemplateDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetTemplateDropdown");
        }
        public async Task<List<Dropdown>> GetApproverRolesDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetApproverRolesDropdown");
        }
        public async Task<List<Dropdown>> GetRecruitmentRoleDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetRecruitmentRoleDropdown");
        }
        public async Task<List<Dropdown>> GetRoleOrEmpDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetRoleOrEmpDropdown");
        }
        async Task<List<Dropdown>> IDropdownService.GetAllowanceWithSalaryStructureDropdown(int SalaryStructurecode)
        {
            //throw new NotImplementedException();
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetAllowanceWithSalaryStructureDropdown?SalaryStructurecode={SalaryStructurecode}");
        }
        public async Task<List<Dropdown>> GetIncentiveCategoryDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetIncentiveCategoryDropdown");
        }
        public async Task<List<Dropdown>> GetBnkStatemntRptFormatDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetBnkStatemntRptFormatDropdown");
        }
        public async Task<List<Dropdown>> GetDepartmentNAASDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetDepartmentNAASDropdown");
        }
        public async Task<List<Dropdown>> GetPurposeNAASDropdown(int DepartmentCode)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetPurposeNAASDropdown?DepartmentCode={DepartmentCode}");
        }
        public async Task<List<Dropdown>> GetCertificationReimbursementMonthDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetCertificationReimbursementMonthDropdown");
        }
        public async Task<List<Dropdown>> GetAdditionalResponsibilityDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetAdditionalResponsibilityDropdown");
        }
        public async Task<List<Dropdown>> GetStatusDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetStatusDropdown");
        }

        //Elearning
        public async Task<List<Dropdown>> GetCourseGroupDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetCourseGroupDropdown");
        }
        public async Task<List<Dropdown>> GetEmpClassReportFilterDropdown(string classification)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetEmpClassReportFilterDropdown?classification={classification}");
        }
        public async Task<List<Dropdown>> GetEvaluationPeriod()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetEvaluationPeriod");
        }
        public async Task<List<Dropdown>> GetDisplayRange()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetDisplayRange");
        }
        public async Task<List<Dropdown>> GetLessonVsCourseGroupDropdown(int LessonCode)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetLessonVsCourseGroupDropdown?LessonCode={LessonCode}");
        }
        public async Task<List<Dropdown>> GetLearningDocumentLessonDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetLearningDocumentLessonDropdown");
        }
        public async Task<List<Dropdown>> GetLessonVsAssessmentDropdown(int LessonCode)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetLessonVsAssessmentDropdown?LessonCode={LessonCode}");
        }
        public async Task<List<Dropdown>> GetLearningReplyDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetLearningReplyDropdown");
        }
        public async Task<List<Dropdown>> GetLearningAnswerDropdown(int? QuestionCode)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetLearningAnswerDropdown?QuestionCode={QuestionCode}");
        }
        public async Task<List<Dropdown>> GetLessonVsCourseDropdown(string Course)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetLessonVsCourseDropdown?Course={Course}");
        }
        public async Task<List<Dropdown>> GetAssessmentVsLessonDropdown(int AssessmentType)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetAssessmentVsLessonDropdown?AssessmentType={AssessmentType}");
        }
        public async Task<List<Dropdown>> GetContentReportLessonCategoryDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetContentReportLessonCategoryDropdown");
        }
        public async Task<List<Dropdown>> GetAssessmentDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetAssessmentDropdown");
        }
        public async Task<List<Dropdown>> GetAssessmentReportLessonDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetAssessmentReportLessonDropdown");
        }
        public async Task<List<Dropdown>> GetCompanyDocumentTypeDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetCompanyDocumentTypeDropdown");
        }
        public async Task<List<Dropdown>> GetCompanyDocumentDropdown(int DocType)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetCompanyDocumentDropdown?DocType={DocType}");
        }
        public async Task<List<Dropdown>> GetMailAlertTypeDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetMailAlertTypeDropDown");
        }

        public async Task<List<Dropdown>> GetSpecialHolidayDropdown(int YearCode)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetSpecialHolidayDropdown?yearCode={YearCode}");
        }

        public async Task<List<Dropdown>> GetAllowanceProvisionDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetAllowanceProvisionDropdown");
        }
        public async Task<List<Dropdown>> GetBankPostedMonthDropdown(String Type)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetBankPostedMonthDropdown?Type={Type}");
        }
        public async Task<List<Dropdown>> GetExternalSalaryProcessingMonthDropdown(int? PayrollGroupCode)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetExternalSalaryProcessingMonthDropdown?PayrollGroupCode={PayrollGroupCode}");
        }
        public async Task<List<Dropdown>> GetExternalPayrollReportMonthDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetExternalPayrollReportMonthDropdown");
        }
        public async Task<List<Dropdown>> GetJVPostingMonthDropdown(int Type, int PayrollGroupCode)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetJVPostingMonthDropdown?Type={Type}&PayrollGroupCode={PayrollGroupCode}");
        }
        public async Task<List<Dropdown>> GetRecoveryAllowanceDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetRecoveryAllowanceDropdown");
        }
        public async Task<List<Dropdown>> GetPayrollGroupWithGrpCmpyCheckDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetPayrollGroupWithGrpCmpyCheckDropdown");
        }
        public async Task<List<Dropdown>> GetERPAccountHeadCombo()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetERPAccountHeadCombo");
        }
        public async Task<List<Dropdown>> GetProductDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetProductDropdown");
        }
        public async Task<List<Dropdown>> GetSubProductDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetSubProductDropdown");
        }
        public async Task<List<Dropdown>> GetTDSAssessmentYearMonthDropdown(int AssessmentYearCode)
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetTDSAssessmentYearMonthDropdown?AssessmentYearCode={AssessmentYearCode}");
        }
        public async Task<List<Dropdown>> GetChangeAuthorityWorkFlowDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetChangeAuthorityWorkFlowDropdown");
        }
        public async Task<List<Dropdown>> GetMasterJobDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetMasterJobDropdown");
        }
        public async Task<List<Dropdown>> GetMetroDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetMetroDropDown");
        }
        public async Task<List<Dropdown>> GetLocationCategoryDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetLocationCategoryDropDown");
        }

        public async Task<List<Dropdown>> GetBusinessTypeDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetBusinessTypeDropDown");
        }
        public async Task<List<Dropdown>> GetZoneDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetZoneDropDown");
        }
        public async Task<List<Dropdown>> GetclusterDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetclusterDropDown");
        }
        public async Task<List<Dropdown>> GetRecruitmentVenueDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetRecruitmentVenueDropdown");
        }
        public async Task<List<Dropdown>> GetRecruitmentExpenseTypeDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetRecruitmentExpenseTypeDropdown");
        }
        public async Task<List<Dropdown>> GetDomainDropDown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetDomainDropDown");
        }
        public async Task<List<Dropdown>> GetWorkModeDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetWorkModeDropdown");
        }
        public async Task<List<Dropdown>> GetExperienceLevelDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetExperienceLevelDropdown");
        }
        public async Task<List<Dropdown>> GetJobEmploymentTypeDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetJobEmploymentTypeDropdown");
        }
        public async Task<List<Dropdown>> GetJobAreaDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetJobAreaDropdown");
        }
        public async Task<List<Dropdown>> GetJobQualificationDropdown()
        {
            return await _httpService.GetAsync<List<Dropdown>>($"{_baseURL}/GetJobQualificationDropdown");
        }
    }
}