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
   public class ResignationTypeService : IResignationTypeService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public ResignationTypeService(IServiceBase httpService)
        {
            _baseURL = "ResignationType";
            _httpService = httpService;
        }
        public async Task<ResignationType> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<ResignationType>($"{_baseURL}/List?TabIndex={TabIndex}");
        }
        public Task<ResponseEntity> Create(ResignationType input)
        {
            return _httpService.PostAsync<ResponseEntity, ResignationType>($"{_baseURL}/Create", input);
        }
        public Task<ResignationType> GetResignationTypeDetails(Guid ResignationTypeGI, Guid LogGI, string Type)
        {
            return _httpService.GetAsync<ResignationType>($"{_baseURL}/ResignationTypeDetails?ResignationTypeGI={ResignationTypeGI}&LogGi={LogGI}&Type={Type}");
        }
        public Task<ResponseEntity> ApproveOrReject(ResignationType model)
        {
            return _httpService.PostAsync<ResponseEntity, ResignationType>($"{_baseURL}/ApproveOrReject", model);
        }
        public async Task<ResponseEntity> Delete(ResignationType model)
        {
            return await _httpService.PostAsync<ResponseEntity, ResignationType>($"{_baseURL}/Delete", model);

        }
    }
}
