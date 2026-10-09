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
    public class DisciplinaryIncidentsActionService : IDisciplinaryIncidentsActionService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public DisciplinaryIncidentsActionService(IServiceBase httpService)
        {
            _baseURL = "DisciplinaryIncidentsAction";
            _baseURL1 = "Common";
            _httpService = httpService;
        }
        public async Task<DisciplinaryIncidentsAction> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<DisciplinaryIncidentsAction>($"{_baseURL}/List?TabIndex={TabIndex}");
          
        }
        public Task<ResponseEntity> Create(DisciplinaryIncidentsAction input)
        {
            return _httpService.PostAsync<ResponseEntity, DisciplinaryIncidentsAction>($"{_baseURL}/Create", input);
        }
        public Task<DisciplinaryIncidentsAction> GetDisciplinaryIncidentsActionDetails(Guid IncidentActionGI, Guid LogGI, string Type)
        {
            return _httpService.GetAsync<DisciplinaryIncidentsAction>($"{_baseURL}/DisciplinaryIncidentsActionDetails?IncidentActionGI={IncidentActionGI}&LogGi={LogGI}&Type={Type}");
        }
        public Task<ResponseEntity> ApproveOrReject(DisciplinaryIncidentsAction model)
        {
            return _httpService.PostAsync<ResponseEntity, DisciplinaryIncidentsAction>($"{_baseURL}/ApproveOrReject", model);
        }
        public async Task<ResponseEntity> Delete(DisciplinaryIncidentsAction model)
        {
            return await _httpService.PostAsync<ResponseEntity, DisciplinaryIncidentsAction>($"{_baseURL}/Delete", model);

        }

        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }
    }
}
