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
  public  class EarnedLeaveEligibilityBasedOnDOJService: IEarnedLeaveEligibilityBasedOnDOJService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public EarnedLeaveEligibilityBasedOnDOJService(IServiceBase httpService)
        {
            _baseURL = "EarnedLeaveEligibilityBasedOnDOJ";
            _httpService = httpService;
        }
        public async Task<EarnedLeaveEligibility> EarnedLeaveEligibilityList(int Leave, int Month,int ConfirmationValue , Filter filter)
        {
            return await _httpService.PostAsync<EarnedLeaveEligibility, Filter>($"{_baseURL}/EarnedLeaveEligibilityList?Leave={Leave}&Month={Month}&ConfirmationValue={ConfirmationValue}", filter);
        }
        public async Task<ResponseEntity> SaveEarnedLeaveEligibility(int Leave, int Month, Filter Filter)
        {
            return await _httpService.PostAsync<ResponseEntity, Filter>($"{_baseURL}/SaveEarnedLeaveEligibility?Leave={Leave}&Month={Month}", Filter);
        }
        public async Task<ResponseEntity> EmployeeWiseSave(EarnedLeaveEligibility model)
        {
            return await _httpService.PostAsync<ResponseEntity, EarnedLeaveEligibility>($"{_baseURL}/EmployeeWiseSave", model);
        }
        public async Task<DataSet> EarnedLeaveEligibilityList_Excel(int Leave, int Month, int ConfirmationValue, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/EarnedLeaveEligibilityList_Excel?Leave={Leave}&Month={Month}&ConfirmationValue={ConfirmationValue}", filter);
        }
        public async Task<ResponseEntity<EarnedLeaveEligibility>> ImportFromExcel(int? Month, int? Leave, EarnedLeaveEligibility model)
        {
            return await _httpService.PostAsync<ResponseEntity<EarnedLeaveEligibility>, EarnedLeaveEligibility>($"{_baseURL}/ImportFromExcel?Month={Month}&Leave={Leave}", model);

        }
    }
}
