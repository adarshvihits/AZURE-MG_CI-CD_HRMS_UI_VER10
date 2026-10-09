using Hrms.Lite.Shared.DashBoard;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DashBoard
{
  public  interface ITravelClaimDashboardService
    {
        public Task<TravelClaimDashboard> GetTravelDashBoardCount();
        public Task<List<RequestList>> GetRequestList(int Type);
        public Task<List<BudjetTracking>> GetBudgetTracking(int Type);
    }
}
