using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.DTO
{
    public class EmployeeVsBonusGroup
    {
        public List<EmployeeVsBonusDetails> EmployeeList { get; set; }
        public EmployeeVsBonusDetails EmployeeVsBonusDetails { get; set; }
        public Filter Filter { get; set; }
        public int TabIndex { get; set; }       
    }

    public class EmployeeVsBonusDetails
    {

        public string ID { get; set; }
        public Guid EmployeeGUID { get; set; }
        public int EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public int BonusGroupCode { get; set; }
        public string BonusGroup { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }

    }

}
