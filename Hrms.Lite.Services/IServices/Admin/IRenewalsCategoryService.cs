using Hrms.Lite.Shared.Admin;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Admin
{
    public interface IRenewalsCategoryService
    {
        public Task<RenewalsCategory> GetRenewalsCategoryList(string TabIndex);
        public Task<ResponseEntity> Create(RenewalsCategory input);
        public Task<RenewalsCategory> GetRenewalsCategoryDetails(int RenewalsCategoryCode, string type);
        public Task<ResponseEntity> Delete(RenewalsCategory model);
        public Task<DataSet> GetRenewalsCategoryList_Excel(string TabIndex);

    }
}
