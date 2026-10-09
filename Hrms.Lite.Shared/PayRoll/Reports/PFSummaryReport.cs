using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.PayRoll.Reports
{
    public class PFSummaryReport
    {
       
        public int Month { get; set; }
        public int PFGroup { get; set; }
        public string Type { get; set; }
        public List<PFSummary> PFSummary { get; set; }
        public Filter SliderFilter { get; set; }
        public UserBase_Code UserBase_Code { get; set; }
        public KendoReport Report { get; set; }

        public decimal ActualPFSalarySUM { get; set; }
        public decimal PFSalarySUM { get; set; }
        public decimal EmployeeContributionSUM { get; set; }
        public decimal PensionFundSUM { get; set; }
        public decimal PFSUM { get; set; }
        public decimal EDLIWageSUM { get; set; }
        public decimal EPFWageSUM { get; set; }
        public decimal AdminChargeSUM { get; set; }
        public decimal EDLIChargeSUM { get; set; }
    }
    public class PFSummary
    {
        public int SlNo { get; set; }
        public Guid EmployeeGI { get; set; }
        public int EmployeeCode { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Location { get; set; }
        public string Division { get; set; }
        public string PFNumber { get; set; }
        public string UAN { get; set; }
        public decimal ActualPFSalary { get; set; }
        public decimal PFSalary { get; set; }
        public decimal EmployeeContribution { get; set; }
        public decimal PensionFund { get; set; }
        public decimal PF { get; set; }
        public decimal EDLIWage { get; set; }
        public decimal EPFWage { get; set; }
        public decimal AdminCharge { get; set; }
        public decimal EDLICharge { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
    }

    public class UserBase_Code
    {
        public int Company_Code { get; set; }
        public int User_Code { get; set; }
    }

}
