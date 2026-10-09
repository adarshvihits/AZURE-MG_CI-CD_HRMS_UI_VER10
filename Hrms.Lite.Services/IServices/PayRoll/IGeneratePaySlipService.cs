using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll
{
    public interface IGeneratePaySlipService
    {
        public Task<IList<PaySlipGeneration>> GetPaySlipGenerationList(int Month, int PayRollGroup,string Type, Filter filter);
       
        Task<DataTable> GetPayslipGenerationMode();
        //Blob upload-SavePayslipUploaded
        public Task<ResponseEntity> SavePayslipUploaded(PaySlipGeneration input,string FileUploadMode);
        public Task<IList<PaySlipGeneration>> GetPayslipAllEmployees(PaySlipGeneration model);
        //Database or Folder upload- GeneratePaySlip
        public Task<HttpResponseMessage> GeneratePaySlip(string FileUploadMode, PaySlipGeneration model);
        Task<DataSet> ExportToExcel(int Month,int PayrollGroup, string Type,Filter filter);
        public Task<PaySlipGeneratedStatus> GetPaySlipGeneratedStatus(int SalaryPeriod, int PayRollGroup);

    }
}
