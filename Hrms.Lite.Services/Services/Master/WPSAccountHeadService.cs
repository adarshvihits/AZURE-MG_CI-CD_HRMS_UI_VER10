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
  public  class WPSAccountHeadService: IWPSAccountHeadService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public WPSAccountHeadService(IServiceBase httpService)
        {
            _baseURL = "WPSAccountHead";
            _httpService = httpService;
        }
        public async Task<WPSAccountHead> GetList(string TabIndex)
        {
            return await _httpService.GetAsync<WPSAccountHead>($"{_baseURL}/List?TabIndex={TabIndex}");
        }
        public async Task<ResponseEntity> Create(WPSAccountHead model)
        {
            return await _httpService.PostAsync<ResponseEntity, WPSAccountHead>($"{_baseURL}/Create", model);
        }
        public async Task<DataSet> GetExcel(string TabIndex)
        {
            return await _httpService.GetAsync<DataSet>($"{_baseURL}/GetAccountHead_Excel?TabIndex={TabIndex}");
        }
    }
}
