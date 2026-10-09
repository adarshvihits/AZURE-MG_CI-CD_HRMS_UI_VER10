using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TimeOffice
{
    public interface ILeaveLapsService
    {
        public Task<LeaveLaps> GetLeaveLapsList(int LeavePeriodCode, int LeaveCode, Filter filter1);
        public Task<DataSet> GetLeaveLapsList_Excel(int LeavePeriodCode, int LeaveCode, Filter filter1);
        public Task<ResponseEntity>LeaveLapsSave(LeaveLaps model);
        public Task<ResponseEntity> LeaveLapsBatchUpdate(LeaveLaps model);
    }
}
