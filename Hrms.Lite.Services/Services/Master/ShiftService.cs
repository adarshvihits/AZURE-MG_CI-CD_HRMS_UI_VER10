using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.Master;
using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice.Generic.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.Master
{
    public class ShiftService : IShiftService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        private readonly string _baseURL1;

        public ShiftService(IServiceBase httpService)
        {
            _baseURL = "Shift";
            _baseURL1 = "Common";
            _httpService = httpService;
        }

        public Task<ResponseEntity> ApproveOrReject(ShiftDetails model)
        {
            return _httpService.PostAsync<ResponseEntity, ShiftDetails>($"{_baseURL}/ApproveOrReject", model);
        }

        public Task<ResponseEntity> Create(ShiftDetails input)
        {
            return _httpService.PostAsync<ResponseEntity, ShiftDetails>($"{_baseURL}/Create", input);
        }

        public async Task<ResponseEntity> Delete(ShiftDetails model)
        {
            return await _httpService.PostAsync<ResponseEntity, ShiftDetails>($"{_baseURL}/Delete", model);
        }

        public async Task<ShiftDetails> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<ShiftDetails>($"{_baseURL}/List?TabIndex={TabIndex}");
        }

        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }

        public Task<ShiftDetails> GetShiftDetails(Guid ShiftGI, Guid LogGI, string Type)
        {
            return _httpService.GetAsync<ShiftDetails>($"{_baseURL}/ShiftDetails?ShiftGI={ShiftGI}&LogGI={LogGI}&Type={Type}");
        }
    }
}
