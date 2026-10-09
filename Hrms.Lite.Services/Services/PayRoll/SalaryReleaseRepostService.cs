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
   public class SalaryReleaseRepostService: ISalaryReleaseRepostService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public SalaryReleaseRepostService(IServiceBase httpService)
        {
            _baseURL = "SalaryReleaseRepost";
            _httpService = httpService;
        }
        public async Task<SalaryReleaseRepost> GetList(int Month, int TabIndex,int ReportFormat)
        {
            return await _httpService.GetAsync<SalaryReleaseRepost>($"{_baseURL}/GetRepostingList?Month={Month}&TabIndex={TabIndex}&ReportFormat={ReportFormat}");
        }
        public Task<ResponseEntity> Save(SalaryReleaseRepost input)
        {
            return _httpService.PostAsync<ResponseEntity, SalaryReleaseRepost>($"{_baseURL}/Save", input);
        }
        public Task<ResponseEntity> RepostingApproval(SalaryReleaseRepost input)
        {
            return _httpService.PostAsync<ResponseEntity, SalaryReleaseRepost>($"{_baseURL}/RepostingApproval", input);
        }
    }
}
