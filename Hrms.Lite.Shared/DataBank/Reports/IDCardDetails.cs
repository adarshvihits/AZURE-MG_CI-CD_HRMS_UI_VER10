using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.Reports
{
   public class IDCardDetails
    {
        public Guid EmployeeGI { get; set; }
        public int EmployeeCode { get; set; }
        public string ID { get; set; }
        public string ContactNumber { get; set; }
        public string EmergencyContactNumber { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public BloodGroup BloodGroup { get; set; }
        public string Photo { get; set; }
     
        public int totalRecords { get; set; }
        public int active { get; set; }

        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public Filter filter { get; set; }
        public File File { get; set; }
        public List<IDCardDetails> IDCardDetailsList { get; set; }

    }
   
}


