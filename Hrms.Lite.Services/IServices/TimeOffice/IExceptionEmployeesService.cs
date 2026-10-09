using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TimeOffice;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TimeOffice
{
   public interface IExceptionEmployeesService
    {
        public Task<ExceptionEmployees> GetEmployeeList(int MonthCode, int BasedOn, Filter Filter);
        public Task<ResponseEntity> AddEmployee(Guid EmployeeGI, string Type, int MonthCode,string remarks);
        public Task<ResponseEntity> BatchUpdate(int MonthCode,int BasedOn,string TypeValue, Filter Filter);
        public Task<DataSet> EmployeeList_Excel(int MonthCode, int BasedOn, Filter Filter);
        public Task<ResponseEntity<List<ExceptionEmployees>>> ImportFromExcel(ExceptionEmployees model);

        public Task<ExceptionEmployees> TransactionHistoryList(int month, Filter filter);


        public Task<DataSet> HistoryEmployeeList_Excel(int MonthCode, Filter Filter);

    }
}
