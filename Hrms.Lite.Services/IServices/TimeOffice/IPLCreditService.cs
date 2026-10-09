using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TimeOffice
{
  public interface IPLCreditService
    {
        public Task<PLCredit> GetPLCreditList(int LeavePeriodCode, int LeaveCode, Filter filter1);
        public Task<DataSet> GetPLCredit_Excel(int LeavePeriodCode, int LeaveCode, Filter filter1);
        public Task<ResponseEntity> PLCreditSave(PLCredit model);
        public Task<ResponseEntity> PLCreditBatchUpdate(PLCredit model);
    }
}
