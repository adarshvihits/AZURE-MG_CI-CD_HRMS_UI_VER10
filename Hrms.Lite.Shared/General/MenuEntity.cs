using System;
using System.Collections.Generic;


namespace Hrms.Lite.Shared.General
{
    public class MenuEntity
    {
        public List<ModuleItem> Module { get; set; }
        public List<SubModuleItem> SubModule { get; set; }
        public List<MenuItem> Menu { get; set; }
    }

    public class ModuleItem
    {
        public int ModuleCode { get; set; }
        public string Name { get; set; }
        public int Active { get; set; }
        public int DisplayOrder { get; set; }
        public string Class { get; set; }
        public string ModuleKey { get; set; }
        public string Route { get; set; }
        public int CompanyCode { get; set; }
    }
    public class SubModuleItem
    {
        public int SubModuleCode { get; set; }
        public string Name { get; set; }
        public int ModuleCode { get; set; }
        public string ModuleName { get; set; }
        public string ModuleClass { get; set; }
        public int Active { get; set; }
        public int DisplayOrder { get; set; }
        public string Class { get; set; }
        public string SubModuleKey { get; set; }
        public int CompanyCode { get; set; }
    }
    public class MenuItem
    {
        public int MenuCode { get; set; }
        public string Name { get; set; }
        public int SubModuleCode { get; set; }
        public string Area { get; set; }
        public string Controller { get; set; }
        public string Class { get; set; }
        public string Action { get; set; }
        public int Active { get; set; }
        public string FormType { get; set; }
        public string MenuKey { get; set; }
        public string Route { get; set; }
        public int IsHrmsMenu { get; set; }
        public int IsEsspMenu { get; set; }
        public int IsStaticMenu { get; set; }
        public int CompanyCode { get; set; }
        public int ModuleCode { get; set; }
    }
}
