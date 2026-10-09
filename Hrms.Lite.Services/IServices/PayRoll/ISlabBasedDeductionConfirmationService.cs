using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll
{
   public interface ISlabBasedDeductionConfirmationService
    {
        public Task<SlabDeductionConfirmation> DedConfirmationList(int MonthCode,int DeductionCode, int ConfirmStatus,Filter filter1);
        public Task<SlabDeductionConfirmation> GetList(int MonthCode, int DeductionCode, int ConfirmStatus ,Filter filter1);
        public Task<ResponseEntity> EmployeeWiseSave(SlabDeductionConfirmation model);
        public Task<ResponseEntity> ConfirmAll(SlabDeductionConfirmation model);
    }
}
