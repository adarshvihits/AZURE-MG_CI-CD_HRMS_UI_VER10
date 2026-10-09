using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank
{
    public class SkillRequisitionService: ISkillRequisitionService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public SkillRequisitionService(IServiceBase httpService)
        {
            _baseURL = "SkillRequisition";
            _httpService = httpService;
        }
        public async Task<SkillRequisition> GetSkillRequisitionList(int SkillTypeCode,int SkillProficiencyCode)
        {
            return await _httpService.GetAsync<SkillRequisition>($"{_baseURL}/GetSkillRequisitionList?SkillTypeCode={SkillTypeCode}&SkillProficiencyCode={SkillProficiencyCode}");
        }
        public async Task<ResponseEntity> SkillRequisitionSave(SkillRequisition model)
        {
            return await _httpService.PostAsync<ResponseEntity, SkillRequisition>($"{_baseURL}/Save", model);
        }
    }
}
