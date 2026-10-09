using Hrms.Lite.Shared.MIS;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DashBoard
{
    public class DataBankDashBoardNew
    {
        public int OnBoarding { get; set; }
        public int OnNotice { get; set; }
        public int FemaleActiveEmployes { get; set; }
        public int MaleActiveEmployes { get; set; }
        public decimal PersonalInformation { get; set; }
        public int ActiveEmployees { get; set; }
        public decimal Attrition { get; set; }
        public int Joining { get; set; }
        public int Separation { get; set; }
        public int ApplicableEmployees { get; set; }
        public int FemaleApplicableEmployes { get; set; }
        public int MaleApplicableEmployes { get; set; }
        public int Vacancy { get; set; }
        public List<DataBankDashBoardCelebrityList> JoiningCelebrityList { get; set; }
        public List<DataBankDashBoardCelebrityList> WeddingAnniversaryCelebrityList { get; set; }
        public List<DataBankDashBoardCelebrityList> WorkAnniversaryCelebrityList { get; set; }
        public List<DataBankDashBoardCelebrityList> BirthdayCelebrityList { get; set; }
        public List<DataBankDashBoardCelebrityList> UpcomingBirthdayCelebrityList { get; set; }
        public List<DashBoardNotification> DashBoardNotificationList { get; set; }
        public List<DataBankDashBoardDiagram> DataBankDashBoardDiagram { get; set; }
        public List<DataBankDashBoardNotification_popup> Notification_PopUp { get; set; }
    }
    public class DataBankDashBoardCelebrityList
    {

        public string CelebrityType { get; set; }
        public DateTime CelibrityDate { get; set; }
        public string EmpId { get; set; }
        public Guid EmployeeGuid { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public decimal YearOFExperience { get; set; }
        public int Age { get; set; }
        public string Mobile { get; set; }
        public string MailId { get; set; }
        public string Remarks { get; set; }
        public string AbsoluteUri { get; set; }
        public string ProfileFilename { get; set; }
        public string Base64string { get; set; }
    }
    public class DataBankDashBoardDiagram
    {
        public string FullName { get; set; }
        public string Type { get; set; }
        public int MaleCount { get; set; }
        public int FeMaleCount { get; set; }
    }
    public class DataBankDashBoardNotification_popup
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public DateTime DOJ { get; set; }
        public string Type { get; set; }
        public DateTime Date { get; set; }
        public string Certificate { get; set; }
        public DateTime RenewalDate { get; set; }


    }
}
