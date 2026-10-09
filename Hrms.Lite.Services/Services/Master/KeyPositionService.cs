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
    public class KeyPositionService: IKeyPositionService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public KeyPositionService(IServiceBase httpService)
        {
            _baseURL = "KeyPosition";
            _baseURL1 = "Common";
            _httpService = httpService;
        }
        public async Task<KeyPosition> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<KeyPosition>($"{_baseURL}/List?TabIndex={TabIndex}");
        }
        public async Task<KeyPosition> KeyPositiondetails()
        {
            return await _httpService.GetAsync<KeyPosition>($"{_baseURL}/GetClassificationDetails");
        }
        public async Task<ResponseEntity> Save(KeyPosition model)
        {
            return await _httpService.PostAsync<ResponseEntity, KeyPosition>($"{_baseURL}/Create", model);
        }
        public Task<KeyPosition> GetKeyPositionDetails(Guid KeyPositionGI, Guid LogGI, string Type)
        {
            return _httpService.GetAsync<KeyPosition>($"{_baseURL}/KeyPositionDetails?KeyPositionGI={KeyPositionGI}&LogGI={LogGI}&Type={Type}");
        }
        public Task<ResponseEntity> ApproveOrReject(KeyPosition input)
        {
            return _httpService.PostAsync<ResponseEntity, KeyPosition>($"{_baseURL}/ApproveOrReject", input);
        }
        public Task<ResponseEntity> Delete(KeyPosition input)
        {
            return _httpService.PostAsync<ResponseEntity, KeyPosition>($"{_baseURL}/Delete", input);
        }
        public async Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return await _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }
    }
}
