using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll
{
    public interface IESISettingsService
    {
        public Task<ESISettings> GetESISettingsApplicableAllowanceList(int DeductionCode);
        public Task<ESISettings> GetESIGeneralsettingsList(int DeductionCode);
        public Task<ESISettings> GetESIEmployeeWiseSettingsList(int DeductionCode, int MonthCode,Filter filter);
        public Task<ESISettings> GetESIEmployeeWiseSettingsDetails(Guid EmployeeGI, int DeductionCode, int MonthCode);
        public Task<ResponseEntity> ESISettingsAllowanceSave(ESISettings model);
        public Task<ResponseEntity> ESIGeneralsettingsSave(ESISettings model);
        public Task<ResponseEntity> ESIEmployeewisesettingsSave(ESISettings model);
        Task<DataSet> GetESIEmployeeWiseSettingsList_Excel(int DeductionCode, int MonthCode, Filter filter);

    }
}
