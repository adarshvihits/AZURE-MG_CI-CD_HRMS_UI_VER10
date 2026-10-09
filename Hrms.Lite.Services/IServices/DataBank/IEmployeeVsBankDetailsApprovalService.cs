using Hrms.Lite.Shared;
using Hrms.Lite.Shared.DataBank;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;

using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
    public interface IEmployeeVsBankDetailsApprovalService
    {
        public Task<EmployeeVsBankDetailsApproval> GetList(Filter filterdata);
        public Task<EmployeeVsBankDetailsApproval> GetEmpBankDetails(Guid EmployeeGI);
        public Task<ResponseEntity> ApproveOrReject(Guid EmployeeGI, string mode);
        public Task<DataSet> GetList_Excel(Filter filterdata);

        Task<File> DownloadDoc(Guid RequestGI, string DocType);

    }
}
