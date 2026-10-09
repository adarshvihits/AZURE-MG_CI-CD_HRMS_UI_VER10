using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Shared.DataBank;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank
{
   public class RequestToHRService: IRequestToHRService
    {

        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public RequestToHRService(IServiceBase httpService)
        {
            _baseURL = "RequestToHR";
            _baseURL1 = "Common";
            _httpService = httpService;
        }

        public async Task<RequestToHR> GetList(int radiomode)
        {
            return await _httpService.GetAsync<RequestToHR>($"{_baseURL}/List?radiomode={radiomode}");
        }

        public async Task<RequestToHR> GetRequestToHRDetails(Guid EmployeeGI, int IncidentCode)
        {
            return await _httpService.GetAsync<RequestToHR>($"{_baseURL}/GetRequestToHRDetails?EmployeeGI={EmployeeGI}&RequestCode={IncidentCode}");

        }


        public Task<ResponseEntity> Create(RequestToHR input)
        {
            return _httpService.PostAsync<ResponseEntity, RequestToHR>($"{_baseURL}/Create", input);
        }

    }
}
