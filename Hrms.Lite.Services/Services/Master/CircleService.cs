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
    public class CircleService: ICircleService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        private readonly string _baseURL1;

        public CircleService(IServiceBase httpService)
        {
            _baseURL = "Circle";
            _baseURL1 = "Common";
            _httpService = httpService;
        }
        public Task<ResponseEntity> ApproveOrReject(Circle model)
        {
            return _httpService.PostAsync<ResponseEntity, Circle>($"{_baseURL}/ApproveOrReject", model);
        }

        public Task<ResponseEntity> Create(Circle input)
        {
            return _httpService.PostAsync<ResponseEntity, Circle>($"{_baseURL}/Create", input);
        }

        public Task<ResponseEntity> Delete(Circle model)
        {
            return _httpService.PostAsync<ResponseEntity, Circle>($"{_baseURL}/Delete", model);
        }

        public async Task<Circle> GetCircleDetails(Guid CircleGI, Guid LogGI, string Type)
        {
            return await _httpService.GetAsync<Circle>($"{_baseURL}/GetCircleDetails?CircleGI={CircleGI}&&LogGI={LogGI}&&Type={Type}");
        }

        public async Task<Circle> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<Circle>($"{_baseURL}/List?TabIndex={TabIndex}");
        }

        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code)
        {
            return _httpService.GetAsync<ResponseEntity>($"{_baseURL1}/GetMasterInactiveValidation?Master={name}&Code={code}");
        }
    }
}
