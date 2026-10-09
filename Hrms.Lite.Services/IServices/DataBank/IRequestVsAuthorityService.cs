using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.DataBank.Reports;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
   public interface IRequestVsAuthorityService
    {
        public Task<RequestVsAuthority> GetRequestVsAuthorityList(int Tabindex, int SearchId,Filter filter, int RequestCode, int LEVEL);
        public Task<ResponseEntity> RequestVsAuthorityBatchUpdate(RequestVsAuthority model,int Tabindex, int SearchId);
        public Task<ResponseEntity> RequestVsAuthorityBatchUpdateValidation(RequestVsAuthority model,int Tabindex, int SearchId);
        public Task<ResponseEntity> RequestVsAuthoritySave_Delete(RequestVsAuthority model);
        public Task<RequestVsAuthority> GetRequestVsAuthorityLevelList(Guid EmployeeGI, int code);
        public Task<RequestVsAuthority> GetRequestVsAuthorityDetailsAddNew(Guid EmployeeGI, int code);
        public Task<RequestVsAuthority> GetRequestVsAuthorityLogList(Guid EmployeeGI);
        public Task<RequestVsAuthority> GetRequestVsAuthorityLevelDetails(Guid EmployeeGI, Guid ReqVsAuthGI);
        public Task<RequestVsAuthority> AlreadyExistCheck(RequestVsAuthority model);
        Task<DataSet> GetRequestVsAuthorityList_Excel(int Tabindex, int SearchId, int RequestCode, int LEVEL,Filter filter);
        public Task<ResponseEntity<List<RequestVsAuthority>>> ImportFromExcel(RequestVsAuthority model);

        public Task<ResponseEntity> EmployeeVSChangeAuthorityBatchUpdate( Reportees model);


    }
}
