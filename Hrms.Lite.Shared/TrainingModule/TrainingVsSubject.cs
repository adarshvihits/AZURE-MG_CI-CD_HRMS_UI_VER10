using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.TrainingModule
{
   public class TrainingVsSubject
    {
        public int TrainingCode { get; set; }      
        public List<TrainingSubject> SubjectDetails { get; set; }
    }


}
