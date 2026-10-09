using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.DTO
{
    public class CompanyDocumentUpload
    {
      public string Type { get; set; }
      public Dropdown DocumentType { get; set; }
      public Dropdown CompanyDocument { get; set; }
      public Filter Filter { get; set; }

      public List<EmployeeList> EmployeeList { get; set; }
      public List<Popup> DocumentList { get; set; }
      public Guid EmployeeGI { get; set; }     
      public EmployeeHeader EmployeeHeader { get; set; }
      public File File { get; set; }
     
    }

    public class EmployeeList
    {
        public Guid EmployeeGI { get; set; }
     
        public string EmployeeID { get; set; }
        public string FullName { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public string Location { get; set; }
        public DateTime DOJ { get; set; } 
        public int totalRecords { get; set; }
        public int active { get; set; }      
        public string Add { get; set; }
        public int Count { get; set; }

    }

    public class Popup
    {
        public Dropdown DocumentType { get; set; }
        public Dropdown CompanyDocument { get; set; }
        public string DocumentFile { get; set; }
        public string FileName { get; set; }
        public byte[] DocData { get; set; }
        public string Base64DocData { get; set; }
        public int TrnCode { get; set; }
        public int EmpCode { get; set; }

    }

}
