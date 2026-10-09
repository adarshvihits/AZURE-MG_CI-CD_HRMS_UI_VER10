using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.Master
{
   public  class QuotesOfDay
    {
        public int ThoughtCode { get; set; }
        public Guid ThoughtGI { get; set; }
        public DateTime Date { get; set; }
        public string Thought { get; set; }
        public int FYCODE { get; set; }
        public string FYNAME { get; set; }

        public int From { get; set; }
        public int To { get; set; }
        //public int totalRecords { get; set; }

        //public int active { get; set; }


        public List<QuotesOfDay> QuotesOfDayDetails { get; set; }
    }
}
