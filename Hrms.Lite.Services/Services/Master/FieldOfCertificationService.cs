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
    public class FieldOfCertificationService : IFieldOfCertificationService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public FieldOfCertificationService(IServiceBase httpService)
        {
            _baseURL = "FieldOfCertification";
            _baseURL1 = "Common";
            _httpService = httpService;
        }

        public Task<ResponseEntity> ApproveOrReject(FieldOfCertification model)
        {
            return _httpService.PostAsync<ResponseEntity, FieldOfCertification>($"{_baseURL}/ApproveOrReject", model);
        }

        public Task<ResponseEntity> Create(FieldOfCertification input)
        {
            return _httpService.PostAsync<ResponseEntity, FieldOfCertification>($"{_baseURL}/Create", input);
        }

        public Task<ResponseEntity> Delete(FieldOfCertification input)
        {
            return _httpService.PostAsync<ResponseEntity, FieldOfCertification>($"{_baseURL}/Delete", input);
        }

        public Task<FieldOfCertification> GetFieldOfCertificationDetails(Guid FieldOfCertificationGI, Guid LogGI, string Type)
        {
            return _httpService.GetAsync<FieldOfCertification>($"{_baseURL}/FieldOfCertificationDetails?FieldOfCertificationGI={FieldOfCertificationGI}&LogGI={LogGI}&Type={Type}");
        }

        public async Task<FieldOfCertification> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<FieldOfCertification>($"{_baseURL}/List?TabIndex={TabIndex}");
        }

        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }
    }
}
