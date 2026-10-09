using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.MIS
{
    public class CompanyMIS
    {

        public List<MISDetailsList> MISDetailsList { get; set; }
        public int Bbb { get; set; }

        public List<PayrollGP> PayrollGP { get; set; }
    }

    public class MISDetailsList
    {
        public int code { get; set; }
        public string name { get; set; }
        public int count { get; set; }
        public int sepCount { get; set; }
        public string reportName { get; set; }
        public string nameHead { get; set; }
        public double valuePercentage { get; set; }
        public decimal sepPercentage { get; set; }

    }

    public class PayrollGP
    {
        public string month { get; set; }
        public int manpower { get; set; }
        public int grosspay { get; set; }
    }

}
