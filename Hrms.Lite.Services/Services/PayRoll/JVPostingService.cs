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
 public   class JVPostingService: IJVPostingService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public JVPostingService(IServiceBase httpService)
        {
            _baseURL = "JVPosting";
            _httpService = httpService;
        }
        public async Task<JVPosting> GetList(int Month, int PayrollGroup, int Type,  Filter filter)
        {
            return await _httpService.PostAsync<JVPosting, Filter>($"{_baseURL}/GetJVPostingList?Month={Month}&PayrollGroup={PayrollGroup}&Type={Type}", filter);
        }
        public Task<ResponseEntity> Save(JVPosting input)
        {
            return _httpService.PostAsync<ResponseEntity, JVPosting>($"{_baseURL}/Save", input);
        }
        public async Task<ResponseEntity<List<JVCheckList>>> CheckJVLinking(int MonthCode)
        {
            return await _httpService.GetAsync<ResponseEntity<List<JVCheckList>>>($"{_baseURL}/CheckJVLinking?MonthCode={MonthCode}");
        }
    }
}
