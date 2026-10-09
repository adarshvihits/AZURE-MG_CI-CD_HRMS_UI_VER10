using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
   public interface IFaqService
    {
        public Task<Faq> GetList(string TabIndex);
        public Task<ResponseEntity> Create(Faq input);
        public Task<Faq> GetFaqDetails(Guid FaqGI, Guid LogGI, string type);
        public Task<ResponseEntity> Delete(Faq model);
        public Task<ResponseEntity> ApproveOrReject(Faq model);

    }
}
