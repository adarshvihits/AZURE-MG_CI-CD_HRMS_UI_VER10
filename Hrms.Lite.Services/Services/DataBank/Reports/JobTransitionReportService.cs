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
    public class JobTransitionReportService : IJobTransitionReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public JobTransitionReportService(IServiceBase httpService)
        {
            _baseURL = "JobTransitionReport";
            _httpService = httpService;
        }

        public async Task<JobTransitionReport> GetJobTransitionReport(string fromdate, string todate, int TranType, JobTransitionReport model)
        {
            return await _httpService.PostAsync<JobTransitionReport, JobTransitionReport>($"{_baseURL}/GetJobTransitionReportDetails?From={fromdate}&To={todate}&TransitionType={TranType}", model);
        }

        public async Task<DataSet> GetJobTransitionReportDetails_Excel(string fromdate, string todate, int TranType, JobTransitionReport model)
        {
            return await _httpService.PostAsync<DataSet, JobTransitionReport>($"{_baseURL}/GetJobTransitionReportDetails_Excel?From={fromdate}&To={todate}&TransitionType={TranType}", model);
        }

        public async Task<JobTransitionReport> GetGridSettingDetails()
        {
            return await _httpService.GetAsync<JobTransitionReport>($"{_baseURL}/GetGridSettings");

        }

      public async Task<JobTransitionReport> GetJobTransitionReportDetails(string From, string To, int TransitionType, JobTransitionReport model)
        {
            return await _httpService.PostAsync<JobTransitionReport, JobTransitionReport>($"{_baseURL}/GetJobTransitionReportDetails?From={From}&To={To}&TransitionType={TransitionType}", model);

        }
    }
}
