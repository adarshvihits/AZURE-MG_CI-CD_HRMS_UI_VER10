using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
    public interface IApplicantTrackingService
    {
        public Task<ApplicantTracking> GetList(int Tabindex);
        public Task<ApplicantTracking> GetShortListedDetails(Guid ResumeDataBankGI);
        public Task<ApplicantTracking_Details> GetInterviewEvaluationDetails(Guid ResumeDataBankGI, string InterviewStatusType);
        public Task<ApplicantTracking_Details> GetEvaluationDetails(Guid ResumeDataBankGI);
        public Task<ApplicantTracking> GetInterviewEvaluationView(Guid ResumeDataBankGI, string InterviewStatusType);
        public Task<ApplicantTracking> GetApplicantTracker(Guid ResumeDataBankGI);
        public Task<ResponseEntity> ScreeningSave(ApplicantTracking model);
        public Task<ResponseEntity> InterviewEvaluationSave(ApplicantTracking model);
        public Task<ResponseEntity> FinalApproval(ApplicantTracking model);
        public Task<IList<ApplicantTrackingListTabs>> GetRAApprovalListTabs();
        public Task<ResponseEntity> OfferReject(ApplicantTracking model);
        public Task<ResponseEntity> HoldReject(ApplicantTracking model);
        public Task<ResumeDataBank> GetResumeDetails(Guid ResumeDataBankGI);
        public Task<ApplicantTracking> GetInterviewDetails(int ApplicantTrackingCode);
        public Task<ResponseEntity> OfferRevise(Guid PreEnrollGI, string Mode, string Remarks);


      
    }
}
