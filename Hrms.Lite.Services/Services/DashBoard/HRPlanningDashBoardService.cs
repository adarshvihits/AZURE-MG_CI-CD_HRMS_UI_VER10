using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DashBoard;
using Hrms.Lite.Shared.DashBoard;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DashBoard
{
    public class HRPlanningDashBoardService : IHRPlanningDashBoardService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public HRPlanningDashBoardService(IServiceBase httpService)
        {
            _baseURL = "HRPlanningDashBoard";
            _httpService = httpService;
        }
       public async Task<HRPlanningDashBoard> GetDashBoardDetails()
        {
            return await _httpService.GetAsync<HRPlanningDashBoard>($"{_baseURL}/GetDashBoardDetails");

        }

        public async Task<HRPlanningDashBoard> GetSepJoiningClassifnDiagram(int PlanningPeriod)
        {
            return await _httpService.GetAsync<HRPlanningDashBoard>($"{_baseURL}/GetSepJoiningClassifnDiagram?Period={PlanningPeriod}");

        }
    }
}
