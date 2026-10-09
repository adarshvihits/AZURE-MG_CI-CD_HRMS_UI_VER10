using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.TrainingModule;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TrainingModule;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.TrainingModule
{
   public class Training_EmployeVsReportService : ITraining_EmployeVsReportService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public Training_EmployeVsReportService(IServiceBase httpService)
        {
            _baseURL = "Training_EmployeVsReport";
            _httpService = httpService;
        }
        public async Task<Training_EmployeVsReport> GetTrainingEmployeeVsReportList(int FYCode,int CategoryCode,int TrainingCode, Filter filter )
        {
            return await _httpService.PostAsync<Training_EmployeVsReport,Filter>($"{_baseURL}/GetTrainingEmployeeVsReportList?FYCode={FYCode}&CategoryCode={CategoryCode}&TrainingCode={TrainingCode}",filter);
        }
        public async Task<Training_EmployeVsReport> GetEmployeeProfileReport(int EmpCode,int FYCode, int TrainingCode, int CategoryCode)
        {
            return await _httpService.GetAsync<Training_EmployeVsReport>($"{_baseURL}/GetEmployeeProfileReport?EmpCode={EmpCode}&FYCode={FYCode}&TrainingCode={TrainingCode}&CategoryCode={CategoryCode}");
        }
        public async Task<DataSet> TrainingEmployeeVsReport_Excel(int FYCode,int CategoryCode,int TrainingCode,Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/TrainingEmployeeVsReport_Excel?FYCode={FYCode}&CategoryCode={CategoryCode}&TrainingCode={TrainingCode}", filter);
        }
        public async Task<Training_EmployeVsReport>GetApplyFilter(int FYCode,int CategoryCode,int TrainingCode,Filter filter)
        {
            return await _httpService.PostAsync<Training_EmployeVsReport,Filter>($"{_baseURL}/GetApplyFilter?FYCode={FYCode}&CategoryCode={CategoryCode}&TrainingCode={TrainingCode}", filter);
        }
        public async Task<Training_EmployeVsReport> GetPopupDetails(int EmployeeCode,int FYCode, int CategoryCode, int TrainingCode)
        {
            return await _httpService.GetAsync<Training_EmployeVsReport>($"{_baseURL}/GetPopupDetails?EmployeeCode={EmployeeCode}&FYCode={FYCode}&CategoryCode={CategoryCode}&TrainingCode={TrainingCode}");
        }
        public async Task<DataSet> GetPopupExcel(int EmployeeCode, int FYCode, int CategoryCode, int TrainingCode)
        {
            return await _httpService.GetAsync<DataSet>($"{_baseURL}/GetPopupExcel?EmployeeCode={EmployeeCode}&FYCode={FYCode}&CategoryCode={CategoryCode}&TrainingCode={TrainingCode}");
        }
    }
}
