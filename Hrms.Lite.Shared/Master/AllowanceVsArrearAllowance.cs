using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.Master
{
   public class AllowanceVsArrearAllowance
    {
    
        public int AllowanceCode { get; set; }
        public Guid AllowanceGI { get; set; }
        public string AllowanceName { get; set; }
        public Allowance ArrearAllowance { get; set; }
        public List<AllowanceVsArrearAllowance> AllowanceVsArrearAllowanceList { get; set; }
    }
}
