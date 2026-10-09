using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TimeOffice
{
    public interface  ILOPSandwichUpdationService
    {
        public Task<LOPSandwichUpdation> GetList(int Month, int Payroll, Filter filter);
        Task<ResponseEntity> LOPSandwichUpdationSave(LOPSandwichUpdation model);
        public Task<LOPSandwichUpdation> GetHistoryList(int Month, int Payroll, Filter filter);
        Task<DataSet> LOPSandwichUpdationExcel(int Month, int Payroll, Filter filter);

    }
}
