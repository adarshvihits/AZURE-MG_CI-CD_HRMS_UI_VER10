using Hrms.Lite.Shared.PayRoll.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll.Reports
{
    public interface IAccountStatementService
    {
        public Task<BankStatement> GetList(int Month);
        public Task<BankStatement> GetEmployeeList(int Month,string Type,string desc,int Code);

        public Task<DataSet> GetAccountStatement_EmployeeList_Excel(int SalaryPeriod, string Type, string Description, int Code);


    }
}
