using System;
using System.Collections.Generic;
using System.Text;
using Hrms.Lite.Shared.General;

namespace Hrms.Lite.Shared.TDS
{
   public class Form16Settings
    {
        public string FinancialYear { get; set; }
        public MasterBase AssessmentYear { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Address { get; set; }
        public string PAN { get; set; }
        public string TAN { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string Designation { get; set; }
        public string Place { get; set; }
        public DateTime ? PrintDate { get; set; }
        public MasterBase GroupCompany { get; set; }

     

    }
   
}
