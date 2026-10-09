using Hrms.Lite.Shared.DataBank;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
    public interface IEconsoleService
    {
        public Task<Econsole> GetList(int tabIndex);
        Task<Econsole> GetCombinationDetails(int id, int DojAppl, int BeforeAfter, string Date);
        public Task<ResponseEntity> Save(Econsole model);
        public Task<Econsole> GetEconsoleDetails(int Code);
        public Task<ResponseEntity> Delete(int Code);

    }
}
