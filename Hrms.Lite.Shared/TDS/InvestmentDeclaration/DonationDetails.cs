using System;
using System.Collections.Generic;
using System.Text;
using Hrms.Lite.Shared.General;
namespace Hrms.Lite.Shared.TDS
{
    public class DonationDetails
    {
        public int TrnCode { get; set; }
        public int EmployeeCode { get; set; }
        public int InvestmentCode { get; set; }
        public string InvestmentName { get; set; }
        public decimal Amount { get; set; }
        public decimal ApprovedAmount { get; set; }
        public int DonDocCount { get; set; }
        public int flag { get; set; }
        public string ApprovalStatus { get; set; }
        public string StatusColor { get; set; }

    }
}
