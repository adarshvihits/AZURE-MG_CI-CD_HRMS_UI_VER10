using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.DTO
{
    public class BranchGrading
    {
   
        public MasterBase Designation { get; set; }
   
        public MasterBase Region { get; set; }
        public MasterBase Grade { get; set; }
        public Change_BranchGrade Change_BranchGrade { get; set; }
        public List<BranchGrading_MainList> MainList { get; set; }
        public List<BranchGrading_GradeList> GradeList { get; set; }
        public List<BranchGrading_EmpDetails> EmpDetails { get; set; }

    }
    public class BranchGrading_MainList
    {
        public MasterBase Location { get; set; }
        public MasterBase Grade { get; set; }
        public MasterBase Region { get; set; }
        public int StdNumbers { get; set; }
        public int CurrentNumbers { get; set; }
        public int VarianceCount { get; set; }
        public int StdGP { get; set; }
        public int GP_PLUS { get; set; }
        public int GP_NEG { get; set; }
        public int MRF { get; set; }
    }
    public class BranchGrading_GradeList
    {
        public MasterBase Designation { get; set; }
        public int Numbers { get; set; }
        public int GrossPay { get; set; }
    }
    public class Change_BranchGrade
    {
        public MasterBase Location { get; set; }
        public MasterBase OldGrade { get; set; }
        public MasterBase NewGrade { get; set; }
    }
    public class BranchGrading_EmpDetails
    {
        public int EmployeeCode { get; set; }
        public string EmployeeID { get; set; }
        public string FullName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public string YearOfService { get; set; }
        public int StdGrossPay { get; set; }
        public int GrossPay { get; set; }
        public int Variance { get; set; }
    }
}
