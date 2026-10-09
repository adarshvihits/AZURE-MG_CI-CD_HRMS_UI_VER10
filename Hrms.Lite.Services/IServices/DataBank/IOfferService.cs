using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using File=Hrms.Lite.Shared.File;

namespace Hrms.Lite.Services.IServices.DataBank
{
   public interface IOfferService
    {
        public Task<OfferLetter> GetofferLetterList(string Tabindex,int DocType, Filter Filter);
        public Task<OfferLetter> GetCandidateList(string Tabindex, Filter Filter,int radiomode);
        //public Task<OfferLetter> GetCandidatePreEnrollList(string Tabindex,int DocType, Filter Filter);
        public Task<ResponseEntity> OfferApprove(OfferLetter model);
        public Task<ResponseEntity> OfferSend(OfferLetter model);

        public Task<ResponseEntity> SaveOfferLetter(File f ,int PreEnrollCode);
        public Task<ResponseEntity> CandidateLinkSend(OfferLetter model);
        Task<File> DownloadDoc(Guid PreEnrollGI);
        public Task<DataSet> GetofferLetterList_Excel(string Tabindex, int DocType, Filter Filter);

    }
}
