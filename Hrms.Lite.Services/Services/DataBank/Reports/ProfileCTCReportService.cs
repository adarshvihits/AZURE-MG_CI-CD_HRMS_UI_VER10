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
   
    public class ProfileCTCReportService:IProfileCTCReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public ProfileCTCReportService(IServiceBase httpService)
        {
            _baseURL = "ProfileCTCReport";
            _httpService = httpService;
        }

        public Task<ProfileCTCReport> GetReportList(int SalaryPeriod, Filter filter)
        {
            return  _httpService.PostAsync<ProfileCTCReport, Filter>($"{_baseURL}/GetProfileCTCReport?SalaryPeriodCode={SalaryPeriod}", filter);
        }

       public Task<DataSet> GetProfileCTCReport_Excel(int SalaryPeriodCode, Filter filter)
        {
            return _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetProfileCTCReport_Excel?SalaryPeriodCode={SalaryPeriodCode}", filter);
        }
    }
}
