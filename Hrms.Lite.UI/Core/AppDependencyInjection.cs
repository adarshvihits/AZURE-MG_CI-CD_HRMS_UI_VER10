using Hrms.Lite.Infrastructure.Helpers;
using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.Account;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Services.IServices.Master;
using Hrms.Lite.Services.IServices.PayRoll;
using Hrms.Lite.Services.IServices.TimeOffice;
using Hrms.Lite.Services.Services;
using Hrms.Lite.Services.Services.Account;
using Hrms.Lite.Services.Services.DataBank;
using Hrms.Lite.Services.Services.General;
using Hrms.Lite.Services.Services.Master;
using Hrms.Lite.Services.Services.PayRoll;
using Hrms.Lite.Services.Services.TimeOffice;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using Hrms.Lite.Services.IServices.Home;
using Hrms.Lite.Services.Services.Home;
using Hrms.Lite.Services.IServices.DataBank.Reports;
using Hrms.Lite.Services.Services.DataBank.Reports;
using Hrms.Lite.Services.Services.TimeOffice.Report;
using Hrms.Lite.Services.IServices.TimeOffice.Report;
using Hrms.Lite.Services.IServices.PayRoll.Reports;
using Hrms.Lite.Services.Services.PayRoll.Reports;
using Hrms.Lite.Services.IServices.DashBoard;
using Hrms.Lite.Services.Services.DashBoard;
using Hrms.Lite.Services.IServices.Recruitment;
using Hrms.Lite.Services.Services.Recruitment;
using Hrms.Lite.Services.IServices.MIS;
using Hrms.Lite.Services.Services.MIS;

using IOnBoardingVacancyStatusReportService = Hrms.Lite.Services.IServices.DataBank.Reports.IVacancyStatusReportService;
using OnBoardingVacancyStatusReportService = Hrms.Lite.Services.Services.DataBank.Reports.VacancyStatusReportService;

using Hrms.Lite.Services.IServices.TDS;
using Hrms.Lite.Services.Services.TDS;
using Hrms.Lite.Services.IServices.TDS.Reports;
using Hrms.Lite.Services.Services.TDS.Reports;
using Hrms.Lite.Services.Services.TrainingModule;
using Hrms.Lite.Services.IServices.TrainingModule;



using Hrms.Lite.Services.IServices.Admin;
using Hrms.Lite.Services.Services.Admin;
using Hrms.Lite.Services.Services.TrainingModule.Report;
using Hrms.Lite.Services.IServices.TrainingModule.Report;




