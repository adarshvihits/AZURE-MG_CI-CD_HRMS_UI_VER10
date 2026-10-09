using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
namespace Hrms.Lite.Shared.Master
{
    public class Bank : MasterBase 
    {
        public int ActiveCount { get; set; }
        public int InactiveCount { get; set; }
        public int PendingCount { get; set; }
        public int DigitsAccountNumber { get; set; }
        public List<Bank> BankList { get; set; }
    }
}
