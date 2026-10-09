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
   public class FieldOfStudyService : IFieldOfStudyService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public FieldOfStudyService(IServiceBase httpService)
        {
            _baseURL = "FieldOfStudy";
            _httpService = httpService;
        }
        public async Task<FieldOfStudy> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<FieldOfStudy>($"{_baseURL}/List?TabIndex={TabIndex}");
        }
        public Task<ResponseEntity> Create(FieldOfStudy input)
        {
            return _httpService.PostAsync<ResponseEntity, FieldOfStudy>($"{_baseURL}/Create", input);
        }
        public Task<FieldOfStudy> GetFieldOfStudyDetails(Guid FieldOfStudyGI, Guid LogGI, string Type)
        {
            return _httpService.GetAsync<FieldOfStudy>($"{_baseURL}/FieldOfStudyDetails?FieldOfStudyGI={FieldOfStudyGI}&LogGi={LogGI}&Type={Type}");
        }
        public Task<ResponseEntity> ApproveOrReject(FieldOfStudy model)
        {
            return _httpService.PostAsync<ResponseEntity, FieldOfStudy>($"{_baseURL}/ApproveOrReject", model);
        }
        public async Task<ResponseEntity> Delete(FieldOfStudy model)
        {
            return await _httpService.PostAsync<ResponseEntity, FieldOfStudy>($"{_baseURL}/Delete", model);

        }
    }
}
