using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.Recruitment;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Recruitment;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.Recruitment
{
    public class ApplicantsService : IApplicantsService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public ApplicantsService(IServiceBase httpService)
        {
            _baseURL = "Applicants";
            _httpService = httpService;
        }

        public Task<Applicants> GetApplicantsList(int TabIndex, Filter filter)
        {
            return _httpService.PostAsync<Applicants, Filter>($"{_baseURL}/GetApplicantsList?TabIndex={TabIndex}", filter);
        }

        public Task<ApplicantProfile> GetApplicantProfile(System.Guid ApplicantGI)
        {
            return _httpService.GetAsync<ApplicantProfile>($"{_baseURL}/GetApplicantProfile?ApplicantGI={ApplicantGI}");
        }
    }
}
