using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace Bissoft.CouncilCMS.Web.Core
{
    public class CmsIdentity : IIdentity
    {
        public CmsIdentity(string name, string authenticationType, bool isAuthenticated, Int32 userId, String email)
        {
            Name = name;
            AuthenticationType = authenticationType;
            IsAuthenticated = isAuthenticated;
            UserId = userId;
            Email = email;
        }

        public string Name { get; private set; }
        public string AuthenticationType { get; private set; }
        public bool IsAuthenticated { get; private set; }
        public Int32 UserId { get; private set; }
        public string Email { get; set; }       
    }
}