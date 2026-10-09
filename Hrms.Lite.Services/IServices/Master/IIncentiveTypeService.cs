using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface IIncentiveTypeService
    {
        public Task<IncentiveType> GetList(string TabIndex);
        public Task<ResponseEntity> Create(IncentiveType input);
        public Task<IncentiveType> GetIncentiveTypeDetails(Guid IncentiveTypeGI, Guid LogGI, string Type);
        public Task<ResponseEntity> Delete(IncentiveType model);
        public Task<ResponseEntity> ApproveOrReject(IncentiveType model);
        public Task<DataSet> GetList_Excel(string TabIndex);
    }
}
