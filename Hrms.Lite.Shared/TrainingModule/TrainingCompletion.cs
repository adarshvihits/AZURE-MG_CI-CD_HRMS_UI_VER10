using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.TrainingModule
{
    public class TrainingCompletion:MasterBase
    {
        public int ScheduleCode { get; set; }
        public int Status { get; set; }
        public int PostassessmentRequired { get; set; }
        public int TrainingCode { get; set; }
        public int ScheduleID { get; set; }
        public string TrainingSubject { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public UserBase User { get; set; }
        public int HeadCount { get; set; }
        public string RequestFromName { get; set; }
        public string Duration { get; set; }
        public DateTime? RequestUptoDate { get; set; }
        public DateTime? Nextevaldate { get; set; }
        public int TopicCount { get; set; }
        public int ParticipantsCount { get; set; }
        public string Remarks { get; set; }
        public List<TrainingCompletion> TrainingCompletionScheduleList { get; set; }

        public FinancialYear FinancialYear { get; set; }
        public Dropdown Training { get; set; }
        public Dropdown Month { get; set; }
        public Dropdown RequestType { get; set; }
        public Dropdown RequestUpTo { get; set; }
        public Dropdown Subject { get; set; }
        public Dropdown TrainerType { get; set; }
        public Dropdown Trainer { get; set; }
        public Dropdown VenueType { get; set; }
        public Dropdown Venue { get; set; }
        public int OutOfMark { get; set; }
        public List<TrainerTopic> TrainerTopicList { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime TrainingDate { get; set; }
        public int TrainingStatusCode { get; set; }
        public string TrainingStatusType { get; set; }

        public List<TrainingCost> TrainingCostList { get; set; }
        public List<TrainerCost> TrainerCostList { get; set; }
        public List<TrainerFeedBack> TrainerFeedBackList { get; set; }
        public List<File> TrainingDocList { get; set; }

    }
  
    public class TrainerCost
    {
        public int TrainerCostHeadCode { get; set; }
        public string TrainerCostName { get; set; }
        public int Trainercost { get; set; }
        public int TrainerTypeCode { get; set; }
        public int TrainerCode { get; set; }
    }

    public class TrainerFeedBack
    {
        
        public string Name { get; set; }
        public string  FeedBack { get; set; }
    }
 
}
