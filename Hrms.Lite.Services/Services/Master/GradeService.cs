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
    public class GradeService : IGradeService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public GradeService(IServiceBase httpService)
        {
            _baseURL = "Grade";
            _baseURL1 = "Common";
            _httpService = httpService;
        }

        public Task<ResponseEntity> ApproveOrReject(Grade model)
        {
            return _httpService.PostAsync<ResponseEntity, Grade>($"{_baseURL}/ApproveOrReject", model);
        }

        public Task<ResponseEntity> Create(Grade Input)
        {
            return _httpService.PostAsync<ResponseEntity, Grade>($"{_baseURL}/Create", Input);
        }

        public Task<ResponseEntity> Delete(Grade Input)
        {
            return _httpService.PostAsync<ResponseEntity, Grade>($"{_baseURL}/Delete", Input);
        }

        public async Task<Grade> GetGradeDetails(Guid GradeGI, Guid LogGI, string Type)
        {
            return await _httpService.GetAsync<Grade>($"{_baseURL}/GetGradeDetails?GradeGI={GradeGI}&&LogGI={LogGI}&&Type={Type}");
        }

        public async Task<Grade> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<Grade>($"{_baseURL}/List?TabIndex={TabIndex}");
        }
        public async Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return await _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }
    }
}
