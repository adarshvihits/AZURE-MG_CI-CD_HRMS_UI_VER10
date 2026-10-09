using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface IAllowanceDeductionVsErpAccountHeadService
    {
        public Task<AccountsHead> GetList(string TabIndex, int GroupCode);
        public Task<ResponseEntity> Create(AccountsHead model);
        public Task<DataSet> GetExcel(string TabIndex, int GroupCode);

       
    }
}
