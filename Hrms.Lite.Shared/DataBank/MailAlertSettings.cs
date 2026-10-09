using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank
{
    public class MailAlertSettings
    {
        public int TransactionTypeCode { get; set; }
        public int ActiveCount { get; set; }

        public Dropdown TransactionType { get; set; }
        public Filter Filter { get; set; }
        public List<MailAlertSettingsList> MailAlertSettingsList { get; set; }
        public List<MailAlertSettingsUncheckList> MailAlertSettingsUncheckList { get; set; }
    }
    public class MailAlertSettingsList
    {
        public int EmployeeCode { get; set; }
        public string EmployeeId { get; set; }
        public Guid EmployeeGI { get; set; }
        public string Name { get; set; }
        public string MailId { get; set; }
        public int active { get; set; }
        public int totalRecords { get; set; }
        public bool Applicable { get; set; }
        public int TransactionTypeCode { get; set; }
    }
    public class MailAlertSettingsUncheckList
    {
        public int EmployeeCode { get; set; }
       
    }
}
