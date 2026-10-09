using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank
{
    public class EmployeeVsBonusGroupService: IEmployeeVsBonusGroupService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public EmployeeVsBonusGroupService(IServiceBase httpService)
        {
            _baseURL = "EmployeeVsBonusGroup";
            _httpService = httpService;
        }
        public async Task<EmployeeVsBonusGroup> GetEmployeeList(int TabIndex, Filter filter)
        {
            return await _httpService.PostAsync<EmployeeVsBonusGroup, Filter>($"{_baseURL}/GetEmployeeList?TabIndex={TabIndex}", filter);
        }
        public async Task<ResponseEntity> Save(EmployeeVsBonusGroup input)
        {
            return await _httpService.PostAsync<ResponseEntity, EmployeeVsBonusGroup>($"{_baseURL}/Create", input);
        }
        public async Task<DataSet> GetEmployeeList_Excel(int TabIndex, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetEmployeeList_Excel?TabIndex={TabIndex}", filter);
        }
        public async Task<ResponseEntity> EmployeeVsBonusGroupBatchUpdate(Filter filter, int BonusGroup, int TabIndex)
        {
            return await _httpService.PostAsync<ResponseEntity, Filter>($"{_baseURL}/EmployeeVsBonusGroupBatchUpdate?BonusGroup={BonusGroup}&TabIndex={TabIndex}", filter);
        }
    }
}
