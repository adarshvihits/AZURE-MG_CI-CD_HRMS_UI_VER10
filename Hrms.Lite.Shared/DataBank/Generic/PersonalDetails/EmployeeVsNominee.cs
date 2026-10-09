using Hrms.Lite.Shared.DataBank.Generic.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.Generic.PersonalDetails
{
    public class EmployeeVsNominee
    {
        public string Mobile { get; set; }
        public string Aadhar { get; set; }
        public string Name { get; set; }
        public Relation Relation { get; set; }
        public bool ApprovedStatus { get; set; }
        public NominationCategory NominationCategory { get; set; }
        public decimal Percentage { get; set; }
        public string Comments { get; set; }
        public int flag { get; set; }
        public Guid EmpVsNomLogGI { get; set; }
    }
}
