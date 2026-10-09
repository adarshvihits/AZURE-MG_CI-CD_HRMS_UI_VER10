using Hrms.Lite.Shared.DataBank.Generic.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.Generic.PersonalDetails
{
   public class Certifications
    {
        public Guid CertificateLogGI { get; set; }
        public int CertificationCode { get; set; }
        public string Certificate { get; set; }
        public int certificateCode { get; set; }
        public string RegistrationNo { get; set; }
        public DateTime? Examdate { get; set; }
        public CertificateType CertificateType { get; set; }
        public string IssuedBy { get; set; }
        public DateTime? Issued { get; set; }
        public CertificateMode CertificateMode { get; set; }
        public CertificateValidity CertificateValidity { get; set; }
        public DateTime? Renewaldate { get; set; }
        public DateTime? ValidTo { get; set; }
        public bool ApprovedStatus { get; set; }
        public int flag { get; set; }
        public File File { get; set; }

        public int LogCode { get; set; }
        public int DocCount { get; set; }
        public int CertificateDocCode { get; set; }
    }
}
