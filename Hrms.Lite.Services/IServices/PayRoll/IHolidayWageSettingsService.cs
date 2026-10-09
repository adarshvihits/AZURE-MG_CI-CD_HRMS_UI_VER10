using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.PayRoll;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Hrms.Lite.Shared.PayRoll.AllowanceSettings;
using Hrms.Lite.Shared.Master;
using System.Data;

namespace Hrms.Lite.Services.IServices.PayRoll
{
    public interface IHolidayWageSettingsService
    {

       
        public Task<ApplicableAllowance> GetApplicableAllowanceList();

        public Task<Allowance> GetBatchUpDateDetails(int AllowanceCode);
        public Task<ResponseEntity> SaveApplicableAllowance(ApplicableAllowance model);
        public Task<ResponseEntity> SaveEmployeeWiseSettings(AllowanceSettingsSave model);

        public Task<ResponseEntity> BatchUpdateEmployeeWiseSettings(AllowanceSettingsBatchUpdate model);

        public Task<List<AllowanceSettingsEmployeeList>> GetSettingsEmployeeList(int AllowanceCode,Filter filter);

        public Task<DataSet> GetSettingsEmployeeList_Excel(int AllowanceCode, Filter filter);
        public Task<ResponseEntity> SaveDefaultSettings(AllowanceSettingsBatchUpdate model);
        public Task<AllowanceSettingsBatchUpdate> GetDefaultSettingsDetails();

    }
}
