using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.Reports
{
    public class EmployeeInformationReport
    {
        public int Tab { get; set; }
        public MasterBase ReportType { get; set; }
        public MasterBase PersonalDetailsType { get; set; }
        public int?[] PersonalDetailsTypes { get; set; }
        public Designation Designation { get; set; }
        public int?[] Designations { get; set; }
        public Department Department { get; set; }
        public int?[] Departments { get; set; }
        public Location Location { get; set; }
        public int?[] Locations { get; set; }
        public Category Category { get; set; }
        public int?[] Categories { get; set; }
        public Grade Grade { get; set; }
        public int?[] Grades { get; set; }
        public PayrollGroup PayrollGroup { get; set; }
        public int?[] PayrollGroups { get; set; }
        public Division Division { get; set; }
        public int?[] Divisions { get; set; }
        public Section Section { get; set; }
        public int?[] Sections { get; set; }
        public State State { get; set; }
        public int?[] States { get; set; }
        public Region Region { get; set; }
        public int?[] Regions { get; set; }
        public FunctionalState FunctionalState { get; set; }
        public int?[] FunctionalStates { get; set; }
        public EmploymentType EmploymentType { get; set; }
        public int?[] EmploymentTypes { get; set; }
        public WorkLocation WorkLocation { get; set; }
        public int?[] WorkLocations { get; set; }
        public WageGradeMaster WageGradeMaster { get; set; }
        public int?[] WageGrades { get; set; }
        public GroupCompany GroupCompanyMaster { get; set; }
        public int?[] GroupCompany { get; set; }
        public List<dynamic> Data { get; set; }
        public List<EmployeeInformationReport_GroupBy> EmployeeGrouping { get; set; }
        public EmployeeInformationReport_MainList MainList { get; set; }
        public List<PersonalDetailsDetailedReport> PersonalDetailsDetailedReport { get; set; }
        public Filter filter { get; set; }

    }
    public class EmployeeInformationReport_GroupBy
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public bool Applicability { get; set; }
    }
    public class EmployeeInformationReport_MainList
    {
        public List<EmployeeInformationReport_Header> Header { get; set; }
        public List<EmployeeInformationReport_EmployeePrimaryInfo> EmployeePrimaryInfo { get; set; }
        public List<EmployeeInformationReport_Details> DailyAttendanceStatusReport_Details { get; set; }


    }
    public class EmployeeInformationReport_Header
    {
        public string HeaderName { get; set; }
        public string FieldName { get; set; }
    }
    public class EmployeeInformationReport_EmployeePrimaryInfo
    {
        public int EmployeeCode { get; set; }
        public string EmployeeID { get; set; }
        public string FullName { get; set; }

    }
    public class EmployeeInformationReport_Details
    {
        public string Description { get; set; }

    }
    public class PersonalDetailsDetailedReport
    {
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public string Category { get; set; }
        public string Region { get; set; }
        public string ReportType { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public string RelationName { get; set; }
        public bool Read { get; set; }
        public bool Write { get; set; }
        public bool Speak { get; set; }
        public string College_University { get; set; }
        public string Aggregate { get; set; }
        public Nullable<DateTime> DateOfBirth { get; set; }
        public string Occupation { get; set; }
        public string StatusName { get; set; }
        public File File { get; set; }
        public string Gender { get; set; }
        public string Mobile { get; set; }
        public int Age { get; set; }
        public DateTime DOJ { get; set; }
        public string PreviousDesignation { get; set; }
        public string UHID { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
