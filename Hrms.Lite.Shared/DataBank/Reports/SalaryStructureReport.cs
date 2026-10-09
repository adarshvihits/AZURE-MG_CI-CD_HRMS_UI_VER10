using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.Reports
{
    public class SalaryStructureReport
    {

        
            public UserBase User { get; set; }
            public int TotalRows { get; set; }
            public int ActiveCount { get; set; }
            public int ResignedCount { get; set; }
            public List<dynamic> Data { get; set; }
            public MainList MainList { get; set; }
        public Filter SliderFilter { get; set; }
        public int TabIndex { get; set; }
        public List<SalaryStructureReportDetails> SalaryStructureReportDetails { get; set; }
        }
        public class MainList
        {
            public List<Header> Header { get; set; }
            public List<SalaryStructure_EmployeePrimaryInfo> EmployeePrimaryInfo { get; set; }
            public List<SalaryStructureDetails> SalaryStructureDetails { get; set; }


        }
        public class Header
        {
            public string HeaderName { get; set; }
            public string FieldName { get; set; }
        }
        public class SalaryStructure_EmployeePrimaryInfo
        {
            public int EmployeeCode { get; set; }
            public Guid EmployeeGI { get; set; }
            public string EmployeeID { get; set; }
            public string FullName { get; set; }
            public string Designation { get; set; }
            public string Department { get; set; }
            public string Location { get; set; }
            public string Category { get; set; }
            public string SalaryStructure { get; set; }
            public string YOS { get; set; }
            public string DOJ { get; set; }
            public string LID { get; set; }
            public string LWD { get; set; }
        }
        public class SalaryStructureDetails
        {
            public string Amount { get; set; }

        }
        public class SalaryStructureReportDetails
        {
            public int EmployeeCode { get; set; }
            public Guid EmployeeGI { get; set; }
            public string EmployeeID { get; set; }
            public string FullName { get; set; }
            public string Designation { get; set; }
            public string Department { get; set; }
            public string Location { get; set; }
            public string Category { get; set; }
            public string SalaryStructure { get; set; }
            public string YOS { get; set; }
            public string DOJ { get; set; }
            public string LID { get; set; }
            public string LWD { get; set; }
            public string BasicSalary { get; set; }
            public string OtherAllowance { get; set; }
            public string GrossSalary { get; set; }
            public int totalRecords { get; set; }
            public int active { get; set; }
        public int resigned { get; set; }

    }
    }
