using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DashBoard;
using Hrms.Lite.Shared.DashBoard;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DashBoard
{
    public class SettingsDashBoardService : ISettingsDashBoardService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public SettingsDashBoardService(IServiceBase httpService)
        {
            _baseURL = "SettingsDashBoard";
            _httpService = httpService;
        }
        public async Task<SettingsDashBoard> GetSettingsDashBoardList()
        {
            return await _httpService.GetAsync<SettingsDashBoard>($"{_baseURL}/GetSettingsDashBoardList");
        }
       
    }
}
