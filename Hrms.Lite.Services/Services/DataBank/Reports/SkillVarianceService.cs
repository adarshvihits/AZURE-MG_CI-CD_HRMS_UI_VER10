using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank.Reports;
using Hrms.Lite.Shared.DataBank.Reports;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank.Reports
{
    public class SkillVarianceService: ISkillVarianceService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public SkillVarianceService(IServiceBase httpService)
        {
            _baseURL = "SkillVarianceReport";
            _httpService = httpService;
        }

        public async Task<SkillVariance> GetSkillRequisitionList(int SkillTypeCode)
        {
            return await _httpService.GetAsync<SkillVariance>($"{_baseURL}/GetList?SkillTypeCode={SkillTypeCode}");
        }
    }
}
