using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
    public interface IJobPostingService
    {
        public Task<JobPosting> GetJobPostingList(int TabIndex);
        public Task<JobPosting> GetJobPostingDetails(Guid JobPostingGI,string mode);
        public Task<ResponseEntity> JobPostingSave(JobPosting model);
        public Task<ResponseEntity> JobPostingDelete(Guid VacancyManagementGI);
    }
}
