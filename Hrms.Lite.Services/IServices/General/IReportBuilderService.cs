using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.General
{
   public interface IReportBuilderService
    {

        #region DATABANK
        public Task<ReportBuilder> GetDataBankReportBuilder_Fields();
        public Task<ReportBuilder> GetDataBankReportBuilder_SavedReports();
        public Task<ReportBuilder> GetDataBankReportBuilder_EditDetails( int ReportCode);
        public Task<DataSet> GetDataBankReportBuilder( ReportBuilder model);
        public Task<DataSet> GetDataBankReportBuilder_SavedReport( int ReportCode, MultiSelectFilter Filter);
        public Task<ResponseEntity> DataBankReportBuilder_Save(ReportBuilder model);
        public Task<ResponseEntity> DataBankReportBuilder_Delete( int ReportCode);

        #endregion

        #region PAYROLL
        public Task<ReportBuilder> GetPayRollReportBuilder_Fields();
        public Task<ReportBuilder> GetPayrollReportBuilder_EditDetails( int ReportCode);
        public Task<ReportBuilder> GetPayrollReportBuilder_SavedReports();
        public Task<DataSet> GetPayRollReportBuilder(ReportBuilder model);
        public Task<DataSet> GetPayRollReportBuilder_SavedReport(int ReportCode, MultiSelectFilter Filter);
        public Task<ResponseEntity> PayRollReportBuilder_Save(ReportBuilder model);
        public Task<ResponseEntity> PayRollReportBuilder_Delete( int ReportCode);
        #endregion
    }
}
