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
    public class BloodBankReportService : IBloodBankReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public BloodBankReportService(IServiceBase httpService)
        {
            _baseURL = "BloodBankReport";
            _httpService = httpService;
        }

        public async Task<BloodBankReport> GetBloodBankReport(BloodBankReport model)
        {
            return await _httpService.PostAsync<BloodBankReport, BloodBankReport>($"{_baseURL}/GetBloodBankReport", model);
        }

       public async Task<DataSet> GetBloodBankReport_Excel(BloodBankReport BloodBankReport)
        {
            return await _httpService.PostAsync<DataSet, BloodBankReport>($"{_baseURL}/GetBloodBankReport_Excel", BloodBankReport);
        }
    }
}
