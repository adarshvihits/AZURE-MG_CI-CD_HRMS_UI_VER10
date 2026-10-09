using Hrms.Lite.Shared.DataBank.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
    public interface IResumeProfileService
    {
        public Task<ResumeDataBank> GetResumeProfile(Guid ResumeGi);

    }
}
