using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank
{
    public class ResumeDataBankService : IResumeDataBankService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public ResumeDataBankService(IServiceBase httpService)
        {
            _baseURL = "ResumeDataBank";
            _httpService = httpService;
        }
        public async Task<ResumeDataBank> GetResumeDataBankDetails(Guid ResumeDataBankGI)
        {
            return await _httpService.GetAsync<ResumeDataBank>($"{_baseURL}/GetResumeDataBankDetails?ResumeDataBankGI={ResumeDataBankGI}");
        }

        public async Task<ResponseEntity> ResumeDataBankSave(ResumeDataBank model)
        {
            return await _httpService.PostAsync<ResponseEntity, ResumeDataBank>($"{_baseURL}/ResumeDataBankSave", model);
        }

        async Task<ResumeDataBank> IResumeDataBankService.GetResumeDataBankList(int Tabindex, DateTime? FROMDATE, DateTime? TODATE)
        {
            return await _httpService.GetAsync<ResumeDataBank>($"{_baseURL}/GetResumeDataBankList?Tabindex={Tabindex}&FROMDATE={FROMDATE}&TODATE={TODATE}");

        }
        async Task<ResumeDataBank> IResumeDataBankService.GetResumeDataBankListt(int Tabindex, DateTime? FROMDATE, DateTime? TODATE, ResumeFilter filter)
        {
            return await _httpService.PostAsync<ResumeDataBank, ResumeFilter>($"{_baseURL}/GetResumeDataBankList?Tabindex={Tabindex}&FROMDATE={FROMDATE}&TODATE={TODATE}", filter);
        }

        public async Task<IList<ResumeDataBankListTabs>> GetResumeDataBankTabs()
        {
            return await _httpService.GetAsync<IList<ResumeDataBankListTabs>>($"{_baseURL}/GetResumeDataBankTabs");
        }

        public async Task<ResponseEntity> ReleaseHoldResume(Guid ResumeDataBankGI)
        {
            return await _httpService.PostAsync<ResponseEntity, Guid>($"{_baseURL}/ReleaseHold", ResumeDataBankGI);

        }

        public async Task<DataSet> GetResumeDataBankListt_Excel(int Tabindex, string? FROMDATE, string? TODATE, ResumeFilter filter)
        {
            return await _httpService.PostAsync<DataSet, ResumeFilter>($"{_baseURL}/GetResumeDataBankListt_Excel?Tabindex={Tabindex}&FROMDATE={FROMDATE}&TODATE={TODATE}", filter);
        }
    }
}


