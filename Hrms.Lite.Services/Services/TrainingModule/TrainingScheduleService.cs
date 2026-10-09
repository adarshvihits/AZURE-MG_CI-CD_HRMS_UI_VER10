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
    public class TrainingScheduleService: ITrainingScheduleService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public TrainingScheduleService(IServiceBase httpService)
        {
            _baseURL = "TrainingSchedule";
            _httpService = httpService;
        }
        public async Task<TrainingSchedule> GetList(int StatusCode, int FinancialYearCode)
        {
            return await _httpService.GetAsync<TrainingSchedule>($"{_baseURL}/GetList?StatusCode={StatusCode}&FinancialYearCode={FinancialYearCode}");
        }

        public async Task<TrainingSchedule> GetTopicList(int ScheduleCode)
        {
            return await _httpService.GetAsync<TrainingSchedule>($"{_baseURL}/GetTopicList?ScheduleCode={ScheduleCode}");
        }

        public async Task<ResponseEntity<string>> GetFRomToDates(int Month)
        {
            return await _httpService.GetAsync<ResponseEntity<string>>($"{_baseURL}/GetFRomToDates?Month={Month}");

        }
        public Task<ResponseEntity> Create(TrainingSchedule input)
        {
            return _httpService.PostAsync<ResponseEntity, TrainingSchedule>($"{_baseURL}/Create", input);
        }

        public Task<ResponseEntity> CreateTopic(TrainingSchedule input)
        {
            return _httpService.PostAsync<ResponseEntity, TrainingSchedule>($"{_baseURL}/CreateTopic", input);
        }
        public async Task<TrainingSchedule> GetTrainingScheduleDetails(int ScheduleCode, string Type)
        {
            return await _httpService.GetAsync<TrainingSchedule>($"{_baseURL}/GetDetails?ScheduleCode={ScheduleCode}&Type={Type}");
        }
        public Task<ResponseEntity> Delete(TrainingSchedule model)
        {
            return _httpService.PostAsync<ResponseEntity, TrainingSchedule>($"{_baseURL}/Delete", model);
        }
        public Task<ResponseEntity> DeleteTopic(int TransactionCode)
        {
            return _httpService.GetAsync<ResponseEntity>($"{_baseURL}/DeleteTopic?TransactionCode={TransactionCode}");
        }

        //PreTestSchedule
        public async Task<PreTrainingSchedule> GetPreScheduleList(int StatusCode)
        {
            return await _httpService.GetAsync<PreTrainingSchedule>($"{_baseURL}/GetPreScheduleList?StatusCode={StatusCode}");
        }
        public Task<ResponseEntity> PreScheduleTestSave(PreTrainingSchedule input)
        {
            return _httpService.PostAsync<ResponseEntity, PreTrainingSchedule>($"{_baseURL}/PreScheduleTestSave", input);
        }

        public async Task<PreTrainingSchedule> GetNotScheduleList(int ScheduleCode,int BasedOn, Filter filter)
        {
            return await _httpService.PostAsync<PreTrainingSchedule, Filter>($"{_baseURL}/GetNotScheduleList?ScheduleCode={ScheduleCode}&BasedOn={BasedOn}", filter);
        }


        public Task<ResponseEntity> PreTestPeoceed(Guid employeeGI, int PreTestScheduleCode)
        {
            return _httpService.GetAsync<ResponseEntity>($"{_baseURL}/PreTestPeoceed?employeeGI={employeeGI}&PreTestScheduleCode={PreTestScheduleCode}");
        } 
        public Task<ResponseEntity> PreTestDelete( int PreTestScheduleCode)
        {
            return _httpService.GetAsync<ResponseEntity>($"{_baseURL}/PreTestDelete?PreTestScheduleCode={PreTestScheduleCode}");
        }
        public Task<ResponseEntity> PreTestDeleteEmployeeWise(Guid employeeGI, int PreTestScheduleCode)
        {
            return _httpService.GetAsync<ResponseEntity>($"{_baseURL}/PreTestDeleteEmployeeWise?employeeGI={employeeGI}&PreTestScheduleCode={PreTestScheduleCode}");
        }
        public async Task<DataSet> GetNotScheduleList_Excel(int ScheduleCode, int BasedOn, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetNotScheduleList_Excel?ScheduleCode={ScheduleCode}&BasedOn={BasedOn}", filter);
        }

    }
}
