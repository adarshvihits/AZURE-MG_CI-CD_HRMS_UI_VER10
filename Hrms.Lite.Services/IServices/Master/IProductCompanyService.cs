using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface IProductCompanyService
    {
        public Task<ProductCompany> GetList(string TabIndex);
        public Task<ResponseEntity> Create(ProductCompany input);
        public Task<ProductCompany> GetProductCompanyDetails(Guid ProductGI, Guid LogGI, string Type);
        public Task<ResponseEntity> Delete(ProductCompany input);
        public Task<ResponseEntity> ApproveOrReject(ProductRequest model);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);


    }
}
