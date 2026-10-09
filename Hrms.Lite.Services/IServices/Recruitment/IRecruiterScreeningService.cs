using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Recruitment;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Recruitment
{
    public interface IRecruiterScreeningService
    {
        Task<List<RecruiterScreeningVacancy>> GetOpenVacancies(int? BusinessTypeCode, int? DepartmentCode, int? SectionCode, int? JobCode);
        Task<RecruiterScreeningCandidates> GetCandidates(int LocationCode, int DepartmentCode, int SectionCode, int GradeCode);
        Task<ResponseEntity> MapCandidate(RecruiterScreeningMapRequest model);
    }
}
