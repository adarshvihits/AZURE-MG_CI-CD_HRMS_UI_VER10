using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll
{
   public interface IBonusUploadService
    {
        public Task<BonusUpload> BonusUploadList(int Month, int IncCategory, Filter filter);
        public Task<ResponseEntity> BonusUploadSave(BonusUpload model);
        Task<DataSet> BonusUploadList_Excel(int Month, int IncCategory, Filter filter);
        public Task<ResponseEntity<List<BonusUpload>>> ImportFromExcel(BonusUpload model);
    }
}
