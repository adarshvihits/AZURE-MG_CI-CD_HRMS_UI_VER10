using Hrms.Lite.Shared.DataBank;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
   public interface IRequestToHRService
    {
        public Task<RequestToHR> GetList(int radiomode);
        public Task<RequestToHR> GetRequestToHRDetails(Guid EmployeeGI, int RequestCode);

        public Task<ResponseEntity> Create(RequestToHR input);
    }
}
