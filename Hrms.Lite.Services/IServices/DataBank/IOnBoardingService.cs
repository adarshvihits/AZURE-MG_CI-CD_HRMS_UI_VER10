using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
  public   interface IOnBoardingService
    {
        public Task<IList<OnBoardingListTabs>> GetOnBoardingTabs();
        public Task<OnBoarding>GetOnBoardingList(int TabIndex,Filter Filter);
        public Task<OfferLetter> GetofferLetterList(int DocType, Filter Filter);
        public Task<OnBoarding> GetJoiningChecklistDetail(Guid PreEnrollGI);
        public Task<ResponseEntity> JoiningCheckSave(OnBoarding model);
        public Task<Url> GetUrl();
        public Task<Url> GetOfferLetterFileName(string Type);

    }
}
