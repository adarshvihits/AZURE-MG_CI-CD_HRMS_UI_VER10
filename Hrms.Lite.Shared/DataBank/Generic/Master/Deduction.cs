using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
namespace Hrms.Lite.Shared.DataBank.Generic.Master
{
    public class Deduction : MasterBase
    {
        public decimal? Amount { get; set; }
        public int? PerAnnum { get; set; }
        public int ActiveCount { get; set; }
        public int InactiveCount { get; set; }
        public int PendingCount { get; set; }
        public int? DisplayOrder { get; set; }
        public DeductionType DeductionType { get; set; }
        public UserBase User { get; set; }

        public List<Deduction> DeductionList { get; set; }
        public decimal? CurrentAmount { get; set; }
        public decimal? NewAmount { get; set; }
        public bool? OldApplicable { get; set; }
    }
    public class DeductionType : MasterBase
    {

    }
}
