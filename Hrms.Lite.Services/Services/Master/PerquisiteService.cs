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
    public class PerquisiteService : IPerquisiteService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public PerquisiteService(IServiceBase httpService)
        {
            _baseURL = "Perquisite";
            _baseURL1 = "Common";
            _httpService = httpService;

        }
        public async Task<Perquisite> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<Perquisite>($"{_baseURL}/List?TabIndex={TabIndex}");
        }

        public Task<ResponseEntity> Create(Perquisite input)
        {
            return _httpService.PostAsync<ResponseEntity, Perquisite>($"{_baseURL}/Create", input);
        }

        public async Task<Perquisite> GetPerquisiteDetails(Guid PerquisiteGI, Guid LogGI, string Type)
        {
            return await _httpService.GetAsync<Perquisite>($"{_baseURL}/PerquisiteDetails?PerquisiteGI={PerquisiteGI}&&LogGI={LogGI}&&Type={Type}");
        }

        public Task<ResponseEntity> Delete(Perquisite model)
        {
            return _httpService.PostAsync<ResponseEntity, Perquisite>($"{_baseURL}/Delete", model);
        }

        public Task<ResponseEntity> ApproveOrReject(Perquisite model)
        {
            return _httpService.PostAsync<ResponseEntity, Perquisite>($"{_baseURL}/ApproveOrReject", model);
        }
        public async Task<DataSet> GetList_Excel(string TabIndex)
        {
            return await _httpService.GetAsync<DataSet>($"{_baseURL}/GetList_Excel?TabIndex={TabIndex}");
        }
    }
}
