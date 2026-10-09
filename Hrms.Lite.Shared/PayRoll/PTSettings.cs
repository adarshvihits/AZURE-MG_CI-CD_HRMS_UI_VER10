using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.PayRoll
{
   public class PTSettings
    {

   
        public PTGroup PTGroup { get; set; }
        public List<PTSettings_Details> Details { get; set; }
        public List<PTSettings_Allowance> Allowance { get; set; }
    }
    public class PTSettings_Details
    {
        public int RowNumber { get; set; }
        public int TrnCode { get; set; }
        public int SalaryFrom { get; set; }
        public int SalaryTo { get; set; }
        public decimal RateAmount { get; set; }
    }
    public class PTSettings_Allowance
    {
        public int AllowanceCode { get; set; }
        public Guid AllowanceGI { get; set; }
        public string Allowance { get; set; }
        public bool Applicability { get; set; }
    }

}
