using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace Bissoft.CouncilCMS.Web.Core
{
    public class CmsPrincipal : IPrincipal
    {
        private string[] roles;
        private string[] premissions;

        public CmsPrincipal(CmsIdentity identity)
        {
            Identity = identity;
        }

        public CmsPrincipal(CmsIdentity identity, string[] premissions)
        {
            Identity = identity;
            this.premissions = new string[premissions.Length];
            premissions.CopyTo(this.premissions, 0);
            Array.Sort(this.premissions);
        }

        public CmsPrincipal(CmsIdentity identity, string[] premissions, string[] roles)
        {
            Identity = identity;

            this.roles = new string[roles.Length];
            roles.CopyTo(this.roles, 0);
            Array.Sort(this.roles);

            this.premissions = new string[premissions.Length];
            premissions.CopyTo(this.premissions, 0);
            Array.Sort(this.premissions);
        }

        public CmsIdentity Identity { get; private set; }
        public List<String> Roles { get { return roles.ToList(); } }
        public List<String> Premissions { get { return premissions.ToList(); } }
        IIdentity IPrincipal.Identity { get
            {
                try
                {

                    return this.Identity;
                }
                catch (Exception e)
                {
                    return null;
                }
            }

        }

        public bool HasPremission(string premission)
        {
            var HasPremission = false;

            if (premission != null)
            {
                HasPremission = Array.BinarySearch(premissions, "*") >= 0 ? true : (Array.BinarySearch(premissions, premission) >= 0 ? true : false);
            }

            return HasPremission;
        }
        public bool HasPremissions(string pattern)
        {
            var HasPremission = false;

            if (pattern != null)
            {
                HasPremission = Array.BinarySearch(premissions, "*") >= 0 ? true : (premissions.Any(x => x.Contains(pattern)));
            }

            return HasPremission;
        }

        public bool IsInRole(string role)
        {
            var IsInRole = false;

            if (role != null)
            {
                IsInRole = (Array.BinarySearch(roles, role) >= 0 ? true : false);
            }

            return IsInRole;
        }

    }
}