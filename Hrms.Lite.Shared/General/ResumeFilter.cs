namespace Hrms.Lite.Shared.General
{
    public class ResumeFilter
    {
        public int Tabindex { get; set; }
        public int[] Designations { get; set; }
        public int[] Qualifications { get; set; }
        public int[] Locations { get; set; }
        public int[] Certifications { get; set; }
        public int[] Skills { get; set; }
        public int[] Districts { get; set; }
        public int[] Maritalstatus { get; set; }
        public int[] Gender { get; set; }
        public int[] ResumeSource { get; set; }
        
        public Operators YSAboveOrBelow { get; set; }
        public int? YOSFrom { get; set; }
        public int? YOSTo { get; set; }
        public Operators AgeAboveOrBelow { get; set; }
        public int? AgeFrom { get; set; }
        public int? AgeTo { get; set; }
        public int SearchId { get; set; }
        public int Take { get; set; }
        public int Skip { get; set; }
        public string SortColumn { get; set; }
        public string SortOrder { get; set; }
        public string Search { get; set; }
        public int start { get; set; }
        public int length { get; set; }
        public string searchValue { get; set; }
        public int draw { get; set; }





    }
}
