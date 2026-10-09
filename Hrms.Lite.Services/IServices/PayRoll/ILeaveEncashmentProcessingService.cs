using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll
{
   public interface ILeaveEncashmentProcessingService
    {
        public Task<LeaveEncashmentProcessing> GetList(int Month, Filter filter);
        public Task<LeaveEncashmentProcessing> GetListReport(int Month,int Allowance, Filter filter);
        public Task<ResponseEntity> Save(LeaveEncashmentProcessing model);
        public Task<DataSet> Encashment_excel(int Month,int Allowance, Filter filter);
        public Task<DataSet> Encashment_excel_Report(int Month, int Allowance, Filter filter);
    }
}
