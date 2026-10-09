using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.Home;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.Home
{
    public class HomeService : IHomeService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public HomeService(IServiceBase httpService)
        {
            _baseURL = "Home";
            _httpService = httpService;
        }
        public async Task<ResponseEntity> SaveTheme(Theme model)
        {
            return await _httpService.PostAsync<ResponseEntity, Theme>($"{_baseURL}/SaveTheme", model);
        }
        public async Task<Theme> GetTheme()
        {                                                                                                           
            return await _httpService.GetAsync<Theme>($"{_baseURL}/GetTheme");
        }
        public async Task<Theme> GetDarkModeStatus()
        {
            return await _httpService.GetAsync<Theme>($"{_baseURL}/GetDarkModeStatus");
        }
        public async Task<ResponseEntity> ToggleDarkMode(int IsDarkMode)
        {
            return await _httpService.PostAsync<ResponseEntity, int>($"{_baseURL}/ToggleDarkMode", IsDarkMode);
        }
    }
}
