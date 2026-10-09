using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
    public interface IMRFVsResumeService
    {
        public Task<MRFVsResume> GetVacancyList(int Type);
        public Task<MRFVsResume> GetMRFVsResumeList(int Type, Guid VacancyGI,ResumeFilter filter);
        public Task<MRFVsResume> GeJobTitle(Guid VacancyGI);
        public Task<ResponseEntity> Create(MRFVsResume input);
    }
}
