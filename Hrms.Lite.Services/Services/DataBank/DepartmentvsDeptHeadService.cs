using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Shared.DataBank;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank
{
    public class DepartmentvsDeptHeadService: IDepartmentvsDeptHeadService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public DepartmentvsDeptHeadService(IServiceBase httpService)
        {
            _baseURL = "DepartmentvsDeptHead";
            _httpService = httpService;
        }

        public async Task<DepartmentvsDeptHead> GetList()
        {
            return await _httpService.GetAsync<DepartmentvsDeptHead>($"{_baseURL}/List");
        }

        public async Task<DepartmentvsDeptHead> GetDetails(int DepartmentCode)
        {
            return await _httpService.GetAsync<DepartmentvsDeptHead>($"{_baseURL}/DepartmentvsDeptHeadDetails?DepartmentCode={DepartmentCode}");
        }

        public Task<ResponseEntity> Save(DepartmentvsDeptHead input)
        {
            return _httpService.PostAsync<ResponseEntity, DepartmentvsDeptHead>($"{_baseURL}/Create", input);
        }

        public async Task<DataSet> GetDepartmentvsDeptHead_Excel()
        {
            return await _httpService.GetAsync<DataSet>($"{_baseURL}/GetDepartmentvsDeptHead_Excel");
        }
    }
}
