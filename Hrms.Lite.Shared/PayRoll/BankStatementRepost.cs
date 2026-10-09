using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.PayRoll
{
  public  class BankStatementRepost
    {
        public int RepostingCount { get; set; }
        public int PendingCount { get; set; }
        public string TabIndex { get; set; }
        public Dropdown PayrollMonth { get; set; }
        public GroupCompany GroupCompany { get; set; }
        public string FileName { get; set; }
        public string PostedUser { get; set; }
        public int PostedUserCode { get; set; }
        public string PostedDate { get; set; }
        public List<BankStatementRepost> BankStatementRepostList { get; set; }
    }
}
