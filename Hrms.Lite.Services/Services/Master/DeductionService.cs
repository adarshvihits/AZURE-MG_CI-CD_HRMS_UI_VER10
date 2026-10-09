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
    public class DeductionService : IDeductionService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public DeductionService(IServiceBase httpService)
        {
            _baseURL = "Deduction";
            _baseURL1 = "Common";
            _httpService = httpService;
        }

        public Task<ResponseEntity> ApproveOrReject(Deduction model)
        {
            return _httpService.PostAsync<ResponseEntity, Deduction>($"{_baseURL}/ApproveOrReject", model);
        }

        public Task<ResponseEntity> Create(Deduction input)
        {
            return _httpService.PostAsync<ResponseEntity, Deduction>($"{_baseURL}/Create", input);
        }

        public Task<ResponseEntity> Delete(Deduction model)
        {
            return _httpService.PostAsync<ResponseEntity, Deduction>($"{_baseURL}/Delete", model);
        }

        public Task<Deduction> GetDeductionDetails(Guid DeductionGI, Guid LogGI, string Type)
        {
            return _httpService.GetAsync<Deduction>($"{_baseURL}/GetDeductionDetails?DeductionGI={DeductionGI}&LogGI={LogGI}&Type={Type}");
        }

        public async Task<Deduction> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<Deduction>($"{_baseURL}/List?TabIndex={TabIndex}");
        }
        public async Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return await _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }
    }
}

//public Task<ResponseEntity> ApproveOrReject(Hobbies model)
//{
//    return _httpService.PostAsync<ResponseEntity, Hobbies>($"{_baseURL}/ApproveOrReject", model);
//}

//public Task<ResponseEntity> Create(Hobbies input)
//{
//    return _httpService.PostAsync<ResponseEntity, Hobbies>($"{_baseURL}/Create", input);
//}

//public Task<ResponseEntity> Delete(Hobbies input)
//{
//    return _httpService.PostAsync<ResponseEntity, Hobbies>($"{_baseURL}/Delete", input);
//}

//public Task<Hobbies> GetHobbyDetails(Guid HobbyGI, Guid LogGI, string Type)
//{
//    return _httpService.GetAsync<Hobbies>($"{_baseURL}/HobbyDetails?HobbyGI={HobbyGI}&LogGI={LogGI}&Type={Type}");
//}

//public async Task<Hobbies> GetList(string TabIndex)
//{
//    return await _httpService.GetAsync<Hobbies>($"{_baseURL}/List?TabIndex={TabIndex}");
//}
