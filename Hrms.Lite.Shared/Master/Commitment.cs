using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.Generic.Master
{
    public class Commitment : MasterBase
    {
        public int ActiveCount { get; set; }
        public int InactiveCount { get; set; }
        public int PendingCount { get; set; }
        public List<Commitment> CommitmentList { get; set; }
        public int CommitmentCode { get; set; }
        public string CommitmentType { get; set; }
        public CommitmentTypeCbo CommitmentTypeCbo { get; set; }

    }
    public class CommitmentTypeCbo : MasterBase
    { }
}
