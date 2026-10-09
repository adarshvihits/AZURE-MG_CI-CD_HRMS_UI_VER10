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
    public class MRFVsResumeService : IMRFVsResumeService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public MRFVsResumeService(IServiceBase httpService)
        {
            _baseURL = "MRFVsResume";
            _httpService = httpService;
        }
        public async Task<MRFVsResume> GetVacancyList(int Type)
        {
            return await _httpService.GetAsync<MRFVsResume>($"{_baseURL}/GetVacancyList?Type={Type}");
        }
        public async Task<MRFVsResume> GetMRFVsResumeList(int Type, Guid VacancyGI, ResumeFilter filter)
        {
            return await _httpService.PostAsync<MRFVsResume, ResumeFilter>($"{_baseURL}/GetMRFVsResumeList?Type={Type}&VacancyGI={VacancyGI}",filter);
        }
        public async Task<MRFVsResume> GeJobTitle(Guid VacancyGI)
        {
            return await _httpService.GetAsync<MRFVsResume>($"{_baseURL}/GeJobTitle?VacancyGI={VacancyGI}");
        }
        public async Task<ResponseEntity> Create(MRFVsResume model)
        {
            return await _httpService.PostAsync<ResponseEntity, MRFVsResume>($"{_baseURL}/Create", model);
        }
    }
}
