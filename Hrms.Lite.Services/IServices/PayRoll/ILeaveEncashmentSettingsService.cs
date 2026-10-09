using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Hrms.Lite.Shared.PayRoll.AllowanceSettings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll
{
    public interface ILeaveEncashmentSettingsService
    {
        public Task<ApplicableAllowance> GetApplicableAllowanceList();
        public Task<ResponseEntity> SaveApplicableAllowance(ApplicableAllowance model);
        public Task<List<AllowanceSettingsEmployeeList>> GetSettingsEmployeeList(int AllowanceCode, Filter filter);
        public Task<Allowance> GetBatchUpDateDetails(int AllowanceCode);
        public Task<ResponseEntity> BatchUpdateEmployeeWiseSettings(AllowanceSettingsBatchUpdate model);
        public Task<ResponseEntity> SaveDefaultSettings(AllowanceSettingsBatchUpdate model);
        public Task<ResponseEntity> SaveEmployeeWiseSettings(AllowanceSettingsSave model);
        public Task<AllowanceSettingsBatchUpdate> GetDefaultSettingsDetails();
    }
}
