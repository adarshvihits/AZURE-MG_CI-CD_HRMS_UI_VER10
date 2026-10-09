using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface IRegionService
    {
        public Task<Region> GetList(string TabIndex);
        public Task<Region> GetRegionDetails(Guid RegionGI, Guid LogGI, string type);
        public Task<ResponseEntity> Create(Region input);
        public Task<ResponseEntity> ApproveOrReject(Region model);
        public Task<ResponseEntity> Delete(Region model);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);
    }
}
