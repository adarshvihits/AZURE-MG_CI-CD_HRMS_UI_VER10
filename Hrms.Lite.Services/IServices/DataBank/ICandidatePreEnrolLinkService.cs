using Hrms.Lite.Shared.CandidateLink;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
   public interface ICandidatePreEnrolLinkService
    {
        Task<IList<PreEnrollCandidateLink>> GetPreEnrollLinkApprovalList(Filter filterdata);
        Task<PersonalDetails> GetPreEnrollLinkApprovalData(Guid PreEnrollGI);

        Task<ResponseEntity> ApprovePreEnrollLinkDetails(PersonalDetails PersonalDetails);
        public Task<PersonalDetails> GetCandidateList(string Tabindex, Filter Filter, int radiomode);
        public Task<ResponseEntity> CandidateLinkSend(PersonalDetails model);
        public Task<ResponseEntity> EditCandidateEmail(PersonalDetails model);
    }
}
