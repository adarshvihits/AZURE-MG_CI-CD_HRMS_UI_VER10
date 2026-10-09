using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll.Reports
{
    public interface ISalaryReconciliationService
    {
        public Task<SalaryReconciliation> GetSalaryReconciliation(int SelectedMonth, int CompareWith, int Status, int EmployeeCode, int PayrollGroup, Filter filter);
        public Task<DataSet> GetEmployeeList_Excel(int SelectedMonth, int CompareWith, int Status, int EmployeeCode, int TypeCode, string Type, int PayrollGroup, Filter filter);

        public Task<SalaryReconciliation> GetSalaryReconciliationEmployeeDetails(int SelectedMonth, int CompareWith, int Status,int EmployeeCode, int TypeCode, string Type, int PayrollGroup, Filter filter);




        //Salary Reconciliation Report

        Task<DataSet> GetSalaryReconciliation_New_Excel(int PayrollGroup, int month, Filter filter);
        Task<SalaryReconciliation> GetSalaryReconciliation_New(int PayrollGroup, int month, Filter filter);
        Task<SalaryReconciliation> GetSalaryReconciliationEmployeeDetails_new(int PayrollGroup, int Month, string Type, int BANKCODE, Filter filter);
        Task<DataSet> GetSalaryReconciliationEmployeeDetails_new_Excel(int PayrollGroup, int Month, string Type, int BANKCODE, Filter filter);
    }
}
