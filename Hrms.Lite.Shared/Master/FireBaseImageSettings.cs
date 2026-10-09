using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.Master
{
   public class FireBaseImageSettings
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public string FileName { get; set; }
        public bool Enablenotification { get; set; }
        public File Attachment { get; set; }
        public string Base64string { get; set; }
        public string AbsoluteUri { get; set; }
        public int TabIndex { get; set; }
        public MobileGeneralSettings MobileGeneralSettings { get; set; }
        public List<FireBaseImageSettings> FireBaseImageSettingsList { get; set; }
    }
    public class MobileGeneralSettings
    {
        
        public string ThemeColourCode { get; set; }
        public string GoogleMapKey { get; set; }
        public int MobilePunchingRadius { get; set; }
        public bool IsSingleDeviceValidation { get; set; }
        //public bool RestrictMultipleDeviceLogin { get; set; }
        public string MobileAppRestrictionBasedOn { get; set; }
        public int MaximumMobilePunch { get; set; }
        public bool EnableFirebaseNotification { get; set; }
        public string DigitalIdFromat { get; set; }
        public string DirectoryListBasedOn { get; set; }
        public string BloodBankListBasedOn { get; set; }
    }
}
