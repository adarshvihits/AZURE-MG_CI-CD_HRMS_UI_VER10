using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
   public interface IDisciplinaryService
    {
        public Task<DisciplinaryIncidentsMaster> GetList(string TabIndex);
        public Task<DisciplinaryIncidentsMaster> GetDisciplinaryIncidentsDetails(Guid DisciplinaryGI, Guid LogGI, string type);
        public Task<ResponseEntity> Create(DisciplinaryIncidentsMaster input);
        public Task<ResponseEntity> ApproveOrReject(DisciplinaryIncidentsMaster model);
        public Task<ResponseEntity> Delete(DisciplinaryIncidentsMaster model);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);

    }
}
