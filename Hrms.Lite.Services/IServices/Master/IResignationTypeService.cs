using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface IResignationTypeService
    {
        public Task<ResignationType> GetList(string TabIndex);
        public Task<ResponseEntity> Create(ResignationType input);
        public Task<ResignationType> GetResignationTypeDetails(Guid ResignationTypeGI, Guid LogGI, string Type);
        public Task<ResponseEntity> ApproveOrReject(ResignationType model);
        public Task<ResponseEntity> Delete(ResignationType model);
    }
}
