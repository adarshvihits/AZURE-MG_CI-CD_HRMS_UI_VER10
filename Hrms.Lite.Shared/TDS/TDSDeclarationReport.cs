using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.TDS
{
  public  class TDSDeclarationReport
    {
      
        public Filter Filter { get; set; }
        public MasterBase AssessmentYear { get; set; }
        public List<TDSDeclarationDetails> TDSDeclarationDetails { get; set; }
        public List<HouseRental> HouseRental { get; set; }
        public List<LandLord> LandLord { get; set; }
        public List<Deduction> Deduction { get; set; }
        public List<LIC> LIC { get; set; }


    }
    public class TDSDeclarationDetails
    {
        public string Declaration { get; set; }
        public int TotalEmployees { get; set; }
        public int CategoryCode { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }



    }
    public class HouseRental
    {
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Location { get; set; }
        public string City { get; set; }
        public string Metro { get; set; }
        public string OtherCity { get; set; }


    }
    public class LandLord
    {
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Location { get; set; }
        public string LandLordName { get; set; }
        public string LandlordAddress { get; set; }
        public string LandlordPAN { get; set; }


    }
    public class Deduction
    {
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Location { get; set; }
        public string DeductionName { get; set; }
        public string Amount { get; set; }
        
    }
    public class LIC
    {
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Location { get; set; }
        public string PolicyNo { get; set; }
        public string Assured { get; set; }

    }
    public class ChapterVIA
    {
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Location { get; set; }
        public decimal PPS { get; set; }
        public decimal NPS { get; set; }

    }

}
