using Hrms.Lite.Shared.DashBoard;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DashBoard
{
    public interface IAdminDashBoardService
    {
        public Task<AdminDashBoard> GetAdminDashBoardCount();
    }
}
