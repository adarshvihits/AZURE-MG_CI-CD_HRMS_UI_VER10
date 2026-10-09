using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace Hrms.Lite.Services.IServices.Master
{
    public interface IERPAccountHeadService
    {

        public Task<ERPAccountHead> GetList(string TabIndex);
        public Task<ResponseEntity> Create(ERPAccountHead input);
        public Task<ERPAccountHead> GetERPAccountHeadDetails(Guid LogGI, Guid ERPAccountHeadGI, string Type);
        public Task<ResponseEntity> ApproveOrReject(ERPAccountHead input);
        public Task<ResponseEntity> Delete(ERPAccountHead input);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);
    }
}
