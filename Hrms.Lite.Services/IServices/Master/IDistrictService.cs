using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface IDistrictService
    {
        public Task<District> GetList(string TabIndex);
        public Task<ResponseEntity> Create(District input);
        public Task<District> GetDistrictDetails(Guid LogGI, Guid DistrictGI, string Type);
        public Task<ResponseEntity> ApproveOrReject(District input);
        public Task<ResponseEntity> Delete(District input);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);
    }
}
