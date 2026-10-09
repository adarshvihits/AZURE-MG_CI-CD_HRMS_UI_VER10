using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TDS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TDS
{
 public   interface ITDSProcessingService
    {
        public Task<ResponseEntity> TDSProcessing(TDSProcessing Model);
        public Task<TDSProcessing> TDSConfirmDetails(int AssessmentYear,int SalaryPeriodCode,int PayrollGroupCode);
        Task<ResponseEntity> TDSConfirmation(TDSProcessing model);
        public Task<DataSet> TDSConfirmDetails_Export(int AssessmentYear, int SalaryPeriodCode,int PayrollGroupCode);
        public Task<System.Data.DataSet> TDSDetailRptExportToExcel(int AssessmentYearCode, int SalaryPeriodCode,int PayrollGroupCode);
        public Task<TDSEmployeeDetails> GetTDSEmployeeDetails(Guid EmployeeGI, int SalaryPeriodCode);

    }
}
