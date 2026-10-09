using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll.Generic.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.PayRoll
{
   public class BonusUpload
    {
        public int EmployeeCode { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public Guid EmployeeGI { get; set; }
        public PayrollMonth Month { get; set; }
        public Dropdown IncentiveCategory { get; set; }
        public string Year { get; set; }
        public string EffectiveMonth { get; set; }
        public string Quarter { get; set; }
        public string Product { get; set; }
        public string SubProduct { get; set; }
        public string BonusType { get; set; }
        public decimal? Amount { get; set; }
        public string AmountStr { get; set; }
        public string Message { get; set; }
        public List<BonusUpload> BonusUploadList { get; set; }
        public File Excel { get; set; }
        public Filter Filter { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public decimal TotalAmount { get; set; }
        public string Remark { get; set; }
    }
}
