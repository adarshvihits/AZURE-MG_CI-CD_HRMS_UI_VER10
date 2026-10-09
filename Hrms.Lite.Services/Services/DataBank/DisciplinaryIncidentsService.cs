using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank
{
   public class DisciplinaryIncidentsService: IDisciplinaryIncidentsService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public DisciplinaryIncidentsService(IServiceBase httpService)
        {
            _baseURL = "DisciplinaryIncident";
            _httpService = httpService;
        }

        async Task<DisciplinaryIncidents> IDisciplinaryIncidentsService.GetDisciplinaryIncidentsList(int Tabindex, int SearchId,int? IncidentCode, Filter filter)
        {
            return await _httpService.PostAsync<DisciplinaryIncidents,Filter>($"{_baseURL}/GetDisciplinaryIncidentsList?Tabindex={Tabindex}&IncidentsCode={IncidentCode}&SearchId={SearchId}", filter);
        }
        async Task<IList<DisciplinaryIncidentsTabs>> IDisciplinaryIncidentsService.GetDisciplinaryIncidentsTabs()
        {
            return await _httpService.GetAsync<IList<DisciplinaryIncidentsTabs>>($"{_baseURL}/GetDisciplinaryIncidentsTabs");
        }
        async Task<ResponseEntity> IDisciplinaryIncidentsService.DisciplinaryIncidentsSave(DisciplinaryIncidents disciplinaryIncidents)
        {
            return await _httpService.PostAsync<ResponseEntity, DisciplinaryIncidents>($"{_baseURL}/DisciplinaryIncidentsSave", disciplinaryIncidents);
        }
        async Task<IList<IncidentLog>> IDisciplinaryIncidentsService.GetDisciplinaryIncidentsLogList(Guid EmployeeGI)
        {
            return await _httpService.GetAsync<IList<IncidentLog>>($"{_baseURL}/GetDisciplinaryIncidentsLogList?EmployeeGI={EmployeeGI}");
        }
        async Task<DisciplinaryIncidents> IDisciplinaryIncidentsService.GetDisciplinaryIncidentsLogDetails(Guid DisciplinaryIncidentGI)
        {
            return await _httpService.GetAsync<DisciplinaryIncidents>($"{_baseURL}/GetDisciplinaryIncidentsLogDetails?DisciplinaryIncidentGI={DisciplinaryIncidentGI}");
        }
        async Task<DisciplinaryIncidents> IDisciplinaryIncidentsService.GetDisciplinaryIncidentsDetails(Guid DisciplinaryIncidentGI)
        {
            return await _httpService.GetAsync<DisciplinaryIncidents>($"{_baseURL}/GetDisciplinaryIncidentsDetails?DisciplinaryIncidentGI={DisciplinaryIncidentGI}");
        }
        async Task<ResponseEntity> IDisciplinaryIncidentsService.DisciplinaryIncidentsDelete(Guid DisciplinaryIncidentGI)
        {
            return await _httpService.DeleteAsync<ResponseEntity>($"{_baseURL}/DisciplinaryIncidentsDetailsDelete?DisciplinaryIncidentGI={DisciplinaryIncidentGI}");
        }

    }
}
