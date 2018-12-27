using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bissoft.CouncilCMS.Web.Core.ExternalLogins
{
    internal class FacebookAccessToken
    {
        public String access_token { get; set; }
        public String token_type { get; set; }
        public String expires_in { get; set; }
    }
     //trot
     // {
     //     "access_token": {access-token}, 
     //     "token_type": {type},
     //     "expires_in":  {seconds-til-expiration}
     // }
}
