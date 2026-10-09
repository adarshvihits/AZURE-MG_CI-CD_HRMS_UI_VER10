using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank.Reports;
using Hrms.Lite.Shared.DataBank.Reports;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank.Reports
{
    public class SocialWallReportService: ISocialWallReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public SocialWallReportService(IServiceBase httpService)
        {
            _baseURL = "SocialWallReport";
            _httpService = httpService;
        }
        public async Task<SocialWallReport> GetSocialWallReportList(int MonthCode)
        {
            return await _httpService.GetAsync<SocialWallReport>($"{_baseURL}/GetSocialWallReportList?MonthCode={MonthCode}");
        }
        public async Task<SocialWallReport> GetEmployeeDetails(int SocialWallCode, int MonthCode)
        {
            return await _httpService.GetAsync<SocialWallReport>($"{_baseURL}/GetEmployeeDetails?SocialWallCode={SocialWallCode}&MonthCode={MonthCode}");
        }
    }
}
