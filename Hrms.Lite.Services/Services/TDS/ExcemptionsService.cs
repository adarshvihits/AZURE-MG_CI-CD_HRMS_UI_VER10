using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.TDS;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TDS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.TDS
{
   public class ExcemptionsService: IExcemptionsService
    {
        private readonly string _baseURL;

        private readonly IServiceBase _httpService;
        public ExcemptionsService(IServiceBase httpService)
        {
            _baseURL = "Excemptions";
            _httpService = httpService;
        }


        Task<Excemptions> IExcemptionsService.GetExcemptionsList()
        {
            return _httpService.GetAsync<Excemptions>($"{_baseURL}/GetExcemptionsList");

        }

        Task<ResponseEntity> IExcemptionsService.SaveExcemptionsSettings(Excemptions model)
        {
            return _httpService.PostAsync<ResponseEntity, Excemptions>($"{_baseURL}/SaveExcemptionsSettings", model);
        }
    }
}
