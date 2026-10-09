using Hrms.Lite.Shared.DataBank;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
    public interface IAdditionalResponsibilityService
    {
        public Task<AdditionalResponsibility> AdditionalResponsibilityList(String TabIndex);
        public Task<AdditionalResponsibility> GetAdditionalResponsibility(Guid EmployeeGI, String TabIndex);
        Task<ResponseEntity> SaveAdditionalResponsibilityDetails(AdditionalResponsibility Model);
        Task<File> DownloadDoc(Guid RequestGI, Guid EmployeeGI);
    }
}
