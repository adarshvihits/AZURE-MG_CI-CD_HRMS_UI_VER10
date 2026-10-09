using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.Master
{
    public class RecruitmentReasonService : IRecruitmentReasonService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public RecruitmentReasonService(IServiceBase httpService)
        {
            _baseURL = "RecruitmentReason";
            _baseURL1 = "Common";
            _httpService = httpService;

        }
        public async Task<RecruitmentReason> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<RecruitmentReason>($"{_baseURL}/List?TabIndex={TabIndex}");
        }

        public Task<ResponseEntity> Create(RecruitmentReason input)
        {
            return _httpService.PostAsync<ResponseEntity, RecruitmentReason>($"{_baseURL}/Create", input);
        }

        public async Task<RecruitmentReason> GetRecruitmentReasonDetails(Guid RecruitmentReasonGI, Guid LogGI, string Type)
        {
            return await _httpService.GetAsync<RecruitmentReason>($"{_baseURL}/RecruitmentReasonDetails?RecruitmentReasonGI={RecruitmentReasonGI}&&LogGI={LogGI}&&Type={Type}");
        }

        public Task<ResponseEntity> Delete(RecruitmentReason model)
        {
            return _httpService.PostAsync<ResponseEntity, RecruitmentReason>($"{_baseURL}/Delete", model);
        }

        public Task<ResponseEntity> ApproveOrReject(RecruitmentReason model)
        {
            return _httpService.PostAsync<ResponseEntity, RecruitmentReason>($"{_baseURL}/ApproveOrReject", model);
        }
        public async Task<DataSet> GetList_Excel(string TabIndex)
        {
            return await _httpService.GetAsync<DataSet>($"{_baseURL}/GetList_Excel?TabIndex={TabIndex}");
        }
    }
}
