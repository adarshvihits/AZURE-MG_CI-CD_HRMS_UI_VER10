using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.Master
{
    public class DueClearanceSettings
    {
        public string Mode { get; set; }
        public int SlNo { get; set; }
        public Guid ParameterGI { get; set; }
        public int ParameterCode { get; set; }
        public string ParameterName { get; set; }
        public Department Department { get; set; }
        public MasterBase ResponsiblePerson { get; set; }
        public List<Dropdown> ResponsiblePersonDropdown { get; set; }
     
        public int[] ResponsiblePersons { get; set; }
        public bool DefaultSettings { get; set; }
        public List<DueClearanceSettings> List { get; set; }
    }
}
