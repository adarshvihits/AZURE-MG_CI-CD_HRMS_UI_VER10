using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank
{
    public class EmployeeReferenceScheme
    {
        public int EmployeeReferenceCode { get; set; }
        public string Roles { get; set; }
        public decimal BonusAmount { get; set; }
        public decimal FirstPayout { get; set; }
        public decimal SecondPayout { get; set; }
        public decimal AdditionalBonus { get; set; }
        public List<EmployeeReferenceScheme> EmployeeReferenceSchemeList { get; set; }
        public string Mode { get; set; }
        public bool Active { get; set; }

        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
    }
}
