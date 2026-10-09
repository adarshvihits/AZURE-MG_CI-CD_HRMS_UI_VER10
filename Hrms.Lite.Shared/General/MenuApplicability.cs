using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.General
{
    public class MenuApplicability
    {
        public List<Submenu_MenuList> MenuList { get; set; }
        public List<Submenu_MenuList> SubMenuList { get; set; }
    public PermissionArray MenuArray { get; set; }

}
    public class Submenu_MenuList
    {
        public int MenuCode { get; set; }
        public string MenuName { get; set; }
        public int SubMenuCode { get; set; }
        public string SubMenuName { get; set; }
        public bool MenuApplicability { get; set; }
        public bool SubMenuApplicability { get; set; }
    }

    public class PermissionArray
    {
        public int[] Code { get; set; }
        public string[] Name { get; set; }
        public bool[] Applicable { get; set; }

    }
}
