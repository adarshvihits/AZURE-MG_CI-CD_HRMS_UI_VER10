using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Shared.DataBank.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank
{
    public class ResumeProfileService : IResumeProfileService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public ResumeProfileService(IServiceBase httpService)
        {
            _baseURL = "ResumeProfile";
            _httpService = httpService;
        }
        public async Task<ResumeDataBank> GetResumeProfile(Guid ResumeGI)
        {
            return await _httpService.GetAsync<ResumeDataBank>($"{_baseURL}/GetResumeProfile?ResumeGI={ResumeGI}");
        }
    }
}
