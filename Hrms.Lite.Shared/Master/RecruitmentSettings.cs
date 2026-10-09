using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.Master
{
   public class RecruitmentSettings
    {
        public string RecruitmentSettingsTypeName { get; set; }
        public int RecruitmentSettingsTypeCode { get; set; }
        public int RecruitmentSettingsType { get; set; } //VALUE
        public bool Applicable { get; set; }
        public MasterBase Designation { get; set; }
        public Guid DesignationGI { get; set; }
        public int MRFApprovalRequired { get; set; }
        public int NoOfInterviewLevels { get; set; }
        public int DrivingLicenseMandatory { get; set; }
        public int BudgetingApplicable { get; set; }
        public int NewMRFRequestPermission { get; set; }
        public int DocumentVerificationRequired { get; set; }
        public int CreditCheckRequired { get; set; }
        public int EmploymentCheckRequired { get; set; }
        public List<RecruitmentSettings> RecruitmentSettingsList { get; set; }
    }
}
