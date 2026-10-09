using Hrms.Lite.Shared;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TDS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace Hrms.Lite.Services.IServices.TDS
{
  public  interface IInvestmentDeclarationApprovalService
    {
        public Task<InvestmentDeclarationDropdown> GetTDSDropDown(int AssessmentYear);
        public Task<InvestmentDeclaration> GetApprovalList(int AssessmentYear, Filter filter);
        public Task<InvestmentDeclaration> GetInvDeclarationApprovalDetails(Guid EmployeeGI, int AssessmentYear);
        public Task<ResponseEntity> SaveInvestmentDeclarationApproval(InvestmentDeclaration InvestmentData);
        public Task<List<DocumentList>> GetDocumentList(Guid EmployeeGI, int AssessmentYear, string idType, string idRef);
        public Task<File> GetDocumentForView(int TrnCode);
    }
}
