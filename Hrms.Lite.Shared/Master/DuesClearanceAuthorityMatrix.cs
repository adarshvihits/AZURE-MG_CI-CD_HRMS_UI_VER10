using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.Master
{

    public class DuesClearanceAuthorityMatrix
    {

        public int SlNo { get; set; }
        public int LevelCode { get; set; }
        public int DueCode { get; set; }
        public string LevelName { get; set; }
        public int PositionCode { get; set; }
        public string PositionName { get; set; }
        public string EmployeName { get; set; }
        public List<DuesClearanceAuthorityMatrix> DuesClearanceAuthorityMatrixList { get; set; }
        public List<ClearanceVsAuthority> ClearanceVsAuthority { get; set; }
        public Filter SliderFilter { get; set; }
    }
    public class ClearanceVsAuthority
    {
        public int SlNo { get; set; }
        public int LevelCode { get; set; }
        public int PositionCode { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeId { get; set; }
        public string PositionName { get; set; }
        public string PositionId { get; set; }
        public string LevelName { get; set; }
        public bool Applicability { get; set; }
    }
}
