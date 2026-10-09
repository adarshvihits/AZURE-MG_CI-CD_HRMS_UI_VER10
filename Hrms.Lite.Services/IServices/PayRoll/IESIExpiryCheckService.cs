using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll
{
    public interface IESIExpiryCheckService
    {
        public Task<ESIExpiryCheck> GetESIExpiryCheckEmployeeList(int Month, Filter filter);
        public Task<DataSet> GetESIExpiryCheckList_Excel(int Month,Filter filter);
        Task<ResponseEntity> SaveESIApplicabilty(ESIExpiryCheck Model);
    }
}
