using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices
{
  public  interface IMenuService
    {
        public Task<MenuEntity> GetMenu(string LoginFrom);
        public Task<MenuEntity> GetModuleList( string LoginFrom);
        public Task<MenuEntity> GetModuleVsMenuList( int ModuleCode);
        public Task<MenuItem> GetMenuDetails( int MenuCode);
         //--Mobile
         Task<MenuApplicability> GetMenuList();
        Task<MenuApplicability> GetSubMenuList(string data, int datacount);
        Task<ResponseEntity> MobileMenuSave(MenuApplicability model);
    }
}
