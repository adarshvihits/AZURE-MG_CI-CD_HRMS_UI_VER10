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
    public class ArearProcessingService : IArearProcessingService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public ArearProcessingService(IServiceBase httpService)
        {
            _baseURL = "ArearProcessing";
            _httpService = httpService;
        }
        public async Task<ArearProcessing> ArearProcessing(int Month, int PayrollGroup, Filter filter)
        {
            return await _httpService.PostAsync<ArearProcessing, Filter>($"{_baseURL}/ArearProcessing?Month={Month}&PayrollGroup={PayrollGroup}", filter);

        }
        public async Task<DataSet> ArearProcessing_Excel(int Month, int PayrollGroup, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/ArearProcessing_Excel?Month={Month}&PayrollGroup={PayrollGroup}", filter);

        }
        async Task<ArearProcessing> IArearProcessingService.ArearProcessingDetails(int Month, int EmployeeCode)
        {
            return await _httpService.GetAsync<ArearProcessing>($"{_baseURL}/ArearProcessingDetails?Month={Month}&EmployeeCode={EmployeeCode}");

        }
    }
}
