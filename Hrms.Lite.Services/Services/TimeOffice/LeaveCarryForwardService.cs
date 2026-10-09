using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.TimeOffice;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.TimeOffice
{
    public class LeaveCarryForwardService : ILeaveCarryForwardService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public LeaveCarryForwardService(IServiceBase httpService)
        {
            _baseURL = "LeaveCarryForward";
            _httpService = httpService;
        }
        public async Task<LeaveCarryForward> LeaveCarryForwardList(int LeavePeriodFrom, int LeavePeriodTo, int LeaveGroup, int Leave, int Month,int EntryType, Filter Filter)
        {
            return await _httpService.PostAsync<LeaveCarryForward,Filter>($"{_baseURL}/LeaveCarryForwardList?LeavePeriodFrom={LeavePeriodFrom}&LeavePeriodTo={LeavePeriodTo}" +
                $"&LeaveGroup={LeaveGroup}&Leave={Leave}&Month={Month}&Type={EntryType}",Filter);
        }
        public async Task<LeaveCarryForward> GetBatchUpDateDetails(int LeavePeriodFrom, int LeavePeriodTo, int LeaveGroup, int Leave, int Month)
        {
            return await _httpService.GetAsync<LeaveCarryForward>($"{_baseURL}/GetBatchUpDateDetails?LeavePeriodFrom={LeavePeriodFrom}" +
                $"&LeavePeriodTo={LeavePeriodTo}&LeaveGroup={LeaveGroup}&Leave={Leave}&Month={Month}");
        }
        public async Task<LeaveCarryForward> GetEditDetails(Guid EmployeeGI, int LeavePeriodFrom, int LeavePeriodTo, int LeaveGroup, int Leave, int Month)
        {
            return await _httpService.GetAsync<LeaveCarryForward>($"{_baseURL}/GetEditDetails?EmployeeGI={EmployeeGI}&LeavePeriodFrom={LeavePeriodFrom}" +
                $"&LeavePeriodTo={LeavePeriodTo}&LeaveGroup={LeaveGroup}&Leave={Leave}&Month={Month}");
        }
        public async Task<ResponseEntity> EmployeeWiseCFSave(LeaveCarryForward model)
        {
            return await _httpService.PostAsync<ResponseEntity,LeaveCarryForward>($"{_baseURL}/EmployeeWiseCFSave",model);
        }
        public async Task<ResponseEntity> LeaveCFBatchUpdate(LeaveCarryForward model)
        {
            return await _httpService.PostAsync<ResponseEntity,LeaveCarryForward>($"{_baseURL}/LeaveCFBatchUpdate",model);
        }
        public async Task<ResponseEntity> LeaveCFSave(LeaveCarryForward model)
        {
            return await _httpService.PostAsync<ResponseEntity, LeaveCarryForward>($"{_baseURL}/LeaveCFSave", model);
        }

        async Task<LeaveCarryForward> ILeaveCarryForwardService.LeaveCarryForwardHistoryList(int LeavePeriodFrom, int LeavePeriodTo, int LeaveGroup, int Leave, Filter filter)
        {
            return await _httpService.PostAsync<LeaveCarryForward, Filter>($"{_baseURL}/LeaveCarryForwardHistoryList?LeavePeriodFrom={LeavePeriodFrom}" +
                $"&LeavePeriodTo={LeavePeriodTo}&LeaveGroup={LeaveGroup}&Leave={Leave}", filter);

        }


        public async Task<DataSet> LeaveCarryForwardList_Excel(int LeavePeriodFrom, int LeavePeriodTo, int LeaveGroup, int Leave, int Month, Filter filter, int Type)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/LeaveCarryForwardList_Excel?LeavePeriodFrom={LeavePeriodFrom}&LeavePeriodTo={LeavePeriodTo}" +
                $"&LeaveGroup={LeaveGroup}&Leave={Leave}&Month={Month}&Type={Type}", filter);
        }

        public async Task<DataSet> LeaveCarryForwardHistoryList_Excel(int LeavePeriodFrom, int LeavePeriodTo, int LeaveGroup, int Leave, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/LeaveCarryForwardHistoryList_Excel?LeavePeriodFrom={LeavePeriodFrom}&LeavePeriodTo={LeavePeriodTo}&LeaveGroup={LeaveGroup}&Leave={Leave}", filter);
        }
    }
}
