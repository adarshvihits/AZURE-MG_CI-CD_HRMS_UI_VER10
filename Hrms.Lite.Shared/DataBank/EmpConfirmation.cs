using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank
{
    public class EmpConfirmation
    {
        public int EmployeeCode { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public Guid EmployeeGI { get; set; }
        public string Designation { get; set; }
        public string FuncDesignation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public DateTime DOJ { get; set; }
        public string Approver { get; set; }
        public DateTime ApproveDate { get; set; }
        public List<EmpConfirmation> EmpConfirmationList { get; set; }
    }
}
