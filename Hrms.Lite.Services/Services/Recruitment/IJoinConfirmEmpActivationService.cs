using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Recruitment
{
    public class JoinConfirmEmpActivationService : IJoinConfirmEmpActivationService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public JoinConfirmEmpActivationService(IServiceBase httpService)
        {
            _baseURL = "JoinConfirmEmpActivation";
            _httpService = httpService;
        }
      public  async Task<PreEnrollment> GetPreEnrollJoiningConfirmDetails(Guid PreEnrollGI)
        {
            return await _httpService.GetAsync<PreEnrollment>($"{_baseURL}/GetPreEnrollJoiningConfirmDetails?PreEnrollGI={PreEnrollGI}");

        }

        public async Task<ResponseEntity> PreEnrollJoiningConfirm(PreEnrollment model)
        {
            return await _httpService.PostAsync<ResponseEntity, PreEnrollment>($"{_baseURL}/PreEnrollJoiningConfirm", model);

        }

      public  async Task<OnBoarding> PreEnrollJoiningConfirmList(Filter Filter)
        {
            return await _httpService.PostAsync<OnBoarding, Filter>($"{_baseURL}/PreEnrollJoiningConfirmList", Filter);
        }

        public async Task<OnBoarding> GetPreEnrollEmployeeActivationList(Filter Filter)
        {
            return await _httpService.PostAsync<OnBoarding, Filter>($"{_baseURL}/GetPreEnrollEmployeeActivationList", Filter);
        }
    }
}
