using Hrms.Lite.Shared.DataBank;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
   public interface IMedicalCheckupService
    {
        public Task<MedicalCheckup> GetMedicalCheckupList(int Type, string TabIndex,Filter filter);
        public Task<ResponseEntity> Save(MedicalCheckup input);
        public Task<MedicalCheckup> GetMedicalCheckUpDetails(Guid EmpGI, Guid MedicalGI);
        public Task<ResponseEntity> Delete(Guid MedicalGI);
        Task<DataSet> GetMedicalCheckupList_Excel(string TabIndex, int Type, Filter filter);

    }
}
