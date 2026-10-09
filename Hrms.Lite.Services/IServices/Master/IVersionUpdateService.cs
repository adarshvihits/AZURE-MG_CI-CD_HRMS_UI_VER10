using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hrms.Lite.Services.IServices.Master
{
   public interface IVersionUpdateService
    
    {
        public Task<VersionUpdate> GetVersionUpdateList(int VersionCode);
        public  Task<ResponseEntity> VersionUpdateSave(VersionUpdate input);
        public  Task<VersionUpdate> GetVersionsList();
        public Task<VersionUpdate> GetVersionUpdateDetails(Guid VersionUpdateGI);
        public Task<ResponseEntity> Create(VersionUpdate input);
        public  Task<ResponseEntity> NewVersionSave(List<NewVersion> input);
        public Task<ResponseEntity<List<VersionUpdate>>> ImportFromExcel(VersionUpdate model);
        public Task<ResponseEntity> Delete(Guid GI);
    }
}
