using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
   public interface ISuccessionPlanningService
    {
        public Task<SuccessionPlanning> GetList(string TabIndex,Filter filter);
        public Task<SuccessionPlanning> GetSuccessionlanningDetails(Guid KeyPositionGI,Guid EmployeeGI);
        public Task<SuccessionPlanning> GetEmployeeList(int EmployeesType,Guid KeyPositionGI,Guid EmployeeGI,Filter filter);
        //public Task<SuccessionPlanning> GetEmployeeList(int EmployeesType, Guid KeyPositionGI, Guid EmployeeGI);

        Task<ResponseEntity> Save(SuccessionPlanning model);
        public Task<ResponseEntity> Delete(Guid SuccessionPlanningGI);
        //public Task<ResponseEntity> Delete(SuccessionPlanning model);

    }
}
