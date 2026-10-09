using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.PayRoll
{
    public class FormulaBasedDeduction
    {        
        public Deduction Deduction { get; set; }
        public MasterBase Operations { get; set; }
        public decimal? Value { get; set; }
        public List<FormulaBasedDeduction_Details> Addition { get; set; }
        public List<FormulaBasedDeduction_Details> Substraction { get; set; }
    }
    public class FormulaBasedDeduction_Details
    {
        public int DeductionCode { get; set; }
        public string DeductionName { get; set; }

        public string Type { get; set; }
        public bool YN { get; set; }
        public decimal? Percentage { get; set; }
    }
}
