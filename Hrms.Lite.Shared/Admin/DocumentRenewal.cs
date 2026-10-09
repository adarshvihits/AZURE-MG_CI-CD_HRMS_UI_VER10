using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.Admin
{
   public class DocumentRenewal
    { 
        public Guid? GI { get; set; }
        public List<DocumentRenewal> DocumentRenewalList { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public Filter filter { get; set; }
        public MasterBase RenewalsCategory { get; set; }
        public string DocumentName { get; set; }
        public DateTime? RenewalDate { get; set; }
        public string Remarks { get; set; }
        public string Mode { get; set; }
    }
}
