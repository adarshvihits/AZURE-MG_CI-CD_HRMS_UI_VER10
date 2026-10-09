using Hrms.Lite.Shared.DataBank;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
    public interface IJobTransitionTypeService
    {
        public Task<JobTransitionType> GetList();

        public Task<ResponseEntity> Save(JobTransitionType model);
        public Task<DataSet> GetJobTransitionType_Excel(int groupCompanyCode);
    }
}
