using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TimeOffice
{
    public interface IOndutyWfhSettingsService
    {
        public Task<OndutyWfhSettings> GetOdWfhEmployeeList(Filter Filter);
        public Task<OndutyWfhSettings> GetOtherSettingsDetails(int LeaveGroup);
        public Task<ResponseEntity> EmployeeWiseOdWfhLimitSave(OndutyWfhSettings model);
        public Task<ResponseEntity> Save(OndutyWfhSettings input);
        Task<DataSet> OdWfhEmployeeList_Excel(Filter filter);
        public Task<ResponseEntity<List<OndutyWfhSettings>>> ImportFromExcel(OndutyWfhSettings model);


    }
}
