using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface IZoneService
    {
        public Task<Zone> GetList(string TabIndex);
        public Task<ResponseEntity> Create(Zone Input);
        public Task<Zone> GetZoneDetails(Guid ZoneGI, Guid LogGI, int ZoneCode, string Type);
        public Task<ResponseEntity> Delete(Zone Input);
        public Task<ResponseEntity> ApproveOrReject(Zone model);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);
    }
}
