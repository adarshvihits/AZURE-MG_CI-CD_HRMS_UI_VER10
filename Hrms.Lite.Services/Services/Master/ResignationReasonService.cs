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
    public class ResignationReasonService : IResignationReasonService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public ResignationReasonService(IServiceBase httpService)
        {
            _baseURL = "ResignationReason";
            _baseURL1 = "Common";
            _httpService = httpService;
        }

        public Task<ResponseEntity> ApproveOrReject(ResignationReason model)
        {
            return _httpService.PostAsync<ResponseEntity, ResignationReason>($"{_baseURL}/ApproveOrReject", model);
        }

        public Task<ResponseEntity> Create(ResignationReason input)
        {
            return _httpService.PostAsync<ResponseEntity, ResignationReason>($"{_baseURL}/Create", input);
        }

        public async Task<ResponseEntity> Delete(ResignationReason model)
        {
            return await _httpService.PostAsync<ResponseEntity, ResignationReason>($"{_baseURL}/Delete", model);

        }

        public async Task<ResignationReason> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<ResignationReason>($"{_baseURL}/List?TabIndex={TabIndex}");
        }

        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");

        }

        public Task<ResignationReason> GetResignationReasonDetails(Guid ResignationReasonGI, Guid LogGI, string Type)
        {
            return _httpService.GetAsync<ResignationReason>($"{_baseURL}/GetResignationReasonDetails?ResignationReasonGI={ResignationReasonGI}&LogGI={LogGI}&Type={Type}");
        }
    }
}



