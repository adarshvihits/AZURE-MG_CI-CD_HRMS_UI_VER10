using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank
{
    public class DealerUndertaking
    {
        public Guid EmployeeGI { get; set; }
        public int EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeID { get; set; }
        public string RequestID { get; set; }
        public Guid RequestGI { get; set; }
        public int RequestCode { get; set; }
        public string Location { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string ApprovalStatus { get; set; }
        public int GeneralCount { get; set; }
        public int ClosedCount { get; set; }
        public int OpenCount { get; set; }
        public DateTime EffectiveDate { get; set; }
        public EmployeeHeader EmployeeHeader { get; set; }
        public bool DullyDealerUndertaking { get; set; }
        public bool DealingRights { get; set; }
        public DateTime? ClosingDate { get; set; }
        public string ClosingRemark { get; set; }
        public string ActiveStatus { get; set; }
        public List<DealerUndertaking> DealerUndertakingList { get; set; }
        public int RowCount { get; set; }
        public List<DealerUndertakingHistory> DealerUndertakingHistory { get; set; }
        public string Remark { get; set; }
    }
    public class DealerUndertakingHistory
    {
        public int RequestCode { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Remarks { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public string Status { get; set; }
        public DateTime? ClosingDate { get; set; }

    }
}
