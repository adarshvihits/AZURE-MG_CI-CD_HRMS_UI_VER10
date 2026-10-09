using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Hrms.Lite.Shared.PayRoll.Generic.Master;
using Hrms.Lite.Shared.TimeOffice.Generic.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.PayRoll.Reports
{
   public class ArrearProcessingReport
    {
  

            public PayrollMonth Month { get; set; }
            public Dropdown PayrollGroup { get; set; }
            public Filter Filter { get; set; }
            public string EmployeeID { get; set; }
            public string EmployeeName { get; set; }
            public int EmployeeCode { get; set; }
            public Guid EmployeeGI { get; set; }
            public string Designation { get; set; }
            public string Department { get; set; }
            public string Location { get; set; }
            public string WithEffectFrom { get; set; }
            public int TotalDays { get; set; }
            public decimal Lop { get; set; }
            public decimal PresentDays { get; set; }
            public decimal Amount { get; set; }
            public decimal PF { get; set; }
            public decimal ESI { get; set; }
            public int totalRecords { get; set; }
            public int active { get; set; }

            public List<ArrearProcessingReport> ArrearProcessingList { get; set; }
            public List<ArrearProcessingDetails> ArearProcessingDetails { get; set; }
            public List<ArrearProcessingPFDetails> ArearProcessingPFDetails { get; set; }
            public List<ArearProcessingESIDetails> ArearProcessingESIDetails { get; set; }
        }
        public class ArrearProcessingDetails
        {
            public int SalaryPeriodCode { get; set; }
            public string SalaryPeriod { get; set; }
            public string Allowance { get; set; }
            public decimal NewAmt { get; set; }
            public decimal OldAmt { get; set; }
            public decimal prevArrear { get; set; }
            public int tDays { get; set; }
            public decimal AmtPerDay { get; set; }
            public int LOP { get; set; }
            public decimal Arrear { get; set; }
            public int preEmp { get; set; }
            public string empDetail { get; set; }
            public decimal totMpArrear { get; set; }
            public int PayDays { get; set; }

        }
        public class ArrearProcessingPFDetails
        {
            public int SalaryPeriodCode { get; set; }
            public string Month { get; set; }

            public decimal newSubSal { get; set; }
            public decimal oldSubSal { get; set; }
            public decimal empPf { get; set; }
            public decimal mgmtPf { get; set; }
            public decimal mgmtFPf { get; set; }
            public int preEmpCode { get; set; }
            public int prevArrear { get; set; }

        }
        public class ArrearProcessingESIDetails
        {
            public int SalaryPeriodCode { get; set; }
            public string Month { get; set; }

            public decimal newSubSal { get; set; }
            public decimal oldSubSal { get; set; }
            public decimal EmpESI { get; set; }
            public decimal MgmtESI { get; set; }
            public int preEmp { get; set; }
            public decimal prevArrear { get; set; }

        }
    }

