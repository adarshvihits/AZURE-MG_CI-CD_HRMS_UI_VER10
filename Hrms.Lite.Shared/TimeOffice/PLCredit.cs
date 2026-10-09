using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Hrms.Lite.Shared.PayRoll.Generic.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.TimeOffice
{
    public class PLCredit
    {
        public UserBase User { get; set; }
        public int EmployeeCode { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public Guid EmployeeGI { get; set; }
        public string Designation { get; set; }
        public string Category { get; set; }
        public string Location { get; set; }
        public DateTime DOJ { get; set; }
        public DateTime PostingDate { get; set; }
        public decimal LOP_AB { get; set; }
        public decimal Opening { get; set; }
        public List<PLCredit> PLCreditList { get; set; }
        public Filter Filter { get; set; }
        public int LeaveCode { get; set; }
        public int LeavePeriodCode { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }

    }
}
