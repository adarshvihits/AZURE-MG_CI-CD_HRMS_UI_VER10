using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll
{
  public  interface ISalaryReleaseRepostService
    {
        public Task<SalaryReleaseRepost> GetList(int Month, int TabIndex,int ReportFormat);
        public Task<ResponseEntity> Save(SalaryReleaseRepost input);
        public Task<ResponseEntity> RepostingApproval(SalaryReleaseRepost input);
    }
}
