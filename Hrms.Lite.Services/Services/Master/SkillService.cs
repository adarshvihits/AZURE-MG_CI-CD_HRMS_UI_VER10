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
    public class SkillService : ISkillService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;


        public SkillService(IServiceBase httpService)
        {
            _baseURL = "Skill";
            _baseURL1 = "Common";
            _httpService = httpService;
        }
        public async Task<Skill> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<Skill>($"{_baseURL}/List?TabIndex={TabIndex}");
        }
        public Task<ResponseEntity> Create(Skill input)
        {
            return _httpService.PostAsync<ResponseEntity, Skill>($"{_baseURL}/Create", input);
        }
        public Task<Skill> GetSkillDetails(Guid SkillGI, Guid LogGI, string Type)
        {
            return _httpService.GetAsync<Skill>($"{_baseURL}/SkillDetails?SkillGI={SkillGI}&LogGi={LogGI}&Type={Type}");
        }
        public Task<ResponseEntity> ApproveOrReject(Skill model)
        {
            return _httpService.PostAsync<ResponseEntity, Skill>($"{_baseURL}/ApproveOrReject", model);
        }
        public async Task<ResponseEntity> Delete(Skill model)
        {
            return await _httpService.PostAsync<ResponseEntity, Skill>($"{_baseURL}/Delete", model);

        }
        public async Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return await _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }
    }
}