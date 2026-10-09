using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll
{
    public interface ISalaryReleaseWorkFlowService
    {
        public Task<SalaryReleaseWorkFlow> GetList(int Month, int TabIndex, Filter filter);
        public Task<ResponseEntity> Save(SalaryReleaseWorkFlow input);
        public Task<DataSet> GetList_Excel(int Month, int TabIndex, Filter filter);
    }
}
