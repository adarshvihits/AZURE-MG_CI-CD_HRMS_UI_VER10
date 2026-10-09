using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.DTO
{
    public class RequestVsAuthority
    {
        public string EmpId { get; set; }
        public string EmployeeName { get; set; }
        public string AuthorityId { get; set; }
        public string AuthorityName { get; set; }
        public Guid EmployeeGI { get; set; }
        public EmployeeHeader EmployeeHeader { get; set; }
        public Designation Designation { get; set; }
        public Department Department { get; set; }
        public Location Location { get; set; }

        public int Tabindex { get; set; }
        public int SearchId { get; set; }
        public int AllTabCount { get; set; }
        public int Assigned { get; set; }
        public int NotAssigned { get; set; }
        public int BulkUpload { get; set; }
        public Guid RequestGI { get; set; }
        public RequestType RequestType { get; set; }
        public AuthorityLevel AuthorityLevel { get; set; }
        public ReportingManager ReportingManager { get; set; }
        public UnassignedList UnassignedList { get; set; }

        public List<RequestVsAuthorityList> RequestVsAuthorityList { get; set; }
        public List<RequesDetails> RequesDetails { get; set; }
        public List<RequestVsAuthorityLogDetails> LogDetails { get; set; }
        public BatchUpdate BatchUpdate { get; set; }
        public AddLevel AddLevel { get; set; }
        public Filter Filter { get; set; }
        public List<AlreadyExists_RequestVsAuthority> AlreadyExists_RequestVsAuthority { get; set; }
        public string Remark { get; set; }
        public List<RequestVsAuthority> UnMatchedList { get; set; }
        public string Message { get; set; }
        public File Excel { get; set; }
    }
    public class RequestVsAuthorityList
    {
        public string EmpId { get; set; }
        public string EmployeeName { get; set; }
        public int EmployeeCode { get; set; }
        public Guid EmployeeGI { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public string Location { get; set; }
        public DateTime DOJ { get; set; }
        public string AuthorityDetails { get; set; }
        public bool RM { get; set; }
        public bool LEAVE { get; set; }
        public bool LOAN { get; set; }
        public bool LETTERS { get; set; }
        public bool ASSETS { get; set; }
        public bool CARRER { get; set; }
        public bool OVERTIME { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
       

    }
    public class RequesDetails
    {
        public int RequestVsAuthorityId { get; set; }
        public Guid RequestVsAuthorityGI { get; set; }
        public int LevelCode { get; set; }
        public string Level { get; set; }
        public string Authority { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
         public string EmployeeID { get; set; }

    }
    public class RequestVsAuthorityLogDetails
    {
        public int RequestCode { get; set; }
        public string RequestName { get; set; }
        public string AuthorityDetails { get; set; }

    }
    public class BatchUpdate
    {
        public RequestType RequestType { get; set; }
        public AuthorityLevel RequestLevel { get; set; }
        public ReportingManager RequestAuthority { get; set; }
    }
    public class AddLevel
    {
        public Guid RequestVsAuthorityGI { get; set; }
        public int LevelCode { get; set; }
        public string Level { get; set; }
        public AuthorityLevel RequestLevel { get; set; }
        public int RequestType { get; set; }
        public string Mode { get; set; }
        public string AuthorityName { get; set; }
    }
    public class RequestType : MasterBase
    {

    }
    public class AuthorityLevel : MasterBase
    {

    }
    public class UnassignedList : MasterBase
    {

    }
    public class AlreadyExists_RequestVsAuthority
    {
        public int Code { get; set; }
        public string Message { get; set; }
    }
}
