using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Hrms.Lite.Shared.TimeOffice.Generic.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.TimeOffice
{
    public class LeaveCarryForward
    {   
        
        public EmployeeHeader EmployeeHeader { get; set; }
        public int EmployeeCode { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public Guid EmployeeGI { get; set; }
        public Designation Designation { get; set; }
        public Department Department { get; set; }
        public Location Location { get; set; }
        public Category Category { get; set; }
        public DateTime DOJ { get; set; }
        public decimal Balance { get; set; }
        public decimal CFMaxLimit { get; set; }
        public decimal CF { get; set; }
        public LeavePeriod LeavePeriodFrom { get; set; }
        public LeavePeriod LeavePeriodTo { get; set; }
        public LeaveGroup LeaveGroup { get; set; }
        public LeaveCF Leave { get; set; }
        public Month Month { get; set; }
        public List<LeaveCarryForward> LeaveCarryForwardList { get; set; }
        public LeaveCFDetails LeaveCFDetails { get; set; }
        public List<LeaveCFLogDetails> LeaveCFLogDetails { get; set; }
        public Filter Filter { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public string UserName { get; set; }
        public string EntryFrom { get; set; }
        public DateTime EntryDate { get; set; }

        public int EntryType { get; set; }

    }
    public class LeaveCF : MasterBase
    {
    }
    public class LeaveCFDetails
    {
        public Guid EmployeeGI { get; set; }
        public LeavePeriod LeavePeriodFrom { get; set; }
        public LeavePeriod LeavePeriodTo { get; set; }
        public LeaveGroup LeaveGroup { get; set; }
        public Leave Leave { get; set; }
        public Month Month { get; set; }

        public decimal Balance { get; set; }
        public decimal Opening { get; set; }
        public decimal Credit { get; set; }
        public decimal Taken { get; set; }
        public decimal Debit { get; set; }
        public decimal EnCashment { get; set; }
        public decimal MaxLimit { get; set; }
        public decimal? CF { get; set; }
        public string Remarks { get; set; }
    }
    public class LeaveCFLogDetails
    {
        public int SlNo { get; set; }
        public DateTime LogDATE { get; set; }
        public string TYPE { get; set; }
        public decimal Opening { get; set; }
        public string Remarks { get; set; }

    }

}