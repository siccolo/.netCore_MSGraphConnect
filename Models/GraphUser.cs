using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models
{
    public class GraphUser
    {
        public string Department { get; private set; }
        public string  DisplayName { get; private set; }
        public string GivenName { get; private set; }
        public string JobTitle { get; private set; }
        public string Mail { get; private set; }
        public string Photo_Base64 { get; private set; }

        public GraphUser(string department, string displayName, string givenName, string jobTitle, string mail, string photo_base64 = null)
        {
            Department = department;
            DisplayName = displayName;
            GivenName = givenName;
            JobTitle = jobTitle;
            Mail = mail;
            Photo_Base64 = photo_base64;
        }
    }
}
