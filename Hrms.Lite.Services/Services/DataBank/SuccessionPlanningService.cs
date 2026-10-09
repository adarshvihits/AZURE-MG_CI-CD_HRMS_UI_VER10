using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank
{
    public class SuccessionPlanningService : ISuccessionPlanningService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public SuccessionPlanningService(IServiceBase httpService)
        {
            _baseURL = "SuccessionPlanning";
            _httpService = httpService;
        }
        public async Task<SuccessionPlanning> GetList(string TabIndex,Filter filter)
        {
            return await _httpService.PostAsync<SuccessionPlanning,Filter>($"{_baseURL}/SuccessionPlanningList?TabIndex={TabIndex}",filter);
        }
        public Task<SuccessionPlanning> GetSuccessionlanningDetails(Guid KeyPositionGI, Guid EmployeeGI)
        {
            return _httpService.GetAsync<SuccessionPlanning>($"{_baseURL}/SuccessionPlanningDetails?KeyPositionGI={KeyPositionGI}&EmployeeGI={EmployeeGI}");
        }



        //public Task<SuccessionPlanning> GetEmployeeList(int EmployeesType, Guid KeyPositionGI, Guid EmployeeGI)
        //{
        //    return _httpService.GetAsync<SuccessionPlanning>($"{_baseURL}/SuccessorEmployeeList?EmployeesType={EmployeesType}&KeyPositionGI={KeyPositionGI}&EmployeeGI={EmployeeGI}");
        //}

        public Task<SuccessionPlanning> GetEmployeeList(int EmployeesType, Guid KeyPositionGI, Guid EmployeeGI, Filter filter)
        {
            return _httpService.PostAsync<SuccessionPlanning, Filter>($"{_baseURL}/SuccessorEmployeeList?EmployeesType={EmployeesType}&KeyPositionGI={KeyPositionGI}&EmployeeGI={EmployeeGI}", filter);
        }

        public async Task<ResponseEntity> Save(SuccessionPlanning model)
        {
            return await _httpService.PostAsync<ResponseEntity, SuccessionPlanning>($"{_baseURL}/Create", model);
        }

        //public async Task<ResponseEntity> Delete(SuccessionPlanning model)
        //{
        //    return await _httpService.PostAsync<ResponseEntity, SuccessionPlanning>($"{_baseURL}/Delete", model);
        //}

        async Task<ResponseEntity> ISuccessionPlanningService.Delete(Guid SuccessionPlanningGI)
        {
            return await _httpService.DeleteAsync<ResponseEntity>($"{_baseURL}/Delete?SuccessionPlanningGI={SuccessionPlanningGI}");
        }
    }   
}
