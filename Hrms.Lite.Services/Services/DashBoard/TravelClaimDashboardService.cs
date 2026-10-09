using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DashBoard;
using Hrms.Lite.Shared.DashBoard;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DashBoard
{
   public class TravelClaimDashboardService: ITravelClaimDashboardService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public TravelClaimDashboardService(IServiceBase httpService)
        {
            _baseURL = "TravelClaimDashboard";
            _httpService = httpService;
        }
        public async Task<TravelClaimDashboard> GetTravelDashBoardCount()
        {
            return await _httpService.GetAsync<TravelClaimDashboard>($"{_baseURL}/GetTravelDashBoardCount");
        }

        public async Task<List<RequestList>> GetRequestList(int Type)
        {
            return await _httpService.GetAsync<List<RequestList>>($"{_baseURL}/GetRequestList?Type={Type}");
        }
        public async Task<List<BudjetTracking>> GetBudgetTracking(int Type)
        {
            return await _httpService.GetAsync<List<BudjetTracking>>($"{_baseURL}/GetBudgetTracking?Type={Type}");
        }
    }
}
