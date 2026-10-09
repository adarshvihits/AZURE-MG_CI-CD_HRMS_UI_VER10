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
    public class DepartmentService: IDepartmentService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public DepartmentService(IServiceBase httpService)
        {
            _baseURL = "department";
            _baseURL1 = "Common";
            _httpService = httpService;
         
        }

        public Task<ResponseEntity> ApproveOrReject(Department model)
        {
            return _httpService.PostAsync<ResponseEntity, Department>($"{_baseURL}/ApproveOrReject", model);
        }

        public Task<ResponseEntity> Create(Department input)
        {
            return _httpService.PostAsync<ResponseEntity, Department>($"{_baseURL}/Create",input);
        }

        public Task<ResponseEntity> Delete(Department model)
        {
            return _httpService.PostAsync<ResponseEntity, Department>($"{_baseURL}/Delete", model);
        }

        public async Task<Department> GetDepartmentDetails(Guid DepartmentGI, Guid LogGI, string Type)
        {
            return await _httpService.GetAsync<Department>($"{_baseURL}/DepartmentDetails?DepartmentGI={DepartmentGI}&&LogGI={LogGI}&&Type={Type}");
        }

        //public Task<ResponseEntity> Create(Department input)
        //{
        //    throw new NotImplementedException();
        //}


        public async Task<Department> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<Department>($"{_baseURL}/List?TabIndex={TabIndex}");
        }

        public async Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return await _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }
        public async Task<List<DepartmentVsSectionBT>> GetSectionBTGridFill()
        {
            return await _httpService.GetAsync<List<DepartmentVsSectionBT>>($"{_baseURL}/SectionBTGridFill");
        }
    }
}
