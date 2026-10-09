using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank
{
    public class BulkLeaveApplicablity
    {

        public Dropdown LeaveGroup { get; set; }
        public string HeaderName { get; set; }
        public Dropdown LeaveType { get; set; }
        public int radioSelection { get; set; }
        public UserBase User { get; set; }
        public int LeaveTypeCode { get; set; }
        public int LeaveGroupCode { get; set; }
        public File Excel { get; set; }
        public string Message { get; set; }
        public Filter Filter { get; set; }
        public string TabName { get; set; }

        public List<BulkLeaveApplicablityList> BulkLeaveApplicablityList { get; set; }
    }


    public class BulkLeaveApplicablityList
    {
        public Guid EmployeeGuid { get; set; }
        public string EmployeeID { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public int BulkUploadingCode { get; set; }
        public string LeaveTypeName { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public string Remarks { get; set; }
        public bool Applicable { get; set; }
    }
}
