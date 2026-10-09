using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface IAllowanceVsArrearAllowanceService
    {

        Task<AllowanceVsArrearAllowance> GetAllowanceVsArrearAllowanceList();
        Task<ResponseEntity> SaveAllowanceVsArrearAllowance(AllowanceVsArrearAllowance model);

    }
}
