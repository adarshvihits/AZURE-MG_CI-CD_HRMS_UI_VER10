using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.Master;
using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.Master
{
    public class ERPAccountHeadService : IERPAccountHeadService
    {

        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public ERPAccountHeadService(IServiceBase httpService)
        {
            _baseURL = "ERPAccountHead";
            _baseURL1 = "Common";
            _httpService = httpService;
        }

        public Task<ResponseEntity> Create(ERPAccountHead input)
        {
            return _httpService.PostAsync<ResponseEntity, ERPAccountHead>($"{_baseURL}/Create", input);
        }

        public async Task<ERPAccountHead> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<ERPAccountHead>($"{_baseURL}/list?TabIndex={TabIndex}");
        }

        public Task<ERPAccountHead> GetERPAccountHeadDetails(Guid LogGI, Guid ERPAccountHeadGI, string Type)
        {
            return _httpService.GetAsync<ERPAccountHead>($"{_baseURL}/GetERPAccountHeadDetails?LogGI={LogGI}&ERPAccountHeadGI={ERPAccountHeadGI}&Type={Type}");
        }

        public Task<ResponseEntity> ApproveOrReject(ERPAccountHead input)
        {
            return _httpService.PostAsync<ResponseEntity, ERPAccountHead>($"{_baseURL}/ApproveOrReject", input);
        }

        public Task<ResponseEntity> Delete(ERPAccountHead input)
        {
            return _httpService.PostAsync<ResponseEntity, ERPAccountHead>($"{_baseURL}/Delete", input);
        }
        public async Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return await _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }
    }
}
