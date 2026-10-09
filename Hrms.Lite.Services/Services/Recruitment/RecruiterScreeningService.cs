using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.Recruitment;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Recruitment;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.Recruitment
{
    public class RecruiterScreeningService : IRecruiterScreeningService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public RecruiterScreeningService(IServiceBase httpService)
        {
            _baseURL = "RecruiterScreening";
            _httpService = httpService;
        }

        public async Task<List<RecruiterScreeningVacancy>> GetOpenVacancies(int? BusinessTypeCode, int? DepartmentCode, int? SectionCode, int? JobCode)
        {
            return await _httpService.GetAsync<List<RecruiterScreeningVacancy>>($"{_baseURL}/GetOpenVacancies?BusinessTypeCode={BusinessTypeCode}&DepartmentCode={DepartmentCode}&SectionCode={SectionCode}&JobCode={JobCode}");
        }

        public async Task<RecruiterScreeningCandidates> GetCandidates(int LocationCode, int DepartmentCode, int SectionCode, int GradeCode)
        {
            return await _httpService.GetAsync<RecruiterScreeningCandidates>($"{_baseURL}/GetCandidates?LocationCode={LocationCode}&DepartmentCode={DepartmentCode}&SectionCode={SectionCode}&GradeCode={GradeCode}");
        }

        public async Task<ResponseEntity> MapCandidate(RecruiterScreeningMapRequest model)
        {
            return await _httpService.PostAsync<ResponseEntity, RecruiterScreeningMapRequest>($"{_baseURL}/MapCandidate", model);
        }
    }
}
