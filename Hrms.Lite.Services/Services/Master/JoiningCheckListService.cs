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
    public class JoiningCheckListService:IJoiningCheckListService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        private readonly string _baseURL1;
        public JoiningCheckListService(IServiceBase httpService)

        {
            _baseURL = "JoiningCheckList";
            _baseURL1 = "Common";
            _httpService = httpService;
        }


        public async Task<JoiningCheckList> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<JoiningCheckList>($"{_baseURL}/List?TabIndex={TabIndex}");
        }
        public Task<ResponseEntity> Save(JoiningCheckList input)
        {
            return _httpService.PostAsync<ResponseEntity, JoiningCheckList>($"{_baseURL}/Create", input);
        }
        public Task<JoiningCheckList> GetJoiningCheckListDetails(Guid JoiningCheckListGI, Guid LogGI, string Type)
        {
            return _httpService.GetAsync<JoiningCheckList>($"{_baseURL}/GetJoiningCheckListDetails?JoiningCheckListGI={JoiningCheckListGI}&LogGI={LogGI}&Type={Type}");
        }
        public Task<ResponseEntity> Delete(JoiningCheckList input)
        {
            return _httpService.PostAsync<ResponseEntity, JoiningCheckList>($"{_baseURL}/Delete", input);
        }

        public Task<ResponseEntity> ApproveOrReject(JoiningCheckList model)
        {
            return _httpService.PostAsync<ResponseEntity, JoiningCheckList>($"{_baseURL}/ApproveOrReject", model);
        }

        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }
    }
}
