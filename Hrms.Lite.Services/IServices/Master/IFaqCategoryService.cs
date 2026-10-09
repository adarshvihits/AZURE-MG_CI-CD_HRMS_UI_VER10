using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
  public  interface IFaqCategoryService
    {
        public Task<FaqCategory> GetList(string TabIndex);
        public Task<ResponseEntity> Create(FaqCategory input);
        public Task<FaqCategory> GetFaqCategoryDetails(Guid FaqCategoryGI, Guid LogGI, string type);
        public Task<ResponseEntity> ApproveOrReject(FaqCategory model);
        public Task<ResponseEntity> Delete(FaqCategory model);

    }
}
