using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface IResignationReasonService
    {
        public Task<ResignationReason> GetList(string TabIndex);
        public Task<ResponseEntity> Create(ResignationReason input);
        public Task<ResignationReason> GetResignationReasonDetails(Guid ResignationReasonGI, Guid LogGI, string Type);
        public Task<ResponseEntity> ApproveOrReject(ResignationReason model);
        public Task<ResponseEntity> Delete(ResignationReason model);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);


    }
}
