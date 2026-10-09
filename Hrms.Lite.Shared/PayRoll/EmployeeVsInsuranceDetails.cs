using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.PayRoll
{
    public class EmployeeVsInsuranceDetails
    {       
        public InsurancePolicy InsurancePolicy { get; set; }
        public int EmployeeCode { get; set; }
        public int TransactionCode { get; set; }
        public string PolicyNumber { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? MaturityDate { get; set; }
        public decimal? PrincipalAmount { get; set; }
        public decimal? PremiumAmount { get; set; }
        public int NoOfInsurance { get; set; }
        public string Remarks { get; set; }
        public Guid EmployeeGI { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string PrinicipalAmt { get; set; }
        public string PremiumAmt { get; set; }
        public string StartDateHead { get; set; }
        public string MaturityDateHead { get; set; }
        public List<InsuranceDetailsEmployeeList> InsuranceDetailsEmployeeList { get; set; }
        public List<EmployeeVsInsuranceDetails> EmployeeVsInsuranceDetailsList { get; set; }
        public Filter Filter { get; set; }
        public File Excel { get; set; }
        public string Message { get; set; }

    }
    public class InsuranceDetailsEmployeeList
    {
        public string ID { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public int NoOfInsurance { get; set; }
        public string Edit { get; set; }
        public int EmployeeCode { get; set; }
        public Guid EmployeeGUID { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }       
    }
}
