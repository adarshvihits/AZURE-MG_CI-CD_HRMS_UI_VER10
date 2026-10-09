using Hrms.Lite.Shared.DashBoard;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DashBoard
{
   public interface ISuccessionDashBoardService
    {
        public Task<SuccessionDashboard> GetDashBoardDiagram( string Classification);
        public Task<SuccessionDashboard> GetSuccessionDashboardDetails();
    }
}
