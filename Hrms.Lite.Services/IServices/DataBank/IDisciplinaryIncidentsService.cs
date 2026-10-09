using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
   public interface IDisciplinaryIncidentsService
    {
        public Task<DisciplinaryIncidents> GetDisciplinaryIncidentsList(int Tabindex, int SearchId,int? IncidentCode,Filter filter);
        public Task<IList<DisciplinaryIncidentsTabs>> GetDisciplinaryIncidentsTabs();
        public Task<ResponseEntity> DisciplinaryIncidentsSave(DisciplinaryIncidents model);
        public Task<IList<IncidentLog>> GetDisciplinaryIncidentsLogList(Guid EmployeeGI);
        public Task<DisciplinaryIncidents> GetDisciplinaryIncidentsLogDetails(Guid DisciplinaryIncidentGI);
        public Task<DisciplinaryIncidents> GetDisciplinaryIncidentsDetails(Guid DisciplinaryIncidentGI);
        public Task<ResponseEntity> DisciplinaryIncidentsDelete(Guid DisciplinaryIncidentGI);
    }
}
