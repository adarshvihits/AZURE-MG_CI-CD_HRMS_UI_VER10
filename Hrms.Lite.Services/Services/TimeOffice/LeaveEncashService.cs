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
   
    public class LeaveEncashService : ILeaveEncashService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public LeaveEncashService(IServiceBase httpService)
        {
            _baseURL = "LeaveEncashment";
            _httpService = httpService;
        }
        public async Task<LeaveEncashment> LeaveEncashList(int LeavePeriod, int LeaveGroup, int Leave,int Month, int Type,Filter filter)
        {
            return await _httpService.PostAsync<LeaveEncashment,Filter>($"{_baseURL}/LeaveEncashmentList?LeavePeriod={LeavePeriod}&LeaveGroup={LeaveGroup}&Leave={Leave}&Month={Month}&Type={Type}",filter);
        }
        public async Task<LeaveEncashment> GetBatchUpDateDetails(int LeavePeriod, int LeaveGroup, int Leave, int Month)
        {
            return await _httpService.GetAsync<LeaveEncashment>($"{_baseURL}/GetBatchUpDateDetails?LeavePeriod={LeavePeriod}&LeaveGroup={LeaveGroup}&Leave={Leave}&Month={Month}");
        }
        public async Task<ResponseEntity> LeaveEncashBatchUpdate(LeaveEncashment model)
        {
            return await _httpService.PostAsync<ResponseEntity, LeaveEncashment>($"{_baseURL}/LeaveEncahmentBatchUpdate", model);
        }
        public async Task<LeaveEncashment> GetEditDetails(Guid EmployeeGI, int LeavePeriod, int LeaveGroup, int Leave, int Month)
        {
            return await _httpService.GetAsync<LeaveEncashment>($"{_baseURL}/GetEditDetails?EmployeeGI={EmployeeGI}&LeavePeriod={LeavePeriod}&LeaveGroup={LeaveGroup}&Leave={Leave}&Month={Month}");
        }
        public async Task<ResponseEntity> EmployeeWiseLeaveEncashModify(LeaveEncashment model)
        {
            return await _httpService.PostAsync<ResponseEntity, LeaveEncashment>($"{_baseURL}/EmployeeWiseEncashmentSave", model);
        }

       public async Task<LeaveEncashment> LeaveEncashmentProjection( int LeavePeriod, int Leave, Filter filter)
        {
            return await _httpService.PostAsync<LeaveEncashment, Filter>($"{_baseURL}/LeaveEncashmentProjection?LeavePeriod={LeavePeriod}&Leave={Leave}", filter);
        }

        public  async Task<LeaveEncashment> LeaveEncashmentHistoryList(int LeavePeriod, int LeaveGroup, int Leave, Filter filter)
        {
            return await _httpService.PostAsync<LeaveEncashment, Filter>($"{_baseURL}/LeaveEncashmentHistoryList?LeavePeriod={LeavePeriod}&LeaveGroup={LeaveGroup}&Leave={Leave}", filter);

        }

      public async Task<DataSet> LeaveEncashmentProjection_Excel(int LeavePeriod, int Leave, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/LeaveEncashmentProjection_Excel?LeavePeriod={LeavePeriod}&Leave={Leave}", filter);

        }

        public async Task<DataSet> LeaveEncashmentList_Excel(int LeavePeriod, int LeaveGroup, int Leave, int Month, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/LeaveEncashmentList_Excel?LeavePeriod={LeavePeriod}&&LeaveGroup={LeaveGroup}&Leave={Leave}&&Month={Month}", filter);

        }
    }
   
}
