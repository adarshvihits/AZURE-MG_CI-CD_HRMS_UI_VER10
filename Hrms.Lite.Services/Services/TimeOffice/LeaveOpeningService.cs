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
    public class LeaveOpeningService : ILeaveOpeningService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public LeaveOpeningService(IServiceBase httpService)
        {
            _baseURL = "LeaveOpening";
            _httpService = httpService;
        }



        public async Task<DataSet> LeaveOpeningHistoryList_Excel(int LeavePeriod, int LeaveGroup, int Leave, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/LeaveOpeningHistoryList_Excel?LeavePeriod={LeavePeriod}&LeaveGroup={LeaveGroup}&Leave={Leave}", filter);
        }
        public async Task<LeaveOpening> LeaveOpeningList(int LeavePeriod, int LeaveGroup, int Leave,int Type,Filter Filter)
        {
            return await _httpService.PostAsync<LeaveOpening,Filter>($"{_baseURL}/LeaveOpeningList?LeavePeriod={LeavePeriod}&LeaveGroup={LeaveGroup}&Leave={Leave}&Type={Type}",Filter);
        }
        public async Task<LeaveOpening> GetBatchUpDateDetails(int LeavePeriod, int LeaveGroup, int Leave)
        {
            return await _httpService.GetAsync<LeaveOpening>($"{_baseURL}/GetBatchUpDateDetails?LeavePeriod={LeavePeriod}&LeaveGroup={LeaveGroup}&Leave={Leave}");
        }
        public async Task<LeaveOpening> GetEditDetails(Guid EmployeeGI, int LeavePeriod, int LeaveGroup, int Leave)
        {
            return await _httpService.GetAsync<LeaveOpening>($"{_baseURL}/GetEditDetails?EmployeeGI={EmployeeGI}&LeavePeriod={LeavePeriod}&LeaveGroup={LeaveGroup}&Leave={Leave}");
        }
        public async Task<ResponseEntity> LeaveOpeningSave(LeaveOpening model)
        {
            return await _httpService.PostAsync<ResponseEntity, LeaveOpening>($"{_baseURL}/LeaveOpeningSave", model);
        }
        public async Task<ResponseEntity> EmployeeWiseLeaveOpeningSave(LeaveOpening model)
        {
            return await _httpService.PostAsync<ResponseEntity, LeaveOpening>($"{_baseURL}/EmployeeWiseLeaveOpeningSave", model);
        }
        public async Task<ResponseEntity> LeaveOpeningBatchUpdate(LeaveOpening model)
        {
            return await _httpService.PostAsync<ResponseEntity, LeaveOpening>($"{_baseURL}/LeaveOpeningBatchUpdate", model);
        }

    public async Task<LeaveOpening> LeaveOpeningHistoryList(int LeavePeriod, int LeaveGroup, int Leave, Filter filter)
        {
            return await _httpService.PostAsync<LeaveOpening, Filter>($"{_baseURL}/LeaveOpeningHistoryList?LeavePeriod={LeavePeriod}&LeaveGroup={LeaveGroup}&Leave={Leave}", filter);

        }

      public async Task<LeaveOpening> LeaveOpeningDeleteList(int LeavePeriod, int LeaveGroup, int Leave, Filter filter)
        {
            return await _httpService.PostAsync<LeaveOpening, Filter>($"{_baseURL}/LeaveOpeningDeleteList?LeavePeriod={LeavePeriod}&LeaveGroup={LeaveGroup}&Leave={Leave}", filter);

        }


        public async Task<ResponseEntity> Delete(LeaveOpening model)
        {
            return await _httpService.PostAsync<ResponseEntity, LeaveOpening>($"{_baseURL}/EmployeeWiseLeaveOpeningDelete", model);
        }


        public async Task<ResponseEntity> DeleteAll(LeaveOpening model)
        {
            return await _httpService.PostAsync<ResponseEntity, LeaveOpening>($"{_baseURL}/LeaveOpeningBatchUpdateDelete", model);
        }

        public async  Task<DataSet> LeaveOpeningList_Excel(int LeavePeriod, int LeaveGroup, int Leave,int Type, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/LeaveOpeningList_Excel?LeavePeriod={LeavePeriod}&LeaveGroup={LeaveGroup}&Leave={Leave}&&Type={Type}", filter);

        }
        public async Task<ResponseEntity<List<LeaveOpening>>> ImportFromExcel(LeaveOpening model)
        {
            return await _httpService.PostAsync<ResponseEntity<List<LeaveOpening>>, LeaveOpening>($"{_baseURL}/ImportFromExcel", model);

        }
    }
}
