using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.Generic.Master
{
   public class Qualification:MasterBase
    {
        public int ActiveCount { get; set; }
        public int InactiveCount { get; set; }
        public int PendingCount { get; set; }
        public string EntryType { get; set; }
        public List<Qualification> QualificationList { get; set; }
       
        public QualificationType QualificationTypeCode { get; set; }
        public int IsProfessional { get; set; }
    }
}
