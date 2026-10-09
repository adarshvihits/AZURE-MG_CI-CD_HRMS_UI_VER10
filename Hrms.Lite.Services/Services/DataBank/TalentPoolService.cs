using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Shared.DataBank.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank
{
    public class TalentPoolService : ITalentPoolService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public TalentPoolService(IServiceBase httpService)
        {
            _baseURL = "TalentPool";
            _httpService = httpService;
        }
        async Task<TalentPool> ITalentPoolService.GetClassificationDetails()
        {
            return await _httpService.GetAsync<TalentPool>($"{_baseURL}/GetClassificationDetails");
        }
        async Task<TalentPool> ITalentPoolService.GetTalentPoolDetails(TalentPool model)
        {
            return await _httpService.PostAsync<TalentPool, TalentPool>($"{_baseURL}/GetTalentPoolDetails", model);
        }
        public async Task<DataSet> GetTalentPoolDetails_Excel(TalentPool model)
        {
            return await _httpService.PostAsync<DataSet, TalentPool>($"{_baseURL}/GetTalentPoolDetails_Excel", model);
        }
      public  async Task<TalentPool>GetEmployeeTalentList(string TalentType,int Code)
        {
            return await _httpService.GetAsync<TalentPool>($"{_baseURL}/GetEmployeeTalentList?TalentType={TalentType}&Code={Code}");
        }
        async Task<TalentPool> ITalentPoolService.GetSkillGraph()
        {
            return await _httpService.GetAsync<TalentPool>($"{_baseURL}/GetSkillGraph");
        }
    }
}
