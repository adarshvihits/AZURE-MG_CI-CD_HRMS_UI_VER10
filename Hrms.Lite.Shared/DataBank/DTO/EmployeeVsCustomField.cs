using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.DTO
{
    public class EmployeeVsCustomField
    {
        public CustomField CustomField { get; set; }
        public string HeaderName { get; set; }
        public string CustomFieldValue { get; set; }
        public Guid EmployeeGI { get; set; }        
        public List<EmployeeVsCustomFieldList> EmployeeVsCustomFieldList { get; set; }
        public Filter Filter { get; set; }
        public File Excel { get; set; }
        public string Message { get; set; }
    }
    public class EmployeeVsCustomFieldList
    {
        public Guid EmployeeGuid { get; set; }
        public string EmployeeID { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public int CustomFieldCode { get; set; }
        public string CustomFieldName { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public string Remarks { get; set; }
    }
    public class CustomField
    {
        public int CustomFieldCode { get; set; }
        public string CustomFieldName { get; set; }
        public string EmployeeStatus { get; set; }
        public Guid CustomFieldGI { get; set; }
        public int SlNo { get; set; }
        public List<CustomField> CustomFieldList { get; set; }
    }
}
