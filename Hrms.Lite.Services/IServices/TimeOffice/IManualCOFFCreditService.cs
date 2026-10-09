using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TimeOffice
{
   public interface IManualCOFFCreditService
    {
        Task<ManualCOFFCredit> ManualCOFFCreditList(Filter filter,int LeaveGroup);
        Task<DataSet> ManualCOFFCreditList_Excel(Filter filter, int LeaveGroup);
        Task<ManualCOFFCredit> GetEditDetails(Guid EmployeeGI);
        Task<ResponseEntity> EmployeeWiseCreditSave(ManualCOFFCredit model);
    }
}
