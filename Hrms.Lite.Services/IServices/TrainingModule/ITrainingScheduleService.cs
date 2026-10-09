using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TrainingModule;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TrainingModule
{
    public interface ITrainingScheduleService
    {
        Task<TrainingSchedule> GetList(int StatusCode, int FinancialYearCode);
        Task<TrainingSchedule> GetTopicList(int ScheduleCode);
        Task<ResponseEntity<string>> GetFRomToDates(int Month);
        Task<ResponseEntity> Create(TrainingSchedule input);
        Task<ResponseEntity> CreateTopic(TrainingSchedule input);
        Task<TrainingSchedule> GetTrainingScheduleDetails(int ScheduleCode, string Type);
        public Task<ResponseEntity> Delete(TrainingSchedule model);
        public Task<ResponseEntity> DeleteTopic(int TransactionCode);

        //PreSchedule

        Task<PreTrainingSchedule> GetPreScheduleList(int StatusCode);
        Task<ResponseEntity> PreScheduleTestSave(PreTrainingSchedule input);
        public Task<PreTrainingSchedule> GetNotScheduleList(int ScheduleCode,int BasedOn, Filter filter);
        public Task<DataSet> GetNotScheduleList_Excel(int ScheduleCode,int BasedOn, Filter filter);
        Task<ResponseEntity> PreTestPeoceed(Guid employeeGI,int PreTestScheduleCode);
        Task<ResponseEntity> PreTestDeleteEmployeeWise(Guid employeeGI,int PreTestScheduleCode);
        Task<ResponseEntity> PreTestDelete(int PreTestScheduleCode);
    }
}
