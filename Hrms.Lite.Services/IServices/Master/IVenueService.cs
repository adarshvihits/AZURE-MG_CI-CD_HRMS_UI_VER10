using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface IVenueService
    {
        public Task<Venue> GetList(string TabIndex);
        public Task<ResponseEntity> Create(Venue Input);
        public Task<ResponseEntity> Delete(Venue Model);
        public Task<Venue> GetVenueDetails(Guid VenueGI, Guid LogGI, string Type);
        public Task<ResponseEntity> ApproveOrReject(Venue Input);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);
    }
}
