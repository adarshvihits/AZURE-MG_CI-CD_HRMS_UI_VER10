using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.PayRoll;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services.PayRoll
{
    public class GratuityProcessingService : IGratuityProcessingService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;

        public GratuityProcessingService(IServiceBase httpService)
        {
            _baseURL = "GratuityProcessing";
            _httpService = httpService;
        }

        public async Task<GratuityProcessing> GetEmployeeVsGratuityDetails(Guid EmployeeGI)
        {
            return await _httpService.GetAsync<GratuityProcessing>($"{_baseURL}/GetEmployeeVsGratuityDetails?EmployeeGI=" + EmployeeGI);

        }

        public async Task<GratuityProcessing> GetGratuityDetails(Guid EmployeeGI)
        {
            return await _httpService.GetAsync<GratuityProcessing>($"{_baseURL}/GetEmployeeVsBreakingServiceList?EmployeeGI=" + EmployeeGI);
        }

        public async Task<GratuityProcessing> GetGratuityList(Filter Filter)
        {
            return await _httpService.PostAsync<GratuityProcessing, Filter>($"{_baseURL}/GetEmployeeList", Filter);
        }

        public async Task<GratuityProcessing_EmployeeList> GetHeaderDetails(Guid EmployeeGI)
        {
            return await _httpService.GetAsync<GratuityProcessing_EmployeeList>($"{_baseURL}/GetGratuityEmployeeHeaderDetails?EmployeeGI={EmployeeGI}");
        }

        public async Task<ResponseEntity> SaveBreakingService(GratuityProcessing model)
        {
            return await _httpService.PostAsync<ResponseEntity, GratuityProcessing>($"{_baseURL}/SaveEmployeeVsBreakingService", model);
        }

        public async Task<ResponseEntity> SaveGratuityDetails(GratuityProcessing model)
        {
            return await _httpService.PostAsync<ResponseEntity, GratuityProcessing>($"{_baseURL}/SaveEmployeeVsGratuityDetails", model);
        }

        public async Task<ResponseEntity> SavePaymentDetails(GratuityProcessing model)
        {
            return await _httpService.PostAsync<ResponseEntity, GratuityProcessing>($"{_baseURL}/SaveEmployeeVsGratuityPaymentDetails", model);
        }

        public async Task<DataSet> GratuityListExcel(Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetEmployeeList_Excel", filter);
        }

        public async Task<GratuityProcessing> GetGratuityList(string From, string To, Filter Filter)
        {
            return await _httpService.PostAsync<GratuityProcessing, Filter>($"{_baseURL}/GetEmployeeVsGratuityProcessedReport?FromDate={From}&ToDate={To}",Filter);
        }

        public async Task<GratuityProcessing> GetProcessingStage(Guid EmployeeGI)
        {
            return await _httpService.GetAsync<GratuityProcessing>($"{_baseURL}/GetGratuityProcessingStage?EmployeeGI={EmployeeGI}");
        }


        public async Task<DataSet> GratuityProcessingReport_Excel(string From, string To, Filter Filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetEmployeeVsGratutyReport_Excel?FromDate={From}&ToDate={To}", Filter);

        }


    }
}
