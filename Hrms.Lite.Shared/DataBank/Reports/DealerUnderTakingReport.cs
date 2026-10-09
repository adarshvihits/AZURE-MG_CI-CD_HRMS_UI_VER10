using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.Reports
{
   
    public class DealerUnderTakingReport
    {
        public int ActiveCount { get; set; }
        public int ResignedCount { get; set; }
        public string Mode { get; set; }
        public int code { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public Filter filter { get; set; }
        public int TabIndex { get; set; }
        public List<DealerUnderTakingList> DealerUnderTakingList { get; set; }
        public string PranNumber { get; set; }
        public DateTime? NpsDate { get; set; }
        public string Remarks { get; set; }
        public string EmployeeId { get; set; }
        public EmployeeHeader EmployeeHeader { get; set; }
    }
    public class DealerUnderTakingList
    {
        public string DepartmentName { get; set; }
        public string DesignationName { get; set; }
        public string LocationName { get; set; }
        public string Name { get; set; }
        public string EmployeeId { get; set; }
        public Guid EmployeeGI { get; set; }
        public int code { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public string ApprovedBy { get; set; }
        public string CreatedBy { get; set; }
        public string Status { get; set; }
        public DateTime CloseDate { get; set; }
        public DateTime EffectiveDate { get; set; }

        public DateTime EntryDate { get; set; }
        public string Remarks { get; set; }

    }
}
