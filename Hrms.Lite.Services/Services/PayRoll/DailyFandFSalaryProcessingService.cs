using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.PayRoll;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.PayRoll
{
   public class DailyFandFSalaryProcessingService: IDailyFandFSalaryProcessingService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public DailyFandFSalaryProcessingService(IServiceBase httpService)
        {
            _baseURL = "DailyFandFSalaryProcessing";
            _httpService = httpService;
        }
        public async Task<DailyFandFSalaryProcessing> GetList(int Month, int PayrollGroup, Filter filter)
        {
            return await _httpService.PostAsync<DailyFandFSalaryProcessing, Filter>($"{_baseURL}/GetDailyFandFList?Month={Month}&PayrollGroup={PayrollGroup}", filter);
        }
       

        public async Task<ResponseEntity<DailyFandFSalaryProcessing>> GetDailyFandF_SalaryProcessing(DailyFandFSalaryProcessing model)
        {
            return await _httpService.PostAsync<ResponseEntity<DailyFandFSalaryProcessing>,DailyFandFSalaryProcessing>($"{_baseURL}/DailyFandFSalaryProcess", model);
        }
    }
}
