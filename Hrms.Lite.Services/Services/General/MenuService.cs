using Hrms.Lite.Services.IServices;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.General
{
  public  class MenuService : IMenuService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public MenuService(IServiceBase httpService)
        {
            _baseURL = "Menu";
            _httpService = httpService;
        }

     public async Task<MenuEntity> GetMenu(string LoginFrom)
      {
            return await _httpService.GetAsync<MenuEntity>($"{_baseURL}/GetMenu?LoginFrom={LoginFrom}");
      }

      public async Task<MenuEntity> GetModuleList(string LoginFrom)
        {
            return await _httpService.GetAsync<MenuEntity>($"{_baseURL}/GetModuleList?LoginFrom={LoginFrom}");
        }

    public async Task<MenuEntity> GetModuleVsMenuList(int ModuleCode)
        {
            return await _httpService.GetAsync<MenuEntity>($"{_baseURL}/GetModuleVsMenuList?ModuleCode={ModuleCode}");

        }
        public async Task<MenuItem> GetMenuDetails(int MenuCode)
        {
            return await _httpService.GetAsync<MenuItem>($"{_baseURL}/GetMenuDetails?MenuCode={MenuCode}");
        }
        //Mobile
        async Task<MenuApplicability> IMenuService.GetMenuList()
        {
            return await _httpService.GetAsync<MenuApplicability>($"{"MenuApplicability"}/GetMenuList");
        }

        async Task<MenuApplicability> IMenuService.GetSubMenuList(string data, int datacount)
        {
            return await _httpService.GetAsync<MenuApplicability>($"{"MenuApplicability"}/GetSubMenuList?data={data}&datacount={datacount}");
        }

        async Task<ResponseEntity> IMenuService.MobileMenuSave(MenuApplicability model)
        {
            return await _httpService.PostAsync<ResponseEntity,MenuApplicability>($"{"MenuApplicability"}/MobileMenuSave", model);

        }
    }
}
