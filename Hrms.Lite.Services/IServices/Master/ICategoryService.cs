using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface ICategoryService
    {
        public Task<Category> GetList(string TabIndex);
        public Task<ResponseEntity> Create(Category input);
        public Task<Category> GetCategoryDetails(Guid CategoryGI,Guid LogGI, string Type);
        public Task<ResponseEntity> ApproveOrReject(Category model);
        public Task<ResponseEntity> Delete(Category model);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);
    }
}
