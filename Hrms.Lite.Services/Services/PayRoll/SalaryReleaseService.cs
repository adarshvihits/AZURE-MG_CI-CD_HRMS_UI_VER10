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
    public class SalaryReleaseService : ISalaryReleaseService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public SalaryReleaseService(IServiceBase httpService)
        {
            _baseURL = "SalaryRelease";
            _httpService = httpService;
        }

        public async Task<SalaryRelease> GetList(int Month, int PayrollGroupCode, Filter filter)
        {
            return await _httpService.PostAsync<SalaryRelease, Filter>($"{_baseURL}/GetSalaryReleaseList?Month={Month}&PayrollGroupCode={PayrollGroupCode}", filter);
        }

      

        public async Task<ResponseEntity> Save(SalaryRelease model)
        {
            return await _httpService.PostAsync<ResponseEntity, SalaryRelease>($"{_baseURL}/GetSalaryReleaseSave", model);
        }

        public async Task<DataSet> GetSalaryReleaseList_EXCEL(int Month, int PayrollGroupCode, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetSalaryReleaseList_EXCEL?Month=" +
                $"{Month}&PayrollGroupCode={PayrollGroupCode}", filter);
        }


    }
}
