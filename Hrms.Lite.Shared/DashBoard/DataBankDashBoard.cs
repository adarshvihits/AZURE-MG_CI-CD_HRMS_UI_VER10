using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DashBoard
{
    public class DataBankDashBoard
    {
        public int ApprovalTracker { get; set; }
        public int NoticePeriodEmployees { get; set; }
        public int InfoMissing { get; set; }
        public int DocumentsMissing { get; set; }
        public int RecentMasterChanges { get; set; }
        public int ActiveEmployees { get; set; }
        public int OnboardingPending { get; set; }
        public int MyApplicability { get; set; }
        public int HRApprovalPending { get; set; }
        public int UpcomingRenewals { get; set; }
        public int OverDueRenewals { get; set; }
        public int Events { get; set; }
        public int Birthday { get; set; }
        public int WeddingAnniversary { get; set; }
        public int WorkAnniversary { get; set; }
        public int Joining { get; set; }
        public int JoiningConfirmation { get; set; }
        public int PreEnrollmentApproval { get; set; }
        public bool ManPower { get; set; }
        public List<DataBankDashBoardList> ManPowerChart { get; set; }
        public int ManPowerTotal { get; set; }
        public List<DataBankDashBoardList> AgeChart { get; set; }         
        public List<DataBankDashBoardList> GenderChart { get; set; }        
        public List<DataBankDashBoardList> YOSChart { get; set; }        
        public int Classification { get; set; }
        public List<DataBankDashBoardList> DataBankDashBoardList { get; set; }
        public List<DataBankDashBoardDetailsList> DataBankDashBoardDetailsList { get; set; }
        public string ListType { get; set; }
    }
    public class DataBankDashBoardList
    {
        public string Type { get; set; }
        public int count { get; set; }
        public string Action { get; set; }
    }
    public class DataBankDashBoardDetailsList
    {
        public string Type { get; set; }
        public int count { get; set; }
        public int EmployeeCode { get; set; }
        public DateTime CelbtnDate { get; set; }
        public string DayName { get; set; }
        public string FullName { get; set; }
        public string EmployeeID { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public string YearOfService { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime ExpiredDate { get; set; }
        public DateTime ResignDate { get; set; }
        public string EmployeeAbsoluteUri { get; set; }
        public DateTime LWD { get; set; }

        public DateTime DOJ { get; set; }
    }
}
