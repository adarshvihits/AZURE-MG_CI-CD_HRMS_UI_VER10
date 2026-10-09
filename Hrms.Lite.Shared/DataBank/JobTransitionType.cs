using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank
{
    public class JobTransitionType
    {
        public int SlNo { get; set; }
        public string JobTransitionCategory { get; set; }
        public int JobTransitionCode { get; set; }
        public String JobTransitionTypeA { get; set; }
        public int JobTransitionTypeCode { get; set; }
        public int GroupCompanyCode { get; set; }
        public Dropdown JobTransitionTypeDropdown { get; set; }
        public UserBase User { get; set; }

        public Dropdown GroupCompany { get; set; }

        public List<JobTransitionType> JobTransitionTypeList { get; set; }
    }
}
