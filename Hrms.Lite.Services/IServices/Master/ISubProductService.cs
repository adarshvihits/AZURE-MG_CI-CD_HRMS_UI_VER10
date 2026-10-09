using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface ISubProductService
    {
        public Task<SubProduct> GetList(string TabIndex);
        public Task<ResponseEntity> Create(SubProduct input);
        public Task<SubProduct> GetSubProductDetails(Guid SubProductGI, Guid LogGI, string Type);
        public Task<ResponseEntity> Delete(SubProduct model);
        public Task<ResponseEntity> ApproveOrReject(SubProduct model);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);
        public Task<DataSet> GetList_Excel(string TabIndex);

    }
}
