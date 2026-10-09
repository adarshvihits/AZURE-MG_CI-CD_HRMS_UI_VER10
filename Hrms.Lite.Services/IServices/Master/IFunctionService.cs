using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface IFunctionService
    {
        public Task<Function> GetList(string TabIndex);
        public Task<ResponseEntity> Create(Function input);
        public Task<Function> GetFunctionDetails(Guid FunctionGI, Guid LogGI, string Type);
        public Task<ResponseEntity> Delete(Function model);
        public Task<ResponseEntity> ApproveOrReject(Function model);
        public Task<DataSet> GetList_Excel(string TabIndex);
    }
}
