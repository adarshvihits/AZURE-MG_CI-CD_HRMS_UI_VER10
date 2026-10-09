using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.TimeOffice;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.TimeOffice
{
    public class LOPSandwichUpdationService : ILOPSandwichUpdationService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public LOPSandwichUpdationService(IServiceBase httpService)
        {
            _baseURL = "LOPSandwichUpdation";
            _httpService = httpService;
        }

        public async Task<LOPSandwichUpdation> GetList(int Month, int Payroll, Filter filter)
        {
            return await _httpService.PostAsync<LOPSandwichUpdation, Filter>($"{_baseURL}/GetEmpList?Month={Month}&Payroll={Payroll}", filter);
        }

        public async Task<ResponseEntity> LOPSandwichUpdationSave(LOPSandwichUpdation model)
        {
            return await _httpService.PostAsync<ResponseEntity, LOPSandwichUpdation>($"{_baseURL}/LOPSandwichUpdationSave", model);
        }

        public async Task<LOPSandwichUpdation> GetHistoryList(int Month, int Payroll, Filter filter)
        {
            return await _httpService.PostAsync<LOPSandwichUpdation, Filter>($"{_baseURL}/GetHistoryList?Month={Month}&Payroll={Payroll}", filter);
        }

        public async Task<DataSet> LOPSandwichUpdationExcel(int Month, int Payroll, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/LOPSandwichUpdationExcel?Month={Month}&Payroll={Payroll}", filter);
        }
    }
}
