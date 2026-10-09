using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.Reports
{
   public class SocialWallReport
    {
        public EmployeeHeader EmployeeHeader { get; set; }
        public int SocialWallCode { get; set; }
        public string Content { get; set; }
        public string Name { get; set; }
        public string EntryUser { get; set; }
        public string EntryDate { get; set; }
        public int Likes { get; set; }
        public int Comment { get; set; }
        public int MonthCode { get; set; }
        public List<SocialWallReport> SocialWallReportList { get; set; }
        public List<EmployeeDetails> EmployeeDetailsList { get; set; }
        public List<CommentEmployeeDetails> CommentEmployeeDetailsList { get; set; }
        public EmployeeDetails employeeDetails { get; set; }
     

    }
    public class EmployeeDetails
    {
        public int SocialWallCode { get; set; }
        public int EmployeeCode { get; set; }
        public string EmployeeID { get; set; }
        public string FullName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public string AbsoluteUrl { get; set; }
        public string FileName { get; set; }
        public string Base64string { get; set; }

    }
    public class CommentEmployeeDetails
    {
        public int SocialWallCode { get; set; }
        public string EmployeeComment { get; set; }
       public int EmployeeCode { get; set; }
        public string EmployeeID { get; set; }
        public string FullName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public string AbsoluteUrl { get; set; }
        public string FileName { get; set; }
        public string Base64string { get; set; }

    }
}

