using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll
{
    public interface ISalaryReleaseService
    {
        public Task<SalaryRelease> GetList(int Month, int PayrollGroupCode, Filter filter);
        public Task<ResponseEntity> Save(SalaryRelease model);
        public Task<DataSet> GetSalaryReleaseList_EXCEL(int Month, int PayrollGroupCode, Filter filter);
    }

}
