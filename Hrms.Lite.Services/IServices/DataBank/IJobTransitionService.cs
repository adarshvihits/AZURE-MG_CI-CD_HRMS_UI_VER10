using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.DataBank.Generic;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
   public  interface IJobTransitionService
    {
        public Task<IList<JobTransition_Tabs>> GetJobTransitionTabs();
        public Task<IList<JobTransition_Tabs>> GetJobTransitionDueListTabs();
        public Task<JobTransition> GetJobTransitionList(int Tabindex, int SearchId,Filter filter);
        public Task<JobTransition> GetJobTransitionConfirmList(int Tabindex, int SearchId, Filter filter);
        public Task<JobTransition> GetJobTransitionDueList(int Tabindex, int SearchId,Filter filter);
        public Task<JobTransition> GetJobTransitionLogList(string FromDate, string ToDate, int Tabindex, int? TransitionType, string Type, Filter filter);
        public Task<JobTransition> GetJobTransitionDeleteList(Filter filter);
        public Task<CompensationStructure> GetSalaryStructureAlwDedGrid(int EmpCode, int SalaryStructureCode, decimal Gross,int VDAGroup);
        public Task<CompensationStructure> GetSalaryStructureAlwDedGridGeojit(int EmpCode, int SalaryStructureCode, decimal CTC/*GrossSalary*/, int VDAGroup,
            decimal NewBasic, decimal ABNewValue, int AllowanceCode, decimal AllowanceAmt, int NewDepartment, int NewLocation);

        public Task<JobTransition> GetJobTransitionGrowthHistory(Guid EmployeeGI);
        public Task<JobTransition> GetJobTransitionDetails( Guid EmployeeGI, Guid HistoryGI, string Mode,string? BasedOn);
        public Task<ResponseEntity> SaveJobTransition(JobTransition JobTransition);
        public Task<ResponseEntity> ApproveJobTransition(JobTransition JobTransition);
        public Task<ResponseEntity> Delete(Guid HistoryGI, string Remarks);
        public Task<DataSet> GetJobTransitionLogList_Excel(string FromDate, string ToDate, int Tabindex, int? TransitionType,string Type, Filter filter);

        public Task<JobTransition> GetRenewalDate( Guid EmployeeGI, int CategoryCode, string EffectiveDate);
        public Task<JobTransition> GetNewSubordinates(int PositionCode, int EmployeeCode);
        public Task<JobTransition> GetJobTransitionReason(string ChangingParameters, int CurrentWageGrade, int NewWageGrade);
        public Task<JobTransition> GetJobTransitionSummary(string ChangingParameters, int CurrentWageGrade, int NewWageGrade);
        public Task<JobTransition> SalaryDetails_JobTransition(JobTransition model);

        //Edit
        public Task<JobTransition> GetJobTransitionEditList(int Tabindex, int SearchId, Filter filter);
        public Task<JobTransition> GetJobTransitionEditDetails(Guid EmployeeGI, Guid HistoryGI, string Mode);
        Task<ResponseEntity> GetJobTransitionPRApplicableValidation(string EffectiveDate, int EmployeeCode);

    }
}
