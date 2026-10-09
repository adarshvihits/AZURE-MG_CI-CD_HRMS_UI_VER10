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
    public class EmployeeCustomFieldService : IEmployeeCustomFieldService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public EmployeeCustomFieldService(IServiceBase httpService)
        {
            _baseURL = "EmployeeVsCustomField";
            _httpService = httpService;
        }
        async Task<EmployeeVsCustomField> IEmployeeCustomFieldService.GetList(int CustomFieldCode, string employeestatus, Filter filter)
        {
            return await _httpService.PostAsync<EmployeeVsCustomField, Filter>($"{_baseURL}/List?CustomFieldCode={CustomFieldCode}&EmployeeStatus={employeestatus}", filter);
        }
        async Task<ResponseEntity> IEmployeeCustomFieldService.Save(EmployeeVsCustomField input)
        {
            return await _httpService.PostAsync<ResponseEntity, EmployeeVsCustomField>($"{_baseURL}/Save", input);
        }
        async Task<ResponseEntity> IEmployeeCustomFieldService.Delete(CustomField input)
        {
            return await _httpService.PostAsync<ResponseEntity, CustomField>($"{_baseURL}/Delete", input);
        }
        async Task<ResponseEntity> IEmployeeCustomFieldService.CustomFieldSave(List<CustomField> input)
        {
            return await _httpService.PostAsync<ResponseEntity, List<CustomField>>($"{_baseURL}/CustomFieldSave", input);
        }
        async Task<DataSet> IEmployeeCustomFieldService.EmployeeVsCustomField_ExportToExcel(int CustomFieldCode, string EmployeeStatus, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/EmployeeVsCustomField_ExportToExcel?CustomFieldCode={CustomFieldCode}&EmployeeStatus={EmployeeStatus}", filter);
        }
        async Task<ResponseEntity<List<EmployeeVsCustomFieldList>>> IEmployeeCustomFieldService.ImportFromExcel(EmployeeVsCustomField model)
        {
            return await _httpService.PostAsync<ResponseEntity<List<EmployeeVsCustomFieldList>>, EmployeeVsCustomField>($"{_baseURL}/ImportFromExcel", model);
        }
        async Task<CustomField> IEmployeeCustomFieldService.GetCustomFieldList()
        {
            return await _httpService.GetAsync<CustomField>($"{_baseURL}/GetCustomFieldList");
        }
    }
}
