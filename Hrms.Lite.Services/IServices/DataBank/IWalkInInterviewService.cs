using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
    public interface IWalkInInterviewService
    {
        public Task<WalkInInterView> GetVacancyList();
        public Task<WalkInInterView> GetVacancyManagementDetails(Guid VacancyManagementGI);
        public Task<ResponseEntity> WalkInInterViewSave(WalkInInterView model);
    }
}
