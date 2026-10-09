using Hrms.Lite.Shared.DataBank.Generic.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.Generic.PersonalDetails
{
    public class FamilyDetails
    {
        public Guid FamilyLogGI { get; set; }
        public string Name { get; set; }
        public string RelationName { get; set; }
        public int RelationCode { get; set; }
        public FamilyRelation Relation { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Occupation { get; set; }
        public LivingStatus Status { get; set; }
        public string StatusName { get; set; }
        public bool ApprovedStatus { get; set; }
        public int flag { get; set; }
    }
}
