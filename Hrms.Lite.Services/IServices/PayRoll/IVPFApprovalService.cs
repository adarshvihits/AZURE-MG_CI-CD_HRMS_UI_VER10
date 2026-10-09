using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll
{
    public interface IVPFApprovalService
    {
        public Task<VPFApproval> GetList(string Tab,int FinancialYear);
        Task<ResponseEntity> Approve(VPFApproval model);
        public Task<DataSet> GetList_Excel(string Tab, int FinancialYear);
    }
}
