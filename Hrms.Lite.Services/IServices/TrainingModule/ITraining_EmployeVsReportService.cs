using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TrainingModule;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TrainingModule
{
   public interface ITraining_EmployeVsReportService
    {
        public Task<Training_EmployeVsReport> GetTrainingEmployeeVsReportList(int FYCode,int CategoryCode,int TrainingCode,Filter filter);
        public Task<Training_EmployeVsReport> GetEmployeeProfileReport(int EmpCode, int FYCode, int TrainingCode, int CategoryCode );
        public Task<DataSet> TrainingEmployeeVsReport_Excel(int FYCode, int CategoryCode, int TrainingCode, Filter filter);
        public Task<Training_EmployeVsReport> GetApplyFilter(int FYCode, int CategoryCode, int TrainingCode, Filter filter);
        public Task<Training_EmployeVsReport> GetPopupDetails(int EmployeeCode, int FYCode, int CategoryCode, int TrainingCode);
        public Task<DataSet> GetPopupExcel(int EmployeeCode, int FYCode, int CategoryCode, int TrainingCode);
    }
}
