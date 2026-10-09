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
   public class ExitChecklistService: IExitChecklistService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public ExitChecklistService(IServiceBase httpService)
        {
            _baseURL = "ExitCheckList";
            _baseURL1 = "Common";
            _httpService = httpService;
        }

        public Task<ResponseEntity> ApproveOrReject(ExitCheckList model)
        {
            return _httpService.PostAsync<ResponseEntity, ExitCheckList>($"{_baseURL}/ApproveOrReject", model);
        }

        public Task<ResponseEntity> Create(ExitCheckList Input)
        {
            return _httpService.PostAsync<ResponseEntity, ExitCheckList>($"{_baseURL}/Create", Input);
        }

        public Task<ResponseEntity> Delete(ExitCheckList Input)
        {
            return _httpService.PostAsync<ResponseEntity, ExitCheckList>($"{_baseURL}/Delete", Input);
        }

        public Task<ExitCheckList> GetExitChecklistDetails(Guid ExitCheckListGI, Guid LogGI, string Type)
        {
            return _httpService.GetAsync<ExitCheckList>($"{_baseURL}/ExitCheckListDetails?ExitCheckListGI={ExitCheckListGI}&LogGI={LogGI}&Type={Type}");
        }

        public async Task<ExitCheckList> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<ExitCheckList>($"{_baseURL}/List?TabIndex={TabIndex}");
        }
        public async Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return await _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }
    }
}
