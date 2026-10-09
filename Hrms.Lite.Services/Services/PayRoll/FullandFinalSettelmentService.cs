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
    public class FullandFinalSettelmentService: IFullandFinalSettelmentService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public FullandFinalSettelmentService(IServiceBase httpService)
        {
            _baseURL = "FullandFinalSettelment";
            _httpService = httpService;
        }

        public async Task<FullandFinalSettelment> GetEmployeeList_FullandFinal(Filter filter, int PayrollGroup, int Month)
        {
            return await _httpService.PostAsync<FullandFinalSettelment, Filter>($"{_baseURL}/GetEmployeeList_FullandFinal?PayrollGroup={PayrollGroup}&Month={Month}", filter);
        }
        public async Task<FullandFinalSettelment> GetEmployeeList(Filter filter)
        {
            return await _httpService.PostAsync<FullandFinalSettelment, Filter>($"{_baseURL}/GetEmployeeList", filter);
        }
        public async Task<FullandFinalSettelment> GetHRMSVarificationandApprovalEmployeeList(string Mode, Filter filter, int Month)
        {
            return await _httpService.PostAsync<FullandFinalSettelment, Filter>($"{_baseURL}/GetHRMSVarificationandApprovalEmployeeList?Mode={Mode}&Month={Month}", filter);
        }
        public async Task<FullandFinalSettelment> GetDetails(Guid EmployeeGI, string Mode)
        {
            return await _httpService.GetAsync<FullandFinalSettelment>($"{_baseURL}/GetDetails?EmployeeGI={EmployeeGI}&Mode={Mode}");
        }

        public async Task<FullandFinalSettelment> GetDetailsFullandFinal(Guid EmployeeGI,int Month, string Mode)
        {
            return await _httpService.GetAsync<FullandFinalSettelment>($"{_baseURL}/GetDetailsFullandFinal?EmployeeGI={EmployeeGI}&Mode={Mode}&Month={Month}");
        }

        public async Task<FullandFinalSettelment> GetHeldAmountDetails(Guid EmployeeGI)
        {
            return await _httpService.GetAsync<FullandFinalSettelment>($"{_baseURL}/GetHeldAmountDetails?EmployeeGI={EmployeeGI}");
        }
        public async Task<FullandFinalSettelment> GetDetailsProcess(Guid EmployeeGI, decimal EncashmentLeaves,decimal NoticeDedDays)
        {
            return await _httpService.GetAsync<FullandFinalSettelment>($"{_baseURL}/GetDetails_Process?EmployeeGI={EmployeeGI}&EncashmentLeaves={EncashmentLeaves}&NoticeDedDays={NoticeDedDays}");
        }
        public async Task<ResponseEntity> SaveFullAndFinalSettelment(FullandFinalSettelment model)
        {
            return await _httpService.PostAsync<ResponseEntity, FullandFinalSettelment>($"{_baseURL}/FullandFinalSettelmentSave", model);
        }
        public async Task<ResponseEntity> FullandFinalSettelmentVarification(FullandFinalSettelment model)
        {
            return await _httpService.PostAsync<ResponseEntity, FullandFinalSettelment>($"{_baseURL}/FullandFinalSettelmentVarification_Approve", model);
        }

        async Task<FullandFinalSettelment> IFullandFinalSettelmentService.GetFandFDeleteList( Filter filter)
        {
            return await _httpService.PostAsync<FullandFinalSettelment, Filter>($"{_baseURL}/GetFandFDeleteList", filter);

        }
        async Task<DataSet> IFullandFinalSettelmentService.GetFandFDeleteList_Excel(Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetFandFDeleteList_Excel", filter);

        }
        
        async Task<ResponseEntity> IFullandFinalSettelmentService.FullandFinalSettelmentDelete(FullandFinalSettelment model)
        {
            return await _httpService.PostAsync<ResponseEntity, FullandFinalSettelment>($"{_baseURL}/FullandFinalSettelmentDelete", model);

        }

        public async Task<FullandFinalSettelment> GetTDSEmployeeDetailsAndProcess(Guid EmployeeGI, int SalaryPeriodCode)
        {
            return await _httpService.GetAsync<FullandFinalSettelment>($"{_baseURL}/GetTDSEmployeeDetailsAndProcess?EmployeeGI={EmployeeGI}&SalaryPeriodCode={SalaryPeriodCode}");
        }
    }
}
