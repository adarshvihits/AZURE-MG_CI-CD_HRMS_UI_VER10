using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank
{
  public  class RequestToHR
    {
        public int RequestCode { get; set; }
        public Designation Designation { get; set; }
        public Department Department { get; set; }
        public Location Location { get; set; }
        public Category Category { get; set; }
        public Guid EmployeeGI { get; set; }
        public File Attachment { get; set; }
        public int EmployeeCode { get; set; }
        public int ApproverEmployeeCode { get; set; }
        public int radiomode { get; set; }
        public bool radiostatus { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Remarks { get; set; }
        public string EntryDate { get; set; }
        public string ApprovedDate { get; set; }
        public string ApprovalComments { get; set; }
        public string Request { get; set; }
        public string Reply { get; set; }
        public Filter SliderFilter { get; set; }
        public EmployeeHeader EmployeeHeader { get; set; }
        public List<RequestToHR> RequestToHRList { get; set; }
        public List<File> RequestToHRDocList { get; set; }
    }
    public class RequestToHRDoc
    {
        public int Code { get; set; }
        public File Attachment { get; set; }
        public string Name { get; set; }
        public int? Point { get; set; }
    }
}
