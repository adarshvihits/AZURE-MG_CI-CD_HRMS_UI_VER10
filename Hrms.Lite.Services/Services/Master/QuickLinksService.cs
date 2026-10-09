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
    public class QuickLinksService: IQuickLinksService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public QuickLinksService(IServiceBase httpService)
        {
            _baseURL = "QuickLinks";
            _baseURL1 = "Common";
            _httpService = httpService;
        }
        public Task<ResponseEntity> ApproveOrReject(QuickLinks model)
        {
            return _httpService.PostAsync<ResponseEntity, QuickLinks>($"{_baseURL}/ApproveOrReject", model);
        }

        public Task<ResponseEntity> Create(QuickLinks input)
        {
            return _httpService.PostAsync<ResponseEntity, QuickLinks>($"{_baseURL}/Create", input);
        }

        public Task<ResponseEntity> Delete(QuickLinks model)
        {
            return _httpService.PostAsync<ResponseEntity, QuickLinks>($"{_baseURL}/Delete", model);
        }

        public async Task<QuickLinks> GetQuickLinksDetails(Guid QuickLinksGI)
        {
            return await _httpService.GetAsync<QuickLinks>($"{_baseURL}/QuickLinksDetails?QuickLinksGI={QuickLinksGI}");
        }

        public async Task<QuickLinks> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<QuickLinks>($"{_baseURL}/List?TabIndex={TabIndex}");
        }

        public async Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return await _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }

        public async Task<QuickLinks> GetCombinationDetails(int TypeCode)
        {
            return await _httpService.GetAsync<QuickLinks>($"{_baseURL}/CombinationDetails?Typecode={TypeCode}");
        }

        public async  Task<ResponseEntity> QuicklinkSave(QuickLinks model)
        {
            return await _httpService.PostAsync<ResponseEntity, QuickLinks>($"{_baseURL}/Create", model);
        }

        public async Task<ResponseEntity> QuicklinkUpdate(QuickLinks model)
        {
            return await _httpService.PostAsync<ResponseEntity, QuickLinks>($"{_baseURL}/Update", model);
        }
    }
}
