using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.Master
{
    public class CompanyDocumentsService : ICompanyDocumentsService
    {
        private readonly string _baseURL;


        private readonly IServiceBase _httpService;
        public CompanyDocumentsService(IServiceBase httpService)
        {
            _baseURL = "CompanyDocuments";
            _httpService = httpService;
        }


        public async Task<CompanyDocuments> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<CompanyDocuments>($"{_baseURL}/GetList?TabIndex={TabIndex}");
        }

        public Task<CompanyDocuments> GetCompanyDocumentsDetails(Guid DocumentGI, Guid LogGI, string type)
        {
            return _httpService.GetAsync<CompanyDocuments>($"{_baseURL}/GetCompanyDocumentsDetails?DocumentGI={DocumentGI}&LogGI={LogGI}&Type={type}");
        }

        public Task<ResponseEntity> Create(CompanyDocuments Input)
        {
            return _httpService.PostAsync<ResponseEntity, CompanyDocuments>($"{_baseURL}/Create", Input);
        }
    }
}
