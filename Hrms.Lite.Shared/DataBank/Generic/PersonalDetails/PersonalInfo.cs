using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.Generic.PersonalDetails
{
    public class PersonalInfo
    {
        public BloodGroup BloodGroup { get; set; }
        public MaritalStatus MaritalStatus { get; set; }
        public Nullable<DateTime> wedding { get; set; }
        public ReligiousView ReligiousView { get; set; }
        public string Caste { get; set; }
        public ReservationCategory Category { get; set; }
        public int[] Language { get; set; }
        public int[] Hobbies { get; set; }
        public List<Classifications> Languagess { get; set; }
        public List<Classifications> Hobby { get; set; }
    }
}
