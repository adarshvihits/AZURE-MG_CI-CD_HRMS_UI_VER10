using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Shared.DataBank;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank
{
    public class MailAlertSettingsService : IMailAlertSettingsService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public MailAlertSettingsService(IServiceBase httpService)
        {
            _baseURL = "MailAlertSettings";
            _baseURL1 = "Common";
            _httpService = httpService;

        }
        

        async Task<MailAlertSettings> IMailAlertSettingsService.GetList(int TypeCode, Filter filterdata)
        {
            return await _httpService.PostAsync<MailAlertSettings, Filter>($"{_baseURL}/GetList?TypeCode={TypeCode}", filterdata);
        }



        public Task<ResponseEntity> Create(MailAlertSettings input)
        {
            return _httpService.PostAsync<ResponseEntity, MailAlertSettings>($"{_baseURL}/Create", input);
        }


        

        public async Task<DataSet> GetList_Excel(int TypeCode, Filter Filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetList_Excel?TypeCode={TypeCode}", Filter);
        }
    }
}
