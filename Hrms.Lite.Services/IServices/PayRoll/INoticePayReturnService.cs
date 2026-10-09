using Hrms.Lite.Shared.PayRoll;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll
{
    public interface INoticePayReturnService
    {
        public Task<NoticePayReturn> GetList(int TabIndex, Filter filterdata);
        public Task<ResponseEntity> Create(NoticePayReturn input);
        public Task<NoticePayReturn> GetNoticePayReturnDetails(Guid EmployeeGI, string Type);
        public Task<ResponseEntity> Delete(NoticePayReturn model);
        public Task<DataSet> GetList_Excel(int TabIndex, Filter Filter);
    }
}

