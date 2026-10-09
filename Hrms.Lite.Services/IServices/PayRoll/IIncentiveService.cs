using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll
{
   public interface IIncentiveService
    {
        public Task<Incentive> GetList(int Month, int BonusCategoryCode, Filter filter);
        public Task<ResponseEntity> Save(Incentive model);
        public Task<ResponseEntity<List<Incentive>>> ImportFromExcel(Incentive model);
        Task<DataSet> IncentiveList_Excel(int Month, int BonusCategoryCode, Filter Filter);


    }
}
