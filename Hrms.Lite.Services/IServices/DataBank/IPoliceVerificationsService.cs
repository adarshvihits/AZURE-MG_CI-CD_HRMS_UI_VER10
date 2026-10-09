using Hrms.Lite.Shared.DataBank;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
  public  interface IPoliceVerificationsService
    {

        public Task<PoliceVerification> GetList(string TabIndex,int Type,Filter filter);
        public Task<ResponseEntity> Save(PoliceVerification input);
        public Task<PoliceVerification> GetVerificationDetails(Guid VerificationGI,Guid EmpGI );
        public Task<ResponseEntity> Delete(Guid VerificationGI);
        Task<DataSet> GetPoliceVerificationList_Excel(string TabIndex, int Type, Filter filter);

    }
}
