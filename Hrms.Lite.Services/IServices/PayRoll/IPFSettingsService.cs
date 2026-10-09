using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll
{
    public interface IPFSettingsService
    {
        public Task<PFSettings> GetPFEmployeeWiseSettingsList(int deduction, int monthcode,int PFGroup,Filter filter);
        public Task<PFSettings> GetPFSettingsApplicableAllowanceList(int deduction,int pfgroup);
        public Task<PFSettings> GetPFGeneralsettingsList(int deduction);
        public Task<PFSettings> GetPFEmployeeWiseSettingsDetails(Guid EmployeeGI, int deduction, int monthcode,int pfgroup);
        public Task<ResponseEntity> PFEmployeewisesettingsSave(PFSettings model);
        public Task<ResponseEntity> GeneralSettingsSave(PFSettings model);
        public Task<ResponseEntity> ApplicableAllowanceSave(PFSettings model);
        Task<DataSet> GetPFEmployeeWiseSettingsList_Excel(int DeductionCode, int MonthCode, Filter filter);

    }
}
