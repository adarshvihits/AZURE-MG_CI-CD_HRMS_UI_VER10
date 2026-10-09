using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface IClusterService
    {
        public Task<Cluster> GetList(string TabIndex);
        public Task<ResponseEntity> Create(Cluster Input);
        public Task<Cluster> GetClusterDetails(Guid ClusterGI, Guid LogGI, int ClusterCode ,string Type);
        public Task<ResponseEntity> Delete(Cluster Input);
        public Task<ResponseEntity> ApproveOrReject(Cluster model);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);
    }
}
