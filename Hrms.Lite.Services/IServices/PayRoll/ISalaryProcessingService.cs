
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll
{
    public interface ISalaryProcessingService
    {
        public Task<SalaryProcessing> SalaryProcessingValidations( int SalaryPeriodCode, int PayrollGroup);

        public Task<ResponseEntity<SalaryProcessing>> MonthlySalaryProcessingValidation(SalaryProcessing model);
        public Task<ResponseEntity<SalaryProcessing>> SalaryProcessing(SalaryProcessing model);

        public Task<SalaryProcessing> SalaryProcessingValidationsPopUp(int SalaryPeriodCode, int PayrollGroup, string TabName, string Description);
    }
}
