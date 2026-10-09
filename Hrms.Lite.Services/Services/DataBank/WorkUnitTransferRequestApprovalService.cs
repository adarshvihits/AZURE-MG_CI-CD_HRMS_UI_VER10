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
    public class WorkUnitTransferRequestApprovalService : IWorkUnitTransferRequestApprovalService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public WorkUnitTransferRequestApprovalService(IServiceBase httpService)
        {
            _baseURL = "WorkUnitTransferRequestApproval";
            _httpService = httpService;
        }
        public async Task<WorkUnitTransferRequest> GetWorkUnitTransferRequestApprovalList()
        {
            return await _httpService.GetAsync<WorkUnitTransferRequest>($"{_baseURL}/GetWorkUnitTransferRequestApprovalList");
        }
        public async Task<WorkUnitTransferRequest> GetEmployeeWorkUnitDetails(Guid EmployeeGI)
        {
            return await _httpService.GetAsync<WorkUnitTransferRequest>($"{_baseURL}/GetEmployeeWorkUnitDetails?EmployeeGI={EmployeeGI}");
        }
        public async Task<ResponseEntity> ApproveOrReject(WorkUnitTransferRequest model)
        {
            return await _httpService.PostAsync<ResponseEntity, WorkUnitTransferRequest>($"{_baseURL}/ApproveOrReject", model);
        }
    }
}
