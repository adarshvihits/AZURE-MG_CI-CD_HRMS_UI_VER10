using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
   public interface IVacancyClosingService
    {
        Task<VacancyCancellation> GetVacancyClosingList();
        Task<VacancyCancellation> GetVacancyClosingDetails(Guid VacancyManagementGI);
        Task<ResponseEntity> VacancyClose(VacancyCancellation model);
    }
}
