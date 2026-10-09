using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.TrainingModule
{
  public   class TrainingLocation :MasterBase
    {
        public int ActiveCount { get; set; }
        public int InactiveCount { get; set; }
        public int PendingCount { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string LocationAddress { get; set; }
        public List<TrainingLocation> TrainingList { get; set; }
        public UserBase User { get; set; }
    }
}
