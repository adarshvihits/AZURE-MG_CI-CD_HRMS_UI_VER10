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
  public  class EmployeeDocumentTypeService:IEmployeeDocumentTypeService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public EmployeeDocumentTypeService(IServiceBase httpService)
        {
            _baseURL = "EmployeeDocumentType";
            _baseURL1 = "Common";
            _httpService = httpService;
        }
        public async Task<EmployeeDocumentType> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<EmployeeDocumentType>($"{_baseURL}/List?TabIndex={TabIndex}");
        }
        public Task<EmployeeDocumentType> GetEmployeeDocumentTypeDetails(Guid DocumentGI, Guid LogGI, string type)
        {
            return _httpService.GetAsync<EmployeeDocumentType>($"{_baseURL}/EmployeeDocumentTypeDetails?DocumentGI={DocumentGI}&LogGI={LogGI}&Type={type}");
        }
        public Task<ResponseEntity> Create(EmployeeDocumentType input)
        {
            return _httpService.PostAsync<ResponseEntity, EmployeeDocumentType>($"{_baseURL}/Create", input);
        }

        public Task<ResponseEntity> ApproveOrReject(EmployeeDocumentType model)
        {
            return _httpService.PostAsync<ResponseEntity, EmployeeDocumentType>($"{_baseURL}/ApproveOrReject", model);
        }
        public async Task<ResponseEntity> Delete(EmployeeDocumentType model)
        {
            return await _httpService.PostAsync<ResponseEntity, EmployeeDocumentType>($"{_baseURL}/Delete", model);

        }
    }
}
