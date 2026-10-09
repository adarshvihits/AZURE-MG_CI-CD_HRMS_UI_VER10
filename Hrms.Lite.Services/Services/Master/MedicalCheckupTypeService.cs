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
    public class MedicalCheckupTypeService: IMedicalCheckupTypeService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;

        private readonly IServiceBase _httpService;
        public MedicalCheckupTypeService(IServiceBase httpService)
        {
            _baseURL = "CheckupType";
            _baseURL1 = "Common";

            _httpService = httpService;
        }
        public async Task<CheckupType> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<CheckupType>($"{_baseURL}/list?TabIndex={TabIndex}");
        }

        public Task<ResponseEntity> Create(CheckupType input)
        {
            return _httpService.PostAsync<ResponseEntity, CheckupType>($"{_baseURL}/Create", input);
        }


        public Task<CheckupType> GetCheckUpDetails(Guid CheckupTypeGI, Guid LogGI, string Type)
        {
            return _httpService.GetAsync<CheckupType>($"{_baseURL}/GetCheckupTypeDetails?CheckupTypeGI={CheckupTypeGI}&LogGI={LogGI}&Type={Type}");
        }
        public Task<ResponseEntity> ApproveOrReject(CheckupType model)
        {
            return _httpService.PostAsync<ResponseEntity, CheckupType>($"{_baseURL}/ApproveOrReject", model);
        }

        public Task<ResponseEntity> Delete(CheckupType input)
        {
            return _httpService.PostAsync<ResponseEntity, CheckupType>($"{_baseURL}/Delete", input);
        }

        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }
    }
}
