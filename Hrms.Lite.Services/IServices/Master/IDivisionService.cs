using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
   public  interface IDivisionService
    {
        public Task<Division> GetList(string TabIndex);
        public Task<ResponseEntity> Create(Division input);
        public Task<Division> GetDivisionDetails(Guid DivisionGI, Guid LogGI, string Type);
        public Task<ResponseEntity> ApproveOrReject(Division model);
        public Task<ResponseEntity> Delete(Division input);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);
    }
}
