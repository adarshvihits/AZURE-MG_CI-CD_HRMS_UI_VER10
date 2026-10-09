using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
namespace Hrms.Lite.Services.IServices.TimeOffice
{
   public interface IManualReaderIntegrationService
    {
        Task<ResponseEntity> ManualReaderIntegration(ManualReaderIntegration model);
    }
}
