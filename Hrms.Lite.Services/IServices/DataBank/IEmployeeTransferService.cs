using Hrms.Lite.Shared.DataBank;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
    public interface IEmployeeTransferService
    {
        public Task<EmployeeTransfer> GetList();
        public Task<DataSet> GetList_Excel();
    }
}
