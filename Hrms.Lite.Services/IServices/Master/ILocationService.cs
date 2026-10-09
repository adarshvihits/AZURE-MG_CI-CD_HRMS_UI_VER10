using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface ILocationService
    {
        public Task<Location> GetList(string TabIndex);
        public Task<ResponseEntity> Create(Location Input);
        public Task<ResponseEntity> Delete(Location Model);
        public Task<Location> GetLocationDetails(Guid LocationGI,Guid LogGI, string Type);
        public Task<ResponseEntity> ApproveOrReject(Location Input);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name,int code);
    }
}
