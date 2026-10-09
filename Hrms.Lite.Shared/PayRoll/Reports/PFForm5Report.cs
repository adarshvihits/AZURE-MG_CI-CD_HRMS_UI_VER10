using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.PayRoll.Reports
{
    public class PFForm5Report
    {
        public int PayrollMonth { get; set; }
        public int From { get; set; }
        public int To { get; set; }
        public Url Url { get; set; }
        public int CompanyCode { get; set; }
        public int UserCode { get; set; }
        public int GroupCompanyCode { get; set; }
        public int Designation { get; set; }
        public int Department { get; set; }
        public int Location { get; set; }
        public int Category { get; set; }
        public int Grade { get; set; }
        public int PayrollGP { get; set; }
        public int State { get; set; }
        public int Region { get; set; }
        public int Division { get; set; }
        public int Section { get; set; }
        public int EmpType { get; set; }
        public int WorkLocation { get; set; }
        public int WageGrade { get; set; }
        public int PFGroup { get; set; }
        public int ESIGroup { get; set; }
        public Filter filter { get; set; }
    }
}
