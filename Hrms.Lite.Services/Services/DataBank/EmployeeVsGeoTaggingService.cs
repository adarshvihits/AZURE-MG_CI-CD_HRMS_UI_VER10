using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.DataBank;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.DataBank
{
    public class EmployeeVsGeoTaggingService : IEmployeeVsGeoTaggingService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public EmployeeVsGeoTaggingService(IServiceBase httpService)
        {
            _baseURL = "EmployeeVsGeoTagging";
            _httpService = httpService;
        }
        async Task<EmployeeVsGeoTagging> IEmployeeVsGeoTaggingService.GetList(Filter filter)
        {
            return await _httpService.PostAsync<EmployeeVsGeoTagging, Filter>($"{_baseURL}/GetList", filter);
        }
        async Task<DataSet> IEmployeeVsGeoTaggingService.GetList_Excel(Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetList_Excel", filter);
        }
        async Task<EmployeeVsGeoTagging> IEmployeeVsGeoTaggingService.GetGeoTaggingDetails(Guid EmployeeGI)
        {
            return await _httpService.GetAsync<EmployeeVsGeoTagging>($"{_baseURL}/GetGeoTaggingDetails?EmployeeGI={EmployeeGI}");
        }
        async Task<ResponseEntity> IEmployeeVsGeoTaggingService.GeoTaggingSave(EmployeeVsGeoTagging model)
        {
            return await _httpService.PostAsync<ResponseEntity, EmployeeVsGeoTagging>($"{_baseURL}/GeoTaggingSave", model);
        }
    }
}
