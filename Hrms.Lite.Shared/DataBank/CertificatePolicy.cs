using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank
{
    public class CertificatePolicy : MasterBase
    {

        public bool Years { get; set; }
        public int? YearsValue { get; set; }
        public int CertificateCode { get; set; }
        public string Validity { get; set; }
        public string CategoryName { get; set; }
        public decimal MaximumReimbursement { get; set; }
        public bool LifeTime { get; set; }
        public Dropdown CertificateCategory { get; set; }
        public UserBase User { get; set; }
        public List<CertificatePolicy> CertificatePolicyList { get; set; }
        public File Attachment { get; set; }
        public int HasData { get; set; }
    }
}
