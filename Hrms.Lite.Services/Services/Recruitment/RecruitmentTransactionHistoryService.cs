using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.Recruitment;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Recruitment.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.Recruitment
{
    public class RecruitmentTransactionHistoryService : IRecruitmentTransactionHistoryService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public RecruitmentTransactionHistoryService(IServiceBase httpService)
        {
            _baseURL = "RecruitmentReports";
            _httpService = httpService;
        }
        public async Task<RecruitmentReports> RecruitmentTransactionHistoryReport(int Tab, string Fromdate, string Todate, int UserCode,int MenuCode, Filter filter)
        {
            return await _httpService.PostAsync<RecruitmentReports,Filter>($"{_baseURL}/RecruitmentTransactionHistoryReport?Tab={Tab}&Fromdate={Fromdate}&Todate={Todate}&UserCode={UserCode}&MenuCode={MenuCode}",filter);
        }

        public async Task<DataSet> RecruitmentTransactionHistoryReport_Excel(int Tab, string Fromdate, string Todate, int UserCode,int MenuCode,Filter filter)
        {
            return await _httpService.PostAsync<DataSet,Filter>($"{_baseURL}/RecruitmentTransactionHistoryReport_Excel?Tab={Tab}&Fromdate={Fromdate}&Todate={Todate}&UserCode={UserCode}&MenuCode={MenuCode}",filter);
        }
    }
}
