using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.PayRoll
{
    public class SlabDeductionSettings
    {
        public int RowNumber { get; set; }
        public int TrnCode { get; set; }
        public int  SalaryFrom { get; set; }
        public int SalaryTo { get; set; }
        public Decimal RateAmount { get; set; }
        public int DeductionCode { get; set; }
        public List<SlabDeductionSettings> SlabDeductionSettingsList { get; set; }
    }
}
