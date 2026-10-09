using System.Collections.Generic;

namespace Hrms.Lite.Shared.Constants
{
   public class FileLocations
    {
        private static Dictionary<string, string> Locations = new Dictionary<string, string>()
        {

            { "EMPLOYEE_PHOTO","employee/image"},
            { "ADDRESS_DOC", "employee/address"},
            { "JOB_TRANSITION_DOCUMENT", "employee/jobtransitiondocument"},
            { "PREVIOUS_EMPLOYER_DOCUMENT", "employee/previousemployerdocument"},
            {"SKILLS_DOCUMENT" , "employee/skillsdocument"},
              {"LEAVE_DOCUMENT" , "employee/leavedocument"}
        };
        public static string Get(string key)
        {
            string msg = null;
            Locations.TryGetValue(key, out msg);
            return msg ?? string.Empty;
        }
    }
}
