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
    public class ReaderService : IReaderService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public ReaderService(IServiceBase httpService)
        {
            _baseURL = "Reader";
            _baseURL1 = "Common";
            _httpService = httpService;
        }
        public async Task<Reader> List(string TabIndex)
        {
            return await _httpService.GetAsync<Reader>($"{_baseURL}/List?TabIndex={TabIndex}");
        }
        public Task<Reader> GetReaderDetails(Guid ReaderGI, Guid LogGI, string Type)
        {
            return _httpService.GetAsync<Reader>($"{_baseURL}/GetReaderDetails?ReaderGI={ReaderGI}&LogGI={LogGI}&Type={Type}");
        }
        public Task<ResponseEntity> Create(Reader input)
        {
            return _httpService.PostAsync<ResponseEntity, Reader>($"{_baseURL}/Create", input);
        }
        public Task<ResponseEntity> ApproveOrReject(Reader model)
        {
            return _httpService.PostAsync<ResponseEntity, Reader>($"{_baseURL}/ApproveOrReject", model);
        }
        public async Task<ResponseEntity> Delete(Reader model)
        {
            return await _httpService.PostAsync<ResponseEntity, Reader>($"{_baseURL}/Delete", model);

        }

        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }
    }
}
