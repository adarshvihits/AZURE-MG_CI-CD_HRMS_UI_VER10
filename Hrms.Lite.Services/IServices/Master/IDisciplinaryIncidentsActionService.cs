using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface IDisciplinaryIncidentsActionService
    {
        public Task<DisciplinaryIncidentsAction> GetList(string TabIndex);
        public Task<ResponseEntity> Create(DisciplinaryIncidentsAction input);
        public Task<DisciplinaryIncidentsAction> GetDisciplinaryIncidentsActionDetails(Guid IncidentActionGI, Guid LogGI, string Type);
        public Task<ResponseEntity> ApproveOrReject(DisciplinaryIncidentsAction model);
        public Task<ResponseEntity> Delete(DisciplinaryIncidentsAction model);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);
    }
}
