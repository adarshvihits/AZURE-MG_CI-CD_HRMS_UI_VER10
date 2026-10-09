using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
    public interface IVacancyManagementApprovalService
    {
        public Task<VacancyManagementApproval> GetVacancyManagementList();
        public Task<VacancyManagementApproval> GetVacancyManagementDetails(Guid VacancyManagementGI);
        public Task<ResponseEntity> VacancyManagementApprove(VacancyManagementApproval model);
    }
}
