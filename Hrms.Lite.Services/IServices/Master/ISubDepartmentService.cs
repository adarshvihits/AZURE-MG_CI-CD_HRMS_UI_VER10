using Hrms.Lite.Services.Services.Master;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface ISubDepartmentService
    {
        public Task<SubDepartment> GetList(string TabIndex);
        public Task<ResponseEntity> Create(SubDepartment input);
        public Task<SubDepartment> GetSubDepartmentDetails(Guid SubDepartmentGI, Guid LogGI, string Type);
        public Task<ResponseEntity> Delete(SubDepartment model);
        public Task<ResponseEntity> ApproveOrReject(SubDepartment model);
        public Task<DataSet> GetList_Excel(string TabIndex);
    }
}
