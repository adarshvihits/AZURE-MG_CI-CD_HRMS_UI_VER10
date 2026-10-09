using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.Master
{
   public class DisciplinaryService: IDisciplinaryService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;

        private readonly IServiceBase _httpService;
        public DisciplinaryService(IServiceBase httpService)
        {
            _baseURL = "DisciplinaryIncidentsMaster";
            _baseURL1 = "Common";

            _httpService = httpService;
        }
        public async Task<DisciplinaryIncidentsMaster> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<DisciplinaryIncidentsMaster>($"{_baseURL}/GetDisciplinaryIncidentsList?TabIndex={TabIndex}");
        }
        public Task<DisciplinaryIncidentsMaster> GetDisciplinaryIncidentsDetails(Guid IncidentsGI, Guid LogGI, string type)
        {
            return _httpService.GetAsync<DisciplinaryIncidentsMaster>($"{_baseURL}/GetDisciplinaryIncidentsDetails?IncidentsGI={IncidentsGI}&LogGI={LogGI}&Type={type}");
        }
        public Task<ResponseEntity> Create(DisciplinaryIncidentsMaster input)
        {
            return _httpService.PostAsync<ResponseEntity, DisciplinaryIncidentsMaster>($"{_baseURL}/Create", input);
        }
        public Task<ResponseEntity> ApproveOrReject(DisciplinaryIncidentsMaster model)
        {
            return _httpService.PostAsync<ResponseEntity, DisciplinaryIncidentsMaster>($"{_baseURL}/ApproveOrReject", model);
        }
        public async Task<ResponseEntity> Delete(DisciplinaryIncidentsMaster model)
        {
            return await _httpService.PostAsync<ResponseEntity, DisciplinaryIncidentsMaster>($"{_baseURL}/Delete", model);

        }

        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }
    }
}
