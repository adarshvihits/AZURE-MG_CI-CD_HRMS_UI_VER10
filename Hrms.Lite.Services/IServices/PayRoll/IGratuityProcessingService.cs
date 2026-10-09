using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll
{
  public   interface IGratuityProcessingService
    {
        public Task<GratuityProcessing> GetGratuityList(Filter Filter);
        public Task<GratuityProcessing> GetGratuityDetails(Guid EmployeeGI);
        public Task<GratuityProcessing> GetProcessingStage(Guid EmployeeGI);
        public Task<GratuityProcessing_EmployeeList> GetHeaderDetails(Guid EmployeeGI);
        public Task<ResponseEntity> SaveBreakingService(GratuityProcessing model);
        public Task<GratuityProcessing> GetEmployeeVsGratuityDetails(Guid EmployeeGI);
        public Task<ResponseEntity> SaveGratuityDetails(GratuityProcessing model);
        public Task<ResponseEntity> SavePaymentDetails(GratuityProcessing model);
        public Task<DataSet> GratuityListExcel(Filter filter);
        public Task<GratuityProcessing> GetGratuityList(string From,string To,Filter Filter);

        public Task<DataSet> GratuityProcessingReport_Excel(string From, string To, Filter Filter);

    }
}
