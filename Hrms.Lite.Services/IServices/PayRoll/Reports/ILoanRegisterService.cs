using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll.Reports
{
    public interface ILoanRegisterService
    {
        public Task<LoanIssue> GetLoanRegisterList(int Tab, int type, Filter filter);
        public Task<DataSet> GetLoanRegister_Excel(int Tab, int type, Filter filter);
        public Task<LoanIssue> GetLoanRegisterDetails(Guid EmployeeGI,int LoanIssueCode);
    }
}
