using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DashBoard
{
    public class LandingPage
    {
        public int logincount { get; set; }
        public int SalaryVariance { get; set; }
        public int TransferCount { get; set; }
        public int PromotionCount { get; set; }
        public int GrpCompanyCode { get; set; }
        public List<DatabankNotification> DatabankNotificationList { get; set; }
        public List<DatabankNotification> PayrollNotificationList { get; set; }
        public List<DashBoardNotification> TimeOfficeNotificationList { get; set; }
        public List<ApplicableCount> PayrollApplicableList { get; set; }
        public List<LoginPopup> LoginDetails { get; set; }
        public List<JobTransferPopup> JobTransferPopupDetails { get; set; }
        public List<PromotionPopup> PromotionPopupDetails { get; set; }
        public string TileName { get; set; }
        public DateTime fromDate { get; set; } = DateTime.Now;
        public DateTime toDate { get; set; } = DateTime.Now;
        public int hrmsCount { get; set; }
        public int esspCount { get; set; }
        public int mobCount { get; set; }
        public List<LoginDetailDiagram> LoginDetailDiagram { get; set; }

    }
    public class DatabankNotification
    {
        public int Code { get; set; }
        public string Type { get; set; }
        public int count { get; set; }
        public string Action { get; set; }
        public string colour { get; set; }
        public string lightcolour { get; set; }
        public string lightcolourclass { get; set; }
        public string Priority { get; set; }
    }
    public class LoginPopup
    {
        public int UserCode { get; set; }
        public string UserName { get; set; }
        public int EmpCode { get; set; }
        public string EmpName { get; set; }
        public string Software { get; set; }
        public string LastLogin { get; set; }
    }
    public class JobTransferPopup
    {
        public int EmpCode { get; set; }
        public string EmpID { get; set; }
        public string EmpName { get; set; }
        public string OLdLoc { get; set; }
        public string NewLoc { get; set; }
        public string EffDate { get; set; }
    }
    public class PromotionPopup
    {
        public int EmpCode { get; set; }
        public string EmpID { get; set; }
        public string EmpName { get; set; }
        public string OLdDesignation { get; set; }
        public string NewDesignation { get; set; }
        public string EffDate { get; set; }
    }
    public class LoginDetailDiagram
    {
        public string FullName { get; set; }
        public string Type { get; set; }
        public int hrmsCount { get; set; }
        public int esspCount { get; set; }
        public int mobileCount { get; set; }
    }
}
