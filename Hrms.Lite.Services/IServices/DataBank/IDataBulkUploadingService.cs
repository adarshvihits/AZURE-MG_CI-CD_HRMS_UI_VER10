using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
    public interface IDataBulkUploadingService
    {
        Task<ResponseEntity<List<DataBulkUploadingList>>> ImportFromExcel(DataBulkUploading model);
        Task<DataSet> BulkUploading_ExportToExcel(int BulkUploadingTypeCode,int radioSelection, Filter filter);
        Task<ResponseEntity> Save(DataBulkUploading input);
        Task<DataBulkUploading> GetList(int BulkUploadingTypeCode,int radioSelection,int EntrytypeCode, Filter filter);
        public Task<DataBulkUploading> GetApplicableList(int BulkUploadingTypeCode, int EntrytypeCode,int radioSelection,Filter filter);
        Task<ResponseEntity> ApplicabilitySave(DataBulkUploading model);
        Task<DataSet> GetApplicableList_Excel(int BulkUploadingTypeCode, int radioSelection, Filter filter);
    }
}

