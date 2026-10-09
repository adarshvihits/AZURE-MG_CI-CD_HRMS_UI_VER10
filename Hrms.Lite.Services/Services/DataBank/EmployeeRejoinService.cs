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
    public class EmployeeRejoinService : IEmployeeRejoinService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public EmployeeRejoinService(IServiceBase httpService)
        {
            _baseURL = "EmployeeRejoin";
            _httpService = httpService;
        }
        public async Task<OnBoarding> GetRejoinList(Filter Filter)
        {
            return await _httpService.PostAsync<OnBoarding, Filter>($"{_baseURL}/GetRejoinList", Filter);
        }
        public async Task<JoiningConfirmation> GetRejoinEmployeeDetails(Guid EmployeeGI)
        {
            return await _httpService.GetAsync<JoiningConfirmation>($"{_baseURL}/GetRejoinEmployeeDetails?EmployeeGI={EmployeeGI}");
        }
        public async Task<ResponseEntity<string>> Save(Enrollment model)
        {
            return await _httpService.PostAsync<ResponseEntity<string>, Enrollment>($"{_baseURL}/Save", model);
        }

    }
}
