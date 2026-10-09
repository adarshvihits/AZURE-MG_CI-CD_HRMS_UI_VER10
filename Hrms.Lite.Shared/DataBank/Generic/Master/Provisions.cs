using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
namespace Hrms.Lite.Shared.DataBank.Generic.Master
{
    public class Provisions : MasterBase
    {
        public bool Applicable { get; set; }
        public string AllowanceType { get; set; }
        public decimal? Amount { get; set; }
        public decimal? PerAnnum { get; set; }
        public decimal? OldAmount { get; set; }
        public bool? OldApplicable { get; set; }
        public decimal? CurrentAmount { get; set; }
        public decimal? NewAmount { get; set; }
        public int AllowanceApplicableFor { get; set; }
    }
}
