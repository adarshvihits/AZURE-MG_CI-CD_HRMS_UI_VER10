using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll
{
   public interface ISalaryReleaseInitializeService
    {
         public Task<SalaryReleaseInitialize> GetList(int Month);
        public Task<ResponseEntity>Create(SalaryReleaseInitialize input);
    }
}
