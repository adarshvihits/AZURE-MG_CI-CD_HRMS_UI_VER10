using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.Reports
{
    public class RenewalInformation
    {
        public int AllTabCount { get; set; }
        public int Tab1Count { get; set; }
        public int Tab2Count { get; set; }
        public int Tab3Count { get; set; }
        public int Tab4Count { get; set; }
        public int MoreCount { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public List<RenewalInformation_Tabs> RenewalInformationTabs { get; set; }
        public List<RenewalInformation_List> RenewalInformationList { get; set; }
        public List<EmployeeProfileSummary> EmployeeProfileSummary { get; set; }
        public RenewalInfoTab RenewalInfoTab { get; set; }
        public int test { get; set; }
        public Filter Filter { get; set; }
    }

    public class RenewalInformation_Tabs
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }

    }
    public class RenewalInformation_List
    {
        public Guid EmployeeGI { get; set; }
        public string Employeeid { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public string Category { get; set; }
        public DateTime DOJ { get; set; }
        public string YOS { get; set; }

        public DateTime DateofExpiry { get; set; }
        public Decimal GrossPay { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
    }
    public class RenewalInfoTab:MasterBase{

    }
}
