using Hrms.Lite.Shared;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Recruitment;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Recruitment
{
    public interface IBackgroundVerificationService
    {        
        public Task<BackgroundVerification> GetCandidateDetails(Guid PreEnrollGI);        

        public Task<ResponseEntity> SaveBackgroundVerification(BackgroundVerification CreditChecking);

        public Task<ResponseEntity> BackgroundVerificationDetailsSave(BackgroundVerification CreditChecking);
    }
}
