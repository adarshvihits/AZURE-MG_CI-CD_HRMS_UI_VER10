using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.Reports
{
    public class PeriodicalEmployeelist
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public Filter filter { get; set; }




        public int EmployeeCode { get; set; }

        public string EmployeeID { get; set; }

        public string EmployeeName{ get; set; }

        public string Designation { get; set; }

        public string Department { get; set; }

        public string Location { get; set; }
        public string YearOfService { get; set; }
        public DateTime? DOJ { get; set; }

        public DateTime? LWD { get; set; }

        public int GrossPay { get; set; }

        public int CTC { get; set; }

        public int totalRecords { get; set; }

        public int active { get; set; }

        public List<PeriodicalEmployeelist> PeriodicalEmployeelistDetails { get; set; }

        public List<PeriodicalEmployeelist_GridSettings> GridSettings { get; set; }
    }

    public class PeriodicalEmployeelist_GridSettings
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public bool Applicability { get; set; }
    }



}
