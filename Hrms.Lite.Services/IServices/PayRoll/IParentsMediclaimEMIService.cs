using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll
{
    public interface IParentsMediclaimEMIService
    {
        Task<ParentsMediclaimEMI> GetList(int Month, int Finyear, Filter filter);
        Task<DataSet> GetList_Excel(int Month, int Finyear, Filter filter);
        Task<ResponseEntity> Save(int Month, int Finyear, Filter filter);
    }
}
