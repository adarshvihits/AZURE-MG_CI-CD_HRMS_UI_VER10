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
   public  class SpecialHolidayApplicabilityService : ISpecialHolidayApplicabilityService   
    {

        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public SpecialHolidayApplicabilityService(IServiceBase httpService)
        {
            _baseURL = "SpecialHolidayApplicability";
            _httpService = httpService;
        }
       
        public async Task<SpecialHolidayApplicability> SpecialholidayEmpList( int HolidayCode,Filter filter)
        {
            return await _httpService.PostAsync<SpecialHolidayApplicability,Filter>($"{_baseURL}/List?HolidayCode={HolidayCode}",filter);
        }
        public Task<ResponseEntity>Save(SpecialHolidayApplicability input)
        {
            return _httpService.PostAsync<ResponseEntity, SpecialHolidayApplicability>($"{_baseURL}/Create", input);
        }
    }
}
