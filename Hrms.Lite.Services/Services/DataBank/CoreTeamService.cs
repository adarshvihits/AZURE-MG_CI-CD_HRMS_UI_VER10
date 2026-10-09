using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Shared.DataBank;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank
{
    public class CoreTeamService : ICoreTeamService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public CoreTeamService(IServiceBase httpService)
        {
            _baseURL = "CoreTeam";
            _httpService = httpService;
        }
        public async Task<CoreTeam> GetCoreTeamList(int TabIndex, Filter filter)
        {
            return await _httpService.PostAsync<CoreTeam,Filter>($"{_baseURL}/GetCoreTeamList?TabIndex={TabIndex}", filter);
        }
        public async Task<ResponseEntity> CoreTeamSave(CoreTeam model)
        {
            return await _httpService.PostAsync<ResponseEntity, CoreTeam>($"{_baseURL}/CoreTeamSave", model);
        }
        public async Task<DataSet> GetCoreTeam_Excel(int TabIndex, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetCoreTeam_Excel?TabIndex={TabIndex}", filter);
        }
    }
}
