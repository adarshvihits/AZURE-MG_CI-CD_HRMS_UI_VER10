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
    public class AdditionalResponsibilityService : IAdditionalResponsibilityService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public AdditionalResponsibilityService(IServiceBase httpService)
        {
            _baseURL = "AdditionalResponsibility";
            _httpService = httpService;
        }
        public async Task<AdditionalResponsibility> AdditionalResponsibilityList(string TabIndex)
        {
            return await _httpService.GetAsync<AdditionalResponsibility>($"{_baseURL}/AdditionalResponsibilityList?TabIndex={TabIndex}");
        }
        public async Task<AdditionalResponsibility> GetAdditionalResponsibility(Guid EmployeeGI, string TabIndex)
        {
            return await _httpService.GetAsync<AdditionalResponsibility>($"{_baseURL}/GetAdditionalResponsibility?EmployeeGI={EmployeeGI}&TabIndex={TabIndex}"
            );
        }
        public async Task<ResponseEntity> SaveAdditionalResponsibilityDetails(AdditionalResponsibility Model)
        {
            return await _httpService.PostAsync<ResponseEntity, AdditionalResponsibility>($"{_baseURL}/SaveAdditionalResponsibilityDetails", Model);

        }
        public async Task<File> DownloadDoc(Guid RequestGI, Guid EmployeeGI)
        {
            return await _httpService.GetAsync<File>($"{_baseURL}/DownloadDoc?RequestGI={RequestGI}&EmployeeGI={EmployeeGI}");
        }
    }
}
