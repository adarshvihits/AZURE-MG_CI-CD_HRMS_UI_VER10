using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.DTO
{
    public class AuthUser
    {
        public UserBase User { get; set; }
        public string CompanyName { get; set; }
        public int CompanyID { get; set; }
        public int UserCode { get; set; }
        public string AvatarLetter { get; set; }
        public Guid EmployeeGI { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string ImageUrl { get; set; }
        public string ImageFileName { get; set; }
        public string Base64string { get; set; }
        [IgnoreDataMember]
        public string PasswordHash { get; set; }
        [IgnoreDataMember]
        public string PasswordSalt { get; set; }
        public string Token { get; set; }
        public string UserType { get; set; }
        public bool IsActive { get; set; }
        public string UserRole { get; set; }
        public int UserRoleCode { get; set; }
        public bool IsNewLogin { get; set; }
        public string DashBoardRoute { get; set; }
        public string DashBoard { get; set; }
        public string GroupCompanyShortName { get; set; }
        public string GroupCompanyName { get; set; }
        public int SalaryVisibility { get; set; }
        public bool IsPasswordExpired { get; set; }
        public int IsEmployeeSearchApplicable { get; set; }
        
    }
}
