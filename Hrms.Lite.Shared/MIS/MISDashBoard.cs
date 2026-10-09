using DocumentFormat.OpenXml.Bibliography;
using Hrms.Lite.Shared.DashBoard;
using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.Master;
using Hrms.Lite.Shared.PayRoll.Reports;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Hrms.Lite.Shared.MIS
{
    public class MISDashBoard
    {
        public int GroupCompanyCode { get; set; }
        public int TransitionTypeCode { get; set; }
        public List<BadHiresAge> BadHiresAgeList { get; set; }
        public List<BadHiresMonth> BadHiresMonthList { get; set; }
        public List<BadHiresGender> BadHiresGenderList { get; set; }
        public List<BadHiresDepartment> BadHiresDepartmentList { get; set; }
        public List<BadHiresLocation> BadHiresLocationList { get; set; }
        public List<BadHiresCategory> BadHiresCategoryList { get; set; }
        public List<BadHiresDivision> BadHiresDivisionList { get; set; }
        public List<BadHiresRegion> BadHiresRegionList { get; set; }
        public List<BadHiresGrade> BadHiresGradeList { get; set; }
        public List<BadHiresEmp> BadHiresEmpList { get; set; }
        public List<LeaveEmployeeList> LeaveEmployeeList { get; set; }
        public List<ABSEENTISM> ABSEENTISM { get; set; }
        public List<RECRUITMENT> RECRUITMENT { get; set; }
        public List<MISDashBoardList> ManPowerChart { get; set; }
        public List<MISDashBoardList> MISDashBoardList { get; set; }
        public List<AbsenteeismList> TimeOfficeDashBoardList { get; set; }
        public List<GenderResignationCountList> GenderResignationCountList { get; set; }
        public List<MonthWiseTerminationList> MonthWiseTerminationList { get; set; }
        public List<GenderResignationReasonCountList> GenderResignationReasonCountList { get; set; }
        public List<AgeBandResignationCountList> AgeBandResignationCountList { get; set; }
        public List<AgeResignationCountList> AgeResignationCountList { get; set; }
        public List<TenureResignationCountList> TenureResignationCountList { get; set; }
        public List<TerminationsDepartmentCount> TerminationsDepartmentCountList { get; set; }
        public List<TerminationsLocationCount> TerminationsLocationCountList { get; set; }
        public List<TerminationsDivisionCount> TerminationsDivisionCountList { get; set; }
        public List<TerminationsGradeCount> TerminationsGradeCountList { get; set; }
        public List<TerminationsCategoryCount> TerminationsCategoryCountList { get; set; }
        public List<TerminationsRegionCount> TerminationsRegionCountList { get; set; }

        public List<GenderJoinersCountList> GenderJoinersCountList { get; set; }
        public List<AgeBandJoinersCountList> AgeBandJoinersCountList { get; set; }
        public List<GenderJoinersDepartmentCountList> GenderJoinersDepartmentCountList { get; set; }
        public List<GenderJoinersLocationCountList> GenderJoinersLocationCountList { get; set; }
        
        public List<GenderJoinersDivisionCountList> GenderJoinersDivisionCountList { get; set; }
        public List<GenderJoinersGradeCountList> GenderJoinersGradeCountList { get; set; }
        public List<GenderJoinersCategoryCountList> GenderJoinersCategoryCountList { get; set; }
        public List<GenderJoinersRegionCountList> GenderJoinersRegionCountList { get; set; }
        public List<BadHires> BadHiresList { get; set; }

        public List<MonthWiseLeaveList> MonthWiseLeaveList { get; set; }
        public List<AgeBandLeaveCountList> AgeBandLeaveCountList { get; set; }
        public List<DepartmentCountList> DepartmentCountList { get; set; }       
        public List<LocationCountList> LocationCountList { get; set; }
        public List<CategoryCountList> CategoryCountList { get; set; }
        public List<RegionCountList> RegionCountList { get; set; }
        public List<GradeCountList> GradeCountList { get; set; }
        public List<DivisionCountList> DivisionCountList { get; set; }
        public List<CatagoryLeaveCountList> CatagoryLeaveCountList { get; set; }

        public List<Employee_MIS_List> Employee_MIS_List { get; set; }
        public List<MonthWiseLeaveTrend> MonthWiseLeaveTrend { get; set; }
        public List<EmployeeMilestones> EmployeeMilestones { get; set; }
        public List<EmployeeRewards> EmployeeRewards { get; set; }
        public List<EmployeeSkills> EmployeeSkills { get; set; }
        public List<EmployeeMap> EmployeeMap { get; set; }

        public int ManPowerTotal { get; set; }
        public LeavePeriod LeavePeriod { get; set; }
        public int resignationCount { get; set; }
        public int averageResignationCount { get; set; }
        public Decimal attritionRate { get; set; }
        public int joiningCount { get; set; }
        public Decimal averageJoinersCount { get; set; }
        public int badHires { get; set; }
        public int badHireDays { get; set; }

        public Decimal leaveCount { get; set; }
        public Decimal avgLeave { get; set; }
        public Decimal lopCount { get; set; }
        public Decimal absCount { get; set; }

        public DateTime? From { get; set; }
        public DateTime? To { get; set; }

        public string fullName { get; set; }
        public string dob { get; set; }
        public string doj { get; set; }
        public string dot { get; set; }
        public string department { get; set; }
        public string designation { get; set; }
        public string location { get; set; }
        public decimal grossPay { get; set; }
        public decimal ctc { get; set; }
        public decimal leave { get; set; }
        public decimal leaveBalance { get; set; }
        public string proPic { get; set; }

        public string totalSalary { get; set; }
        public int avgSalary { get; set; }
        public int finYear { get; set; }
        public int AttendanceStatusTypeCode { get; set; }
        public string totalIncrement { get; set; }
        public int avgIncrement { get; set; }
        public string totalMonth { get; set; }
        public string avgCom { get; set; }

        public int TodayCount { get; set; }
        public int TomorrowCount { get; set; }
        public int YesterdayCount { get; set; }
        public int CurrentWeakCount { get; set; }
        public int CurrentMonthCount { get; set; }
        public int PreviousMonthCount { get; set; }
        public List<MonthWiseSalaryTrend> MonthWiseSalaryTrend { get; set; }
        public List<SalaryIncrementRange> SalaryIncrementRange { get; set; }
        public List<SalaryDepartmentWise> SalaryDepartmentWise { get; set; }
        public List<SalaryLocationWise> SalaryLocationWise { get; set; }
        public List<SalaryDivisionWise> SalaryDivisionWise { get; set; }
        public List<SalaryGradeWise> SalaryGradeWise { get; set; }
        public List<SalaryCategoryWise> SalaryCategoryWise { get; set; }
        public List<SalaryRegionWise> SalaryRegionWise { get; set; }
        public List<AllowanceMonthComparisonList> AllowanceMonthComparison { get; set; }
        public List<DeductionMonthComparisonList> DeductionMonthComparison { get; set; }
        public List<SalaryAllowance> SalaryAllowanceList { get; set; }
        public List<SalaryDeduction> SalaryDeductionList { get; set; }
    


        public List<YearlyTrainingCost> YearlyTrainingCostList { get; set; }
        public List<PerHeadTrainingCost> PerHeadTrainingCostList { get; set; }
        public List<TrainingEmployeeCount> TrainingEmployeeCountList { get; set; }
        public List<MonthlyTrainings> MonthlyTrainingsList { get; set; }
        public List<TrainingEmp> TrainingEmpList { get; set; }
        public List<TrainingCostList> TrainingCostList { get; set; }
        public decimal absRate { get; set; }
        public decimal absenteesmCount { get; set; }
        public List<AbsMonthWise> AbsMonthWise { get; set; }
        public List<AgeBandAbsList> AgeBandAbsList { get; set; }
        public List<TenureAbsList> TenureAbsList { get; set; }
        public List<AbsDepartmentList> AbsDepartmentList { get; set; }
        public List<AbsLocationList> AbsLocationList { get; set; }
        public List<AbsDivisionList> AbsDivisionList { get; set; }
        public List<AbsCategoryList> AbsCategoryList { get; set; }
        public List<AbsRegionList> AbsRegionList { get; set; }
        public List<AbsGradeList> AbsGradeList { get; set; }
        public List<GetAbsDaysList> GetAbsDaysList { get; set; }

        public List<GetHistoricalJoiningCountList> GetHistoricalJoiningCountList { get; set; }
        public List<GetHistoricalTerminationCountList> GetHistoricalTerminationCountList { get; set; }
        public List<GetHistoricalBadHires> HistoricalBadHiresList { get; set; }
        public List<GetHistoricalJoiningCountMonthList> GetHistoricalJoiningCountMonthList { get; set; }
        public List<GetHistoricalTerminationsCountMonthList> GetHistoricalTerminationsCountMonthList { get; set; }
        public List<GetHistoricalAttritionMonthList> GetHistoricalAttritionMonthList { get; set; }
        public List<GetHistoricalWFHList> HistoricalWFHList { get; set; }
        public List<GetHistoricalLOPList> HistoricalLOPList { get; set; }
        public List<GetHistoricalSalaryList> HistoricalSalaryList { get; set; }
        public List<GetHistoricalSalaryIncList> HistoricalSalaryIncList { get; set; }

        public int vacancy { get; set; }
        public int interviewed { get; set; }
        public int offered { get; set; }
        public int joined { get; set; }
        public List<VacancyTrend> VacancyTrendList { get; set; }
        public List<TopVacancyDesignation> TopVacancyDesignationList { get; set; }

        public int Type { get; set; }


        public int employeeCount { get; set; }
        public int avgAge { get; set; }
        public int employeeMaleCount { get; set; }
        public int employeeFeMaleCount { get; set; }
        public int avgGP { get; set; }
        public decimal avgTenure { get; set; }
        public int noticeperiod { get; set; }
        public int LoanBalance { get; set; }
        public int RetirementDue { get; set; }

        public List<SalaryRange> SalaryRangeList { get; set; }
        public List<HRGender> HRGenderList { get; set; }
        public List<HRDepartment> HRDepartmentList { get; set; }
        public List<HRRegion> HRRegionList { get; set; }
        public List<HRDivision> HRDivisionList { get; set; }
        public List<HRGrade> HRGradeList { get; set; }
        public List<HRCategory> HRCategoryList { get; set; }
        public List<HRAge> HRAgeList { get; set; }
        public List<HRTenure> HRTenureList { get; set; }
        public List<HRTimetoQuitJob> HRTimetoQuitJob { get; set; }
        public List<HRLocation> HRLocationList { get; set; }
        public List<HRTimeOffice> HRTimeOfficeList { get; set; }
        public List<HRTODepartment> HRTODepartmentList { get; set; }
        public List<HRTOLocation> HRTOLocationList { get; set; }
        public List<HRTODivision> HRTODivisionList { get; set; }
        public List<HRTOCategory> HRTOCategoryList { get; set; }
        public List<HRTORegion> HRTORegionList { get; set; }
        public List<HRTOGrade> HRTOGradeList { get; set; }

        public List<Months> Months { get; set; }
        public int? CurrentSalaryPeriodCode { get; set; }
        public List<AttendanceDayWise> AttendanceDayWiseList { get; set; }
        public List<AttendanceDateWise> AttendanceDateWiseList { get; set; }

        public int totalEmp { get; set; }
        public int totalEmpMale { get; set; }
        public int totalEmpFemale { get; set; }
        public int totalPrvEmp { get; set; }
        public int newHires { get; set; }
        public int resignations { get; set; }
        public int difference { get; set; }
        public int pfEmployer { get; set; }
        public int pfEmployee { get; set; }
        public int esiEmployer { get; set; }
        public int esiEmployee { get; set; }
        public List<AllowanceOverView> AllowanceOverView { get; set; }
        public List<DeductionOverView> DeductionOverView { get; set; }
        public List<CTCOverView> CTCOverView {get; set; }
        public List<PGOverView> PGOverView { get; set; }

        public int geoTagging { get; set; }
        public int allowance { get; set; }
        public int deduction { get; set; }
        public int netPay { get; set; }
        public decimal lateIn { get; set; }
        public decimal earlyOut { get; set; }
        public decimal ot { get; set; }
        public decimal th { get; set; }
        public List<GetLeaveDaysList> GetLeaveDaysList { get; set; }
        public List<EmployeeLateInEarlyOut> EmployeeLateInEarlyOut { get; set; }

        public List<SalaryList> SalaryList { get; set; }
        
        public List<MISGrievence> Grievence { get; set; }

        public List<TransitionTypeCount> TransitionTypeCountList { get; set; }
        public List<TransitionTypeDepartment> TransitionTypeDepartmentList { get; set; }
        public List<TransitionTypeLocation> TransitionTypeLocationList { get; set; }
        public List<TransitionTypeDivision> TransitionTypeDivisionList { get; set; }
        public List<TransitionTypeCategory> TransitionTypeCategoryList { get; set; }
        public List<TransitionTypeGrade> TransitionTypeGradeList { get; set; }
        public List<TransitionTypeRegion> TransitionTypeRegionList { get; set; }
        public List<AttendanceDateList> AttendanceDateList { get; set; } 
        public List<TransitionEmployeeCount> TransitionEmployeeCountList { get; set; }
        public List<PFEmployeeCount> PFEmployeeCountList { get; set; }
        public List<MISGrievenceList> MISGrievenceList { get; set; }
        public List<MISEmployeeCarrerDetails> MISEmployeeCarrerDetails { get; set; }
        public string FileName { get; set; }

    }
    public class MISEmployeeCarrerDetails
    {
        public int employeecode { get; set; }
        public string fullname { get; set; }
        public DateTime doj { get; set; }
        public string employeeid { get; set; }
        public DateTime dob { get; set; }
        public string grade { get; set; }
        public string gender { get; set; }
        public string status { get; set; }
        public string category { get; set; }
        public string designation { get; set; }
        public string department { get; set; }
        public string location { get; set; }
        public string division { get; set; }
        public string absoluteuri { get; set; }

        public DateTime dateofapplication { get; set; }
        public DateTime leavefrom { get; set; }
        public DateTime leaveto { get; set; }
        public string leavename { get; set; }
        public string leavereason { get; set; }
        public Decimal noofdays { get; set; }
        public string recommendedby { get; set; }
        public string recommendedfor { get; set; }
        public DateTime rewarddate { get; set; }
        public int points { get; set; }
        public string reward { get; set; }
        public string skillset { get; set; }
        public string skillname { get; set; }
        public string skilllevel { get; set; }

    }

    public class MISGrievenceList
    {
        public int grievanceCatID { get; set; }
        public int count { get; set; }
        public string name { get; set; }
        public string shortname { get; set; }

        public int grievancecode { get; set; }

        public string referenceno { get; set; }
        public string grievancegi { get; set; }
        public int companycode { get; set; }
        public int employeecode { get; set; }
        public string fullname { get; set; }
        public DateTime doj { get; set; }

        public string employeeid { get; set; }
        public string department { get; set; }

        public DateTime requesteddate { get; set; }
        public string grievance { get; set; }
        public string suggestion { get; set; }

        public string acceptanceapproved { get; set; }
        public DateTime acceptedon { get; set; }
        public string actiontaken { get; set; }

        public DateTime actiontakenon { get; set; }

        public string location { get; set; }
        public string division { get; set; }
        public DateTime dob { get; set; }
        public string grade { get; set; }
        public string gender { get; set; }
        public string status { get; set; }
        public string category { get; set; }
        public string absoluteuri { get; set; }
        public string fullName { get; set; }
        public string designation { get; set; }
        public string FileName { get; set; }
        
    }

    public class PFEmployeeCount
    {
        public int empCode { get; set; }
        public string empId { get; set; }
        public string fullName { get; set; }
        public string url { get; set; }
        public DateTime dob { get; set; }
        public DateTime doj { get; set; }
        public DateTime sepDate { get; set; }
        public decimal age { get; set; }
        public decimal years { get; set; }
        public string department { get; set; }
        public string location { get; set; }
        public string division { get; set; }
        public string catagory { get; set; }
        public string grade { get; set; }
        public string region { get; set; }
        public string designation { get; set; }
        public string gender { get; set; }
        public string status { get; set; }
        public string yos { get; set; }
        public int employeePF { get; set; }
        public int employerPF { get; set; }
        public int employeeESI { get; set; }
        public int employerESI { get; set; }
        public int allowance { get; set; }
        public string alwName { get; set; }
        public int deduction { get; set; }
        public string dedName { get; set; }
        public string absoluteurl { get; set; }
    }




    public class TransitionEmployeeCount
    {
        public int employeecode { get; set; }
        public string employeeid { get; set; }
        public string fullname { get; set; }
        public string designation { get; set; }
        public int departmentcode { get; set; }
        public string department { get; set; }
        public int locationcode { get; set; }
        public string location { get; set; }
        public int categorycode { get; set; }
        public string category { get; set; }
        public int divisioncode { get; set; }
        public string division { get; set; }
        public int gradecode { get; set; }
        public string grade { get; set; }
        public int regioncode { get; set; }
        public string region { get; set; }
        public DateTime doj { get; set; }
        public string gender { get; set; }
        public int typecode { get; set; }
        public string transitiontype { get; set; }
        public string absoluteurl { get; set; }
        public string FileName { get; set; }
    }



    public class AttendanceDateList
    {
        public int empCode { get; set; }
        public string empId { get; set; }
        public string fullName { get; set; }
        public string url { get; set; }
        public DateTime dob { get; set; }
        public DateTime doj { get; set; }
        public DateTime sepDate { get; set; }
        public decimal age { get; set; }
        public decimal years { get; set; }
        public string department { get; set; }
        public string location { get; set; }
        public string division { get; set; }
        public string catagory { get; set; }
        public string grade { get; set; }
        public string region { get; set; }
        public string designation { get; set; }
        public string gender { get; set; }
        public string status { get; set; }
        public string yos { get; set; }

        public string punchingDate { get; set; }
        public string lateIn { get; set; }
        public string earlyOut { get; set; }
        public string overTime { get; set; }
        public string totalHours { get; set; }

        public int total { get; set; }
        public string FileName { get; set; }
    }

    public class TransitionTypeCount
    {
        public int count { get; set; }
        public string month { get; set; }
    }
    //public class TransitionTypeDepartment
    //{
    //    public int count { get; set; }
    //    public string department { get; set; }
    //    public string fullname { get; set; }
    //}
    //public class TransitionTypeLocation
    //{
    //    public int count { get; set; }
    //    public string location { get; set; }
    //    public string fullname { get; set; }
    //}
    //public class TransitionTypeDivision
    //{
    //    public int count { get; set; }
    //    public string division { get; set; }
    //}
    //public class TransitionTypeCategory
    //{
    //    public int count { get; set; }
    //    public string category { get; set; }
    //}
    //public class TransitionTypeGrade
    //{
    //    public int count { get; set; }
    //    public string grade { get; set; }
    //}
    //public class TransitionTypeRegion
    //{
    //    public int count { get; set; }
    //    public string region { get; set; }
    //}

    public class MISGrievence
    {
        public int grievanceCatID { get; set; }
        public int count { get; set; }
        public string name { get; set; }
        public string shortname { get; set; }


    }


    public class Months
    {
        public int SalaryPeriodCode { get; set; }
        public string Month { get; set; }
    }

    public class MISDashBoardList
    {
        public string Type { get; set; }
        public int count { get; set; }
        public string Action { get; set; }
    }
    public class AbsenteeismList
    {
        public string Date { get; set; }
        public int count { get; set; }
    }

    public class GenderResignationCountList
    {
        public string name { get; set; }

        public int genCount { get; set; }
    }
    public class MonthWiseTerminationList
    {
        public string monthName { get; set; }

        public int ejocount { get; set; }

        public int esepcount { get; set; }

    }
    public class GenderResignationReasonCountList
    {
        public int code { get; set; }
        public string name { get; set; }

        public string shrtName { get; set; }

        public int reasonCount { get; set; }
    }
    public class TenureResignationCountList
    {
        public string years { get; set; }

        public int total { get; set; }
    }
    public class AgeBandResignationCountList
    {
        public string years { get; set; }

        public int total { get; set; }
    }
    public class AgeResignationCountList
    {
    
        public int empCode { get; set; }
        public string empId { get; set; }
        public string fullName { get; set; }
        public string url { get; set; }
        public DateTime dob { get; set; }
        public DateTime doj { get; set; }
        public DateTime sepDate { get; set; }
        public decimal age { get; set; }
        public decimal years { get; set; }
        public string department { get; set; }
        public string location { get; set; }
        public string division { get; set; }
        public string catagory { get; set; }
        public string grade { get; set; }
        public string region { get; set; }
        public string designation { get; set; }
        public string gender { get; set; }
        public string status { get; set; }
        public string yos { get; set; }


        public int total { get; set; }
        public string FileName { get; set; }
    }
    public class TerminationsDepartmentCount
    {
        public int code { get; set; }

        public string depName { get; set; }

        public string shrtName { get; set; }

        public int depCount { get; set; }
    }

    public class TerminationsLocationCount
    {
        public int code { get; set; }
        public string depName { get; set; }

        public string shrtName { get; set; }

        public int depCount { get; set; }
    }
    public class TerminationsDivisionCount
    {
        public int code { get; set; }
        public string depName { get; set; }

        public string shrtName { get; set; }

        public int depCount { get; set; }
    }
    public class TerminationsGradeCount
    {
        public int code { get; set; }
        public string depName { get; set; }

        public string shrtName { get; set; }

        public int depCount { get; set; }
    }
    public class TerminationsCategoryCount
    {
        public int code { get; set; }
        public string depName { get; set; }

        public string shrtName { get; set; }

        public int depCount { get; set; }
    }
    public class TerminationsRegionCount
    {
        public int code { get; set; }
        public string depName { get; set; }

        public string shrtName { get; set; }

        public int depCount { get; set; }
    }


    public class GenderJoinersDepartmentCountList
    {
        public int code { get; set; }

        public string depName { get; set; }

        public string shrtName { get; set; }

        public int depCount { get; set; }
    }

    public class GenderJoinersLocationCountList
    {
        public int code { get; set; }
        public string locName { get; set; }

        public string shrtName { get; set; }

        public int locCount { get; set; }
    }

    public class GenderJoinersDivisionCountList
        {
        public int code { get; set; }
        public string locName { get; set; }

        public string shrtName { get; set; }

        public int locCount { get; set; }
    }
    public class GenderJoinersGradeCountList
    {
        public int code { get; set; }
        public string locName { get; set; }

        public string shrtName { get; set; }

        public int locCount { get; set; }
    }
    public class GenderJoinersCategoryCountList
    {
        public int code { get; set; }
        public string locName { get; set; }

        public string shrtName { get; set; }

        public int locCount { get; set; }
    }
    public class GenderJoinersRegionCountList
    {
        public int code { get; set; }
        public string locName { get; set; }

        public string shrtName { get; set; }

        public int locCount { get; set; }
    }

    public class GenderJoinersCountList
    {
        public string name { get; set; }

        public int genJCount { get; set; }
    }
    public class AgeBandJoinersCountList
    {
        public string years { get; set; }

        public int total { get; set; }
    }
    public class MonthWiseLeaveList
    {
        public string month { get; set; }

        public decimal leave { get; set; }
    }
    //public class AgeBandLeaveCountList
    //{
    //    public string years { get; set; }

    //    public int total { get; set; }
    //}
    //public class DepartmentCountList
    //{
    //    public string department { get; set; }

    //    public string shrtName { get; set; }

    //    public decimal maleCount { get; set; }

    //    public decimal femaleCount { get; set; }

    //    public decimal total { get; set; }
    //}
    //public class LocationCountList
    //{
    //    public string location { get; set; }

    //    public string shrtName { get; set; }

    //    public decimal maleCount { get; set; }

    //    public decimal femaleCount { get; set; }

    //    public decimal total { get; set; }
    //}
    //public class DivisionCountList
    //{
    //    public string division { get; set; }

    //    public string shrtName { get; set; }

    //    public decimal maleCount { get; set; }

    //    public decimal femaleCount { get; set; }

    //    public decimal total { get; set; }
    //}

    //public class CategoryCountList
    //{
    //    public string category { get; set; }

    //    public string shrtName { get; set; }

    //    public decimal maleCount { get; set; }

    //    public decimal femaleCount { get; set; }

    //    public decimal total { get; set; }
    //}
    //public class GradeCountList
    //{
    //    public string grade { get; set; }

    //    public string shrtName { get; set; }

    //    public decimal maleCount { get; set; }

    //    public decimal femaleCount { get; set; }

    //    public decimal total { get; set; }
    //}
    //public class RegionCountList
    //{
    //    public string region { get; set; }

    //    public string shrtName { get; set; }

    //    public decimal maleCount { get; set; }

    //    public decimal femaleCount { get; set; }

    //    public decimal total { get; set; }
    //}
    //public class CatagoryLeaveCountList
    //{
    //    public string catagory { get; set; }

    //    public decimal total { get; set; }
    //}

    public class Employee_MIS_List
    {
        public int employeeCode { get; set; }
        public string employeeID { get; set; }
        public string fullName { get; set; }
        public string absoluteUrl { get; set; }
        public string FileName { get; set; }
    }
    public class MonthWiseLeaveTrend
    {
        public string monthName { get; set; }

        public string type { get; set; }

        public Decimal total { get; set; }

        public DateTime firstDay { get; set; }

    }
    public class MonthWiseSalaryTrend
    {
        public string monthName { get; set; }

        public Decimal total { get; set; }

    }
    public class SalaryIncrementRange
    {
        public string department { get; set; }

        public int maxPay { get; set; }

        public int minPay { get; set; }

    }
    public class EmployeeMilestones
    {
        public string name { get; set; }
        public DateTime date { get; set; }
        public string department { get; set; }
        public string designation { get; set; }
        public string location { get; set; }
        public string grade { get; set; }
        public string catagory { get; set; }
        public string type { get; set; }
        public int balance { get; set; }
        public int gp { get; set; }

    }
    public class EmployeeRewards
    {
        public string name { get; set; }
        public string recommendedName { get; set; }
        public string recommendedFor { get; set; }
        public string rewardDate { get; set; }
        public int points { get; set; }
        public string reward { get; set; }
    }

    public class EmployeeSkills
    {
        public string skillType { get; set; }
        public string sktypeShrt { get; set; }
        public string skill { get; set; }
        public string skillShrt { get; set; }
        public string proficiency { get; set; }
        public string profShrt { get; set; }
        public string rating { get; set; }

    }
    public class EmployeeMap
    {
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string location { get; set; }
        public DateTime punchingTime { get; set; }
        public string punchingStatus { get; set; }

        public List<double> latLng { get; set; }

    }

    public class SalaryDepartmentWise
    {
        public int code { get; set; }
        public string department { get; set; }

        public string shrtName { get; set; }

        public decimal total { get; set; }
    }
    public class SalaryLocationWise
    {
        public int code { get; set; }
        public string location { get; set; }

        public string shrtName { get; set; }

        public decimal total { get; set; }
    }
    public class SalaryDivisionWise
    {
        public int code { get; set; }
        public string division { get; set; }

        public string shrtName { get; set; }

        public decimal total { get; set; }
    }
    public class SalaryGradeWise
    {
        public int code { get; set; }
        public string grade { get; set; }

        public string shrtName { get; set; }

        public decimal total { get; set; }
    }
    public class SalaryCategoryWise
    {
        public int code { get; set; }
        public string category { get; set; }

        public string shrtName { get; set; }

        public decimal total { get; set; }
    }
    public class SalaryRegionWise
    {
        public int code { get; set; }
        public string region { get; set; }

        public string shrtName { get; set; }

        public decimal total { get; set; }
    }
    public class AbsMonthWise
    {
        public string monthName { get; set; }

        public Decimal total { get; set; }

    }
    public class AgeBandAbsList
    {
        public string years { get; set; }

        public decimal total { get; set; }
    }

    public class TenureAbsList
    {
        public string years { get; set; }

        public decimal total { get; set; }

    }
    public class AbsDepartmentList
    {
        public string department { get; set; }
        public string shrtName { get; set; }

        public decimal total { get; set; }

    }

    public class AbsRegionList
    {
        public string region { get; set; }
        public string shrtName { get; set; }

        public decimal total { get; set; }

    }
    public class AbsGradeList
    {
        public string grade { get; set; }
        public string shrtName { get; set; }

        public decimal total { get; set; }

    }
    public class AbsLocationList
    {
        public string location { get; set; }
        public string shrtName { get; set; }

        public decimal total { get; set; }

    }
    public class AbsDivisionList
    {
        public string division { get; set; }
        public string shrtName { get; set; }

        public decimal total { get; set; }

    }
    public class AbsCategoryList
    {
        public string category { get; set; }
        public string shrtName { get; set; }

        public decimal total { get; set; }

    }


    public class GetAbsDaysList
    {
        public string weekDay { get; set; }

        public decimal total { get; set; }

    }
    public class GetHistoricalJoiningCountList
    {
        public string year { get; set; }
        public string gender { get; set; }
        public DateTime date { get; set; }
        public int empCount { get; set; }

    }
    public class GetHistoricalTerminationCountList
    {
        public string year { get; set; }
        public string gender { get; set; }
        public DateTime date { get; set; }
        public int empCount { get; set; }
    }
    public class GetHistoricalWFHList
    {
        public string year { get; set; }
        public string month { get; set; }
        public int empCount { get; set; }
    }
    public class GetHistoricalSalaryList
    {
        public string year { get; set; }
        public string month { get; set; }
        public int total { get; set; }
    }
    public class GetHistoricalSalaryIncList
    {
        public string year { get; set; }
        public string month { get; set; }
        public int total { get; set; }
    }
    public class GetHistoricalLOPList
    {
        public string year { get; set; }
        public string month { get; set; }
        public int empCount { get; set; }
    }
    public class GetHistoricalJoiningCountMonthList
    {
        public string year { get; set; }
        public string month { get; set; }
       // public DateTime firstDay { get; set; }
        public int empCount { get; set; }

    }
    public class GetHistoricalTerminationsCountMonthList
    {
        public string year { get; set; }
        public string month { get; set; }
        public int empCount { get; set; }
    }
    public class GetHistoricalAttritionMonthList
    {
        public string year { get; set; }
        public string month { get; set; }
        public decimal empCount { get; set; }

    }
    public class AllowanceMonthComparisonList
    {
        public string allowance { get; set; }

        public string shrtName { get; set; }

        public decimal currMonth { get; set; }

        public decimal prvMonth { get; set; }
    }
    public class DeductionMonthComparisonList
    {
        public string allowance { get; set; }

        public string shrtName { get; set; }

        public decimal currMonth { get; set; }

        public decimal prvMonth { get; set; }
    }
    public class VacancyTrend
    {
        public string month { get; set; }

        public int total { get; set; }

    }

    public class SalaryRange
    {
        public string range { get; set; }

        public int total { get; set; }

    }
    public class TopVacancyDesignation
    {
        public string name { get; set; }

        public int total { get; set; }

    }
    public class BadHires
    {
        public string type { get; set; }

        public int total { get; set; }

        public string color { get; set; }

    }
    public class GetHistoricalBadHires
    {
        public string year { get; set; }
        public string gender { get; set; }
        public DateTime date { get; set; }
        public int empCount { get; set; }

    }
    public class HRGender
    {
        public string gender { get; set; }

        public int total { get; set; }

    }
    public class HRDepartment
    {

        public int code { get; set; }

        public string department { get; set; }

        public string shrtName { get; set; }

        public int total { get; set; }

        public int salary { get; set; }

    }
    public class HRAge
    {
        public string age { get; set; }

        public int male { get; set; }

        public int feMale { get; set; }

        public int maleSalary { get; set; }

        public int feMaleSalary { get; set; }

        public int total { get; set; }

    }
    public class HRTenure
    {
        public string age { get; set; }

        public int male { get; set; }

        public int feMale { get; set; }

        public int maleSalary { get; set; }

        public int feMaleSalary { get; set; }

        public int total { get; set; }

    }

    public class HRTimetoQuitJob
    {
        public int year { get; set; }

        public decimal percentage { get; set; }

        public int count { get; set; }

        public int total { get; set; }

    }

    public class HRLocation
    {
        public int code { get; set; }

        public string location { get; set; }

        public string shrtName { get; set; }

        public int salary { get; set; }

        public int total { get; set; }

    }
    public class HRTimeOffice
    {
        public string monthName { get; set; }

        public decimal lateIn { get; set; }

        public decimal earlyOut { get; set; }

        public decimal overTime { get; set; }

    }
    public class HRTODepartment
    {

        public int code { get; set; }
        public string department { get; set; }

        public string shrtName { get; set; }

        public decimal lateIn { get; set; }

        public decimal earlyOut { get; set; }

        public decimal overTime { get; set; }

    }
    public class HRTOLocation
    {
        public string location { get; set; }

        public string shrtName { get; set; }

        public decimal lateIn { get; set; }

        public decimal earlyOut { get; set; }

        public decimal overTime { get; set; }

    }
    public class HRTODivision
    {
        public string division { get; set; }

        public string shrtName { get; set; }

        public decimal lateIn { get; set; }

        public decimal earlyOut { get; set; }

        public decimal overTime { get; set; }

    }
    public class HRTOGrade
    {
        public string grade { get; set; }

        public string shrtName { get; set; }

        public decimal lateIn { get; set; }

        public decimal earlyOut { get; set; }

        public decimal overTime { get; set; }

    }

    public class HRTOCategory
    {
        public string category { get; set; }

        public string shrtName { get; set; }

        public decimal lateIn { get; set; }

        public decimal earlyOut { get; set; }

        public decimal overTime { get; set; }

    }

    public class HRTORegion
    {
        public string region { get; set; }

        public string shrtName { get; set; }

        public decimal lateIn { get; set; }

        public decimal earlyOut { get; set; }

        public decimal overTime { get; set; }

    }

    public class HRRegion
    {
        public int code { get; set; }
        public string region { get; set; }

        public string shrtName { get; set; }

        public int salary { get; set; }

        public int total { get; set; }

    }
    public class HRDivision
    {
        public int code { get; set; }

        public string division { get; set; }

        public string shrtName { get; set; }

        public int salary { get; set; }

        public int total { get; set; }

    }
    public class HRGrade
    {
        public int code { get; set; }
        public string grade { get; set; }

        public string shrtName { get; set; }

        public int salary { get; set; }

        public int total { get; set; }

    }
    public class HRCategory
    {
        public int code { get; set; }

        public string category { get; set; }

        public string shrtName { get; set; }

        public int salary { get; set; }

        public int total { get; set; }

    }
    public class SalaryAllowance
    {
        public string allowance { get; set; }

        public string shrtName { get; set; }

        public int total { get; set; }

        public string totalInWords { get; set; }

    }
    public class SalaryDeduction
    {
        public string deduction { get; set; }

        public string shrtName { get; set; }

        public int total { get; set; }

        public string totalInWords { get; set; }

    }
    public class AttendanceDayWise
    {
        public string day { get; set; }

        public string leaveDay { get; set; }

        public string remark { get; set; }

        public decimal lateIn { get; set; }

        public decimal earlyOut { get; set; }

        public decimal hours { get; set; }


    }
    public class AttendanceDateWise
    {
        public string punchingDate { get; set; }

        public string category { get; set; }

        public decimal total { get; set; }

        public string color { get; set; }

    }

    public class AllowanceOverView
    {
        public string allowance { get; set; }

        public string shrtName { get; set; }

        public int total { get; set; }

        public string totalInWords { get; set; }

    }

    public class DeductionOverView
    {
        public string deduction { get; set; }

        public string shrtName { get; set; }

        public int total { get; set; }

        public string totalInWords { get; set; }

    }
    public class CTCOverView
    {
        public string type { get; set; }

        public string color { get; set; }

        public int total { get; set; }

    }

    public class PGOverView
    {
        public string type { get; set; }
        public int total { get; set; }

    }
    public class GetLeaveDaysList
    {
        public string weekDay { get; set; }

        public decimal total { get; set; }

    }
    public class EmployeeLateInEarlyOut
    {
        public string type { get; set; }

        public decimal hours { get; set; }

    }

    public class SalaryList
    {
        public int empCode { get; set; }
        public string empId { get; set; }
        public string fullName { get; set; }
        public string url { get; set; }
        public DateTime dob { get; set; }
        public DateTime doj { get; set; }
        public DateTime sepDate { get; set; }
        public decimal age { get; set; }
        public decimal years { get; set; }
        public string department { get; set; }
        public string location { get; set; }
        public string division { get; set; }
        public string catagory { get; set; }
        public string grade { get; set; }
        public string region { get; set; }
        public string designation { get; set; }
        public string gender { get; set; }
        public string status { get; set; }
        public string yos { get; set; }
        public int spCode { get; set; }
        public string month { get; set; }
        public string alwName { get; set; }
        public int allowance { get; set; }
        public int pf { get; set; }
        public int esi { get; set; }
        public int total { get; set; }
        public string FileName { get; set; }
    }
    public class ABSEENTISM
    {
        public int employeecode { get; set; }
        public string fullname { get; set; }
        public DateTime doj { get; set; }
        public string employeeid { get; set; }
        public DateTime dob { get; set; }
        public string grade { get; set; }
        public string gender { get; set; }
        public string status { get; set; }
        public string category { get; set; }
        public string designation { get; set; }
        public string department { get; set; }
        public string location { get; set; }
        public string division { get; set; }
        public string absoluteuri { get; set; }

        public DateTime date { get; set; }
        public string name { get; set; }
        public int age { get; set; }
        public decimal years { get; set; }
        public int slabid { get; set; }
        public string weekdayname { get; set; }
        public string FileName { get; set; }

    }
    public class RECRUITMENT
    {
        public string jobcode { get; set; }
        public string jobtitle { get; set; }
        public DateTime needbydate { get; set; }
        public string experience { get; set; }
        public string department { get; set; }
        public string designation { get; set; }
        public int noofrequirements { get; set; }
        public int vacancy { get; set; }
        public string exgrosspay { get; set; }
        public string remarks { get; set; }
        public string agegroup { get; set; }
        public DateTime approvedon { get; set; }

        public string fullname { get; set; }
        public string employeeid { get; set; }

        public string requestedbydepartment { get; set; }
        public string name { get; set; }


        public string ApplicantTrackingCode { get; set; }
        public string ApplicantTrackingGI { get; set; }
        public string CompanyCode { get; set; }
        public string ResumeDataBankCode { get; set; }
        public int NoOfInterviewLevels { get; set; }
        public string SelectionCode { get; set; }
        public string InterviewStatus { get; set; }
        public DateTime CompletionDate { get; set; }
        public decimal ExpectedSalary { get; set; }
        public decimal OfferedSalary { get; set; }

        public string User { get; set; }
        public DateTime CreatedDate { get; set; }
        public string FinalApprovalRemarks { get; set; }
        public string DocumentVerificationStatus { get; set; }
        public DateTime? VerifiedOn { get; set; }
        public string VerifiedBy { get; set; }
        public string RejectReasonCode { get; set; }
        public string PreEnrollCode { get; set; }
        public string OfferProcessingLevels { get; set; }

        public DateTime dob { get; set; }
        public string Gender { get; set; }
        public string PersonalMobile { get; set; }
        public DateTime doj { get; set; }
        public string location { get; set; }
        public DateTime renewaldate { get; set; }
        public string Adderess { get; set; }
        public int noticeperiod { get; set; }

        public int pin { get; set; }
        public string district { get; set; }
        public string offeracceptance { get; set; }
        public string OfferLetterAbsoluteUri { get; set; }
        public string JoiningConfirmationStatus { get; set; }

    }
    public class AgeBandLeaveCountList
    {
        public string years { get; set; }
        public string code { get; set; }

        public int total { get; set; }
    }
    public class DepartmentCountList
    {
        public string department { get; set; }

        public string shrtName { get; set; }
        public int code { get; set; }

        public decimal maleCount { get; set; }

        public decimal femaleCount { get; set; }

        public decimal total { get; set; }
    }
    public class LocationCountList
    {
        public string location { get; set; }

        public string shrtName { get; set; }
        public int code { get; set; }

        public decimal maleCount { get; set; }

        public decimal femaleCount { get; set; }

        public decimal total { get; set; }
    }
    public class DivisionCountList
    {
        public string division { get; set; }

        public string shrtName { get; set; }
        public int code { get; set; }
        public decimal maleCount { get; set; }

        public decimal femaleCount { get; set; }

        public decimal total { get; set; }
    }

    public class CategoryCountList
    {
        public string category { get; set; }

        public string shrtName { get; set; }
        public int code { get; set; }
        public decimal maleCount { get; set; }

        public decimal femaleCount { get; set; }

        public decimal total { get; set; }
    }
    public class GradeCountList
    {
        public string grade { get; set; }

        public string shrtName { get; set; }
        public int code { get; set; }
        public decimal maleCount { get; set; }

        public decimal femaleCount { get; set; }

        public decimal total { get; set; }
    }
    public class RegionCountList
    {
        public string region { get; set; }

        public string shrtName { get; set; }
        public int code { get; set; }
        public decimal maleCount { get; set; }

        public decimal femaleCount { get; set; }

        public decimal total { get; set; }
    }
    public class CatagoryLeaveCountList
    {
        public string catagory { get; set; }
        public int code { get; set; }
        public decimal total { get; set; }
    }

    public class LeaveEmployeeList
    {
        public int employeecode { get; set; }
        public string employeeid { get; set; }
        public string fullname { get; set; }
        public DateTime dob { get; set; }
        public decimal age { get; set; }
        public string designation { get; set; }
        public int departmentcode { get; set; }
        public string department { get; set; }
        public int locationcode { get; set; }
        public string location { get; set; }
        public int categorycode { get; set; }
        public string category { get; set; }
        public int divisioncode { get; set; }
        public string division { get; set; }
        public int gradecode { get; set; }
        public string grade { get; set; }
        public int regioncode { get; set; }
        public string region { get; set; }
        public string gender { get; set; }
        public DateTime leavedate { get; set; }
        public int leavecode { get; set; }
        public string leavetype { get; set; }
        public string daystatus { get; set; }
    }
    public class TransitionTypeDepartment
    {
        public int count { get; set; }
        public int code { get; set; }
        public string department { get; set; }
        public string fullname { get; set; }
    }
    public class TransitionTypeLocation
    {
        public int count { get; set; }
        public int code { get; set; }
        public string location { get; set; }
        public string fullname { get; set; }
    }
    public class TransitionTypeDivision
    {
        public int count { get; set; }
        public int code { get; set; }
        public string division { get; set; }
    }
    public class TransitionTypeCategory
    {
        public int count { get; set; }
        public int code { get; set; }
        public string category { get; set; }
    }
    public class TransitionTypeGrade
    {
        public int count { get; set; }
        public int code { get; set; }
        public string grade { get; set; }
    }
    public class TransitionTypeRegion
    {
        public int count { get; set; }
        public int code { get; set; }
        public string region { get; set; }
    }

   


    public class BadHiresMonth
    {
        public int count { get; set; }
        public string month { get; set; }
    }
    public class BadHiresGender
    {
        public int count { get; set; }
        public int code { get; set; }
        public string gender { get; set; }
    }
    public class BadHiresAge
    {
        public int count { get; set; }
        public string years { get; set; }
    }
    public class BadHiresDepartment
    {
        public int count { get; set; }
        public int code { get; set; }
        public string department { get; set; }
        public string fullname { get; set; }
    }
    public class BadHiresLocation
    {
        public int count { get; set; }
        public int code { get; set; }
        public string location { get; set; }
        public string fullname { get; set; }
    }
    public class BadHiresDivision
    {
        public int count { get; set; }
        public int code { get; set; }
        public string division { get; set; }
    }
    public class BadHiresCategory
    {
        public int count { get; set; }
        public int code { get; set; }
        public string category { get; set; }
    }
    public class BadHiresGrade
    {
        public int count { get; set; }
        public int code { get; set; }
        public string grade { get; set; }
    }
    public class BadHiresRegion
    {
        public int count { get; set; }
        public int code { get; set; }
        public string region { get; set; }
    }
    public class BadHiresEmp
    {
        public int employeecode { get; set; }
        public string employeeid { get; set; }
        public string fullname { get; set; }
        public string designation { get; set; }
        public int departmentcode { get; set; }
        public string department { get; set; }
        public int locationcode { get; set; }
        public string location { get; set; }
        public int categorycode { get; set; }
        public string category { get; set; }
        public int divisioncode { get; set; }
        public string division { get; set; }
        public int gradecode { get; set; }
        public string grade { get; set; }
        public int regioncode { get; set; }
        public string region { get; set; }
        public DateTime doj { get; set; }
        public decimal age { get; set; }
        public int gendercode { get; set; }
        public string gender { get; set; }
        public string absoluteuri { get; set; }
    }
    public class YearlyTrainingCost
    {
        public int code { get; set; }
        public int trainingexpense { get; set; }
        public int allotedamount { get; set; }
        public string name { get; set; }
    }
    public class PerHeadTrainingCost
    {
        public int code { get; set; }
        public decimal perheadexpense { get; set; }
        public string fullname { get; set; }
        public string name { get; set; }
    }
    public class TrainingEmployeeCount
    {
        public int code { get; set; }
        public int traineecount { get; set; }
        public string fullname { get; set; }
        public string name { get; set; }
    }
    public class MonthlyTrainings
    {
        public int code { get; set; }
        public int trainingcount { get; set; }
        public string month { get; set; }
    }
    public class TrainingEmp
    {
        public int employeecode { get; set; }
        public string employeeid { get; set; }
        public string fullname { get; set; }
        public string designation { get; set; }
        public string department { get; set; }
        public string location { get; set; }
        public string category { get; set; }
        public string division { get; set; }
        public string grade { get; set; }
        public string region { get; set; }
        public DateTime dot { get; set; }
        public int subjectcode { get; set; }
        public string status { get; set; }
    }

    public class TrainingCostList
    {
        public int code { get; set; }
        public int trainingexpense { get; set; }
        public string subject { get; set; }
        public DateTime dot { get; set; }
        public int traineecount { get; set; }

    }
}
