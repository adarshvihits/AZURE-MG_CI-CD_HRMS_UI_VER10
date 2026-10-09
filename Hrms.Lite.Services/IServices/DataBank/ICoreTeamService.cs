using Hrms.Lite.Shared.DataBank;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.DataBank
{
    public interface ICoreTeamService
    {
        Task<CoreTeam> GetCoreTeamList(int TabIndex , Filter filter);
        Task<ResponseEntity> CoreTeamSave(CoreTeam model);
        Task<DataSet> GetCoreTeam_Excel(int TabIndex, Filter filter);
    }

}
