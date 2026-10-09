using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.DTO
{
 public class HRPlanningBudget
    {
  
        public MasterBase HRPlanning { get; set; }
        public Designation Designation { get; set; }
        public int TotalBudgetedNo { get; set; }
        public File Excel { get; set; }
        public MasterBase Type { get; set; }
        public int TotalCurrentNo { get; set; }
        public int TotalVariance { get; set; }
        public string IsOverwrite { get; set; }

        public List<HRPlanningBudget> MainList { get; set; }
        public List<HRPlanningBudgetDetails> Details { get; set; }
        public string Message { get; set; }
    }
    public class HRPlanningBudgetDetails
    {
        public int TrnCode { get; set; }
        public Guid GI { get; set; }
        public Location Location { get; set; }
        public string LocationName { get; set; }
        public Region Region { get; set; }
        public string BudgetedNo { get; set; }
        public int CurrentNo { get; set; }
        public int VarianceNo { get; set; }
        public int VariancePerce { get; set; }
        public int FilledPerce { get; set; }
        public int CurrentMRF { get; set; }
        public int RAApproved { get; set; }
        public String Short_Excess { get; set; }
        public int NewMRF_Planned { get; set; }
        public bool Select { get; set; }
        public string Remark { get; set; }

    }
}
