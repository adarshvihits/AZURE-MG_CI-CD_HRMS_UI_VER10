using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using File = Hrms.Lite.Shared.File;

namespace Hrms.Lite.Services.Services.DataBank
{
    public class OfferService : IOfferService
    {
        private readonly string _baseURL;
       
        private readonly IServiceBase _httpService;
        public OfferService(IServiceBase httpService)
        {
          
            _baseURL = "OnBoarding";
            _httpService = httpService;
        }
        public async Task<OfferLetter> GetofferLetterList(string Tabindex,int DocType, Filter Filter)
        {
            return await _httpService.PostAsync<OfferLetter, Filter>($"{_baseURL}/GetOfferLetterListDetails?DocumentType={DocType}&Tab={Tabindex}", Filter);
        }

        public async Task<OfferLetter> GetCandidateList(string TabIndex, Filter Filter,int radiomode)
        {
            return await _httpService.PostAsync<OfferLetter, Filter>($"{_baseURL}/GetCandidateListDetails?TabIndex={TabIndex}&radiomode={radiomode}", Filter);
        }

        //public async Task<OfferLetter> GetCandidatePreEnrollList(string Tabindex, int DocType, Filter Filter)
        //{
        //    return await _httpService.PostAsync<OfferLetter, Filter>($"{_baseURL}/GetCandidatePreEnrollListDetails?DocumentType={DocType}&Tab={Tabindex}", Filter);
        //}
        public async Task<ResponseEntity> OfferApprove(OfferLetter model)
        {
            return await _httpService.PostAsync<ResponseEntity, OfferLetter>($"{_baseURL}/GetOfferLetterApprove", model);
        }

        public async Task<ResponseEntity> OfferSend(OfferLetter model)
        {
            return await _httpService.PostAsync<ResponseEntity, OfferLetter>($"{_baseURL}/SendOfferLetter", model);
        }
        public async Task<ResponseEntity> CandidateLinkSend(OfferLetter model)
        {
            return await _httpService.PostAsync<ResponseEntity, OfferLetter>($"{_baseURL}/CandidateLinkSend", model);
        }
        public async Task<ResponseEntity> SaveOfferLetter(File f, int PreEnrollCode)
        {
            return await _httpService.PostAsync<ResponseEntity, File>($"{_baseURL}/SaveOfferLetter?PreEnrollCode={PreEnrollCode}", f);
        }
        public async Task<File> DownloadDoc(Guid PreEnrollGI)
        {
            return await _httpService.GetAsync<File>($"{_baseURL}/DownloadDoc?PreEnrollGI={PreEnrollGI}");
        }
        public async Task<DataSet> GetofferLetterList_Excel(string Tabindex, int DocType, Filter Filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetofferLetterList_Excel?DocumentType={DocType}&Tab={Tabindex}", Filter);
        }
    }
}
