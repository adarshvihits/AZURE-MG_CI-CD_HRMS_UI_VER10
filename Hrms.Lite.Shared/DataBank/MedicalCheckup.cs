using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank
{
    public class MedicalCheckup
    {

        public int DoneCount { get; set; }
        public int PendingCount { get; set; }
       
        public Guid EmployeeGI { get; set; }
        public Guid MedicalCheckupGI { get; set; }
        public File Attachment { get; set; }
        public MasterBase CheckupType { get; set; }
        public DateTime? CheckupDate { get; set; }
        public string HospitalDetails { get; set; }
        public DateTime? CheckupValidity { get; set; }
        public string HealthCardNumber { get; set; }
        public string CheckupDetails { get; set; }
        public string CheckupBy { get; set; }
        public string Remarks { get; set; }
        public List<MedicalCheckupList> MedicalCheckupList { get; set; }
        public EmployeeHeader EmployeeHeader { get; set; }
        public Filter Filter { get; set; }
    }
    public class MedicalCheckupList
    {

        public Guid MedicalCheckupGI { get; set; }
        public Guid EmployeeGI { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public string Gender { get; set; }
        public string Mobile { get; set; }
        public int Age { get; set; }
        public DateTime DOJ { get; set; }
        public DateTime? DateOfCheckup { get; set; }
        public string CheckupType { get; set; }
        public string CheckupStatus { get; set; }
        public DateTime? Validity { get; set; }
        public string HealthCardNo { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
    }
}

