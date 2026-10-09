using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
 public  interface IHRPlannningService
    {
        public Task<HRPlanning> GetPlanningDetails(int HRPlanningCode);
        public Task<HRPlanning> GetHRPlanningCombination2GridDetails(int HRPlanningCode,int Combination1TrnCode);       
        public Task<HRPlanning> GetCurrentMonthPlanningDetails();
        public Task<HRPlanning> GetHRPlanningList(int HRPlanningCode, int Combination1TrnCode, int BasedOn, string monthCode, Filter Filter);

        public Task<HRPlanning> GetHRPlanningDetailsForModify(int HRPlanningCode, int Combination1TrnCode,  int Combination2TrnCode);
        public Task<ResponseEntity> HRPlanningSave(HRPlanning input);
        public Task<ResponseEntity> HRPlanningModify(HRPlanning input);
        public Task<ResponseEntity> HRPlanningDelete(int? HRPlanningCode, int? Combination1TrnCode, int? Combination2TrnCode);

        public Task<HRPlanning> GetEmployeeList(int HRplanCode, int Combination1Code, int Combination2Code);
    }
}
