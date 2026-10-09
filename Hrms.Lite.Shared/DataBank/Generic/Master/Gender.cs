using Hrms.Lite.Shared.General;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
namespace Hrms.Lite.Shared.DataBank.Generic.Master
{ 
    public class Gender : MasterBase
    {
        public SelectList DropdownData { get; set; }
    }
}
