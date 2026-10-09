using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using Hrms.Lite.Shared.General;

namespace Hrms.Lite.Shared.DataBank.Generic
{
    public class EmployeeClassification
    {
        public Location Location { get; set; }
        public Department Department { get; set; }
        public Category Category { get; set; }
        public MasterBase Company { get; set; }
        public int CategoryCode { get; set; }
        public int CategoryTypeCode { get; set; }
        public Grade Grade { get; set; }
        public Designation Designation { get; set; }
        public PayrollGroup PayrollGroup { get; set; }
        public VDAGroup VDAGroup { get; set; }
        public int NoticePeriod { get; set; }
        public string RenewalDate { get; set; }
        public string RetirementDate { get; set; }
        
        public Division Division { get; set; }
        public Section Section { get; set; }
        public EmploymentType EmploymentType { get; set; }
        public WorkLocation WorkLocation { get; set; }
        public WageGradeMaster WageGrade { get; set; }
        public MasterBase SubDepartment { get; set; }
        public MasterBase Function { get; set; }
        public MasterBase IncenticeApplicable { get; set; }
        public IncentiveCostCenter IncentiveCostCenter { get; set; }
    }
}
