using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.Master
{
   public class UserApplicabilityAdvancedUser
    {
        public int GroupingCode { get; set; }
        public int ClassificationCode { get; set; }
        public int UserCode { get; set; }
        public string Role { get; set; }
        public string UserName { get; set; }
        public string Mobile { get; set; }
        public string ActiveFrom { get; set; }
        public string ActiveTo { get; set; }
        public bool Applicable { get; set; }
        public string Mode { get; set; }
        public List<UserApplicabilityAdvancedUser> UserApplicabilityAdvancedUserDetails { get; set; }
    }
}
