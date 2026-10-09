using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.MIS;

using Hrms.Lite.Services.IServices.DashBoard;
using Hrms.Lite.Shared.DashBoard;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Hrms.Lite.Shared.MIS;
using DocumentFormat.OpenXml.Office2010.CustomUI;
using Hrms.Lite.Shared.PayRoll.Reports;
using Hrms.Lite.Shared.General;

namespace Hrms.Lite.Services.Services.MIS
{
    public class MISDashBoardService : IMISDashBoardService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public MISDashBoardService(IServiceBase httpService)
        {
            _baseURL = "MISDashBoard";
            _httpService = httpService;
        }
        public async Task<MISDashBoard> GetDashBoardDiagrams(string Diagram, string ToggleActive, int Classification)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetDashBoardDiagrams?Diagram={Diagram}&ToggleActive={ToggleActive}&Classification={Classification}");
        }

        public async Task<MISDashBoard> GetDashBoardTODiagrams(string Diagram, int? ShiftCode)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetDashBoardTODiagrams?Diagram={Diagram}&ShiftCode={ShiftCode}");
        }

        #region Resignation Analysis
        public async Task<MISDashBoard> GetResignationCount(string from, string to, int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetResignationCount?from={from}&to={to}&groupCom={groupCom}");
        }
        public async Task<MISDashBoard> GetAverageResignationCount()
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetAverageResignationCount");
        }
        public async Task<MISDashBoard> GetGenderResignationCount(string from, string to, int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetGenderResignationCount?from={from}&to={to}&groupCom={groupCom}");
        }

        public async Task<MISDashBoard> GetResignationMonthwiseCount(int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetResignationMonthwiseCount?groupCom={groupCom}");
        }

        public async Task<MISDashBoard> GetResignationReasonCount(string from, string to, int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetResignationReasonCount?from={from}&to={to}&groupCom={groupCom}");
        }
        public async Task<MISDashBoard> GetResignationageBandCount(string from, string to, int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetResignationageBandCount?from={from}&to={to}&groupCom={groupCom}");
        }

        public async Task<MISDashBoard> GetResignationTenureCount(string from, string to, int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetResignationTenureCount?from={from}&to={to}&groupCom={groupCom}");
        }
        public async Task<MISDashBoard> GetAttritionRate(string from, string to, int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetAttritionRate?from={from}&to={to}&groupCom={groupCom}");
        }
        public async Task<MISDashBoard> GetTermDepartmentCount(string from, string to, int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetTermDepartmentCount?from={from}&to={to}&groupCom={groupCom}");
        }
        public async Task<MISDashBoard> GetTermLocationCount(string from, string to, int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetTermLocationCount?from={from}&to={to}&groupCom={groupCom}");
        }
        public async Task<MISDashBoard> GetTermDivisionCount(string from, string to, int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetTermDivisionCount?from={from}&to={to}&groupCom={groupCom}");
        }
        public async Task<MISDashBoard> GetTermGradeCount(string from, string to, int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetTermGradeCount?from={from}&to={to}&groupCom={groupCom}");
        }
        public async Task<MISDashBoard> GetTermCategoryCount(string from, string to, int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetTermCategoryCount?from={from}&to={to}&groupCom={groupCom}");
        }
        public async Task<MISDashBoard> GetTermRegionCount(string from, string to, int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetTermRegionCount?from={from}&to={to}&groupCom={groupCom}");
        }

        #region resignation List
        public async Task<MISDashBoard> GetResignationageBandCountList(string from, string to, int groupCom, string age)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetResignationageBandCountList?from={from}&to={to}&groupCom={groupCom}&age={age}");
        }
        public async Task<MISDashBoard> GetResignationTenureCountList(string from, string to, int groupCom, string age)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetResignationTenureCountList?from={from}&to={to}&groupCom={groupCom}&age={age}");
        }
        public async Task<MISDashBoard> GetGenderResignationCountList(string from, string to, int groupCom, string age)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetGenderResignationCountList?from={from}&to={to}&groupCom={groupCom}&age={age}");
        }
        public async Task<MISDashBoard> GetDepartmentResignationCountList(string from, string to, int groupCom, string type, string age)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetDepartmentResignationCountList?from={from}&to={to}&groupCom={groupCom}&type={type}&age={age}");
        }
        #endregion
        #endregion

        #region Joiners Analysis

        public async Task<MISDashBoard> GetJoinersCount(string from, string to, int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetJoinersCount?from={from}&to={to}&groupCom={groupCom}");
        }
        public async Task<MISDashBoard> GetGenderJoinersCount(string from, string to, int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetGenderJoinersCount?from={from}&to={to}&groupCom={groupCom}");
        }
        public async Task<MISDashBoard> GetJoinersageBandCount(string from, string to, int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetJoinersageBandCount?from={from}&to={to}&groupCom={groupCom}");
        }
        public async Task<MISDashBoard> GetJoinersDepartmentCount(string from, string to, int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetJoinersDepartmentCount?from={from}&to={to}&groupCom={groupCom}");
        }
        public async Task<MISDashBoard> GetJoinersLocationCount(string from, string to, int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetJoinersLocationCount?from={from}&to={to}&groupCom={groupCom}");
        }
        public async Task<MISDashBoard> GetJoinersDivisionCount(string from, string to, int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetJoinersDivisionCount?from={from}&to={to}&groupCom={groupCom}");
        }
        public async Task<MISDashBoard> GetJoinersGradeCount(string from, string to, int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetJoinersGradeCount?from={from}&to={to}&groupCom={groupCom}");
        }
        public async Task<MISDashBoard> GetJoinersCategoryCount(string from, string to, int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetJoinersCategoryCount?from={from}&to={to}&groupCom={groupCom}");
        }
        public async Task<MISDashBoard> GetJoinersRegionCount(string from, string to, int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetJoinersRegionCount?from={from}&to={to}&groupCom={groupCom}");
        }
        public async Task<MISDashBoard> GetBadHires(string from, string to, int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetBadHires?from={from}&to={to}&groupCom={groupCom}");
        }

        #region Joiners List

        public async Task<MISDashBoard> GetJoinersCountList(string from, string to, int groupCom, string type, string age)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetJoinersCountList?from={from}&to={to}&groupCom={groupCom}&type={type}&age={age}");
        }

        #endregion 

        #endregion

        #region Leave Analysis

        public async Task<MISDashBoard> GetLeaveData(int leavePeriod, int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetLeaveData?leavePeriod={leavePeriod}&groupCom={groupCom}");
        }
        public async Task<MISDashBoard> GetLeaveMonthwiseCount(int leavePeriod, int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetLeaveMonthwiseCount?leavePeriod={leavePeriod}&groupCom={groupCom}");
        }
        public async Task<MISDashBoard> GetLeaveageBandCount(int leavePeriod, int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetLeaveageBandCount?leavePeriod={leavePeriod}&groupCom={groupCom}");
        }
        public async Task<MISDashBoard> GetDepartmentCount(int leavePeriod, int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetDepartmentCount?leavePeriod={leavePeriod}&groupCom={groupCom}");
        }
        public async Task<MISDashBoard> GetLocationCount(int leavePeriod, int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetLocationCount?leavePeriod={leavePeriod}&groupCom={groupCom}");
        }
        public async Task<MISDashBoard> GetDivisionCount(int leavePeriod, int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetDivisionCount?leavePeriod={leavePeriod}&groupCom={groupCom}");
        }
        public async Task<MISDashBoard> GetCategoryCount(int leavePeriod, int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetCategoryCount?leavePeriod={leavePeriod}&groupCom={groupCom}");
        }
        public async Task<MISDashBoard> GetGradeCount(int leavePeriod, int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetGradeCount?leavePeriod={leavePeriod}&groupCom={groupCom}");
        }
        public async Task<MISDashBoard> GetRegionCount(int leavePeriod, int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetRegionCount?leavePeriod={leavePeriod}&groupCom={groupCom}");
        }
        public async Task<MISDashBoard> GetCatagoryLeaveCount(int leavePeriod, int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetCatagoryLeaveCount?leavePeriod={leavePeriod}&groupCom={groupCom}");
        }

        #endregion

        #region Employee Analysis

        #region List
        public async Task<MISDashBoard> GetEmployeeSkillDetail(int EmployeeCode)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetEmployeeSkillInYear?Employeecode={EmployeeCode}");
        }
        public async Task<MISDashBoard> GetEmployeeRewardDetail(int EmployeeCode)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetEmployeeRewardInYear?Employeecode={EmployeeCode}");
        }
        public async Task<MISDashBoard> GetEmployeeLeaveDetail(int EmployeeCode)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetEmployeeLeaveInYear?Employeecode={EmployeeCode}");
        }

        #endregion

        public async Task<MISDashBoard> GetEmployeeList(int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetEmployeeList?groupCom={groupCom}");
        }

        public async Task<MISDashBoard> GetEmployeeInformation(int empCode)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetEmployeeInformation?empCode={empCode}");
        }
        public async Task<MISDashBoard> GetEmployeeLeaveTrend(int empCode)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetEmployeeLeaveTrend?empCode={empCode}");
        }
        public async Task<MISDashBoard> GetEmployeeMilestones(int empCode)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetEmployeeMilestones?empCode={empCode}");
        }
        public async Task<MISDashBoard> GetEmployeeRewards(int empCode)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetEmployeeRewards?empCode={empCode}");
        }
        public async Task<MISDashBoard> GetEmployeeSkills(int empCode)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetEmployeeSkills?empCode={empCode}");
        }
        public async Task<MISDashBoard> GetEmployeeMap(int empCode)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetEmployeeMap?empCode={empCode}");
        }
        #endregion

        #region Salary Analysis

        public async Task<MISDashBoard> GetSalaryInformation(int finCode, int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetSalaryInformation?finCode={finCode}&groupCom={groupCom}");
        }
        public async Task<MISDashBoard> GetSalaryIncrementInformation(int finCode, int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetSalaryIncrementInformation?finCode={finCode}&groupCom={groupCom}");
        }
        public async Task<MISDashBoard> GetSalaryTrend(int finCode, int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetSalaryTrend?finCode={finCode}&groupCom={groupCom}");
        }
        public async Task<MISDashBoard> GetSalaryIncrementRange(int finCode, int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetSalaryIncrementRange?finCode={finCode}&groupCom={groupCom}");
        }
        public async Task<MISDashBoard> GetSalaryDepartmentWise(int finCode, int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetSalaryDepartmentWise?finCode={finCode}&groupCom={groupCom}");
        }
        public async Task<MISDashBoard> GetSalaryLocationWise(int finCode, int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetSalaryLocationWise?finCode={finCode}&groupCom={groupCom}");
        }
        public async Task<MISDashBoard> GetSalaryDivisionWise(int finCode, int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetSalaryDivisionWise?finCode={finCode}&groupCom={groupCom}");
        }
        public async Task<MISDashBoard> GetSalaryGradeWise(int finCode, int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetSalaryGradeWise?finCode={finCode}&groupCom={groupCom}");
        }
        public async Task<MISDashBoard> GetSalaryCategoryWise(int finCode, int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetSalaryCategoryWise?finCode={finCode}&groupCom={groupCom}");
        }
        public async Task<MISDashBoard> GetSalaryRegionWise(int finCode, int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetSalaryRegionWise?finCode={finCode}&groupCom={groupCom}");
        }
        public async Task<MISDashBoard> GetAllowanceMonthComparison(int month, int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetAllowanceMonthComparison?month={month}&groupCom={groupCom}");
        }
        public async Task<MISDashBoard> GetDeductionMonthComparison(int month, int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetDeductionMonthComparison?month={month}&groupCom={groupCom}");
        }
        public async Task<MISDashBoard> GetSalaryAllowance(int finCode, int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetSalaryAllowance?finCode={finCode}&groupCom={groupCom}");
        }
        public async Task<MISDashBoard> GetSalaryDeduction(int finCode, int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetSalaryDeduction?finCode={finCode}&groupCom={groupCom}");
        }
        public async Task<MISDashBoard> GetSalaryList(int finCode, int groupCom, string type, string text)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetSalaryList?finCode={finCode}&groupCom={groupCom}&type={type}&text={text}");
        }
        #endregion

        #region Absteesm 
        public async Task<MISDashBoard> GetAbsRate(string from, string to)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetAbsRate?from={from}&to={to}");
        }
        public async Task<MISDashBoard> GetAbsMonth(string from, string to)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetAbsMonth?from={from}&to={to}");
        }
        public async Task<MISDashBoard> GetAbsAge(string from, string to)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetAbsAge?from={from}&to={to}");
        }
        public async Task<MISDashBoard> GetAbsTenure(string from, string to)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetAbsTenure?from={from}&to={to}");
        }
        public async Task<MISDashBoard> GetAbsDepartment(string from, string to)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetAbsDepartment?from={from}&to={to}");
        }
        public async Task<MISDashBoard> GetAbsLocation(string from, string to)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetAbsLocation?from={from}&to={to}");
        }
        public async Task<MISDashBoard> GetAbsDivision(string from, string to)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetAbsDivision?from={from}&to={to}");
        }
        public async Task<MISDashBoard> GetAbsCategory(string from, string to)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetAbsCategory?from={from}&to={to}");
        }
        public async Task<MISDashBoard> GetAbsGrade(string from, string to)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetAbsGrade?from={from}&to={to}");
        }
        public async Task<MISDashBoard> GetAbsRegion(string from, string to)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetAbsRegion?from={from}&to={to}");
        }
        public async Task<MISDashBoard> GetAbsDays(string from, string to)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetAbsDays?from={from}&to={to}");
        }
        public async Task<MISDashBoard> GetAbsMonthWiseDetail(DateTime FROMDATE, DateTime TODATE, int GROUPCOMPANYCODE, int MONTHCODE)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetAbsMonthWiseDetail?FROMDATE={FROMDATE}&TODATE={TODATE}&GROUPCOMPANYCODE={GROUPCOMPANYCODE}&MONTHCODE={MONTHCODE}");
        }

        public async Task<MISDashBoard> GetAbsAgeSlabWiseDetail(DateTime FROMDATE, DateTime TODATE, int GROUPCOMPANYCODE, string AGESLAB)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetAbsAgeSlabWiseDetail?FROMDATE={FROMDATE}&TODATE={TODATE}&GROUPCOMPANYCODE={GROUPCOMPANYCODE}&AGESLAB={AGESLAB}");
        }

        public async Task<MISDashBoard> GetAbsServiceDetail(DateTime FROMDATE, DateTime TODATE, int GROUPCOMPANYCODE, string YEARSLAB)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetAbsServiceDetail?FROMDATE={FROMDATE}&TODATE={TODATE}&GROUPCOMPANYCODE={GROUPCOMPANYCODE}&YEARSLAB={YEARSLAB}");
        }
        public async Task<MISDashBoard> GetAbsDepartmentDetail(DateTime FROMDATE, DateTime TODATE, int GROUPCOMPANYCODE, string DEPARTMENT)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetAbsDepartmentDetail?FROMDATE={FROMDATE}&TODATE={TODATE}&GROUPCOMPANYCODE={GROUPCOMPANYCODE}&DEPARTMENT={DEPARTMENT}");
        }
        public async Task<MISDashBoard> GetAbsLocationDetail(DateTime FROMDATE, DateTime TODATE, int GROUPCOMPANYCODE, string LOCATION)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetAbsLocationDetail?FROMDATE={FROMDATE}&TODATE={TODATE}&GROUPCOMPANYCODE={GROUPCOMPANYCODE}&LOCATION={LOCATION}");
        }


        public async Task<MISDashBoard> GetAbsWeekdayDetail(DateTime FROMDATE, DateTime TODATE, int GROUPCOMPANYCODE, string DAY)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetAbsWeekdayDetail?FROMDATE={FROMDATE}&TODATE={TODATE}&GROUPCOMPANYCODE={GROUPCOMPANYCODE}&DAY={DAY}");
        }

        public async Task<MISDashBoard> GetAbsDivisionDetail(DateTime FROMDATE, DateTime TODATE, int GROUPCOMPANYCODE, string DIVISION)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetAbsDivisionDetail?FROMDATE={FROMDATE}&TODATE={TODATE}&GROUPCOMPANYCODE={GROUPCOMPANYCODE}&DIVISION={DIVISION}");
        }
        public async Task<MISDashBoard> GetAbsCategoryDetail(DateTime FROMDATE, DateTime TODATE, int GROUPCOMPANYCODE, string CATEGORY)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetAbsCategoryDetail?FROMDATE={FROMDATE}&TODATE={TODATE}&GROUPCOMPANYCODE={GROUPCOMPANYCODE}&CATEGORY={CATEGORY}");
        }
        public async Task<MISDashBoard> GetAbsRegionDetail(DateTime FROMDATE, DateTime TODATE, int GROUPCOMPANYCODE, string REGION)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetAbsRegionDetail?FROMDATE={FROMDATE}&TODATE={TODATE}&GROUPCOMPANYCODE={GROUPCOMPANYCODE}&REGION={REGION}");
        }
        public async Task<MISDashBoard> GetAbsGradeDetail(DateTime FROMDATE, DateTime TODATE, int GROUPCOMPANYCODE, string GRADE)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetAbsGradeDetail?FROMDATE={FROMDATE}&TODATE={TODATE}&GROUPCOMPANYCODE={GROUPCOMPANYCODE}&GRADE={GRADE}");
        }
        #endregion

        #region Historical Trends
        public async Task<MISDashBoard> GetHistoricalJoiningCount(int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetHistoricalJoiningCount?groupCom=" + groupCom);
        }
        public async Task<MISDashBoard> GetHistoricalTerminationCount(int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetHistoricalTerminationCount?groupCom=" + groupCom);
        }
        public async Task<MISDashBoard> GetHistoricalBadHiresCount(int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetHistoricalBadHiresCount?groupCom=" + groupCom);
        }
        public async Task<MISDashBoard> GetHistoricalJoiningCountMonth()
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetHistoricalJoiningCountMonth");
        }
        public async Task<MISDashBoard> GetHistoricalTerminationsCountMonth()
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetHistoricalTerminationsCountMonth");
        }
        public async Task<MISDashBoard> GetHistoricalAttritionMonth()
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetHistoricalAttritionMonth");
        }
        public async Task<MISDashBoard> GetHistoricalWFH(int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetHistoricalWFH?groupCom=" + groupCom);
        }
        public async Task<MISDashBoard> GetHistoricalSalary(int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetHistoricalSalary?groupCom=" + groupCom);
        }
        public async Task<MISDashBoard> GetHistoricalSalaryInc(int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetHistoricalSalaryInc?groupCom=" + groupCom);
        }

        #region List

        public async Task<MISDashBoard> GetHistoricalList(int groupCom, string type, string age)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetHistoricalList?groupCom={groupCom}&type={type}&text={age}");
        }

        #endregion


        #endregion

        #region Recruitmrnt Analysis

        public async Task<MISDashBoard> GetRecruitmentInformation(int finCode, int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetRecruitmentInformation?finCode={finCode}&groupCom={groupCom}");
        }
        public async Task<MISDashBoard> GetRecruitmentVacancyTrend(int finCode, int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetRecruitmentVacancyTrend?finCode={finCode}&groupCom={groupCom}");
        }
        public async Task<MISDashBoard> GetTopVacancyDesignation(int finCode, int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetTopVacancyDesignation?finCode={finCode}&groupCom={groupCom}");
        }
        public async Task<MISDashBoard> GetVacancyDetails(int FinancialYear, int GrpCompanyCode, string MONTH)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetVacancyDetails?FinancialYear={FinancialYear}&GrpCompanyCode={GrpCompanyCode}&MONTH={MONTH}");
        }

        public async Task<MISDashBoard> GetRecruitmentInformations(int FinancialYear, int GrpCompanyCode, string TYPE)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetRecruitmentInformations?FinancialYear={FinancialYear}&GrpCompanyCode={GrpCompanyCode}&TYPE={TYPE}");
        }
        public async Task<MISDashBoard> GetVacancyDesignationDetails(int FinancialYear, int GrpCompanyCode, string TYPE)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetVacancyDesignationDetails?FinancialYear={FinancialYear}&GrpCompanyCode={GrpCompanyCode}&TYPE={TYPE}");
        }
        #endregion

        #region HR at Glance
        public async Task<MISDashBoard> GetHrInformation(int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetHrInformation?groupCom={groupCom}");
        }
        public async Task<MISDashBoard> GetSalaryRange(int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetSalaryRange?groupCom={groupCom}");
        }
        public async Task<MISDashBoard> GetGenderHR(int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetGenderHR?groupCom={groupCom}");
        }
        public async Task<MISDashBoard> GetDepartmentHR(int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetDepartmentHR?groupCom={groupCom}");
        }
        public async Task<MISDashBoard> GetAgeHR(int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetAgeHR?groupCom={groupCom}");
        }
        public async Task<MISDashBoard> GetTenureHR(int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetTenureHR?groupCom={groupCom}");
        }
        public async Task<MISDashBoard> GetTimeToQuitJobHR(int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetTimeToQuitJobHR?groupCom={groupCom}");
        }
        public async Task<MISDashBoard> GetLocationHR(int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetLocationHR?groupCom={groupCom}");
        }
        public async Task<MISDashBoard> GetRegionHR(int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetRegionHR?groupCom={groupCom}");
        }
        public async Task<MISDashBoard> GetDivisionHR(int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetDivisionHR?groupCom={groupCom}");
        }
        public async Task<MISDashBoard> GetGradeHR(int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetGradeHR?groupCom={groupCom}");
        }
        public async Task<MISDashBoard> GetCategoryHR(int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetCategoryHR?groupCom={groupCom}");
        }

        #region List


        public async Task<MISDashBoard> GetActiveEmployeeCountList(int groupCom, string type, string age)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetActiveEmployeeCountList?groupCom={groupCom}&type={type}&text={age}");
        }

        #endregion


        #endregion

        #region Workforce Time Trends

        public async Task<MISDashBoard> GetTimeOfficeHR(int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetTimeOfficeHR?groupCom={groupCom}");
        }
        public async Task<MISDashBoard> GetTODepartmentHR(int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetTODepartmentHR?groupCom={groupCom}");
        }
        public async Task<MISDashBoard> GetTOLocationHR(int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetTOLocationHR?groupCom={groupCom}");
        }
        public async Task<MISDashBoard> GetTODivisionHR(int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetTODivisionHR?groupCom={groupCom}");
        }
        public async Task<MISDashBoard> GetTOCategoryHR(int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetTOCategoryHR?groupCom={groupCom}");
        }
        public async Task<MISDashBoard> GetTORegionHR(int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetTORegionHR?groupCom={groupCom}");
        }
        public async Task<MISDashBoard> GetTOGradeHR(int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetTOGradeHR?groupCom={groupCom}");
        }

        #endregion

        #region Attendance OverView

        public async Task<MISDashBoard> GetMonths(int FinancialYearCode)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetMonths?FinancialYearCode={FinancialYearCode}");
        }
        public async Task<MISDashBoard> GetAttendanceDayWise(int month, int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetAttendanceDayWise?month={month}&groupCom={groupCom}");
        }
        public async Task<MISDashBoard> GetAttendanceMonthData(int month, int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetAttendanceMonthData?month={month}&groupCom={groupCom}");
        }
        public async Task<MISDashBoard> GetAttendanceDateWise(int day, int month, int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetAttendanceDateWise?day={day}&month={month}&groupCom={groupCom}");
        }

        #region List

        public async Task<MISDashBoard> GetAttendanceDateList(int day, int month, int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetAttendanceDateList?day={day}&month={month}&groupCom={groupCom}");
        }

        #endregion


        #endregion

        #region PayRollOverView
        public async Task<MISDashBoard> GetEmpData(int month, int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetEmpData?month={month}&groupCom={groupCom}");
        }

        public async Task<MISDashBoard> GetPRAllowance(int month, int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetPRAllowance?month={month}&groupCom={groupCom}");
        }
        public async Task<MISDashBoard> GetPRDeduction(int month, int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetPRDeduction?month={month}&groupCom={groupCom}");
        }
        public async Task<MISDashBoard> GetPRCTC(int month, int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetPRCTC?month={month}&groupCom={groupCom}");
        }
        public async Task<MISDashBoard> GetPG(int month, int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetPG?month={month}&groupCom={groupCom}");
        }
        public async Task<MISDashBoard> GetPFData(int month, int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetPFData?month={month}&groupCom={groupCom}");
        }
        public async Task<MISDashBoard> GetAlwData(int month, int groupCom, string type)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetAlwData?month={month}&groupCom={groupCom}&type={type}");
        }
        #endregion

        #region LeaveOverView
        public async Task<MISDashBoard> GetLeaveType(int month, int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetLeaveType?month={month}&groupCom={groupCom}");
        }
        public async Task<MISDashBoard> GetLeaveDays(int month, int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetLeaveDays?month={month}&groupCom={groupCom}");
        }
        public async Task<MISDashBoard> GetLeaveDepartment(int month, int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetLeaveDepartment?month={month}&groupCom={groupCom}");
        }
        public async Task<MISDashBoard> GetLeaveLocation(int month, int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetLeaveLocation?month={month}&groupCom={groupCom}");
        }
        public async Task<MISDashBoard> GetLeaveDivision(int month, int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetLeaveDivision?month={month}&groupCom={groupCom}");
        }
        public async Task<MISDashBoard> GetLeaveCategory(int month, int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetLeaveCategory?month={month}&groupCom={groupCom}");
        }
        public async Task<MISDashBoard> GetLeaveGrade(int month, int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetLeaveGrade?month={month}&groupCom={groupCom}");
        }
        public async Task<MISDashBoard> GetLeaveRegion(int month, int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetLeaveRegion?month={month}&groupCom={groupCom}");
        }
        #endregion 
        public async Task<MISDashBoard> GetPFDatas(int month, int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetPFData?month={month}&groupCom={groupCom}");
        }
        #region Late in OverView
        public async Task<MISDashBoard> GetLateInDepartment(int month, int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetLateInDepartment?month={month}&groupCom={groupCom}");
        }
        public async Task<MISDashBoard> GetLateInLocation(int month, int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetLateInLocation?month={month}&groupCom={groupCom}");
        }
        public async Task<MISDashBoard> GetLateInDivision(int month, int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetLateInDivision?month={month}&groupCom={groupCom}");
        }
        public async Task<MISDashBoard> GetLateInCategory(int month, int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetLateInCategory?month={month}&groupCom={groupCom}");
        }
        public async Task<MISDashBoard> GetLateInGrade(int month, int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetLateInGrade?month={month}&groupCom={groupCom}");
        }
        public async Task<MISDashBoard> GetLateInRegion(int month, int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetLateInRegion?month={month}&groupCom={groupCom}");
        }

        public async Task<MISDashBoard> GetAttendanceList(int groupCom, int month, string type, int code)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetAttendanceList?groupCom={groupCom}&month={month}&type={type}&code={code}");
        }

        #endregion

        #region Emloyee OverView
        public async Task<MISDashBoard> GetEmpGeoTagStatus(int month, int empCode)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetEmpGeoTagStatus?month={month}&empCode={empCode}");
        }
        public async Task<MISDashBoard> GetAttendanceEmpMonthData(int month, int empCode)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetAttendanceEmpMonthData?month={month}&empCode={empCode}");
        }
        public async Task<MISDashBoard> GetEmployeeListOv(int month, int groupCom)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetEmployeeListOv?month={month}&groupCom={groupCom}");
        }
        public async Task<MISDashBoard> GetAttendanceEmpDayWise(int month, int empCode)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetAttendanceEmpDayWise?month={month}&empCode={empCode}");
        }
        public async Task<MISDashBoard> GetEmployeeLateInEarlyOut(int month, int empCode)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetEmployeeLateInEarlyOut?month={month}&empCode={empCode}");
        }
        #endregion

        #region Grievance

        public async Task<MISDashBoard> GetGrievanceByFinancialYear(int GrpCompanyCode, int financialyear)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetGrievenceCount?GrpCompanyCode={GrpCompanyCode}&financialyear={financialyear}");
        }

        public async Task<MISDashBoard> GetGrievanceCompletedByFinancialYear(int GrpCompanyCode, int financialyear)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetGrievenceCountCompleted?GrpCompanyCode={GrpCompanyCode}&financialyear={financialyear}");
        }

        public async Task<MISDashBoard> GetGrievanceAcceptedByFinancialYear(int GrpCompanyCode, int financialyear)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetGrievenceCountAccepted?GrpCompanyCode={GrpCompanyCode}&financialyear={financialyear}");
        }

        public async Task<MISDashBoard> GetGrievanceNonAcceptedByFinancialYear(int GrpCompanyCode, int financialyear)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetGrievenceCountNonAccepted?GrpCompanyCode={GrpCompanyCode}&financialyear={financialyear}");
        }



        public async Task<MISDashBoard> GetGrievanceRejectedByFinancialYear(int GrpCompanyCode, int financialyear)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetGrievenceCountRejected?GrpCompanyCode={GrpCompanyCode}&financialyear={financialyear}");
        }

        #region List
        public async Task<MISDashBoard> GetGrievenceCountGraphDataView(int GrievanceCatid, int FinancialYear, int GrpCompanyCode, string TYPE)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetGrievenceCountGraphDataView?GrievanceCatid={GrievanceCatid}&FinancialYear={FinancialYear}&GrpCompanyCode={GrpCompanyCode}&TYPE={TYPE}");
        }

        #endregion

        #endregion

        #region Transition Type

        public async Task<MISDashBoard> GetTransitionTypeCount(int groupCom, int financialYr, int typeCode)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetTransitionTypeCount?groupCom={groupCom}&financialYr={financialYr}&typeCode={typeCode}");
        }
        public async Task<MISDashBoard> GetTransitionTypeDepartment(int groupCom, int financialYr, int typeCode)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetTransitionTypeDepartment?groupCom={groupCom}&financialYr={financialYr}&typeCode={typeCode}");
        }
        public async Task<MISDashBoard> GetTransitionTypeLocation(int groupCom, int financialYr, int typeCode)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetTransitionTypeLocation?groupCom={groupCom}&financialYr={financialYr}&typeCode={typeCode}");
        }
        public async Task<MISDashBoard> GetTransitionTypeDivision(int groupCom, int financialYr, int typeCode)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetTransitionTypeDivision?groupCom={groupCom}&financialYr={financialYr}&typeCode={typeCode}");
        }
        public async Task<MISDashBoard> GetTransitionTypeCategory(int groupCom, int financialYr, int typeCode)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetTransitionTypeCategory?groupCom={groupCom}&financialYr={financialYr}&typeCode={typeCode}");
        }
        public async Task<MISDashBoard> GetTransitionTypeGrade(int groupCom, int financialYr, int typeCode)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetTransitionTypeGrade?groupCom={groupCom}&financialYr={financialYr}&typeCode={typeCode}");
        }
        public async Task<MISDashBoard> GetTransitionTypeRegion(int groupCom, int financialYr, int typeCode)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetTransitionTypeRegion?groupCom={groupCom}&financialYr={financialYr}&typeCode={typeCode}");
        }

        #region List    

        public async Task<MISDashBoard> GetTransitionEmployeeCount(int groupCom, int financialYr, int typeCode, string type, int text)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetTransitionEmployeeCount?groupCom={groupCom}&financialYr={financialYr}&typeCode={typeCode}&type={type}&text={text}");
        }

        #endregion

        #endregion
        public async Task<MISDashBoard> GetLeaveEmployeeList(int leavePeriod, int groupCom, string type, string code)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetLeaveEmployeeList?leavePeriod={leavePeriod}&groupCom={groupCom}&type={type}&code={code}");
        }




        public async Task<MISDashBoard> GetBadHiresMonth(int groupCom, int financialYr)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetBadHiresMonth?groupCom={groupCom}&financialYr={financialYr}");
        }
        public async Task<MISDashBoard> GetBadHiresAge(int groupCom, int financialYr)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetBadHiresAge?groupCom={groupCom}&financialYr={financialYr}");
        }
        public async Task<MISDashBoard> GetBadHiresGender(int groupCom, int financialYr)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetBadHiresGender?groupCom={groupCom}&financialYr={financialYr}");
        }
        public async Task<MISDashBoard> GetBadHiresDepartment(int groupCom, int financialYr)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetBadHiresDepartment?groupCom={groupCom}&financialYr={financialYr}");
        }
        public async Task<MISDashBoard> GetBadHiresLocation(int groupCom, int financialYr)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetBadHiresLocation?groupCom={groupCom}&financialYr={financialYr}");
        }
        public async Task<MISDashBoard> GetBadHiresDivision(int groupCom, int financialYr)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetBadHiresDivision?groupCom={groupCom}&financialYr={financialYr}");
        }
        public async Task<MISDashBoard> GetBadHiresCategory(int groupCom, int financialYr)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetBadHiresCategory?groupCom={groupCom}&financialYr={financialYr}");
        }
        public async Task<MISDashBoard> GetBadHiresGrade(int groupCom, int financialYr)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetBadHiresGrade?groupCom={groupCom}&financialYr={financialYr}");
        }
        public async Task<MISDashBoard> GetBadHiresRegion(int groupCom, int financialYr)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetBadHiresRegion?groupCom={groupCom}&financialYr={financialYr}");
        }
        public async Task<MISDashBoard> GetBadHiresEmpList(int groupCom, int financialYr, string type, string code)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetBadHiresEmpList?groupCom={groupCom}&financialYr={financialYr}&type={type}&code={code}");
        }
        public async Task<MISDashBoard> GetYearlyTrainingCost(int financialYr)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetYearlyTrainingCost?financialYr={financialYr}");
        }
        public async Task<MISDashBoard> GetPerHeadTrainingCost(int financialYr)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetPerHeadTrainingCost?financialYr={financialYr}");
        }
        public async Task<MISDashBoard> GetTrainingEmployeeCount(int financialYr)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetTrainingEmployeeCount?financialYr={financialYr}");
        }
        public async Task<MISDashBoard> GetMonthlyTrainings(int financialYr)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetMonthlyTrainings?financialYr={financialYr}");
        }
        public async Task<MISDashBoard> GetTrainingEmp(int financialYr, int code)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetTrainingEmp?financialYr={financialYr}&code={code}");
        }
        public async Task<MISDashBoard> GetTrainingCostList(int code)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetTrainingCostList?code={code}");
        }

        public async Task<MISDashBoard> GetDashBoardDiagrams(string Classification)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetDashBoardDiagrams?Classification={Classification}");
        }

        public async Task<MISDashBoard> GetAttendanceCount(int financialYr,int  groupCom,int AttendanceCode)
        {
            return await _httpService.GetAsync<MISDashBoard>($"{_baseURL}/GetAttendanceCount?financialYr={financialYr}&groupCom={groupCom}&AttendanceCode={AttendanceCode}");
        }
    }
}
