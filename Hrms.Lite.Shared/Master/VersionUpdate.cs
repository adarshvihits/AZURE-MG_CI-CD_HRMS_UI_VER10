using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.Master
{
    public class VersionUpdate : MasterBase
    {
        public UserBase User { get; set; }
        public File Excel { get; set; }
        public MasterBase Version { get; set; }
        public int SlNo { get; set; }
        public MasterBase Module { get; set; }
        public MasterBase Type { get; set; }
        public string Message { get; set; }
        public string VersionName { get; set; }
        public string ModuleName { get; set; }
        public string TypeName { get; set; }
        public string VersionDescription { get; set; }
        public List<VersionUpdate> VersionUpdateList { get; set; }
        public List<NewVersion> NewVersionList { get; set; }

    }
    public class NewVersion 
    {
        public int SlNo { get; set; }
        public int? Code { get; set; }
        public Guid? GI { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public int InUse { get; set; }
    }

}
