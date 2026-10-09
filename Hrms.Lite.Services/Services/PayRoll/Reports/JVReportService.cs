using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.PayRoll.Reports;
using Hrms.Lite.Shared.PayRoll.Reports;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.PayRoll.Reports
{
  public  class JVReportService: IJVReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public JVReportService(IServiceBase httpService)
        {
            _baseURL = "JVReport";
            _httpService = httpService;
        }


        public async Task<System.Data.DataSet> GetJVExportToExcelDetails(JVReport model)
        {
            return await _httpService.PostAsync<System.Data.DataSet,JVReport>($"{_baseURL}/GetJVExportToExcelDetails", model);
        }
        public async Task<System.Data.DataSet> GetERPJVExportToExcelDetails(JVReport model)
        {
            return await _httpService.PostAsync<System.Data.DataSet, JVReport>($"{_baseURL}/GetERPJVExportToExcelDetails", model);
        }
    }
}
