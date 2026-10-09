using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.General
{
    public interface IDropdownService
    {

        Task<List<Dropdown>> GetPriorityDropdown();
        Task<List<Dropdown>> GetRecruiterScreeningBusinessTypeDropdown();
        Task<List<Dropdown>> GetRecruiterScreeningDepartmentDropdown(int? BusinessTypeCode);
        Task<List<Dropdown>> GetRecruiterScreeningSectionDropdown(int? BusinessTypeCode, int? DepartmentCode);
        Task<List<Dropdown>> GetRecruiterScreeningJobDropdown(int? BusinessTypeCode, int? DepartmentCode, int? SectionCode);
        Task<List<Dropdown>> GetInterviewPanelDropdown(int LocationCode, int DepartmentCode, int SectionCode, int GradeCode);

        Task<List<Dropdown>> GetJobPostingDestinationDropdown();
        Task<List<Dropdown>> GetHRPlanningPeriodDropDown();

        Task<List<Dropdown>> GetPlanningLocationDropdown(int? PlanningPeriod);
        Task<List<Dropdown>> GetAbscondingDaysDropDown();
        Task<List<Dropdown>> GetSalutationDropDown();
        Task<List<Dropdown>> GetGenderDropDown();
        Task<List<Dropdown>> GrievanceCategoryDropDown();
        Task<List<Dropdown>> GetCategoryDropdown();
        Task<List<Dropdown>> GetCategoryTypeDropdown();
        Task<List<Dropdown>> GetDepartmentDropdown();
        Task<List<Dropdown>> GetDesignationDropdown();
        Task<List<Dropdown>> GetDurationDropdown();
        Task<List<Dropdown>> GetPayrollGroupDropdown();
        Task<List<Dropdown>> GetGradeDropdown();
        Task<List<Dropdown>> GetPayTypeDropDown();
        Task<List<Dropdown>> GetActivityDropDown();
        Task<List<Dropdown>> GetWorkFlowAllowanceDropDown();
        Task<List<Dropdown>> GetLocationDropdown();
        Task<List<Dropdown>> GetCountryDropDown();
        Task<List<Dropdown>> GetRegionDropDown();
        Task<List<Dropdown>> GetStateDropDown();
        Task<List<Dropdown>> GetPTGroupDropDown();
        Task<List<Dropdown>> GetESIGroupDropDown();
        Task<List<Dropdown>> GetLessonCategoryDropdown();
        Task<List<Dropdown>> DocumentCategoryDropDown();
        Task<List<Dropdown>> GetPreenrollRelationDropDown();
        Task<List<Dropdown>> GetAreaDropDown();
        Task<List<Dropdown>> GetFunctionalStateDropDown();
        Task<List<Dropdown>> GetLocationTypeDropDown();
        Task<List<Dropdown>> GetStateDropDown(int? CountryCode);
        Task<List<Dropdown>> GetDistrictDropdown(int? StateCode);
        Task<List<Dropdown>> GetBankDropdown();
        Task<List<Dropdown>> GetPaymentModeDropDown();
        Task<List<Dropdown>> GetShiftTypeDropDown();
        Task<List<Dropdown>> GetShiftCategoryDropDown();
        Task<List<Dropdown>> GetBudgetGradingGradeDropdown();
        Task<List<Dropdown>> GetPrevMonthAdj_EffectiveMonthDropdown(int? Month);
        Task<List<Dropdown>> GetShiftDetailsDropdown();
        Task<List<Dropdown>> GetDutyRosterShiftDropdown();
        Task<List<Dropdown>> GetDutyRosterEmployeeWiseShiftDropdown(Guid EmployeeGI);
        Task<List<Dropdown>> GetLeaveEncashment_MonthDropdown(int? PayrollGroupCode,int LeavePeriod);
        Task<List<Dropdown>> GetLeaveEncashmentHistory_MonthDropdown(int LeavePeriod);
        Task<List<Dropdown>> GetPunchingMethodDropdown();
        Task<List<Dropdown>> GetAllowanceDropdown();
        Task<List<Dropdown>> GetFamilyRelationDropdown();
        Task<List<Dropdown>> GetBloodGroupDropdown();
        Task<List<Dropdown>> GetMaritalStatusDropdown();

        Task<List<Dropdown>> GetCertificateCategoryDropDown();
        Task<List<Dropdown>> GetReligiousViewDropdown();
        Task<List<Dropdown>> GetHobbiesViewDropdown();
        Task<List<Dropdown>> GetLanguagesViewDropdown();
        Task<List<Dropdown>> GeEmergencyContactRelationDropdown();
        public Task<List<Dropdown>> GetTrainingDuration();
        Task<List<Dropdown>> GetReportingManagerDropdown();
        Task<List<Dropdown>> GetSkillTypeDropDown(int? SkillCode);
        Task<List<Dropdown>> GetQualificationDropDown(int? QualificationCode);
        Task<List<Dropdown>> GetAllQualificationDropDown();
        Task<List<Dropdown>> GetSpecialisationDropDown(int? QualificationCode);
        Task<List<Dropdown>> GetAssetCategoryDropDown();
        Task<List<Dropdown>> GetAssetDropDown(int? AssetCategoryCode);
        Task<List<Dropdown>> GetAssetReturnConditionDropDown();
        Task<List<Dropdown>> GetAssetAllocationFilterDropDown();
        Task<List<Dropdown>> GetEmployeeDropDown();
        Task<List<Dropdown>> GetEmployeeListDropDown();
        Task<List<Dropdown>> GetRewardsDropdown(Guid EmployeeGI);
        Task<List<Dropdown>> GetRewardsRecommendedByDropdown(Guid EmployeeGI);
        Task<List<Dropdown>> GetRewardPointsCriteriaDropdown(int? RewardCode);
        Task<List<Dropdown>> GetCostCenterDropDown();
        Task<List<Dropdown>> GetJobTransistionTypeDropDown();
        Task<List<Dropdown>> GetRewardsAndRecognitionFilterDropDown();
        Task<List<Dropdown>> GetIncidentTypeDropDown();
        Task<List<Dropdown>> GetActionTakenDropDown();
        Task<List<Dropdown>> GetIncidentDropDown();
        Task<List<Dropdown>> GetTrainingDropDown();
        Task<List<Dropdown>> GetRequestTypeTrainingDropDown();
        Task<List<Dropdown>> GetTrainingModeDropDown();
        Task<List<Dropdown>> GetTrainingLogSearchDropDown();
        Task<List<Dropdown>> GetPostAssessmentScoreDropDown();
        Task<List<Dropdown>> GetAppraisalEvaluatorDropDown(Guid EmployeeGI);
        Task<List<Dropdown>> GetAppraisalEvaluationPeriodDropDown();
        Task<List<Dropdown>> GetAppraisalTypeDropDown();
        Task<List<Dropdown>> GetAppraisalRatingDropDown();
        Task<List<Dropdown>> GetAppraisalRecommendationDropDown();
        Task<List<Dropdown>> GetSalaryVisibilityDropDown();
        Task<List<Dropdown>> GetUserRoleDropDown();
        Task<List<Dropdown>> GetAssetMasterCategoryDropDown();
        Task<List<Dropdown>> GetAssetDepartmentDropDown();
        Task<List<Dropdown>> GetAssetConditionDropDown();
        Task<List<Dropdown>> GetHRPlanningDropDown();
        Task<List<Dropdown>> GetHRPlanningCombination1DropDown(int? HRPlanningCode);
        Task<List<Dropdown>> GetHRPlanningAddNewCombination1DropDown(int? HRPlanningCode);
        Task<List<Dropdown>> GetHRPlanningMonthDropDown();
        Task<List<Dropdown>> GetDocUploadDocTypeDropDown(string Mode, int? DocumentCode);
        Task<List<Dropdown>> GetDocUploadGridDropDown();
        Task<List<Dropdown>> GetApprisalTypeAssesmentModeDropDown();
        Task<List<Dropdown>> GetESIExpiryCheckMonthDropdown();
        Task<List<Dropdown>> GetTriningAssesmentModeDropDown();
        Task<List<Dropdown>> GetTriningCategoryDropDown();
        Task<List<Dropdown>> GetTriningTypeDropDown();
        Task<List<Dropdown>> GetJobTransitionTypeDropdown();
        Task<List<Dropdown>> GetFilteredJobTransitionTypeDropDown();
        Task<List<Dropdown>> GetJobTransitionReasonDropDown(int Typecode);
        Task<List<Dropdown>> GetRequestTypeDropDown();
        Task<List<Dropdown>> GetRequestLevelDropDown(int Typecode);
        Task<List<Dropdown>> GetRequestAuthorityDropDown();
        Task<List<Dropdown>> GetReqDropDown();

        Task<List<Dropdown>> GetEditEmployeeInfoListDropDown();
        Task<List<Dropdown>> GetHRPlanningMasterDropDown();
        Task<List<Dropdown>> GetResignationType();
        Task<List<Dropdown>> GetResignationReason(int ResignationTypeCode);
        Task<List<Dropdown>> GetExitcheckStatus();
        Task<List<Dropdown>> GetQualificationTypeDropDown(int QualificationTypeCode);
        Task<List<Dropdown>> GetRequestListDropDown();
        Task<List<Dropdown>> GetPMSRecommendationTypeDropDown();
        Task<List<Dropdown>> GetMealTypeDropDown();
        Task<List<Dropdown>> GetMealTimeDropDown();
        Task<List<Dropdown>> GetMonthDropdown();
        Task<List<Dropdown>> GetSalaryPostingMonthDropdown(int payrollgroup);
        Task<List<Dropdown>> GetFullAndFinalMonthDropdown(int payrollgroup);
        Task<List<Dropdown>> GetIndividualSalaryPostingMonthDropdown(int PayrollGroup);
        Task<List<Dropdown>> GetPaysheetDraftMonthDropdown();

        // Task<List<Dropdown>> GetTrainingReportDropdown();

        Task<List<Dropdown>> GetTrainingEmployeeVsReportDropdown();
        Task<List<Dropdown>> GetTrainingSubjectDropDown();
        Task<List<Dropdown>> GetSalaryHoldMonthDropDown(int PayrollGroupCode);
        Task<List<Dropdown>> GetSalaryReleaseMonthDropdown(int PayrollGroupCode);
        Task<List<Dropdown>> GetAttendanceStatusDropdown();
        Task<List<Dropdown>> GetDailyAttnReportTypeDropDown();
        Task<List<Dropdown>> GetAttendanceStatusTypeDropdown();
        Task<List<Dropdown>> GetDivisionDropdown();
        Task<List<Dropdown>> GetSectionDropdown();
        Task<List<Dropdown>> GetEmploymentTypeDropdown();
        Task<List<Dropdown>> GetWorkLocationDropdown();
        Task<List<Dropdown>> GetWageGradeDropdown();
        Task<List<Dropdown>> GetEmploymentStatusDropdown();
        Task<List<Dropdown>> GetPersoalDetailsFilterDropdown();
        Task<List<Dropdown>> GetEmployeeLeaveDropDown(string Type, int? LeavePeriodCode);
        Task<List<Dropdown>> GetMonthlyAttendanceSummaryMonthDropdown();
        Task<List<Dropdown>> GetLeaveGroupFilterDropdown();
        Task<List<Dropdown>> GetLeavePeriodFilterDropdown();
        Task<List<Dropdown>> GetPLCreditLeaveDropdown();
        Task<List<Dropdown>> GetLeaveFilterDropdown();
        Task<List<Dropdown>> GetPeriodicLeaveCreditLeaveGroupApplicableLeavesDropdown(int LeaveGroup);
        Task<List<Dropdown>> GetPeriodicLeaveCreditLeaveGroupApplicableLeavesPeriodDropdown(int LeavePeriod);
        Task<List<Dropdown>> GetLeaveGroupApplicableLeavesDropdown(int LeaveGroup);    
        Task<List<Dropdown>> GetLeaveIncrementTypeDropdown();
        Task<List<Dropdown>> GetLeaveEncashment_LeaveDropdown(int LeaveGroup);
        Task<List<Dropdown>> GetLeaveCF_LeaveDropdown(int LeavegRoup);
        Task<List<Dropdown>> GetLeavePeriodToFilterDropdown(int LeavePeriodFromCode);
        Task<List<Dropdown>> GetCalendarYearDropdown();
        Task<List<Dropdown>> GetHolidayTypeDropdown();
        Task<List<Dropdown>> GetHolidaySessionDropdown();
        Task<List<Dropdown>> GetDeductionDropdown();
        Task<List<Dropdown>> GetLoanTypeDropdown();//sree
        Task<List<Dropdown>> GetRepaymentMethodDropdown();//sree
        Task<List<Dropdown>> GetHolidayWageSettingsAllowanceDropdown();//sree-25-04-2022                                       
        Task<List<Dropdown>> GetDutyRosterCircleDropdown();
        Task<List<Dropdown>> GetResignationEmployeeDropDown();
        Task<List<Dropdown>> GetCalculationTypeDropdown();//sree
        Task<List<Dropdown>> GetOvertimeSettingsAllowanceDropdown();
        Task<List<Dropdown>> GetOffdayWageSettingsAllowanceDropdown();
        Task<List<Dropdown>> GetAttendanceUnLockMonthDropdown(int? PayrollGroup);
        Task<List<Dropdown>> GetDayStatusDropDown();
        Task<List<Dropdown>> GetHolidayGroupDropdown();
        Task<List<Dropdown>> GetDayStatusWithoutSecondHalfDropDown();
        Task<List<Dropdown>> GetLeaveDropDown(string type, Guid EmployeeGI);
        Task<List<Dropdown>> GetLeaveVsLeaveReasonsDropDown(int LeaveCode);

        Task<List<Dropdown>> DailyAttnEntryBatchUpDateLeaveDropDown();

        Task<List<Dropdown>> GetCheckUpTypeDropDown();
        Task<List<Dropdown>> GetLeaveDropDownWithAll(string type);

        Task<List<Dropdown>> GetLeaveReasonsDropDown();
        Task<List<Dropdown>> GetCOFFEntitlesDropDown(Guid EmployeeGI, string Date, string Mode);
        Task<List<Dropdown>> GetLeaveReasonsDropDownWithAll();
        Task<List<Dropdown>> GetFinancialYearDropdown();
        Task<List<Dropdown>> GetDateFilter();
        Task<List<Dropdown>> GetJoiningcheckStatusDropdown();
        Task<List<Dropdown>> GetAllowanceType();
        Task<List<Dropdown>> GetCheckListType();
        Task<List<Dropdown>> GetApplicableFor();
        Task<List<Dropdown>> GetDisplayType();
        Task<List<Dropdown>> GetDeductionType();
        Task<List<Dropdown>> GetAbsenteeismRegularizationDropDown();
        Task<List<Dropdown>> GetAbsenteeismReportType_Dropdown();
        Task<List<Dropdown>> GetBankStatementFormatDropDown();

        Task<List<Dropdown>> GetEmployeeProfileSummaryGroupByDropDown();
        Task<List<Dropdown>> GetOperators();
        Task<List<Dropdown>> GetPFGroupDropdown();
        Task<List<Dropdown>> GetPFDeductionDropdown();
        Task<List<Dropdown>> GetESIDeductionDropdown();
        Task<List<Dropdown>> GetJoiningAndSeprationgionGrouping();
        Task<List<Dropdown>> GetRenewalInformationDropdown();

        Task<List<Dropdown>> GetEmployeeGroupSummary();
        Task<List<Dropdown>> GetRepaymentMonthDropdown(string DisbursementDate);
        Task<List<Dropdown>> GetRepaymentMethodhDropdown(int LoanType);

        Task<List<Dropdown>> GetExitInterviewResultDropDown();
        Task<List<Dropdown>> GetExitInterViewGroupDropDown();


        Task<List<Dropdown>> GetResumeMappingFilterTypeDropDown();
        Task<List<Dropdown>> EmployeeTransferTypeDropDown();
        Task<List<Dropdown>> GetEmpDropDown(int? LocationCode, int? RegionCode, int? DepartmentCode);
        Task<List<Dropdown>> AccountHeadTypeCombo();

        Task<List<Dropdown>> GetExitInterviewQuestionTypeDropDown();
        Task<List<Dropdown>> GetAllowanceOrDeductionDropDown(int Typecode);
        Task<List<Dropdown>> GetCertificatePolicyDropdown(string Type);
        #region PT Confirmation
        Task<List<Dropdown>> GetPTConfirmationDeductionDropdown();
        Task<List<Dropdown>> GetPTConfirmationMonthDropdown();
        #endregion
        #region  resumedataBank
        Task<List<Dropdown>> GetResumeSourceDropdown();

        #endregion
        #region Reader
        Task<List<Dropdown>> GetReaderTypeDropdown();
        #endregion
        Task<List<Dropdown>> GetEditEmployee_EmployeeDropDown(string Type);
        Task<List<Dropdown>> GetSkillBasedOnSkillTypeDropdown(int SkillTypeCode);
        Task<List<Dropdown>> GetSkillType_Dropdown();
        Task<List<Dropdown>> GetSalaryStructureTypeDropDown();
        Task<List<Dropdown>> GetSalaryStructureFormulatypeDropDown();
        Task<List<Dropdown>> GetWageTypeDropDown();
        #region EmployeeWiseOffDaySettings

        Task<List<Dropdown>> GetOffDaySettings_Dropdown();
        Task<List<Dropdown>> GetEmployeeWiseOffDaySettingsNewCombination_Dropdown(int YearCode);
        Task<List<Dropdown>> GetEmployeeWiseOffDaySettingsPrvCombination_Dropdown(int YearCode);
        #endregion
        #region Attrition Report
        Task<List<Dropdown>> GetAttritionGroupingDropdown();
        Task<List<Dropdown>> GetAttritionCalculationDropdown();
        #endregion
        #region Reader Log Report
        Task<List<Dropdown>> GetReaderLogReportClassificationDropdown(int GroupByCode);
        #endregion
        Task<List<Dropdown>> GeSalaryStructureDropDown();
        #region HR Policy
        Task<List<Dropdown>> GetHRPolicyCombinationDropDown();
        Task<List<Dropdown>> GetEventGroupCombinationDropDown();
        Task<List<Dropdown>> GetAnnouncementCombinationDropDown();
        Task<List<Dropdown>> GetLearningGroupSettingsCombinationDropDown();
        Task<List<Dropdown>> GetHRPolicyCategoryDropDown();
        #endregion
        #region Applicant Tracking
        Task<List<Dropdown>> GetInterviewSelectionDropdown();
        Task<List<Dropdown>> GetInterviewLevelDropdown();
        Task<List<Dropdown>> GetInterviewTypeDropdown();
        Task<List<Dropdown>> GetInterviewModeDropdown();
        Task<List<Dropdown>> GetEvaluationStatusDropdown();
        Task<List<Dropdown>> GetInteviewNextLevelDropdown();
        Task<List<Dropdown>> GetInteviewBackgroundVarificationStatusDropdown();
        Task<List<Dropdown>> GetInteviewBackgroundVarificationTypeDropdown();
        Task<List<Dropdown>> GetInteviewSalaryCommunicationDropdown();

        #endregion
        Task<List<Dropdown>> GetSkillDropdown();

        #region RecruitmentDashBoard
        Task<List<Dropdown>> GetFilterBydDropDown();
        Task<List<Dropdown>> GetRecruitmentdDropDown(int FilterBy);
        Task<List<Dropdown>> GetCancellationReasonDropDown();
        #endregion
        public Task<List<Dropdown>> GetMRFRequestedByDropdown();
        public Task<List<Dropdown>> GetTDSConfirmation_DeductionDropdown();
        public Task<List<Dropdown>> GetTDSConfirmationMonthDropdown(int AssessmentYearCode);
        public Task<List<Dropdown>> GetVariableDeduction_DeductionDropdown();
        public Task<List<Dropdown>> GetVariableAllowance_AllowanceDropdown();

        #region  BUdget Upload AND VARINCE
        Task<List<Dropdown>> BudgetDesignationDropdown();
        Task<List<Dropdown>> BudgetVarianceDesignationDropdown();

        #endregion

        #region #BudgetGrading
        Task<List<Dropdown>> GetBudgetGradingDesignationDropdown();
        Task<List<Dropdown>> CandidateRejectReasonDropdown();
        #endregion

        #region MRF 
        public Task<List<Dropdown>> GetMRFDesignationDropdown();
        #endregion
        Task<List<Dropdown>> GetPersonalDetailReportTypeDropdown();
        Task<List<Dropdown>> GetLeaveGroupApplicableLeavesDropdown(int? LeaveGroup);
        Task<List<Dropdown>> GetDueClearanceResponsiblePersonDropdown(int? DepartmentCode);
        Task<List<Dropdown>> GetResponsiblePersonDropdown(int? DepartmentCode);
        Task<List<Dropdown>> GetMenuSearchDropdown();
        Task<List<Dropdown>> GetCircleAuthorityActionDropdown();
        #region PayrollReportMonth
        Task<List<Dropdown>> GetPayrollReportMonthDropdown();
        Task<List<Dropdown>> GetSalaryProcessedMonthDropdown();
        #endregion
        #region SalaryReconciliation
        Task<List<Dropdown>> GetSalaryReconciliationStatusDropdown();
        Task<List<Dropdown>> GetSalaryReconciliationEmployeeDropDown(int Status);

        #endregion
        Task<List<Dropdown>> GetLeaveEncashmentSettingsAllowanceDropdown();
        public Task<List<Dropdown>> GetSalaryIntegrity_AlwDedDropdown(int? Type);


        #region Monthly_AttendanceProcessing
        Task<List<Dropdown>> GetMonthly_AttendanceProcessing_MonthDropdown(int? PayrollGroupCode);
        #endregion
        #region individual Monthly_Attendance and Salary Processing
        Task<List<Dropdown>> GetIndividual_ProcessingEmployeeDropDown(int Status, int SalaryPeriod);
        #endregion

        #region SalaryProcessing                
        Task<List<Dropdown>> GetSalaryProcessing_MonthDropdown(int? PayrollGroupCode);
        Task<List<Dropdown>> GetSalaryProcessingAttenLockMonthDropdown(int? PayrollGroupCode);

        Task<List<Dropdown>> GetSalaryProcessedMonth_Dropdown(int? PayrollGroup);
        Task<List<Dropdown>> GetFandFSalaryProcessing_MonthDropdown(int? PayrollGroupCode);
        #endregion
        public Task<List<Dropdown>> GetLoanSettingsDropDown();
        public Task<List<Dropdown>> GetDeductionSettingsDropDown();
        public Task<List<Dropdown>> GetRewardsnRecognitionRewardsDropdown();
        public Task<List<Dropdown>> GetVariableProvisionAndPerk_AllowanceDropdown();
        public Task<List<Dropdown>> GetDocumentTypeDropdown();

        #region Leave opening n CF entry Type
        Task<List<Dropdown>> GetLeaveOpeningEntryTypeDropDown();
        Task<List<Dropdown>> GetLeaveCFEntryTypeDropDown();

        #endregion


        Task<List<Dropdown>> GetModules();

        Task<List<Dropdown>> GetMisReports(int moduleCode);
        Task<List<Dropdown>> GetEmployeeList();
        Task<List<Dropdown>> GetSalaryIntegrityPreviousMonthDropdown(int Month);

        #region PMS
        public Task<List<Dropdown>> GetAppraisalCategory();
        public Task<List<Dropdown>> GetAppraisalFactorCategory();
        public Task<List<Dropdown>> GetPMSAppraisalType();
        Task<List<Dropdown>> GetPMSAppraisalCombo();
        Task<List<Dropdown>> GetPMSAppraisalAuthorityLevel();
        Task<List<Dropdown>> GetPMSAppraisal();
        Task<List<Dropdown>> GetPMSAppraisalRole();
        public Task<List<Dropdown>> GetPMSAppraisalAuhLevelCombo(int AppraisalCode);
        public Task<List<Dropdown>> GetAccountHeadCombo(string Type);
        public Task<List<Dropdown>> GetWPSAccountHeadCombo(string Type);
        #endregion
        Task<List<Dropdown>> InsuranceCompany();
        Task<List<Dropdown>> GetInsurancePolicyDropDown();
        public Task<List<Dropdown>> GetCostCenterCombo();
        Task<List<Dropdown>> GetVDACombo();
        Task<List<Dropdown>> GetAppraisalRoleTypeDropDown();
        Task<List<Dropdown>> GetAppraisalTypeUnitDropDown();

        #region income tax rates
        Task<List<Dropdown>> GetAssessmentYearDropdown();
        Task<List<Dropdown>> GetAssessmentYearForTDSOldDropdown();
        Task<List<Dropdown>> GetITCategoryDropdown();
        #endregion

        public Task<List<Dropdown>> GetFormulaBasedDeductionDropdown();
        public Task<List<Dropdown>> GetFormulaBasedDeductionOperationsDropdown();

        Task<List<Dropdown>> GetNextMonthDropDown(int SalaryPeriodCode);
        Task<List<Dropdown>> GetPayrollReportPreviousMonthDropdown(int Month);
        Task<List<Dropdown>> GetPFSummaryGroupbyDropdown();

        #region CUSTOM
        Task<List<Dropdown>> GetAttendanceGroupDropdown();
        public Task<List<Dropdown>> GetSkillProficiencyDropdown();
        Task<List<Dropdown>> GetPerformanceAllowanceDropdown();
        #endregion
        Task<List<Dropdown>> GetOtherDocumentsDropDown(int? DocumentCode);
        Task<List<Dropdown>> GetERPClassificationDropDown();
        Task<List<Dropdown>> GetPaySheetTypeDropDown();
        Task<List<Dropdown>> GetCustomFieldDropDown();
        Task<List<Dropdown>> GetEntryDataTypeDropDown();
        Task<List<Dropdown>> GetBulkUploadingTypeDropDown(int EntryType);
        Task<List<Dropdown>> GetIncentiveAllowanceDropdown();


        Task<List<Dropdown>> GetIncentiveMonthDropdown();
        Task<List<Dropdown>> GetCautionDepositDeductionDropdown();
        Task<List<Dropdown>> GetVariableAllowanceMonthDropdown();
        Task<List<Dropdown>> GetCautionDepositAllowanceDropdown();

        Task<List<Dropdown>> GetProfitCenterDropDown();

        #region TDS
        Task<List<Dropdown>> GetRegimeTypeDropdown();
        public Task<List<Dropdown>> GetTDSvsComponentTypeDropdown();
        public Task<List<Dropdown>> GetTDSvsAllowanceDropdown();
        public Task<List<Dropdown>> GetPerquisiteDropdown();
        public Task<List<Dropdown>> GetExcemptionDropdown();

        #endregion

        #region Absconding Termination
        Task<List<Dropdown>> GetAbscountingTerminationReasonDropDown();

        #endregion

        Task<List<Dropdown>> GetGroupCompanyDropDown();
        Task<List<Dropdown>> GetAuthorityDropDown(int TypeCode, int LevelCode);

        Task<List<Dropdown>> GetERPCostCenterDropDown();
        Task<List<Dropdown>> GetAttendanceProcessedMonthDropdown();

        Task<List<Dropdown>> GetLWFDeductionDropdown();
        Task<List<Dropdown>> GetPayrollReportBuilderMonthDropdown(int FinancialYear, int Type);
        
        Task<List<Dropdown>> GetDailyAttendanceEntry_MonthDropdown(int? PayrollGroupCode);
        Task<List<Dropdown>> GetEmployeeMoodDropdown();
        Task<List<Dropdown>> GetMaternityEsiLeaveDropDown();
        Task<List<Dropdown>> GetYearlySalaryEmployee(int FinancialYear);
        public Task<List<Dropdown>> GetSalaryAdvanceLoanTypeDropdown();
        public Task<List<Dropdown>> GetSalaryAdvanceAndLoanIssue_LoanTypeDropdown(string TabIndex);
        Task<List<Dropdown>> GetTrainingReportDropdown(int FYCode, int CategoryCode);

        Task<List<Dropdown>> GetTrainingEmployeeDropdown(int FYCode,int CategoryCode,int TrainingCode);
        public Task<List<Dropdown>> GetTrnScheduleDropdown();
        public Task<List<Dropdown>> GetTrainingDropdown();

        Task<List<Dropdown>> GetSheduleTrainingTypeDropDown();
        Task<List<Dropdown>> GetTrainingSubjectTopicDropDown(int TrainingCode);
        Task<List<Dropdown>> GetSheduleTrainerDropdown(int type);
        Task<List<Dropdown>> GetScheduleVenueTypeDropDown();
        Task<List<Dropdown>> GetSheduleVenueDropdown(int type);
        Task<List<Dropdown>> GetTrainingStatusType();


        #region PPR
        Task<List<Dropdown>> GetEntryTypeDropdown();
        Task<List<Dropdown>> GetReviewCategoryDropdown();

        Task<List<Dropdown>> GetFactorGroupDropDown();
        Task<List<Dropdown>> GetFactorTypeDropDown();
        Task<List<Dropdown>> GetRatingMethodDropDown(int? id);

        Task<List<Dropdown>> GetUnitDropDown();
        Task<List<Dropdown>> GetReviewPeriod();
        Task<List<Dropdown>> GetCalenderYearDropdown();
        Task<List<Dropdown>> GetPPRAppraisalCombo();

        Task<List<Dropdown>> GetPPRAppraisalRole();

        Task<List<Dropdown>> GetPPRAppraisalAuthorityLevel();
        Task<List<Dropdown>> GetRatingTypeDropDown(int? FactorTypeCode);
        #endregion
        public Task<List<Dropdown>> GetTrainingCostDropdown();
        Task<List<Dropdown>> GetScheduleCategory();

        Task<List<Dropdown>> GetJobTransitionDropDown();

        Task<List<Dropdown>> GetTrainingCategoryDropdown();

        Task<List<Dropdown>> GetArrearVsAllowanceDropdown();
        Task<List<Dropdown>> GetBonusProcessingMonthDropdown( int MonthCode);
        Task<List<Dropdown>> GetBonusGroupDropdown();
        Task<List<Dropdown>> GetpPeriodicDeductionDropdown(int MonthFrom, int MonthTo);
        Task<List<Dropdown>> GetPeriodicDeductionReportToMonthDropdown(int Month);
        Task<List<Dropdown>> GetEmpVsShiftAllowanceDropdown();
        Task<List<Dropdown>> GetUserDropdown();
        Task<List<Dropdown>> GetRecruitmentMenuDropdown();
        Task<List<Dropdown>> GetTransactionUserDropdown();
        Task<List<Dropdown>> GetTransactionMenuDropdown();

        Task<List<Dropdown>> HRAttendanceApprovalListAuthDropdown(string TabIndex, int LeavePeriodCode, int Level, int Month);
        Task<List<Dropdown>> GetTrainingGroupVsEmployeeDropdown();

        Task<List<Dropdown>> GetTrainingSubjectDropdown();
        Task<List<Dropdown>> GetReactivationTypeDropdown();
        Task<List<Dropdown>> GetPolicyDropdown();
        Task<List<Dropdown>> GetClubbingLeaveDropDown(int LeaveCode);
        Task<List<Dropdown>> GetPunchRegularizationReasonDropDown();
        Task<List<Dropdown>> GetSurveyTypeDropdown();
        Task<List<Dropdown>> GetReplyGroupDropdown();
        Task<List<Dropdown>> GetEmployeeTaskStatusDropDown();
        Task<List<Dropdown>> GetSurveyDropDown();
        Task<List<Dropdown>> GetEarnedLeaveDropdown(int LeaveGroup);
        Task<List<Dropdown>> GetRenewalsCategoryDropdown();
        Task<List<Dropdown>> GetLeaveBasedOnLaps(int LeaveGpCode);

        Task<List<Dropdown>> GetLearningGroup();

        Task<List<Dropdown>> GetLessonGroupDropdown();
        Task<List<Dropdown>> GetIncrementToolReasonDropDown();
        Task<List<Dropdown>> GetIncrementToolTransitionTypeDropdown();
        Task<List<Dropdown>> GetRoleOrEmpDropdown();
        Task<List<Dropdown>> GetLesson();
        Task<List<Dropdown>> GetVersionDropDown();
        Task<List<Dropdown>> GetVersionUpdateTypeDropDown();

        Task<List<Dropdown>> GetRemittanceTypeDropDown();
        Task<List<Dropdown>> GetRoundOffDropDown();
        Task<List<Dropdown>> GetCitytypeList();
        Task<List<Dropdown>> RateCalculationType();
        Task<List<Dropdown>> LeaveApplicationHistoryLeaveDropdown(string type, Guid EmployeeGI);
        Task<List<Dropdown>> GetDocumentType();
        Task<List<Dropdown>> GetClassificationTypeDropdown();
        Task<List<Dropdown>> GetClassification_Dropdown(int GroupingCode);
        Task<List<Dropdown>> GetResignationStatusDropdown();
        Task<List<Dropdown>> GetUserVsGroupCompanyDropDown();
        Task<List<Dropdown>> GetTransitionTypeDropdown();
        Task<List<Dropdown>> GetMailAlertTypeDropDown();
        Task<List<Dropdown>> GetAttendanceSuspensionDropDown();
        Task<List<Dropdown>> GetSalaryPerDayAllowanceDropDown();
        Task<List<Dropdown>> GetLearningReplyGroupDropdown();
        Task<List<Dropdown>> GetQuestionTemplateDropdown();
        Task<List<Dropdown>> GetLessonDropdown();
        Task<List<Dropdown>> GetTestLevelDropdown();
            Task<List<Dropdown>> GetLearningReplyDropdown(int replyGroupCode);
        Task<List<Dropdown>> GetReplyCombo(int replyGroupCode);
        Task<List<Dropdown>> GetKeyPositionRiskDropdown();
        Task<List<Dropdown>> GetTrainingReplyDropdown(int ReplygroupCode);
        Task<List<Dropdown>> GetTrainingReplyGroupDropdown();
        Task<List<Dropdown>> GetFullDayHalfDayStatusDropDown();
        Task<List<Dropdown>> GetCOFFEntitlesBasedonDayStatusDropDown(Guid EmployeeGI, string Date, string Mode, int FH, int SH);

        Task<List<Dropdown>> GetTrainingPreTestDropDown();
        Task<List<Dropdown>> GetTrainingPostTestDropDown();
        Task<List<Dropdown>> GetSurveyCodeDropDown();
        Task<List<Dropdown>> GetLearningCourse();
        Task<List<Dropdown>> GetCompanyProfileGroupCompanyDropDown();
        Task<List<Dropdown>> GetPrivilegeType();
        Task<List<Dropdown>> GetCommitmentType();

        Task<List<Dropdown>> GetFaqSoftwareDropDown();
        Task<List<Dropdown>> GetFaqCategoryDropDown();

        Task<List<Dropdown>> GetAttendanceDropdown();
        Task<List<Dropdown>> GetCommitteeDropDown();
        Task<List<Dropdown>> GetMemberTypeDropDown();
        Task<List<Dropdown>> GetLoanTypeDetailsDropdown();
        Task<List<Dropdown>> GetSalaryAdvance_LoanTypeDropdown();

        Task<List<Dropdown>> GetLocationVsEsiDropDown(int? LocationCode);
        Task<List<Dropdown>> GetLocationVsPTgroupDropDown(int? LocationCode);
        Task<List<Dropdown>> GetTargetGroupDropdown();
        Task<List<Dropdown>> GetTargetCategoryDropdown();
        Task<List<Dropdown>> GetAppraisalLevelDropdown(int HeadCode);
        Task<List<Dropdown>> GetAllRegionsDropDown();
        Task<List<Dropdown>> GetConsoleTypeDropDown();
        Task<List<Dropdown>> GetConsoleTypesDropDown();

        Task<List<Dropdown>> GetEconsoleCategoryDropDown();
        Task<List<Dropdown>> GetSubDepartmentDropDown();
        Task<List<Dropdown>> GetFunctionDropDown();
        Task<List<Dropdown>> GetGCVsPayrollGroup(int GcCode);
        Task<List<Dropdown>> GetWageGradeVsSalaryBand(int WageGradeCode);
        #region ROlE MATRIX
        Task<List<Dropdown>> GetRoleDropdown();
        Task<List<Dropdown>> GetCountryVsLocationDropDown(int? CountryCode);

        Task<List<Dropdown>> GetRoleClassificationDropdown();
        #endregion
        Task<List<Dropdown>> GetEventDropDown();

        Task<List<Dropdown>> GetNewPositionDropdown(int Designation, int Location, int Departmnet);
        Task<List<Dropdown>> GetReplacementEmployeeDropdown(int Designation, int Location, int Departmnet,string Type);

        Task<List<Dropdown>> GetIncentiveCostCenterDropdown(int Applicability);
        Task<List<Dropdown>> GetRoleMatrixRoleDropdown();
        Task<List<Dropdown>> GetWorkFlowDropdown();
        Task<List<Dropdown>> GetTemplateDropdown();
        Task<List<Dropdown>> GetApproverRolesDropdown();
        Task<List<Dropdown>> GetRecruitmentRoleDropdown();
        Task<List<Dropdown>> GetAllowanceWithSalaryStructureDropdown(int SalaryStructurecode);
        Task<List<Dropdown>> GetIncentiveCategoryDropdown();
        Task<List<Dropdown>> GetBnkStatemntRptFormatDropdown();

        Task<List<Dropdown>> GetDepartmentNAASDropdown();
        Task<List<Dropdown>> GetPurposeNAASDropdown(int DepartmentCode);
        Task<List<Dropdown>> GetCertificationReimbursementMonthDropdown();
        Task<List<Dropdown>> GetAdditionalResponsibilityDropdown();
        Task<List<Dropdown>> GetStatusDropdown();

        //Elearning
        Task<List<Dropdown>> GetCourseGroupDropdown();
        Task<List<Dropdown>> GetEmpClassReportFilterDropdown(string classification);
        Task<List<Dropdown>> GetEvaluationPeriod();
        Task<List<Dropdown>> GetDisplayRange();
        Task<List<Dropdown>> GetLessonVsCourseGroupDropdown(int LessonCode);
        Task<List<Dropdown>> GetLearningDocumentLessonDropdown();
        Task<List<Dropdown>> GetLessonVsAssessmentDropdown(int LessonCode);
        Task<List<Dropdown>> GetLearningReplyDropdown();
        Task<List<Dropdown>> GetLearningAnswerDropdown(int? QuestionCode);
        Task<List<Dropdown>> GetLessonVsCourseDropdown(string Course);
        Task<List<Dropdown>> GetAssessmentVsLessonDropdown(int AssessmentType);
        Task<List<Dropdown>> GetContentReportLessonCategoryDropdown();
        Task<List<Dropdown>> GetAssessmentDropdown();
        Task<List<Dropdown>> GetAssessmentReportLessonDropdown();
        Task<List<Dropdown>> GetCompanyDocumentTypeDropdown();
        Task<List<Dropdown>> GetCompanyDocumentDropdown(int DocType);
        Task<List<Dropdown>> GetSpecialHolidayDropdown(int YearCode);
        Task<List<Dropdown>> GetAllowanceProvisionDropdown();
        Task<List<Dropdown>> GetBankPostedMonthDropdown(String Type);
        Task<List<Dropdown>> GetExternalSalaryProcessingMonthDropdown(int? PayrollGroupCode);
        Task<List<Dropdown>> GetExternalPayrollReportMonthDropdown();
        Task<List<Dropdown>> GetJVPostingMonthDropdown(int Type, int PayrollGroupCode);
        Task<List<Dropdown>> GetRecoveryAllowanceDropdown();
        Task<List<Dropdown>> GetPayrollGroupWithGrpCmpyCheckDropdown();
        public Task<List<Dropdown>> GetERPAccountHeadCombo();
        public Task<List<Dropdown>> GetProductDropdown();
        public Task<List<Dropdown>> GetSubProductDropdown();
        public Task<List<Dropdown>> GetTDSAssessmentYearMonthDropdown(int AssessmentYearCode);
        public Task<List<Dropdown>> GetChangeAuthorityWorkFlowDropdown();
        Task<List<Dropdown>> GetMasterJobDropdown();
        Task<List<Dropdown>> GetMetroDropDown();
        Task<List<Dropdown>> GetLocationCategoryDropDown();

        Task<List<Dropdown>> GetBusinessTypeDropDown();
        Task<List<Dropdown>> GetZoneDropDown();

        Task<List<Dropdown>> GetclusterDropDown();
        Task<List<Dropdown>> GetDomainDropDown();

        public Task<List<Dropdown>> GetRecruitmentVenueDropdown();
        public Task<List<Dropdown>> GetRecruitmentExpenseTypeDropdown();

        public Task<List<Dropdown>> GetWorkModeDropdown();
        public Task<List<Dropdown>> GetExperienceLevelDropdown();
        public Task<List<Dropdown>> GetJobEmploymentTypeDropdown();
        public Task<List<Dropdown>> GetJobAreaDropdown();
        public Task<List<Dropdown>> GetJobQualificationDropdown();

    }
}

