using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll
{
  public interface ISalaryReleaseBankStatementService
    {
        public Task<SalaryReleaseBankStatement> GetList(int Month, int ReportFormat, Filter filter);
        public Task<SalaryReleaseBankStatement> GetPreviousList(int Month, Filter filter);
        public Task<System.Data.DataSet> GetReleaseStatementDetails(SalaryReleaseBankStatement model);
        public Task<ResponseEntity> Save(SalaryReleaseBankStatement input);
        Task<ResponseEntity> LogErrorToDB(string errorMessage);
    }
}
