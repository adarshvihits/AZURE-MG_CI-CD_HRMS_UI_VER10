using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.PayRoll;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.PayRoll
{
  public  interface IJVPostingService
    {
        public Task<JVPosting> GetList(int Month,int PayrollGroup,int Type,  Filter filter);
        public Task<ResponseEntity> Save(JVPosting input);
        public Task<ResponseEntity<List<JVCheckList>>> CheckJVLinking(int MonthCode);
    }
}
