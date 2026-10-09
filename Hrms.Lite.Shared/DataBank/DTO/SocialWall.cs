using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.DTO
{
    public class SocialWall
    {
        public EmployeeHeader EmployeeHeader { get; set; }
        public Guid SocialWallGI { get; set; }
        public Guid EmployeeGI { get; set; }
        public int EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string EmployeeID { get; set; }
        public DateTime UploadDate { get; set; }
        public DateTime ApprovedDate { get; set; }
        public File file { get; set; }
        public string PostTime { get; set; }
        public List<SocialWall> SocialWallList { get; set; }
        public int ActiveCount { get; set; }
        public int PendingCount { get; set; }
        public string ApproveMode { get; set; }//Approve,Reject
        public string Remarks { get; set; }
        public string Tab { get; set; }
        public int[] GroupCompany { get; set; }
        public Filter Filter { get; set; }
    }
}
