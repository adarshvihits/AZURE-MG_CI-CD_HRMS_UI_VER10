using Hrms.Lite.Shared.DataBank.Generic.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.Generic.PersonalDetails
{
    public class Education
    {
        public Guid EducationLogGI { get; set; }
        public int EducationLogCode { get; set; }
        
        public QualificationType QualificationType { get; set; }
        public  Qualification Qualification { get; set; }
        public Specialisation Specialisation { get; set; }
        public YearOfCompletion YearOfCompletion { get; set; }
        public string QualificationTypeName { get; set; }
        public string QualificationName { get; set; }
        public string SpecialisationName { get; set; }
        public string YearOfCompletionName { get; set; }
        public bool ApprovedStatus { get; set; }
        public int flag { get; set; }
        public string College_University { get; set; }
        public string Aggregate { get; set; }
        public File File { get; set; }
    }
}
