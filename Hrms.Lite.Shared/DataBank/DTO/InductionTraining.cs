using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.DTO
{
    public class InductionTraining
    {
        public int EmployeeCode { get; set; }
        public int HeadCode { get; set; }
        public string EmpId { get; set; }
        public Guid EmployeeGuid { get; set; }
        public string EmployeeName { get; set; }
        public Designation Designation { get; set; }
        public Department Department { get; set; }
        public Location Location { get; set; }
        public DateTime DOJ { get; set; }
        public bool Applicability { get; set; }
        public DateTime? AttendedDate { get; set; }
        public DateTime? ScheduledDate { get; set; }
        public DateTime? AcceptedDate { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string InitiateDescription { get; set; }
        public string AcceptedDescription { get; set; }
        public string Mode { get; set; }
        public string Remarks { get; set; }
        public List<InductionTraining> InductionTrainingList { get; set; }
        public List<InductionTrainingType> InductionTrainingTypeList { get; set; }
        public List<InductionTrainingAcceptance> InductionTrainingAcceptanceList { get; set; }
        public List<InductionTrainingCompletion> InductionTrainingCompletionList { get; set; }
        public List<InductionReport> InductionReportList { get; set; }
        public Filter Filter { get; set; }
    }

    public class InductionTrainingType
    {
        public int TrainingCode { get; set; }

        public Guid GI { get; set; }
        public string TrainingName { get; set; }
        public bool Applicability { get; set; }
    }
    public class InductionTrainingAcceptance
    {
        public int HeadCode { get; set; }
        public int EmployeeCode { get; set; }
        public string TrainingName { get; set; }
        public DateTime? SheduledDate { get; set; }
        public string Description { get; set; }
    }
    public class InductionTrainingCompletion
    {
        public int HeadCode { get; set; }
        public int EmployeeCode { get; set; }
        public string TrainingName { get; set; }
        public DateTime? SheduledDate { get; set; }
        public string Description { get; set; }
    }
    public class InductionReport
    {
        public string ID { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public DateTime? InitiateDate { get; set; }
        public string InitiateDescription { get; set; }
        public string InitiateBy { get; set; }
        public DateTime? ScheduledDate { get; set; }
        public string EmpDescription { get; set; }
        public DateTime? EmpAcceptedDate { get; set; }
        public string EmpAcceptStatus { get; set; }
        public string ApprDescription { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public string ApprStatus { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
    }
}
