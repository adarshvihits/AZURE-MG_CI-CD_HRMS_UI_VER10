using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
   public interface IResumeDataBankService
    {
        public Task<IList<ResumeDataBankListTabs>> GetResumeDataBankTabs();
        public Task<ResumeDataBank> GetResumeDataBankList(int Tabindex, DateTime? FROMDATE, DateTime? TODATE);
        public Task<ResumeDataBank> GetResumeDataBankListt(int Tabindex, DateTime? FROMDATE, DateTime? TODATE, ResumeFilter filter);
        Task<DataSet> GetResumeDataBankListt_Excel(int Tabindex, string? FROMDATE, string? TODATE, ResumeFilter filter);
        public Task<ResumeDataBank> GetResumeDataBankDetails(Guid ResumeDataBankGI);
        public Task<ResponseEntity> ResumeDataBankSave(ResumeDataBank model);
        public Task<ResponseEntity> ReleaseHoldResume(Guid ResumeDataBankGI);

    }
}