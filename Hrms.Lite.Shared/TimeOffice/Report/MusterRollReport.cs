using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.TimeOffice.Report
{
    public class MusterRollReport
    {
        public List<string> Head { get; set; }
        public List<Rowss> Rows { get; set; }
        public int Month { get; set; }
        public Filter filter { get; set; }

    }
    public class Rowss
    {
        public List<string> Columns { get; set; }
    }
}
