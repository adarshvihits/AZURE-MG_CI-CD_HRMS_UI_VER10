using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
    public interface IInductionTrainingService
    {
        public Task<InductionTraining> GetInductionList(Filter filter);
        public Task<InductionTraining> GetInductionTrainingList();
        public Task<ResponseEntity> InductionSave(InductionTraining model);
        public Task<DataSet> ExportToExcel(Filter Filter);
    }
}
