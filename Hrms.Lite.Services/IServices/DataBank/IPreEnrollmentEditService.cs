using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
   public interface IPreEnrollmentEditService
    {
        public Task<Enrollment> GetPreEnrollmentDetails(Guid EnrollGI);
        public Task<PreEnrollment> GetPreEnrollmentEditApprovalDetails(int LogCode);
        public Task<ResponseEntity> ApproveReject(PreEnrollment model);
        public Task<ResponseEntity> Save(PreEnrollment model);
        public Task<OnBoarding> GetPreEnrollEditApprovalList( Filter Filter);
        public Task<OnBoarding> GetOnBoardingList(int TabIndex, Filter Filter);
    }
}
