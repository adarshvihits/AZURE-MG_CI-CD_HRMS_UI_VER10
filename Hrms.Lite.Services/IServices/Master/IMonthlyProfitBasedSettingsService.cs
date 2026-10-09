using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface IMonthlyProfitBasedSettingsService
    {
        public Task<MonthlyProfitBasedSettings> GetList(int Month);
        public Task<DataSet> GetList_Excel(int Month);
        public Task<ResponseEntity<List<MonthlyProfitBasedSettings>>> ImportFromExcel(MonthlyProfitBasedSettings model);
    }
}
