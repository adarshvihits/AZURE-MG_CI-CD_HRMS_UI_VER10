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
    public class PeriodicLeaveCreditService: IPeriodicLeaveCreditService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public PeriodicLeaveCreditService(IServiceBase httpService)
        {
            _baseURL = "PeriodicLeaveCredit";
            _httpService = httpService;
        }
       public async Task<PeriodicLeaveCredit> PeriodicLeaveCreditList(int LeavePeriod, int LeaveGroup, int Leave, Filter filter, int Month)
        {
            return await _httpService.PostAsync<PeriodicLeaveCredit, Filter>($"{_baseURL}/PeriodicLeaveCreditList?LeavePeriod={LeavePeriod}&LeaveGroup={LeaveGroup}&Leave={Leave}&Month={Month}", filter);
        }
        public async Task<ResponseEntity> SavePeriodicLeaveCredit(int LeavePeriod, int LeaveGroup, int Leave, Filter Filter, int Month)
        {
            return await _httpService.PostAsync<ResponseEntity, Filter>($"{_baseURL}/SavePeriodicLeaveCredit?LeavePeriod={LeavePeriod}&LeaveGroup={LeaveGroup}&Leave={Leave}&Month={Month}", Filter);
        }
        public async Task<DataSet> PeriodicLeaveCreditList_Excel(int LeavePeriod, int LeaveGroup, int Leave, int Month, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/PeriodicLeaveCreditList_Excel?LeavePeriod={LeavePeriod}&LeaveGroup={LeaveGroup}&Leave={Leave}&Month={Month}", filter);
        }
        public async Task<PeriodicLeaveCredit> PeriodicLeaveCreditHistoryList(int LeavePeriod, int Leave, int Month, int LeaveGroup, Filter filter)
        {
            return await _httpService.PostAsync<PeriodicLeaveCredit, Filter>($"{_baseURL}/PeriodicLeaveCreditHistoryList?LeavePeriod={LeavePeriod}&Leave={Leave}&Month={Month}&LeaveGroup={LeaveGroup}", filter);
        }
        public async Task<DataSet> PeriodicLeaveCreditHistoryList_Excel(int LeavePeriod, int LeaveGroup, int Leave, int Month, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/PeriodicLeaveCreditHistoryList_Excel?LeavePeriod={LeavePeriod}&LeaveGroup={LeaveGroup}&Leave={Leave}&Month={Month}", filter);
        }
    }
}
