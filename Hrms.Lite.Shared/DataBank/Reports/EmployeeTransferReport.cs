using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.Reports
{
    public class EmployeeTransferReport
    {
        public Filter Filter { get; set; }
        public List<EmployeeTransfer_EmployeePrimaryInfo> EmployeePrimaryInfo { get; set; }
        public DateTime? TransferFromDate { get; set; }
        public DateTime? TransferToDate { get; set; }
        public int EmployeeTransferType { get; set; }
    }

    public class EmployeeTransfer_EmployeePrimaryInfo
    {
        public int EmployeeCode { get; set; }
        public Guid EmployeeGI { get; set; }
        public string EmployeeID { get; set; }
        public string FullName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public string Category { get; set; }
        public string Region { get; set; }
        public string State { get; set; }
        public string DOJ { get; set; }
        public string transferDate { get; set; }
        public string Age { get; set; }
        public string Transfer_Joining { get; set; }
        public int active { get; set; }
        public int totalRecords { get; set; }
    }
}
