using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TimeOffice
{
   public interface ILeaveCreditDebitService
    {
        public Task<LeaveCreditDebit> LeaveCreditDebitList(int LeavePeriod, int Leave, int Month, int DebitCredit,int LeaveGroup,Filter filter);
        // public Task<LeaveCreditDebit> GetBatchUpDateDetails(int LeavePeriod,int LeaveGroup, int Leave, int Month, int Type);
        public Task<LeaveCreditDebit> GetBatchUpDateDetails(int LeaveGroup, int LeavePeriod, int Leave, int Month, int Type);

        public Task<LeaveCreditDebit> GetEditDetails(Guid EmployeeGI, int LeavePeriod, int Leave, int Month, int Type);
        public Task<ResponseEntity> LeaveCreditDebitSave(LeaveCreditDebit model);
        public Task<ResponseEntity> EmployeeWiseLeaveCreditDebitSave(LeaveCreditDebit model);

        Task<DataSet> LeaveCreditDebitList_Excel(int LeavePeriod, int Leave, int Month, int DebitCredit, int LeaveGroup, Filter filter);

        public Task<LeaveCreditDebit> LeaveCreditDebitHistoryList(int LeavePeriod, int Leave, int Month, int DebitCredit, Filter filter);
        public Task<ResponseEntity<List<LeaveCreditDebit>>> ImportFromExcel(LeaveCreditDebit model);
    }
}