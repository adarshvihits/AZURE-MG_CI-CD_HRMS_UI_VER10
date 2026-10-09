using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Recruitment;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Recruitment
{
    public interface IDocumentVerificationService
    {
        public Task<DocumentVerification> GetDocumentVerificationList();
        public Task<DocumentVerification> GetOtherDocumentList(Guid ApplicantGI);
        public Task<DocumentVerification> GetDocumentVerificationDetails(Guid VacancyGI, int CandidateCode, int TrackingCode);
        public Task<DocumentVerification> GetIdentityDocumentDetails(Guid ApplicantGI, string DOC_TYPE);
        public Task<DocumentVerification> GetOtherDocumentDetails(string OtherDocName,Guid EmployeeGI);
        public Task<ResponseEntity> Verify(DocumentVerification model);
        public Task<ResponseEntity> ApplicantDocumentUploadSave(DocumentVerification model, string DOC_TYPE);
        public Task<ResponseEntity> DocumentApprove(DocumentVerification model, string DOC_TYPE, int ApplicantTrackingCode);
        public Task<ResponseEntity> ApplicantOtherDocumentUploadSave(DocumentVerification model);
        public Task<ResponseEntity> IdentityDocumentDelete(Guid ApplicantGI, string DocType);
        public Task<ResponseEntity> OtherDocumentDelete(Guid OtherDocGI);
    }
}
