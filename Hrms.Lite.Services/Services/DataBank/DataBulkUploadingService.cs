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
    public class DataBulkUploadingService : IDataBulkUploadingService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public DataBulkUploadingService(IServiceBase httpService)
        {
            _baseURL = "DataBulkUploading";
            _httpService = httpService;
        }
        async Task<DataBulkUploading> IDataBulkUploadingService.GetList(int BulkUploadingTypeCode,int radioSelection,int EntrytypeCode, Filter filter)
        {
            return await _httpService.PostAsync<DataBulkUploading, Filter>($"{_baseURL}/List?BulkUploadingTypeCode={BulkUploadingTypeCode}&radioSelection={radioSelection}&EntrytypeCode={EntrytypeCode}", filter);
        }
        async Task<DataBulkUploading> IDataBulkUploadingService.GetApplicableList(int BulkUploadingTypeCode,int EntrytypeCode,int radioSelection, Filter filter)
        {
            return await _httpService.PostAsync<DataBulkUploading, Filter>($"{_baseURL}/GetApplicableList?BulkUploadingTypeCode={BulkUploadingTypeCode}&EntrytypeCode={EntrytypeCode}&radioSelection={radioSelection}", filter);
        }
        async Task<ResponseEntity> IDataBulkUploadingService.Save(DataBulkUploading input)
        {
            return await _httpService.PostAsync<ResponseEntity, DataBulkUploading>($"{_baseURL}/Save", input);
        }
        async Task<ResponseEntity> IDataBulkUploadingService.ApplicabilitySave(DataBulkUploading model)
        {
            return await _httpService.PostAsync<ResponseEntity, DataBulkUploading>($"{_baseURL}/ApplicabilitySave", model);

        }
        async Task<DataSet> IDataBulkUploadingService.BulkUploading_ExportToExcel(int BulkUploadingTypeCode,int radioSelection, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/BulkUploading_ExportToExcel?BulkUploadingTypeCode={BulkUploadingTypeCode}&radioSelection={radioSelection}", filter);
        }
        async Task<ResponseEntity<List<DataBulkUploadingList>>> IDataBulkUploadingService.ImportFromExcel(DataBulkUploading model)
        {
            return await _httpService.PostAsync<ResponseEntity<List<DataBulkUploadingList>>, DataBulkUploading>($"{_baseURL}/ImportFromExcel", model);
        }
        async Task<DataSet> IDataBulkUploadingService.GetApplicableList_Excel(int BulkUploadingTypeCode, int radioSelection, Filter filter)
        {
            return await _httpService.PostAsync<DataSet, Filter>($"{_baseURL}/GetApplicableList_Excel?BulkUploadingTypeCode={BulkUploadingTypeCode}&radioSelection={radioSelection}", filter);
        }
    }
}

