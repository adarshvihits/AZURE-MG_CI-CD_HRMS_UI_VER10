using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.DTO
{
    public class EmployeeVsAppraisalDate
    {
        public List<EmployeeVsAppraisalDateList> EmployeeVsAppraisalDateList { get; set; }
        public Filter filter { get; set; }
        public Filter Filter { get; set; }
        public File Excel { get; set; }
        public Guid EmployeeGuid { get; set; }
        public DateTime AppraisalDate { get; set; }
    }
    public class EmployeeVsAppraisalDateList
    {
        public int EmployeeCode { get; set; }
        public string EmpId { get; set; }
        public Guid EmployeeGuid { get; set; }
        public string EmployeeName { get; set; }
        public string DESIGNATION { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public string Category { get; set; }
        public string DOJ { get; set; }
        public string NextAppraisalDate { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
    }
}
