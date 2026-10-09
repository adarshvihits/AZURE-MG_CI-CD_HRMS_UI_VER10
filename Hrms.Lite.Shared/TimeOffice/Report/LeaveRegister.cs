using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.TimeOffice.Report
{
    public class LeaveRegister
    {
        public List<LeaveRegisterList> LeaveRegisterList { get; set; }
        public LeavePeriod LeavePeriod { get; set; }
        public int EmployeeCode { get; set; }
        public EmployeeHeader EmployeeHeader { get; set; }
        public Filter filter { get; set; }
        public string Type { get; set; }
        public List<LeaveRegisterReport_GridSettings> GridSettings { get; set; }
        public List<EmployeeVsLeaveDetails> LeaveDetails { get; set; }
        public int BasedOn { get; set; }
    }
    public class LeaveRegisterReport_GridSettings
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public bool Applicability { get; set; }
   
    }
    public class LeaveRegisterList
    {

        public int EmployeeCode { get; set; }
        public string EmployeeID { get; set; }
        public Guid EmployeeGI { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public string LEAVE { get; set; }
        public decimal LEVCODE { get; set; }
        public decimal OPENING { get; set; }
        public decimal CF { get; set; }
        public decimal CREDIT { get; set; }
        public decimal DEBIT { get; set; }
        public decimal SURRENDER { get; set; }
        public decimal TAKEN { get; set; }
        public decimal BALANCE { get; set; }
        public decimal APPLIED { get; set; }
        public decimal TOTALBALANCE     { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
    }
    public class EmployeeVsLeaveDetails
    {
        public DateTime LeaveDate { get; set; }
        public string Leave { get; set; }

        public string DayStatus { get; set; }
    }
}
