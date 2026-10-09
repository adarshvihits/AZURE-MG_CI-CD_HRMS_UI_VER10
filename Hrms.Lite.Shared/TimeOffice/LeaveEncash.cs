using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Hrms.Lite.Shared.TimeOffice.Generic.Master;
using System;
using System.Collections.Generic;
using System.Text;
namespace Hrms.Lite.Shared.TimeOffice
{
    public class LeaveEncashment
    {
        public EmployeeHeader EmployeeHeader { get; set; }

        public int EmployeeCode { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public Guid EmployeeGI { get; set; }
        public Designation Designation { get; set; }
        public Department Department { get; set; }
        public Location Location { get; set; }
        public Category Category { get; set; }
        public DateTime DOJ { get; set; }
        public int LDS { get; set; }
        public int ENApllicableSalary { get; set; }
        public int EligibleENAmount { get; set; }
        public decimal Balance { get; set; }
        public decimal Taken { get; set; }
        public decimal MaxLimit { get; set; }
        public decimal Value { get; set; }
        public LeavePeriod LeavePeriod { get; set; }
        public LeaveGroup LeaveGroup { get; set; }
        public LeaveEnCash Leave { get; set; }
        public Month Month { get; set; }
        public LeaveEncashmentBatchUpDate LeaveEncashmentBatchUpDate { get; set; }
        public LeaveEncashmentEditDetils LeaveEncashmentEditDetils { get; set; }
        public List<LeaveEncashment> LeaveEncashmentList { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public static int EsiDeduction { get; set; }
        public static int MonthCode { get; set; }

        public string UserName { get; set; }
        public string EntryFrom { get; set; }
        public DateTime EntryDate { get; set; }

        public int EntryType { get; set; }
        public Filter Filter { get; set; }
        public int PendingCount { get; set; }
        public int ConfirmedCount { get; set; }

        
    }
    public class LeaveEnCash : MasterBase
    {
    }
    public class LeaveEncashmentBatchUpDate
    {


        public decimal Value { get; set; }
        public LeavePeriod LeavePeriod { get; set; }
        public LeaveGroup LeaveGroup { get; set; }
        public Leave Leave { get; set; }
        public Month Month { get; set; }
       
    }
    public class LeaveEncashmentEditDetils
    {
        public Guid EmployeeGI { get; set; }
        public LeavePeriod LeavePeriod { get; set; }
        public LeaveGroup LeaveGroup { get; set; }
        public Leave Leave { get; set; }
        public Month Month { get; set; }

        public decimal Opening { get; set; }
        public decimal Credit { get; set; }
        public decimal Taken { get; set; }
        public decimal Debit { get; set; }
        public decimal CF { get; set; }
        public decimal Balance { get; set; }
        public decimal MaxLimit { get; set; }
        public decimal EnCashment { get; set; }
        public string Remarks { get; set; }


    }
}






//namespace Hrms.Lite.Shared.TimeOffice
//{
//    public class LeaveEncash
//    {
//        public LeavePeriod LeavePeriod { get; set; }
//        public LeaveGroup LeaveGroup { get; set; }
//        public Leave Leave { get; set; }
//        public Month Month { get; set; }
//        public LeaveEncashBatchUpDate LeaveEncashBatchUpDate { get; set; }
//    }

//    public class LeaveEncashBatchUpDate
//    {
//        public decimal Encashment { get; set; }
//        public LeavePeriod LeavePeriod { get; set; }
//        public LeaveGroup LeaveGroup { get; set; }
//        public Leave Leave { get; set; }
//        public Month Month { get; set; }
//    }

//}
