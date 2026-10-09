using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
   public interface ILanguagesService
    {
        public Task<Languages> GetList(string TabIndex);
        public Task<ResponseEntity> Create(Languages input);
        public Task<Languages> GetLanguageDetails(Guid LanguageGI, Guid LogGI, string Type);
        public Task<ResponseEntity> ApproveOrReject(Languages model);
        public Task<ResponseEntity> Delete(Languages input);
        public Task<ResponseEntity> GetMasterInactiveValidation(string name, int code);
    }
}
