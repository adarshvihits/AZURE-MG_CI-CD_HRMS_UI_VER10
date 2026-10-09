using Hrms.Lite.Shared.DataBank;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
    public interface IDepartmentvsDeptHeadService
    {
        public Task<DepartmentvsDeptHead> GetList();

        public Task<DepartmentvsDeptHead> GetDetails(int DepartmentCode);

        public Task<ResponseEntity> Save(DepartmentvsDeptHead input);

        public Task<DataSet> GetDepartmentvsDeptHead_Excel();
    }
}
