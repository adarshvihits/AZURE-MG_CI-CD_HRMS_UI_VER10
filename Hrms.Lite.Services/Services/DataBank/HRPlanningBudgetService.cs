using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DashBoard;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Shared.DashBoard;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank
{
    public class HRPlanningBudgetService : IHRPlanningBudgetService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public HRPlanningBudgetService(IServiceBase httpService)
        {
            _baseURL = "HRPlanningBudget";
            _httpService = httpService;
        }
        #region BudgetUpload
        public async Task<HRPlanningBudget> GetBudgetUploadList(int HRPlanningCode, int DesignationCode)
        {
            return await _httpService.GetAsync<HRPlanningBudget>($"{_baseURL}/GetBudgetUploadList?HRPlanningCode={HRPlanningCode}&DesignationCode={DesignationCode}");
        }

       public async Task<ResponseEntity<List<HRPlanningBudgetDetails>>> ImportFromExcel(HRPlanningBudget model)
        {
            return await _httpService.PostAsync<ResponseEntity<List<HRPlanningBudgetDetails>>, HRPlanningBudget>($"{_baseURL}/ImportFromExcel", model);

        }

        #endregion

        #region BudgetApprove
       public async Task<HRPlanningBudget> GetBudgetUploadApprovalList(int HRPlanningCode, int DesignationCode)
        {
            return await _httpService.GetAsync<HRPlanningBudget>($"{_baseURL}/GetBudgetUploadApprovalList?HRPlanningCode={HRPlanningCode}&DesignationCode={DesignationCode}");

        }

       public async Task<HRPlanningBudget> GetBudgetUploadApprovalMainList()
        {
            return await _httpService.GetAsync<HRPlanningBudget>($"{_baseURL}/GetBudgetUploadApprovalMainList");

        }

    public async Task<ResponseEntity> BudgetApprove(HRPlanningBudget model, string Mode)
        {
            return await _httpService.PostAsync<ResponseEntity, HRPlanningBudget>($"{_baseURL}/BudgetApprove?Mode={Mode}", model);
        }
        #endregion
        #region Variance
        public async Task<HRPlanningBudget> GetBudgetVarianceReport(int HRPlanningCode, int DesignationCode, int Type)
        {
            return await _httpService.GetAsync<HRPlanningBudget>($"{_baseURL}/GetBudgetVarianceReport?HRPlanningCode={HRPlanningCode}&DesignationCode={DesignationCode}&Type={Type}");
        }
        public async Task<ResponseEntity> CreateMRF(HRPlanningBudget model)
        {
            return await _httpService.PostAsync<ResponseEntity, HRPlanningBudget>($"{_baseURL}/CreateMRF", model);
        }
        #endregion
    }
}
