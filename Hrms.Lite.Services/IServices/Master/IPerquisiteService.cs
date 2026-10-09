using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface IPerquisiteService
    {
        public Task<Perquisite> GetList(string TabIndex);
        public Task<ResponseEntity> Create(Perquisite input);
        public Task<Perquisite> GetPerquisiteDetails(Guid PerquisiteGI, Guid LogGI, string Type);
        public Task<ResponseEntity> Delete(Perquisite model);
        public Task<ResponseEntity> ApproveOrReject(Perquisite model);
        public Task<DataSet> GetList_Excel(string TabIndex);
    }
}
