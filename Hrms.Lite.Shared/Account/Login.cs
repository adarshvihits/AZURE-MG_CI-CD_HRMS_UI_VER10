using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.Account
{
    public class Login
    {
        public AuthUser authUser { get; set; }
        public MenuEntity MenuEntity { get; set; }
        public int GrpCompanyCode { get; set; }
        public string CompanyCode { get; set; }
        public string UserCode { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public string CompanyName { get; set; }
        public string MACAddress { get; set; }
        public string IPAddress { get; set; }
        public string INOUT { get; set; }
        public string software { get; set; }
        public int GroupCompanyCode { get; set; }
        public int Captcha { get; set; }
        public string LoginStatus { get; set; }
        public bool LoginSuccess { get; set; }
        public List<Dropdown> GroupCompanyDrpdown { get; set; }
        public SoftwareURLs SoftwareURLs { get; set; }
        public string GrpChange { get; set; }
        public string ProductGroup { get; set; }
        public int isRedirected { get; set; }
    }
  
    public class CustomSoftware
    {
        public File SoftwareLogo { get; set; }
        public File SoftwareName { get; set; } //ESSP SoftwareName
        public File HRMS_SoftwareName { get; set; }
         public File AbsoluteUri { get; set; }
        public File CompanyLogoBlobUrl { get; set; }
    }
}
