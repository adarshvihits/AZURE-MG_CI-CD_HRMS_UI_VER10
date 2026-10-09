using Hrms.Lite.Shared.DataBank.Generic;
using Hrms.Lite.Shared.DataBank.Generic.PersonalDetails;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.DTO
{
  public  class CompanyProfile
    {
        public UserBase User { get; set; }
        public string SearchType { get; set; }
        public string SearchTypeName { get; set; }
        public List<CompanyProfileDetails> GroupCompanyDetails { get; set; }
        public List<CompanyProfileDetails> SectionDetails { get; set; }
        public List<CompanyProfileDetails> DesignationDetails { get; set; }
        public List<CompanyProfileDetails> DepartmentDetails { get; set; }
        public List<CompanyProfileDetails> RegionDetails { get; set; }
        public List<CompanyProfileDetails> LocationDetails { get; set; }
        public List<CompanyProfileDetails> CategoryDetails { get; set; }
        public List<CompanyProfileDetails> GradeDetails { get; set; }
        public List<CompanyProfileDetails> PayrollGroupDetails { get; set; }
        public List<CompanyProfileDetails> SalaryBandDetails { get; set; }
        public List<CompanyProfileDetails> AgeBandDetails { get; set; }
        public List<CompanyProfileDetails> GenderDetails { get; set; }
        public List<CompanyProfileDetails> ServiceBandDetails { get; set; }
        public List<CompanyProfileDetails> QualificationDetails { get; set; }
        public List<CompanyProfileDetailedView> CompanyProfileDetailedView { get; set; }
        public List<CompanyProfileDetails> DivisionDetails { get; set; }
        public int GrpCmpCode { get; set; }
        public Filter Filter { get; set; }
    }
    public class CompanyProfileDetails
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public int GP { get; set; }
    }
    public class CompanyProfileDetailedView
    {
        public Guid EmployeeGI { get; set; }
        public int EmployeeCode { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public string Region { get; set; }
        public string DOJ { get; set; }
        public int Count { get; set; }
        public int GP { get; set; }
    }

}
