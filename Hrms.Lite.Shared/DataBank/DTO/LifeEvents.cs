using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.DTO
{
    public class LifeEvents
    {        
        public List<LifeEventsCelebrityList> LifeEventsCelebrityList { get; set; }
        public List<LifeEventsEventList> LifeEventsEventList { get; set; }
        public Guid EventGI { get; set; }
        public File Attachment { get; set; }
        public string EventName { get; set; }
        public string Description { get; set; }
        public string EventTime { get; set; }
        public DateTime? EventDate { get; set; }
        public string Mode { get; set; }

    }
    public class LifeEventsCelebrityList
    {

        public string CelebrityType { get; set; }
        public DateTime CelibrityDate { get; set; }

        public string EmpId { get; set; }
        public Guid EmployeeGuid { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string Department { get; set; }
        public decimal YearOFExperience { get; set; }
        public int Age { get; set; }
        public string Mobile { get; set; }
        public string MailId { get; set; }
        public string Remarks { get; set; }
        public string AbsoluteUri { get; set; }
        public string Base64string { get; set; }
        public string FileName { get; set; }

    }
    public class LifeEventsEventList
    {
        public Guid EventGI { get; set; }
        public File Attachment { get; set; }
        public string EventName { get; set; }
        public string Description { get; set; }
        public string EventTime { get; set; }
        public DateTime? EventDate { get; set; }
        public string AbsoluteUri { get; set; }
        public string FileName { get; set; }
        public string Base64string { get; set; }

    }
}

