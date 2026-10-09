using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.PayRoll;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.PayRoll
{
  public  class SalaryReleaseBankStatementService: ISalaryReleaseBankStatementService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public SalaryReleaseBankStatementService(IServiceBase httpService)
        {
            _baseURL = "SalaryReleaseBankStatement";
            _httpService = httpService;
        }
        public async Task<SalaryReleaseBankStatement> GetList(int Month,int ReportFormat, Filter filter)
        {
            return await _httpService.PostAsync<SalaryReleaseBankStatement, Filter>($"{_baseURL}/GetReleaseStatementList?Month={Month}&ReportFormat={ReportFormat}", filter);
        }
        public async Task<SalaryReleaseBankStatement> GetPreviousList( int Month, Filter filter)
        {
            return await _httpService.PostAsync<SalaryReleaseBankStatement, Filter>($"{_baseURL}/GetReleaseStatementPreviousList?Month={Month}", filter);
        }
        public async Task<System.Data.DataSet> GetReleaseStatementDetails(SalaryReleaseBankStatement model)
        {
            return await _httpService.PostAsync<System.Data.DataSet, SalaryReleaseBankStatement>($"{_baseURL}/GetReleaseStatementDetails",model);
        }
        public Task<ResponseEntity> Save(SalaryReleaseBankStatement input)
        {
            return _httpService.PostAsync<ResponseEntity, SalaryReleaseBankStatement>($"{_baseURL}/Save", input);
        }
        public async Task<ResponseEntity> LogErrorToDB(string errorMessage)
        {
            return await _httpService.GetAsync<ResponseEntity>($"{_baseURL}/LogErrorToDB?errorMessage={errorMessage}");
        }
    }
}
