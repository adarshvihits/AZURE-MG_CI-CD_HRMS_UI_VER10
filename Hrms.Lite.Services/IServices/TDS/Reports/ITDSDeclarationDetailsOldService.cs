using Hrms.Lite.Shared;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TDS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TDS.Reports
{
  public  interface ITDSDeclarationDetailsOldService
    {
        public Task<InvestmentDeclaration> GetEmployeeList(int AssessmentYear, Filter filter);
        public Task<InvestmentDeclarationDropdown> GetTDSDropDown(int AssessmentYear);
        public Task<InvestmentDeclaration> GetInvestmentDeclarationDetails(Guid EmployeeGI, int AssessmentYear);
        public Task<List<DocumentList>> GetDocumentList(Guid EmployeeGI, int AssessmentYear, string idType, string idRef);
        public Task<File> GetDocumentForView(int TrnCode);
    }
}
