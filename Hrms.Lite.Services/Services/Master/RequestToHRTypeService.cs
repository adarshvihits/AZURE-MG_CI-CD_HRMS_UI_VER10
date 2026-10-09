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
   public class RequestToHRTypeService: IRequestToHRTypeService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public RequestToHRTypeService(IServiceBase httpService)
        {
            _baseURL = "RequestToHRType";
            _baseURL1 = "Common";
            _httpService = httpService;
        }
        public async Task<RequestToHRType> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<RequestToHRType>($"{_baseURL}/List?TabIndex={TabIndex}");
        }
        public Task<ResponseEntity> Create(RequestToHRType input)
        {
            return _httpService.PostAsync<ResponseEntity, RequestToHRType>($"{_baseURL}/Create", input);
        }
        public Task<RequestToHRType> GetDetails(Guid RequestToHrTypeGI, Guid LogGI, string type)
        {
            return _httpService.GetAsync<RequestToHRType>($"{_baseURL}/GetDetails?RequestToHrTypeGI={RequestToHrTypeGI}&LogGI={LogGI}&Type={type}");
        }
        public async Task<ResponseEntity> Delete(RequestToHRType model)
        {
            return await _httpService.PostAsync<ResponseEntity, RequestToHRType>($"{_baseURL}/Delete", model);

        }
        public Task<ResponseEntity> ApproveOrReject(RequestToHRType model)
        {
            return _httpService.PostAsync<ResponseEntity, RequestToHRType>($"{_baseURL}/ApproveOrReject", model);
        }
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }
    }
}
