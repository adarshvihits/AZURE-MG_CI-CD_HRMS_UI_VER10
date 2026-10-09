using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.PayRoll;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.PayRoll
{
    public class PreFullandFinalSettlementService : IPreFullandFinalSettlementService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public PreFullandFinalSettlementService(IServiceBase httpService)
        {
            _baseURL = "PreFullandFinalSettlement";
            _httpService = httpService;
        }
        public async Task<PreFullandFinalSettlement> GetList(int Month, int PayrollGroup, Filter filter)
        {
            return await _httpService.PostAsync<PreFullandFinalSettlement, Filter>($"{_baseURL}/GetPreFullandFinalSettlementList?Month={Month}&PayrollGroup={PayrollGroup}", filter);
        }
        public async Task<PreFullandFinalSettlement> GetPreFandFDetails(Guid EmployeeGI,int Month)
        {
            return await _httpService.GetAsync<PreFullandFinalSettlement>($"{_baseURL}/GetPreFandFDetails?EmployeeGI={EmployeeGI}&Month={Month}");
        }
        public async Task<ResponseEntity> PreFandFSave(PreFullandFinalSettlement model)
        {
            return await _httpService.PostAsync<ResponseEntity, PreFullandFinalSettlement>($"{_baseURL}/PreFandFSave", model);
        }
        public async Task<DataSet> GetList_EXCEL(int Month, int PayrollGroupCode, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetList_EXCEL?Month=" + $"{Month}&PayrollGroupCode={PayrollGroupCode}", filter);

        }
        #region Approval
        public async Task<PreFullandFinalSettlement> GetApprovalList(int Month, int PayrollGroup, Filter filter)
        {
            return await _httpService.PostAsync<PreFullandFinalSettlement, Filter>($"{_baseURL}/GetPreFullandFinalApprovalList?Month={Month}&PayrollGroup={PayrollGroup}", filter);
        }
        public async Task<PreFullandFinalSettlement> PreFandFApprovalDetails(Guid EmployeeGI,int Month)
        {
            return await _httpService.GetAsync<PreFullandFinalSettlement>($"{_baseURL}/PreFandFApprovalDetails?EmployeeGI={EmployeeGI}&Month={Month}");
        }
        public async Task<ResponseEntity> PreFandFApproveOrReject(Guid PreSettlementGI, string Mode,string Remarks)
        {
            return await _httpService.GetAsync<ResponseEntity>($"{_baseURL}/PreFandFApproveOrReject?PreSettlementGI={PreSettlementGI}&Mode={Mode}&Remarks={Remarks}");
        }
        public async Task<DataSet> GetApprovalList_EXCEL(int Month, int PayrollGroupCode, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetApprovalList_EXCEL?Month=" + $"{Month}&PayrollGroupCode={PayrollGroupCode}", filter);

        }

        #endregion
    }
}

