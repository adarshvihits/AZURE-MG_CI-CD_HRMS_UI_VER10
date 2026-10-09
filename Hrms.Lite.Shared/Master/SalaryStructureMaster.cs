using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.Master
{
    public class SalaryStructureMaster : MasterBase
    {
        public int ActiveCount { get; set; }
        public int InactiveCount { get; set; }
        public int PendingCount { get; set; }        
        public int DisplayOrder { get; set; }
        public string Remarks { get; set; }
        public string ApprovalStatus { get; set; }
        public bool InUse { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public string EntryComments { get; set; }
        public WageGradeMaster WageGrade { get; set; }
        public decimal BasicInKerala { get; set; }
        public decimal HRAForMetro { get; set; }
        public decimal HRAForNonMetro { get; set; }
        public decimal HRAForKerala { get; set; }
        public decimal CCAMetro { get; set; }
        public decimal CCANonMetro { get; set; }
        public decimal CCAKerala { get; set; }
        public decimal LTA { get; set; }
        public WageType WageType { get; set; }
        public Currency Currency { get; set; }
        public SalaryStructureType SalaryStructureType { get; set; }
        public List<Allowance> FixedAllowance { get; set; }
        public List<Allowance> VariableAllowance { get; set; }
        public List<Deduction> FixedDeduction { get; set; }
        public List<Deduction> VariableDeduction { get; set; }
        public List<Provisions> FixedProvisions { get; set; }
        public List<Provisions> VariableProvisions { get; set; }
        public List<AssuredBonous> AssuredBonous { get; set; }
        public SalaryStructureMaster OldData { get; set; }
        public List<SalaryStructureMaster> List { get; set; }
    }
    public class Currency : MasterBase
    {

    }
    public class SalaryStructureType : MasterBase
    {

    }
    public class SalaryStructureList : SalaryStructureMaster
    {
        public TabCount Count { get; set; }

    }
    public class AssuredBonous : MasterBase
    {
        public decimal? AssuredAmt { get; set; }
        public decimal? BlokedAmt { get; set; }

    }
}
