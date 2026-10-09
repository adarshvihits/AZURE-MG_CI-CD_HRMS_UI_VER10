using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface IJobPostingConfirmationService
    {
        public Task<JobPostingConfirmationViewModel> GetJobPostingConfirmationList(string TabIndex);
        public Task<JobPostingConfirmationDetails> GetVacancyDetails(int VacancySourceCode, int SourceTranactionCode);
        public Task<ResponseEntity> ConfirmPosting(JobPostingConfirmationRequest model);
    }
}
