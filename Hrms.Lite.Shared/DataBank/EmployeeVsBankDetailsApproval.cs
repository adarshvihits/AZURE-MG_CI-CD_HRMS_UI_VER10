using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank
{
    public class EmployeeVsBankDetailsApproval
    {
        public Filter filter { get; set; }
        public int EmployeeCode { get; set; }
        public Guid EmployeeGI { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public string BankName { get; set; }
        public string BankBranch { get; set; }
        public string AccountNo { get; set; }
        public string IFSC { get; set; }
        public string PayMode { get; set; }
        public int active { get; set; }
        public int totalRecords { get; set; }
        public List<EmployeeVsBankDetailsApproval> EmployeeVsBankDetailsList { get; set; }
        public EmployeeHeader EmployeeHeader { get; set; }
        public File File { get; set; }
    }
}
