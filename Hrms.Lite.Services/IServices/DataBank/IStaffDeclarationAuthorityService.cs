using Hrms.Lite.Shared.DataBank;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
    public interface IStaffDeclarationAuthorityService
    {
        public Task<StaffDeclarationAuthority> GetList( Filter filterdata);
        public Task<ResponseEntity> Create(StaffDeclarationAuthority input);
        public Task<DataSet> GetList_Excel( Filter Filter);
    }
}
