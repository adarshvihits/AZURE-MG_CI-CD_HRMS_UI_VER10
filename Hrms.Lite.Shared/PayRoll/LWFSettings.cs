using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.PayRoll
{

    public class LWFSettings
    {
       
        public int DeductionCode { get; set; }
        public int StateCode { get; set; }
        public string State { get; set; }
        public string Months { get; set; }
        public string ELWF { get; set; }
        public string MLWF { get; set; }
        public List<LWFSettings> LWFSettingsList { get; set; }
        public List<LWFSettingsStateDetails> LWFSettingsStateDetailsList { get; set; }

    }
    public class LWFSettingsStateDetails
    {
        public int MonthCode { get; set; }
        public bool Applicability { get; set; }
        public string Month { get; set; }
        public string ELWF { get; set; }
        public string MLWF { get; set; }

    }
}
