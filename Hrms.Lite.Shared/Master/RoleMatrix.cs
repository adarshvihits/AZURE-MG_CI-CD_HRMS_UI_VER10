using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;
namespace Hrms.Lite.Shared.Master
{
  public  class RoleMatrix
    {
        public int Empcode { get; set; }
        public Role Role { get; set; }
        public int ParameterCode { get; set; }
        public string ParameterName { get; set; }
        public string ColumnCaption { get; set; }
        public string Authority { get; set; }
        public string Position { get; set; }
        public int PositionCode { get; set; }
        public string Section { get; set; }
        public string Grade { get; set; }

        public EmployeeHeader EmployeeHeader { get; set; }
        public List<RoleMatrix> RoleMatrixList { get; set; }
    }
}
