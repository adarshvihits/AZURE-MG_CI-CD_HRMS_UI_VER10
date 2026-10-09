using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.DTO
{
    public class DataBulkUploading
    {
        public MasterBase BulkUploadingType { get; set; }
        public string HeaderName { get; set; }
        public string BulkUploadingValue { get; set; }
        public Guid EmployeeGI { get; set; }        
        public List<DataBulkUploadingList> DataBulkUploadingList { get; set; }
        public File Excel { get; set; }
        public Filter Filter { get; set; }
        public string Message { get; set; }
        public Dropdown EntryDataType { get; set; }
        public int radioSelection { get; set; }
        public Dropdown uploadItem { get; set; }
        public string TabName { get; set; }
    }
    public class DataBulkUploadingList
    {
        public Guid EmployeeGuid { get; set; }
        public string EmployeeID { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public int BulkUploadingCode { get; set; }
        public string BulkUploadingName { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public string Remarks { get; set; }
                public bool Applicable { get; set; }

    }

}
