using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface ICompanyVsPayrollGroupService
    {
        public Task<CompanyVsPayrollGroup> GetList(int GroupCompanyCode);

        
        Task<ResponseEntity> Save(CompanyVsPayrollGroup model);
    }
}
