using Hrms.Lite.Services.IServices;
using Hrms.Lite.Services.IServices.General;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.Services
{
 public  class ReportBuilderService : IReportBuilderService
    {
        private readonly string _baseURL;
        private readonly IServiceBase _httpService;
        public ReportBuilderService(IServiceBase httpService)
        {
            _baseURL = "ReportBuilder";
            _httpService = httpService;
        }
        #region DATABANK
        public async Task<ReportBuilder> GetDataBankReportBuilder_Fields()
        {
            return await _httpService.GetAsync<ReportBuilder>($"{_baseURL}/GetDataBankReportBuilder_Fields");
        }

      public async Task<ResponseEntity> DataBankReportBuilder_Save(ReportBuilder model)
        {
            return await _httpService.PostAsync<ResponseEntity,ReportBuilder>($"{_baseURL}/DataBankReportBuilder_Save", model);
        }

      public async Task<DataSet> GetDataBankReportBuilder(ReportBuilder model)
        {
            return await _httpService.PostAsync<DataSet, ReportBuilder>($"{_baseURL}/GetDataBankReportBuilder", model);
        }

     public async Task<ReportBuilder> GetDataBankReportBuilder_SavedReports()
        {
            return await _httpService.GetAsync<ReportBuilder>($"{_baseURL}/GetDataBankReportBuilder_SavedReports");
        }

       public async Task<ReportBuilder> GetDataBankReportBuilder_EditDetails(int ReportCode)
        {
            return await _httpService.GetAsync<ReportBuilder>($"{_baseURL}/GetDataBankReportBuilder_EditDetails?ReportCode={ReportCode}" );
        }

     public async Task<DataSet> GetDataBankReportBuilder_SavedReport(int ReportCode, MultiSelectFilter Filter)
        {
            return await _httpService.PostAsync<DataSet, MultiSelectFilter>($"{_baseURL}/GetDataBankReportBuilder_SavedReport?ReportCode={ReportCode}", Filter);

        }

     public async Task<ResponseEntity> DataBankReportBuilder_Delete(int ReportCode)
        {
            return await _httpService.PostAsync<ResponseEntity, Int32>($"{_baseURL}/DataBankReportBuilder_Delete", ReportCode);

        }
        #endregion

        #region PAYROLL
      public async Task<ReportBuilder> GetPayRollReportBuilder_Fields()
        {
            return await _httpService.GetAsync<ReportBuilder>($"{_baseURL}/GetPayRollReportBuilder_Fields");
        }

      public async Task<ReportBuilder> GetPayrollReportBuilder_EditDetails(int ReportCode)
        {
            return await _httpService.GetAsync<ReportBuilder>($"{_baseURL}/GetPayrollReportBuilder_EditDetails?ReportCode={ReportCode}");

        }

        public async Task<ReportBuilder> GetPayrollReportBuilder_SavedReports()
        {
            return await _httpService.GetAsync<ReportBuilder>($"{_baseURL}/GetPayrollReportBuilder_SavedReports");

        }

        async Task<DataSet> IReportBuilderService.GetPayRollReportBuilder(ReportBuilder model)
        {
            return await _httpService.PostAsync<DataSet, ReportBuilder>($"{_baseURL}/GetPayRollReportBuilder", model);

        }

        public   async Task<DataSet> GetPayRollReportBuilder_SavedReport(int ReportCode, MultiSelectFilter Filter)
        {
            return await _httpService.PostAsync<DataSet, MultiSelectFilter>($"{_baseURL}/GetPayrollReportBuilder_SavedReport?ReportCode={ReportCode}", Filter);

        }

        public async Task<ResponseEntity> PayRollReportBuilder_Save(ReportBuilder model)
        {
            return await _httpService.PostAsync<ResponseEntity, ReportBuilder>($"{_baseURL}/PayRollReportBuilder_Save", model);

        }

        public async Task<ResponseEntity> PayRollReportBuilder_Delete(int ReportCode)
        {
            return await _httpService.PostAsync<ResponseEntity, Int32>($"{_baseURL}/PayRollReportBuilder_Delete", ReportCode);
        }

        #endregion
    }
}
