using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
    public interface ITrainingService
    {
        public Task<IList<TrainingListTabs>> GetTrainingTabs();
        public Task<Training> GetTrainingList(int Tabindex, int SearchId, int? TrainingTabCode, Filter Filter);
        public Task<Training> GetTrainingDetails(Guid EmployeeTrainingGI);
        public Task<Training> GetTrainingTypeDetails(int TrainingCode);
        public Task<IList<TrainingLog>> GetTrainingLogList(Guid EmployeeGI,int SearchId);
        public Task<ResponseEntity> TrainingSave(Training model);
        public Task<ResponseEntity> TrainingDetailsDelete(Guid EmployeeTrainingGI);

    }
}
