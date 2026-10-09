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
    public class LeaveCreditDebitService: ILeaveCreditDebitService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public LeaveCreditDebitService(IServiceBase httpService)
        {
            _baseURL = "LeaveCreditDebit";
            _httpService = httpService;
        }
        public async Task<LeaveCreditDebit> LeaveCreditDebitList(int LeavePeriod, int Leave,int Month, int DebitCredit,int LeaveGroup,Filter filter)
        {
            return await _httpService.PostAsync<LeaveCreditDebit,Filter>($"{_baseURL}/LeaveCreditDebitList?LeavePeriod={LeavePeriod}&Leave={Leave}&Month={Month}&DebitCredit={DebitCredit}&LeaveGroup={LeaveGroup}",filter);
        }
        public async Task<LeaveCreditDebit> GetBatchUpDateDetails(int LeavePeriod, int LeaveGroup, int Leave, int Month, int Type)
        {
            return await _httpService.GetAsync<LeaveCreditDebit>($"{_baseURL}/GetBatchUpDateDetails?LeavePeriod={LeavePeriod}&LeaveGroup={LeaveGroup}&Leave={Leave}&Month={Month}&Type={Type}");
        }
        public async Task<LeaveCreditDebit> GetEditDetails(Guid EmployeeGI, int LeavePeriod, int Leave, int Month, int Type)
        {
            return await _httpService.GetAsync<LeaveCreditDebit>($"{_baseURL}/GetEditDetails?EmployeeGI={EmployeeGI}&LeavePeriod={LeavePeriod}&Leave={Leave}&Month={Month}&Type={Type}");
        }
        public async Task<ResponseEntity> LeaveCreditDebitSave(LeaveCreditDebit model)
        {
            return await _httpService.PostAsync<ResponseEntity,LeaveCreditDebit>($"{_baseURL}/LeaveCreditDebitBatchUpdate", model);
        }
        public async Task<ResponseEntity> EmployeeWiseLeaveCreditDebitSave(LeaveCreditDebit model)
        {
            return await _httpService.PostAsync<ResponseEntity, LeaveCreditDebit>($"{_baseURL}/EmployeeWiseCreditDebitSave", model);
        }

      public async Task<LeaveCreditDebit> LeaveCreditDebitHistoryList(int LeavePeriod, int Leave, int Month, int DebitCredit, Filter filter)
        {
            return await _httpService.PostAsync<LeaveCreditDebit, Filter>($"{_baseURL}/LeaveCreditDebitHistoryList?LeavePeriod={LeavePeriod}&Leave={Leave}&Month={Month}&DebitCredit={DebitCredit}", filter);
        }




        public async Task<DataSet> LeaveCreditDebitList_Excel(int LeavePeriod, int Leave, int Month, int DebitCredit,int LeaveGroup, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/LeaveCreditDebitList_Excel?LeavePeriod={LeavePeriod}&Leave={Leave}&Month={Month}&DebitCredit={DebitCredit}&LeaveGroup={LeaveGroup}", filter);
        }
        public async Task<ResponseEntity<List<LeaveCreditDebit>>> ImportFromExcel(LeaveCreditDebit model)
        {
            return await _httpService.PostAsync<ResponseEntity<List<LeaveCreditDebit>>, LeaveCreditDebit>($"{_baseURL}/ImportFromExcel", model);

        }
    }
}
