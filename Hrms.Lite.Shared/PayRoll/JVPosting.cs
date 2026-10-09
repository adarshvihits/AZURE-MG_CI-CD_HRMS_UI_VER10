using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.PayRoll
{
   public class JVPosting
    {
        public Dropdown PayrollMonth { get; set; }
        public PayrollGroup PayrollGroup { get; set; }
        public string Location { get; set; }
        public int radioSelection { get; set; }
        public string GlAccountCode { get; set; }
        public string SerialNo { get; set; }
        public string PayElementDesc { get; set; }
        public string VocherDate { get; set; }
        public string ProcessPeriod { get; set; }
        public string RefNo { get; set; }
        public string Narration { get; set; }
        public string DrOrCr { get; set; }
        public decimal Amount { get; set; }
        public string DeptCostCenter { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public List<JVPosting> JVPostingList { get; set; }
    }
    public class JVCheckList
    {
        public string Name { get; set; }
        public string Reason { get; set; }
    }
}
