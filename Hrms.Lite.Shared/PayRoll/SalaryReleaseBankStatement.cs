using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.PayRoll
{
   public class SalaryReleaseBankStatement
    {
        public MasterBase PayrollMonth { get; set; }
        public int ReportFormat { get; set; }
        public string ReportFormatName { get; set; }
        public string Date { get; set; }
        public string FileName { get; set; }
        public EmployeeHeader EmployeeHeader { get; set; }
        public bool Applicability { get; set; }
        public string Designation { get; set; }
        public string AccountNo { get; set; }
        public string IFSC { get; set; }
        public string Bank { get; set; }
        public decimal NetPay { get; set; }
        public DateTime ExecutionDate { get; set; }
        public Filter Filter { get; set; }
        public string Type { get; set; }
        public string TransferStatus { get; set; }

        public List<SalaryReleaseBankStatement> SalaryReleaseBankStatementList { get; set; }
    }
}
