using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.TDS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.TDS
{
   public interface IRegimeService
    {
        public Task<List<RegimeSelectionEmployeeList>> GetRegimeEmployeeList(int AssessmentYear, Filter filter);
        public Task<ResponseEntity> SaveRegimeSelection(RegimeSelectionEmployeeList model, int AssessmentYear);
        public Task<ResponseEntity> BatchUpdateRegimeSelection(RegimeBatchUpdate model);
    }
}
