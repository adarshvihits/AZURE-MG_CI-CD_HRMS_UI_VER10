using Hrms.Lite.Shared.Master;
using Hrms.Lite.Shared.PayRoll.Generic.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.PayRoll
{
    public class PaySlipGeneration
    {
        public List<PaySlipGeneration> PaySlipGenerationList { get; set; }

        public PayrollMonth Month { get; set; }
        public PayrollGroup PayGroup { get; set; }  
        public  string Type { get; set; }
        public Guid EmployeeGI { get; set; }
        public int EmployeeCode { get; set; }
        public int CompanyCode { get; set; }       
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Category { get; set; }
        public string Location { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string PayrollGroup { get; set; }
        public int LOP { get; set; }
        public int GrossAllowance { get; set; }
        public int GrossDeduction { get; set; }
        public int Advance { get; set; }
        public int NetPay { get; set; }
        public string PaymentMode { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        //public string PayslipUploadMode { get; set; }
        public string AbsoluteUri { get; set; }
        public string  FileName { get; set; }
    }
    public class PaySlipGeneratedStatus
    {
        public int TotalEmployeeCount { get; set; }
        public int GeneratedEmployeeCount { get; set; }
    }
}
