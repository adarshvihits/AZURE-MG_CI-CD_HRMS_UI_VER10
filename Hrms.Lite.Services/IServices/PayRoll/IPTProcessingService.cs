using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll
{
    public interface IPTProcessingService
    {
        public Task<PTProcessing> GetPTGroupDetails(int currMonth,int PTgroup);
        public Task<PTProcessing> GetPTProcessingList(int currMonth, int PTgroup,Filter filter);
        public Task<PTProcessing> GetBatchUpdateDetails(int Month, int PTgroup);
        public Task<ResponseEntity> PTProcessingSave(PTProcessing model);
        Task<DataSet> GetPTProcessingDetails_Excel(int MonthCode, int PTGroupCode, Filter filter);

    }
}
