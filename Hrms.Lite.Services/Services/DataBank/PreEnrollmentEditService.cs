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
    public class PreEnrollmentEditService : IPreEnrollmentEditService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public PreEnrollmentEditService(IServiceBase httpService)
        {
            _baseURL = "PreEnrollmentEdit";
            _httpService = httpService;
        }

        public async Task<PreEnrollment> GetPreEnrollmentEditApprovalDetails(int LogCode)
        {
            return await _httpService.GetAsync<PreEnrollment>($"{_baseURL}/GetApprovalDetails?LogCode={LogCode}");

        }


        public async Task<OnBoarding> GetOnBoardingList(int TabIndex, Filter Filter)
        {
            return await _httpService.PostAsync<OnBoarding, Filter>($"OnBoarding/OnBoardingList?TabIndex={TabIndex}", Filter);
        }
        public async Task<Enrollment> GetPreEnrollmentDetails(Guid EnrollGI)
        {
          return  await _httpService.GetAsync<Enrollment>($"Enrollment/GetPreEnrollment?EnrollGI={EnrollGI}");
        }
        public  async Task<OnBoarding> GetPreEnrollEditApprovalList(Filter Filter)
        {
            return await _httpService.PostAsync<OnBoarding, Filter>($"{_baseURL}/GetPreEnrollEditApprovalList",Filter);

        }
        public async Task<ResponseEntity> Save(PreEnrollment model)
        {
            return await _httpService.PostAsync<ResponseEntity, PreEnrollment>($"{_baseURL}/PreEnrollmentEditSave", model);
        }
        public async Task<ResponseEntity> ApproveReject(PreEnrollment model)
        {
            return await _httpService.PostAsync<ResponseEntity, PreEnrollment>($"{_baseURL}/ApproveReject", model);
        }
    }
}
