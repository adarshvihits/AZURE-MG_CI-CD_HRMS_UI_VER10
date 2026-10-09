using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.TrainingModule
{
    public class TrainingEmployeeGroup : MasterBase
    {
        public string EntryType { get; set; }

        public int ActiveCount { get; set; }
        public int InactiveCount { get; set; }
        public int PendingCount { get; set; }
        public string Training { get; set; }
        public int TrainingCode { get; set; }
        public List<TrainingEmployeeGroup> TrainingEmployeeGroupList { get; set; }
        public List<Trainings> TrainingsList { get; set; }
        public UserBase User { get; set; }
        public Trainings Trainings { get; set; }


    }
    public class Trainings
    {
        public int TrainingCode { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public bool Applicability { get; set; }
        public bool Mandatory { get; set; }
        public Dropdown TrainingDuration { get; set; }


    }
}
