using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
    public interface IEditEmployeeInfoService
    {
        public Task<EditEmployeeInfo> GetEditEmployeeInfoList(int Tabindex, int SearchId,Filter filter);
        public Task<EditEmployeeInfo> GetEditEmployeeInfoDetails(Guid EmployeeGI);
        public Task<EditEmployeeInfo> GetEditEmployeeInfoLogDetails(Guid EmployeeGI, Guid LogGI);
        public Task<EditEmployeeInfo> GetEditEmployeeInfoApproveDetails(Guid EmployeeGI, Guid LogGI);
        public Task<ResponseEntity> SaveEditEmployeeInfo(EditEmployeeInfo model);
        public Task<ResponseEntity> ApproveEditEmployeeInfo(EditEmployeeInfo model);

        public Task<EditEmployeeInfo> GetEmployeelistDetails(string Type,Filter filter1);
        public Task<DataSet> editEmployeeInfoTrnList_Excel(Filter filter, int Tabindex, int SearchId);


    }
}
