using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Hrms.Lite.Shared.PayRoll.Generic.Master;
using Hrms.Lite.Shared.TimeOffice.Generic.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.TimeOffice
{
    public class AttendanceWagesConfirmation
    {
        public int PendingCount { get; set; }
        public int ConfirmedCount { get; set; }
        public int EmployeeCode { get; set; }
        public Month Month { get; set; }
        public Dropdown PGroup { get; set; }
        public Filter Filter { get; set; }
        public string TabValues { get; set; }
        public int ConfirmationStatus { get; set; }
        public Guid EmployeeGI { get; set; }
        public PayrollGroup PayrollGroup { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeId { get; set; }
        public string Type { get; set; }
        public string Mode { get; set; }
        public string IDName { get; set; }
        public AttendanceWagesConfirmationDetails Details { get; set; }
        public List<AttendanceWagesConfirmationDetails> ConfirmationList { get; set; }
        public List<AttnDetails> AttnDetails { get; set; }
        public List<LHCDetails> LHCDetails { get; set; }
        public int HolidayCount { get; set; }
        public int OffdayCount { get; set; }
        public int OTCount { get; set; }
        public int LessCount { get; set; }
        public int ShiftCount { get; set; }
        public File Excel { get; set; }
        public string Tab { get; set; }
        public int MonthCode { get; set; }
        public decimal DeductionDays { get; set; }
        public decimal TotalMinToCutEditDays { get; set; }
        public string Remarks { get; set; }
        public Designation Designation { get; set; }
        public Department Department { get; set; }




        public string EmployeeID { get; set; }
        public decimal? MinutesToCut { get; set; }
        public string MinutesToCutstr { get; set; }
    }
    public class AttendanceWagesConfirmationDetails
    {
        public EmployeeHeader EmployeeHeader { get; set; }
        public int Month { get; set; }
        public int EmployeeCode { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public Guid EmployeeGI { get; set; }
        public Designation Designation { get; set; }
        public Department Department { get; set; }
        public Location Location { get; set; }

        public string LOG { get; set; }
        public decimal ActualValue { get; set; }
        public decimal ConfirmedValues { get; set; }
        public string Remarks { get; set; }
        public decimal TotalMinutesToCut { get; set; }
        public decimal LateinMinutes { get; set; }
        public decimal EarlyGoingMin { get; set; }
        public Dropdown Division { get; set; }
        public decimal DeductionDays { get; set; }
        public decimal NoOfDays { get; set; }
        public decimal? MinutesToCut { get; set; }
        public string MinutesToCutstr { get; set; }
        public Filter Filter { get; set; }
        public decimal TotalMinToCutEdit { get; set; }
        public int active { get; set; }
        public int totalRecords { get; set; }

        public int ActHrs { get; set; }
        public int ActMin { get; set; }
        public int ConfHrs { get; set; }
        public int ConfMin { get; set; }
        public decimal Late { get; set; }
        public decimal Early { get; set; }
        public decimal TotalDay { get; set; }
        public decimal Balance { get; set; }
        public decimal BalanceAfterDeduction { get; set; }
        public decimal LateHrDay { get; set; }

        public int Tab { get; set; }
        public int PendingCount { get; set; }
        public int ConfirmedCount { get; set; }
    }
    public class AttnDetails
    {
        public string Date { get; set; }
        public string IN { get; set; }
        public string Out { get; set; }
        public string LateIn { get; set; }
        public string EarlyOut { get; set; }
        public string Total { get; set; }
        
        public string Type { get; set; }
        public string Value { get; set; }
        public ShiftDetails Shift { get; set; }
        public string ACTUALWORKING { get; set; }
        public string ScheduledHours { get; set; }
        public string LessHours { get; set; }

    }
    public class LHCDetails
    {


        public string Date { get; set; }
        public string Day_Name { get; set; }
        public string sftName { get; set; }
        public string StartTime { get; set; }
        public string SHIFTHOURS { get; set; }

        public Decimal CUTTING_TIME { get; set; }
        public Decimal indays { get; set; }

        public ShiftDetails Shift { get; set; }
        public string IN { get; set; }
        public string Out { get; set; }
        public string LateIn { get; set; }
        public string EarlyOut { get; set; }
        public string Total { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public string ACTUALWORKING { get; set; }
        public string ScheduledHours { get; set; }
        public string LessHours { get; set; }

    }
}
