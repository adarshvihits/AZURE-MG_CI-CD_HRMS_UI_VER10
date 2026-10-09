using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.Master
{
    public class HRLetterSettings
    {
        public List<HRLetterSettings> HRLetterSettingsList { get; set; }

        public int LetterType { get; set; }

        public string SigName { get; set; }

        public string SigDesignation { get; set; }

        public string CompanyName { get; set; }

        public File Attachment1 { get; set; }

        public File Attachment2 { get; set; }

        public UserBase User { get; set; }

        public string IndexNum { get; set; }
    }
}
