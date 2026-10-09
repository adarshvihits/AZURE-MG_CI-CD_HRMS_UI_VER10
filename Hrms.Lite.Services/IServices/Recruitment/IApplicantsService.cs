using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Recruitment;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Recruitment
{
    public interface IApplicantsService
    {
        public Task<Applicants> GetApplicantsList(int TabIndex, Filter filter);
        public Task<ApplicantProfile> GetApplicantProfile(System.Guid ApplicantGI);
    }
}
