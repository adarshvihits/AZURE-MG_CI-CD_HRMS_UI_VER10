using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.Admin
{
    public class DocumentsFolder
    {
        public List<DocumentsFolderList> DocumentsFolderList { get; set; }
        public int DocumentCategoryCode { get; set; }
        public Dropdown CategoryDropdown { get; set; }
        public Guid DocumentGI { get; set; }
        public int DocumentCode { get; set; }
        public String DocumentName { get; set; }
        public String Description { get; set; }
        public string Mode { get; set; }
        public string RenewalDate { get; set; }
        public bool RenewalDateApplicable { get; set; }
        public File File { get; set; }
        public List<DocParameterList> ParameterList { get; set; }
        public int TotalDocuments { get; set; }

    }
    public class DocumentsFolderList
    {
        public int SlNo { get; set; }
        public Guid DocumentGI { get; set; }
        public int DocumentCategoryCode { get; set; }
        public String DocumentCategory { get; set; }
        public String DocumentName { get; set; }
        public String Description { get; set; }
        public int DocumentCode { get; set; }
        public string EntryDate { get; set; }
        public string RenewalDate { get; set; }
        public File File { get; set; }
    }
    public class DocParameterList
    {
        public int ParameterCode { get; set; }
        public string ParameterName { get; set; }
        public string Remarks { get; set; }
    }
}
