using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DashBoard;
using Hrms.Lite.Shared.DashBoard;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DashBoard
{
    public class SuccessionDashBoardService : ISuccessionDashBoardService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public SuccessionDashBoardService(IServiceBase httpService)
        {
            _baseURL = "SuccessionDashboard";
            _httpService = httpService;
        }
        public async Task<SuccessionDashboard> GetDashBoardDiagram(string Classification)
        {
            return await _httpService.GetAsync<SuccessionDashboard>($"{_baseURL}/GetDashBoardDiagram?Classification={Classification}");
            
        }
        public async Task<SuccessionDashboard> GetSuccessionDashboardDetails()
        {
            return await _httpService.GetAsync<SuccessionDashboard>($"{_baseURL}/GetSuccessionDashboardDetails");
        }
    }
}
