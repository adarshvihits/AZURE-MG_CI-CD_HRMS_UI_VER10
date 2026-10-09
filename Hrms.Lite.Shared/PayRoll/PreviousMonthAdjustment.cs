using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Hrms.Lite.Shared.PayRoll.Generic.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.PayRoll
{
    public class PreviousMonthAdjustment
    {
        public int EmployeeCode { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public Guid EmployeeGI { get; set; }
        public PayrollGroup PayrollGroup { get; set; }
        public PayrollMonth ProcessingMonth { get; set; }
        public PayrollMonth EffectiveMonth { get; set; }
        public Designation Designation { get; set; }
        public Department Department { get; set; }
        public Location Location { get; set; }
        public decimal? Days { get; set; }
        public string Mode { get; set; }
        public List<PreviousMonthAdjustment> PrevMonthAdjList { get; set; }
        public Filter Filter { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public List<PreviousMonthAdjustment> UnMatchedList { get; set; }
        public string Message { get; set; }
        public File Excel { get; set; }
        public string Remarks { get; set; }
        public string NoOfDays { get; set; }
        public string AdjustmentType { get; set; }
        public decimal Amount { get; set; }

    }
}
