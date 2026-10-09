using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
    public interface IDuesClearanceAuthorityMatrixService
    {
        public Task<DuesClearanceAuthorityMatrix> GetList();
        public Task<DuesClearanceAuthorityMatrix> GetClearanceVsAuthorityPopup(int LevelCode, Filter filter);
        public Task<ResponseEntity> Create(DuesClearanceAuthorityMatrix input);
        public Task<DataSet> GetList_Excel();
    }
}
