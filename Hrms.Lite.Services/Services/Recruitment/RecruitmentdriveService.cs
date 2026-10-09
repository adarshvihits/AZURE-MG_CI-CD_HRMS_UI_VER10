using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.Recruitment;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Hrms.Lite.Shared.Recruitment;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.Recruitment
{
    public  class RecruitmentdriveService: IRecruitmentdriveService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;

        private readonly IServiceBase _httpService;
        public RecruitmentdriveService(IServiceBase httpService)
        {
            _baseURL = "Recruitmentdrive";
            _baseURL1 = "Common";

            _httpService = httpService;
        }
        public async Task<Recruitmentdrive> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<Recruitmentdrive>($"{_baseURL}/List?TabIndex={TabIndex}");
        }
        public Task<ResponseEntity> Save(Recruitmentdrive model)
        {
            return _httpService.PostAsync<ResponseEntity, Recruitmentdrive>($"{_baseURL}/SaveRecruitmentdrive", model);
        }
        public async Task<Recruitmentdrive> GetResultList(Guid RecruitmentDriveGI, Guid LogGI, string Type)
        {
            return await _httpService.GetAsync<Recruitmentdrive>($"{_baseURL}/RecruitmentdriveDetails?RecruitmentDriveGI={RecruitmentDriveGI}&LogGI={LogGI}&Type={Type}");
        }
        public async Task<ResponseEntity> Delete(Guid gi)
        {
            return await _httpService.DeleteAsync<ResponseEntity>($"{_baseURL}/Delete?gi={gi}");

        }
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }
        public Task<ResponseEntity> ApproveOrReject(Recruitmentdrive model)
        {
            return _httpService.PostAsync<ResponseEntity, Recruitmentdrive>($"{_baseURL}/ApproveOrReject", model);
        }

    }
}
