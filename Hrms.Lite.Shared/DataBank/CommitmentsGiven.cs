using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank
{
    public class CommitmentsGiven
    {
        public int ResignationCode { get; set; }
        public Guid ResignationGI { get; set; }
        public int PendingCount { get; set; }
        public int CommitmentsCount { get; set; }
        public int EmployeeCode { get; set; }
        public Guid EmployeeGI { get; set; }
        public string EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public string DOJ { get; set; }

        //public string Category { get; set; }
        //public string LWD { get; set; }
        //public string EntryFrom { get; set; }
        //public string Stage { get; set; }
        public string EmployeeAbsoluteUri { get; set; }
        public List<CommitmentsGiven> CommitmentsGivenList { get; set; }
        public Filter filter { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
        public UserBase User { get; set; }
        public List<CommitmentsGivenPopUpDetails> CommitmentsGivenPopUpDetails { get; set; }
        public int Tabindex { get; set; }
    }
    public class CommitmentsGivenPopUpDetails
    {
        public int SlNo { get; set; }
        public string Commitment { get; set; }
        public string CommitmentType { get; set; }
        public string CommittedDate { get; set; }
        public string Description { get; set; }
    }
}
