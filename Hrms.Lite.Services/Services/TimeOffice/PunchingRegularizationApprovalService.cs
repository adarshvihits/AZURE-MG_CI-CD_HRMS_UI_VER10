using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.TimeOffice;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.TimeOffice
{
    public class PunchingRegularizationApprovalService : IPunchingRegularizationApprovalService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public PunchingRegularizationApprovalService(IServiceBase httpService)
        {
            _baseURL = "PunchingRegularizationApproval";
            _httpService = httpService;
        }



        public async Task<PunchingRegularizationApproval> GetList(Guid EmployeeGI)
        {
            return await _httpService.GetAsync<PunchingRegularizationApproval>($"{_baseURL}/GetList?EmployeeGI={EmployeeGI}");
        }


        public async Task<ResponseEntity> Approve(PunchingRegularizationApproval model)
        {
            return await _httpService.PostAsync<ResponseEntity, PunchingRegularizationApproval>($"{_baseURL}/PunchingRegularizationApproval", model);
        }

        public async Task<ResponseEntity> Reject(PunchingRegularizationApproval model)
        {
            return await _httpService.PostAsync<ResponseEntity, PunchingRegularizationApproval>($"{_baseURL}/PunchingRegularizationReject?", model);
        }
    }
}
