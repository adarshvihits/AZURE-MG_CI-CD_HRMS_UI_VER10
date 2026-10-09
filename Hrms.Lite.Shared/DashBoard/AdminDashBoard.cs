using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DashBoard
{
   public class AdminDashBoard
    {
        public int TotalTask { get; set; }
        public int TaskCompleted { get; set; }
        public int RenewalDocument { get; set; }
        public int PhotoGallery { get; set; }
        public SummaryGraph SummaryGraph { get; set; }
       public ToDo ToDo { get; set; }
         public List<photogallaryCount> GetPhotogallaries { get; set; }

    }
    public class SummaryGraph
    {
        public decimal Yellow { get; set; }
        public decimal Green { get; set; }
        public decimal Red { get; set; }
  
    }
    public class ToDo
    {
        public decimal Yellow { get; set; }
        public decimal Green { get; set; }
        public decimal Red { get; set; }

    }
    public class photogallaryCount
    {
        public string Category { get; set; }
        public string AbsoluteUri { get; set; }
        public string FileName { get; set; }
        public string Description { get; set; }
    }

}
