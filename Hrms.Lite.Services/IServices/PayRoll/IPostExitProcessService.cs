using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll
{
  public interface IPostExitProcessService
    {
        public Task<PostExitProcess> GetList(string fromdate, string todate, Filter filter);
        public Task<DataSet> GetPostExitProcessList_Excel( string FromDate, string ToDate, Filter filter);
        public Task<PostExitProcess> GetPostExitDetails(Guid EmpGI);
        public Task<ResponseEntity> Save(PostExitProcess model);
    }
}
