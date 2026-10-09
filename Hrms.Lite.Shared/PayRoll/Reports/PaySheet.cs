using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.PayRoll.Reports
{
    public class PaySheet
    {
        public UserBase User { get; set; }
        public MasterBase PayrollMonth { get; set; }
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
        public List<dynamic> Data { get; set; }
        public MasterBase PaySheetType { get; set; }
        public PaySheet_MainList MainList { get; set; }
        public int BasedOn { get; set; }
        public PaymentMode PaymentMode { get; set; }
        public int?[] PaymentModes { get; set; }
        public bool HoldEmployees { get; set; }
        public Region Region { get; set; }
        public int?[] Regions { get; set; }
        public State State { get; set; }
        public int?[] States { get; set; }
        public MasterBase PayrollMonthFROM { get; set; }
        public MasterBase PayrollMonthTO { get; set; }
    }
    public class PaySheet_MainList
    {
        public List<PaySheet_Header> Header { get; set; }
        public List<PaySheetInfo> Info { get; set; }
        public List<PaySheet_Details> Details { get; set; }


    }
    public class PaySheet_Header
    {
        public string HeaderName { get; set; }
        public string FieldName { get; set; }
    }
    public class PaySheetInfo
    {
        public int SLNo { get; set; }
        public int EmployeeCode { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }

    }
    public class PaySheet_Details
    {
        public string Description { get; set; }

    }
}
