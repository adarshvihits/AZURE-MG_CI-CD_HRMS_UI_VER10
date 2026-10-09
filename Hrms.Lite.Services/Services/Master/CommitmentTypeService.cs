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
    public class CommitmentTypeService : ICommitmentTypeService
    {

        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public CommitmentTypeService(IServiceBase httpService)
        {
            _baseURL = "CommitmentType";
            _baseURL1 = "Common";
            _httpService = httpService;
        }

        public Task<ResponseEntity> ApproveOrReject(CommitmentType model)
        {
            return _httpService.PostAsync<ResponseEntity, CommitmentType>($"{_baseURL}/ApproveOrReject", model);
        }

        public Task<ResponseEntity> Create(CommitmentType input)
        {
            return _httpService.PostAsync<ResponseEntity, CommitmentType>($"{_baseURL}/Create", input);
        }

        public async Task<ResponseEntity> Delete(CommitmentType model)
        {
            return await _httpService.PostAsync<ResponseEntity, CommitmentType>($"{_baseURL}/Delete", model);

        }

        public Task<CommitmentType> GetCommitmentTypeDetails(Guid CommitmentTypeGI, Guid LogGI, string Type)
        {
            return _httpService.GetAsync<CommitmentType>($"{_baseURL}/GetCommitmentTypeDetails?CommitmentTypeGI={CommitmentTypeGI}&LogGi={LogGI}&Type={Type}");
        }

        public async Task<CommitmentType> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<CommitmentType>($"{_baseURL}/list?TabIndex={TabIndex}");
        }
        public async Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return await _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }
    }
}
