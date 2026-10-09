using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using Hrms.Lite.Shared.PayRoll.Reports;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll
{
    public interface IMonthlySalaryProcessingIndividualService
    {
        public Task<SalaryProcessing> Individual_SalaryProcessingValidations(int SalaryPeriodCode, int EmployeeCode);
        public Task<ResponseEntity<SalaryProcessing>> Individual_SalaryProcessing(SalaryProcessing model);
        public Task<SalaryIntegrityReport> Individual_SalaryProcessing_Informations(int SelectedMonth, int EmployeeCode);
    }
}
