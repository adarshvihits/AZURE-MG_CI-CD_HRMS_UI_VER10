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
  public  class BankStatementRepostService: IBankStatementRepostService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public BankStatementRepostService(IServiceBase httpService)
        {
            _baseURL = "BankStatementRepost";
            _httpService = httpService;
        }
        public async Task<BankStatementRepost> GetList(int Month,int TabIndex)
        {
            return await _httpService.GetAsync<BankStatementRepost>($"{_baseURL}/GetRepostingList?Month={Month}&TabIndex={TabIndex}");
        }
        public Task<ResponseEntity> Save(BankStatementRepost input)
        {
            return _httpService.PostAsync<ResponseEntity, BankStatementRepost>($"{_baseURL}/Save", input);
        }
        public Task<ResponseEntity> RepostingApproval(BankStatementRepost input)
        {
            return _httpService.PostAsync<ResponseEntity, BankStatementRepost>($"{_baseURL}/RepostingApproval", input);
        }
    }
}
