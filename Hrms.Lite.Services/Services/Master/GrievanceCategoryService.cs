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
    public class GrievanceCategoryService : IGrievanceCategoryService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public GrievanceCategoryService(IServiceBase httpService)
        {
            _baseURL = "GrievanceCategory";
            _baseURL1 = "Common";
            _httpService = httpService;
        }

        public Task<ResponseEntity> ApproveOrReject(GrievanceCategory model)
        {
            return _httpService.PostAsync<ResponseEntity, GrievanceCategory>($"{_baseURL}/ApproveOrReject", model);
        }

        public Task<ResponseEntity> Create(GrievanceCategory input)
        {
            return _httpService.PostAsync<ResponseEntity, GrievanceCategory>($"{_baseURL}/Create", input);
        }

        public async Task<ResponseEntity> Delete(GrievanceCategory model)
        {
            return await _httpService.PostAsync<ResponseEntity, GrievanceCategory>($"{_baseURL}/Delete", model);
        }

        public async Task<GrievanceCategory> GetGrievanceCatDetails(Guid GrievanceGI, Guid LogGI, string Type)
        {
            return await _httpService.GetAsync<GrievanceCategory>($"{_baseURL}/GrievanceCategoryDetails?GrievanceCategoryGI={GrievanceGI}&LogGI={LogGI}&Type={Type}");
        }

        public async Task<GrievanceCategory> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<GrievanceCategory>($"{_baseURL}/List?TabIndex={TabIndex}");
        }

        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }
    }
}
