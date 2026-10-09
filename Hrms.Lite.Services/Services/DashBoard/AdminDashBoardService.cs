using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DashBoard;
using Hrms.Lite.Shared.DashBoard;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DashBoard
{
    public class AdminDashBoardService: IAdminDashBoardService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public AdminDashBoardService(IServiceBase httpService)
        {
            _baseURL = "AdminDashBoard";
            _httpService = httpService;
        }
        public async Task<AdminDashBoard> GetAdminDashBoardCount()
        {
            return await _httpService.GetAsync<AdminDashBoard>($"{_baseURL}/GetAdminDashBoardCount");
        }
    }
}
