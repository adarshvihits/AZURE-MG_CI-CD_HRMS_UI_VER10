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
  public  class WPSReportService: IWPSReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public WPSReportService(IServiceBase httpService)
        {
            _baseURL = "WPSReport";
            _httpService = httpService;
        }
        public Task<System.Data.DataSet> GetDetails(int SalaryPeriod, WPSReport model)
        {
            return _httpService.PostAsync<System.Data.DataSet, WPSReport>($"{_baseURL}/GetWPSReportData?SalaryPeriod={SalaryPeriod}", model);
        }

    }
}
