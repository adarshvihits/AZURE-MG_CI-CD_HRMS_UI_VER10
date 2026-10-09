using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.Master
{
    public class Allowance : MasterBase
    {
        public decimal? Amount { get; set; }
        public decimal? PerAnnum { get; set; }

        public decimal? AmountOld { get; set; }
        public decimal? PerAnnumOld { get; set; }
        public int ActiveCount { get; set; }
        public int InactiveCount { get; set; }
        public int PendingCount { get; set; }
        public string EntryType { get; set; }
        public bool SalaryComponent { get; set; }
        public string AllowanceType { get; set; }
        public int DisplayOrder { get; set; }
        public AllowanceTypeCbo AllowanceTypeCbo { get; set; }
        public ApplicableFor ApplicableFor { get; set; }
        public List<Allowance> AllowanceList { get; set; }
        public UserBase User { get; set; }
        public ResponseEntity ResponseEntity { get; set; }
        public List<Deduction_SetApplicability> Deduction_SetApplicability { get; set; }
        public decimal? CurrentAmount { get; set; }
        public decimal? NewAmount { get; set; }
        public bool? OldApplicable { get; set; }
        public MasterBase FormulaType { get; set; }
       

        public Dropdown DisplayType { get; set; }
        public int AllowanceApplicableFor { get; set; }

    }
    public class Deduction_SetApplicability
    {
        public int LogCode { get; set; }
        public int DeductionCode { get; set; }
        public string Particular { get; set; }
        public string Type { get; set; }
        public bool Applicability { get; set; }
        public decimal? Percentage { get; set; }
    }
    public class AllowanceTypeCbo : MasterBase
    { }
    public class ApplicableFor : MasterBase
    { }
}
