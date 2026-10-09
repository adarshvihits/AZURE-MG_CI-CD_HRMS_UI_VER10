using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.PayRoll
{
    public class BonusProcessing
    {
        public Dropdown ProcessMonth { get; set; }
        public Dropdown PeriodFrom { get; set; }
        public Dropdown PeriodTo { get; set; }
        public Dropdown BonusGroup { get; set; }
        public Filter Filter { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public int EmployeeCode { get; set; }
        public Guid EmployeeGI { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public Decimal? BonusTotal { get; set; }
        public Decimal? SalaryTotal { get; set; }
        public Decimal? LopTotal { get; set; }
        public DateTime DOJ { get; set; }
        public DateTime? LWD { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public EmployeeHeader EmployeeHeader { get; set; }
        public List<BonusProcessing> BonusProcessingList { get; set; }
        public List<BonusProcessingDetails> BonusProcessingDetails { get; set; }
    }

    public class BonusProcessingDetails
    {
        public string Month { get; set; }
        public Decimal? BonusAmount { get; set; }
        public Decimal? BonusApplSalary { get; set; }
        public Decimal? LopAmount { get; set; }

    }
}
