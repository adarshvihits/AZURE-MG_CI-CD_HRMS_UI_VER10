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
    public class EarnedLeaveEligibilityService: IEarnedLeaveEligibilityService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public EarnedLeaveEligibilityService(IServiceBase httpService)
        {
            _baseURL = "EarnedLeaveEligibility";
            _httpService = httpService;
        }
        public async Task<EarnedLeaveEligibility> EarnedLeaveEligibilityList(int Leave, int Month, Filter filter)
        {
            return await _httpService.PostAsync<EarnedLeaveEligibility, Filter>($"{_baseURL}/EarnedLeaveEligibilityList?Leave={Leave}&Month={Month}", filter);
        }
        public async Task<ResponseEntity> SaveEarnedLeaveEligibility(int Leave, int Month, Filter Filter)
        {
            return await _httpService.PostAsync<ResponseEntity, Filter>($"{_baseURL}/SaveEarnedLeaveEligibility?Leave={Leave}&Month={Month}", Filter);
        }
        public async Task<DataSet> EarnedLeaveEligibilityList_Excel(int Leave, int Month, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/EarnedLeaveEligibilityList_Excel?Leave={Leave}&Month={Month}", filter);
        }
    }
}
