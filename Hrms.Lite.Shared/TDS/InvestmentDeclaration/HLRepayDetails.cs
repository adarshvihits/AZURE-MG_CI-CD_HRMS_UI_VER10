using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.TDS
{
    public class HLRepayDetails
    {
        public int TrnCode { get; set; }
        public string HouseAddress { get; set; }
        public string LocationStatusName { get; set; }
        public string OwnerStatusName { get; set; }
        public string PurposeName { get; set; }
        public Dropdown LocationStatus { get; set; }
        public Dropdown OwnerStatus { get; set; }
        public decimal Percentage { get; set; }
        public Dropdown Purpose { get; set; }
        public DateTime DateofLoan { get; set; }
        public DateTime DateofCompletion { get; set; }
        public string LoanDate { get; set; }
        public string CompletionDate { get; set; }
        public decimal PrincipalAmt { get; set; }
        public decimal Interest { get; set; }
        public decimal PreEMIInterest { get; set; }
        public decimal TotalInterest { get; set; }
        public string NameofBank { get; set; }
        public string PANofBank { get; set; }
        public string BankAddress { get; set; }
        public string Remarks { get; set; }
        public int RepayDocCount { get; set; }
        public int flag { get; set; }
        public string ApprovalStatus { get; set; }
        public string StatusColor { get; set; }
    }
}
