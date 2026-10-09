using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.DTO
{
   public class Appraisal
    {
        public Guid AppraisalGI { get; set; }
        public int AppraisalGIId { get; set; }
        public EmployeeHeader EmployeeHeader { get; set; }
        public string EmpId { get; set; }
        public string EmployeeName { get; set; }
        public Guid EmployeeGI { get; set; }
        public FilterSlabComboFill DurationwiseFilterSlab { get; set; }
        public List<AppraisalList> AppraisalList { get; set; }
        public List<AppraisalTabs> AppraisalTabs { get; set; }
        public AppraisalRequest AppraisalRequest { get; set; }
        public int Tabindex { get; set; }
        public int SearchId { get; set; }
        public int AllTabCount { get; set; }
        public int Tab1Count { get; set; }
        public int Tab2Count { get; set; }
        public int Tab3Count { get; set; }
        public int Tab4Count { get; set; }
        public int MoreTabCount { get; set; }
        public int AssesmentMode { get; set; }
        public Filter Filter { get; set; }
        public int TabCode { get; set; }
        public int AppraisalTypeCode { get; set; }

    }
    public class AppraisalList
    {
        public int AppraisalTrnId { get; set; }
        public Guid AppraisalTrnGI { get; set; }
        public int EmployeeCode { get; set; }
        public string EmpId { get; set; }
        public Guid EmployeeGuid { get; set; }
        public string EmployeeName { get; set; }
        public Designation Designation { get; set; }
        public Department Department { get; set; }
        public Location Location { get; set; }
        public AppraisalRequest AppraisalRequest { get; set; }
    }
    public class AppraisalTabs
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
    }
    public class AppraisalRequest
    {
        public Guid AppraisalTrnGI { get; set; }
        public Guid EmployeeGI { get; set; }
        public EmployeeHeader EmployeeHeader { get; set; }
        public EvaluationPeriod EvaluationPeriod { get; set; }
        public AppraisalType AppraisalType { get; set; }
        public Recommendation Recommendation { get; set; }
        public Evaluator Appraiser { get; set; }
        public Evaluator Reviewer { get; set; }

        public AppraisalRating SelfRating { get; set; }
        public AppraisalRating AppraiserRating { get; set; }
        public AppraisalRating ReviewerRating { get; set; }
        public string SelfScore { get; set; }
        public string AppraiserScore { get; set; }
        public string ReviewerScore { get; set; }
        public string Description { get; set; }
        public string Remarks { get; set; }
        public List<AppraisalLog> AppraisalLog { get; set; }
    }
    public class Evaluator : MasterBase
    {
    }
    public class AppraisalRating : MasterBase
    {
    }
    public class Recommendation : MasterBase
    {
    }
    public class AppraisalLog
    {
        public AppraisalRequest AppraisalRequest { get; set; }


    }
}

