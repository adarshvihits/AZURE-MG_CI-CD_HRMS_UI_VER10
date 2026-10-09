using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.Generic.PersonalDetails
{
    public class PersonalDetailsDropDown
    {
        public List<FamilyRelation> FamilyRelation { get; set; }
        public List<BloodGroup> bloodGroup { get; set; }
        public List<MaritalStatus> MaritalStatus { get; set; }
        public List<ReligiousView> Religion { get; set; }
        public List<ReservationCategory> ReservationCategory { get; set; }
        //hobby
        //Language
        public List<Relation> Relation { get; set; }
        public List<NominationCategory> NominationCategory { get; set; }
        public List<LivingStatus> Status { get; set; }
        public List<QualificationType> QualificationType { get; set; }
        public List<YearOfCompletion> YearOfCompletion { get; set; }
        public List<Skill> Skill { get; set; }
        public List<SkillProficiency> SkillProficiency { get; set; }
        public List<CertificateType> CertificateType { get; set; }
        public List<CertificateMode> CertificateMode { get; set; }
        public List<CertificateValidity> CertificateValidity { get; set; }

        public List<Hobbies> Hobbies { get; set; }
        public List<Languages> Languages { get; set; }
        public List<MedicalCheckupList> MedicalCheckup { get; set; }
    }
}
