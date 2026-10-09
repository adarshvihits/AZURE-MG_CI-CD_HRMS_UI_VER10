using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.TimeOffice;
using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using Hrms.Lite.Shared.TimeOffice;
using Hrms.Lite.Shared.TimeOffice.Generic.Master;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.TimeOffice
{
   public class ManualReaderIntegrationService: IManualReaderIntegrationService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public ManualReaderIntegrationService(IServiceBase httpService)
        {
            _baseURL = "ManualReaderIntegration";
            _httpService = httpService;
        }

        public async Task<ResponseEntity> ManualReaderIntegration(ManualReaderIntegration model)
        {
            return await _httpService.PostAsync<ResponseEntity, ManualReaderIntegration>($"{_baseURL}/ManualReaderIntegration", model);
        }

    }
}
