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
    public class DesignationService : IDesignationService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public DesignationService(IServiceBase httpService)
        {
            _baseURL = "Designation";
            _baseURL1 = "Common";
            _httpService = httpService;
        }

        public async Task<Designation> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<Designation>($"{_baseURL}/list?TabIndex={TabIndex}");
        }

        public Task<ResponseEntity> Create(Designation Input)
        {
            return _httpService.PostAsync<ResponseEntity, Designation>($"{_baseURL}/Create", Input);
        }

        public Task<Designation> GetDesignationDetails(Guid DesignationGI, Guid LogGI, string type)
        {
            return _httpService.GetAsync<Designation>($"{_baseURL}/GetDesignationDetails?DesignationGI={DesignationGI}&LogGI={LogGI}&Type={type}");
        }

        public Task<ResponseEntity> Delete(Designation model)
        {
            return _httpService.PostAsync<ResponseEntity, Designation>($"{_baseURL}/Delete", model);
        }

        public Task<ResponseEntity> ApproveOrReject(Designation input)
        {
            return _httpService.PostAsync<ResponseEntity, Designation>($"{_baseURL}/ApproveOrReject", input);
        }

        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }
    }
}
