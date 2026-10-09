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
    public class SkillTypeService:ISkillTypeService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public SkillTypeService(IServiceBase httpService)
        {
            _baseURL = "SkillType";
            _baseURL1 = "Common";
            _httpService = httpService;
        }
        public async Task<SkillType> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<SkillType>($"{_baseURL}/List?TabIndex={TabIndex}");
        }
        public Task<ResponseEntity> Save(SkillType input)
        {
            return _httpService.PostAsync<ResponseEntity, SkillType>($"{_baseURL}/Create", input);
        }
        public Task<SkillType> GetSkillTypeDetails(Guid SkillTypeGI, Guid LogGI, string Type)
        {
            return _httpService.GetAsync<SkillType>($"{_baseURL}/SkillTypeDetails?SkillTypeGI={SkillTypeGI}&LogGi={LogGI}&Type={Type}");
        }
        public Task<ResponseEntity> ApproveOrReject(SkillType model)
        {
            return _httpService.PostAsync<ResponseEntity, SkillType>($"{_baseURL}/ApproveOrReject", model);
        }
        public async Task<ResponseEntity> Delete(SkillType model)
        {
            return await _httpService.PostAsync<ResponseEntity, SkillType>($"{_baseURL}/Delete", model);

        }

        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }
    }
}
