using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TimeOffice
{
    public interface IAbscondingIntimationService
    {
        Task<AbscondingIntimation> GetList(Filter filter);
        public Task<AbscondingIntimation> GetHistoryDetails(Guid EmployeeGI);

        public Task<ResponseEntity> SaveDetag(AbscondingIntimation model);
    }
}
