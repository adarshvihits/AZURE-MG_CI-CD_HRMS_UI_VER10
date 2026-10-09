using System;
using System.Collections.Generic;
using System.Text;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;


namespace Hrms.Lite.Shared.PayRoll
{
    public class ESIExpiryCheck
    {
        public Dropdown Month { get; set; }
        public Filter Filter { get; set; }
        

        public UserBase User { get; set; }
        public List<ESIExpiryCheckDetails> ESIExpiryCheckList { get; set; }
    }

    public class ESIExpiryCheckDetails
    {
        public bool Applicable { get; set; }
        public string ID { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public decimal? GrossSalary { get; set; }
        public int EmployeeCode { get; set; }
        public Guid EmployeeGUID { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
    }
}
