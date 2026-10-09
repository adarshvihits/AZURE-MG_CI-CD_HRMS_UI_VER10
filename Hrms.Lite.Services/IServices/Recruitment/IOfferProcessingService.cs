using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Recruitment
{
    public interface IOfferProcessingService
    {
        public Task<OnBoarding> GetOfferProcessingList( int TabIndex, Filter Filter, string TabName);
        
        #region OfferProcessing
        public Task<PreEnrollment> GetOfferProcessingDetails( Guid ResumeDataBankGI, int ApplicantTrackingCode);
        public Task<ResponseEntity<string>> SaveOfferProcessing(PreEnrollment preEnrollment);
        #endregion
        #region Credit and Employement check
        public Task<PreEnrollment> GetCandidateDetails( Guid PreEnrollGI);
        public Task<ResponseEntity> SaveCreditCheck(CreditEmployeementChecking CreditChecking);
        public Task<ResponseEntity> SaveEmployementCheck(CreditEmployeementChecking EmployementChecking);
        #endregion
        #region OfferInitiation
        public Task<OnBoarding> GetInitiationList(string TabIndex, Filter Filter);
        public Task<PreEnrollment> GetOfferInitiationDetails(Guid PreEnrollGI);
        public Task<PreEnrollment> PreviewOfferProcessing(PreEnrollment model);
        public Task<Enrollment> SalaryDetails_enroll(Enrollment model);
        public Task<PreEnrollment> GetOfferLetterDetails(Guid PreEnrollGI);
        public Task<ResponseEntity> RepotService(int Code);
        public Task<ResponseEntity> OfferInitiation(PreEnrollment model);
        public Task<ResponseEntity> OfferAcceptance(PreEnrollment model);
        #endregion
    }
}
