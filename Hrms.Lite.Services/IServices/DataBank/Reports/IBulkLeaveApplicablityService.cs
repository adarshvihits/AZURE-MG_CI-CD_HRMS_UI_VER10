using Hrms.Lite.Shared.DataBank;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
    public interface IBulkLeaveApplicablityService
    {

        public Task<BulkLeaveApplicablity> GetApplicableList(int LeaveTypeCode, int LeaveGroupCode, int radioSelection, Filter filter);

        Task<DataSet> GetApplicableList_Excel(int LeaveTypeCode, int LeaveGroupCode, int radioSelection, Filter filter);

        Task<ResponseEntity<List<BulkLeaveApplicablityList>>> ImportFromExcel(BulkLeaveApplicablity model);

        Task<ResponseEntity> ApplicabilitySave(BulkLeaveApplicablity model);
    }
}
