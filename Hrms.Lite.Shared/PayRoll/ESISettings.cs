using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.PayRoll
{
    public class ESISettings
    {       
        public MasterBase ESIDeduction { get; set; }
        public MasterBase PayrollMonth { get; set; }
        public List<ESISettingsDetails> ESISettingsList { get; set; }
        public ESISettingsDetails ESISettingsDetails { get; set; }
        public List<ESIGeneralSettings> ESIGeneralSettings { get; set; }
        public List<ESIApplicableAllowance> ESIApplicableAllowance { get; set; }
        public EmployeeHeader EmployeeHeader { get; set; }
        public Filter Filter { get; set; }
    }
    public class ESISettingsDetails
    {
        public Guid EmployeeGI { get; set; }
        public int EmployeeCode { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Location { get; set; }
        public string Designation { get; set; }
        public DateTime DOJ { get; set; }
        public ESIGroup ESIGroup { get; set; }
        public string ESINo { get; set; }
        public decimal EESI { get; set; }
        public decimal MESI { get; set; }
        public decimal MinLimit { get; set; }
        public decimal EmpMaxLimit { get; set; }


        public decimal Gen_EESI { get; set; }
        public decimal Gen_MESI { get; set; }
        public decimal Gen_MinLimit { get; set; }
        public decimal Gen_EmpMaxLimit { get; set; }


        public string Remarks { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public string FileName { get; set; }

    }
    public class ESIGeneralSettings
    {
        public int ParameterCode { get; set; }
        public string ParameterName { get; set; }
        public decimal Value { get; set; }
        public bool Applicability { get; set; }
    }
    public class ESIApplicableAllowance
    {
        public int AllowanceCode { get; set; }
        public string AllowanceName { get; set; }
        public bool YN { get; set; }
        public decimal? Applicable { get; set; }
    }
}
