using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll
{
 public   interface IBonusProcessingService 
    {
        Task<ResponseEntity> BonusProcessing(BonusProcessing model);
        Task<BonusProcessing> GetBonusProcessingList( int MonthCode, int BonusGroup ,Filter filter);
        Task<DataSet> GetBonusProcessingList_Excel( int MonthCode, int BonusGroup, Filter filter);
        Task<BonusProcessing> GetBonusProcessingDetails(int EmployeeCode, int MonthCode, int Bonus);
    }
}
