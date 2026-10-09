using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.TDS
{
    public class TaxCalculator
    {
        public EmployeeHeader EmployeeHeader { get; set; }
        public Guid EmployeeGuid { get; set; }
        public List<TDSComponentSplitUpNew> TDSComponentSplitUpNew { get; set; }
        public List<TaxNew> TaxDetailsNew { get; set; }
        public List<TDSComponentSplitUpOld> TDSComponentSplitUpOld { get; set; }
        public List<TaxOld> TaxDetailsOld { get; set; }
        public decimal TaxOld { get; set; }
        public decimal TaxNew { get; set; }
        public string Regime { get; set; }
        public Filter filter { get; set; }
        public List<TDSEmployeeList> TDSEmployeeList { get; set; }
        public int AssessmentYr { get; set; }
        public int Parameter { get; set; }


    }


    public class TDSComponentSplitUpNew
    {
        public int SlNo { get; set; }

        public string Head { get; set; }
        public decimal ProcessedSalary { get; set; }
        public decimal ProjectedSalary { get; set; }
        public decimal Total { get; set; }


    }
    public class TDSComponentSplitUpOld
    {
        public int SlNo { get; set; }

        public string Head { get; set; }
        public decimal ProcessedSalary { get; set; }
        public decimal ProjectedSalary { get; set; }
        public decimal Total { get; set; }


    }
    public class TaxNew
    {
        public decimal Amount { get; set; }
        public decimal ITRate { get; set; }
        public decimal TDS { get; set; }
        public decimal Edu { get; set; }
        public decimal Total { get; set; }

    }
    public class TaxOld
    {
        public decimal Amount { get; set; }
        public decimal ITRate { get; set; }
        public decimal TDS { get; set; }
        public decimal Edu { get; set; }
        public decimal Total { get; set; }

    }
    public class TDSEmployeeList
    {

        public EmployeeHeader EmployeeHeader { get; set; }
        public Guid EmployeeGuid { get; set; }
        public int EmployeeCode { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
    }
}
