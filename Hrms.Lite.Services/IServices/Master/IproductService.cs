using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
   public  interface IproductService
    {
        public Task<Product> GetList(string TabIndex);
        public Task<ResponseEntity>Create(Product input);
        public Task<Product> GetProductDetails(Guid ProductGI, Guid LogGI, string Type);
        public Task<ResponseEntity> Delete(Product model);
        public Task<ResponseEntity> ApproveOrReject(Product model);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);
        public Task<DataSet> GetList_Excel(string TabIndex);
    }
}
