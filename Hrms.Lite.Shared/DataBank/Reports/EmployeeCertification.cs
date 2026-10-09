using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.Reports
{
    public class EmployeeCertification
    {
        public int TabIndex { get; set; }
        public int ActiveCount { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public Filter Filter { get; set; }
      
        public List<EmployeeCertificationList> EmployeeCertificationList { get; set; }
        
        public string Remarks { get; set; }

        public string EmployeeId { get; set; }
       
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public int CertificateTypecode { get; set; }
    }
    public class EmployeeCertificationList
    {
        public string CertificateName { get; set; }
        public string RegisterNo { get; set; }
        public string Name { get; set; }
        public string EmployeeId { get; set; }
        public Guid EmployeeGI { get; set; }
        public int code { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }

    }
}
