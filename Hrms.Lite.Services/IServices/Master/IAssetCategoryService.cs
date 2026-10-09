using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface IAssetCategoryService
    {
        public Task<AssetCategory> GetList(string TabIndex);
        public Task<ResponseEntity> Create(AssetCategory input);
        public Task<AssetCategory> GetAssetCategoryDetails(Guid AssetCategoryGI, Guid LogGI, string Type);
        public Task<ResponseEntity> ApproveOrReject(AssetCategory model);
        public Task<ResponseEntity> Delete(AssetCategory model);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);

    }
}
