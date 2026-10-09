using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.Reports
{
    public class TransactionLog
    {
        public string RefNo { get; set; }
        public string UserID { get; set; }
        public string EmployeeID { get; set; }
        public string UserName { get; set; }
        public Filter Filter { get; set; }
        public List<TransactionLog> TransactionlogList { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string CandidateName { get; set; }
        public string Transaction { get; set; }
        public string Actions { get; set; }
        public string Date { get; set; }
        public int UserDropdown { get; set; }
        public int MenuDropdown { get; set; }
        public string Time { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public int BasedOn { get; set; }
    }
}
