using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.PayRoll
{
   public class Incentive
    {
        public int TrnCode { get; set; }
        public Guid EmployeeGI { get; set; }
        public string EmployeeID { get; set; }
        public int EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public int BonusCategoryCode { get; set; }
        public string BonusType { get; set; }
        public Filter filter { get; set; }
        public int Month { get; set; }
        public string Designation { get; set; }
        public string Location { get; set; }
        public decimal Amount { get; set; }
        public string AmountStr { get; set; }
        public decimal TotalAmount { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public List<Incentive> IncentiveList { get; set; }
        public File Excel { get; set; }
        public string Remarks { get; set; }

    }
}
