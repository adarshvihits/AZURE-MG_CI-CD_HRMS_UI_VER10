using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
    public interface IPositionManagerService
    {
        public Task<List<PositionManager>> GetPositionManagerList(Filter filter);
        public Task<PositionManager> GetPositionManagerSummary(Filter filter);
        public Task<PositionManager> GetPositionManagerDetails(Guid PositionGI);
    }
}
