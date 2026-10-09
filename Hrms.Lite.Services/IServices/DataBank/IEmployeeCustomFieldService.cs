using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
    public interface IEmployeeCustomFieldService
    {
        Task<CustomField> GetCustomFieldList();
        Task<ResponseEntity> CustomFieldSave(List<CustomField> input);
        Task<EmployeeVsCustomField> GetList(int CustomFieldCode,string employeestatus, Filter filter);
        Task<ResponseEntity> Save(EmployeeVsCustomField input);
        Task<ResponseEntity> Delete(CustomField input);
        Task<DataSet> EmployeeVsCustomField_ExportToExcel(int CustomFieldCode, string EmployeeStatus, Filter filter);
        Task<ResponseEntity<List<EmployeeVsCustomFieldList>>> ImportFromExcel(EmployeeVsCustomField model);
    }
}
