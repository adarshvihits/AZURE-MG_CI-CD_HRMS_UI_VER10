using Hrms.Lite.Shared.General;
using System;

namespace Hrms.Lite.Shared.DataBank.Reports
{
    public class Candidate_Education
    {
        public Guid EducationLogGI { get; set; }
        public MasterBase CourseType { get; set; }
        public MasterBase Qualification { get; set; }
        public MasterBase Specialisation { get; set; }
        public int YearOfCompletion { get; set; }
        public string CourseTypeName { get; set; }
        public string QualificationName { get; set; }
        public string SpecialisationName { get; set; }
        public string YearOfCompletionName { get; set; }
        public string College_University { get; set; }
        public string Institution { get; set; }
        public string CourseDuration { get; set; }
        public int CourseDurationMonths { get; set; }
        public int CourseDurationYears { get; set; }
        public decimal Aggregate { get; set; }
        public bool HighestQual { get; set; }
        public int RowId { get; set; }
        public File File1 { get; set; }
        public File File2 { get; set; }

    }
}
    