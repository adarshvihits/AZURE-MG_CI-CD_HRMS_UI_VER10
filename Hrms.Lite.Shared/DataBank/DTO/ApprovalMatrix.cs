using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.DTO
{
    public class ApprovalMatrix
    { 
        public Guid ApprovalMatrixGI { get; set; }
        public MasterBase WorkFlow { get; set; }
        public MasterBase WorkFlowList { get; set; }
        public MasterBase Level { get; set; }
        public int? NoOfLevels { get; set; }
        public MasterBase Role { get; set; }
        public List<ApprovalRole> ApprovalRoleList { get; set; }
        public List<ApprovalMatrix> ApprovalMatrixList { get; set; }
        public Filter filter { get; set; }
        public int active { get; set; }
        public int totalRecords { get; set; }
        public string WorkFlowName { get; set; }
        public string RoleName { get; set; }
        public string LevelName { get; set; }
        public string Mode { get; set; }
        public MasterBase Employee { get; set; }
    
        public string ValidationMessage { get; set; }
        public int AlreadyExistsStatus { get; set; }
    }
    public class ApprovalRole
    {
        public MasterBase Level { get; set; }
        public MasterBase Role { get; set; }
    }
}
