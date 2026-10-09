using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
namespace Hrms.Lite.Shared.DataBank.Reports
{
    public class ReassignmentMovementReport
    {
        public DateTime?  DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public int?[] TrType { get; set; }
        public Dropdown TransitionType { get; set; }
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
}
