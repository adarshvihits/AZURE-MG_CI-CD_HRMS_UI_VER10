using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface IBussinessTypeService
    {
        public Task<BussinessType> GetList(string TabIndex);
        public Task<ResponseEntity> Create(BussinessType input);
        public Task<BussinessType> GetBussinessTypeDetails(Guid BussinessTypeGI, Guid LogGI, string Type);
        public Task<ResponseEntity> Delete(BussinessType model);
        public Task<ResponseEntity> ApproveOrReject(BussinessType model);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);

    }
}
