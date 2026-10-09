using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.Master;
using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.Master
{
    public class CommitmentService : ICommitmentService
    {

        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public CommitmentService(IServiceBase httpService)
        {
            _baseURL = "Commitment";
            _baseURL1 = "Common";
            _httpService = httpService;
        }

        public Task<ResponseEntity> ApproveOrReject(Commitment model)
        {
            return _httpService.PostAsync<ResponseEntity, Commitment>($"{_baseURL}/ApproveOrReject", model);
        }

        public Task<ResponseEntity> Create(Commitment input)
        {
            return _httpService.PostAsync<ResponseEntity, Commitment>($"{_baseURL}/Create", input);
        }

        public async Task<ResponseEntity> Delete(Commitment model)
        {
            return await _httpService.PostAsync<ResponseEntity, Commitment>($"{_baseURL}/Delete", model);

        }

        public Task<Commitment> GetCommitmentDetails(Guid CommitmentGI, Guid LogGI, string Type)
        {
            return _httpService.GetAsync<Commitment>($"{_baseURL}/GetCommitmentDetails?CommitmentGI={CommitmentGI}&LogGi={LogGI}&Type={Type}");
        }

        public async Task<Commitment> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<Commitment>($"{_baseURL}/list?TabIndex={TabIndex}");
        }
        public async Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return await _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }
    }
}
