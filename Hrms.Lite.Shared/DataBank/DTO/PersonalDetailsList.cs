using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.DTO
{
    public class PersonalDetailsList
    {
        public Guid EmployeeGI { get; set; }
        public String Employeeid { get; set; }
        public String EmployeeName { get; set; }
        public String Designation { get; set; }
        public String Department { get; set; }
        public String Location { get; set; }
        public DateTime DOJ { get; set; }
        public bool Addr { get; set; }
        public bool Cont { get; set; }
        public bool Pers { get; set; }
        public bool Fmly { get; set; }
        public bool Educ { get; set; }
        public bool Skills { get; set; }
        public bool Cert { get; set; }
        public bool Prev { get; set; }
        public bool Photo { get; set; }
        public bool EmployeeVsNominee { get; set; }
        public decimal percentage { get; set; }
        public string profilepercentage { get; set; }
        public string Totalprofilepercentage { get; set; }
        public decimal TotalPercentage { get; set; } = 0;
        public int totalRecords { get; set; }
        public int active { get; set; }
        public int Average { get; set; }
        public bool MakerChecker { get; set; }

        public PersonalDetailsFilter PersonalDetailsFilter { get; set; }
        public Filter Filter { get; set; }
        public List<PersonalDetailsList> PersonalDetailsLists { get; set; }

    }
    public class PersonalDetailsFilter:MasterBase{
    }
}
