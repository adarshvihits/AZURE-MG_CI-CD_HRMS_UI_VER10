using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;

namespace Hrms.Lite.Shared.Recruitment
{
    public class InterviewFeedbackSettings
    {
        public UserBase User { get; set; }
        public Guid FeedbackSetGI { get; set; }
        public int FeedbackSetCode { get; set; }
        public string SetID { get; set; }
        public string Name { get; set; }
        public int RoundType { get; set; } // 1-First Round 2-Second Round
        public string RoundName { get; set; }
        public string Mode { get; set; }
        public int QuestionCount { get; set; }
        public int FirstRoundCount { get; set; }
        public int SecondRoundCount { get; set; }
        public int GradeCode { get; set; } // grade filter on the list
        public List<int> SelectedGrades { get; set; }
        public List<string> GradeNameList { get; set; }
        public List<Dropdown> GradeList { get; set; }
        public List<InterviewFeedbackQuestion> QuestionList { get; set; }
        public List<InterviewFeedbackSettings> FeedbackSetList { get; set; }
    }

    public class InterviewFeedbackQuestion
    {
        public int QuestionCode { get; set; }
        public string Question { get; set; }
        public int DisplayOrder { get; set; }
        public bool Active { get; set; }
    }
}
