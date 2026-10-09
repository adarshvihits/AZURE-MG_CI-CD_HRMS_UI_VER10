
using System;
using System.Collections.Generic;
using Hrms.Lite.Shared.General;

namespace Hrms.Lite.Shared.TDS
{
  public  class PreviousEmployer
    {

        public EmployeeHeader Employee { get; set; }
        public MasterBase AssessmentYear { get; set; }
        public string PrevEmployerName { get; set; }
        public string PrevEmployerPAN { get; set; }
        public string PrevEmployerTAN { get; set; }
        public DateTime PrevEmployerFromDate { get; set; }
        public DateTime PrevEmployerToDate { get; set; }
        public decimal GrossIncome { get; set; }
        public decimal PTDed { get; set; }
        public decimal PFDed { get; set; }
        public decimal TDSDed { get; set; }
        public File File1 { get; set; }
        public File File2 { get; set; }
        public string Remarks { get; set; }
        public List<EmployeeList> TDSEmployeeList { get; set; }
        public Filter filter { get; set; }
    }
    public class EmployeeList
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

	
