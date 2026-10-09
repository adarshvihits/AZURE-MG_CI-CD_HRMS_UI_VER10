using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.PayRoll.Reports;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.PayRoll.Reports
{
   public class FormIVReportService : IFormIVReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public FormIVReportService(IServiceBase httpService)
        {
            _baseURL = "FormIVReport";
            _httpService = httpService;
        }
        public Task<FormIVReport> GetFormIVReport( Filter filter,int Month)
        {
            return _httpService.PostAsync<FormIVReport, Filter>($"{_baseURL}/GetFormIVReport?Month={Month}", filter);

        }
        public async Task<DataSet> FormIVReport_Excel(int Month, Filter Filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetFormIVReport_Excel?Month={Month}", Filter);

        }
    }
}
