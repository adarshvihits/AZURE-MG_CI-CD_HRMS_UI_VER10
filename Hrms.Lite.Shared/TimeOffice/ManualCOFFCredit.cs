using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Hrms.Lite.Shared.TimeOffice.Generic.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.TimeOffice
{

    public class ManualCOFFCredit
    {
        public UserBase User { get; set; }
        public EmployeeHeader EmployeeHeader { get; set; }
        public int EmployeeCode { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public Guid EmployeeGI { get; set; }
        public Designation Designation { get; set; }
        public Department Department { get; set; }
        public Location Location { get; set; }
        public Category Category { get; set; }
        public LeaveGroup LeaveGroup { get; set; }
        public DateTime DOJ { get; set; }
        public DateTime? EntitledDate { get; set; }
        public string EntitledDate_string { get; set; }
        public MasterBase AttendanceDayStatus { get; set; }
        public decimal Balance { get; set; }
        public decimal AppliedLeave { get; set; }
        public decimal Value { get; set; }
        public List<ManualCOFFCredit> ManualCOFFCreditList { get; set; }
        public List<ManualCOFFCreditLogDetails> ManualCOFFCreditLogDetailsList { get; set; }

        public Filter Filter { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public string UserName { get; set; }
        public string EntryFrom { get; set; }
        public DateTime EntryDate { get; set; }
        public string Remarks { get; set; }
        public decimal? Count { get; set; }
    }


    public class ManualCOFFCreditLogDetails
    {
        public int SlNo { get; set; }
        public DateTime LogDATE { get; set; }
        public string AppStatus { get; set; }
        public decimal Opening { get; set; }
        public string Remarks { get; set; }

    }

}

