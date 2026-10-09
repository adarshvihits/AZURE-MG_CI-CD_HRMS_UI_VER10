using Hrms.Lite.Shared.DataBank.Generic;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
    public interface ISalaryIncrementToolService
    {
        Task<SalaryIncrementTool> GetSettingsList();
        Task<SalaryIncrementTool> GetAllowanceList();
        Task<SalaryIncrementTool> GetClassificationList();
        Task<SalaryIncrementTool> GetFixedList();
        Task<SalaryIncrementTool> GetFixedSettingsList();
        public Task<ResponseEntity> Create(SalaryIncrementTool model);
        public Task<ResponseEntity> PrepareDoccument(SalaryIncrementTool model);
        public Task<ResponseEntity> ProcessExcelClick(SalaryIncrementTool model);
        public Task<ResponseEntity<List<SalaryIncrementTool>>> ImportFromExcel(SalaryIncrementTool model);
    }
}
