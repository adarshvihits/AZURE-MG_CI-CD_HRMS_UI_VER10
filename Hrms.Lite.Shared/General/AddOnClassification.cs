using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.General
{
    public class AddOnClassification
    {
        public bool AddOnClassification_Applicability { get; set; }
        public bool DivisionExists { get; set; }
        public bool SectionExists { get; set; }
        public bool EmploymentTypeExists { get; set; }
        public bool WorkLocationExists { get; set; }
        public bool WageGradeExists { get; set; }
    }
}
