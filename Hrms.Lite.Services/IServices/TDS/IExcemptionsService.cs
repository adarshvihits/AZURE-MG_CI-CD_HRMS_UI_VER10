using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TDS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TDS
{
   public interface IExcemptionsService
    {
        Task<Excemptions> GetExcemptionsList();
        Task<ResponseEntity> SaveExcemptionsSettings(Excemptions model);
    }
}
