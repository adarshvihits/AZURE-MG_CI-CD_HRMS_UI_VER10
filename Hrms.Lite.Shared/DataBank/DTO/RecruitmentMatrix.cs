using Hrms.Lite.Shared.General;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.DTO
{
    public class RecruitmentMatrix
    {
        public Guid RecruitmentMatrixGI { get; set; }
        public MasterBase Template { get; set; }
        public MasterBase TemplateList { get; set; }
        public MasterBase Level { get; set; }
        public int? NoOfLevels { get; set; }
        public MasterBase Role { get; set; }
        public List<ApprovalRoleR> ApprovalRoleList { get; set; }
        public List<RecruitmentMatrix> RecruitmentMatrixList { get; set; }
        public Filter filter { get; set; }
        public int LevelCode { get; set; }
        public int RecruitmentRoleCode { get; set; }
        public int RoleCode { get; set; }
        public int RecMatrixCode { get; set; }
        public string CountryName { get; set; }
        public string RoleOrEmployee { get; set; }
        public string RecruitmentRoleName { get; set; }
        public string EmployeeName { get; set; }
        public int active { get; set; }
        public int totalRecords { get; set; }

        public string RoleName { get; set; }
        public string LevelName { get; set; }
        public string Mode { get; set; }
        public MasterBase Employee { get; set; }

        public string ValidationMessage { get; set; }
        public int AlreadyExistsStatus { get; set; }
    }
    public class ApprovalRoleR
    {
        public Dropdown Level { get; set; }
        public Dropdown Role { get; set; }
        public Dropdown RecruitmentRole { get; set; }
        public Dropdown RoleOrEmp { get; set; }
        public int[] EmployeeCode { get; set; }
        public string[] EmployeeName { get; set; }
        
        public SelectList ResponsiblePerson { get; set; }
    }
}
