using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank
{
    public class EmployeeVsVDAGroupService: IEmployeeVsVDAGroupService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public EmployeeVsVDAGroupService(IServiceBase httpService)
        {
            _baseURL = "EmployeeVsVDAGroup";
            _httpService = httpService;
        }
        public async Task<VDAGroup> GetList(string Tabindex, Filter filter)
        {
            return await _httpService.PostAsync<VDAGroup, Filter>($"{_baseURL}/List?Tabindex={Tabindex}", filter);
        }
        public async Task<ResponseEntity> Create(VDAGroup model)
        {
            return await _httpService.PostAsync<ResponseEntity, VDAGroup>($"{_baseURL}/Create", model);
        }
        public async Task<ResponseEntity> BatchUpdate(VDAGroup model)
        {
            return await _httpService.PostAsync<ResponseEntity, VDAGroup>($"{_baseURL}/BatchUpdate", model);
        }
        public async Task<DataSet> EmployeeVDAGroup_Excel(string TabIndex, Filter Filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/EmployeeVDAGroup_Excel?TabIndex={TabIndex}", Filter);
        }
        public async Task<ResponseEntity<List<VDAGroup>>> ImportFromExcel(string TabIndex,VDAGroup model)
        {
            return await _httpService.PostAsync<ResponseEntity<List<VDAGroup>>, VDAGroup>($"{_baseURL}/ImportFromExcel?TabIndex={TabIndex}", model);

        }
    }
}
