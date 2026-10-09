using Hrms.Lite.Shared.General;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hrms.Lite.Shared.DataBank.DTO
{
    public class JoiningConfirmation
    {
        public Enrollment Enrollment { get; set; }

        public string DOB_Password { get; set; }
        public string MOBILE_password { get; set; }
        public string company { get; set; }
        public List<AlreadyExists_JoiningConfirmation> AlreadyExists_JoiningConfirmation { get; set; }

    }
    public class AlreadyExists_JoiningConfirmation
    {
        public int Code { get; set; }
        public string Message { get; set; }
    }
}

