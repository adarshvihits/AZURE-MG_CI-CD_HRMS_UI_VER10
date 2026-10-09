using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank
{
    public class AdditionalResponsibility
    {
        public Guid EmployeeGI { get; set; }
        public int EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeID { get; set; }
        public string RequestID { get; set; }
        public Guid RequestGI { get; set; }
        public string Location { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string ApprovalStatus { get; set; }
        public int GeneralCount { get; set; }
        public int ClosedCount { get; set; }
        public int OpenCount { get; set; }
        public EmployeeHeader EmployeeHeader { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public Dropdown ResAdditionalResponsibility { get; set; }
        public Dropdown Branch { get; set; }
        public Dropdown Status { get; set; }
        public DateTime? ClosingDate { get; set; }
        public string Remarks { get; set; }
        public List<AdditionalResponsibility> AdditionalResponsibilityList { get; set; }
        public int RowCount { get; set; }
        public List<AdditionalResponsibilityHistory> AdditionalResponsibilityHistory { get; set; }
        public string Remark { get; set; }
        public File Attachment { get; set; }
    }
    public class AdditionalResponsibilityHistory
    {
        public int RequestCode { get; set; }
        public string Description { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string Branch { get; set; }
        public string Status { get; set; }
        public DateTime? ClosingDate { get; set; }

    }
}
