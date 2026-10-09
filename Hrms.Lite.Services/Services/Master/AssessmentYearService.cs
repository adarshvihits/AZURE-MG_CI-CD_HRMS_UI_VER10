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
    public class AssessmentYearService : IAssessmentYearService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;



        private readonly IServiceBase _httpService;
        public AssessmentYearService(IServiceBase httpService)
        {
            _baseURL = "AssessmentYear";
            _baseURL1 = "Common";

            _httpService = httpService;
        }



        public async Task<AssessmentYear> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<AssessmentYear>($"{_baseURL}/List?TabIndex={TabIndex}");
        }


        public async Task<AssessmentYear> AssessmentYearDetails(Guid AssessmentYearGI, Guid LogGI, string Type)
        {
            return await _httpService.GetAsync<AssessmentYear>($"{_baseURL}/AssessmentYearDetails?AssessmentYearGI={AssessmentYearGI}&LogGI={LogGI}&Type={Type}");

        }

        public Task<ResponseEntity> Create(AssessmentYear input)
        {
            return _httpService.PostAsync<ResponseEntity, AssessmentYear>($"{_baseURL}/Create", input);

        }


        public Task<ResponseEntity> Delete(AssessmentYear model)
        { 
            return _httpService.PostAsync<ResponseEntity, AssessmentYear>($"{_baseURL}/Delete", model);
        }

        public Task<ResponseEntity> ApproveOrReject(AssessmentYear model)
        {
            return _httpService.PostAsync<ResponseEntity, AssessmentYear>($"{_baseURL}/ApproveOrReject", model);
        }


        //public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        //{
        //    return _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        //}

    }
}
