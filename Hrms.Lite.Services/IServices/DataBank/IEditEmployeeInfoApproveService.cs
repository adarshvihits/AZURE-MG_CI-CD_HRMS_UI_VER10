using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
    public interface  IEditEmployeeInfoApproveService
    {
        public Task<EditEmployeeInfoApprove> GetEditEmployeeInfoApproveDetails( int searchID, Filter filter);
        public Task<EditEmployeeInfoApprove> GetEditEmployeeApproveDetails(Guid EmployeeGI, Guid LogGI);

        public Task<ResponseEntity> ApproveEditApproveEmployeeInfo(EditEmployeeInfoApprove model);

        public Task<DataSet> EditEmployeeInfoApprove_Excel(Filter filter);

    }
}
