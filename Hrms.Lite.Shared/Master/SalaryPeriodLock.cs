using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.Master
{
    public class SalaryPeriodLock : MasterBase
    {
     
        public Guid MonthGI { get; set; }
  
        public Guid PayrollGroupGI { get; set; }
        public bool Attendance { get; set; }
        public bool Lock { get; set; }
        public string LockType { get; set; }
        public bool salaryProcessing { get; set; }
        public bool Report { get; set; }
        public bool Payslip { get; set; }
        public int MonthCode { get; set; }
        public int PGCode { get; set; }
        public string Remarks { get; set; }
        public string AttendanceProcessingLockStatus { get; set; }
        public string SalaryProcessingLockStatus { get; set; }
        public List<SalaryPeriodLock> SalaryPeriodLockList { get; set; }
        public List<SalaryPeriodLockUnlock> LockUnlock { get; set; }
        public List<SalaryPeriodLockUnlock> OpenMonth { get; set; }
    }
    public class SalaryPeriodLockUnlock
    {
        public string MonthName { get; set; }
        public string PGName { get; set; }
        public string LockType { get; set; }
        public string Lock { get; set; }
        public string User { get; set; }
        public DateTime Date { get; set; }
        public string Reason { get; set; }

    }
}
