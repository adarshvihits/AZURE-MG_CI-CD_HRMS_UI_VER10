using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.PayRoll;
using Hrms.Lite.Shared.PayRoll;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.PayRoll
{
    public class NoticePayReturnService : INoticePayReturnService
    {
        private readonly string _baseURL;
        private readonly string _baseURL1;
        private readonly IServiceBase _httpService;
        public NoticePayReturnService(IServiceBase httpService)
        {
            _baseURL = "NoticePayReturn";
            _baseURL1 = "Common";
            _httpService = httpService;

        }
        public async Task<NoticePayReturn> GetNoticePayReturnDetails(Guid EmployeeGI, string Type)
        {
            return await _httpService.GetAsync<NoticePayReturn>($"{_baseURL}/GetNoticePayReturnDetails?EmployeeGI={EmployeeGI}&&Type={Type}");
        }

        async Task<NoticePayReturn> INoticePayReturnService.GetList(int Tabindex, Filter filterdata)
        {
            return await _httpService.PostAsync<NoticePayReturn, Filter>($"{_baseURL}/GetList?Tabindex={Tabindex}", filterdata);
        }



        public Task<ResponseEntity> Create(NoticePayReturn input)
        {
            return _httpService.PostAsync<ResponseEntity, NoticePayReturn>($"{_baseURL}/Create", input);
        }


        public Task<ResponseEntity> Delete(NoticePayReturn model)
        {
            return _httpService.PostAsync<ResponseEntity, NoticePayReturn>($"{_baseURL}/Delete", model);
        }

        public async Task<DataSet> GetList_Excel(int Tabindex, Filter Filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetList_Excel?Tabindex={Tabindex}", Filter);
        }
    }
}
