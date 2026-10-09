using Hrms.Lite.Shared.DataBank.Generic.Master;
using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.DTO
{
    public class TalentPool
    {      
        public List<Classifications> Skills { get; set; }
        public List<Graph> SkillGraph { get; set; }
        public List<Graph> SkillProficiencyGraph { get; set; }  
        public List<Graph> EducationGraph { get; set; }   
        public List<Graph> LanguagesGraph { get; set; }
        public List<Classifications> SkillProficiency { get; set; }
        public List<Classifications> Education { get; set; }
        public List<Classifications> Languages { get; set; }
        public Gender Gender { get; set; }
        public int[] GenderCodes { get; set; }
        public Operators AgeAboveOrBelow { get; set; }        
        public int? AgeFrom { get; set; }
        public int? AgeTo { get; set; }
        public Operators YSAboveOrBelow { get; set; }        
        public int? YOSFrom { get; set; }
        public int? YOSTo { get; set; }
        public List<TalentPoolList> TalentPoolList { get; set; }
        public Filter Filter { get; set; }
        public bool AllSkills { get; set; }
        public bool AllSkillProficiency { get; set; }
        public bool AllEducation { get; set; }
        public bool AllLanguages { get; set; }

    }
    public class Classifications
    {
        public Guid LanguageLogGI { get; set; }
        public Guid HobbiesLogGI { get; set; }
        public int CODE { get; set; }
        public string NAME { get; set; }
        public bool APPLICABILITY { get; set; }
        public bool Read { get; set; }
        public bool Write { get; set; }
        public bool Speak { get; set; }
        public int COUNTS { get; set; }
    }
    public class Graph
    {
        public Guid LanguageLogGI { get; set; }
        public Guid HobbiesLogGI { get; set; }
        public int CODE { get; set; }
        public string NAME { get; set; }
        public bool APPLICABILITY { get; set; }
        public bool Read { get; set; }
        public bool Write { get; set; }
        public bool Speak { get; set; }
        public int COUNTS { get; set; }
    }
    public class TalentPoolList
    {
        public Guid EmployeeGI { get; set; }
        public string EmployeeID { get; set; }
        public string FullName { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public string Location { get; set; }
        public int totalRecords { get; set; }
        public int active { get; set; }
    }

  
}
