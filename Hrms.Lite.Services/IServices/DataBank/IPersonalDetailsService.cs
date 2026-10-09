using Hrms.Lite.Shared;
using Hrms.Lite.Shared.DataBank;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.DataBank.Generic.PersonalDetails;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
    public interface IPersonalDetailsService
    {
        public Task<PersonalDetails> GetPersonalDetails(Guid EmployeeGI);
        public Task<PersonalDetailsList> GetPersonalDetailsList(int SearchId,Filter Filter);
        public Task<PersonalDetails> GetPersonalDetailsEducation(Guid LogGI);
        public Task<PersonalDetails> GetPersonalDetailsFamily(Guid LogGI);
        public Task<PersonalDetails> GetPersonalDetailsCertificate(int TrnCode);
        public Task<PersonalDetails> GetPersonalDetailsEmpVsNominee(int NomCatCode, int RelationCode, Guid EmployeeGI);
        public Task<PersonalDetails> GetPersonalDetailsSkill(Guid LogGI);
        public Task<PersonalDetails> GetPersonalDetailsPrevEmployment(Guid LogGI);
        public Task<ResponseEntity> SavePersonalDetails(PersonalDetails personalDetails);
        Task<File> ViewDocument(int Code, string DocType);
        public Task<PersonalDetailsDropDown> GetPersonalDetailsDropDown();
        public Task<ResponseEntity> DeletePersonalDetailsFamilyDetails(Guid LogGI);
        public Task<ResponseEntity> DeletePersonalDetailsEducation(Guid LogGI);
        public Task<ResponseEntity> DeletePersonalDetailsCertificate(int TrnCode);
        public Task<ResponseEntity> DeletePersonalDetailsEmpVsNominee(int NomcatCode, int RelationCode, Guid EmployeeGI);
        public Task<ResponseEntity> DeletePersonalDetailsSkill(Guid LogGI);
        public Task<ResponseEntity> DeletePersonalDetailsPrevEmployment(Guid LogGI);
        public Task<DataSet> GetPersonalDetailsList_Excel(Filter Filter, int SearchId);
        #region PERSONAL DETAILS LOCK       
        public Task<PersonalDetailsLock> GetLockUnlockList(string TabIndex,Filter filter);
        public Task<DataSet> GetLockUnlockList_Excel(string TabIndex,Filter filter);
        public Task<ResponseEntity> LockUnLockSave(string TabIndex, PersonalDetailsLock model);
        public Task<ResponseEntity> LockBatchUpdate(string TabIndex, Filter model);
        #endregion

        public Task<List<CertificationDocList>> GetCertificationDocumentList( int LogCode);
    }
}
