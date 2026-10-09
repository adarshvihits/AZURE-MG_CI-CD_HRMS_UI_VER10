
using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.PayRoll.Reports
{
  public  class WageRegister
    {
        public int  EmployeeCode { get; set; }
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public int GroupCompanyId { get; set; }
        public MasterBase PayrollMonth { get; set; }
        public int Designation { get; set; }
        public int Department { get; set; }
        public int Location { get; set; }
        public int Category { get; set; }
        public int Grade { get; set; }
        public int PayrollGroup { get; set; }
        public int Division { get; set; }
        public int Section { get; set; }
        public int EmploymentType { get; set; }
        public int WorkLocation { get; set; }
        public int WageGradeMaster { get; set; }
        public int SalaryStructure { get; set; }
        public int PaymentMode { get; set; }
        public int AttendanceGroup { get; set; }
        public int WageType { get; set; }
        public Url Url { get; set; }

    }
}
