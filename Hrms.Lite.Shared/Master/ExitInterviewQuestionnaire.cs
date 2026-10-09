using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.Master
{
    public class ExitInterviewQuestionnaire : MasterBase
    {
        public int ActiveCount { get; set; }
        public int InactiveCount { get; set; }
        public int PendingCount { get; set; }
        public UserBase User { get; set; }
        public Guid? GroupGI { get; set; }
        public Guid? TypeGI { get; set; }
        public string GroupName { get; set; }
        public string TypeName { get; set; }
        public string ResultName { get; set; }
        public List<PossibleResultsClassifications> Results { get; set; }

        public List<ExitInterviewQuestionnaire> ExitInterviewQuestionnaireList { get; set; }
        public Guid? ResultGI { get; set; }
        public int?[] Result { get; set; }
        public int QuestionCode { get; set; }
        public int TypeCode { get; set; }
        public int GroupCode { get; set; }


    }
    public class PossibleResultsClassifications : UserBase
    {
        public int SlNo { get; set; }
        public int ResultCode { get; set; }
        public string Name { get; set; }      
        public int InUse { get; set; }
    }

}
