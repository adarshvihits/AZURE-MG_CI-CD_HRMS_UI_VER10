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
   public class AllowanceVsArrearAllowanceService : IAllowanceVsArrearAllowanceService
    {
        private readonly string _baseURL;

        private readonly IServiceBase _httpService;
        public AllowanceVsArrearAllowanceService(IServiceBase httpService)
        {
            _baseURL = "AllowanceVsArrearAllowance";
            _httpService = httpService;
        }    

     
        Task<AllowanceVsArrearAllowance> IAllowanceVsArrearAllowanceService.GetAllowanceVsArrearAllowanceList()
        {
            return _httpService.GetAsync<AllowanceVsArrearAllowance>($"{_baseURL}/GetAllowanceVsArrearAllowanceList");

        }

        Task<ResponseEntity> IAllowanceVsArrearAllowanceService.SaveAllowanceVsArrearAllowance(AllowanceVsArrearAllowance model)
        {
            return _httpService.PostAsync<ResponseEntity, AllowanceVsArrearAllowance>($"{_baseURL}/SaveAllowanceVsArrearAllowance", model);
        }
    }
}
