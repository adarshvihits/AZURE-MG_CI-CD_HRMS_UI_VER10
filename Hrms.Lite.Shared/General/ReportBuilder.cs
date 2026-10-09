
using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.General
{
    public class ReportBuilder
    {
       
        public Saved_Report Saved_Report { get; set; }
        public MultiSelectFilter Filter { get; set; }
        public Filter SliderFilter { get; set; }
        public List<ReportBuilderDetails> Details { get; set; }
        public List<ReportBuilderDetails> Combinations { get; set; }
        public List<ReportBuilder_SavedDetails> SavedDetails { get; set; }
        public List<dynamic> Data { get; set; }
        public ReportBuilder_MainList MainList { get; set; }
        public string ReportName { get; set; }
        public string Mode { get; set; }
        public int ReportCode { get; set; }
        public MasterBase FinancialYear { get; set; }
        public MasterBase Type { get; set; }
        public MasterBase From { get; set; }
        public MasterBase To { get; set; }
        public MasterBase Month { get; set; }
        public MasterBase BasedOn { get; set; }
        public int PaySelectionBasis { get; set; }
        public int Tab { get; set; }
    }


    public class Saved_Report
    {
        public int Code { get; set; }
        public string Name { get; set; }
    }
    public class ReportBuilderDetails
    {
     
        public int Code { get; set; }
        public int Order { get; set; }
        public string FieldGroup { get; set; }
        public string Field { get; set; }
         public bool Applicability { get; set; }
    }
    public class ReportBuilder_SavedDetails
    {
        public int Code { get; set; }
        public int SlNo { get; set; }
        public string ReportName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
    }

    public class ReportBuilder_MainList
    {
        public List<Header> Header { get; set; }
        public List<ReportBuilder_PrimaryInfo> PrimaryInfo { get; set; }
        public List<ReportBuilderInfoDetails> ReportBuilderInfoDetails { get; set; }


    }

    public class ReportBuilder_PrimaryInfo
    {
        public int Code { get; set; }

    }
    public class ReportBuilderInfoDetails
    {
        public string Details { get; set; }


    }

}
