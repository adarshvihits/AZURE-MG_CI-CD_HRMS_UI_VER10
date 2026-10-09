using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TimeOffice
{
    public interface ILeaveCancellationService
    {
        public Task<LeaveCancellation> GetEmployeeLeaveCancellationList(int PayrollCode, int MonthCode,int LeaveCode, Filter filter);
        public Task<ResponseEntity> LeaveCancellationSave(LeaveCancellation model);

        public Task<DataSet> GetEmployeeLeaveCancellationList_Excel(int PayrollCode,int MonthCode, int LeaveCode, Filter filter);
        public Task<LeaveCancellation> GetLeaveCancellationLog(int PayrollCode, int MonthCode, int LeaveCode, Filter filter);
        public Task<DataSet> GetLeaveCancellationLog_Excel(int PayrollCode, int MonthCode, int LeaveCode, Filter filter);
    }
}
