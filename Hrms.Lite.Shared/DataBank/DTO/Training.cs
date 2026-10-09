using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.DTO
{
    public class Training
    {
        public int EmployeeTrainingId { get; set; }
        public Filter filter { get; set; }
        public Guid EmployeeTrainingGI { get; set; }        
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public Guid EmployeeGI { get; set; }
        public EmployeeHeader EmployeeHeader { get; set; }
        public Designation Designation { get; set; }
        public Department Department { get; set; }
        public Location Location { get; set; }
        public Category Category { get; set; }
        public string TrainingName { get; set; }
        public DateTime TrainingDate { get; set; }
        public int EmpCode { get; set; }
        public int Tabindex { get; set; }
        public int SearchId { get; set; }
        public int AllTabCount { get; set; }
        public int Tab1Count { get; set; }
        public int Tab2Count { get; set; }
        public int Tab3Count { get; set; }
        public int Tab4Count { get; set; }
        public int MoreCount { get; set; }
        public int PendingTabCount { get; set; }
        public AssetAllocationFilterCombo FilterList { get; set; }
        public TrainingRequest TrainingRequest { get; set; }
        public List<TrainingList> TrainingList { get; set; }
        public List<TrainingListTabs> TrainingListTabs { get; set; }
        public Filter Filter { get; set; }
        public int TabCode { get; set; }
        public int TrainingTabCode { get; set; }


    }

    public class TrainingList
    {
        public int EmployeeTrainingId { get; set; }
        public Guid EmployeeTrainingGI { get; set; }
        public int EmployeeCode { get; set; }
        public Guid EmployeeGI { get; set; }

        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public Designation Designation { get; set; }
        public Department Department { get; set; }
        public Location Location { get; set; }
        public string TrainingName { get; set; }
        public DateTime AttendedDate { get; set; }
        public TrainingRequest TrainingRequest { get; set; }
    }
    public class TrainingListTabs
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }

    }
    public class TrainingName : MasterBase
    {

    }
    public class TrainingMode : MasterBase
    {
    }
    public class TrainingAssessmentMode : MasterBase
    {
    }

    public class TrainingRequest
    {
        public int EmployeeTrainingId { get; set; }
        public Guid EmployeeTrainingGI { get; set; }
        public EmployeeHeader EmployeeHeader { get; set; }
        public Guid EmployeeGI { get; set; }
        public TrainingName TrainingName { get; set; }

        public DateTime? AttendedDate { get; set; }
        public string Trainer { get; set; }
        public string Type { get; set; }
        public TrainingMode Mode { get; set; }
        public TrainingAssessmentMode TrainingAssessmentMode { get; set; }
        public decimal Duration { get; set; }
        public decimal PostAssessmentScore { get; set; }
        public AssetAllocationFilterCombo FilterLog { get; set; }
        public string EntryUserDetails { get; set; }
        public DateTime EntryDate { get; set; }
        public string Remarks { get; set; }
        public List<TrainingLog> TrainingLog { get; set; }

    }
    public class TrainingLog
    {
        public TrainingRequest TrainingRequest { get; set; }
        public string PostAssessmentScore { get; set; }

    }
}
