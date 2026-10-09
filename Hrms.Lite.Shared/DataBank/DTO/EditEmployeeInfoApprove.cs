using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.DTO
{
    public class EditEmployeeInfoApprove
    {
        public FilterSlabComboFill FilterCombo { get; set; }
        public Filter filter { get; set; }
        public List<EditInfoApproveList> EditInfoList { get; set; }

        public EmployeeHeader EmployeeHeader { get; set; }
        public Guid EmployeeGI { get; set; }
        public Guid LogGI { get; set; }

        public string Remarks { get; set; }
        public string EntryUser { get; set; }
        public DateTime EntryDateAndTime { get; set; }

        public string ApproveMode { get; set; }

        public UserBase User { get; set; }


        public List<EditEmpInfoDetailsList> EditEmpDetailsList { get; set; }


        public class EditInfoApproveList
        {
            public Guid EmployeeGI { get; set; }
            public Guid LogGI { get; set; }
            public string EmployeeID { get; set; }
            public string FullName { get; set; }
            public string Department { get; set; }
            public string Designation { get; set; }
            public string Location { get; set; }
            public DateTime ExpectedDOJ { get; set; }
            public DateTime EditDate { get; set; }
            public bool MakerChecker { get; set; }
            public int totalRecords { get; set; }
            public int active { get; set; }
        }

        public class EditEmpInfoDetailsList
        {
            public string Name { get; set; }
            public string Old { get; set; }
            public string New { get; set; }
        }
    }
}
