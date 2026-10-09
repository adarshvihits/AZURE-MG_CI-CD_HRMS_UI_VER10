using Hrms.Lite.Shared.General;
using System;

namespace Hrms.Lite.Shared.DataBank.Reports
{
    public class Candidate_Certifications
    {
        public int RowID { get; set; }
        public MasterBase Certificate { get; set; }
        public string RegistrationNo { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public DateTime? UpdationDate { get; set; }
        public bool ApprovedStatus { get; set; }
        public string Remarks { get; set; }
        public File File { get; set; }
        public string CertificateName { get; set; }

    }
}