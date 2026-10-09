using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface IQuoteOfDayServices
    {
        
        Task<QuotesOfDay> GetList(int FYCODE);
        public Task<ResponseEntity> QuotesOfDaySave(QuotesOfDay model);
        public Task<ResponseEntity> Delete(QuotesOfDay model);
        public Task<QuotesOfDay> GetBatchUpdateDetails(QuotesOfDay model);
        public Task<ResponseEntity> BatchUpdateSave(QuotesOfDay model);
    }
}