namespace Hrms.Lite.UI.Core
{
    public static class AppDependencyInjection
    {
        public static void AddDependencyInjectionServices(this IServiceCollection services)
        {
            #region General
            services.AddTransient<IServiceBase, ServiceBase>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IHomeService, HomeService>();
            services.AddTransient<IDropdownService, DropdownService>();
            services.AddTransient<ICommonService, CommonService>();
            services.AddTransient<IMenuService, MenuService>();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.TryAddSingleton<IBlob, Blob>();
            services.TryAddSingleton<IAWSBucket, AWSBucket>();
            services.TryAddSingleton<IHelpers, Helpers>();
            services.AddTransient<IReportBuilderService, ReportBuilderService>();           
            services.AddTransient<IPasswordSettingsService,PasswordSettingsService>();
            services.AddTransient<ICipherService,CipherService>();
            #endregion

            #region Admin
            services.AddTransient<IHrSettingsService, HrSettingsService>();
            services.AddTransient<IDocumentRenewalService, DocumentRenewalService>();
            services.AddTransient<IOrganizationTaskService, OrganizationTaskService>();
            services.AddTransient<IPhotoGalleryService, PhotoGalleryService>();

            
            #region Settings
            services.AddTransient<IRenewalsCategoryService, RenewalsCategoryService>();
            services.AddTransient<IDocumentCategoryService, DocumentCategoryService>();

            #endregion
            #region Transaction
            services.AddTransient<IDocumentsFolderService, DocumentsFolderService>();

            #endregion
            #endregion

           
            #region DashBoard
            services.AddTransient<IDataBankDashBoardService, DataBankDashBoardService>();
            services.AddTransient<ITimeOfficeDashBoardService, TimeOfficeDashBoardService>();
            services.AddTransient<IPayRollDashBoardService, PayRollDashBoardService>();
            services.AddTransient<IRecruitmentDashBoardService, RecruitmentDashBoardService>();
            services.AddTransient<ISettingsDashBoardService, SettingsDashBoardService>();
            services.AddTransient<ILandingPageService, LandingPageService>();
            services.AddTransient<IAdminDashBoardService, AdminDashBoardService>();
            services.AddTransient<ISuccessionDashBoardService, SuccessionDashBoardService>();
            services.AddTransient<IHRPlanningDashBoardService, HRPlanningDashBoardService>();
            services.AddTransient<ITravelClaimDashboardService, TravelClaimDashboardService>();
            #endregion

            #region Master
            services.AddTransient<IDepartmentService, DepartmentService>();
            services.AddTransient<ICountryService, CountryService>();
            services.AddTransient<IStateService, StateService>();
            services.AddTransient<IDistrictService, DistrictService>();
            services.AddTransient<IDesignationService, DesignationService>();
            services.AddTransient<IUserRoleService, UserRoleService>();
            services.AddTransient<IRewardsService, RewardsService>();
            services.AddTransient<IAssetService, AssetService>();
            services.AddTransient<IDisciplinaryService, DisciplinaryService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IGradeService, GradeService>();
            services.AddTransient<IWageGradeService, WageGradeService >();
            services.AddTransient<IAppraisalTypeMasterService, AppraisalTypeMasterService>();
            services.AddTransient<ITrainingMasterService, TrainingMasterService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IEvaluationPeriodService, EvaluationPeriodService>();
            services.AddTransient<ILocationService, LocationService>();
            services.AddTransient<IJobTransitionReasonService, JobTransitionReasonService>();
            services.AddTransient<IEditEmployeeInfoService, EditEmployeeInfoService>();
            services.AddTransient<IDivisionService, DivisionService>();
            services.AddTransient<IEmploymentTypeService, EmploymentTypeService>();
            services.AddTransient<ISectionService, SectionService>();
            services.AddTransient<IWorkLocationService, WorkLocationService>();
            services.AddTransient<ISkillService, SkillService>();
            services.AddTransient<IResignationTypeService, ResignationTypeService>();
            services.AddTransient<IESIGroupService, ESIGroupService>();
            services.AddTransient<IHrPlanningMasterService, HrPlanningMasterService>();
            services.AddTransient<IPlanningApprovalService, PlanningApprovalService>();
            services.AddTransient<IHobbiesService, HobbiesService>();
            services.AddTransient<IRegionService, RegionService>();
            services.AddTransient<IBankService, BankService>();
            services.AddTransient<IFieldOfStudyService, FieldOfStudyService>();
            services.AddTransient<IAllowanceService, AllowanceService>();
            services.AddTransient<IResignationReasonService, ResignationReasonService>();
            services.AddTransient<IDeductionService, DeductionService>();
            services.AddTransient<IQualificationService, QualificationService>();
            services.AddTransient<IPayrollGroupService, PayrollGroupService>();
            services.AddTransient<IPTGroupService, PTGroupService>();
            services.AddTransient<IShiftService, ShiftService>();
            services.AddTransient<IAssetCategoryService, AssetCategoryService>();
            services.AddTransient<ILanguagesService, LanguagesService>();
            services.AddTransient<IFieldOfCertificationService, FieldOfCertificationService>();
            services.AddTransient<IDisciplinaryIncidentsActionService, DisciplinaryIncidentsActionService>();
            services.AddTransient<ILeaveGroupService, LeaveGroupService>();
            services.AddTransient<IHolidayGroupService, HolidayGroupService>();
            services.AddTransient<IHolidaySerivce, HolidayService>();
            services.AddTransient<ILeavePeriodService, LeavePeriodService>();
            services.AddTransient<IAttendanceStatusService, AttendanceStatusService>();
            services.AddTransient<ILoanService, LoanService>();
            services.AddTransient<IReaderService, ReaderService>();
            services.AddTransient<IExitChecklistService, ExitChecklistService>();
            services.AddTransient<IJoiningCheckListService, JoiningCheckListService>();
            services.AddTransient<ISkillTypeService, SkillTypeService>();
            services.AddTransient<IInterviewTypeService, InterviewTypeService>();
            services.AddTransient<IYearlyOffdaySettingsService, YearlyOffdaySettingsService>();
            services.AddTransient<IPFGroupService, PFGroupService>();
            services.AddTransient<IFinancialYearService, FinancialYearService>();
            services.AddTransient<IKeyPositionService, KeyPositionService>();
            services.AddTransient<IExitQuestionnaireService, ExitQuestionnaireService>();
            services.AddTransient<IExitInterviewGroupService, ExitInterviewGroupService>();
            services.AddTransient<ISalaryStructureService, SalaryStructureService>();
            services.AddTransient<ILeaveRuleService, LeaveRuleService>();
            services.AddTransient<IAttendanceRequestReasonService, AttendanceRequestReasonService>();
            services.AddTransient<IMedicalCheckupTypeService, MedicalCheckupTypeService>();
            services.AddTransient<IProductCompanyService, ProductCompanyService>();
            services.AddTransient<ISalaryPeriodLockService, SalaryPeriodLockService>();
            services.AddTransient<IRecruitmentSettingsService, RecruitmentSettingsService>();
            services.AddTransient<IResumeSourceService, ResumeSourceService>();
            services.AddTransient<IDueClearanceSettingsService, DueClearanceSettingsService>();
            services.AddTransient<ICircleService, CircleService>();
            services.AddTransient<ICoffRuleService, CoffRuleService>();
            services.AddTransient<IGrievanceCategoryService, GrievanceCategoryService>();
            services.AddTransient<ITimeSheetActivityService, TimeSheetActivityService>();
            services.AddTransient<ITimeSheetProjectService, TimeSheetProjectService>();
            services.AddTransient<IAllowanceDeductionVsAccountHeadService, AllowanceDeductionVsAccountHeadService>();
            services.AddTransient<IAccountsHeadService, AccountsHeadService>();
            services.AddTransient<ICostCenterService, CostCenterService>();
            services.AddTransient<IInsurancePolicyService, InsurancePolicyService>();
            services.AddTransient<IVDAGroupService, VDAGroupService>();
            services.AddTransient<IAssessmentYearService, AssessmentYearService>();
            services.AddTransient<IQuickLinksService, QuickLinksService>();
            services.AddTransient<IRelievingLetterService, RelievingLetterService>();
            services.AddTransient<IOfferLetterReportService, OfferLetterReportService>();
            services.AddTransient<IWPSAccountHeadService, WPSAccountHeadService>();
            services.AddTransient<ICompanyGeneralSettingsService, CompanyGeneralSettingsService >();
            services.AddTransient<IQuoteOfDayServices, QuoteOfDayServices>();
            services.AddTransient<IDutyRosterLockService, DutyRosterLockService>();
            services.AddTransient<ITrainingCategoryService, TrainingCategoryService>();
            services.AddTransient<IHRLetterSettingsService, HRLetterSettingsService>();
            services.AddTransient<ITrainingSubjectService, TrainingSubjectService>();
            services.AddTransient<IAllowanceVsArrearAllowanceService, AllowanceVsArrearAllowanceService>();
            services.AddTransient<IBonusGroupService, BonusGroupService>();
            services.AddTransient<IEmpVsMobileRegistrationService, EmpVsMobileRegistrationService>();
            services.AddTransient<IFireBaseImageSettingsService, FireBaseImageSettingsService>();
            services.AddTransient<IEmployeeVsMobileRestrictionService, EmployeeVsMobileRestrictionService>();
            services.AddTransient<IEditEmployeeInfoApproveService, EditEmployeeInfoApproveService>();
            services.AddTransient<IVersionUpdateService, VersionUpdateService>();
            services.AddTransient<IRoundOffSettingsService, RoundOffSettingsService>();
            services.AddTransient<IExitAnalysisReportService, ExitAnalysisReportService>();
            services.AddTransient<IRewardReferingService, RewardReferingService>();
            services.AddTransient<IRequestToHRTypeService, RequestToHRTypeService>();
            services.AddTransient<IEmployeeDocumentTypeService, EmployeeDocumentTypeService>();
            services.AddTransient<IUserApplicabilityAdvancedUserService, UserApplicabilityAdvancedUserService>();
            services.AddTransient<IFaqCategoryService, FaqCategoryService>();
            services.AddTransient<IFaqService, FaqService>();
            services.AddTransient<ICommitmentTypeService, CommitmentTypeService>();
            services.AddTransient<ICommitmentService, CommitmentService>();
            services.AddTransient<ISubDepartmentService, SubDepartmentService>();
            services.AddTransient<IFunctionService, FunctionService>();
            services.AddTransient<IIncentiveCostCenterService, IncentiveCostCenterService>();
            services.AddTransient<IFunctionalStateService, FunctionalStateService>();
            services.AddTransient<IDepartmentCostCenterService, DepartmentCostCenterService>();
            services.AddTransient<IERPCostCenterService, ERPCostCenterService>();
            services.AddTransient<IProfitCenterService, ProfitCenterService>();
            services.AddTransient<IERPAccountHeadService, ERPAccountHeadService>();
            services.AddTransient<IConsultancyService, ConsultancyService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IApproverRoleService, ApproverRoleService>();
            services.AddTransient<IRecruitmentReasonService, RecruitmentReasonService>();
            services.AddTransient<ICompanyVsPayrollGroupService, CompanyVsPayrollGroupService>();
            services.AddTransient<IIncentiveTypeService, IncentiveTypeService>();
            services.AddTransient<IproductService, ProductService>();
            services.AddTransient<ISubProductService, SubProductService>();
            services.AddTransient<IRoleMatrixService, RoleMatrixService>();
            services.AddTransient<IPerquisiteService, PerquisiteService>();
            services.AddTransient<INightAllowancePurposeService, NightAllowancePurposeService>();
            services.AddTransient<INightAllowanceAmountSettingsService, NightAllowanceAmountSettingsService>();
            services.AddTransient<IDuesClearanceAuthorityMatrixService, DuesClearanceAuthorityMatrixService>();
            services.AddTransient<ICompanyDocumentsService, CompanyDocumentsService>();
            services.AddTransient<IActivityService, ActivityService>();
            services.AddTransient<ISpecialHolidayApplicabilityService, SpecialHolidayApplicabilityService>();
            services.AddTransient<IAllowanceDeductionVsErpAccountHeadService, AllowanceDeductionVsErpAccountHeadService>();
            services.AddTransient<IMonthlyProfitBasedSettingsService, MonthlyProfitBasedSettingsService>();
            services.AddTransient<IJobDescriptionService, JobDescriptionService>();
            services.AddTransient<IBussinessTypeService, BussinessTypeService>();
            services.AddTransient<IClusterService, ClusterService>();
            services.AddTransient<IRecruitmentdriveService, RecruitmentdriveService>();
            services.AddTransient<IVenueService, VenueService>();
            services.AddTransient<IZoneService, ZoneService>();
            services.AddTransient<IJobMasterService, JobMasterService>();



            #endregion

            #region Databank
            services.AddTransient<IEmployeeVsBankDetailsApprovalService, EmployeeVsBankDetailsApprovalService>();
            services.AddTransient<IStaffConfirmationReportService, StaffConfirmationReportService>();
            services.AddTransient<ICertificatePolicyService, CertificatePolicyService>();
            services.AddTransient<IOnBoardingService, OnBoardingService>();
            services.AddTransient< IPreEnrollmentService ,PreEnrollmentService >();
            services.AddTransient<IEnrollmentService, EnrollmentService>();
            services.AddTransient<IJoiningConfirmationService, JoiningConfirmationService>();
            services.AddTransient<IPersonalDetailsService, PersonalDetailsService>();
            services.AddTransient<IAssetAllocationService, AssetAllocationService>();
            services.AddTransient<IRewardsAndRecognitionService, RewardsAndRecognitionService>();
            services.AddTransient<IDisciplinaryIncidentsService, DisciplinaryIncidentsService>();
            services.AddTransient<ITrainingService, TrainingService>();
            services.AddTransient<IAppraisalService, AppraisalService>();
            services.AddTransient<IIndustryBenchmarkSalaryService, IndustryBenchmarkSalaryService>();
            services.AddTransient<IHRPlannningService, HRPlannningService>();
            services.AddTransient<IHRPlanningBudgetService, HRPlanningBudgetService>();            
            services.AddTransient<IDocumentUploadService, DocumentUploadService>();
            services.AddTransient<IJobTransitionService, JobTransitionService>();
            services.AddTransient<IRequestVsAuthorityService, RequestVsAuthorityService>();
            services.AddTransient<IResignationService, ResignationService>();
            services.AddTransient<IEmployeeProfileService, EmployeeProfileService>();
            services.AddTransient<ICompanyProfileService, CompanyProfileService>();
            services.AddTransient<ITalentPoolService, TalentPoolService>();
            services.AddTransient<IJoiningConfirmationService, JoiningConfirmationService>();
            services.AddTransient<IResumeDataBankService, ResumeDataBankService>();
            services.AddTransient<IPersonalDetailsApproveService, PersonalDetailsApproveService>();
            services.AddTransient<IApplicantTrackingService, ApplicantTrackingService>();
            services.AddTransient<IVacancyManagementService, VacancyManagementService>();
            services.AddTransient<ISuccessionPlanningService, SuccessionPlanningService>();
            services.AddTransient<IGrievanceAuthorityService, GrievanceAuthorityService>();
            services.AddTransient<IMedicalCheckupService, MedicalCheckupService>();
            services.AddTransient<IPoliciesCircularService, PoliciesCircularService>();
            services.AddTransient<IPoliceVerificationsService, PoliceVerificationsService>();
            services.AddTransient<IMRFVsResumeService, MRFVsResumeService>();

            services.AddTransient<IJobPostingService, JobPostingService>();
            services.AddTransient<IVacancyManagementApprovalService, VacancyManagementApprovalService>();
            services.AddTransient<IVacancyCancellationService, VacancyCancellationService>();
            services.AddTransient<IBranchGradingService, BranchGradingService>();
            services.AddTransient<IWalkInInterviewService, WalkInInterviewService>();
            services.AddTransient<ILifeEventsService, LifeEventsService>();
            services.AddTransient<IInductionTrainingService, InductionTrainingService>();
            services.AddTransient<ITrainingCompletionService, TrainingCompletionService>();
            services.AddTransient<IEmployeeVsCostCenterService, EmployeeVsCostCenterService>();
            services.AddTransient<IEmployeeVsVDAGroupService, EmployeeVsVDAGroupService>();
            services.AddTransient<IEmployeeVsVDAGroupService, EmployeeVsVDAGroupService>();
            services.AddTransient<ISkillRequisitionService, SkillRequisitionService>();
            services.AddTransient<IEmployeeDocumentDownloadService, EmployeeDocumentDownloadService>();
            services.AddTransient<ISocialWallService, SocialWallService>();
            services.AddTransient<IEmployeeVsGeoTaggingService, EmployeeVsGeoTaggingService>();
            services.AddTransient<IEmployeeCustomFieldService, EmployeeCustomFieldService>();
            services.AddTransient<IDataBulkUploadingService, DataBulkUploadingService>();
            services.AddTransient<IAbscountingEmployeeTerminationService, AbscountingEmployeeTerminationService>();
            services.AddTransient<IOfferService, OfferService>();
            services.AddTransient<IEmployeeRejoinService, EmployeeRejoinService>();
            services.AddTransient<IHRAnnouncementService, HRAnnouncementService>();
            services.AddTransient<ICandidatePreEnrolLinkService, CandidatePreEnrolLinkService>();  
            services.AddTransient<IUHIDService, UHIDService>();
            services.AddTransient<IEmployeeMoodServices, EmployeeMoodServices>();
            services.AddTransient<IEmpVsAppliAllowanceOrDeductionService, EmpVsAppliAllowanceOrDeductionService>();
            services.AddTransient<IVacancyClosingService, VacancyClosingService>();
            services.AddTransient<IEmployeeVsBonusGroupService, EmployeeVsBonusGroupService>();
            services.AddTransient<IEmployeeVsAppraisalDateService, EmployeeVsAppraisalDateService>();
            services.AddTransient<IEmployeeVsWorkLocationService, EmployeeVsWorkLocationService>();
            services.AddTransient<ICoreTeamService, CoreTeamService>();
            services.AddTransient<ILocationMarkingApplicabilityService, LocationMarkingApplicabilityService>();
            services.AddTransient<ITaskReportService, TaskReportService>();
            services.AddTransient<IHRDuesClearanceService, HRDuesClearanceService>();
            services.AddTransient<ISalaryIncrementToolService, SalaryIncrementToolService>();
            services.AddTransient<IInstantMessagingService, InstantMessagingService>();
            services.AddTransient<INewsFeedService, NewsFeedService>();
            services.AddTransient<ISocialWallReportService, SocialWallReportService>();
            services.AddTransient<IWorkUnitTransferRequestApprovalService, WorkUnitTransferRequestApprovalService>();
            services.AddTransient<IWorkUnitVsAuthoritySettingsService, WorkUnitVsAuthoritySettingsService>();
            services.AddTransient<IEmployeeFeedbackService, EmployeeFeedbackService>();
            services.AddTransient<IIncidentReportService, IncidentReportService>();
            services.AddTransient<Ip2pRecognitionService, p2pRecognitionService>();
            services.AddTransient<IRequestToHRService, RequestToHRService>();
            services.AddTransient<ISuspensionService, SuspensionService>();
            services.AddTransient<IEconsoleService, EconsoleService>();
            services.AddTransient<IDepartmentWiseCostCenterService, DepartmentWiseCostCenterService>();
            services.AddTransient<IDepartmentWiseERPCostCenterService, DepartmentWiseERPCostCenterService>();
            services.AddTransient<IInterviewSchedulingService, InterviewSchedulingService>();
            services.AddTransient<IDepartmentvsDeptHeadService, DepartmentvsDeptHeadService>();
            services.AddTransient<IResumeProfileService, ResumeProfileService>();
            services.AddTransient<IBulkLeaveApplicablityService, BulkLeaveApplicablityService>();
            services.AddTransient<ICommitmentsGivenService, CommitmentsGivenService>();
            services.AddTransient<IPositionManagementService, PositionManagementService>();
            services.AddTransient<IPositionManagerService, PositionManagerService>();
            services.AddTransient<IDepartmentWiseProfitCenterService, DepartmentWiseProfitCenterService>();
            services.AddTransient<IEventGroupService, EventGroupService>();
            services.AddTransient<IJobTransitionTypeService, JobTransitionTypeService>();
            services.AddTransient<IApprovalMatrixService, ApprovalMatrixService>();
            services.AddTransient<IEmpConfirmationService, EmpConfirmationService>();
            services.AddTransient<IRecruitmentMatrixService, RecruitmentMatrixService>();
            services.AddTransient<IEmployeeTransferService, EmployeeTransferService>();
            services.AddTransient<IAdditionalResponsibilityService, AdditionalResponsibilityService>();
            services.AddTransient<IDealerUndertakingService, DealerUndertakingService>();
            services.AddTransient<IDealerUndertakingApprovalService, DealerUndertakingApprovalService>();
            services.AddTransient<IReassignmentMovementReportService, ReassignmentMovementReportService>();
            services.AddTransient<IEmployeeVsGratuityService, EmployeeVsGratuityService>();
            services.AddTransient<IEmployeeVsNpsService, EmployeeVsNpsService>();
            services.AddTransient<IADDeactivateService, ADDeactivateService>();
            services.AddTransient<ICompanyDocumentUploadService, CompanyDocumentUploadService>();
            services.AddTransient<IReassignmentIncentiveReportService, ReassignmentIncentiveReportService>();
            services.AddTransient<IMailAlertSettingsService, MailAlertSettingsService>();
            services.AddTransient<IStaffDeclarationAuthorityService, StaffDeclarationAuthorityService>();
            services.AddTransient<IWageGradeVsSumAssuredService, WageGradeVsSumAssuredService>();
            services.AddTransient<IEmployeeVsReportingManagerService, EmployeeVsReportingManagerService>();
            services.AddTransient<IEmployeeProvisionUpdateService, EmployeeProvisionUpdateService>();
            services.AddTransient<IEmployeeReferenceSchemeService, EmployeeReferenceSchemeService>();
            services.AddTransient<IEmployeeBGVService, EmployeeBGVService>();
            services.AddTransient<IPreEnrollmentEditService, PreEnrollmentEditService>();
            services.AddTransient<IEmployeeVsRenewalDateService, EmployeeVsRenewalDateService>();
            services.AddTransient<IChangeAuthorityService, ChangeAuthorityService>();
            services.AddTransient<ICompanyLettersDetailsService, CompanyLettersDetailsService>();
            services.AddTransient<IPlanningPeriodService, PlanningPeriodService>();
            services.AddTransient<INewBranchPositionCreationService, NewBranchPositionCreationService>();
            services.AddTransient<IJobPostingConfirmationService, JobPostingConfirmationService>();
           
            #region Reports
            services.AddTransient<IBloodBankReportService, BloodBankReportService>();
            services.AddTransient<IJoiningReportService, JoiningReportService>();
            services.AddTransient<ISalaryStructureReportService, SalaryStructureReportService>();
            services.AddTransient<ISeparationReportService, SeparationReportService>();
            services.AddTransient<IRenewalInformationService, RenewalInformationService>();
            services.AddTransient<IEmployeeGroupSummaryReportService, EmployeeGroupSummaryReportService>();
            services.AddTransient<ICelebrityListService,CelebrityListService>();
            services.AddTransient<IJobTransitionReportService, JobTransitionReportService>();
            services.AddTransient<IAttritionReportService, AttritionReportService>();
            services.AddTransient<IEmployeeInformationReportService, EmployeeInformationReportService>();
            services.AddTransient<IGrowthHistoryService, GrowthHistoryService>();
            services.AddTransient<IServiceRecordService, ServiceRecordService>();
            services.AddTransient<IAbscondingReportService, AbscondingReportService>();
            services.AddTransient<IEmployeeTransferReportService, EmployeeTransferReportService>();
            services.AddTransient<IResignationTrackerReportService, ResignationTrackerReportService>();
            services.AddTransient<IProfileCTCReportService, ProfileCTCReportService>();
            services.AddTransient<IExitQuestionnaireAnalysisService, ExitQuestionnaireAnalysisService>();
            services.AddTransient<IEmployeeCompanyDocumentsService, EmployeeCompanyDocumentsService>();
            services.AddTransient<IOrganizationalChartService, OrganizationalChartService>();
            services.AddTransient<IGrievanceReportService, GrievanceReportService>();
            services.AddTransient<IInductionTrainingDetailedReportService, InductionTrainingDetailedReportService>();
            services.AddTransient<IOnBoardingVacancyStatusReportService, OnBoardingVacancyStatusReportService>();
            services.AddTransient<ISkillMatrixService, SkillMatrixService>();
            services.AddTransient<ISkillVarianceService, SkillVarianceService>();
            services.AddTransient<ISalaryIncrementLetterService, SalaryIncrementLetterService>();
            services.AddTransient<ICanteenDateWiseReportService, CanteenDateWiseReportService>();
            services.AddTransient<IReporteesService, ReporteesService>();
            services.AddTransient<IPeriodicalEmployeelistService, PeriodicalEmployeelistService>();
            services.AddTransient<IYearOfServiceReportService, YearOfServiceReportService>();
            services.AddTransient<IAppraisalDueListService, AppraisalDueListService>();
            services.AddTransient<IRetirementDueReportService, RetirementDueReportService>();
            services.AddTransient<IOnboardingChecklistReportService, OnboardingChecklistReportService>();
            services.AddTransient<IExitCheckListReportService, ExitCheckListReportService>();
            services.AddTransient<IExitInterviewReportService, ExitInterviewReportService>();
            services.AddTransient<IHRPolicyAcceptanceReportService, HRPolicyAcceptanceReportService>();
            services.AddTransient<IIDCardDetailsService, IDCardDetailsService>();
            services.AddTransient<ITransactionLogService, TransactionLogService>();
            services.AddTransient<IOfferAcceptanceReportService, OfferAcceptanceReportService>();
            services.AddTransient<ISalaryPerDayReportService, SalaryPerDayReportService>();
            services.AddTransient<ISalaryProjectionReportService, SalaryProjectionReportService>();
            services.AddTransient<ITransferTrackingReportService, TransferTrackingReportService>();
            services.AddTransient<IConfirmationTrackingReportService, ConfirmationTrackingReportService>();
            services.AddTransient<ICertificationTrackingReportService, CertificationTrackingReportService>();
            services.AddTransient<IRecruitmentTrackerReportService, RecruitmentTrackerReportService>();
            services.AddTransient<IDueClearenceTrackingReportService, DueClearenceTrackingReportService>();
            services.AddTransient<IAppointmentLetterSubmissionReportService, AppointmentLetterSubmissionReportService>();
            services.AddTransient<IEConsoleSubmissionReportService, EConsoleSubmissionReportService>();
            services.AddTransient<IReassignmentIncentiveReportService, ReassignmentIncentiveReportService>();
            services.AddTransient<IReassignmentMovementReportService, ReassignmentMovementReportService>();
            services.AddTransient<IIncentiveAutomationReportService, IncentiveAutomationReportService>();
            services.AddTransient<IDutyJoiningRptService, DutyJoiningRptService>();
            services.AddTransient<IDealerUnderTakingReportService, DealerUnderTakingReportService>();
            services.AddTransient<IEmployeeCertificationService, EmployeeCertificationService>();
            services.AddTransient<IReplacementOrNewExpReportService, ReplacementOrNewExpReportService>();
            services.AddTransient<IParentsMediclaimRequestReportService, ParentsMediclaimRequestReportService>();
            services.AddTransient<ISourceDetailsReportService, SourceDetailsReportService>();
            services.AddTransient<INightAllowanceTrackingService, NightAllowanceTrackingService>();
            #endregion


            #endregion

            #region TimeOffice
            services.AddTransient<IDailyAttendanceEntryService, DailyAttendanceEntryService>();
            services.AddTransient<ILeaveOpeningService, LeaveOpeningService>();
            services.AddTransient<ILeaveCreditDebitService, LeaveCreditDebitService>();
            services.AddTransient<ILeaveCarryForwardService, LeaveCarryForwardService>();
            services.AddTransient<ILeaveEncashService, LeaveEncashService>();
            services.AddTransient<IDutyRosterService, DutyRosterService>();
            services.AddTransient<IHRAttendanceApprovalService, HRAttendanceApprovalService>();
            services.AddTransient<IMonthlyAttendanceProcessingService, MonthlyAttendanceProcessingService>();
            services.AddTransient<ICoffCreditService, CoffCreditService>();
            services.AddTransient<IAttendanceWagesConfirmationService, AttendanceWagesConfirmationService>();
            services.AddTransient<IEmployeeWiseOffdaySettingsService, EmployeeWiseOffdaySettingsService>();
            services.AddTransient<IAttendanceSettingService, AttendanceSettingService>();
            services.AddTransient<IDailyAttendanceLogService, DailyAttendanceLogService>();  
            services.AddTransient<IMonthlyAttendanceUploadingService, MonthlyAttendanceUploadingService>();  
            services.AddTransient<IDailyOTConfirmationService, DailyOTConfirmationService>();  
            services.AddTransient<IPeriodicLeaveCreditService, PeriodicLeaveCreditService>();  
            services.AddTransient<ILeaveApplicationService, LeaveApplicationService>();
            services.AddTransient<ILeaveApplicationService, LeaveApplicationService>();
            services.AddTransient<IMaternityLeaveApplicationService, MaternityLeaveApplicationService>();
            services.AddTransient<ICoffBalanceRptService, CoffBalanceRptService>();
            services.AddTransient<ILeaveCancellationService, LeaveCancellationService>();
            services.AddTransient<IEmployeeVsMinPresentDaysService, EmployeeVsMinPresentDaysService>();
            services.AddTransient<IEmployeeVsShiftAllowanceService, EmployeeVsShiftAllowanceService>();
            services.AddTransient<IPunchingRegularizationApprovalService, PunchingRegularizationApprovalService>();
            services.AddTransient<IExceptionEmployeesService, ExceptionEmployeesService>();
            services.AddTransient<IPunchingRegularizationReportService, PunchingRegularizationReportService>();
            services.AddTransient<IEarnedLeaveEligibilityService, EarnedLeaveEligibilityService>();
            services.AddTransient<ILeaveLapsService, LeaveLapsService>();
            services.AddTransient<IHRAbsentRegularizationService, HRAbsentRegularizationService>();
            services.AddTransient<ILOPSandwichUpdationService, LOPSandwichUpdationService>();
            services.AddTransient<IManualReaderIntegrationService, ManualReaderIntegrationService>();
            services.AddTransient<IEarnedLeaveEligibilityBasedOnDOJService, EarnedLeaveEligibilityBasedOnDOJService>();
            services.AddTransient<IOndutyWfhSettingsService, OndutyWfhSettingsService>();
            services.AddTransient<IManualCOFFCreditService, ManualCOFFCreditService>();
            services.AddTransient<IAbscondingIntimationService, AbscondingIntimationService>();
            services.AddTransient<IPLCreditService, PLCreditService>();
            #region Reports
            services.AddTransient<IMusteRollReportService, MusteRollReportService>();
            services.AddTransient<ILeaveRegisterService, LeaveRegisterService>();
            services.AddTransient<IDailyAttendanceStatusReportService, DailyAttendanceStatusReportService>();
            services.AddTransient<ILeaveApplicationReportService, LeaveApplicationReportService>();
            services.AddTransient<IReaderLogReportService, ReaderLogReportService>();
            services.AddTransient<IAbsenteeismReportService, AbsenteeismReportService>();
            services.AddTransient<IMobilePunchingLogReportService, MobilePunchingLogReportService>();
            services.AddTransient<IDailyAttendanceReportService, DailyAttendanceReportService>();
            services.AddTransient<IMonthlyAttendanceSummaryReportService, MonthlyAttendanceSummaryReportService>();
            services.AddTransient<ILivePunchReportService, LivePunchReportService>();
            services.AddTransient<IMaternityEsiLeaveReportService, MaternityEsiLeaveReportService>();
            services.AddTransient<IWeekOffAdjustedReportService, WeekOffAdjustedReportService>();
            services.AddTransient<IEmployeeVsLeaveDetailsReportService, EmployeeVsLeaveDetailsReportService>();
            services.AddTransient<IMonthlyWorkHourReportService, MonthlyWorkHourReportService>();
            services.AddTransient<IEmpMovementRptService, EmpMovementRptService>();
            services.AddTransient<ILeaveBalanceAsOnDateService, LeaveBalanceAsOnDateService>();
            services.AddTransient<ILessHourReportService, LessHourReportService>();
            services.AddTransient<IDailyPerformanceService, DailyPerformanceService>();

            #endregion
            #endregion

            #region PayRoll
            services.AddTransient<IReferralIncentiveReportService, ReferralIncentiveReportService>();
            services.AddTransient<IVariableAllowanceService, VariableAllowanceService>();
            services.AddTransient<IVariableDeductionService, VariableDeductionService>();
            services.AddTransient<ITDSConfirmationService, TDSConfirmationService>();
            services.AddTransient<ILoanIssueService, LoanIssueService>();
            services.AddTransient<IHolidayWageSettingsService, HolidayWageSettingsService>();
            services.AddTransient<IOffdayWageSettingsService, OffdayWageSettingsService>();
            services.AddTransient<IOvertimeSettingsService, OvertimeSettingsService>();
            services.AddTransient<ILoanTopUpService, LoanTopUpService>();
            services.AddTransient<ILoanClosureService, LoanClosureService>();
            services.AddTransient<IPFSettingsService, PFSettingsService>();
            services.AddTransient<IESISettingsService, ESISettingsService>();
            services.AddTransient<ILOPSettingsService, LOPSettingsService>();
            services.AddTransient<IPTConfirmationService, PTConfirmationService>();
            services.AddTransient<ILoanConfirmationService, LoanConfirmationService>();
            services.AddTransient<IPTSettingsService, PTSettingsService>();
            services.AddTransient<IPTProcessingService, PTProcessingService>();
            services.AddTransient<IBankStatementReportService, BankStatementReportService>();
            services.AddTransient<IFullandFinalSettelmentService, FullandFinalSettelmentService>();
            services.AddTransient<ISalaryProcessingService, SalaryProcessingService>();
            services.AddTransient<IESIReportService, ESIReportService>();
            services.AddTransient<IGratuitySettingsService, GratuitySettingsService>();
            services.AddTransient<ILeaveEncashmentSettingsService, LeaveEncashmentSettingsService>();
            services.AddTransient<IMonthlySalaryProcessingIndividualService, MonthlySalaryProcessingIndividualService>();
            services.AddTransient<ISalaryReportsService, SalaryReportsService>();
            services.AddTransient<ISalaryHoldService, SalaryHoldService>();
            services.AddTransient<ISalaryReleaseService, SalaryReleaseService>();
            services.AddTransient<IEmployeeVsInsuranceDetailsService, EmployeeVsInsuranceDetailsService>();
            services.AddTransient<IIncomeTaxRatesService, IncomeTaxRatesService>();
            services.AddTransient<IMonthlyVDASettingsService, MonthlyVDASettingsService>();
            services.AddTransient<ITDSVsAllowanceSettingsService, TDSVsAllowanceSettingsService>();
            services.AddTransient<IFormulaBasedDeductionService, FormulaBasedDeductionService>();
            services.AddTransient<IFullAndFinalKendoReportService, FullAndFinalKendoReportService>();
            services.AddTransient<IVariableProvisionsAndPerkService, VariableProvisionsAndPerkService>();
            services.AddTransient<IGeneratePaySlipService, GeneratePaySlipService>();
            services.AddTransient<ISendPayslipService, SendPayslipService>();
            services.AddTransient<ISalaryPostingService, SalaryPostingService>();
            services.AddTransient<IPreviousMonthAdjustmentsService, PreviousMonthAdjustmentsService>();
            services.AddTransient<IStateVsLWFService, StateVsLWFService>();
            services.AddTransient<IArearProcessingService, ArearProcessingService>();
            services.AddTransient<IPostExitProcessService, PostExitProcessService>();
            services.AddTransient<IGratuityProcessingService, GratuityProcessingService>();
            services.AddTransient<ISalaryAdvanceIssueService, SalaryAdvanceIssueService>();
            services.AddTransient<ILeaveEncashmentProcessingService, LeaveEncashmentProcessingService>();
            services.AddTransient<ISalaryReleaseReportService, SalaryReleaseReportService>();
            services.AddTransient<IArrearProcessingReportService,ArrearProcessingReportService>();
            services.AddTransient<IBonusProcessingService, BonusProcessingService>();
            services.AddTransient<IESIExpiryCheckService, ESIExpiryCheckService>();
            services.AddTransient<IMonthlyEmployeeVsCostCenterService, MonthlyEmployeeVsCostCenterService>();
            services.AddTransient<ISlabBasedDeductionConfirmationService, SlabBasedDeductionConfirmationService>();
            services.AddTransient<ISlabBasedDeductionService, SlabBasedDeductionService>();
            services.AddTransient<ICostPaySheetService, CostPaySheetService>();
            services.AddTransient<INegativeSalaryCorrectionService, NegativeSalaryCorrectionService>();
            services.AddTransient<IFormIVReportService, FormIVReportService>();
            services.AddTransient<IIncentiveReportService, IncentiveReportService>();
            services.AddTransient<IMonthlyPaymentRemittanceService, MonthlyPaymentRemittanceService>();
            services.AddTransient<ISalaryRecoveryService, SalaryRecoveryService>();
            services.AddTransient<IPFForm15GService, PFForm15GService>();
            services.AddTransient<IEmployeeRequestLoanIssueService, EmployeeRequestLoanIssueService>();
            services.AddTransient<IVPFApprovalService, VPFApprovalService>();
            services.AddTransient<IBonusUploadService, BonusUploadService>();
            services.AddTransient<ISalaryReleaseInitializeService, SalaryReleaseInitializeService>();
            services.AddTransient<IJVPostingService, JVPostingService>();
            services.AddTransient<IBankStatementService, BankStatementService>();
            services.AddTransient<ISalaryReleaseBankStatementService, SalaryReleaseBankStatementService>();
            services.AddTransient<IParentsMediclaimEMIService, ParentsMediclaimEMIService>();
            services.AddTransient<ICertificationReimbursementService, CertificationReimbursementService>();
            services.AddTransient<IBankStatementRepostService, BankStatementRepostService>();
            services.AddTransient<ISalaryEditService, SalaryEditService>();
            services.AddTransient<ISalaryReleaseRepostService, SalaryReleaseRepostService>();
            services.AddTransient<ISalaryReleaseWorkFlowService, SalaryReleaseWorkFlowService>();
            services.AddTransient<IIncentiveService, IncentiveService>();
            services.AddTransient<INoticePayReturnService, NoticePayReturnService>();
            services.AddTransient<IDailyFandFSalaryProcessingService, DailyFandFSalaryProcessingService>();
            services.AddTransient<IPreFullandFinalSettlementService, PreFullandFinalSettlementService>();
            #region Reports
            services.AddTransient<ISalaryDetailedReportService, SalaryDetailedReportService>();
            services.AddTransient<ISalarySummaryReportService, SalarySummaryReportService>();
            services.AddTransient<IPFSummaryReportService, PFSummaryReportService>();
            services.AddTransient<IYearlySalayService, YearlySalaryService>();
            services.AddTransient<IESISummaryReportService, ESISummaryReportService>();
            services.AddTransient<IPfEcrReportService, PfEcrReportService>();
            services.AddTransient<IAccountStatementService, AccountStatementService>();
            services.AddTransient<IPaySheetService, PaySheetService>();
            services.AddTransient<ILoanRegisterService, LoanRegisterService>();
            services.AddTransient<INonPayrollAllowanceReportService, NonPayrollAllowanceReportService>();
            services.AddTransient<IGratuityProjectionReportService, GratuityProjectionReportService>();
            services.AddTransient<ISalaryReconciliationService, SalaryReconciliationService>();
            services.AddTransient<IPayslipService, PayslipService>();
            services.AddTransient<IFullAndFinalSettlementSlipService, FullAndFinalSettlementSlipService>();
            services.AddTransient<ITimeSheetDetailedReportService, TimeSheetDetailedReportService>();
            services.AddTransient<IPFGroupSummaryReportService, PFGroupSummaryReportService>();
            services.AddTransient<IESIGroupSummaryReportService, ESIGroupSummaryReportService>();
            services.AddTransient<ISalaryGroupSummaryReportService, SalaryGroupSummaryReportService>();
            services.AddTransient<IWPSReportService, WPSReportService>();
            services.AddTransient<ISalaryReportDraftService, SalaryReportDraftService>();
            services.AddTransient<IPTReportService, PTReportService>();
            services.AddTransient<ILWFReportService, LWFReportService>();
            services.AddTransient<ILOPReportService, LOPReportService>();
            services.AddTransient<ICashStatementService, CashStatementService>();
            services.AddTransient<IBonusReportService, BonusReportService>();
            services.AddTransient<IPeriodWiseDeductionReportService, PeriodWiseDeductionReportService>();
            services.AddTransient<IMonthlyCTCReportService, MonthlyCTCReportService>();
            services.AddTransient<ICostProcessingService, CostProcessingService>();
            services.AddTransient<IShiftVarianceReportService, ShiftVarianceReportService>();
            services.AddTransient<IProcessReadinessCheckService, ProcessReadinessCheckService>();
            services.AddTransient<IForm12LabourReportService, Form12LabourReportService>();
            services.AddTransient<ILOPPeriodwiseService, LOPPeriodwiseService>();
            services.AddTransient<IPFFormExitReportService, PFFormExitReportService>();
            services.AddTransient<ILoanIssueReportService, LoanIssueReportService>();
            services.AddTransient<IJVReportService, JVReportService>();
            services.AddTransient<IPreFullandFinalSettlementReportService, PreFullandFinalSettlementReportService>();
            services.AddTransient<IIncentivePostingReportService, IncentivePostingReportService>();
            #endregion


            #endregion

            #region MIS
            services.AddTransient<ICompanyMISService, CompanyMISService>();
            services.AddTransient<IMISDashBoardService, MISDashBoardService>();
            services.AddTransient<IMISUserSettingsService, MISUserSettingsService>();
            #endregion

            #region Recruitment
            services.AddTransient<IOfferProcessingService, OfferProcessingService>();
            services.AddTransient<IJoinConfirmEmpActivationService, JoinConfirmEmpActivationService>();
            services.AddTransient<IInterviewFeedbackSettingsService, InterviewFeedbackSettingsService>();
            services.AddTransient<IDocumentVerificationService, DocumentVerificationService>();
            services.AddTransient<IBackgroundVerificationService, BackgroundVerificationService>();
            services.AddTransient<IMasterInterviewPanelService, MasterInterviewPanelService>();
            services.AddTransient<IApplicantsService, ApplicantsService>();
            services.AddTransient<IRecruiterScreeningService, RecruiterScreeningService>();
            #region Reports
            services.AddTransient<IApplicantTrackingReportService, ApplicantTrackingReportService>();
            services.AddTransient<Services.IServices.Recruitment.IVacancyStatusReportService, Services.Services.Recruitment.VacancyStatusReportService>();
            services.AddTransient<IVacancyCancellationReportService, VacancyCancellationReportService>();
            services.AddTransient<IRecruitmentTransactionHistoryService, RecruitmentTransactionHistoryService>();
            services.AddTransient<IBackGroundVerificationReportService, BackGroundVerificationReportService>();
            #endregion
            #endregion

   

          
            #region TDS

            services.AddTransient<IInvestmentDeclarationService, InvestmentDeclarationService>();
            services.AddTransient<IRegimeService, RegimeService>();
            services.AddTransient<ITDSProcessingService, TDSProcessingService>();
            services.AddTransient<IEmployeeVsPerquisitesService, EmployeeVsPerquisitesService>();
            services.AddTransient<ITDSReportService, TDSReportService>();
            services.AddTransient<IEmployeeVsExcemptionService, EmployeeVsExcemptionService>();
            services.AddTransient<ITDSSlipService, TDSSlipService>();
            services.AddTransient<IInvestmentDeclarationApprovalService, InvestmentDeclarationApprovalService>();
            services.AddTransient<IExcemptionsService, ExcemptionsService>();
            services.AddTransient<ITDS24QService, TDS24QService>();
            services.AddTransient<ITDSReturnFillingService, TDSReturnFillingService>();
            services.AddTransient<ITDSFORM12BService, TDSFORM12BService>();
            services.AddTransient<IForm16SettingsService, Form16SettingsService>();
            services.AddTransient<IFORM12BBService, FORM12BBService>();
            services.AddTransient<ITDSVariationReportService, TDSVariationReportService>();
            services.AddTransient<ITDSDeclarationReportService, TDSDeclarationReportService>();
            services.AddTransient<IForm16Service, Form16Service>();
            services.AddTransient<IPreviousEmployerHRMSService, PreviousEmployerHRMSService>();
            services.AddTransient<ITaxCalculatorService, TaxCalculatorService>();
            services.AddTransient<ITDSDeclarationDetailsOldService, TDSDeclarationDetailsOldService>();
            services.AddTransient<IEmployeePerquisitesReportService, EmployeePerquisitesReportService>();
            #endregion
            #region TDS
            services.AddTransient<ITrainingScheduleService, TrainingScheduleService>();
            #endregion

            #region Training Module
            services.AddTransient<ITrnParticipantService, TrnParticipantService>();
            services.AddTransient<ITrainingExternalTrainerService, TrainingExternalTrainerService>();
            services.AddTransient<ITrainingLocationService, TrainingLocationService>();
            services.AddTransient<ITrainingVsSubjectService, TrainingVsSubjectService>();
            services.AddTransient<ITrainingCostReportService, TrainingCostReportService>();
            services.AddTransient<ITrainingEmployeeGroupService, TrainingEmployeeGroupService>();

   
            services.AddTransient<IParticipantEntryService, ParticipantEntryService>();
            services.AddTransient<ITRAININGCOMPLETIONService, TRAININGCOMPLETIONService>();
            services.AddTransient<ITrainingCostService, TrainingCostService>();
            services.AddTransient<ITrainingSubjectService, TrainingSubjectService>();
            services.AddTransient<ITrnScheduleRptService, TrnScheduleRptService>();
            services.AddTransient<IMonthlyReportService, MonthlyReportService>();
            services.AddTransient<ITraineeCostService, TraineeCostService>();
            services.AddTransient<ITrainingReportServices, TrainingReportService>();
            services.AddTransient<ITraining_EmployeVsReportService, Training_EmployeVsReportService>();
            services.AddTransient<ITrainingCostAllotedServices, TrainingCostAllotedServices>();
            services.AddTransient<ITrainingCostAllotedReportServices, TrainingCostAllotedReportServices>();
            services.AddTransient<ITrainingGroupVsEmployeeService, TrainingGroupVsEmployeeService>();
            services.AddTransient<ITrainingDueListService, TrainingDueListService>();
            services.AddTransient<ITraining_EmpVsSubjectService, Training_EmpVsSubjectService>();
            services.AddTransient<IAttendanceEntryMobilePunchService, AttendanceEntryMobilePunchService>();
            services.AddTransient<ITrainingReplyGroupServices, TrainingReplyGroupService>();
            services.AddTransient<ITrainingReplyService, TrainingReplyService>();
            services.AddTransient<ITrainingQuestionsService, TrainingQuestionsService>();
            #region Reports

            services.AddTransient<ITrainingPreTestReportService, TrainingPreTestReportService>();
            services.AddTransient<ITrainingPostTestReportService, TrainingPostTestReportService>();
            #endregion
            #endregion

       
          
         
        }
    }
}
