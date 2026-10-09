using Hrms.Lite.Shared.TDS.Generic.Master;
using System;
using System.Collections.Generic;
using System.Text;
using Hrms.Lite.Shared.PayRoll.Generic.Master;

namespace Hrms.Lite.Shared.TDS
{
  public  class InvestmentDeclarationDropdown
    {
        public List<City> City { get; set; }

        public List<PayrollMonth> From { get; set; }
        public List<PayrollMonth> To { get; set; }
        public List<RelationShip> RelationShip { get; set; }
        public List<YesorNo> YesorNo { get; set; }
        public List<RelationShip> MedicalCoverage { get; set; }
        public List<NatureofDisability> NatureofDisability { get; set; }
        public List<CriticalAilments> CriticalAilments { get; set; }
        public List<PercentageofDisability> PercentageofDisability { get; set; }
        public List<RelationShip> DependentRelationShip { get; set; }
        public List<LocationStatus> LocationStatus { get; set; }
        public List<OwnerStatus> OwnerStatus { get; set; }
        public List<Purpose> Purpose { get; set; }

    }
}
