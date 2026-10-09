using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank
{
    public class HRPlannningService : IHRPlannningService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public HRPlannningService(IServiceBase httpService)
        {
            _baseURL = "HRPlanning";
            _httpService = httpService;
        }

        public async Task<HRPlanning> GetHRPlanningCombination2GridDetails(int HRPlanningCode, int Combination1TrnCode)
        {
            return await _httpService.GetAsync<HRPlanning>($"{_baseURL}/GetHRPlanningDetailsAddNew?HRPlanningCode={HRPlanningCode}&Combination1TrnCode={Combination1TrnCode}");
        }

        public async Task<HRPlanning> GetHRPlanningDetailsForModify(int HRPlanningCode, int Combination1TrnCode, int Combination2TrnCode)
        {
            return await _httpService.GetAsync<HRPlanning>($"{_baseURL}/GetHRPlanningDetailsForModify?HRPlanningCode={HRPlanningCode}&Combination1TrnCode={Combination1TrnCode}&Combination2TrnCode={ Combination2TrnCode}");

        }
       public async Task<HRPlanning> GetCurrentMonthPlanningDetails()
        {
            return await _httpService.GetAsync<HRPlanning>($"{_baseURL}/GetCurrentMonthPlanningDetails");

        }
        public async Task<HRPlanning> GetHRPlanningList(int HRPlanningCode, int Combination1TrnCode, int BasedOn, string monthCode, Filter Filter)
        {
            return await _httpService.PostAsync<HRPlanning,Filter>($"{_baseURL}/GetHRPlanningList?HRPlanningCode={HRPlanningCode}&Combination1TrnCode={Combination1TrnCode}&BasedOn={ BasedOn}&monthCode={ monthCode}",Filter);

        }

        public async Task<HRPlanning> GetPlanningDetails(int HRPlanningCode)
        {
            return await _httpService.GetAsync<HRPlanning>($"{_baseURL}/GetPlanningDetails?HRPlanningCode={HRPlanningCode}");
        }

        public async Task<ResponseEntity> HRPlanningDelete(int? HRPlanningCode, int? Combination1TrnCode, int? Combination2TrnCode)
       {
      return await _httpService.DeleteAsync<ResponseEntity>($"{_baseURL}/HRPlanningDelete?HRPlanningCode={HRPlanningCode}&Combination1={Combination1TrnCode}&Combination2={Combination2TrnCode}");

      
    }
        public async Task<ResponseEntity> HRPlanningModify(HRPlanning model)
        {
            return await _httpService.PostAsync<ResponseEntity, HRPlanning>($"{_baseURL}/HRPlanningModify", model);
        }

        public async Task<ResponseEntity> HRPlanningSave(HRPlanning model)
        {
            return await _httpService.PostAsync<ResponseEntity, HRPlanning>($"{_baseURL}/HRPlanningSave", model);
        
        }

        public async Task<HRPlanning> GetEmployeeList(int HRplanCode, int Combination1Code, int Combination2Code)
        {
            return await _httpService.GetAsync<HRPlanning>($"{_baseURL}/GetNoticePeriodEmployees?HRPlanningCode={HRplanCode}&Combination1TrnCode={Combination1Code}&Combination2TrnCode={Combination2Code}");

        }
    }  
}
