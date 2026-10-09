using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.TimeOffice
{
    public class OndutyWfhSettings
    {

        public LeaveGroup LeaveGroup { get; set; }
        public Filter Filter { get; set; }
        public bool OdRemarksMandatory { get; set; }
        public bool OdPreDatedApplAlwed { get; set; }
        public int? OdNoOfDaysPrior { get; set; }
        public bool OdAdvanceApplRequired { get; set; }
        public int? OdNoOfDaysAdvance { get; set; }
        public bool WfhRemarksMandatory { get; set; }
        public bool WfhPreDatedApplAlwed { get; set; }
        public int? WfhNoOfDaysPrior { get; set; }
        public bool WfhAdvanceApplRequired { get; set; }
        public int? WfhNoOfDaysAdvance { get; set; }
        public UserBase User { get; set; }
        public int EmployeeCode { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Remark { get; set; }
        public Guid EmployeeGI { get; set; }

        public Designation Designation { get; set; }
        public Department Department { get; set; }
        public Location Location { get; set; }
        public File Excel { get; set; }
        public decimal? OdLimit { get; set; }
        public string OdLimitStr { get; set; }
        public string WfhLimitStr { get; set; }
        public decimal? WfhLimit { get; set; }
        public int totalRecords { get; set; }
        public int activeRow { get; set; }
        public int Action { get; set; }
        public string Message { get; set; }
        public List<OndutyWfhSettings> OndutyWfhSettingsList { get; set; }

    }

}
