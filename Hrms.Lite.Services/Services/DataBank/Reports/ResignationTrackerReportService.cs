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
    public class ResignationTrackerReportService : IResignationTrackerReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public ResignationTrackerReportService(IServiceBase httpService)
        {
            _baseURL = "ResignationTracker";
            _httpService = httpService;
        }
        public async Task<ResignationTracker> GetResignationTrackingList(Filter model,string status)
        {
            return await _httpService.PostAsync<ResignationTracker, Filter>($"{_baseURL}/GetResignationTrackingList?status={status}", model);
        }
        public async Task<ResignationTracker> GetResignationTracker(Guid ResignationGI)
        {
            return await _httpService.GetAsync<ResignationTracker>($"{_baseURL}/GetResignationTracker?ResignationGI={ResignationGI}");
        }

        public async Task<ResignationTracker> GetTrackerDetails(Guid ResignationGI, Guid EmployeeGI)
        {
            return await _httpService.GetAsync<ResignationTracker>($"{_baseURL}/GetTrackerDetails?ResignationGI={ResignationGI}&EmployeeGI={EmployeeGI}");
        }

        public async Task<ResignationTracker> GetWithdrawTrackerDetails(Guid ResignationGI, Guid EmployeeGI)
        {
            return await _httpService.GetAsync<ResignationTracker>($"{_baseURL}/GetWithdrawTrackerDetails?ResignationGI={ResignationGI}&EmployeeGI={EmployeeGI}");
        }
        public async Task<DataSet> GetResignationTrackingList_Excel(Filter filter,string status)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetResignationTrackingList_Excel?status={status}", filter);
        }
    }
}
