using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Recruitment
{
   public interface IJoinConfirmEmpActivationService
    {
        Task<OnBoarding> PreEnrollJoiningConfirmList( Filter Filter);
        public Task<PreEnrollment> GetPreEnrollJoiningConfirmDetails( Guid PreEnrollGI);
        public Task<ResponseEntity> PreEnrollJoiningConfirm(PreEnrollment model);
        Task<OnBoarding> GetPreEnrollEmployeeActivationList(Filter Filter);

        
    }
}
