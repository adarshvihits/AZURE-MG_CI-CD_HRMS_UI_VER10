using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank
{
    public class EmployeeTransfer
    {
        public int EmployeeCode { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public Guid EmployeeGI { get; set; }
        public string Reason { get; set; }
        public string RequestId { get; set; }
        public DateTime RequestDate { get; set; }
        public string FuncDesignation { get; set; }
        public string Location { get; set; }
        public string Approver { get; set; }
        public DateTime ApproveDate { get; set; }
        public List<EmployeeTransfer> EmployeeTransferList { get; set; }
    }
}
