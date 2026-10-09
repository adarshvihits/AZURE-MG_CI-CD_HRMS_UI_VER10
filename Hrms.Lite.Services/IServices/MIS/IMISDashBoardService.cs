using DocumentFormat.OpenXml.Office2010.CustomUI;
using Hrms.Lite.Shared.DashBoard;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.MIS;
using Hrms.Lite.Shared.PayRoll.Reports;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.MIS
{
    public interface IMISDashBoardService
    {

        public Task<MISDashBoard> GetDashBoardDiagrams(string Diagram, string ToggleActive, int Classification);
        Task<MISDashBoard> GetDashBoardTODiagrams(string Diagram, int? ShiftCode);

        #region Resignation Analysis
        Task<MISDashBoard> GetResignationCount(string from, string to, int groupCom);
        Task<MISDashBoard> GetAverageResignationCount();
        Task<MISDashBoard> GetGenderResignationCount(string from, string to, int groupCom);
        Task<MISDashBoard> GetResignationMonthwiseCount(int groupCom);
        Task<MISDashBoard> GetResignationReasonCount(string from, string to, int groupCom);
        Task<MISDashBoard> GetResignationageBandCount(string from, string to, int groupCom);

        Task<MISDashBoard> GetResignationTenureCount(string from, string to, int groupCom);
        Task<MISDashBoard> GetAttritionRate(string from, string to, int groupCom);
        Task<MISDashBoard> GetTermDepartmentCount(string from, string to, int groupCom);
        Task<MISDashBoard> GetTermLocationCount(string from, string to, int groupCom);
        Task<MISDashBoard> GetTermDivisionCount(string from, string to, int groupCom);
        Task<MISDashBoard> GetTermGradeCount(string from, string to, int groupCom);
        Task<MISDashBoard> GetTermCategoryCount(string from, string to, int groupCom);
        Task<MISDashBoard> GetTermRegionCount(string from, string to, int groupCom);

        #region Resignation List
        Task<MISDashBoard> GetGenderResignationCountList(string from, string to, int groupCom, string age);
        Task<MISDashBoard> GetResignationageBandCountList(string from, string to, int groupCom, string age);
        Task<MISDashBoard> GetResignationTenureCountList(string from, string to, int groupCom, string age);
        Task<MISDashBoard> GetDepartmentResignationCountList(string from, string to, int groupCom, string type, string age);
        #endregion
        #endregion

        #region Joiners Analysis
        Task<MISDashBoard> GetJoinersCount(string from, string to, int groupCom);
        Task<MISDashBoard> GetGenderJoinersCount(string from, string to, int groupCom);
        Task<MISDashBoard> GetJoinersageBandCount(string from, string to, int groupCom);
        Task<MISDashBoard> GetJoinersDepartmentCount(string from, string to, int groupCom);
        Task<MISDashBoard> GetJoinersLocationCount(string from, string to, int groupCom);
        Task<MISDashBoard> GetJoinersDivisionCount(string from, string to, int groupCom);
        Task<MISDashBoard> GetJoinersGradeCount(string from, string to, int groupCom);
        Task<MISDashBoard> GetJoinersCategoryCount(string from, string to, int groupCom);
        Task<MISDashBoard> GetJoinersRegionCount(string from, string to, int groupCom);
        Task<MISDashBoard> GetBadHires(string from, string to, int groupCom);

        #region Joiners List

        Task<MISDashBoard> GetJoinersCountList(string from, string to, int groupCom, string type, string age);

        #endregion

        #endregion

        #region Leave Analysis
        Task<MISDashBoard> GetLeaveData(int leavePeriod, int groupCom);
        Task<MISDashBoard> GetLeaveMonthwiseCount(int leavePeriod, int groupCom);
        Task<MISDashBoard> GetLeaveageBandCount(int leavePeriod, int groupCom);
        Task<MISDashBoard> GetDepartmentCount(int leavePeriod, int groupCom);
        Task<MISDashBoard> GetLocationCount(int leavePeriod, int groupCom);
        Task<MISDashBoard> GetRegionCount(int leavePeriod, int groupCom);
        Task<MISDashBoard> GetDivisionCount(int leavePeriod, int groupCom);
        Task<MISDashBoard> GetGradeCount(int leavePeriod, int groupCom);
        Task<MISDashBoard> GetCategoryCount(int leavePeriod, int groupCom);
        Task<MISDashBoard> GetCatagoryLeaveCount(int leavePeriod, int groupCom);
        #endregion

        #region Employee Analysis
        Task<MISDashBoard> GetEmployeeList(int groupCom);
        Task<MISDashBoard> GetEmployeeInformation(int empCode);
        Task<MISDashBoard> GetEmployeeLeaveTrend(int empCode);
        Task<MISDashBoard> GetEmployeeMilestones(int empCode);
        Task<MISDashBoard> GetEmployeeRewards(int empCode);
        Task<MISDashBoard> GetEmployeeSkills(int empCode);
        Task<MISDashBoard> GetEmployeeMap(int empCode);

        #region List

        Task<MISDashBoard> GetEmployeeSkillDetail(int EmployeeCode);
        Task<MISDashBoard> GetEmployeeRewardDetail(int EmployeeCode);
        Task<MISDashBoard> GetEmployeeLeaveDetail(int EmployeeCode);

        #endregion


        #endregion

        #region Salary Analysis
        Task<MISDashBoard> GetSalaryInformation(int finCode, int groupCom);
        Task<MISDashBoard> GetSalaryIncrementInformation(int finCode, int groupCom);
        Task<MISDashBoard> GetSalaryTrend(int finCode, int groupCom);
        Task<MISDashBoard> GetSalaryIncrementRange(int finCode, int groupCom);
        Task<MISDashBoard> GetSalaryDepartmentWise(int finCode, int groupCom);
        Task<MISDashBoard> GetSalaryLocationWise(int finCode, int groupCom);
        Task<MISDashBoard> GetSalaryDivisionWise(int finCode, int groupCom);
        Task<MISDashBoard> GetSalaryGradeWise(int finCode, int groupCom);
        Task<MISDashBoard> GetSalaryCategoryWise(int finCode, int groupCom);
        Task<MISDashBoard> GetSalaryRegionWise(int finCode, int groupCom);
        Task<MISDashBoard> GetAllowanceMonthComparison(int month, int groupCom);
        Task<MISDashBoard> GetDeductionMonthComparison(int month, int groupCom);
        Task<MISDashBoard> GetSalaryAllowance(int finCode, int groupCom);
        Task<MISDashBoard> GetSalaryDeduction(int finCode, int groupCom);

        #region List
        Task<MISDashBoard> GetSalaryList(int finCode, int groupCom, string type, string text);

        #endregion
        #endregion

        #region Absenteesm
        Task<MISDashBoard> GetAbsRate(string from, string to);
        Task<MISDashBoard> GetAbsMonth(string from, string to);
        Task<MISDashBoard> GetAbsAge(string from, string to);
        Task<MISDashBoard> GetAbsTenure(string from, string to);
        Task<MISDashBoard> GetAbsDepartment(string from, string to);
        Task<MISDashBoard> GetAbsLocation(string from, string to);
        Task<MISDashBoard> GetAbsDivision(string from, string to);
        Task<MISDashBoard> GetAbsCategory(string from, string to);
        Task<MISDashBoard> GetAbsGrade(string from, string to);
        Task<MISDashBoard> GetAbsRegion(string from, string to);
        Task<MISDashBoard> GetAbsDays(string from, string to);
        Task<MISDashBoard> GetAbsMonthWiseDetail(DateTime FROMDATE, DateTime TODATE, int GROUPCOMPANYCODE, int MONTHCODE);
        Task<MISDashBoard> GetAbsAgeSlabWiseDetail(DateTime FROMDATE, DateTime TODATE, int GROUPCOMPANYCODE, string AGESLAB);
        Task<MISDashBoard> GetAbsServiceDetail(DateTime FROMDATE, DateTime TODATE, int GROUPCOMPANYCODE, string YEARSLAB);
        Task<MISDashBoard> GetAbsDepartmentDetail(DateTime FROMDATE, DateTime TODATE, int GROUPCOMPANYCODE, string DEPARTMENT);
        Task<MISDashBoard> GetAbsLocationDetail(DateTime FROMDATE, DateTime TODATE, int GROUPCOMPANYCODE, string LOCATION);

        Task<MISDashBoard> GetAbsWeekdayDetail(DateTime FROMDATE, DateTime TODATE, int GROUPCOMPANYCODE, string DAY);
        Task<MISDashBoard> GetAbsDivisionDetail(DateTime FROMDATE, DateTime TODATE, int GROUPCOMPANYCODE, string DIVISON);
        Task<MISDashBoard> GetAbsCategoryDetail(DateTime FROMDATE, DateTime TODATE, int GROUPCOMPANYCODE, string CATEGORY);
        Task<MISDashBoard> GetAbsRegionDetail(DateTime FROMDATE, DateTime TODATE, int GROUPCOMPANYCODE, string REGION);

        Task<MISDashBoard> GetAbsGradeDetail(DateTime FROMDATE, DateTime TODATE, int GROUPCOMPANYCODE, string GRADE);
        #endregion

        #region Historical Trends
        Task<MISDashBoard> GetHistoricalJoiningCount(int groupCom);
        Task<MISDashBoard> GetHistoricalTerminationCount(int groupCom);
        Task<MISDashBoard> GetHistoricalBadHiresCount(int groupCom);
        Task<MISDashBoard> GetHistoricalJoiningCountMonth();
        Task<MISDashBoard> GetHistoricalTerminationsCountMonth();
        Task<MISDashBoard> GetHistoricalAttritionMonth();
        Task<MISDashBoard> GetHistoricalWFH(int groupCom);
        Task<MISDashBoard> GetHistoricalSalary(int groupCom);
        Task<MISDashBoard> GetHistoricalSalaryInc(int groupCom);
        Task<MISDashBoard> GetHistoricalList(int groupCom, string type, string text);
        #endregion

        #region Recruitment Analysis
        Task<MISDashBoard> GetRecruitmentInformation(int finCode, int groupCom);
        Task<MISDashBoard> GetRecruitmentVacancyTrend(int finCode, int groupCom);
        Task<MISDashBoard> GetTopVacancyDesignation(int finCode, int groupCom);


        Task<MISDashBoard> GetVacancyDetails(int FinancialYear, int GrpCompanyCode, string MONTH);
        Task<MISDashBoard> GetRecruitmentInformations(int FinancialYear, int GrpCompanyCode, string TYPE);

        Task<MISDashBoard> GetVacancyDesignationDetails(int FinancialYear, int GrpCompanyCode, string TYPE);
        #endregion

        #region HR at glance
        Task<MISDashBoard> GetHrInformation(int groupCom);
        Task<MISDashBoard> GetSalaryRange(int groupCom);
        Task<MISDashBoard> GetGenderHR(int groupCom);
        Task<MISDashBoard> GetDepartmentHR(int groupCom);
        Task<MISDashBoard> GetRegionHR(int groupCom);
        Task<MISDashBoard> GetDivisionHR(int groupCom);
        Task<MISDashBoard> GetGradeHR(int groupCom);
        Task<MISDashBoard> GetCategoryHR(int groupCom);
        Task<MISDashBoard> GetAgeHR(int groupCom);
        Task<MISDashBoard> GetTenureHR(int groupCom);
        Task<MISDashBoard> GetLocationHR(int groupCom);
        Task<MISDashBoard> GetTimeToQuitJobHR(int groupCom);


        Task<MISDashBoard> GetDashBoardDiagrams(string Classification);

        #region List
        Task<MISDashBoard> GetActiveEmployeeCountList(int groupCom, string type, string age);

        #endregion

        #endregion

        #region Workforce Time Trends

        Task<MISDashBoard> GetTimeOfficeHR(int groupCom);
        Task<MISDashBoard> GetTODepartmentHR(int groupCom);
        Task<MISDashBoard> GetTOLocationHR(int groupCom);
        Task<MISDashBoard> GetTODivisionHR(int groupCom);
        Task<MISDashBoard> GetTOGradeHR(int groupCom);
        Task<MISDashBoard> GetTOCategoryHR(int groupCom);
        Task<MISDashBoard> GetTORegionHR(int groupCom);

        #endregion

        #region Attendance OverView

        Task<MISDashBoard> GetAttendanceMonthData(int month, int groupCom);
        Task<MISDashBoard> GetAttendanceDayWise(int month, int groupCom);
        Task<MISDashBoard> GetAttendanceDateWise(int day, int month, int groupCom);
        public Task<MISDashBoard> GetMonths(int FinancialYearCode);

        #region List
        Task<MISDashBoard> GetAttendanceDateList(int day, int month, int groupCom);

        #endregion

        #endregion

        #region PayRollOverView
        Task<MISDashBoard> GetPRAllowance(int month, int groupCom);
        Task<MISDashBoard> GetPRDeduction(int month, int groupCom);
        Task<MISDashBoard> GetPRCTC(int month, int groupCom);
        Task<MISDashBoard> GetPG(int month, int groupCom);
        Task<MISDashBoard> GetEmpData(int month, int groupCom);

        #region List        

        Task<MISDashBoard> GetPFDatas(int month, int groupCom);
        Task<MISDashBoard> GetAlwData(int month, int groupCom, string type);


        #endregion

        #endregion

        #region Leave OverView
        Task<MISDashBoard> GetLeaveType(int month, int groupCom);
        Task<MISDashBoard> GetLeaveDays(int month, int groupCom);
        Task<MISDashBoard> GetLeaveDepartment(int month, int groupCom);
        Task<MISDashBoard> GetLeaveLocation(int month, int groupCom);
        Task<MISDashBoard> GetLeaveDivision(int month, int groupCom);
        Task<MISDashBoard> GetLeaveCategory(int month, int groupCom);
        Task<MISDashBoard> GetLeaveGrade(int month, int groupCom);
        Task<MISDashBoard> GetLeaveRegion(int month, int groupCom);

        #endregion

        #region Late in OverView

        Task<MISDashBoard> GetLateInDepartment(int month, int groupCom);
        Task<MISDashBoard> GetLateInLocation(int month, int groupCom);
        Task<MISDashBoard> GetLateInDivision(int month, int groupCom);
        Task<MISDashBoard> GetLateInCategory(int month, int groupCom);
        Task<MISDashBoard> GetLateInGrade(int month, int groupCom);
        Task<MISDashBoard> GetLateInRegion(int month, int groupCom);

        Task<MISDashBoard> GetAttendanceList(int groupCom, int month, string type, int code);

        #endregion

        #region Employee Overview

        Task<MISDashBoard> GetEmpGeoTagStatus(int month, int empCode);
        Task<MISDashBoard> GetAttendanceEmpMonthData(int month, int empCode);
        Task<MISDashBoard> GetEmployeeListOv(int month, int groupCom);
        Task<MISDashBoard> GetAttendanceEmpDayWise(int month, int empCode);
        Task<MISDashBoard> GetEmployeeLateInEarlyOut(int month, int empCode);

        #endregion

        #region Grievance
        Task<MISDashBoard> GetGrievanceByFinancialYear(int GrpCompanyCode, int financialyear);
        Task<MISDashBoard> GetGrievanceCompletedByFinancialYear(int GrpCompanyCode, int financialyear);
        Task<MISDashBoard> GetGrievanceAcceptedByFinancialYear(int GrpCompanyCode, int financialyear);
        Task<MISDashBoard> GetGrievanceNonAcceptedByFinancialYear(int GrpCompanyCode, int financialyear);
        Task<MISDashBoard> GetGrievanceRejectedByFinancialYear(int GrpCompanyCode, int financialyear);

        #region List

        Task<MISDashBoard> GetGrievenceCountGraphDataView(int GrievanceCatid, int FinancialYear, int GrpCompanyCode, string TYPE);

        #endregion

        #endregion

        #region Transition Type
        Task<MISDashBoard> GetTransitionTypeCount(int groupCom, int financialYr, int typeCode);
        Task<MISDashBoard> GetTransitionTypeDepartment(int groupCom, int financialYr, int typeCode);
        Task<MISDashBoard> GetTransitionTypeLocation(int groupCom, int financialYr, int typeCode);
        Task<MISDashBoard> GetTransitionTypeDivision(int groupCom, int financialYr, int typeCode);
        Task<MISDashBoard> GetTransitionTypeCategory(int groupCom, int financialYr, int typeCode);
        Task<MISDashBoard> GetTransitionTypeGrade(int groupCom, int financialYr, int typeCode);
        Task<MISDashBoard> GetTransitionTypeRegion(int groupCom, int financialYr, int typeCode);

        #region List

        Task<MISDashBoard> GetTransitionEmployeeCount(int groupCom, int financialYr, int typeCode, string type, int text);

        #endregion

        #endregion
        Task<MISDashBoard> GetLeaveEmployeeList(int leavePeriod, int groupCom, string type, string code);

        #region BAD_HIRES
        Task<MISDashBoard> GetBadHiresMonth(int groupCom, int financialYr);
        Task<MISDashBoard> GetBadHiresAge(int groupCom, int financialYr);
        Task<MISDashBoard> GetBadHiresGender(int groupCom, int financialYr);
        Task<MISDashBoard> GetBadHiresDepartment(int groupCom, int financialYr);
        Task<MISDashBoard> GetBadHiresLocation(int groupCom, int financialYr);
        Task<MISDashBoard> GetBadHiresCategory(int groupCom, int financialYr);
        Task<MISDashBoard> GetBadHiresDivision(int groupCom, int financialYr);
        Task<MISDashBoard> GetBadHiresGrade(int groupCom, int financialYr);
        Task<MISDashBoard> GetBadHiresRegion(int groupCom, int financialYr);
        Task<MISDashBoard> GetBadHiresEmpList(int groupCom, int financialYr, string type, string code);
        #endregion

        Task<MISDashBoard> GetYearlyTrainingCost(int financialYr);
        Task<MISDashBoard> GetPerHeadTrainingCost(int financialYr);
        Task<MISDashBoard> GetTrainingEmployeeCount(int financialYr);
        Task<MISDashBoard> GetMonthlyTrainings(int financialYr);
        Task<MISDashBoard> GetTrainingEmp(int financialYr, int code);
        Task<MISDashBoard> GetTrainingCostList(int code);


        Task<MISDashBoard> GetAttendanceCount(int financialYr,int groupCom,int AttendanceCode);
    }
}
