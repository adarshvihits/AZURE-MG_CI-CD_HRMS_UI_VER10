using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.DataBank.Generic;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank
{
   public class JobTransitionService: IJobTransitionService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public JobTransitionService(IServiceBase httpService)
        {
            _baseURL = "JobTransition";
            _httpService = httpService;
        }

        public async Task<IList<JobTransition_Tabs>> GetJobTransitionTabs()
        {

            return await _httpService.GetAsync<IList<JobTransition_Tabs>>($"{_baseURL}/GetJobTransitionTabs");

        }

        public async Task<IList<JobTransition_Tabs>> GetJobTransitionDueListTabs()
        {
            return await _httpService.GetAsync<IList<JobTransition_Tabs>>($"{_baseURL}/GetJobTransitionDueListTabs");
        }

      public  async Task<JobTransition> GetJobTransitionDueList(int Tabindex, int SearchId,Filter filter)
        {
            return await _httpService.PostAsync<JobTransition,Filter>($"{_baseURL}/GetJobTransitionDueList?Tabindex={Tabindex}&SearchId={SearchId}",filter);
        }
        public async Task<JobTransition> GetJobTransitionList(int Tabindex, int SearchId, Filter filter)
        {
            return await _httpService.PostAsync<JobTransition, Filter>($"{_baseURL}/GetJobTransitionList?Tabindex={Tabindex}&SearchId={SearchId}",filter);
        }

        public async Task<JobTransition> GetJobTransitionLogList(string FromDate, string ToDate, int Tabindex, int? TransitionType, string Type,Filter filter)
        {
            return await _httpService.PostAsync<JobTransition, Filter>($"{_baseURL}/GetJobTransitionLogList?FromDate={FromDate}&ToDate={ToDate}&Tabindex={Tabindex}&TransitionType={TransitionType}&Type={Type}", filter);
        }


        public async Task<DataSet> GetJobTransitionLogList_Excel(string FromDate, string ToDate, int Tabindex, int? TransitionType, string Type, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetJobTransitionLogList_Excel?FromDate={FromDate}&ToDate={ToDate}&Tabindex={Tabindex}&TransitionType={TransitionType}&Type={Type}",filter);
        }
        public async Task<JobTransition> GetJobTransitionConfirmList(int Tabindex, int SearchId, Filter filter)
        {
            return await _httpService.PostAsync<JobTransition, Filter>($"{_baseURL}/GetJobTransitionConfirmList?Tabindex={Tabindex}&SearchId={SearchId}", filter);
        }
        public async Task<ResponseEntity> ApproveJobTransition(JobTransition JobTransition)
        {
            return await _httpService.PostAsync<ResponseEntity, JobTransition>($"{_baseURL}/ApproveJobTransition", JobTransition);
        }


        public async Task<ResponseEntity> SaveJobTransition(JobTransition JobTransition)
        {
            return await _httpService.PostAsync<ResponseEntity, JobTransition>($"{_baseURL}/SaveJobTransition", JobTransition);
        }

        public  async Task<JobTransition> GetJobTransitionDetails(Guid EmployeeGI, Guid HistoryGI, string Mode,string BasedOn)
        {
            return await _httpService.GetAsync<JobTransition>($"{_baseURL}/GetJobTransitionDetails?EmployeeGI={EmployeeGI}&HistoryGI={HistoryGI}&Mode={Mode}&BasedOn={BasedOn}" );

        }

        public async Task<JobTransition> GetJobTransitionGrowthHistory(Guid EmployeeGI)
        {
            return await _httpService.GetAsync<JobTransition>($"{_baseURL}/GetJobTransitionGrowthHistory?EmployeeGI={EmployeeGI}");
        }

        public async Task<JobTransition> GetJobTransitionDeleteList(Filter filter)
        {
            return await _httpService.PostAsync<JobTransition, Filter>($"{_baseURL}/GetJobTransitionDeleteList", filter);
        }
        public async Task<CompensationStructure> GetSalaryStructureAlwDedGrid(int EmpCode, int SalaryStructureCode, decimal Gross, int VDAGroup)
        {
            return await _httpService.GetAsync<CompensationStructure>($"{_baseURL}/GetSalaryStructureAlwDedGrid?EmpCode={EmpCode}&SalaryStructureCode={SalaryStructureCode}&Gross={Gross}&VDAGroup={VDAGroup}");
        }
        public async Task<CompensationStructure> GetSalaryStructureAlwDedGridGeojit(int EmpCode, int SalaryStructureCode, decimal CTC/*GrossSalary*/, int VDAGroup,
            decimal NewBasic, decimal ABNewValue, int AllowanceCode, decimal AllowanceAmt, int NewDepartment, int NewLocation)
        {
            return await _httpService.GetAsync<CompensationStructure>($"{_baseURL}/GetSalaryStructureAlwDedGridGeojit?EmpCode={EmpCode}&SalaryStructureCode={SalaryStructureCode}&CTC={CTC}&VDAGroup={VDAGroup}&NewBasic={NewBasic}&ABNewValue={ABNewValue}&AllowanceCode={AllowanceCode}&AllowanceAmt={AllowanceAmt}&NewDepartment={NewDepartment}&NewLocation={NewLocation}");/*GrossSalary={GrossSalary}*/
        }
        public async Task<ResponseEntity> Delete(Guid HistoryGI,string Remarks)
        {
            return await _httpService.DeleteAsync<ResponseEntity>($"{_baseURL}/DeleteJobTransition?HistoryGI={HistoryGI}&EntryComments={Remarks}");
        }

       public async Task<JobTransition> GetRenewalDate(Guid EmployeeGI, int CategoryCode, string EffectiveDate)
        {
            return await _httpService.GetAsync<JobTransition>($"{_baseURL}/GetRenewalDate?EmployeeGI={EmployeeGI}&CategoryCode={CategoryCode}&EffectiveDate={EffectiveDate}");

        }
        public async Task<JobTransition> GetNewSubordinates(int PositionCode, int EmployeeCode)
        {
            return await _httpService.GetAsync<JobTransition>($"{_baseURL}/GetNewSubordinates?PositionCode={PositionCode}&EmployeeCode={EmployeeCode}");

        }
        public async Task<JobTransition> GetJobTransitionReason(string ChangingParameters, int CurrentWageGrade, int NewWageGrade)
        {
            return await _httpService.GetAsync<JobTransition>($"{_baseURL}/GetJobTransitionReason?ChangingParameters={ChangingParameters}&CurrentWageGrade={CurrentWageGrade}&NewWageGrade={NewWageGrade}");

        }
        public async Task<JobTransition> GetJobTransitionSummary(string ChangingParameters, int CurrentWageGrade, int NewWageGrade)
        {
            return await _httpService.GetAsync<JobTransition>($"{_baseURL}/GetJobTransitionSummary?ChangingParameters={ChangingParameters}&CurrentWageGrade={CurrentWageGrade}&NewWageGrade={NewWageGrade}");

        }
        public async Task<JobTransition> SalaryDetails_JobTransition(JobTransition model)
        {
            return await _httpService.PostAsync<JobTransition, JobTransition>($"{_baseURL}/SalaryDetails_JobTransition", model);
        }
        //Edit
        public async Task<JobTransition> GetJobTransitionEditList(int Tabindex, int SearchId, Filter filter)
        {
            return await _httpService.PostAsync<JobTransition, Filter>($"{_baseURL}/GetJobTransitionEditList?Tabindex={Tabindex}&SearchId={SearchId}", filter);
        }
        public async Task<JobTransition> GetJobTransitionEditDetails(Guid EmployeeGI, Guid HistoryGI, string Mode)
        {
            return await _httpService.GetAsync<JobTransition>($"{_baseURL}/GetJobTransitionEditDetails?EmployeeGI={EmployeeGI}&HistoryGI={HistoryGI}&Mode={Mode}");

        }

        public async Task<ResponseEntity> GetJobTransitionPRApplicableValidation(string EffectiveDate, int EmployeeCode)
        {
            return await _httpService.GetAsync<ResponseEntity>($"{_baseURL}/GetJobTransitionPRApplicableValidation?EffectiveDate={EffectiveDate}&EmployeeCode={EmployeeCode}");

        }
    }
}
