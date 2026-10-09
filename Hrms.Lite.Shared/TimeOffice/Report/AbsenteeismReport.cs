using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.TimeOffice.Report
{
    public class AbsenteeismReport
    {
        
        public Filter filter { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public AbsenteeismReport_Type Type { get; set; }
        public string PopPupHead { get; set; }
        public string Reason { get; set; }
        public List<AbsenteeismReport_AttendanceStatus> AttendanceStatus { get; set; }
        public List<AbsenteeismReport_Reason> ReasonWise { get; set; }
        public List<AbsenteeismReport_Details> Details { get; set; }
        public Filter Filter { get; set; }
    }
    public class AbsenteeismReport_AttendanceStatus
    {

        public Guid EmployeeGI { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public DateTime DATE { get; set; }
        public int ActualManPower { get; set; }
        public decimal PhyPR { get; set; }
        public decimal OD { get; set; }
        public decimal RA { get; set; }
        public decimal Leave { get; set; }
        public decimal ML { get; set; }
        public decimal ESI { get; set; }
        public decimal OffDay { get; set; }
        public decimal Holiday { get; set; }
        public decimal LOP { get; set; }
        public decimal AB { get; set; }
        public decimal AB_PER { get; set; }
        public int active { get; set; }
        public int totalRecords { get; set; }

    }
    public class AbsenteeismReport_Reason
    {
        public Guid EmployeeGI { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public DateTime DATE { get; set; }
        public int Lin { get; set; }
        public int Eout { get; set; }
        public int MisIn { get; set; }
        public int MisOut { get; set; }
        public int MisInOut { get; set; }
        public int CIDMis { get; set; }
        public int ShiftMis { get; set; }
        public int ShiftNotAllocated { get; set; }
        public int TotHrsNotAchieved { get; set; }
        public int LAP { get; set; }
        public int active { get; set; }
        public int totalRecords { get; set; }


    }
    public class AbsenteeismReport_Details
    {
        public int SlNo { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public DateTime? Date { get; set; }
        public string Shift { get; set; }
        public string IN { get; set; }
        public string Out { get; set; }
        public string AttnStatus { get; set; }
        public string Reason { get; set; }
        public int active { get; set; }
        public int totalRecords { get; set; }
    }
    public class AbsenteeismReport_Type : MasterBase
    { }
}


