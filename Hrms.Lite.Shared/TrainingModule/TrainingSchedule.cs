using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Hrms.Lite.Shared.TrainingModule
{
    public class TrainingSchedule:MasterBase
    {
        public int ScheduleCode { get; set; }
        public int Status { get; set; }
        public int PostassessmentRequired { get; set; }
        public int TrainingCode { get; set; }
        public int ScheduleID { get; set; }
        public string TrainingSubject { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int HeadCount { get; set; }
        public string RequestFromName { get; set; }
        public string Duration { get; set; }
        public DateTime? RequestUptoDate { get; set; }
        public DateTime? Nextevaldate { get; set; }
        public int TopicCount { get; set; }
        public int? ParticipantsCount { get; set; }
        public string Remarks { get; set; }

        public int SheduledCount { get; set; }
        public int CompletedCount { get; set; }
        public int NotCompletedCount { get; set; }
        public decimal AllotedAmount { get; set; }
        public decimal CostAmount { get; set; }
        public decimal VarienceAmount { get; set; }
        public List<TrainingSchedule> TrainingScheduleList { get; set; }

        public FinancialYear FinancialYear { get; set; }
        public Dropdown Training { get; set; }
        public Dropdown Month { get; set; }
        public Dropdown RequestType { get; set; }
        public Dropdown RequestUpTo { get; set; }
        public Dropdown Subject { get; set; }
        public Dropdown Category { get; set; }
        public Dropdown TrainerType { get; set; }
        public Dropdown Trainer { get; set; }
        public Dropdown VenueType { get; set; }
        public Dropdown Venue { get; set; }
        public int? OutOfMark { get; set; }
        public List<TrainerTopic> TrainerTopicList { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime TrainingDate { get; set; }

        public DataTable DataTable { get; set; }
    } 
    public class TrainerTopic 
    { 
        public int TransactionCode { get; set; }
        public int ScheduleCode { get; set; }
        public int TrainingCode { get; set; }
        public int TrainingSubjectCode { get; set; }
        public string Topicsub { get; set; }
        public string Trainer { get; set; }
        public string Venue { get; set; }
        public DateTime Trainingdate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
   
}
