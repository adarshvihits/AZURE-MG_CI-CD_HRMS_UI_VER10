using Hrms.Lite.Shared.DataBank.Generic.Master;

using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.Generic
{
    public class StatutoryClassification
    {
        public DateTime? PFEnrollDate { get; set; }        
        public string PFNumber { get; set; }
        public string UANNumber { get; set; }
        public DateTime? ESIEnrollDate { get; set; }
        public string ESINumber { get; set; }
        public string ESIDispensary { get; set; }
        public PaymentMode PaymentMode { get; set; }
        public Bank Bank { get; set; }
        public string BankBranch { get; set; }
        public string AccountNo { get; set; }
        public string IFSC { get; set; }
        public File BankFile { get; set; }
        public string PFApplicability { get; set; }
        public string ESIApplicability { get; set; }
        public string PTApplicability { get; set; }
        public ESIGroup ESIGroup { get; set; }
        public PFGroup PFGroup { get; set; }
        public PTGroup PTGroup { get; set; }
        public File Passbook { get; set; }
        public string GratuityNo { get; set; }
        public string NSDL { get; set; }
        public string MedicalPolicy { get; set; }
        public string AccidentPolicy { get; set; }
        public string EDLI { get; set; }

    }
}
