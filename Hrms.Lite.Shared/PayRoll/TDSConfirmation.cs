using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Hrms.Lite.Shared.PayRoll.Generic.Master;
using System;
using System.Collections.Generic;
using System.Text;


namespace Hrms.Lite.Shared.PayRoll
{
    public class TDSConfirmation
    {
        public UserBase User { get; set; }
        public int EmployeeCode { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public Guid EmployeeGI { get; set; }
        public Deduction Deduction { get; set; }
        public PayrollMonth TDSMonth { get; set; }
        public Designation Designation { get; set; }
        public Department Department { get; set; }
        public Location Location { get; set; }
        public decimal Amount { get; set; }
        public string AmountStr { get; set; }
        public string Message { get; set; }
        public List<TDSConfirmation> TDSConfirmationList { get; set; }
        public File Excel { get; set; }
        public Filter Filter { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public int EmployeeswithAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public string Remark { get; set; }
    }
    public class TDSVsAllowanceSettings
    {
        public UserBase User { get; set; }
        public List<TDSVsAllowanceSettings> settings { get; set; }
        public int AllowanceCode { get; set; }
        public string AllowanceName { get; set; }
        public bool YN { get; set; }
        public Dropdown ComponentType { get; set; }
    }

}
