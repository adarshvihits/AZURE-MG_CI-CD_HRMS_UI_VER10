using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.Master
{
   public class UserRole:MasterBase
    {
        public string userrole { get; set; }
        public string Remarks { get; set; }
        public int ActiveCount { get; set; }
        public int InactiveCount { get; set; }
        public int PendingCount { get; set; }
        public SalaryVisibility SalaryVisibility { get; set; }
        public string PermittedActions { get; set; }
        public string EntryType { get; set; }
        public bool View { get; set; }
        public bool Create { get; set; }
        public bool Modify { get; set; }
        public bool Approve { get; set; }
        public bool Delete { get; set; }
        public List<FormPermissionModule> UserVsModule { get; set; }
        public List<FormPermissionSubModule> UserVsSubModule { get; set; }
        public List<FormPermissionMenu> UserVsMenu { get; set; }
        public List<FormPermissionMenuTab> UserVsMenuTab { get; set; }
        public List<UserRole> UserRoleList { get; set; }
        public string Editmode { get; set; }

        //public List<UserRoleTabs> UserRoleTabs { get; set; }

    }
    public class FormPermissionMenuTab
    {
        public Guid? GI { get; set; }
        public int MenuCode { get; set; }
        public string ModuleName { get; set; }
        public string MenuName { get; set; }
        public string TabName { get; set; }
        public int TabCode { get; set; }
        public bool Applicable { get; set; }
    }
    public class SalaryVisibility : MasterBase
    {

    }
    public class FormPermissionModule
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public bool Applicable { get; set; }
    }
    public class FormPermissionSubModule
    {
        public int Code { get; set; }
        public string ModuleName { get; set; }
        public string Name { get; set; }
        public bool Applicable { get; set; }
    }
    public class FormPermissionMenu
    {
        public int Code { get; set; }
        public string ModuleName { get; set; }
        public string SubModuleName { get; set; }
        public string Name { get; set; }
        public bool Applicable { get; set; }
    }
    public class Head
    {
        public Guid GI { get; set; }
        public Guid LogGI { get; set; }
        public FormPermissionArray Module { get; set; }
        public FormPermissionArray SubModule { get; set; }
        public FormPermissionArray Menu { get; set; }
        public string Mode { get; set; }
        public string EDITMODE { get; set; }
    }
    public class FormPermissionArray
    {
        public int[] Code { get; set; }
        public string[] Name { get; set; }
        public bool[] Applicable { get; set; }
        public string mode { get; set; }

    }
    public class UserRoleTabs
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
    }
}
