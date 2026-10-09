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
    public class BussinessTypeService : IBussinessTypeService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public BussinessTypeService(IServiceBase httpService)
        {
            _baseURL = "BussinessType";
            _baseURL1 = "Common";
            _httpService = httpService;

        }

        public Task<ResponseEntity> ApproveOrReject(BussinessType model)
        {
            return _httpService.PostAsync<ResponseEntity, BussinessType>($"{_baseURL}/ApproveOrReject", model);
        }

        public Task<ResponseEntity> Create(BussinessType input)
        {
            return _httpService.PostAsync<ResponseEntity, BussinessType>($"{_baseURL}/Create", input);
        }

        public Task<ResponseEntity> Delete(BussinessType model)
        {
            return _httpService.PostAsync<ResponseEntity, BussinessType>($"{_baseURL}/Delete", model);
        }

        public async Task<BussinessType> GetBussinessTypeDetails(Guid BussinessTypeGI, Guid LogGI, string Type)
        {
            return await _httpService.GetAsync<BussinessType>($"{_baseURL}/BussinessTypeDetails?BussinessTypeGI={BussinessTypeGI}&&LogGI={LogGI}&&Type={Type}");
        }

        //public Task<ResponseEntity> Create(BussinessType input)
        //{
        //    throw new NotImplementedException();
        //}


        public async Task<BussinessType> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<BussinessType>($"{_baseURL}/List?TabIndex={TabIndex}");
        }

        public async Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return await _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }
    }
}
