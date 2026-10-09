using Hrms.Lite.Shared.DataBank.Generic.Master;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.General
{
  public class MasterBase
    {
        public int? Code { get; set; }
        public Guid? LogGI { get; set; }
        public Guid? GI { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string UserCode { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public string Mode { get; set; }
        public string ApproveMode { get; set; }
        public bool Applicable { get; set; }
        public bool MakerChecker { get; set; }
        public bool EditActive { get; set; }
        public bool IsMandatory { get; set; }
        public decimal Duration { get; set; }
        public int CtcApplicable { get; set; }

    }
}
