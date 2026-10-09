using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.TrainingModule
{
    public class PreTrainingSchedule
    {
        public int TestHeadCode { get; set; }
        public int CompanyCode { get; set; }
       public int EmployeeCode { get; set; }
        public Guid EmployeeGI { get; set; }
        public string EmployeeID { get; set; }
        public string Fullname { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }

        public int PreTestScheduleCode { get; set; }
        public int Status { get; set; }
        public int PostassessmentRequired { get; set; }
        public int TrainingCode { get; set; }
        public int ScheduleID { get; set; }
        public string Training { get; set; }
        public string EntryStatus { get; set; }
        public Decimal Mark { get; set; }
        public Decimal OutOf { get; set; }
        public Decimal PercentageAcheived { get; set; }
        public DateTime ScheduleDate { get; set; }
        public DateTime EndDate { get; set; }
        public int HeadCount { get; set; }
        public string RequestFromName { get; set; }
        public string TestPassedStatus { get; set; }
        public string Duration { get; set; }
        public DateTime? RequestUptoDate { get; set; }
        public DateTime? Nextevaldate { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public int TopicCount { get; set; }
        public int? ParticipantsCount { get; set; }
        public string Remarks { get; set; }
        public Filter Filter { get; set; }
        public int SheduledCount { get; set; }
        public int CompletedCount { get; set; }
        public int NotCompletedCount { get; set; }
        public decimal AllotedAmount { get; set; }
        public decimal CostAmount { get; set; }
        public decimal VarienceAmount { get; set; }
        public List<PreTrainingSchedule> PreTrainingScheduleList { get; set; }
        public int totalRecords { get; set; }

        public int active { get; set; }
    }
}
