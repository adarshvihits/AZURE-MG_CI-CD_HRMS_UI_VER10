using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.PayRoll.Reports;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.PayRoll.Reports
{
    public class SalaryReconciliationService : ISalaryReconciliationService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public SalaryReconciliationService(IServiceBase httpService)
        {
            _baseURL = "SalaryReconciliation";
            _httpService = httpService;
        }
        public async Task<SalaryReconciliation> GetSalaryReconciliation(int SelectedMonth, int CompareWith, int Status, int EmployeeCode,int PayrollGroup, Filter filter)
        {
            return await _httpService.PostAsync<SalaryReconciliation, Filter>($"{_baseURL}/GetSalaryReconciliation?SelectedMonth={SelectedMonth}&CompareWith={CompareWith}&Status={Status}&EmployeeCode={EmployeeCode}&PayrollGroup={PayrollGroup}", filter);
        }
        public async Task<SalaryReconciliation> GetSalaryReconciliationEmployeeDetails(int SelectedMonth, int CompareWith, int Status, int EmployeeCode, int TypeCode, string Type, int PayrollGroup, Filter filter)
        {
            return await _httpService.PostAsync<SalaryReconciliation, Filter>($"{_baseURL}/GetSalaryReconciliationEmployeeDetails?SelectedMonth={SelectedMonth}&CompareWith={CompareWith}&Status={Status}&EmployeeCode={EmployeeCode}&TypeCode={TypeCode}&Type={Type}&PayrollGroup={PayrollGroup}", filter);

        }

       public async Task<DataSet> GetEmployeeList_Excel(int SelectedMonth, int CompareWith, int Status, int EmployeeCode, int TypeCode, string Type, int PayrollGroup, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetEmployeeList_Excel?SelectedMonth={SelectedMonth}&CompareWith={CompareWith}&Status={Status}&EmployeeCode={EmployeeCode}&TypeCode={TypeCode}&Type={Type}&PayrollGroup={PayrollGroup}", filter);

        }


        //Salary Reconciliation Report

        public async Task<SalaryReconciliation> GetSalaryReconciliation_New(int month, int PayrollGroup, Filter filter)
        {
            return await _httpService.PostAsync<SalaryReconciliation, Filter>($"{_baseURL}/GetSalaryReconciliation_New?PayrollGroup={PayrollGroup}&month={month}", filter);
        }
        public async Task<DataSet> GetSalaryReconciliation_New_Excel(int PayrollGroup, int month, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetSalaryReconciliation_New_Excel?PayrollGroup={PayrollGroup}&month={month}", filter);

        }


        public async Task<SalaryReconciliation> GetSalaryReconciliationEmployeeDetails_new(int PayrollGroup, int Month, string Type, int BANKCODE, Filter filter)
        {
            return await _httpService.PostAsync<SalaryReconciliation, Filter>($"{_baseURL}/GetSalaryReconciliationEmployeeDetails_new?PayrollGroup={PayrollGroup}&Month={Month}&Type={Type}&BANKCODE={BANKCODE}", filter);
        }    
        public async Task<DataSet> GetSalaryReconciliationEmployeeDetails_new_Excel(int PayrollGroup, int Month, string Type, int BANKCODE, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetSalaryReconciliationEmployeeDetails_new_Excel?PayrollGroup={PayrollGroup}&Month={Month}&Type={Type}&BANKCODE={BANKCODE}", filter);

        }

    }
}
