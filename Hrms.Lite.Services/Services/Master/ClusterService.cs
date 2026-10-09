using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.Master
{
    public class ClusterService: IClusterService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public ClusterService(IServiceBase httpService)
        {
            _baseURL = "Cluster";
            _baseURL1 = "Common";
            _httpService = httpService;
        }

        public Task<ResponseEntity> ApproveOrReject(Cluster model)
        {
            return _httpService.PostAsync<ResponseEntity, Cluster>($"{_baseURL}/ApproveOrReject", model);
        }

        public Task<ResponseEntity> Create(Cluster Input)
        {
            return _httpService.PostAsync<ResponseEntity, Cluster>($"{_baseURL}/Create", Input);
        }

        public Task<ResponseEntity> Delete(Cluster Input)
        {
            return _httpService.PostAsync<ResponseEntity, Cluster>($"{_baseURL}/Delete", Input);
        }

        public async Task<Cluster> GetClusterDetails(Guid ClusterGI, Guid LogGI,int ClusterCode, string Type)
        {
            return await _httpService.GetAsync<Cluster>($"{_baseURL}/GetClusterDetails?ClusterGI={ClusterGI}&&LogGI={LogGI}&&Type={Type}&&ClusterCode={ClusterCode}");
        }

        public async Task<Cluster> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<Cluster>($"{_baseURL}/List?TabIndex={TabIndex}");
        }
        public async Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return await _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }
    }
}
