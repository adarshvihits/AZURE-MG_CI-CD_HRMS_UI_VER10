using Hrms.Lite.Shared;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.DataBank.Generic;
using Hrms.Lite.Shared.DataBank.Generic.PersonalDetails;
using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
    public interface IEmployeeProfileService
    {
        public Task<EmployeeProfile> GetEmployeeProfileList(int Tabindex, Filter filterdata);

        public Task<System.Data.DataSet> GetEmployeeProfileList_Excel( int TabIndex, Filter filterdata);
        public Task<EmployeeProfile> GetEmployeeProfileDetails(Guid EmployeeGI, int FinancialYear);
        public Task<List<Certifications>> GetCertificationsList(Guid EmployeeGI,int TabIndex );
        public Task<EmployeeProfile> GetEmployeeSummary(int GroupBy, Filter filter);
        public Task<CompensationStructureReport> GetCompensationDetails(Guid EmpGI);
        //public Task<CompensationStructureReport> GetEmployeeSummary(int GroupBy, Filter filter);\
        Task<File> EmpProfileDocView(int Code,string DocType);
        public Task<List<DocumentList>> GetCertificationDocumentList( int LogCode);
    }
}
