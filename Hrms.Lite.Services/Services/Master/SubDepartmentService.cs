using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.Master
{
    public class SubDepartmentService : ISubDepartmentService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public SubDepartmentService(IServiceBase httpService)
        {
            _baseURL = "SubDepartment";
            _baseURL1 = "Common";
            _httpService = httpService;

        }
        public async Task<SubDepartment> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<SubDepartment>($"{_baseURL}/List?TabIndex={TabIndex}");
        }

        public Task<ResponseEntity> Create(SubDepartment input)
        {
            return _httpService.PostAsync<ResponseEntity, SubDepartment>($"{_baseURL}/Create", input);
        }

        public async Task<SubDepartment> GetSubDepartmentDetails(Guid SubDepartmentGI, Guid LogGI, string Type)
        {
            return await _httpService.GetAsync<SubDepartment>($"{_baseURL}/SubDepartmentDetails?SubDepartmentGI={SubDepartmentGI}&&LogGI={LogGI}&&Type={Type}");
        }

        public Task<ResponseEntity> Delete(SubDepartment model)
        {
            return _httpService.PostAsync<ResponseEntity, SubDepartment>($"{_baseURL}/Delete", model);
        }

        public Task<ResponseEntity> ApproveOrReject(SubDepartment model)
        {
            return _httpService.PostAsync<ResponseEntity, SubDepartment>($"{_baseURL}/ApproveOrReject", model);
        }
        public async Task<DataSet> GetList_Excel(string TabIndex)
        {
            return await _httpService.GetAsync<DataSet>($"{_baseURL}/GetList_Excel?TabIndex={TabIndex}");
        }
    }
}
