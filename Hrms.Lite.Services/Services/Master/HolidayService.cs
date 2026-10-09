using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.Master;
using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.Master
{
    public class HolidayService : IHolidaySerivce
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public HolidayService(IServiceBase httpService)
        {
            _baseURL = "Holiday";
            _httpService = httpService;
        }
        public Task<ResponseEntity> ApproveOrReject(Holiday model)
        {
            return _httpService.PostAsync<ResponseEntity, Holiday>($"{_baseURL}/ApproveOrReject", model);
        }

        public Task<ResponseEntity> Create(Holiday input)
        {
            return _httpService.PostAsync<ResponseEntity, Holiday>($"{_baseURL}/Create", input);
        }

        public Task<ResponseEntity> Delete(Holiday model)
        {
            return _httpService.PostAsync<ResponseEntity, Holiday>($"{_baseURL}/Delete", model);
          
        }

        public Task<Holiday> GetHolidayDetails(Guid HolidayGI, Guid LogGI, string Type)
        {
            return _httpService.GetAsync<Holiday>($"{_baseURL}/HolidayDetails?HolidayGI={HolidayGI}&LogGI={LogGI}&Type={Type}");
        }

        public async Task<Holiday> GetList(string TabIndex, int yearcode)
        {
            return await _httpService.GetAsync<Holiday>($"{_baseURL}/List?TabIndex={TabIndex}&yearcode={yearcode}");
        }

        public async Task<DateAndMonth> GetYearCode()
        {
            return await _httpService.GetAsync<DateAndMonth>($"{_baseURL}/GetYearCode");
        }
    }
}
