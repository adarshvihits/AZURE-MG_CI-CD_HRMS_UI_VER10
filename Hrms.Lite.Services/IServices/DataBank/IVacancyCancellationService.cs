using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
   public interface IVacancyCancellationService
    {
        public Task<VacancyCancellation> GetList();
        public Task<VacancyCancellation> GetCancellationDetails(Guid VacancycancellationGI);
        public Task<ResponseEntity> Cancel(VacancyCancellation model);
    }
}
