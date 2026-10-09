using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TrainingModule;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TrainingModule
{
    public interface ITraining_EmpVsSubjectService
    {
        public Task<Training_EmpVsSubject> GetTrainingEmpVsSubjectList(int FYCode,int SubjectCode,Filter filter);
        public Task<Training_EmpVsSubject> GetEmployeeProfileReport(int EmpCode,int FYCode, int SubjectCode);
        public Task<DataSet> TrainingEmpVsSubject_Excel(int FYCode, int SubjectCode, Filter filter);
        public Task<Training_EmpVsSubject> GetApplyFilter(int FYCode, int SubjectCode, Filter filter);
    }
}
