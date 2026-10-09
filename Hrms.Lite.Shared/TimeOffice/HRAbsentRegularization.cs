using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.TimeOffice
{
    public class HRAbsentRegularization
    {
        public Dropdown PGroup { get; set; }
        public Dropdown Month { get; set; }
        public Dropdown Leave { get; set; }
        public decimal LeaveBalance { get; set; }
        public List<HRAbsentRegularizationList> HRAbsentRegularizationList { get; set; }
        public List<ABStatusDetails> ABStatusDetails { get; set; }
        public EmployeeHeader EmployeeHeader { get; set; }
        public Filter Filter { get; set; }
    }
    public class HRAbsentRegularizationList
    {
        public int EmpCode { get; set; }
        public Guid EmpGI { get; set; }
        public string EmpId { get; set; }
        public string EmpName { get; set; }
        public string Department { get; set; }
        public decimal AbscentCount { get; set; }
        public decimal LeaveBalance { get; set; }
        public string Location { get; set; }
        public string Designation { get; set; }
        public bool Applicable { get; set; }
         public int active { get; set; }
        public int totalRecords { get; set; }

    }
    public class ABStatusDetails
    {
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public bool Applicable { get; set; }

    }
}

