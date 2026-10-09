using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Hrms.Lite.Shared.PayRoll.Generic.Master;
using System;
using System.Collections.Generic;
using System.Text;
using static Hrms.Lite.Shared.PayRoll.Generic.PayrollCommon;

namespace Hrms.Lite.Shared.PayRoll
{
    public class PFSettings
    {
        public EmployeeHeader EmployeeHeader { get; set; }
        public PFDeduction PFDeduction { get; set; }
        public PayrollMonth PayrollMonth { get; set; }
        public PFGroup PFGroup { get; set; }
        public List<PFSettingsDetails> PFSettingsList { get; set; }
        public PFSettingsDetails PFSettingsDetails { get; set; }
        public List<PFGeneralSettings> PFGeneralSettings { get; set; }
        public List<PFApplicableAllowance> PFApplicableAllowance { get; set; }
        public Filter Filter { get; set; }
    }
    public class PFSettingsDetails
    {
        public Guid EmployeeGI { get; set; }
        public int EmployeeCode { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Location { get; set; }
        public DateTime DOJ { get; set; }
        public PFGroup PFGroup { get; set; }
        public string PFNo { get; set; }
        public string UANNo { get; set; }
        public decimal EPFPercentage { get; set; }
        public decimal ERPFPercentage { get; set; }
        public decimal EPSPercentage { get; set; }
        public decimal VPFPercentage { get; set; }
        public decimal VPFAmount { get; set; }
        public decimal MinLimit { get; set; }
        public decimal EmpMaxLimit { get; set; }
        public decimal EmployerMaxLimit { get; set; }
        public string Remarks { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }

        public decimal Gen_EPF { get; set; }
        public decimal Gen_ERPF { get; set; }
        public decimal Gen_EPS { get; set; }
        public decimal Gen_MinLimit { get; set; }
        public decimal Gen_EmpMaxLimit { get; set; }
        public decimal Gen_EmployerMaxLimit { get; set; }
    }
    public class PFGeneralSettings
    {
        public int ParameterCode { get; set; }
        public string ParameterName { get; set; }
        public decimal Value { get; set; }
        public bool Applicability { get; set; }
    }
    public class PFApplicableAllowance
    {
        public int AllowanceCode { get; set; }
        public string AllowanceName { get; set; }
        public bool YN { get; set; }
        public decimal? Applicable { get; set; }
        public PFGroup PFGroup { get; set; }
    }
}
