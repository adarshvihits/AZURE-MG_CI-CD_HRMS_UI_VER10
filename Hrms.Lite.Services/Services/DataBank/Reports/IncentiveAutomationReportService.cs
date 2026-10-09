using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank.Reports;
using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank.Reports
{
   public class IncentiveAutomationReportService : IIncentiveAutomationReportService
    {

        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public IncentiveAutomationReportService(IServiceBase httpService)
        {
            _baseURL = "IncentiveAutomationReport";
            _httpService = httpService;
        }

        public async Task<IncentiveAutomationReport> GetList(int monthcode,Filter filter)
        {
            return await _httpService.PostAsync<IncentiveAutomationReport, Filter>($"{_baseURL}/GetList?monthCode={monthcode}", filter);
        }

        public async Task<DataSet> GetList_Excel(int monthcode, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetList_Excel?monthCode={monthcode}", filter);
        }
        public Task<ResponseEntity> SaveIDBUpload(int monthcode)
        {
            return _httpService.PostAsync<ResponseEntity, int>($"{_baseURL}/SaveIDBUpload", monthcode);
        }   
    }
}
