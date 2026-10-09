using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.PayRoll;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.PayRoll
{
    public class CostProcessingService : ICostProcessingService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public CostProcessingService(IServiceBase httpService)
        {
            _baseURL = "CostProcessing";
            _httpService = httpService;
        }

      
        public Task<ResponseEntity<CostProcessing>> GetEmployeeVsCostCenterPendingList(int MonthCode, int PayrollGroup)
        {
            return _httpService.GetAsync<ResponseEntity<CostProcessing>>($"{_baseURL}/GetEmployeeVsCostCenterPendingList?MonthCode={MonthCode}&PayrollGroup={PayrollGroup}");
        }
        //public Task<CostProcessing> GetEmployeeVsCostCenterPendingList(int MonthCode, int PayrollGroup)
        //{
        //    return _httpService.GetAsync<CostProcessing>($"{_baseURL}/GetEmployeeVsCostCenterPendingList?MonthCode={MonthCode}&PayrollGroup={PayrollGroup}");
        //}
        public async Task<DataSet> GetEmployeeVsCostCenterPendingList_Excel(int MonthCode,int PayrollGroup)
        {
            return await _httpService.GetAsync<DataSet>($"{_baseURL}/GetEmployeeVsCostCenterPendingList_Excel?PayrollGroup={PayrollGroup}&MonthCode={MonthCode}");

        }
        public Task<ResponseEntity> ProcessCost(int MonthCode, int PayrollGroup)
        {
            return _httpService.GetAsync<ResponseEntity>($"{_baseURL}/ProcessCost?MonthCode={MonthCode}&PayrollGroup={PayrollGroup}");
        }

    }
}
