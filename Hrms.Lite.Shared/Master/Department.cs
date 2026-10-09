using Hrms.Lite.Shared.General;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Hrms.Lite.Shared.Master
{
    public class Department:MasterBase
    {
        //public Guid UniqueID { get; set; }
        //public string Name { get; set; }

        //[JsonIgnore]
        //public IFormFile Logo { get; set; }
        //public int salutation { get; set; }
        public int ActiveCount { get; set; }
        public int InactiveCount { get; set; }
        public int PendingCount { get; set; }
        public List<Department> DepartmentList { get; set; }
        public SelectList DropdownData { get; set; }
        public List<DepartmentVsSectionBT> DepartmentVsSectionBT { get; set; }
    }
    public class DepartmentVsSectionBT
    {
        public int SectionCode { get; set; }
        public string SectionName { get; set; }
        public int BusinessTypeCode { get; set; }
        public string BusinessTypeName { get; set; }
        public bool Applicable { get; set; }
    }
}
