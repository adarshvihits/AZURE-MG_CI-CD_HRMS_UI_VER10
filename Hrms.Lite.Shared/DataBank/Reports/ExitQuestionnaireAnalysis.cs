using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.Reports
{
    public class ExitQuestionnaireAnalysis
    {
        public List<ExitQuestionnaireAnalysisList> ExitQuestionnaireAnalysisList { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public int Count { get; set; }
        public int QuestionCode { get; set; }
        public int AnswerCode { get; set; }
        public Filter SliderFilter { get; set; }
    }
    public class ExitQuestionnaireAnalysisList
    {

        public int QuestionCode { get; set; }
        public Guid QuestionGI { get; set; }
        public Guid AnswerGI { get; set; }
        public int AnswerCode { get; set; }
        public string EmployeeID { get; set; }
        public Guid EmployeeGI { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public int TotalAttempts { get; set; }
        public int Count { get; set; }
        public decimal Percentage { get; set; }
        public DateTime? LWD { get; set; }
        public string Remarks { get; set; }

    }
}
