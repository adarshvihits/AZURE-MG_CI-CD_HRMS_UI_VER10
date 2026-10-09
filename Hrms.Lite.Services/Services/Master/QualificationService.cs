using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.Master;
using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.Master
{
    public class QualificationService : IQualificationService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public QualificationService(IServiceBase httpService)
        {
            _baseURL = "Qualification";
            _baseURL1 = "Common";
            _httpService = httpService;
        }
        public async Task<Qualification> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<Qualification>($"{_baseURL}/List?TabIndex={TabIndex}");
        }
        public Task<ResponseEntity> Create(Qualification input)
        {
            return _httpService.PostAsync<ResponseEntity, Qualification>($"{_baseURL}/Create", input);
        }
        public Task<Qualification> GetQualificationDetails(Guid QualificationGI, Guid LogGI, string Type)
        {
            return _httpService.GetAsync<Qualification>($"{_baseURL}/QualificationDetails?QualificationGI={QualificationGI}&LogGI={LogGI}&Type={Type}");
        }
        public Task<ResponseEntity> ApproveOrReject(Qualification model)
        {
            return _httpService.PostAsync<ResponseEntity, Qualification>($"{_baseURL}/ApproveOrReject", model);
        }
        public Task<ResponseEntity> Delete(Qualification input)
        {
            return _httpService.PostAsync<ResponseEntity, Qualification>($"{_baseURL}/Delete", input);
        }
        public async Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return await _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }
    }
}
