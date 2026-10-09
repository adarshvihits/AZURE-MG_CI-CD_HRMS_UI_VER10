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
   public class SectionService : ISectionService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public SectionService(IServiceBase httpService)
        {
            _baseURL = "Section";
            _baseURL1 = "Common";
            _httpService = httpService;
        }

        public Task<ResponseEntity> ApproveOrReject(Section model)
        {
            return _httpService.PostAsync<ResponseEntity, Section>($"{_baseURL}/ApproveOrReject", model);
        }

        public Task<ResponseEntity> Create(Section Input)
        {
            return _httpService.PostAsync<ResponseEntity, Section>($"{_baseURL}/Create", Input);
        }

        public Task<ResponseEntity> Delete(Section input)
        {
            return _httpService.PostAsync<ResponseEntity, Section>($"{_baseURL}/Delete", input);
        }

        public async Task<Section> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<Section>($"{_baseURL}/List?TabIndex={TabIndex}");
        }

        public Task<Section> GetSectionDetails(Guid SectionGI, Guid LogGI, string Type)
        {
            return _httpService.GetAsync<Section>($"{_baseURL}/GetSectionDetails?SectionGI={SectionGI}&LogGI={LogGI}&Type={Type}");
        }
        public async Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return await _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }
    }
}
