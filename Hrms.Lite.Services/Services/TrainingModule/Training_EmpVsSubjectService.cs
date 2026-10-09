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
    public class Training_EmpVsSubjectService : ITraining_EmpVsSubjectService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public Training_EmpVsSubjectService(IServiceBase httpService)
        {
            _baseURL = "Training_EmpVsSubject";
            _httpService = httpService;
        }
        public async Task<Training_EmpVsSubject> GetTrainingEmpVsSubjectList(int FYCode, int SubjectCode, Filter filter)
        {
            return await _httpService.PostAsync<Training_EmpVsSubject, Filter>($"{_baseURL}/GetTrainingEmpVsSubjectList?FYCode={FYCode}&SubjectCode={SubjectCode}", filter);
        }
  
        public async Task<DataSet> TrainingEmpVsSubject_Excel(int FYCode,int SubjectCode, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/TrainingEmpVsSubject_Excel?FYCode={FYCode}&SubjectCode={SubjectCode}", filter);
        }
        public async Task<Training_EmpVsSubject> GetApplyFilter(int FYCode, int SubjectCode, Filter filter)
        {
            return await _httpService.PostAsync<Training_EmpVsSubject, Filter>($"{_baseURL}/GetApplyFilter?FYCode={FYCode}&SubjectCode={SubjectCode}", filter);
        }
        public async Task<Training_EmpVsSubject> GetEmployeeProfileReport(int EmpCode, int FYCode, int SubjectCode)
        {
            return await _httpService.GetAsync<Training_EmpVsSubject>($"{_baseURL}/GetEmployeeProfileReport?EmpCode={EmpCode}&FYCode={FYCode}&SubjectCode={SubjectCode}");
        }
    }
}

