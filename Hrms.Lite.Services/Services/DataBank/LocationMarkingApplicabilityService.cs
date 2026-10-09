using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Shared.DataBank;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank
{
    public class LocationMarkingApplicabilityService: ILocationMarkingApplicabilityService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public LocationMarkingApplicabilityService(IServiceBase httpService)
        {
            _baseURL = "LocationMarkingApplicability";
            _httpService = httpService;
        }
        public async Task<LocationMarkingApplicability> GetList(int TabIndex, Filter filter)
        {
            return await _httpService.PostAsync<LocationMarkingApplicability, Filter>($"{_baseURL}/GetEmpList?TabIndex={TabIndex}", filter);
        }
        public async Task<ResponseEntity> ApplicableEmpSave(LocationMarkingApplicability model)
        {
            return await _httpService.PostAsync<ResponseEntity, LocationMarkingApplicability>($"{_baseURL}/ApplicableEmpSave", model);
        }
        public async Task<DataSet> GetApplicableEmp_Excel(int TabIndex, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetApplicableEmp_Excel?TabIndex={TabIndex}", filter);
        }
    }
}
