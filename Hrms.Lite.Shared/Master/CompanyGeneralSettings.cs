using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.Master
{
    public class CompanyGeneralSettings
    {
        public List<CompanyGeneralSettings> CompanyGeneralSettingsList { get; set; }

        public string Settings { get; set; }

        public int NoticePeriod { get; set; }
        
        public int PasswordExpiry { get; set; }

        public Boolean NeedExcelPassword { get; set; }

        public int PunchingIDSameasComID { get; set; }

        public int SocialWallExpiry { get; set; }

        public int ShowCelebritiesinsocial { get; set; }

        public int ShowPhotoinsocial { get; set; }

        public int RegularizationMaximumLimit { get; set; }

        public int RegularizationApplyWithinDays { get; set; }
        public int LoanWFEnableOrDisable { get; set; }

    }
}
