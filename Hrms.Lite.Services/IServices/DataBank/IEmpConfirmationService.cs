using Hrms.Lite.Shared.DataBank;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
    public interface IEmpConfirmationService
    {
        public Task<EmpConfirmation> GetList();
        public Task<DataSet> EmpConfirmation_Excel();

    }
}
