using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Shared.DataBank;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank
{
    public class DealerUndertakingApprovalService : IDealerUndertakingApprovalService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public DealerUndertakingApprovalService(IServiceBase httpService)
        {
            _baseURL = "DealerUndertaking";
            _httpService = httpService;
        }
        public async Task<DealerUndertakingApproval> DealerUndertakingApprovalList()
        {
            return await _httpService.GetAsync<DealerUndertakingApproval>($"{_baseURL}/DealerUndertakingApprovalList");
        }
        public async Task<DealerUndertakingApproval> GetDealerUndertakingApproval(Guid EmployeeGI)
        {
            return await _httpService.GetAsync<DealerUndertakingApproval>($"{_baseURL}/GetDealerUndertakingApproval?EmployeeGI={EmployeeGI}");
        }
        public async Task<DealerUndertakingApproval> GetDealerUndertakingApprovalDetails(Guid EmployeeGI, int RequestCode)
        {
            return await _httpService.GetAsync<DealerUndertakingApproval>($"{_baseURL}/GetDealerUndertakingApprovalDetails?EmployeeGI={EmployeeGI}&RequestCode={RequestCode}");
        }
        public async Task<ResponseEntity> ApproveDealerUndertakingDetails(DealerUndertakingApproval Model)
        {
            return await _httpService.PostAsync<ResponseEntity, DealerUndertakingApproval>($"{_baseURL}/ApproveDealerUndertakingDetails", Model);

        }
    }
}
