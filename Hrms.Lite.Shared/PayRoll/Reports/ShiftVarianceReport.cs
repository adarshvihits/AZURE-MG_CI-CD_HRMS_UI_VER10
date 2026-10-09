using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Hrms.Lite.Shared.PayRoll.Reports
{
    public class ShiftVarianceReport
    {
        public Filter Filter { get; set; }
        public DataSet ds { get; set; }
        public DateTime ShiftDate { get; set; }
        public List<ShiftVariance> ShiftVarianceList { get; set; }
    }
    public class ShiftVariance
    {
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public string Section { get; set; }
        public string Date { get; set; }
        public string AllocatedShift { get; set; }
        public string ShiftIn { get; set; }
        public string ShiftOut { get; set; }
        public string PunchLog { get; set; }
        public string Status { get; set; }
        public string Day { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public string Authority { get; set; }
    }
}
