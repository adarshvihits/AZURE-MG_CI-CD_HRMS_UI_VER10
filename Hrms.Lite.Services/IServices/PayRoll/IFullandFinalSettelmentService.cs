using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Hrms.Lite.Shared.General;
using System.Data;

namespace Hrms.Lite.Services.IServices.PayRoll
{
    public interface IFullandFinalSettelmentService
    {

        public Task<FullandFinalSettelment> GetEmployeeList_FullandFinal(Filter filter, int PayrollGroup, int Month);
        public Task<FullandFinalSettelment> GetEmployeeList(Filter filter);
        public Task<FullandFinalSettelment> GetHRMSVarificationandApprovalEmployeeList(string Mode, Filter filter, int Month);
        public Task<FullandFinalSettelment> GetDetails(Guid EmployeeGI, string Mode);
        public Task<FullandFinalSettelment> GetDetailsFullandFinal(Guid EmployeeGI,int Month, string Mode);
        public Task<FullandFinalSettelment> GetHeldAmountDetails(Guid EmployeeGI);
        public Task<FullandFinalSettelment> GetDetailsProcess(Guid EmployeeGI, decimal EncashmentLeaves, decimal NoticeDedDays);

        public Task<FullandFinalSettelment> GetTDSEmployeeDetailsAndProcess( Guid EmployeeGI, int SalaryPeriodCode);
        public Task<ResponseEntity> SaveFullAndFinalSettelment(FullandFinalSettelment model);
        public Task<ResponseEntity> FullandFinalSettelmentVarification(FullandFinalSettelment model);

        /************************************* full and final delete *******************************************/

        Task<FullandFinalSettelment> GetFandFDeleteList(Filter filter);
        Task<DataSet> GetFandFDeleteList_Excel(Filter filter);
        Task<ResponseEntity> FullandFinalSettelmentDelete(FullandFinalSettelment model);
    }
}
