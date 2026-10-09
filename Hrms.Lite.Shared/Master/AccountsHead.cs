using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.Master
{
    public class AccountsHead:MasterBase
    {
        public int ActiveCount { get; set; }
        public int InactiveCount { get; set; }
        public int PendingCount { get; set; }
        public List<AccountsHead> AccountsHeadList { get; set; }
        public List<AccountsHeadTablist> AccountsHeadTablist { get; set; }
        public MasterBase Grouping { get; set; }
        public List<ClassificationVsERPcode> ClassificationVsERPCodeList { get; set; }
        public string AccountsHeadUserCode { get; set; }
        public int AccountsHeadCodeType { get; set; }
        public string AccountsHeadCodeTypeName { get; set; }
        public string TabName { get; set; }
        public int Ledger { get; set; }
        public int GroupCompanyCode { get; set; }
    }

    public class ClassificationVsERPcode
    {
        public int ClassificationCode { get; set; }
        public string ClassificationName { get; set; }
        public int MasterCode { get; set; }
        public string MasterName { get; set; }
        public int CompanyCode { get; set; }
        public string CompanyName { get; set; }
        public string ERPCode { get; set; }

    }

    public class AccountsHeadTablist
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public int Ledger { get; set; }
        public string LedgerName { get; set; }
        public string AllowanceType { get; set; }
        public string AccountsHeadUserCode { get; set; }
    }
}
