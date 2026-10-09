using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank;
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

namespace Hrms.Lite.Services.Services.DataBank
{
    public class PersonalDetailsService: IPersonalDetailsService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public PersonalDetailsService(IServiceBase httpService)
        {
            _baseURL = "PersonalDetails";
            _httpService = httpService;
        }

        async Task<PersonalDetails> IPersonalDetailsService.GetPersonalDetails(Guid EmployeeGI)
        {
            return await _httpService.GetAsync<PersonalDetails>($"{_baseURL}/GetPersonalDetails?EmployeeGI={EmployeeGI}");
        }
        async Task<PersonalDetailsList> IPersonalDetailsService.GetPersonalDetailsList(int SearchId,Filter Filter)
        {
            return await _httpService.PostAsync<PersonalDetailsList,Filter>($"{_baseURL}/GetPersonalDetailsList?SearchId={SearchId}",Filter);
        }
        async Task<PersonalDetails> IPersonalDetailsService.GetPersonalDetailsEducation(Guid LogGI)
        {
            return await _httpService.GetAsync<PersonalDetails>($"{_baseURL}/GetPersonalDetailsEducation?LogGI={LogGI}");
        }
        async Task<PersonalDetails> IPersonalDetailsService.GetPersonalDetailsFamily(Guid LogGI)
        {
            return await _httpService.GetAsync<PersonalDetails>($"{_baseURL}/GetPersonalDetailsFamily?LogGI={LogGI}");
        }
        async Task<PersonalDetails> IPersonalDetailsService.GetPersonalDetailsCertificate(int TrnCode)
        {
            return await _httpService.GetAsync<PersonalDetails>($"{_baseURL}/GetPersonalDetailsCertificate?TrnCode={TrnCode}");
        }
        async Task<PersonalDetails> IPersonalDetailsService.GetPersonalDetailsEmpVsNominee(int NomCatCode, int RelationCode, Guid EmployeeGI)
        {
            return await _httpService.GetAsync<PersonalDetails>($"{_baseURL}/GetPersonalDetailsEmpVsNominee?NomCatCode={NomCatCode}&RelationCode={RelationCode}&EmployeeGI={EmployeeGI}");
        }
        async Task<PersonalDetails> IPersonalDetailsService.GetPersonalDetailsSkill(Guid LogGI)
        {
            return await _httpService.GetAsync<PersonalDetails>($"{_baseURL}/GetPersonalDetailsSkills?LogGI={LogGI}");
        }
        async Task<PersonalDetails> IPersonalDetailsService.GetPersonalDetailsPrevEmployment(Guid LogGI)
        {
            return await _httpService.GetAsync<PersonalDetails>($"{_baseURL}/GetPersonalDetailsPrevEmployment?LogGI={LogGI}");
        }

        async Task<ResponseEntity> IPersonalDetailsService.SavePersonalDetails(PersonalDetails personalDetails)
        {
            return await _httpService.PostAsync<ResponseEntity, PersonalDetails>($"{_baseURL}/SavePersonalDetails", personalDetails);
        }

        public async Task<File> ViewDocument(int Code, string DocType)
        {
            return await _httpService.GetAsync<File>($"{_baseURL}/ViewPersonalDetailsDocument?Code={Code}&Type={DocType}");
        }
        async Task<ResponseEntity> IPersonalDetailsService.DeletePersonalDetailsFamilyDetails(Guid LogGI)
        {
            return await _httpService.DeleteAsync<ResponseEntity>($"{_baseURL}/DeletePersonalDetailsFamilyDetails?LogGI={LogGI}");
        }
        async Task<ResponseEntity> IPersonalDetailsService.DeletePersonalDetailsEducation(Guid LogGI)
        {
            return await _httpService.DeleteAsync<ResponseEntity>($"{_baseURL}/DeletePersonalDetailsEducation?LogGI={LogGI}");
        }
        async Task<ResponseEntity> IPersonalDetailsService.DeletePersonalDetailsCertificate(int TrnCode)
        {
            return await _httpService.DeleteAsync<ResponseEntity>($"{_baseURL}/DeletePersonalDetailsCertificationDetails?TrnCode={TrnCode}");
        }
        async Task<ResponseEntity> IPersonalDetailsService.DeletePersonalDetailsEmpVsNominee(int NomcatCode, int RelationCode, Guid EmployeeGI)
        {
            return await _httpService.DeleteAsync<ResponseEntity>($"{_baseURL}/DeletePersonalDetailsEmpVsNomineeDetails?NomcatCode={NomcatCode}&RelationCode={RelationCode}&EmployeeGI={EmployeeGI}");
        }
        async Task<ResponseEntity> IPersonalDetailsService.DeletePersonalDetailsSkill(Guid LogGI)
        {
            return await _httpService.DeleteAsync<ResponseEntity>($"{_baseURL}/DeletePersonalDetailsSkillsDetails?LogGI={LogGI}");
        }
        async Task<ResponseEntity> IPersonalDetailsService.DeletePersonalDetailsPrevEmployment(Guid LogGI)
        {
            return await _httpService.DeleteAsync<ResponseEntity>($"{_baseURL}/DeletePersonalDetailsPrevEmployment?LogGI={LogGI}");
        }
        async Task<PersonalDetailsDropDown> IPersonalDetailsService.GetPersonalDetailsDropDown()
        {
            return await _httpService.GetAsync<PersonalDetailsDropDown>($"{_baseURL}/GetPersonalDetailsDropDown");
        }
        public async Task<DataSet> GetPersonalDetailsList_Excel(Filter Filter, int SearchId)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetPersonalDetailsList_Excel?SearchId={SearchId}", Filter);
        }
        #region PERSPNAL DETAILS LOCK
        async Task<PersonalDetailsLock> IPersonalDetailsService.GetLockUnlockList(string TabIndex, Filter filter)
        {
          return await _httpService.PostAsync<PersonalDetailsLock, Filter>($"{_baseURL}/GetLockUnlockList?TabIndex={TabIndex}", filter);

        }

        async Task<DataSet> IPersonalDetailsService.GetLockUnlockList_Excel(string TabIndex, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetLockUnlockList_Excel?TabIndex={TabIndex}", filter);

        }

        async Task<ResponseEntity> IPersonalDetailsService.LockUnLockSave(string TabIndex, PersonalDetailsLock model)
        {
            return await _httpService.PostAsync<ResponseEntity, PersonalDetailsLock>($"{_baseURL}/LockUnLockSave?TabIndex={TabIndex}", model);

        }

        async Task<ResponseEntity> IPersonalDetailsService.LockBatchUpdate(string TabIndex,  Filter model)
        {
            return await _httpService.PostAsync<ResponseEntity, Filter>($"{_baseURL}/LockBatchUpdate?TabIndex={TabIndex}", model);

        }
        #endregion
        public async Task<List<CertificationDocList>> GetCertificationDocumentList(int LogCode)
        {
            return await _httpService.GetAsync<List<CertificationDocList>>($"{_baseURL}/GetCertificationDocumentList?LogCode={LogCode}");
        }
    }
}
