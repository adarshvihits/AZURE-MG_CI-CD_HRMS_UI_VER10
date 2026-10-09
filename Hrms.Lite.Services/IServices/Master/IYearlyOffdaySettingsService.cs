using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
  public  interface IYearlyOffdaySettingsService
    {
        public Task<YearlyOffdaySettings> GetYearlyOffdayList();
        public Task<YearlyOffdaySettings> GetDetails(Guid OffdaySettingsGI);
        public Task<YearlyOffdaySettings> GetDaysList(int Year,YearlyOffdaySettings model);
        public Task<ResponseEntity> Save(YearlyOffdaySettings model);
        public Task<ResponseEntity> Delete(YearlyOffdaySettings model);
    }
}
