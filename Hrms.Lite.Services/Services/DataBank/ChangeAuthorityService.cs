using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Shared.DataBank;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank
{
    public class ChangeAuthorityService: IChangeAuthorityService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public ChangeAuthorityService(IServiceBase httpService)
        {
            _baseURL = "ChangeAuthority";
            _httpService = httpService;
        }
        public async Task<ChangeAuthority> GetList(int WorkflowCode, Filter filter)
        {
            return await _httpService.PostAsync<ChangeAuthority,Filter>($"{_baseURL}/GetList?WorkflowCode={WorkflowCode}",filter);
        }
        public async Task<ChangeAuthority> GetAuthorityDetails(int TrnCode, int WorkflowCode)
        {
            return await _httpService.GetAsync<ChangeAuthority>($"{_baseURL}/GetAuthorityDetails?TrnCode={TrnCode}&WorkflowCode={WorkflowCode}");
        }
        public async Task<ResponseEntity> AuthorityUpdate(int TrnCode, int LevelCode, int AuthorityCode, int HDCode, int WorkflowCode)
        {
            return await _httpService.GetAsync<ResponseEntity>($"{_baseURL}/AuthorityUpdate?TrnCode={TrnCode}&LevelCode={LevelCode}&AuthorityCode={AuthorityCode}&HDCode={HDCode}&WorkflowCode={WorkflowCode}");
        }
    }
}
