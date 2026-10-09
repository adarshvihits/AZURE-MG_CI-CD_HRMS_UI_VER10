using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.DataBank.DTO;
using Hrms.Lite.Shared.General;
using Hrms.Lite.Shared.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.Generic
{
    public class EmployeeBasicInfo
    {
        public Salutation Salutation { get; set; }
        public string FullName { get; set; }
        public DateTime? DOB { get; set; }
        public Gender Gender { get; set; }
        public string FathersName { get; set; }
        public string AdharNo { get; set; }
        public string CandidatePhotoFileName { get; set; }
        public string PanNo { get; set; }
        public DateTime? AcceptedDOJ { get; set; }
        public DateTime? GroupDOJ { get; set; }
        public DateTime? TentativeDOJ { get; set; }
        public DateTime? RenewalDate { get; set; }
        public DateTime? ConfirmationDate { get; set; }
        public string RetirementType { get; set; }
        public File File { get; set; }
        public File EmployeeThumbNail { get; set; }
        public File OfferLetter { get; set; }
        public Relation Relation { get; set; }
        public PositionManagement Position { get; set; }
        public string PositionNewReplacement { get; set; }
    }
}
