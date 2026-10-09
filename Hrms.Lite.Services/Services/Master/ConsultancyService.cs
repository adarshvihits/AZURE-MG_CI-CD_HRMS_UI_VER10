using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.Master
{
    public class ConsultancyService : IConsultancyService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public ConsultancyService(IServiceBase httpService)
        {
            _baseURL = "Consultancy";
            _baseURL1 = "Common";
            _httpService = httpService;

        }

        public async Task<Consultancy> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<Consultancy>($"{_baseURL}/List?TabIndex={TabIndex}");
        }

        public Task<ResponseEntity> Create(Consultancy input)
        {
            return _httpService.PostAsync<ResponseEntity, Consultancy>($"{_baseURL}/Create", input);
        }

        public async Task<Consultancy> GetConsultancyDetails(Guid ConsultancyGI, Guid LogGI, string Type)
        {
            return await _httpService.GetAsync<Consultancy>($"{_baseURL}/SubConsultancyDetails?ConsultancyGI={ConsultancyGI}&&LogGI={LogGI}&&Type={Type}");
        }

        public Task<ResponseEntity> Delete(Consultancy model)
        {
            return _httpService.PostAsync<ResponseEntity, Consultancy>($"{_baseURL}/Delete", model);
        }

        public Task<ResponseEntity> ApproveOrReject(Consultancy model)
        {
            return _httpService.PostAsync<ResponseEntity, Consultancy>($"{_baseURL}/ApproveOrReject", model);
        }

        public async Task<DataSet> GetList_Excel(string TabIndex)
        {
            return await _httpService.GetAsync<DataSet>($"{_baseURL}/GetList_Excel?TabIndex={TabIndex}");
        }
    }
}
