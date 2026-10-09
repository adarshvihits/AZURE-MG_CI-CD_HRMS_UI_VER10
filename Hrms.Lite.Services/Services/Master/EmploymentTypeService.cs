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
   public class EmploymentTypeService : IEmploymentTypeService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;

        public EmploymentTypeService(IServiceBase httpService)
        {
            _baseURL = "EmploymentType";
            _baseURL1 = "Common";
            _httpService = httpService;
        }
        public async Task<EmploymentType> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<EmploymentType>($"{_baseURL}/List?TabIndex={TabIndex}");
        }
        public Task<ResponseEntity> Create(EmploymentType input)
        {
            return _httpService.PostAsync<ResponseEntity, EmploymentType>($"{_baseURL}/Create", input);
        }
        public Task<EmploymentType> GetEmploymentTypeDetails(Guid EmploymentTypeGI, Guid LogGI, string Type)
        {
            return _httpService.GetAsync<EmploymentType>($"{_baseURL}/GetEmploymentTypeDetails?EmploymentTypeGI={EmploymentTypeGI}&LogGi={LogGI}&Type={Type}");
        }
        public Task<ResponseEntity> ApproveOrReject(EmploymentType model)
        {
            return _httpService.PostAsync<ResponseEntity, EmploymentType>($"{_baseURL}/ApproveOrReject", model);
        }
        public async Task<ResponseEntity> Delete(EmploymentType model)
        {
            return await _httpService.PostAsync<ResponseEntity, EmploymentType>($"{_baseURL}/Delete", model);

        }
        public async Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return await _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }

    }
}
