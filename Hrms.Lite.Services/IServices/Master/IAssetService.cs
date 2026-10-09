using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
   public interface IAssetService
    {
        public Task<Asset> GetList(string TabIndex);
        public Task<Asset> GetAssetDetails(Guid AssetGI, Guid LogGI, string type);
        public Task<ResponseEntity> Create(Asset input);
        public Task<ResponseEntity> ApproveOrReject(Asset model);
        public Task<ResponseEntity> Delete(Guid GI);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);
    }
}
